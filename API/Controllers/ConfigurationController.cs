using Microsoft.AspNetCore.Mvc;
using PhotographyOfMovingObjects;

namespace Project.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationController : ControllerBase
{
    [HttpGet("Delay")]
    public IActionResult GetFallDelayMs()
    {
        return Ok(Photography.FallDelay.Milliseconds);
    }
    
    [HttpPatch("Delay/{delayMs}")]
    public IActionResult SetFallDelayMs(int delayMs)
    {
        Photography.SetFallDelay(TimeSpan.FromMilliseconds(delayMs));
        return Ok();
    }
    
    [HttpGet("Camera/Delay")]
    public IActionResult GetCameraDelayMs()
    {
        return Ok(Photography.DelayCamera.Milliseconds);
    }
    
    [HttpPatch("Camera/Delay/{delayMs}")]
    public IActionResult SetCameraDelayMs(int delayMs)
    {
        Photography.SetCameraDelay(TimeSpan.FromMilliseconds(delayMs));
        return Ok();
    }
    
    [HttpGet("Flash/Delay")]
    public IActionResult GetFlashDelayMs()
    {
        return Ok(Photography.DelayFlash.Milliseconds);
    }
    
    [HttpPatch("Flash/Delay/{delayMs}")]
    public IActionResult SetFlashDelayMs(int delayMs)
    {
        Photography.SetFlashDelay(TimeSpan.FromMilliseconds(delayMs));
        return Ok();
    }

    [HttpGet("Flash/PinNumber")]
    public IActionResult GetFlashGpioPin()
    {
        return Ok(FlashTrigger.Flash.FlashPinNumber);
    }

    [HttpPatch("Flash/PinNumber/{pinNumber}")]
    public IActionResult SetFlashGpioPin(int pinNumber)
    {
        FlashTrigger.Flash.SetFlashPin(pinNumber);
        return Ok();
    }

    [HttpGet("Trigger/PinNumber")]
    public IActionResult GetTriggerGpioPin()
    {
        return Ok(ImpulseReader.Trigger.PinNumber);
    }
    
    [HttpPatch("Trigger/PinNumber/{pinNumber}")]
    public IActionResult SetTriggerGpioPin(int pinNumber)
    {
        ImpulseReader.Trigger.SetTriggerPin(pinNumber);
        return Ok();
    }
}