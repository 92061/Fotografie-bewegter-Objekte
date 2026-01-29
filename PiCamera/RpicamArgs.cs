using System.Diagnostics.CodeAnalysis;

namespace PiCamera;

public class RpicamArgs
{
    private readonly List<string> _args = [ 
        "--nopreview",
        "--signal 1",
        "--timeout 0"
    ];
    
    public Output Output { get; protected internal set; }
    public string? OutputAdditional { get; protected internal set; }

    public string[] GetArgsArray => _args.ToArray();
    public string GetArgsString => string.Join(' ', _args);

    internal void AddArgument(string arg) => _args.Add(arg);
}

[SuppressMessage("ReSharper", "InvalidXmlDocComment")]
public static class RpicamArgsBuilder
{
    /// <summary>
    /// Lists the detected cameras attached to your Raspberry Pi and their available sensor modes. <br />
    /// Sensor mode identifiers have the following form: S[Bayer order][Bit-depth]_[Optional packing] : [Resolution list] <br />
    /// Crop is specified in native sensor pixels (even in pixel binning mode) as ([x], [y])/[Width]×[Height]. (x, y) specifies the location of the crop window of size width × height in the sensor array. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#list-cameras"/>
    /// </summary>
    public static void GetCameras(this RpicamArgs builder) => builder.AddArgument("--list-cameras");
    
    /// <summary>
    /// Selects the camera to use. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#camera"/>
    /// </summary>
    /// <param name="index">Specify an index from the list of available cameras.</param>
    public static void UseCamera(this RpicamArgs builder, int index) => builder.AddArgument($"--camera {index}");

    /// <summary>
    /// Allows you to specify a camera mode. <br />
    /// The system selects the closest available option for the sensor if there is not an exact match for a provided value. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#mode"/>
    /// </summary>
    /// <param name="mode">[width]:[height]:[bit-depth]:[packing]</param>
    public static void Mode(this RpicamArgs builder, string mode) => builder.AddArgument($"--mode {mode}");
    
    /// <summary>
    /// Specifies output resolution, in pixels, of the captured image.
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#width-and-height"/>
    /// </summary>
    public static void WidthAndHeight(this RpicamArgs builder, int width, int height) => builder.AddArgument($"--width {width} --height {height}");
    
    /// <summary>
    /// Flips the image horizontally.
    /// </summary>
    public static void Hflip(this RpicamArgs builder) => builder.AddArgument("--hflip");
    
    /// <summary>
    /// Flips the image vertically.
    /// </summary>
    public static void Vflip(this RpicamArgs builder) => builder.AddArgument("--vflip");
    
    /// <summary>
    /// Rotates the image extracted from the sensor. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#rotation"/>
    /// </summary>
    /// <param name="rotate">true for a 180-degree rotation</param>
    public static void Rotate180(this RpicamArgs builder, bool rotate) => builder.AddArgument($"--rotation {(rotate ? "180" : "0")}");
    
    /// <summary>
    /// Specifies the exposure time, using the shutter, in microseconds. <br />
    /// Gain can still vary when you use this option. <br />
    /// If the camera runs at a framerate so fast it does not allow for the specified exposure time (for instance, a framerate of 1fps and an exposure time of 10000 microseconds), the sensor will use the maximum exposure time allowed by the framerate. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#rotation"/>
    /// </summary>
    public static void ShutterSpeed(this RpicamArgs builder, TimeSpan microseconds) => builder.AddArgument($"--shutter {microseconds.TotalMicroseconds}");
    
    /// <summary>
    /// Sets the combined analogue and digital gain. <br />
    /// When the sensor driver can provide the requested gain, only uses analogue gain. <br />
    /// When analogue gain reaches the maximum value, the ISP applies digital gain. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#gain"/>
    /// </summary>
    public static void Gain(this RpicamArgs builder, float gain) => builder.AddArgument($"--gain {gain}");
    
    /// <summary>
    /// Sets the metering mode of the Automatic Exposure/Gain Control (AEC/AGC) algorithm. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#metering"/>
    /// </summary>
    public static void Metering(this RpicamArgs builder, MeteringMode mode) => builder.AddArgument($"--metering {mode.AsString()}");
    
    /// <summary>
    /// Sets the exposure profile. Changing the exposure profile should not affect the image exposure. Instead, different modes adjust gain settings to achieve the same net result. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#exposure"/>
    /// </summary>
    public static void Exposure(this RpicamArgs builder, ExposureMode mode) => builder.AddArgument($"--exposure {mode.AsString()}");
    
    /// <summary>
    /// Specifies the exposure value (EV) compensation of the image in stops. Accepts a numeric value that controls target values passed to the Automatic Exposure/Gain Control (AEC/AGC) processing algorithm along the following spectrum: <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#ev"/>
    /// </summary>
    /// <param name="ev">
    /// <list type="bullet">
    ///     <item><description>-10.0 applies minimum target values</description></item>
    ///     <item><description>0.0 applies standard target values</description></item>
    ///     <item><description>+10.0 applies maximum target values</description></item>
    /// </list>
    /// </param>
    public static void Ev(this RpicamArgs builder, int ev) => builder.AddArgument($"--ev {ev}");
    
    /// <summary>
    /// Sets the Auto White Balance (AWB) mode. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#awb"/>
    /// </summary>
    public static void Awb(this RpicamArgs builder, AwbMode mode) => builder.AddArgument($"--awb {mode.AsString()}");
    
    /// <summary>
    /// Sets a fixed red and blue gain value to be used instead of an Auto White Balance (AWB) algorithm. Set non-zero values to disable AWB. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#awbgains"/>
    /// </summary>
    public static void AwbGains(this RpicamArgs builder, float red, float blue) => builder.AddArgument($"--awbgains {red:F},{blue:F}");
    
    /// <summary>
    /// Specifies the image brightness, added as an offset to all pixels in the output image.
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#brightness"/>
    /// </summary>
    /// <param name="brightness">
    /// <list type="bullet">
    ///     <item><description>-1.0 applies minimum brightness (black)</description></item>
    ///     <item><description>0.0 applies standard brightness</description></item>
    ///     <item><description>+1.0 applies maximum brightness (white)</description></item>
    /// </list>
    /// </param>
    public static void Brightness(this RpicamArgs builder, float brightness) => builder.AddArgument($"--brightness {brightness:F}");
    
    /// <summary>
    /// Specifies the image contrast. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#contrast"/>
    /// </summary>
    /// <param name="contrast">
    /// <list type="bullet">
    ///     <item><description>0.0 applies minimum contrast</description></item>
    ///     <item><description>values greater than 0.0, but less than 1.0 apply less than the default amount of contrast</description></item>
    ///     <item><description>1.0 applies the default amount of contrast</description></item>
    ///     <item><description>values greater than 1.0 apply extra contrast</description></item>
    /// </list>
    /// </param>
    public static void Contrast(this RpicamArgs builder, float contrast) => builder.AddArgument($"--contrast {contrast:F}");
    
    /// <summary>
    /// Specifies the image color saturation. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#saturation"/>
    /// </summary>
    /// <param name="saturation">
    /// <list type="bullet">
    ///     <item><description>0.0 applies minimum saturation (gray scale)</description></item>
    ///     <item><description>values greater than 0.0, but less than 1.0 apply less than the default amount of saturation</description></item>
    ///     <item><description>1.0 applies the default amount of saturation</description></item>
    ///     <item><description>values greater than 1.0 apply extra saturation</description></item>
    /// </list>
    /// </param>
    public static void Saturation(this RpicamArgs builder, float saturation) => builder.AddArgument($"--saturation {saturation:F}");
    
    /// <summary>
    /// Sets image sharpness. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#sharpness"/>
    /// </summary>
    /// <param name="sharpness">
    /// <list type="bullet">
    ///     <item><description>0.0 applies no sharpening</description></item>
    ///     <item><description>values greater than 0.0, but less than 1.0 apply less than the default amount of sharpening</description></item>
    ///     <item><description>1.0 applies the default amount of sharpening</description></item>
    ///     <item><description>values greater than 1.0 apply extra sharpening</description></item>
    /// </list>
    /// </param>
    public static void Sharpness(this RpicamArgs builder, float sharpness) => builder.AddArgument($"--sharpness {sharpness:F}");
    
    /// <summary>
    /// Sets the denoising mode. <br />
    /// Even fast color denoise can lower framerates. High quality color denoise significantly lowers framerates. <br />
    /// <seealso ref="https://www.raspberrypi.com/documentation/computers/camera_software.html#denoise"/>
    /// </summary>
    public static void Denoise(this RpicamArgs builder, DenoiseMode mode) => builder.AddArgument($"--denoise {mode.AsString()}");
    
    /// <summary>
    /// Specifies the autofocus mode. <br />
    /// This option is only supported for certain camera modules. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#autofocus-mode"/>
    /// </summary>
    public static void AutofocusMode(this RpicamArgs builder, AutoFocusMode mode) => builder.AddArgument($"--autofocus-mode {mode.AsString()}");
    
    /// <summary>
    /// Specifies the autofocus range. <br />
    /// This option is only supported for certain camera modules. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#autofocus-range"/>
    /// </summary>
    public static void AutofocusRange(this RpicamArgs builder, AutoFocusRange range) => builder.AddArgument($"--autofocus-range {range.AsString()}");
    
    
    /// <summary>
    /// Specifies the autofocus window within the full field of the sensor. <br />
    /// Accepts four decimal values, representing a percentage of the available width and heights. <br />
    /// The default value uses the middle third of the output image in both dimensions (1/9 of the total image area). <br />
    /// This option is only supported for certain camera modules. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#autofocus-window"/>
    /// </summary>
    /// <param name="x">X coordinates to skip before applying autofocus</param>
    /// <param name="y">Y coordinates to skip before applying autofocus</param>
    /// <param name="width">Autofocus area width</param>
    /// <param name="height">Autofocus area height</param>
    public static void AutofocusWindow(this RpicamArgs builder, float x, float y, float width, float height) => builder.AddArgument($"--autofocus-window {x:F},{y:F},{width:F},{height:F}");
    
    /// <summary>
    /// Sets the JPEG quality. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#quality"/>
    /// </summary>
    /// <param name="jpegQuality">Accepts a value between 1 and 100. default is 93</param>
    public static void Quality(this RpicamArgs builder, int jpegQuality) => builder.AddArgument($"--quality {jpegQuality}");
    
    /// <summary>
    /// Sets the encoder to use for image output. <br />
    /// This option always determines the encoding, overriding the extension passed to output. <br />
    /// When using the datetime and timestamp options, this option determines the output file extension.
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#encoding"/>
    /// </summary>
    public static void Encoding(this RpicamArgs builder, Encoding encoding) => builder.AddArgument($"--encoding {encoding.AsString()}");
    
    /// <summary>
    /// Saves a raw Bayer file in DNG format in addition to the output image. Replaces the output file name extension with .dng. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#raw"/>
    /// </summary>
    /// <param name="raw">true to output as raw</param>
    public static void Raw(this RpicamArgs builder, bool raw) { if(raw) builder.AddArgument("--raw"); }
    
    /// <summary>
    /// Sets the output of the record images. <br />
    /// <seealso href="https://www.raspberrypi.com/documentation/computers/camera_software.html#output"/>
    /// </summary>
    /// <param name="additional">Used for <see cref="Output.File"/> and <see cref="Output.Network"/></param>
    public static void Output(this RpicamArgs builder, Output output, string? additional = null)
    {
        builder.Output = output;
        builder.OutputAdditional = additional;
        builder.AddArgument($"--output {output.AsString(additional)}");
        if (output is PiCamera.Output.Stream)
            builder.AddArgument("--verbose 0");
    }
}