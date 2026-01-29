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
        Device = new (AdbClient, (DeviceData)data);
        return true;
    }

    public static bool StartCameraApp(string cameraAppName = "com.google.android.GoogleCameraEng")
    {
        if (Device is null)
        {
            Console.WriteLine("No device connected!");
            return false;
        }
        Device.StartApp(cameraAppName);
        return true;
    }

    public static bool TakePicture()
    {
        if (Device is null)
        {
            Console.WriteLine("No device connected!");
            return false;
        }
        Device.SendKeyEvent("KEYCODE_CAMERA");
        return true;
    }
}