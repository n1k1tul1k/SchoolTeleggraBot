using System.Linq;
using System.Threading.Tasks;
using SchoolTelegramBot.AppCore.DB;
using SchoolTelegramBot.AppCore.Utils;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SchoolTelegramBot.AppCore.Commands
{
    public class GetTimetableForInputDay : ICommand
    {
        public bool CanExecute(Update update)
            => update.CallbackQuery?.Data?.StartsWith("ChangeDay") == true && update.CallbackQuery?.Message != null;
        
        public async Task Execute(Update update, ITelegramBotClient client)
        {
            var inputDay = update.CallbackQuery?.Data?.Split('_')[1];
            using (var ctx = new ApplicationContext())
            {
                var chatId = update.CallbackQuery?.Message?.Chat.Id ?? 0;
                var messageId = update.CallbackQuery?.Message?.MessageId ?? 0;
                var sendData = "На даний день уроків не було знайдено ;)";  
                var groupName = ctx
                    .Users
                    .First(x => x.UserId == chatId).Class;
                var timeTableArr = ctx.Lessons
                    .Where(x => x.DateOfWeek == inputDay 
                                                       && x.ClassName == groupName)
                    .OrderBy(x => x.LessonNumber)
                    .Select(lesson => $"{lesson.LessonNumber}. {char.ToUpper(lesson.Name[0]) + lesson.Name.Remove(0,1)}");
                
                if (timeTableArr.Count() > 0)
                    sendData = $"{groupName}\r\n" + string.Join('\n', timeTableArr);

                try
                {
                    await TelegramClientUtils.SendMessageWithDeleting(client, sendData, messageId, chatId,
                        ReplyMarkupStorage.GetMainFunctionsKeyboard());
                }
                finally
                {
                    //LOGGING
                }
            }
        }
    }
}