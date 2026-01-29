using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhotographyOfMovingObjects;

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
    [ProducesResponseType<FileContentHttpResult>(StatusCodes.Status200OK)]
    public async Task<FileContentHttpResult> TakePicture()
    {
        Photography.LatestPicture = await Camera.TakePicture(HttpContext.RequestAborted);
        return TypedResults.File(Photography.LatestPicture, "image/jpeg");
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
}