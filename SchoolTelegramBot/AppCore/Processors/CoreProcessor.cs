using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SchoolTelegramBot.AppCore
{
    public class CoreProcessor
    {
        private readonly ITelegramBotClient _client;
        private readonly IEnumerable<ICommand> _commands;
        
        public CoreProcessor(ITelegramBotClient botClient, IEnumerable<ICommand> commands)
        {
            _commands = commands;
            _client = botClient;
        }

        public async Task Handle([FromBody] Update update)
        {
            var chatId = update.Message?.Chat.Id ?? update.CallbackQuery?.Message?.Chat.Id;
            var cmd = _commands.FirstOrDefault(s => s.CanExecute(update));

            if (cmd != null)
                await cmd.Execute(update, _client);
            else
                if(chatId != null)
                     await _client.SendTextMessageAsync(chatId, "Невідома команда ;( ");
        }
    }
}