namespace SchoolTelegramBot.AppCore.Models
{
    public class LessonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateOfWeek { get; set; }
        public int LessonNumber { get; set; }
        public string ClassName { get; set; }
    }
}