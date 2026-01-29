using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhotographyOfMovingObjects;
using PiCamera;
using Project.Controllers.DTO;
using Camera = PhotographyOfMovingObjects.Camera;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class CameraController : ControllerBase
{
    
    /// <summary>
    /// Returns the configured "Camera Delay".
    /// </summary>
    [HttpGet("Delay")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetCameraDelayMs()
    {
        return TypedResults.Ok((int)Photography.DelayCamera.TotalMilliseconds);
    }
    
    /// <summary>
    /// Sets the "Camera Delay"
    /// </summary>
    /// <param name="delayMs">Milliseconds</param>
    [HttpPatch("Delay")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetCameraDelayMs([FromBody]int delayMs)
    {
        Photography.DelayCamera = TimeSpan.FromMilliseconds(delayMs);
        return TypedResults.Ok();
    }
    
    /// <summary>
    /// Takes a photo.
    /// </summary>
    /// <response code="200">Photo taken</response>
    [HttpPost("TakePicture")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok TakePicture()
    {
        Camera.TakePicture(HttpContext.RequestAborted);
        return TypedResults.Ok();
    }
    
    /// <summary>
    /// Gets the latest captured Image
    /// </summary>
    /// <response code="200">Latest photo</response>
    [HttpGet("LatestPhoto")]
    [ProducesResponseType<FileContentHttpResult>(StatusCodes.Status200OK)]
    public FileContentHttpResult LatestImage()
    {
        return TypedResults.File(Photography.LatestPicture, "image/png");
    }

    /// <summary>
    /// Change the settings of the camera
    /// </summary>
    /// <response code="200">Settings changed</response>
    public Ok ChangeSettings([FromBody] CameraSettings settings)
    {
        RpicamArgs args = new();
        
        if(settings.AutofocusMode is { } autofocusMode)
            args.AutofocusMode(autofocusMode);
        if(settings.AutofocusRange is { } autofocusRange)
            args.AutofocusRange(autofocusRange);
        if(settings.AutofocusWindow is { } autofocusWindow)
            args.AutofocusWindow(autofocusWindow.x, autofocusWindow.y, autofocusWindow.w, autofocusWindow.h);
        if(settings.Awb is { } awb)
            args.Awb(awb);
        if(settings.Brightness is { } brightness)
            args.Brightness(brightness);
        if(settings.Contrast is { } contrast)
            args.Contrast(contrast);
        if(settings.Denoise is { } denoise)
            args.Denoise(denoise);
        if(settings.Encoding is { } encoding)
         args.Encoding(encoding);
        if(settings.Ev is { } ev)
            args.Ev(ev);
        if(settings.Exposure is { } exposure)
            args.Exposure(exposure);
        if(settings.Gain is { } gain)
            args.Gain(gain);
        if(settings.Hflip is true)
            args.Hflip();
        if(settings.Vflip is true)
            args.Vflip();
        if(settings.Metering is { } metering)
            args.Metering(metering);
        if(settings.Mode is { } mode)
            args.Mode(mode);
        if(settings.Quality is { } quality)
            args.Quality(quality);
        if(settings.Raw is true)
            args.Raw(true);
        if(settings.Rotate180 is true)
            args.Rotate180(true);
        if(settings.Saturation is { } saturation)
            args.Saturation(saturation);
        if(settings.Sharpness is { } sharpness)
            args.Sharpness(sharpness);
        if(settings.ShutterSpeed is { } shutterSpeed)
            args.ShutterSpeed(TimeSpan.FromMicroseconds(shutterSpeed));
        if(settings is { Width: { } w, Height: { } h })
            args.WidthAndHeight(w, h);

        Camera.CameraArgs = args;

        return TypedResults.Ok();
    }
}