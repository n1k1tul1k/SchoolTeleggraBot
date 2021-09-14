using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolTelegramBot.AppCore;
using Telegram.Bot.Types;

namespace SchoolTelegramBot.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BotController
    {
        private readonly CoreProcessor _processor;

        public BotController(CoreProcessor coreProcessor)
        {
            _processor = coreProcessor;
        }

        [HttpGet]
        public string Check() => "OK";
        
        [HttpPost]
        public async Task Handle([FromBody] Update update)
        {
            await _processor.Handle(update);
        }
    }
}