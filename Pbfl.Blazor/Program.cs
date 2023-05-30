using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Pbfl.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

const string resourceUrl = "http://localhost:5005";
const double timeoutSeconds = 300;

builder.Services.AddHttpClient("CDSClient", client =>
{
    // See https://learn.microsoft.com/powerapps/developer/data-platform/webapi/compose-http-requests-handle-errors
    client.BaseAddress = new Uri($"{resourceUrl}");
    client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
});

await builder.Build().RunAsync();