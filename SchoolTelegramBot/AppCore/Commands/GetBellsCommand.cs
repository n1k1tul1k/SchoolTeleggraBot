using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolTelegramBot.AppCore.Utils;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SchoolTelegramBot.AppCore.Commands
{
    public class GetBellsCommand: ICommand
    {

        private readonly Dictionary<int, string> _bellsTimetableDictionary = new Dictionary<int, string>()
        {
            { 1, "8.00-8.45" },
            { 2, "8.55-9.40" },
            { 3, "9.55-10.40" },
            { 4, "10.55-11.40" },
            { 5, "11.55-12.40" },
            { 6, "12.50-13.35" },
            { 7, "14.40-15.25" },
            { 8, "14.40-15.25" }
        };
        
        public bool CanExecute(Update update) => 
            update.Type == UpdateType.CallbackQuery 
            && update.CallbackQuery?.Data == "GetBellsTimetable";

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            var chatId = update.CallbackQuery?.From.Id ?? 0 ;
            var messageId = update.CallbackQuery?.Message?.MessageId ?? 0;
            List<string> formattedBells = _bellsTimetableDictionary
                .Select(x => $"{x.Key}. {x.Value}")
                .ToList();
            
            var message = String.Join('\n', formattedBells);
            await TelegramClientUtils.SendMessageWithDeleting(client, message, messageId, chatId, ReplyMarkupStorage.GetMainFunctionsKeyboard() );
        }
    }
}