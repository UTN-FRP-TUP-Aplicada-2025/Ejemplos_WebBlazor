


Requiere este nuget.
```
Microsoft.AspNetCore.Authentication.JwtBearer
```


En el Program.cs agregar despues e la definición del esquema de autenticación por cookies
```csharp
#region autentificación externar - despues de las cookies de autenticación
  builder.Services.AddAuthentication().AddJwtBearer("JWT", options =>
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
```

En Login.razor se agrega  lo siguiente;

```
[Inject] IHttpContextAccessor HttpContextAccessor { get; set; }
```

y estos dos metodos
```
protected override async Task OnInitializedAsync()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            var ssoToken = httpContext.Request.Query["sso_token"];
            if (!string.IsNullOrEmpty(ssoToken))
            {
                await ProcessSSOToken(ssoToken);
                return;
            }
        }
    }

    async Task ProcessSSOToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("tu-clave-secreta-muy-segura-de-al-menos-32-caracteres");

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "App2",
                ValidAudience = "App1",
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Extraer claims del token
            var nombreUsuario = principal.FindFirst(ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                // Crear sesión automáticamente
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, nombreUsuario),
                    new Claim("CUIT", "23244324"), // claim personalizada
                    new Claim("LoginMethod", "SSO")
                };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var cookiePrincipal = new ClaimsPrincipal(identity);

                var httpContext = HttpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    await httpContext.SignInAsync("Cookies", cookiePrincipal);
                    var returnUrl = httpContext.Request.Query["returnurl"];
                    if (string.IsNullOrEmpty(returnUrl))
                        returnUrl = "/personas";

                    Navigation.NavigateTo(returnUrl, forceLoad: true);
                }
            }
        }
        catch (Exception ex)
        {
            error = "Token SSO inválido: " + ex.Message;
            isError = true;
        }
    }
```