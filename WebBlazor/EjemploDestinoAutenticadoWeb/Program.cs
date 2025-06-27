using EjemploData.Common.MMS;
using EjemploData.DALs;
using EjemploData.DALs.MSSDALs;
using EjemploData.Services;
using EjemploDestinoAutenticadoWeb.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;

//Microsoft.AspNetCore.Authentication.JwtBearer
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

#region entidades de datos y negocio
builder.Services.AddSingleton<SqlConnection>(sp =>
{
    var configuration = sp.GetService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("CadenaConexion");
    return new SqlConnection(connectionString);
});

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

#region autentificación externar - despues de las cookies de autenticación
builder.Services.AddAuthentication()
    .AddJwtBearer("JWT", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "App2", // Identificador de la aplicación origen
            ValidAudience = "App1", // Identificador de la aplicación destino
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu-clave-secreta-muy-segura-de-al-menos-32-caracteres"))
        };
    });
#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
