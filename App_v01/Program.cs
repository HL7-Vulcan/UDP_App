using UDP_App.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddScoped<UDP_App.Services.SessionWorkspace>();
builder.Services.AddScoped<UDP_App.Services.DataServiceTP>();
builder.Services.AddScoped<UDP_App.Services.SushiService>();
builder.Services.AddScoped<UDP_App.Services.DataService01>();
builder.Services.AddScoped<UDP_App.Services.DataService>();
builder.Services.AddScoped<UDP_App.Services.DataService05>();
builder.Services.AddScoped<UDP_App.Services.DataService06>();
builder.Services.AddScoped<UDP_App.Services.DataService07>();
builder.Services.AddScoped<UDP_App.Services.DataService08>();
builder.Services.AddScoped<UDP_App.Services.DataService09>();
builder.Services.AddScoped<UDP_App.Services.DataService04>();
builder.Services.AddScoped<UDP_App.Services.DataService10>();
builder.Services.AddScoped<UDP_App.Services.DataService11>();
builder.Services.AddScoped<UDP_App.Services.DataService12>();
builder.Services.AddScoped<UDP_App.Services.DataService13>();
builder.Services.AddScoped<UDP_App.Services.DataService14>();
builder.Services.AddSingleton<UDP_App.Services.SettingsService>();
builder.Services.AddSingleton<UDP_App.Services.TerminologyService>();
builder.Services.AddScoped<UDP_App.Services.FhirBrowseService>();
builder.Services.AddScoped<UDP_App.Services.BuildService>();
builder.Services.AddScoped<UDP_App.Services.FhirPipelineService>();
builder.Services.AddScoped<UDP_App.Services.AmendmentStore>();
builder.Services.AddSingleton<UDP_App.Services.FhirQuantityService>();

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
