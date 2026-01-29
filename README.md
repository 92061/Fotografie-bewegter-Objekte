# Fotografie bewegter Objekte

Takes a picture using [`rpicam-still`](https://www.raspberrypi.com/documentation/computers/camera_software.html#rpicam-still) when a GPIO Pin (Trigger) is set high.
Additionally sets off a flash by setting another GPIO-Pin high.

Both flash and camera are set off on a configurable delay.

![image](frontend/Website-Screenshot.png)


## Getting Started

### Build Requirements

- [ASP.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [node.js](https://nodejs.org/en)
- [npm](https://www.npmjs.com/)

### Build

Use the provided [`build.sh`](build.sh) script.

The script will generate the current OpenApi definition ([`API/API.json`](API/API.json)), build the website and generate the executable.

### Run

#### Requirements

- [ASP.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

#### Start

Run `dotnet API.dll`

## Built with

- [ASP.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [node.js](https://nodejs.org/en)
- [npm](https://www.npmjs.com/)
- [System.Device.Gpio](https://github.com/dotnet/iot)
- [Openur.Mono.Unix](https://github.com/mono/mono.posix)
- [Swashbuckle.AspNetCore.SwaggerUI](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Microsoft.AspNetCore.OpenApi](https://github.com/Microsoft/OpenAPI.NET)
- [Nuxt](https://nuxt.com/)
  - [Nuxt UI](https://ui.nuxt.com/)
  - [NuxtOpenFetch](https://nuxt-open-fetch.norbiros.dev/)