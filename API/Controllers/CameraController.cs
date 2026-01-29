using Iot.Device.Camera;
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
    [HttpPatch("Delay/{delayMs}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetCameraDelayMs(int delayMs)
    {
        Photography.DelayCamera = TimeSpan.FromMilliseconds(delayMs);
        return TypedResults.Ok();
    }

    /// <summary>
    /// Gets all available Cameras
    /// </summary>
    [HttpGet("Cameras")]
    [ProducesResponseType<CameraInfo[]>(StatusCodes.Status200OK)]
    public async Task<Ok<CameraInfo[]>> GetCameras()
    {
        IEnumerable<CameraInfo> c = await Camera.GetCameras();
        return TypedResults.Ok(c.ToArray());
    }
    
    /// <summary>
    /// Sets the camera to use
    /// </summary>
    [HttpPatch("Use/{index:int}")]
    [HttpPatch("Use/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Results<Ok, NotFound>> SetCamera(int? index = null, string? name = null)
    {
        IEnumerable<CameraInfo> c = await Camera.GetCameras();
        if (c.FirstOrDefault(ci => ci.Index == index || ci.Name == name) is not { } info)
            return TypedResults.NotFound();
        Camera.SelectedCamera = info;
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