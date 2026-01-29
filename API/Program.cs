using System.Reflection;
using Microsoft.AspNetCore.HttpLogging;
using PhotographyOfMovingObjects;

//Initialize Static classes....
Console.WriteLine($"Trigger GPIO Pin: {Trigger.PinNumber}");
Console.WriteLine($"Flash GPIO Pin: {Flash.PinNumber}");
Console.WriteLine($"Delay Camera: {Photography.DelayCamera} Delay Flash: {Photography.DelayFlash}");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowAnyHeader();
    });
});

builder.Services.AddHttpLogging(httpLoggingOptions =>
{
    httpLoggingOptions.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.Response;
});

WebApplication app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseWebSockets();

app.UseCors();

app.UseHttpLogging();

app.Run();
