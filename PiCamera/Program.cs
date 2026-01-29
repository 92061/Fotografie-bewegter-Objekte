using PiCamera;

RpicamArgs camArgs = new ();
camArgs.Encoding(Encoding.Jpeg);
camArgs.Output(Output.Stream);

using Camera camera = new (RpiCameraApp.RpicamStill, camArgs);

camera.TakePicture();
File.WriteAllBytes("test.png", await camera.GetPicture(Output.Stream));

camera.TakePicture();
File.WriteAllBytes("test2.png", await camera.GetPicture(Output.Stream));