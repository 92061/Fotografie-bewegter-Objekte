using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.SignalR;
using PhotographyOfMovingObjects;
using Project;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi().ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddHttpLogging(httpLoggingOptions =>
{
    httpLoggingOptions.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.Response;
});

builder.Services.AddSignalR();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.MapControllers();

app.UseFileServer();

app.UseCors();

app.UseHttpLogging();

app.MapHub<NotificationHub>("/notifications");

try
{
    //Initialize Static classes....
    Console.WriteLine($"Trigger GPIO Pin: {Trigger.PinNumber}");
    Console.WriteLine($"Flash GPIO Pin: {Flash.PinNumber}");
    Console.WriteLine($"Delay Camera: {Photography.DelayCamera} Delay Flash: {Photography.DelayFlash}");

    // Set-up SignalR notifications
    Camera.PictureTaken += async () =>
    {
        if (app.Services.GetService<IHubContext<NotificationHub>>() is not { } hub)
            return;
        await hub.Clients.All.SendAsync("snap");
    };
    Camera.PictureReady += async () =>
    {
        if (app.Services.GetService<IHubContext<NotificationHub>>() is not { } hub)
            return;
        await hub.Clients.All.SendAsync("picture");
    };
    Trigger.Triggered += async type =>
    {
        if (app.Services.GetService<IHubContext<NotificationHub>>() is not { } hub)
            return;
        await hub.Clients.All.SendAsync("trigger", type);
    };
    Flash.Triggered += async () =>
    {
        if (app.Services.GetService<IHubContext<NotificationHub>>() is not { } hub)
            return;
        await hub.Clients.All.SendAsync("flash");
    };
}
catch (Exception)
{
    Console.WriteLine("Failed starting dependencies.");
}

app.Run();
 