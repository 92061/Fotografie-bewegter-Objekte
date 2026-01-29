using Iot.Device.Camera;
using Iot.Device.Camera.Settings;
using Iot.Device.Common;

namespace PhotographyOfMovingObjects;

/// <summary>
/// https://github.com/dotnet/iot/blob/main/src/devices/Camera/README.md
/// </summary>
public static class Camera
{
    private static readonly ProcessSettings ProcessSettings = ProcessSettingsFactory.CreateForLibcamerastillAndStderr();

    /// <summary>
    /// Camera to use for taking pictures
    /// </summary>
    public static CameraInfo? SelectedCamera
    {
        get => _camera;
        set
        {
            _camera = value;
            _procArgs = CreateArgs();
        }
    }
    private static CameraInfo? _camera = null;

    /// <summary>
    /// Min 1ms
    /// </summary>
    public static int DelayMs
    {
        get => _delay;
        set
        {
            _delay = value;
            _procArgs = CreateArgs();
        }
    }

    private static int _delay = 1;
    
    // ReSharper disable once InconsistentNaming
    private static readonly ProcessRunner proc = new (ProcessSettings);
    private static string[] _procArgs = [];


    static Camera()
    {
        SelectedCamera = GetCameras().Result.FirstOrDefault();
    }

    /// <summary>
    /// Takes a picture 
    /// </summary>
    /// <param name="delay">Time to wait before taking a picutre</param>
    /// <param name="stream">Stream to write the picture to</param>
    public static async Task TakePictureTask(Stream stream)
    {
        await proc.ExecuteAsync(_procArgs, stream);
        Console.WriteLine("Camera!");
    }

    public static async Task<IEnumerable<CameraInfo>> GetCameras()
    {
        using ProcessRunner proc = new (ProcessSettings);
        string text = await proc.ExecuteReadOutputAsStringAsync(string.Empty);
        return await CameraInfo.From(text);
    }

    private static string[] CreateArgs()
    {
        if (_camera is null)
            return [];

        (int width, int height) = CameraResolution(_camera);

        CommandOptionsBuilder builder = new CommandOptionsBuilder()
            .WithCamera(_camera.Index)
            .WithResolution(width, height)
            .WithTimeout(_delay);
        return builder.GetArguments();
    }

    private static (int width, int height) CameraResolution(CameraInfo info)
    {
        string maxRes = info.MaxResolution;
        string[] split = maxRes.Split(',');
        return (int.Parse(split[0]), int.Parse(split[1]));
    }
}