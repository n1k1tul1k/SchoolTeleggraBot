using SchoolTelegramBot.AppCore.DB;
using SchoolTelegramBot.AppCore.Models;
using SchoolTelegramBot.AppCore.Utils;
using SchoolTelegramBot.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace SchoolTelegramBot.AppCore.Commands
{
    public class StartCommand : ICommand
    {
        public bool CanExecute(Update update) =>
            update.Message?.Text?.StartsWith("/start") == true;
        
        public async Task Execute(Update update, ITelegramBotClient client)
        {
            using (var context = new ApplicationContext())
            {
                
                var chatId = update?.Message?.Chat.Id;
                
                if (context.Users.FirstOrDefault(x => x.UserId == chatId) == null && chatId != null)
                {
                    StateProcessor.AddNew(chatId.Value);
                    var firstName = update.Message.Chat.FirstName;
                    var lastName = update.Message.Chat.LastName;
                    var username = update.Message.Chat.Username;
                    context.Users.Add(new UserModel()
                        {UserId = chatId.Value, Name = firstName, Surname = lastName, Username = username});
                    await context.SaveChangesAsync();
                    var rmk = new ReplyKeyboardMarkup(new[]
                    {
                        KeyboardButton.WithRequestContact("Підтвердити особистість."),
                    }) { ResizeKeyboard = true};

                    await client.SendTextMessageAsync(chatId,
                        "Привіт 😉 \nСпочатку тобі траба підтвердити свою особистість, це зовсім не складно 👇🏼 ",
                        replyMarkup: rmk);
                }
                else
                    await client.SendTextMessageAsync(chatId,
                        "Повторний запуск бота!\nБудь ласка, оберіть потрібну команду.", replyMarkup: ReplyMarkupStorage.GetMainFunctionsKeyboard());
            }
        }
    }
}