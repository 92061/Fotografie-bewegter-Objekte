using PiCamera;

RpicamArgs camArgs = new ();
camArgs.Encoding(Encoding.Jpeg);
camArgs.Output(Output.File, "%d");

using Camera camera = new (RpiCameraApp.RpicamStill, camArgs);

camera.TakePicture();
File.WriteAllBytes("test.png", camera.GetPicture());

camera.TakePicture();
File.WriteAllBytes("test2.png", camera.GetPicture());