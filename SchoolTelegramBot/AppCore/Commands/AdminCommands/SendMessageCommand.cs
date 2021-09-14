using System.Linq;
using System.Threading.Tasks;
using SchoolTelegramBot.AppCore.DB;
using SchoolTelegramBot.AppCore.Models;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SchoolTelegramBot.AppCore.Commands.AdminCommands
{
    public class SendMessageCommand : ICommand
    {
        //TODO:: HOTFIX
        private bool isAdmin(long chatId)
        {
            return chatId == 1034522809 ;
        }
        public bool CanExecute(Update update) =>
            isAdmin(update?.Message?.Chat.Id ?? 0) == true
            && update.Message?.Type == MessageType.Text 
            && update.Message?.Text?.StartsWith("/sendmsg") == true;
       
        public async Task Execute(Update update, ITelegramBotClient client)
        {
            using (var ctx = new ApplicationContext())
            {
                var updateMessage = update.Message?.Text;
                var message = string.Join(' ', 
                    updateMessage.Split(' ').Skip(1));
                var ctxUsers = ctx.Users;
                foreach (var user in ctxUsers)
                {
                    var userId = user.UserId;
                    await client.SendTextMessageAsync(userId, message);
                }
            }
        }
    }
}