using PiCamera;

namespace PhotographyOfMovingObjects;

/// <summary>
/// https://github.com/dotnet/iot/blob/main/src/devices/Camera/README.md
/// </summary>
public static class Camera
{
    public static event PictureTakeEvent? PictureTaken;
    public delegate void PictureTakeEvent(byte[] data);

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
    /// <param name="ct">Cancellation Token</param>
    /// <param name="delay">Delay before taking the picture</param>
    public static async Task<byte[]> TakePicture(CancellationToken? ct = null, TimeSpan? delay = null)
    {
        if(delay is { } d)
            Thread.Sleep(d);
        
        _picamera.TakePicture();
        byte[] picture = await _picamera.GetPicture(ct);

        PictureTaken?.Invoke(picture);
        Console.WriteLine("Camera!");

        return picture;
    }
}