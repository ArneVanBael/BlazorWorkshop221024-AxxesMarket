using Duende.Bff.Yarp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddBff()
    .AddServerSideSessions()
    .AddRemoteApis();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = "cookie";
//    options.DefaultChallengeScheme = "oidc";
//    options.DefaultSignOutScheme = "oidc";
//})
//    .AddCookie("cookie", options =>
//    {
//        options.Cookie.Name = "__Host-blazor";
//        options.Cookie.SameSite = SameSiteMode.Strict;
//    })
//    .AddOpenIdConnect("oidc", options =>
//    {
//        //options.Authority = "https://localhost:7138/";
//        options.Authority = "https://demo.duendesoftware.com/";

//        // confidential client using code flow + PKCE
//        //options.ClientId = "AxxesMarket.SPA";
//        options.ClientId = "interactive.confidential";
//        options.ClientSecret = "secret";
//        options.ResponseType = "code";
//        options.ResponseMode = "query";

//        options.MapInboundClaims = false;
//        options.GetClaimsFromUserInfoEndpoint = true;
//        options.SaveTokens = true;

//        // request scopes + refresh tokens
//        options.Scope.Clear();
//        options.Scope.Add("openid");
//        options.Scope.Add("profile");
//        options.Scope.Add("api");
//        options.Scope.Add("offline_access");
//    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapRazorPages();


app.UseEndpoints(endpoints =>
{
    endpoints.MapBffManagementEndpoints();

    // remote endpoint
    endpoints.MapRemoteBffApiEndpoint("/api", "https://localhost:7138/api")
        .WithOptionalUserAccessToken();

    // MVC controllers
    endpoints.MapControllers()
        .RequireAuthorization()    // no anonymous access
        .AsBffApiEndpoint();       // BFF pre/post processing


    endpoints.MapFallbackToFile("index.html");
});


app.Run();