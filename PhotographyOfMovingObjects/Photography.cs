using System.Device.Gpio;

namespace PhotographyOfMovingObjects;

public static class Photography
{
    static void Main(string[] args)
    {
        Thread.Sleep(Timeout.Infinite); //TODO Add Program-Exit
    }
    
    public static TimeSpan DelayCamera { get; private set; } = TimeSpan.FromMilliseconds(0);
    public static TimeSpan DelayFlash { get; private set; } = TimeSpan.FromMilliseconds(0);
    public static TimeSpan FallDelay { get; private set; } = TimeSpan.FromMilliseconds(100);
    
    private static Task<bool> _takePicture = Camera.TakePicture(DelayCamera);
    private static Task _triggerFlash = Flash.Trigger(DelayFlash);

    public static bool Armed = false;
    
    static Photography()
    {
        Trigger.Triggered += Triggered;
    }

    private static void Triggered(PinEventTypes type)
    {
        if (type is not PinEventTypes.Rising)
            return;
        if (!Armed)
            Console.WriteLine("Triggered, but not armed!");
        Console.WriteLine("Trigger received. Fall delay...");
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
        Console.WriteLine($"Reset. Status {(Armed ? "Armed" : "Disarmed")}");
    }

    public static void Arm() => Armed = true;
    public static void Disarm() => Armed = false;
}