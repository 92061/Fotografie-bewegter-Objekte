using System.Device.Gpio;

namespace PhotographyOfMovingObjects;

public static class Photography
{
    static void Main(string[] args)
    {
        Thread.Sleep(Timeout.Infinite); //TODO Add Program-Exit
    }
    
    /// <summary>
    /// The delay between Trigger and the picture being taken
    /// </summary>
    public static TimeSpan DelayCamera
    {
        get => _delayCamera;
        set
        {
            _delayCamera = value;
            ResetTasks();
        }
    }
    private static TimeSpan _delayCamera = TimeSpan.Zero;

    /// <summary>
    /// The delay between Trigger and the flash triggering
    /// </summary>
    public static TimeSpan DelayFlash
    {
        get => _delayFlash;
        set
        {
            _delayFlash = value;
            ResetTasks();
        }
    }
    private static TimeSpan _delayFlash = TimeSpan.Zero;

    /// <summary>
    /// On which flank of the GPIO input do we Trigger
    /// </summary>
    public static PinEventTypes TriggerOn = PinEventTypes.Rising;

    public static byte[] LatestPicture = [];

    private static Task _takePicture = new (async void () =>
    {
        try
        {
            LatestPicture = await Camera.TakePicture(CancellationToken.None, DelayCamera);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    });
    private static Task _triggerFlash = new (() => Flash.Trigger(DelayFlash));
    
    
    static Photography()
    {
        Trigger.Triggered += Triggered;
    }

    /// <summary>
    /// Handles the GPIO Trigger event
    /// </summary>
    /// <param name="type"></param>
    private static void Triggered(PinEventTypes type)
    {
        if (type != TriggerOn)
        {
            Console.WriteLine("Wrong flank!");
            return;
        }
        
        _takePicture.Start();
        _triggerFlash.Start();
        Console.WriteLine("Triggered!");
        Task.WaitAll(_takePicture, _triggerFlash);
        ResetTasks();
    }
    
    private static void ResetTasks()
    {
        _takePicture = new (async void () =>
        {
            try
            {
                LatestPicture = await Camera.TakePicture(CancellationToken.None, DelayCamera);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
        _triggerFlash = new Task(() => Flash.Trigger(DelayFlash));
    }
}