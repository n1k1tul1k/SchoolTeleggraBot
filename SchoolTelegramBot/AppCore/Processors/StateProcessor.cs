using System.Linq;
using SchoolTelegramBot.AppCore.DB;
using SchoolTelegramBot.AppCore.Models;

namespace SchoolTelegramBot.AppCore
{
    public static class StateProcessor
    {
        public static void AddNew(long chatId)
        {
            using (var ctx = new ApplicationContext())
            {
                ctx.States.Add(new StateModel {UserId = chatId, State = StateEnum.Newfag});
                ctx.SaveChanges();
            }
        }

        public static StateEnum GetState(long chatId)
        {
            StateEnum state = StateEnum.Newfag;
            using (var ctx = new ApplicationContext())
            {
                var users = ctx.States.Where(x => x.UserId == chatId);
                if (users.Count() != 0)
                {
                    return users.First().State;
                }
            }

            return state;
        }
        public static void UpdateState(long chatId, StateEnum state)
        {
            using (var ctx = new ApplicationContext())
            {
                var users = ctx.States.Where(x => x.UserId == chatId);
                if (users.Count() > 0)
                {
                    users.First().State = state;
                    ctx.SaveChanges();

                }
            }
        }
    }
}