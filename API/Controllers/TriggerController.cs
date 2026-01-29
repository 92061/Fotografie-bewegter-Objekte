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
    /// Returns the GPIO Pin-Number of the Trigger
    /// </summary>
    [HttpGet("PinNumber")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetTriggerGpioPin()
    {
        return TypedResults.Ok(Trigger.PinNumber);
    }
    
    /// <summary>
    /// Sets the GPIO Pin-Number of the Trigger
    /// </summary>
    /// <param name="pinNumber"></param>
    [HttpPatch("PinNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetTriggerGpioPin([FromBody]int pinNumber)
    {
        Trigger.SetTriggerPin(pinNumber);
        return TypedResults.Ok();
    }
    
    
    /// <summary>
    /// Get the Flank on which the trigger executes
    /// </summary>
    [HttpGet("Flank")]
    [ProducesResponseType<PinEventTypes>(StatusCodes.Status200OK)]
    public Ok<PinEventTypes> SetTriggerGpioPin()
    {
        return TypedResults.Ok(Photography.OnTriggerFlank);
    }
    
    /// <summary>
    /// Sets the Flank on which the trigger should execute
    /// </summary>
    [HttpPatch("Flank")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetTriggerGpioPin([FromBody]PinEventTypes flank)
    {
        Photography.OnTriggerFlank = flank;
        return TypedResults.Ok();
    }
}