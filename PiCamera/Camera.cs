using System.Diagnostics;
using Mono.Unix;
using Mono.Unix.Native;

namespace PiCamera;

public class Camera : IDisposable
{
    private readonly Process _rpiCamProc;
    private readonly RpicamArgs _args;
    
    /// <summary>
    /// Gets invoked when the signal to capture a picture has been sent
    /// </summary>
    public event CameraEvent? PictureTaken;
    /// <summary>
    /// Gets invoked when the Picture has been taken
    /// </summary>
    public event CameraEvent? PictureReady;
    public delegate void CameraEvent();
    
    /// <summary>
    /// Create a new process of the specified app with the provided arguments-list
    /// </summary>
    /// <exception cref="Exception">Camera-app could not be started.</exception>
    public Camera(RpiCameraApp app, RpicamArgs args)
    {
        this._rpiCamProc = new Process();
        this._args = args;
        
        ProcessStartInfo cameraProcessInfo = new(app.AsString(), args.GetArgsString)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        _rpiCamProc.StartInfo = cameraProcessInfo;
        
        Console.WriteLine($"{cameraProcessInfo.FileName} {cameraProcessInfo.Arguments}");
        if (!_rpiCamProc.Start())
        {
            throw new Exception("Could not start Camera process");
        }
        
        if(args.Output is Output.Stream)
            _copyDataThread.Start(this); // Start the thread that copies the data written to stdout if output type is Stream
        
        Console.WriteLine($"PID: {_rpiCamProc.Id}");
        Thread.Sleep(500); //Startup delay
    }

    /// <summary>
    /// Index of the latest picture (used for file %d)
    /// </summary>
    private uint _newPictureIndex = 0;
    /// <summary>
    /// Data of the lastest picture, if output-mode stream is used
    /// </summary>
    private readonly MemoryStream _lastPicture = new();
    /// <summary>
    /// If output-mode is Stream, copy data from stdout to _lastPicture
    /// </summary>
    private readonly Thread _copyDataThread = new (o =>
    {
        Camera? c = o as Camera;
        c?._rpiCamProc.StandardOutput.BaseStream.CopyTo(c._lastPicture);
    });

    /// <summary>
    /// Take a picture. <br />
    /// Sends the signal to the camera-process to take a picture.
    /// </summary>
    /// <exception cref="Exception">Signal could not be sent.</exception>
    public void TakePicture()
    {
        // If output-mode is Stream, reset the Stream of the latest picture
        if (_args.Output is Output.Stream)
        {
            _lastPicture.Position = 0;
            _lastPicture.SetLength(0);
        }

        // Lock the process, so separate threads can not try and take more than one picture at a time
        lock (_rpiCamProc)
        {
            // https://www.raspberrypi.com/documentation/computers/camera_software.html#signal
            if (!_rpiCamProc.SendSignal(Signum.SIGUSR1))
                throw new Exception("Failed sending Signal to capture picture");
            
            PictureTaken?.Invoke();
            _newPictureIndex++;
        
            // Wait for picture to be ready (taken and saved)
            Console.WriteLine("Waiting for picture...");
            if(_args.Output is Output.Stream)
                Thread.Sleep(1000); // TODO: Calculate or get the correct value
            else if (_args.Output is Output.File)
            {
                Console.WriteLine(LatestFilePath);
                while(!File.Exists(LatestFilePath) || !UnixHelper.IsWritten(LatestFilePath))
                    Thread.Sleep(10);
            }
            else
                Thread.Sleep(100); // TODO: Calculate or get the correct value
            
            Console.WriteLine("Picture is ready!");
            PictureReady?.Invoke();
        }
    }

    /// <summary>
    /// Get the latest picture that has been taken
    /// </summary>
    public byte[] GetPicture()
    {
        if (_args.Output is Output.File)
        {
            Console.WriteLine($"Getting file {LatestFilePath}");
            if (File.Exists(LatestFilePath))
                return File.ReadAllBytes(LatestFilePath);
            else
                Console.WriteLine($"File does not exist {LatestFilePath}");
        }

        if (_args.Output is Output.Stream)
            return _lastPicture.ToArray();
        
        return [];
    }

    /// <summary>
    /// Filename of the latest picture, if output-mode is File
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    private string LatestFilePath => _args.OutputAdditional is { Length: > 0 } filePath
        ? filePath.Replace("%d", $"{_newPictureIndex - 1}")
        : throw new ArgumentException("Missing additional argument for File-output");
    
    public void Dispose()
    {
        _rpiCamProc.SendSignal(Signum.SIGUSR2);
        _rpiCamProc.WaitForExit();
        _rpiCamProc.Dispose();
        if(_copyDataThread.IsAlive)
            _copyDataThread.Join();
    }
}

/// <summary>
/// Helperclass to send Signals to a POSIX-Process
/// </summary>
internal static class UnixHelper
{
    /// <summary>
    /// Send Signal to Process
    /// </summary>
    /// <param name="process"></param>
    /// <param name="signal"></param>
    /// <returns>true if signal was sent</returns>
    public static bool SendSignal(this Process process, Signum signal)
    {
        if (Syscall.kill(process.Id, signal) == 0)
        {
            Console.WriteLine($"Sent {signal}");
            return true;
        }
        else
        {
            Errno errno = Stdlib.GetLastError();
            Console.WriteLine($"Failed to send {signal}, errno = {errno} ({UnixMarshal.GetErrorDescription(errno)})");
            Console.WriteLine(process.Id);
            Console.WriteLine(process.ExitCode);
            return false;
        }
    }

    public static bool IsWritten(string filePath)
    {
        FileInfo fi = new (filePath);
        return fi.LastWriteTimeUtc.Add(TimeSpan.FromMilliseconds(10)) > DateTime.UtcNow;
    }
}