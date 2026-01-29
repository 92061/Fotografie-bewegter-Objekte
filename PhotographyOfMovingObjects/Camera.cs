using System.Net;
using AdvancedSharpAdbClient;
using AdvancedSharpAdbClient.DeviceCommands;
using AdvancedSharpAdbClient.Models;

namespace PhotographyOfMovingObjects;

public static class Camera
{
    private static readonly AdbServer AdbServer = new();
    private static readonly AdbClient AdbClient = new();
    private static DeviceClient? _device = null;
    public static bool Connected => _device is not null;

    public const string DefaultCameraApp = "com.google.android.GoogleCameraEng";
    private const int CameraStartupDelayMs = 200;

    static Camera()
    {
        if (!AdbServer.Instance.GetStatus().IsRunning)
            AdbServer.StartServer("/usr/bin/adb", false);
        AdbClient.Connect(IPAddress.Loopback);
        Connect();
    }

    public static bool Connect()
    {
        DeviceData? data = AdbClient.GetDevices().FirstOrDefault();
        if (data is null)
            return false;
        switch (data.Value.State)
        {
            case DeviceState.Online:
                break;
            default:
                Console.WriteLine(data.Value.State);
                return false;
        }
        _device = new (AdbClient, (DeviceData)data);
        return true;
    }

    public static bool StartCameraApp(string cameraAppName = DefaultCameraApp)
    {
        if (_device is null)
        {
            Console.WriteLine("No device connected!");
            return false;
        }
        _device.StartApp(cameraAppName);
        Thread.Sleep(CameraStartupDelayMs);
        return CameraRunning(cameraAppName);
    }

    public static bool CameraRunning(string cameraAppName = DefaultCameraApp)
    {
        if (_device is null)
        {
            Console.WriteLine("No device connected!");
            return false;
        }
        AppStatus appStatus = _device.GetAppStatus(cameraAppName);
        return appStatus is not AppStatus.Stopped;
    }

    public static Task<bool> TakePicture(TimeSpan delay)
    {
        if (!CameraRunning())
        {
            Console.WriteLine("Camera not running!");
            return new (() => false);
        }
        
        return new Task<bool>(() => TakePictureInternal(delay));
    }

    private static bool TakePictureInternal(TimeSpan delay)
    {
        if (_device is null)
        {
            Console.WriteLine("No device connected!");
            return false;
        }
        Thread.Sleep(delay);
        _device.SendKeyEvent("KEYCODE_CAMERA");
        Console.WriteLine("Camera!");
        return true;
    }
}