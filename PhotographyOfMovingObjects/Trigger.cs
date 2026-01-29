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

    private static readonly TimeSpan EventTimeout = TimeSpan.FromMilliseconds(500);
    private static DateTime _lastEventTime = DateTime.Now;

    public static event TriggeredEvent? Triggered;
    public delegate void TriggeredEvent(PinEventTypes type);
    
    static Trigger()
    {
        _triggerPin = GpioController.OpenPin(DefaultTriggerPinNumber, PinMode.InputPullUp);
        GpioController.RegisterCallbackForPinValueChangedEvent(DefaultTriggerPinNumber, 
            PinEventTypes.Rising,
            OnPinValueChanged);
    }
    
    public static void SetTriggerPin(int pinNumber, PinMode mode = PinMode.InputPullUp)
    {
        _triggerPin.Close();
        _triggerPin.Dispose();
        GpioController.UnregisterCallbackForPinValueChangedEvent(_triggerPin.PinNumber, OnPinValueChanged);
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
            Console.WriteLine("Trigger!");
            _lastEventTime = DateTime.Now;
        }else
            Console.WriteLine("Detected, but below timeout-threshold.");
    }
}