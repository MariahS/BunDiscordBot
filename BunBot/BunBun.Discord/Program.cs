using BunBun.Discord.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BunBun.Discord
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        
        public async Task MainAsync()
        {
            using (var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();
                

                client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                // Starts up bot on discord
                await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("discordToken"));
                await client.StartAsync();

                // Starts CommandHandling Services
                await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                await Task.Delay(-1);
            }
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            var config = new DiscordSocketConfig { MessageCacheSize = 100 };

            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<XivAppService>()
                .AddSingleton<FfLogsService>()
                .AddSingleton<HelperExtentions>()
                .BuildServiceProvider();
        }
    }

}
