namespace SchoolTelegramBot.AppCore.Models
{
    public class StateModel
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public StateEnum State { get; set; }
    }

    public enum StateEnum
    {
        Newfag,
        PhoneVerified,
        Verified
    }
}