using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PhotographyOfMovingObjects;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class FlashController : ControllerBase
{
    /// <summary>
    /// Returns the configured "Flash Delay".
    /// </summary>
    /// <response code="200">Returns the delay (in ms) of the flash after the trigger is triggered.</response>
    [HttpGet("Delay")]
    [EndpointDescription("Returns the configured \"Flash Delay\".")]
    [EndpointSummary("Returns the configured \"Flash Delay\".")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetFlashDelayMs()
    {
        return TypedResults.Ok((int)Photography.DelayFlash.TotalMilliseconds);
    }
    
    /// <summary>
    /// Sets the "Flash Delay".
    /// </summary>
    /// <param name="delayMs">The delay of the flash (in ms).</param>
    [HttpPatch("Delay")]
    [EndpointDescription("Sets the \"Flash Delay\".")]
    [EndpointSummary("Sets the \"Flash Delay\".")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetFlashDelayMs([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)][Range(0, int.MaxValue)]int delayMs)
    {
        Photography.DelayFlash = TimeSpan.FromMilliseconds(delayMs);
        return TypedResults.Ok();
    }

    /// <summary>
    /// Returns the GPIO Pin-Number of the Flash.
    /// </summary>
    /// <response code="200">Returns the GPIO Pin-Number of the Flash. <seealso href="https://pinout.xyz/"/></response>
    [HttpGet("PinNumber")]
    [EndpointDescription("Returns the GPIO Pin-Number of the Flash.")]
    [EndpointSummary("Returns the GPIO Pin-Number of the Flash.")]
    [ProducesResponseType<int>(StatusCodes.Status200OK, "text/plain")]
    public Ok<int> GetFlashGpioPin()
    {
        return TypedResults.Ok(Flash.PinNumber);
    }

    /// <summary>
    /// Sets the GPIO Pin-Number of the Flash.
    /// </summary>
    /// <param name="pinNumber">GPIO Pin-Number of the Flash. <seealso href="https://pinout.xyz/"/></param>
    [HttpPatch("PinNumber")]
    [EndpointDescription("Sets the GPIO Pin-Number of the Flash.")]
    [EndpointSummary("Sets the GPIO Pin-Number of the Flash.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok SetFlashGpioPin([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)][Range(0, 27)]int pinNumber)
    {
        Flash.PinNumber = pinNumber;
        return TypedResults.Ok();
    }
    
    /// <summary>
    /// Triggers the flash.
    /// </summary>
    [HttpPost("Flash")]
    [EndpointDescription("Triggers the flash.")]
    [EndpointSummary("Triggers the flash.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Ok Trigger()
    {
        Flash.Trigger();
        return TypedResults.Ok();
    }
}