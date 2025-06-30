using EjemploWeb.Components;
using Microsoft.AspNetCore.Components.Server;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

if (builder.Environment.IsDevelopment())
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}


#region entidades
builder.Services.AddSingleton<EjemploWeb.Services.PersonasService>();
#endregion

#region 
//Límite de mensaje SignalR: Por defecto, SignalR tiene límites de tamaño de mensaje
//Timeout de JSInterop: Las llamadas JavaScript tienen timeouts configurados
//Buffer del WebSocket: El WebSocket puede saturarse con datos grandes
builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 1024 * 1024; // 1MB
    options.StreamBufferCapacity = 50;
});

builder.Services.Configure<CircuitOptions>(options =>
{
    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(5);
});
#endregion

var app = builder.Build();

#region para el manejo de fechas - despues del var app
var cultureInfo = new CultureInfo("es-ES");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
