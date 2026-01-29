using System.Net;
using AdvancedSharpAdbClient;
using AdvancedSharpAdbClient.DeviceCommands;
using AdvancedSharpAdbClient.Models;

namespace CameraTrigger;

public static class Camera
{
    private static readonly AdbServer AdbServer = new();
    private static readonly AdbClient AdbClient = new();
    private static DeviceClient? Device = null;
    public static bool Connected => Device is not null;

    public const string DefaultCameraApp = "com.google.android.GoogleCameraEng";
    private const int CameraStartupDelayMs = 200;

    static Camera()
    {
        if (!AdbServer.Instance.GetStatus().IsRunning)
            AdbServer.StartServer("/usr/bin/adb", false);
        AdbClient.Connect(IPAddress.Loopback);
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
        Device = new (AdbClient, (DeviceData)data);
        return true;
    }

    public static bool StartCameraApp(string cameraAppName = DefaultCameraApp)
    {
        if (Device is null)
        {
            Console.WriteLine("No device connected!");
            return false;
        }
        Device.StartApp(cameraAppName);
        Thread.Sleep(CameraStartupDelayMs);
        return CameraRunning(cameraAppName);
    }

    public static bool CameraRunning(string cameraAppName = DefaultCameraApp)
    {
        if (Device is null)
        {
            Console.WriteLine("No device connected!");
            return false;
        }
        AppStatus appStatus = Device.GetAppStatus(cameraAppName);
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
        DateTime startTime = DateTime.Now;
        if (Device is null)
        {
            Console.WriteLine("No device connected!");
            return false;
        }
        Thread.Sleep(delay.Subtract(DateTime.Now.Subtract(startTime)));
        Device.SendKeyEvent("KEYCODE_CAMERA");
        return true;
    }
}