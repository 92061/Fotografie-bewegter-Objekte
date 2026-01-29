using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhotographyOfMovingObjects;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class FlashController : ControllerBase
{
    /// <summary>
    /// Returns the configured "Flash Delay".
    /// </summary>
    [HttpGet("Delay")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetFlashDelayMs()
    {
        return TypedResults.Ok((int)Photography.DelayFlash.TotalMilliseconds);
    }
    
    /// <summary>
    /// Sets the "Camera Delay"
    /// </summary>
    /// <param name="delayMs">Milliseconds</param>
    [HttpPatch("Delay/{delayMs}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetFlashDelayMs(int delayMs)
    {
        Photography.DelayFlash = TimeSpan.FromMilliseconds(delayMs);
        return TypedResults.Ok();
    }

    /// <summary>
    /// Returns the GPIO Pin-Number of the Flash
    /// </summary>
    [HttpGet("Flash/PinNumber")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetFlashGpioPin()
    {
        return TypedResults.Ok(Flash.PinNumber);
    }

    /// <summary>
    /// Sets the GPIO Pin-Number of the Flash
    /// </summary>
    /// <param name="pinNumber"></param>
    [HttpPatch("Flash/PinNumber/{pinNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetFlashGpioPin(int pinNumber)
    {
        Flash.PinNumber = pinNumber;
        return TypedResults.Ok();
    }
}