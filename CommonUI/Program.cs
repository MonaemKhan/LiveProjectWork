using Blazored.Toast;
using CommonUI.Components;
using CommonUI.Configuration;

var builder = WebApplication.CreateBuilder(args);

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddHttpClient("ApiClient", client => client.BaseAddress = new Uri(apiBaseUrl));

//for Serversite render mode
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor().AddHubOptions(options =>
{
    options.MaximumReceiveMessageSize = (20*1024*1024); //20MB 
});

//configured Service
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureJsonNamingConvention();
builder.Services.ConfigureRepositoryWrapper();

builder.Services.AddBlazoredToast();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseHttpsRedirection();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
