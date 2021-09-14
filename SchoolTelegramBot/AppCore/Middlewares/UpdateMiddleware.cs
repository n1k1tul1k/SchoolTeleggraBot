using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace SchoolTelegramBot.AppCore.Middlewares
{
    public class UpdateMiddleware 
    {
        public async Task InvokeAsync(HttpContext context, [FromBody]Update update, RequestDelegate next)
        {
            if (update?.Message?.Chat.Id != null || update?.CallbackQuery?.Message?.Chat.Id != null)
                await next.Invoke(context);
        }
    }
}