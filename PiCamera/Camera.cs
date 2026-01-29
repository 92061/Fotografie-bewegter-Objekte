using System.Diagnostics;
using Mono.Unix;
using Mono.Unix.Native;

namespace PiCamera;

public class Camera : IDisposable
{
    private readonly Process _rpiCamProc = new ();
    
    public Camera(RpiCameraApp app, RpicamArgs args)
    {
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
            Console.WriteLine($"PID: {_rpiCamProc.Id}");
            _copyDataThread.Start(this);
            Thread.Sleep(1000); //Startup delay
        }
    }

    private readonly MemoryStream _lastPicture = new();
    private readonly Thread _copyDataThread = new (o =>
    {
        Camera? c = o as Camera;
        c?._rpiCamProc.StandardOutput.BaseStream.CopyTo(c._lastPicture);
    });

    public bool TakePicture()
    {
        _lastPicture.Position = 0;
        _lastPicture.SetLength(0);
        // https://www.raspberrypi.com/documentation/computers/camera_software.html#signal
        bool ret = _rpiCamProc.SendSignal(Signum.SIGUSR1);
        Thread.Sleep(200);
        return ret;
    }

    public async Task<byte[]> GetPicture(Output output, string? additional = null, CancellationToken? ct = null)
    {
        if (output is Output.File
            && additional is { Length: > 0 } filePath
            && File.Exists(filePath))
            return await File.ReadAllBytesAsync(filePath, ct ?? CancellationToken.None);

        if (output is Output.Stream)
            return _lastPicture.ToArray();
        
        return [];
    }

    private void RpiCamProcOnExited(object? sender, EventArgs e)
    {
        Environment.Exit(_rpiCamProc.ExitCode);
    }
    
    public void Dispose()
    {
        _rpiCamProc.SendSignal(Signum.SIGUSR2);
        _rpiCamProc.WaitForExit();
        _rpiCamProc.Dispose();
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
}