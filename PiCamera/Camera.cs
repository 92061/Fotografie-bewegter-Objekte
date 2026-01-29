using System.Diagnostics;
using Mono.Unix;
using Mono.Unix.Native;

namespace PiCamera;

public class Camera : IDisposable
{
    private readonly Process _rpiCamProc = new ();
    private readonly RpicamArgs _args;
    
    public event CameraEvent? PictureTaken;
    public event CameraEvent? PictureReady;
    public delegate void CameraEvent();
    
    public Camera(RpiCameraApp app, RpicamArgs args)
    {
        this._args = args;
        _rpiCamProc.Exited += RpiCamProcOnExited;
        
        ProcessStartInfo cameraProcessInfo = new(app.AsString(), args.GetArgsString)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        _rpiCamProc.StartInfo = cameraProcessInfo;
        
        Console.WriteLine($"{cameraProcessInfo.FileName} {cameraProcessInfo.Arguments}");
        if(!_rpiCamProc.Start())
            throw new Exception("Could not start Camera process");
        else
        {
            if(args.Output is Output.Stream)
                _copyDataThread.Start(this);
            Console.WriteLine($"PID: {_rpiCamProc.Id}");
            Thread.Sleep(1000); //Startup delay
        }
    }

    private uint _newPictureIndex = 0;
    private readonly MemoryStream _lastPicture = new();
    private readonly Thread _copyDataThread = new (o =>
    {
        Camera? c = o as Camera;
        c?._rpiCamProc.StandardOutput.BaseStream.CopyTo(c._lastPicture);
    });

    public bool TakePicture()
    {
        if (_args.Output is Output.Stream)
        {
            _lastPicture.Position = 0;
            _lastPicture.SetLength(0);
        }

        bool ret;
        lock (_rpiCamProc)
        {
            // https://www.raspberrypi.com/documentation/computers/camera_software.html#signal
            ret = _rpiCamProc.SendSignal(Signum.SIGUSR1);
            PictureTaken?.Invoke();
            _newPictureIndex++;
        
            if(_args.Output is Output.Stream)
                Thread.Sleep(1000);
            else if (_args.Output is Output.File)
            {
                while(!File.Exists(LatestFilePath) || !UnixHelper.IsWritten(LatestFilePath))
                    Thread.Sleep(10);
            }
            else
                Thread.Sleep(100);
            
            PictureReady?.Invoke();
        }
        
        return ret;
    }

    public byte[] GetPicture()
    {
        if (_args.Output is Output.File)
        {
            lock (LatestFilePath)
            {
                Console.WriteLine($"Getting file {LatestFilePath}");
                if (File.Exists(LatestFilePath))
                    return File.ReadAllBytes(LatestFilePath);
                else
                    Console.WriteLine($"File does not exist {LatestFilePath}");
            }
        }

        if (_args.Output is Output.Stream)
            return _lastPicture.ToArray();
        
        return [];
    }

    private string LatestFilePath => _args.OutputAdditional is { Length: > 0 } filePath
        ? filePath.Replace("%d", $"{_newPictureIndex - 1}")
        : throw new ArgumentException("Missing additional argument for File-output");

    private void RpiCamProcOnExited(object? sender, EventArgs e)
    {
        Environment.Exit(_rpiCamProc.ExitCode);
    }
    
    public void Dispose()
    {
        _rpiCamProc.SendSignal(Signum.SIGUSR2);
        _rpiCamProc.WaitForExit();
        _rpiCamProc.Dispose();
        if(_copyDataThread.IsAlive)
            _copyDataThread.Join();
    }
}

internal static class UnixHelper
{
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