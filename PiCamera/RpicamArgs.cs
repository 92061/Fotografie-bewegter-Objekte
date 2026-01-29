namespace PiCamera;

public class RpicamArgs()
{
    private readonly List<string> _args = [ 
        "--nopreview",
        "--signal 1",
        "--timeout 0"
    ];
    
    public Output Output { get; internal protected set; }
    public string? OutputAdditional { get; internal protected set; }

    public string[] GetArgsArray => _args.ToArray();
    public string GetArgsString => string.Join(' ', _args);

    internal void AddArgument(string arg) => _args.Add(arg);
}

public static class RpicamArgsBuilder
{
    public static void GetCameras(this RpicamArgs builder) => builder.AddArgument("--list-cameras");

    public static void UseCamera(this RpicamArgs builder, int index) => builder.AddArgument($"--camera {index}");
    
    public static void Hflip(this RpicamArgs builder) => builder.AddArgument("--hflip");
    
    public static void Vflip(this RpicamArgs builder) => builder.AddArgument("--vflip");
    
    public static void Rotate180(this RpicamArgs builder, bool rotate) => builder.AddArgument($"--rotation {(rotate ? "180" : "0")}");
    
    public static void ShutterSpeed(this RpicamArgs builder, TimeSpan milliseconds) => builder.AddArgument($"--shutter {milliseconds.TotalMilliseconds}");
    
    public static void Gain(this RpicamArgs builder, int gain) => builder.AddArgument($"--gain {gain}");
    
    public static void Metering(this RpicamArgs builder, MeteringMode mode) => builder.AddArgument($"--metering {mode.AsString()}");
    
    public static void Exposure(this RpicamArgs builder, ExposureMode mode) => builder.AddArgument($"--exposure {mode.AsString()}");
    
    public static void Ev(this RpicamArgs builder, int ev) => builder.AddArgument($"--ev {ev}");
    
    public static void Awb(this RpicamArgs builder, AwbMode mode) => builder.AddArgument($"--awb {mode.AsString()}");
    
    public static void AwbGains(this RpicamArgs builder, float red, float blue) => builder.AddArgument($"--awbgains {red:F},{blue:F}");
    
    public static void Brightness(this RpicamArgs builder, float brightness) => builder.AddArgument($"--brightness {brightness:F}");
    
    public static void Contrast(this RpicamArgs builder, float contrast) => builder.AddArgument($"--contrast {contrast:F}");
    
    public static void Saturation(this RpicamArgs builder, float saturation) => builder.AddArgument($"--saturation {saturation:F}");
    
    public static void Sharpness(this RpicamArgs builder, float sharpness) => builder.AddArgument($"--sharpness {sharpness:F}");
    
    public static void Denoise(this RpicamArgs builder, DenoiseMode mode) => builder.AddArgument($"--denoise {mode.AsString()}");
    
    public static void AutofocusMode(this RpicamArgs builder, AutoFocusMode mode) => builder.AddArgument($"--autofocus-mode {mode.AsString()}");
    
    public static void AutofocusRange(this RpicamArgs builder, AutoFocusRange range) => builder.AddArgument($"--autofocus-range {range.AsString()}");
    
    public static void Quality(this RpicamArgs builder, int jpegQuality) => builder.AddArgument($"--quality {jpegQuality}");
    
    public static void Encoding(this RpicamArgs builder, Encoding encoding) => builder.AddArgument($"--encoding {encoding.AsString()}");
    
    public static void Raw(this RpicamArgs builder, bool raw) => builder.AddArgument($"--raw {(raw ? 1 : 0)}");
    
    public static void Flicker(this RpicamArgs builder, Flicker flicker) => builder.AddArgument($"--flicker {flicker.AsTimespan().TotalMicroseconds}");
    
    public static void Flicker(this RpicamArgs builder, TimeSpan microseconds) => builder.AddArgument($"--flicker {microseconds.TotalMicroseconds}");
    
    public static void Output(this RpicamArgs builder, Output output, string? additional = null)
    {
        builder.Output = output;
        builder.OutputAdditional = additional;
        builder.AddArgument($"--output {output.AsString(additional)}");
        if (output is PiCamera.Output.Stream)
            builder.AddArgument("--verbose 0");
        else if(output is PiCamera.Output.File)
            builder.AddArgument("--flush");
    }
}