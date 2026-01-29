using System.Device.Gpio;
using CameraTrigger;
using FlashTrigger;
using ImpulseReader;

namespace PhotographyOfMovingObjects;

class Program
{
    public static TimeSpan DelayCamera;
    public static TimeSpan DelayFlash;
    
    private static Task<bool> _takePicture = Camera.TakePicture(DelayCamera);
    private static Task _triggerFlash = Flash.Trigger(DelayFlash);
    
    public Program()
    {
        DelayCamera = TimeSpan.FromSeconds(1);
        DelayFlash = TimeSpan.FromSeconds(1);
        Trigger.Triggered += Triggered;
        Thread.Sleep(Timeout.Infinite); //TODO Add Program-Exit
    }
    
    static void Main(string[] args)
    {
        _ = new Program();
    }

    private static void ResetTasks()
    {
        _takePicture = Camera.TakePicture(DelayCamera);
        _triggerFlash = Flash.Trigger(DelayFlash);
    }

    private static void Triggered(PinEventTypes type)
    {
        _takePicture.Start();
        _triggerFlash.Start();
        while(_takePicture.IsCompleted == false && _triggerFlash.IsCompleted == false)
            Thread.Sleep(10);
        ResetTasks();
    }
}