using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace SchoolTelegramBot.AppCore.Utils
{
    public class TelegramClientUtils
    {
        public static async Task SendMessageWithDeleting(ITelegramBotClient client, string message, int messageId, long chatId, InlineKeyboardMarkup keyboardMarkup = null)
        {
            try
            {
                await client.DeleteMessageAsync(chatId, messageId);
            }
            finally
            {
                if (keyboardMarkup != null)
                    await client.SendTextMessageAsync(chatId, message, replyMarkup: keyboardMarkup);
                else
                    await client.SendTextMessageAsync(chatId, message);
            }
        }
    }
}