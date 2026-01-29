using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
    [HttpPost("Flash")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Ok> Flash()
    {
        await PhotographyOfMovingObjects.Flash.FlashTask();
        return TypedResults.Ok();
    }

    /// <summary>
    /// Takes a photo.
    /// </summary>
    /// <response code="200">Photo taken</response>
    [HttpPost("Photo")]
    [ProducesResponseType<FileStreamHttpResult>(StatusCodes.Status200OK)]
    public async Task<FileStreamHttpResult> TakePhoto()
    {
        MemoryStream stream = new ();
        await Camera.TakePictureTask(stream);
        return TypedResults.File(stream, "image/jpeg");
    }
}