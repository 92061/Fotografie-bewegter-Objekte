using PiCamera;

namespace PhotographyOfMovingObjects;

/// <summary>
/// https://github.com/dotnet/iot/blob/main/src/devices/Camera/README.md
/// </summary>
public static class Camera
{
    /// <summary>
    /// Min 1ms
    /// </summary>
    public static int DelayMs = 1;
    
    public static event PictureTakeEvent? PictureTaken;
    public delegate void PictureTakeEvent(Stream stream);

    private static PiCamera.Camera _picamera;
    public static RpicamArgs CameraArgs
    {
        get => _args;
        set
        {
            _args = value;
            _picamera = new PiCamera.Camera(RpiCameraApp.RpicamStill, CameraArgs);
        }
    }

    private static RpicamArgs _args;


    static Camera()
    {
        RpicamArgs a = new();
        a.Encoding(Encoding.Jpeg);
        a.Output(Output.File, "%d.jpg");
        _args = a;
        _picamera = new (RpiCameraApp.RpicamStill, CameraArgs);
    }

    /// <summary>
    /// Takes a picture 
    /// </summary>
    /// <param name="stream">Stream to write the picture to</param>
    /// <param name="ct">Cancellation Token</param>
    public static async Task TakePictureTask(Stream stream, CancellationToken ct)
    {
        Thread.Sleep(DelayMs);
        _picamera.TakePicture();
        byte[] picture = await _picamera.GetPicture();
        stream.Write(picture);

        PictureTaken?.Invoke(stream);
        Console.WriteLine("Camera!");
    }
}