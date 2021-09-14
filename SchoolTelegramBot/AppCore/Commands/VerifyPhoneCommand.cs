using System.Linq;
using System.Threading.Tasks;
using SchoolTelegramBot.AppCore.DB;
using SchoolTelegramBot.AppCore.Models;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SchoolTelegramBot.AppCore.Commands
{
    public class VerifyPhoneCommand : ICommand
    {
        public bool CanExecute(Update update)
            => update.Type == UpdateType.Message &&
               update.Message?.Contact != null;
        

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            using (var ctx = new ApplicationContext())
            {
                var phoneNumber = update.Message?.Contact?.PhoneNumber;
                var chatId = update.Message?.Chat.Id;
                var currentUser = ctx.Users?.Where(x => x.UserId == chatId).First();
                currentUser.PhoneNumber = phoneNumber;
                await ctx.SaveChangesAsync();
                StateProcessor.UpdateState(chatId.Value, StateEnum.PhoneVerified);
                await client.SendTextMessageAsync(chatId, "Надішліть клас, в якому ви навчаєтесь (11А, 7Б, 5В ...)");
            }
        }
    }
}