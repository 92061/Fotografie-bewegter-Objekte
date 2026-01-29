using Microsoft.AspNetCore.Mvc;

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
        Task flash = FlashTrigger.Flash.Trigger(TimeSpan.Zero);
        flash.Start();
        flash.Wait();
        if (flash.IsCompletedSuccessfully)
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
        Task<bool> takePhoto = CameraTrigger.Camera.TakePicture(TimeSpan.Zero);
        takePhoto.Start();
        takePhoto.Wait();
        if (takePhoto.IsCompletedSuccessfully)
            return Ok();
        else
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}