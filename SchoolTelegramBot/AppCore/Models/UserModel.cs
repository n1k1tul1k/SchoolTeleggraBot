namespace SchoolTelegramBot.AppCore.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Class { get; set; }
    }
    //TODO:: Implement new field::Role ;)))
    public enum RoleEnum
    {
        SchoolLearner,
        Teacher,
        Admin,
    }
}