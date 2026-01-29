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
            _args.Output(Output.File, "%d.jpg");
            CreateCamera();
        }
    }
    private static RpicamArgs _args = null!;


    static Camera()
    {
        CameraArgs = new();
    }

    private static void CreateCamera()
    {
        _picamera?.Dispose();
        _picamera = new (RpiCameraApp.RpicamStill, CameraArgs);
        _picamera.PictureTaken += () => PictureTaken?.Invoke();
        _picamera.PictureReady += () => PictureReady?.Invoke();
    }

    /// <summary>
    /// Takes a picture 
    /// </summary>
    /// <param name="delay">Delay before taking the picture</param>
    public static void TakePicture(TimeSpan? delay = null)
    {
        if(delay is { } d)
            Thread.Sleep(d);
        
        _picamera.TakePicture();
    }

    public static byte[] LatestPicture => _picamera.GetPicture();
}