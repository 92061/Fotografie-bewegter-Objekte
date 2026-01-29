using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PhotographyOfMovingObjects;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class ActionController : ControllerBase
{
    /// <summary>
    /// Triggers the flash.
    /// </summary>
    /// <response code="200">Flash triggered</response>
    /// <response code="500">Task failed!</response>
    [HttpPost("Flash")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Flash()
    {
        Task flash = PhotographyOfMovingObjects.Flash.Trigger(TimeSpan.Zero);
        flash.Start();
        flash.Wait();
        if (flash.IsCompletedSuccessfully)
            return Ok();
        else
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Connects the Android Debug Brdige
    /// </summary>
    /// <response code="200">Connected</response>
    /// <response code="500">Task failed!</response>
    [HttpPost("ADB/Connect")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ConnectAdbDevice()
    {
        bool success = Camera.Connect();
        if (success)
            return Ok();
        else
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Returns the ADB connection Status
    /// </summary>
    /// <response code="200">true if connected</response>
    [HttpGet("ADB/Status")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK, "text/plain")]
    public IActionResult CheckAdbConnection()
    {
        return Ok(Camera.Connected);
    }

    /// <summary>
    /// Opens the camera app
    /// </summary>
    /// <response code="200">App started</response>
    /// <response code="500">Task failed!</response>
    [HttpPost("StartCamera")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult StartCamera([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)]string? appName = null)
    {
        bool success = appName is not null ? Camera.StartCameraApp(appName) : Camera.StartCameraApp();
        if (success)
            return Ok();
        else
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Takes a photo.
    /// </summary>
    /// <response code="200">Photo taken</response>
    /// <response code="500">Task failed!</response>
    [HttpPost("Photo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult TakePhoto()
    {
        Task<bool> takePhoto = Camera.TakePicture(TimeSpan.Zero);
        takePhoto.Start();
        takePhoto.Wait();
        if (takePhoto.IsCompletedSuccessfully)
            return Ok();
        else
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}