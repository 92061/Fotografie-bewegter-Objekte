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
        get => TimeSpan.FromMilliseconds(Camera.DelayMs);
        set => Camera.DelayMs = (int)value.TotalMilliseconds;
    }

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
    
    /// <summary>
    /// Stream to output the captured Image to
    /// </summary>
    public static Stream ImageStream
    {
        get => _imageStream;
        set
        {
            _imageStream = value;
            _takePicture = Camera.TakePictureTask(value);
        }
    }
    private static Stream _imageStream = new MemoryStream(1);

    private static Task _takePicture = Camera.TakePictureTask(_imageStream);
    private static Task _triggerFlash = Flash.FlashTask();
    
    
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
        _takePicture = Camera.TakePictureTask(ImageStream);
        _triggerFlash = Flash.FlashTask(_delayFlash);
    }
}