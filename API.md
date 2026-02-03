# API | v1

> Version 1.0.0


## Path Table

| Method | Path | Description |
| --- | --- | --- |
| GET | [/Camera/Delay](#getcameradelay) | Returns the configured "Camera Delay". |
| PATCH | [/Camera/Delay](#patchcameradelay) | Sets the "Camera Delay". |
| POST | [/Camera/TakePicture](#postcameratakepicture) | Takes a photo. |
| GET | [/Camera/LatestPhoto](#getcameralatestphoto) | Gets the latest captured Image. |
| POST | [/Camera/Settings](#postcamerasettings) | Change the settings of the camera. |
| GET | [/Flash/Delay](#getflashdelay) | Returns the configured "Flash Delay". |
| PATCH | [/Flash/Delay](#patchflashdelay) | Sets the "Flash Delay". |
| GET | [/Flash/PinNumber](#getflashpinnumber) | Returns the GPIO Pin-Number of the Flash. |
| PATCH | [/Flash/PinNumber](#patchflashpinnumber) | Sets the GPIO Pin-Number of the Flash. |
| POST | [/Flash/Flash](#postflashflash) | Triggers the flash. |
| GET | [/Trigger/PinNumber](#gettriggerpinnumber) | Returns the GPIO Pin-Number of the Trigger. |
| PATCH | [/Trigger/PinNumber](#patchtriggerpinnumber) | Sets the GPIO Pin-Number of the Trigger. |
| GET | [/Trigger/Flank](#gettriggerflank) | Get the Flank on which the trigger executes. |
| PATCH | [/Trigger/Flank](#patchtriggerflank) | Sets the Flank on which the trigger should execute. |

## Reference Table

| Name | Path | Description |
| --- | --- | --- |
| AutoFocusMode | [#/components/schemas/AutoFocusMode](#componentsschemasautofocusmode) |  |
| AutoFocusRange | [#/components/schemas/AutoFocusRange](#componentsschemasautofocusrange) |  |
| AutofocusWindow | [#/components/schemas/AutofocusWindow](#componentsschemasautofocuswindow) |  |
| AwbMode | [#/components/schemas/AwbMode](#componentsschemasawbmode) |  |
| CameraSettings | [#/components/schemas/CameraSettings](#componentsschemascamerasettings) |  |
| DenoiseMode | [#/components/schemas/DenoiseMode](#componentsschemasdenoisemode) |  |
| Encoding | [#/components/schemas/Encoding](#componentsschemasencoding) |  |
| EntityTagHeaderValue | [#/components/schemas/EntityTagHeaderValue](#componentsschemasentitytagheadervalue) |  |
| ExposureMode | [#/components/schemas/ExposureMode](#componentsschemasexposuremode) |  |
| FileContentHttpResult | [#/components/schemas/FileContentHttpResult](#componentsschemasfilecontenthttpresult) |  |
| MeteringMode | [#/components/schemas/MeteringMode](#componentsschemasmeteringmode) |  |
| PinEventTypes | [#/components/schemas/PinEventTypes](#componentsschemaspineventtypes) |  |
| ReadOnlyMemoryOfbyte | [#/components/schemas/ReadOnlyMemoryOfbyte](#componentsschemasreadonlymemoryofbyte) |  |
| StringSegment | [#/components/schemas/StringSegment](#componentsschemasstringsegment) |  |

## Path Details

***

### [GET]/Camera/Delay

- Summary  
Returns the configured "Camera Delay".

- Description  
Returns the configured "Camera Delay".

#### Responses

- 200 OK

`text/plain`

```typescript
integer
```

***

### [PATCH]/Camera/Delay

- Summary  
Sets the "Camera Delay".

- Description  
Sets the "Camera Delay".

#### RequestBody

- application/json

```typescript
integer
```

- text/json

```typescript
integer
```

- application/*+json

```typescript
integer
```

#### Responses

- 200 OK

***

### [POST]/Camera/TakePicture

- Summary  
Takes a photo.

- Description  
Takes a photo.

#### Responses

- 200 OK

***

### [GET]/Camera/LatestPhoto

- Summary  
Gets the latest captured Image.

- Description  
Gets the latest captured Image.

#### Responses

- 200 OK

`text/plain`

```typescript
{
  contentType?: string
  fileDownloadName?: string
  lastModified?: string
  entityTag: {
    tag: {
      buffer?: string
      offset?: integer
      length?: integer
      value?: string
      hasValue?: boolean
    }
    isWeak?: boolean
  }
  enableRangeProcessing?: boolean
  fileLength?: integer
  fileContents?: string
}
```

`application/json`

```typescript
{
  contentType?: string
  fileDownloadName?: string
  lastModified?: string
  entityTag: {
    tag: {
      buffer?: string
      offset?: integer
      length?: integer
      value?: string
      hasValue?: boolean
    }
    isWeak?: boolean
  }
  enableRangeProcessing?: boolean
  fileLength?: integer
  fileContents?: string
}
```

`text/json`

```typescript
{
  contentType?: string
  fileDownloadName?: string
  lastModified?: string
  entityTag: {
    tag: {
      buffer?: string
      offset?: integer
      length?: integer
      value?: string
      hasValue?: boolean
    }
    isWeak?: boolean
  }
  enableRangeProcessing?: boolean
  fileLength?: integer
  fileContents?: string
}
```

***

### [POST]/Camera/Settings

- Summary  
Change the settings of the camera.

- Description  
Change the settings of the camera.

#### RequestBody

- application/json

```typescript
{
  mode?: string
  width?: integer
  height?: integer
  hflip?: boolean
  vflip?: boolean
  rotate180?: boolean
  shutterSpeed?: integer
  gain?: number
  ev?: number
  brightness?: number
  contrast?: number
  saturation?: number
  sharpness?: number
  autofocusWindow: {
    x: number
    y: number
    w: number
    h: number
  }
  quality?: integer
  raw?: boolean
}
```

- text/json

```typescript
{
  mode?: string
  width?: integer
  height?: integer
  hflip?: boolean
  vflip?: boolean
  rotate180?: boolean
  shutterSpeed?: integer
  gain?: number
  ev?: number
  brightness?: number
  contrast?: number
  saturation?: number
  sharpness?: number
  autofocusWindow: {
    x: number
    y: number
    w: number
    h: number
  }
  quality?: integer
  raw?: boolean
}
```

- application/*+json

```typescript
{
  mode?: string
  width?: integer
  height?: integer
  hflip?: boolean
  vflip?: boolean
  rotate180?: boolean
  shutterSpeed?: integer
  gain?: number
  ev?: number
  brightness?: number
  contrast?: number
  saturation?: number
  sharpness?: number
  autofocusWindow: {
    x: number
    y: number
    w: number
    h: number
  }
  quality?: integer
  raw?: boolean
}
```

#### Responses

- 200 OK

***

### [GET]/Flash/Delay

- Summary  
Returns the configured "Flash Delay".

- Description  
Returns the configured "Flash Delay".

#### Responses

- 200 OK

`text/plain`

```typescript
integer
```

***

### [PATCH]/Flash/Delay

- Summary  
Sets the "Flash Delay".

- Description  
Sets the "Flash Delay".

#### RequestBody

- application/json

```typescript
integer
```

- text/json

```typescript
integer
```

- application/*+json

```typescript
integer
```

#### Responses

- 200 OK

***

### [GET]/Flash/PinNumber

- Summary  
Returns the GPIO Pin-Number of the Flash.

- Description  
Returns the GPIO Pin-Number of the Flash.

#### Responses

- 200 OK

`text/plain`

```typescript
integer
```

***

### [PATCH]/Flash/PinNumber

- Summary  
Sets the GPIO Pin-Number of the Flash.

- Description  
Sets the GPIO Pin-Number of the Flash.

#### RequestBody

- application/json

```typescript
integer
```

- text/json

```typescript
integer
```

- application/*+json

```typescript
integer
```

#### Responses

- 200 OK

***

### [POST]/Flash/Flash

- Summary  
Triggers the flash.

- Description  
Triggers the flash.

#### Responses

- 200 OK

***

### [GET]/Trigger/PinNumber

- Summary  
Returns the GPIO Pin-Number of the Trigger.

- Description  
Returns the GPIO Pin-Number of the Trigger.

#### Responses

- 200 OK

`text/plain`

```typescript
integer
```

***

### [PATCH]/Trigger/PinNumber

- Summary  
Sets the GPIO Pin-Number of the Trigger.

- Description  
Sets the GPIO Pin-Number of the Trigger.

#### RequestBody

- application/json

```typescript
integer
```

- text/json

```typescript
integer
```

- application/*+json

```typescript
integer
```

#### Responses

- 200 OK

***

### [GET]/Trigger/Flank

- Summary  
Get the Flank on which the trigger executes.

- Description  
Get the Flank on which the trigger executes.

#### Responses

- 200 OK

`text/plain`

```typescript
string
```

`application/json`

```typescript
string
```

`text/json`

```typescript
string
```

***

### [PATCH]/Trigger/Flank

- Summary  
Sets the Flank on which the trigger should execute.

- Description  
Sets the Flank on which the trigger should execute.

#### RequestBody

- application/json

```typescript
string
```

- text/json

```typescript
string
```

- application/*+json

```typescript
string
```

#### Responses

- 200 OK

## References

### #/components/schemas/AutoFocusMode

```typescript
```

### #/components/schemas/AutoFocusRange

```typescript
```

### #/components/schemas/AutofocusWindow

```typescript
{
  x: number
  y: number
  w: number
  h: number
}
```

### #/components/schemas/AwbMode

```typescript
```

### #/components/schemas/CameraSettings

```typescript
{
  mode?: string
  width?: integer
  height?: integer
  hflip?: boolean
  vflip?: boolean
  rotate180?: boolean
  shutterSpeed?: integer
  gain?: number
  ev?: number
  brightness?: number
  contrast?: number
  saturation?: number
  sharpness?: number
  autofocusWindow: {
    x: number
    y: number
    w: number
    h: number
  }
  quality?: integer
  raw?: boolean
}
```

### #/components/schemas/DenoiseMode

```typescript
```

### #/components/schemas/Encoding

```typescript
```

### #/components/schemas/EntityTagHeaderValue

```typescript
{
  tag: {
    buffer?: string
    offset?: integer
    length?: integer
    value?: string
    hasValue?: boolean
  }
  isWeak?: boolean
}
```

### #/components/schemas/ExposureMode

```typescript
```

### #/components/schemas/FileContentHttpResult

```typescript
{
  contentType?: string
  fileDownloadName?: string
  lastModified?: string
  entityTag: {
    tag: {
      buffer?: string
      offset?: integer
      length?: integer
      value?: string
      hasValue?: boolean
    }
    isWeak?: boolean
  }
  enableRangeProcessing?: boolean
  fileLength?: integer
  fileContents?: string
}
```

### #/components/schemas/MeteringMode

```typescript
```

### #/components/schemas/PinEventTypes

```typescript
string
```

### #/components/schemas/ReadOnlyMemoryOfbyte

```typescript
string
```

### #/components/schemas/StringSegment

```typescript
{
  buffer?: string
  offset?: integer
  length?: integer
  value?: string
  hasValue?: boolean
}
```