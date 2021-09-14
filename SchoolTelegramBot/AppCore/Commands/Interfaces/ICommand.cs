using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SchoolTelegramBot.Controllers
{
    public interface ICommand
    {
        bool CanExecute(Update update); 
        Task Execute(Update update, ITelegramBotClient client);
    }
}