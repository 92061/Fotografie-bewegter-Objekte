using System.Device.Gpio;

namespace FlashTrigger;

/*
 * https://learn.microsoft.com/en-us/dotnet/iot/tutorials/blink-led
 */

public static class Flash
{
    private static readonly GpioController GpioController = new();
    private const int DefaultFlashPinNumber = 18; 
    private static GpioPin _flashPin;
    private const int FlashHighTimeoutMs = 50;

    static Flash()
    {
        _flashPin = GpioController.OpenPin(DefaultFlashPinNumber, PinMode.Output);
    }

    public static void SetFlashPin(int pinNumber)
    {
        _flashPin.Close();
        _flashPin.Dispose();
        _flashPin = GpioController.OpenPin(pinNumber, PinMode.Output);
    }

    public static Task Trigger()
    {
        return new (TriggerInternal);
    }

    private static void TriggerInternal()
    {
        _flashPin.Write(PinValue.High);
        Thread.Sleep(FlashHighTimeoutMs);
        _flashPin.Write(PinValue.Low);
    }
}