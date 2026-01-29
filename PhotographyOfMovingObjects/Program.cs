using System.Device.Gpio;
using CameraTrigger;
using FlashTrigger;
using ImpulseReader;

namespace PhotographyOfMovingObjects;

class Program
{
    static void Main(string[] args)
    {
        _ = new Program();
    }
    
    public static TimeSpan DelayCamera { get; private set; } = TimeSpan.FromMilliseconds(0);
    public static TimeSpan DelayFlash { get; private set; } = TimeSpan.FromMilliseconds(0);
    public static TimeSpan FallDelay { get; private set; } = TimeSpan.FromMilliseconds(100);
    
    private static Task<bool> _takePicture = Camera.TakePicture(DelayCamera);
    private static Task _triggerFlash = Flash.Trigger(DelayFlash);
    
    public Program()
    {
        Trigger.Triggered += Triggered;
        Thread.Sleep(Timeout.Infinite); //TODO Add Program-Exit
    }

    private static void Triggered(PinEventTypes type)
    {
        Thread.Sleep(FallDelay);
        _takePicture.Start();
        _triggerFlash.Start();
        while(_takePicture.IsCompleted == false && _triggerFlash.IsCompleted == false)
            Thread.Sleep(10);
        ResetTasks();
    }

    public static void SetFallDelay(TimeSpan fallDelay) => FallDelay = fallDelay;
    public static void SetCameraDelay(TimeSpan delay)
    {
        DelayCamera = delay;
        ResetTasks();
    }
    public static void SetFlashDelay(TimeSpan delay)
    {
        DelayFlash = delay;
        ResetTasks();
    }

    private static void ResetTasks()
    {
        _takePicture = Camera.TakePicture(DelayCamera);
        _triggerFlash = Flash.Trigger(DelayFlash);
    }
}