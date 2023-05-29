using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

namespace Pbfl.Server.Helpers;

public static class AuthHelper
{
    public static WebApplicationBuilder AddOurAuthentication(this WebApplicationBuilder builder)
    {
        var siteUri = Environment.GetEnvironmentVariable("SITE_URI") ?? (!builder.Environment.IsDevelopment() ? "https://localhost/" : null);
        var siteUriBuilder = siteUri != null ? new UriBuilder(siteUri) : null;
        if (siteUriBuilder != null)
        {
            Log.Information("SITE_URI: " + siteUriBuilder);
        }

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (context) =>
                    {
                        if (siteUriBuilder != null)
                        {
                            var uriBuilder = new UriBuilder(context.RedirectUri)
                            {
                                Scheme = siteUriBuilder.Scheme,
                                Host = siteUriBuilder.Host,
                                Port = siteUriBuilder.Port
                            };

                            var uri = uriBuilder.ToString();
                            context.HttpContext.Response.Redirect(uri);
                        }
                        else
                        {
                            context.HttpContext.Response.Redirect(context.RedirectUri);
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        return builder;
    }
}