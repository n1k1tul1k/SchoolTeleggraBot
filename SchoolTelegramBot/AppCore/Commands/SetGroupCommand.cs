using System.Linq;
using System.Threading.Tasks;
using SchoolTelegramBot.AppCore.DB;
using SchoolTelegramBot.AppCore.Models;
using SchoolTelegramBot.AppCore.Utils;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SchoolTelegramBot.AppCore.Commands
{
    public class SetGroupCommand : ICommand
    {
        public bool CanExecute(Update update) 
            =>  update.Message?.Text?.Length <= 3 
                && StateProcessor.GetState(update.Message?.Chat.Id ?? 0) == StateEnum.PhoneVerified;

        private async Task SendWelcomeMessages(long chatId, ITelegramBotClient client)
        {
            await client.SendTextMessageAsync(chatId, "Дякую за реєстрацію!", replyMarkup: new ReplyKeyboardRemove());
            await client.SendTextMessageAsync(chatId, "Оберіть потрібну дію",
                replyMarkup: ReplyMarkupStorage.GetMainFunctionsKeyboard());
        }
        public async Task Execute(Update update, ITelegramBotClient client)
        {
            var chatId = update.Message?.Chat.Id ?? 0;
            var messageData = update.Message?.Text ?? "d";
            (string number, char character) data = ("default", 'd');
            switch (messageData.Length)
            {
                case 2:
                    data = (messageData[0].ToString(), messageData[1]);
                    break;
                case 3:
                    data = ($"{messageData[0]}{messageData[1]}", messageData[2]);
                    break;
            }
            int number = 0 ;
            
            if (int.TryParse(data.number, out number) && char.IsLetter(data.character))
            {
                using (var ctx = new ApplicationContext())
                {
                    var user = ctx.Users.First(x => x.UserId == chatId);
                    ctx.States.First(x => x.UserId == chatId).State = StateEnum.Verified;
                    user.Class = messageData
                        .ToUpper()
                        .Replace('А', 'A')
                        .Replace('Б', 'B')
                        .Replace('В', 'C');
                    await ctx.SaveChangesAsync();
                }

                await SendWelcomeMessages(chatId, client);

            }
            else
                await client.SendTextMessageAsync(chatId, "Невірний формат.");
        }
    }
}