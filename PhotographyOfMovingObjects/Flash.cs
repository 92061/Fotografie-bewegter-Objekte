using System.Device.Gpio;

namespace PhotographyOfMovingObjects;

/*
 * https://learn.microsoft.com/en-us/dotnet/iot/tutorials/blink-led
 */

public static class Flash
{
    private static readonly GpioController GpioController = new();
    private const int DefaultFlashPinNumber = 17; 
    private const int FlashHighTimeoutMs = 5;
    
    public static event FlashEvent? Triggered;
    public delegate void FlashEvent();

    public static int PinNumber
    {
        get => _flashPin.PinNumber;
        set
        {
            _flashPin.Close();
            _flashPin.Dispose();
            _flashPin = GpioController.OpenPin(value, PinMode.Output);
        }
    }
    private static GpioPin _flashPin;

    static Flash()
    {
        _flashPin = GpioController.OpenPin(DefaultFlashPinNumber, PinMode.Output);
    }

    /// <summary>
    /// Set off the flash
    /// </summary>
    /// <param name="delay">Delay the flash by this value</param>
    public static void Trigger(TimeSpan? delay = null)
    {
        if(delay is { } d)
            Thread.Sleep(d);
            
        _flashPin.Write(PinValue.High);
        Triggered?.Invoke();
        Thread.Sleep(FlashHighTimeoutMs);
        _flashPin.Write(PinValue.Low);
    }
}