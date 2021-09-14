using Telegram.Bot.Types.ReplyMarkups;

namespace SchoolTelegramBot.AppCore.Utils
{
    public class ReplyMarkupStorage
    {
        public static InlineKeyboardMarkup GetMainFunctionsKeyboard() 
            => new InlineKeyboardMarkup(new []
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отримати розклад на поточний день", "GetLessonsTimetable"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отримати розклад дзвінків", "GetBellsTimetable"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Отримати розклад на інший день", "ChangeDayCallback"),     
            }
        });
    }
}