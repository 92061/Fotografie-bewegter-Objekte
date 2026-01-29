using CameraTrigger;

namespace Tests;

public class CameraTests
{
    public CameraTests()
    {
        Camera.Connect();
    }
    
    [Fact]
    public void TestAdbBridge()
    {
        Assert.True(Camera.Connected, "Camera not connected");
    }

    [Fact]
    public void TestStartCamera()
    {
        Assert.True(Camera.StartCameraApp(), "Could not start camera");
    }

    [Fact]
    public async Task TestTakingPicture()
    {
        Task<bool> takePicture = Camera.TakePicture();
        takePicture.Start();
        await takePicture;
        Assert.True(takePicture is { Result: true }, "Unable to take picture");
    }
}