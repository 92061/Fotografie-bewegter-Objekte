using PiCamera;

namespace PiCamera;

public enum RpiCameraApp
{
    RpicamStill,
    RpicamJpeg
}

/// <summary>
/// Automatic Exposure/Gain Control (AEC/AGC) algorithm mode <br />
/// <see cref="RpicamArgsBuilder.Metering"/> <br />
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#metering"/>
/// </summary>
public enum MeteringMode
{
    /// <summary>
    /// centre weighted metering (default)
    /// </summary>
    Center,
    /// <summary>
    /// spot metering
    /// </summary>
    Spot,
    /// <summary>
    /// average or whole frame metering
    /// </summary>
    Average
}

/// <summary>
/// Exposure profile. <br />
/// <see cref="RpicamArgsBuilder.Exposure"/> <br />
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#exposure"/>
/// </summary>
public enum ExposureMode
{
    /// <summary>
    /// normal exposure, normal gains (default)
    /// </summary>
    Normal,
    /// <summary>
    /// short exposure, larger gains
    /// </summary>
    Sport,
    /// <summary>
    /// long exposure, smaller gains
    /// </summary>
    Long
}

/// <summary>
/// Auto White Balance (AWB) mode <br />
/// <see cref="RpicamArgsBuilder.Awb"/> <br />
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#awb"/>
/// </summary>
public enum AwbMode
{
    /// <summary>
    /// 2500K to 8000K (default)
    /// </summary>
    Auto,
    /// <summary>
    /// 2500K to 3000K
    /// </summary>
    Incandescent,
    /// <summary>
    /// 3000K to 3500K
    /// </summary>
    Tungsten,
    /// <summary>
    /// 4000K to 4700K
    /// </summary>
    Fluorescent,
    /// <summary>
    /// 3000K to 5000K
    /// </summary>
    Indoor,
    /// <summary>
    /// 5500K to 6500K
    /// </summary>
    Daylight,
    /// <summary>
    /// 7000K to 8500K
    /// </summary>
    Cloudy
}

/// <summary>
/// Denoising mode <br />
/// <see cref="RpicamArgsBuilder.Denoise"/> <br />
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#denoise"/>
/// </summary>
public enum DenoiseMode
{
    /// <summary>
    /// Enables standard spatial denoise. Uses extra-fast color denoise for video, and high-quality color denoise for images. Enables no extra color denoise in the preview window. (default)
    /// </summary>
    Auto,
    /// <summary>
    /// Disables spatial and colour denoise.
    /// </summary>
    Off,
    /// <summary>
    /// Disables colour denoise.
    /// </summary>
    CdnOff,
    /// <summary>
    /// Uses fast colour denoise.
    /// </summary>
    CdnFast,
    /// <summary>
    /// Uses high-quality color denoise. Not appropriate for video/viewfinder due to reduced throughput.
    /// </summary>
    CdnHq
}

/// <summary>
/// Autofocus mode <br />
/// <see cref="RpicamArgsBuilder.AutofocusMode"/> <br />
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#autofocus-mode"/>
/// </summary>
public enum AutoFocusMode
{
    /// <summary>
    /// Puts the camera into continuous autofocus mode unless lens-position or autofocus-on-capture override the mode to manual (default)
    /// </summary>
    Default,
    /// <summary>
    /// Only moves the lens for an autofocus sweep when the camera starts or just before capture if autofocus-on-capture is also used
    /// </summary>
    Auto,
    /// <summary>
    /// Does not move the lens at all unless manually configured with lens-position
    /// </summary>
    Manual,
    /// <summary>
    /// Adjusts the lens position automatically as the scene changes
    /// </summary>
    Continous
}

/// <summary>
/// Autofocus range <br />
/// <see cref="RpicamArgsBuilder.AutofocusRange"/> <br />
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#autofocus-range"/>
/// </summary>
public enum AutoFocusRange
{
    /// <summary>
    /// Focuses from reasonably close to infinity (default)
    /// </summary>
    Normal,
    /// <summary>
    /// Focuses only on close objects, including the closest focal distances supported by the camera
    /// </summary>
    Macro,
    /// <summary>
    /// Focus on the entire range, from the very closest objects to infinity
    /// </summary>
    Full
}

/// <summary>
/// Not used...
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#autofocus-speed"/>
/// </summary>
public enum AutoFocusSpeed
{
    /// <summary>
    /// changes the lens position at normal speed (default)
    /// </summary>
    Normal,
    /// <summary>
    /// changes the lens position quickly
    /// </summary>
    Fast
}

/// <summary>
/// <see cref="RpicamArgsBuilder.Encoding"/> <br />
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#encoding"/>
/// </summary>
public enum Encoding
{
    /// <summary>
    /// JPEG (default)
    /// </summary>
    Jpeg,
    /// <summary>
    /// PNG
    /// </summary>
    Png,
    /// <summary>
    /// Binary dump of uncompressed RGB pixels
    /// </summary>
    Rgb,
    /// <summary>
    /// BMP
    /// </summary>
    Bmp,
    /// <summary>
    /// Binary dump of uncompressed YUV420 pixels
    /// </summary>
    Yuv420
}

/// <summary>
/// <see cref="RpicamArgsBuilder.Output"/> <br />
/// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#output"/>
/// </summary>
public enum Output
{
    /// <summary>
    /// Include the %d directive in the file name to replace the directive with a count that increments for each opened file. This directive supports standard C format directive modifiers. (default)
    /// </summary>
    File,
    /// <summary>
    /// write to stdout
    /// </summary>
    Stream,
    /// <summary>
    /// A network address for UDP or TCP streaming.
    /// </summary>
    Network
}

public static class Helper
{
    public static string AsString(this RpiCameraApp app) => app switch
    {
        RpiCameraApp.RpicamStill => "rpicam-still",
        RpiCameraApp.RpicamJpeg => "rpicam-jpeg",
        _ => RpiCameraApp.RpicamStill.AsString()
    };

    public static string AsString(this MeteringMode meteringMode) => meteringMode switch
    {
        MeteringMode.Center => "centre",
        MeteringMode.Spot => "spot",
        MeteringMode.Average => "average",
        _ => MeteringMode.Center.AsString()
    };

    public static string AsString(this ExposureMode meteringMode) => meteringMode switch
    {
        ExposureMode.Normal => "normal",
        ExposureMode.Sport => "sport",
        ExposureMode.Long => "long",
        _ => ExposureMode.Normal.AsString()
    };

    public static string AsString(this AwbMode awbMode) => awbMode switch
    {
        AwbMode.Auto => "auto",
        AwbMode.Incandescent => "incandescent",
        AwbMode.Tungsten => "tungsten",
        AwbMode.Fluorescent => "fluorescent",
        AwbMode.Indoor => "indoor",
        AwbMode.Daylight => "daylight",
        AwbMode.Cloudy => "cloudy",
        _ => AwbMode.Auto.AsString()
    };

    public static string AsString(this DenoiseMode denoiseMode) => denoiseMode switch
    {
        DenoiseMode.Auto => "auto",
        DenoiseMode.Off => "off",
        DenoiseMode.CdnOff => "cdn_off",
        DenoiseMode.CdnFast => "cdn_fast",
        DenoiseMode.CdnHq => "cdn_hq",
        _ => DenoiseMode.Auto.AsString()
    };

    public static string AsString(this AutoFocusMode autoFocusMode) => autoFocusMode switch
    {
        AutoFocusMode.Default => "default",
        AutoFocusMode.Auto => "auto",
        AutoFocusMode.Manual => "manual",
        AutoFocusMode.Continous => "continous",
        _ => AutoFocusMode.Auto.AsString()
    };

    public static string AsString(this AutoFocusRange autoFocusRange) => autoFocusRange switch
    {
        AutoFocusRange.Normal => "normal",
        AutoFocusRange.Macro => "macro",
        AutoFocusRange.Full => "full",
        _ => AutoFocusRange.Normal.AsString()
    };

    public static string AsString(this AutoFocusSpeed autoFocusSpeed) => autoFocusSpeed switch
    {
        AutoFocusSpeed.Normal => "normal",
        AutoFocusSpeed.Fast => "fast",
        _ => AutoFocusSpeed.Normal.AsString()
    };

    public static string AsString(this Encoding encoding) => encoding switch
    {
        Encoding.Jpeg => "jpg",
        Encoding.Png => "png",
        Encoding.Rgb => "rgb",
        Encoding.Bmp => "bmp",
        Encoding.Yuv420 => "yuv420",
        _ => Encoding.Jpeg.AsString()
    };
    
    // https://www.raspberrypi.com/documentation/computers/camera_software.html#output
    public static string AsString(this Output output, string? additional = null) => output switch
    {
        Output.Stream => "-",
        Output.File => additional ?? "-",
        Output.Network => additional.CheckNetworkAddress() ?? throw new ArgumentException(),
        _ => Output.Stream.AsString()
    };

    private static string? CheckNetworkAddress(this string? addressStr)
    {
        if (addressStr is null)
            return null;
        
        Uri uri = new (addressStr);
        if (uri.Scheme != "tcp" && uri.Scheme != "udp")
            return null;

        return addressStr;
    }
}