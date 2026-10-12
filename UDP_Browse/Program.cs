using UDP_Browse.Components;
using UDP_Browse.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<SettingsService>();
builder.Services.AddScoped<FhirBrowseService>();
builder.Services.AddScoped<BuildService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .DisableAntiforgery();

app.Run();
