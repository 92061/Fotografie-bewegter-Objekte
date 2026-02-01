using System.Device.Gpio;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhotographyOfMovingObjects;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class TriggerController : ControllerBase
{

    /// <summary>
    /// Returns the GPIO Pin-Number of the Trigger.
    /// </summary>
    /// <response code="200">Returns the GPIO Pin-Number of the Trigger. <seealso href="https://pinout.xyz/"/></response>
    [HttpGet("PinNumber")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetTriggerGpioPin()
    {
        return TypedResults.Ok(Trigger.PinNumber);
    }
    
    /// <summary>
    /// Sets the GPIO Pin-Number of the Trigger.
    /// </summary>
    /// <param name="pinNumber">GPIO Pin-Number of the Trigger. <seealso href="https://pinout.xyz/"/></param>
    [HttpPatch("PinNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetTriggerGpioPin([FromBody]int pinNumber)
    {
        Trigger.SetTriggerPin(pinNumber);
        return TypedResults.Ok();
    }
    
    
    /// <summary>
    /// Get the Flank on which the trigger executes.
    /// </summary>
    /// <response code="200">Returns the Flank on which the Trigger will trigger. <seealso cref="PinEventTypes"/></response>
    [HttpGet("Flank")]
    [ProducesResponseType<PinEventTypes>(StatusCodes.Status200OK)]
    public Ok<PinEventTypes> SetTriggerGpioPin()
    {
        return TypedResults.Ok(Photography.OnTriggerFlank);
    }
    
    /// <summary>
    /// Sets the Flank on which the trigger should execute.
    /// </summary>
    /// <param name="flank">Flank on which the Trigger should trigger. <seealso cref="PinEventTypes"/></param>
    [HttpPatch("Flank")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetTriggerGpioPin([FromBody]PinEventTypes flank)
    {
        Photography.OnTriggerFlank = flank;
        return TypedResults.Ok();
    }
}