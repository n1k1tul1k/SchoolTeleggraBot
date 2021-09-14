using SchoolTelegramBot.AppCore.Models;
using SchoolTelegramBot.AppCore.DB;

namespace SchoolTelegramBot.AppCore
{
    public static class LessonsProcessor
    {
        public static void AddLesson(string lessonName, int lessonNumber, 
            string dateOfWeek, string className)
        {
            using (var ctx = new ApplicationContext())
            {
                ctx.Lessons.Add(new LessonModel
                {
                    LessonNumber = lessonNumber,
                    Name = lessonName,
                    DateOfWeek = dateOfWeek,
                    ClassName = className
                });
            }
        }

        private static void AddLessons()
        {
            
        }
    }
}