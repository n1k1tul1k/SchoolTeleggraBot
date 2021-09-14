using System.Threading.Tasks;
using SchoolTelegramBot.AppCore.Utils;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SchoolTelegramBot.AppCore.Commands
{
    public class GetTimetableDays : ICommand
    {
        public bool CanExecute(Update update)
            => update.CallbackQuery?.Data == "ChangeDayCallback" && update.CallbackQuery.Message != null;

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            var chatId = update.CallbackQuery?.Message?.Chat.Id ?? 0;
            var messageId = update.CallbackQuery?.Message?.MessageId ?? 0;
            InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Понеділок", "ChangeDay_Monday")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Вівторок", "ChangeDay_Tuesday")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Середа", "ChangeDay_Wednesday")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Четверг", "ChangeDay_Thursday")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("П'ятниця", "ChangeDay_Friday")
                },
            });
            await TelegramClientUtils.SendMessageWithDeleting(client, "Обери потрібний день 👇",messageId, 
                chatId, 
                inlineKeyboardMarkup);
        }
    }
}