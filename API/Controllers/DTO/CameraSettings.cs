using PiCamera;

namespace Project.Controllers.DTO;

public sealed record CameraSettings
{
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Mode" path="/summary" />
    /// </summary>
    public string? Mode { get; init; }
    
    /// <summary>
    /// Width of the output picture
    /// </summary>
    public int? Width { get; init; }
    
    /// <summary>
    /// Height of the output picture
    /// </summary>
    public int? Height { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Hflip" path="/summary" />
    /// </summary>
    public bool? Hflip { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Vflip" path="/summary" />
    /// </summary>
    public bool? Vflip { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Rotate180" path="/summary" />
    /// </summary>
    public bool? Rotate180 { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.ShutterSpeed" path="/summary" />
    /// </summary>
    public int? ShutterSpeed { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Gain" path="/summary" />
    /// </summary>
    public float? Gain { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Metering" path="/summary" />
    /// </summary>
    public MeteringMode? Metering { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Exposure" path="/summary" />
    /// </summary>
    public ExposureMode? Exposure { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Ev" path="/summary" />
    /// </summary>
    public float? Ev { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Awb" path="/summary" />
    /// </summary>
    public AwbMode? Awb { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Brightness" path="/summary" />
    /// </summary>
    public float? Brightness { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Contrast" path="/summary" />
    /// </summary>
    public float? Contrast { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Saturation" path="/summary" />
    /// </summary>
    public float? Saturation { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Sharpness" path="/summary" />
    /// </summary>
    public float? Sharpness { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Denoise" path="/summary" />
    /// </summary>
    public DenoiseMode? Denoise { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.AutofocusMode" path="/summary" />
    /// </summary>
    public AutoFocusMode? AutofocusMode { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.AutofocusRange" path="/summary" />
    /// </summary>
    public AutoFocusRange? AutofocusRange { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="AutofocusWindow" path="/summary" />
    /// </summary>
    public AutofocusWindow? AutofocusWindow { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Quality" path="/summary" />
    /// </summary>
    public int? Quality { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Encoding" path="/summary" />
    /// </summary>
    public Encoding? Encoding { get; init; }
    
    /// <summary>
    /// <inheritdoc cref="PiCamera.RpicamArgsBuilder.Raw" path="/summary" />
    /// </summary>
    public bool? Raw { get; init; }
}

/// <summary>
/// <inheritdoc cref="PiCamera.RpicamArgsBuilder.AutofocusWindow" path="/summary" />
/// </summary>
public sealed record AutofocusWindow(float x, float y, float w, float h);