using PiCamera;

namespace PhotographyOfMovingObjects;

/// <summary>
/// https://github.com/dotnet/iot/blob/main/src/devices/Camera/README.md
/// </summary>
public static class Camera
{
    public static event PiCamera.Camera.CameraEvent? PictureTaken;
    public static event PiCamera.Camera.CameraEvent? PictureReady;

    private static PiCamera.Camera _picamera = null!;
    public static RpicamArgs CameraArgs
    {
        get => _args;
        set
        {
            _args = value;
            CreateCamera();
        }
    }

    private static RpicamArgs _args = null!;


    static Camera()
    {
        RpicamArgs a = new();
        a.Encoding(Encoding.Jpeg);
        a.Output(Output.File, "%d.jpg");
        CameraArgs = a;
    }

    private static void CreateCamera()
    {
        _picamera = new (RpiCameraApp.RpicamStill, CameraArgs);
        _picamera.PictureTaken += () => PictureTaken?.Invoke();
        _picamera.PictureReady += () => PictureReady?.Invoke();
    }

    /// <summary>
    /// Takes a picture 
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <param name="delay">Delay before taking the picture</param>
    public static byte[] TakePicture(CancellationToken? ct = null, TimeSpan? delay = null)
    {
        if(delay is { } d)
            Thread.Sleep(d);
        
        _picamera.TakePicture();
        byte[] picture = _picamera.GetPicture();
        
        Console.WriteLine("Camera!");

        return picture;
    }

    public static byte[] LatestPicture => _picamera.GetPicture();
}