using BethanysPieShopHRM.App.MessageHandlers;
using BlazorSokoban.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSokoban
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient<SokobanApiAuthorizationMessageHandler>();

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
            });

            builder.Services.AddAuthorizationCore(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    Policies.Policies.CanCreateLevels,
                    Policies.Policies.CanCreateLevelsPolicy());

                authorizationOptions.AddPolicy(
                    Policies.Policies.CanPlayLevels,
                    Policies.Policies.CanPlayLevelsPolicy());
            });

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient<ILevelDataService, LevelDataService>(client => client.BaseAddress = new Uri("https://localhost:44373/"))
                .AddHttpMessageHandler<SokobanApiAuthorizationMessageHandler>();


            await builder.Build().RunAsync();
        }
    }
}
