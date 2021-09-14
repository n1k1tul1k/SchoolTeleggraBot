using System;
using System.Linq;
using System.Threading.Tasks;
using SchoolTelegramBot.AppCore.DB;
using SchoolTelegramBot.AppCore.Utils;
using SchoolTelegramBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SchoolTelegramBot.AppCore.Commands
{
    public class GetTimetableCommand : ICommand
    {
        public bool CanExecute(Update update)
        {
            return update.Type == UpdateType.CallbackQuery
                   && update.CallbackQuery?.Data == "GetLessonsTimetable";
        }

        public async Task Execute(Update update, ITelegramBotClient client)
        {
            using (var ctx = new ApplicationContext())
            {
                var chatId = update.CallbackQuery?.Message?.Chat.Id;
                if (chatId != null)
                {
                    var className = ctx.Users?.Where(x => x.UserId == chatId).First().Class;
                    var messageId = update.CallbackQuery?.Message?.MessageId ?? 0;
                    var dateOfWeek =DateTime.Today.DayOfWeek .ToString(); //TODO::FIX!
                    var lessonsQueue =
                        ctx.Lessons
                            .Where(lesson => lesson.ClassName == className && lesson.DateOfWeek == dateOfWeek)
                            .OrderBy(x => x.LessonNumber)
                            .Select(lesson =>
                                $"{lesson.LessonNumber}. {char.ToUpper(lesson.Name[0]) + lesson.Name.Remove(0, 1)}");
                    if (lessonsQueue.Count() > 0)
                    {
                        var formattedLessons =
                            $"{className};{StringsUtils.DaysOfWeekTranslation[dateOfWeek]}\r\n" +
                            string.Join("\n", lessonsQueue);
                        await client.DeleteMessageAsync(chatId, messageId);
                        await client.SendTextMessageAsync(chatId, formattedLessons, 
                            replyMarkup: ReplyMarkupStorage.GetMainFunctionsKeyboard());
                    }
                    else
                    {
                        await client.DeleteMessageAsync(chatId, messageId);
                        await client.SendTextMessageAsync(chatId,
                            "На поточний день, не було знайдено будь яких уроків. ",
                            replyMarkup: ReplyMarkupStorage.GetMainFunctionsKeyboard());
                    }
                }
            }
        }
    }
}