using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhotographyOfMovingObjects;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class ActionController : ControllerBase
{
    /// <summary>
    /// Gets the latest captured Image
    /// </summary>
    /// <response code="200">Latest photo</response>
    [HttpPost("LatestPhoto")]
    [ProducesResponseType<FileStreamHttpResult>(StatusCodes.Status200OK)]
    public FileStreamHttpResult LatestImage()
    {
        return TypedResults.File(Photography.ImageStream, "image/jpeg");
    }
}