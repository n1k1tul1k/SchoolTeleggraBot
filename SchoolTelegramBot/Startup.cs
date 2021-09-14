using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SchoolTelegramBot.AppCore;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;

namespace SchoolTelegramBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var botToken = Configuration.GetValue<string>("BotToken");
            var telegramBotClient = new TelegramBotClient(botToken);
            var assemblie = Assembly.GetExecutingAssembly();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<ITelegramBotClient>(telegramBotClient);
            services.AddTransient<CoreProcessor>();
            services.Scan(scan =>
                scan
                    .FromAssemblies(assemblie)
                    .AddClasses(classes => classes.AssignableTo<ICommand>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SchoolTelegramBot", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITelegramBotClient telegramBotClient)
        {
            var url = Configuration.GetValue<string>("Url");
            telegramBotClient.SetWebhookAsync($"{url}/api/bot");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolTelegramBot v1"));
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}