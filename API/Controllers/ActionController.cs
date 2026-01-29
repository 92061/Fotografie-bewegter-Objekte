using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class ActionController : ControllerBase
{
    [HttpPost("Flash")]
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

    [HttpPost("Photo")]
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