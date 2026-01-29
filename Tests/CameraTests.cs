using CameraTrigger;

namespace Tests;

public class CameraTests
{
    [Fact]
    public void TestAdbBridge()
    {
        Assert.True(Camera.Connect());
    }

    [Fact]
    public void TestStartCamera()
    {
        Camera.Connect();
        Assert.True(Camera.StartCameraApp());
    }

    [Fact]
    public void TestTakingPicture()
    {
        Camera.Connect();
        Assert.True(Camera.TakePicture());
    }
}