## Trigger flash and camera after trigger.

### Camera
Camera is an Android Smartphone connected via [ADB](https://developer.android.com/tools/adb): [AdvancedSharpAdbClient](https://github.com/SharpAdb/AdvancedSharpAdbClient).

### Flash
Flash is connected via GPIO pin and Optocoupler.
Default pin `17`.

### Trigger
Trigger is an impulse read from GPIO.
Default pin `14`.

## API

Configure delays, Pins from REST-API: `http://<server>/swagger`
Check `openAPI.json`