using System.Collections.Generic;

namespace SchoolTelegramBot.AppCore.Utils
{
    public class StringsUtils
    {
        public static Dictionary<string, string> DaysOfWeekTranslation = new Dictionary<string, string>() 
        {
            { "Monday" , "Понеділок"},
            {"Tuesday", "Вівторок"},
            {"Wednesday" , "Середа"},
            {"Thursday", "Четверг"},
            {"Friday", "П'ятниця"}
        };
}
}