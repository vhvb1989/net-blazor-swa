﻿var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var backendUri = builder.Configuration["BACKEND_URI"] ?? "https://localhost:7181";
builder.Services.AddScoped(
    sp => new HttpClient
    {
        BaseAddress = new Uri(backendUri)
    });

builder.Services.AddHttpClient();
builder.Services.AddSingleton<OpenAIPromptQueue>();
builder.Services.AddLocalStorageServices();
builder.Services.AddSessionStorageServices();
builder.Services.AddSpeechSynthesisServices();
builder.Services.AddSpeechRecognitionServices();
builder.Services.AddMudServices();
builder.Services.AddLocalization();
builder.Services.AddScoped<CultureService>();

await JSHost.ImportAsync(
    moduleName: nameof(JavaScriptModule),
    moduleUrl: $"../js/i-frame.js?{Guid.NewGuid()}");

var host = builder.Build()
    .DetectClientCulture();

await host.RunAsync();
