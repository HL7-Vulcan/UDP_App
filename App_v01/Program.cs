using UDP_App.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<UDP_App.Services.DataService>();
builder.Services.AddScoped<UDP_App.Services.DataService05>();
builder.Services.AddScoped<UDP_App.Services.DataService06>();
builder.Services.AddScoped<UDP_App.Services.DataService07>();
builder.Services.AddScoped<UDP_App.Services.DataService08>();
builder.Services.AddScoped<UDP_App.Services.DataService09>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
