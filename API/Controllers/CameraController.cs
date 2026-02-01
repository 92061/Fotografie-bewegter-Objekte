using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    /// <response code="200">Returns the delay (in ms) of the camera after the trigger is triggered.</response>
    [HttpGet("Delay")]
    [EndpointDescription("Returns the configured \"Camera Delay\".")]
    [EndpointSummary("Returns the configured \"Camera Delay\".")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetCameraDelayMs()
    {
        return TypedResults.Ok((int)Photography.DelayCamera.TotalMilliseconds);
    }
    
    /// <summary>
    /// Sets the "Camera Delay".
    /// </summary>
    /// <param name="delayMs">The delay of the camera (in ms).</param>
    [HttpPatch("Delay")]
    [EndpointDescription("Sets the \"Camera Delay\".")]
    [EndpointSummary("Sets the \"Camera Delay\".")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetCameraDelayMs([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)][Range(0, int.MaxValue)]int delayMs)
    {
        Photography.DelayCamera = TimeSpan.FromMilliseconds(delayMs);
        return TypedResults.Ok();
    }
    
    /// <summary>
    /// Takes a photo.
    /// </summary>
    [HttpPost("TakePicture")]
    [EndpointDescription("Takes a photo.")]
    [EndpointSummary("Takes a photo.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok TakePicture()
    {
        Camera.TakePicture();
        return TypedResults.Ok();
    }
    
    /// <summary>
    /// Gets the latest captured Image.
    /// </summary>
    /// <response code="200">Returns the latest captured Photo.</response>
    [HttpGet("LatestPhoto")]
    [EndpointDescription("Gets the latest captured Image.")]
    [EndpointSummary("Gets the latest captured Image.")]
    [ProducesResponseType<FileContentHttpResult>(StatusCodes.Status200OK)]
    public FileContentHttpResult LatestImage()
    {
        return TypedResults.File(Photography.LatestPicture, "image/png");
    }

    /// <summary>
    /// Change the settings of the camera.
    /// </summary>
    /// <param name="settings">The Camera settings that are requested to being changed. <seealso cref="CameraSettings"/></param>
    [HttpPost("Settings")]
    [EndpointDescription("Change the settings of the camera.")]
    [EndpointSummary("Change the settings of the camera.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok ChangeSettings([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)]CameraSettings settings)
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