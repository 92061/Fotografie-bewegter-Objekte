using System.Device.Gpio;

namespace PhotographyOfMovingObjects;

/*
 * https://learn.microsoft.com/en-us/dotnet/iot/tutorials/gpio-input
 */

public static class Trigger
{
    private static readonly GpioController GpioController = new();
    private const int DefaultTriggerPinNumber = 14; 
    private static GpioPin _triggerPin;
    public static int PinNumber => _triggerPin.PinNumber;

    /// <summary>
    /// Bounce-timeout
    /// </summary>
    private static readonly TimeSpan EventTimeout = TimeSpan.FromMilliseconds(50);
    private static DateTime _lastEventTime = DateTime.Now;

    /// <summary>
    /// Invoked when the trigger has been triggered
    /// </summary>
    public static event TriggeredEvent? Triggered;
    public delegate void TriggeredEvent(PinEventTypes type);
    
    static Trigger()
    {
        _triggerPin = GpioController.OpenPin(DefaultTriggerPinNumber, PinMode.InputPullUp);
        GpioController.RegisterCallbackForPinValueChangedEvent(DefaultTriggerPinNumber, 
            PinEventTypes.Rising | PinEventTypes.Falling,
            OnPinValueChanged);
    }
    
    /// <summary>
    /// Sets the pin the trigger should use.
    /// </summary>
    /// <param name="pinNumber"></param>
    /// <param name="mode"></param>
    /// <exception cref="Exception"></exception>
    public static void SetTriggerPin(int pinNumber, PinMode mode = PinMode.InputPullUp)
    {
        if (!GpioController.IsPinModeSupported(pinNumber, mode))
            throw new Exception($"Pin {pinNumber} doesn't support {mode}!");

        // If previous pin was used, close it
        if (GpioController.IsPinOpen(_triggerPin.PinNumber))
        {
            _triggerPin.Close();
            _triggerPin.Dispose();
            GpioController.UnregisterCallbackForPinValueChangedEvent(_triggerPin.PinNumber, OnPinValueChanged);
        }
        
        _triggerPin = GpioController.OpenPin(pinNumber, mode);
        GpioController.RegisterCallbackForPinValueChangedEvent(DefaultTriggerPinNumber, 
            PinEventTypes.Rising | PinEventTypes.Falling,
            OnPinValueChanged);
    }

    private static void OnPinValueChanged(object sender, PinValueChangedEventArgs e)
    {
        if (DateTime.Now - _lastEventTime > EventTimeout)
        {
            Triggered?.Invoke(e.ChangeType);
            _lastEventTime = DateTime.Now;
            Console.WriteLine($"Trigger! {e.ChangeType}");
        }else
            Console.WriteLine($"Trigger detected, but below timeout-threshold. {e.ChangeType}");
    }
}