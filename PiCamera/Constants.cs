using PiCamera;

namespace PiCamera;

public enum RpiCameraApp
{
    RpicamStill,
    RpicamJpeg
}

public enum MeteringMode
{
    Center,
    Spot,
    Average
}

public enum ExposureMode
{
    Normal,
    Spot
}

public enum AwbMode
{
    Auto,
    Incandescent,
    Tungsten,
    Fluorescent,
    Indor,
    Daylight,
    Cloudy,
    Custom
}

public enum DenoiseMode
{
    Auto,
    Off,
    CdnOff,
    CdnFast,
    CdnHq
}

public enum AutoFocusMode
{
    Auto,
    Manual,
    Continous
}

public enum AutoFocusRange
{
    Normal,
    Macro,
    Full
}

public enum AutoFocusSpeed
{
    Normal,
    Fast
}

public enum Encoding
{
    Jpeg,
    Png,
    Rgb24,
    Rgb48,
    Bmp,
    Yuv420
}

public enum Flicker
{
    Hz50,
    Hz60
}

public enum Output
{
    Stream,
    File,
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
        ExposureMode.Spot => "spot",
        _ => ExposureMode.Normal.AsString()
    };

    public static string AsString(this AwbMode awbMode) => awbMode switch
    {
        AwbMode.Auto => "auto",
        AwbMode.Incandescent => "incandescent",
        AwbMode.Tungsten => "tungsten",
        AwbMode.Fluorescent => "fluorescent",
        AwbMode.Indor => "indoor",
        AwbMode.Daylight => "daylight",
        AwbMode.Cloudy => "cloudy",
        AwbMode.Custom => "custom",
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
        Encoding.Rgb24 => "rgb/rgb24",
        Encoding.Rgb48 => "rgb/rgb48",
        Encoding.Bmp => "bmp",
        Encoding.Yuv420 => "yuv420",
        _ => Encoding.Jpeg.AsString()
    };

    public static TimeSpan AsTimespan(this Flicker flicker) => flicker switch
    {
        Flicker.Hz50 => TimeSpan.FromMicroseconds(10000),
        Flicker.Hz60 => TimeSpan.FromMicroseconds(8333),
        _ => TimeSpan.Zero
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