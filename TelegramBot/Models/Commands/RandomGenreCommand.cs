using System;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class RandomGenreCommand : Command
    {
        private static readonly HttpClient client = new HttpClient();
        public override string Name => @"/randomGenre";

        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;

            var data = await client.GetStringAsync("http://localhost:51181/muzgenrebot/randomgenre");
            await botClient.SendTextMessageAsync(chatId, data, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}
