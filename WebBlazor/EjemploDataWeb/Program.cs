using EjemploData.DALs.MSSDALs;
using EjemploData.DALs;
using EjemploData.Services;
using EjemploDataWeb.Components;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using EjemploData.Common.MMS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

#region entidades de datos y negocio
builder.Services.AddSingleton<SqlConnection>(sp =>
{
    var configuration = sp.GetService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("CadenaConexion");
    return new SqlConnection(connectionString);
});

//La conexión se mantiene viva solo durante la duración de una solicitud HTTP.
//Es más eficiente en aplicaciones con múltiples solicitudes simultáneas.
builder.Services.AddScoped<ITransaction<SqlTransaction>, SqlServerTransaction>();

builder.Services.AddScoped<PersonasMSSDAL>();
builder.Services.AddScoped<UsuariosMSSDAL>();
builder.Services.AddScoped<RolesMSSDAL>();
builder.Services.AddScoped<UsuariosRolesMSSDAL>();
//
builder.Services.AddScoped<PersonasService>();
builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<RolesService>();
#endregion

#region autentificación-autorizacion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token"; //default Cookie
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        options.ReturnUrlParameter = "returnurl";
        //
        options.Cookie.IsEssential = true;//algunos navegadores bloquean las cookies que no son esenciales
        options.Cookie.MaxAge = null;// TimeSpan.FromMinutes(30);
        //                             //options.IdleTimeout = TimeSpan.FromDays(30); //tiempo de inactividad
        options.Cookie.HttpOnly = true; //evita acceso de javascript
        options.Cookie.SameSite = SameSiteMode.Strict;// Lax para casos como OAuth, OpenID Connect, etc.
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
#endregion

var app = builder.Build();

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
