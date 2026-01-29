using System.Reflection;
using PhotographyOfMovingObjects;

//Initialize Static classes....
Console.WriteLine($"Trigger GPIO Pin: {Trigger.PinNumber}");
Console.WriteLine($"Flash GPIO Pin: {Flash.PinNumber}");
Console.WriteLine($"Camera app: {Camera.DefaultCameraApp}");
Console.WriteLine($"Delay Fall: {Photography.FallDelay} Delay Camera: {Photography.DelayCamera} Delay Flash: {Photography.DelayFlash}");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
