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
    [HttpGet("Trigger/PinNumber")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetTriggerGpioPin()
    {
        return TypedResults.Ok(Trigger.PinNumber);
    }
    
    /// <summary>
    /// Sets the GPIO Pin-Number of the Trigger
    /// </summary>
    /// <param name="pinNumber"></param>
    [HttpPatch("Trigger/PinNumber/{pinNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetTriggerGpioPin(int pinNumber)
    {
        Trigger.SetTriggerPin(pinNumber);
        return TypedResults.Ok();
    }
}