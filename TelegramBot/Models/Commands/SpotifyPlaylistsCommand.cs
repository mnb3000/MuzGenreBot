using System;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class SpotifyPlaylistsCommand : Command
    {
        private static readonly HttpClient client = new HttpClient();
        public static string text { get; set; } = "";
        public override string Name => "/playlist ";

        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            text = (message.Text).Replace("/playlist ", "");

            if (text.Contains(" "))
            {
                text.Replace(" ", "_");
            }

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, "What genre do you need?", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            
            
            string link = $"http://localhost:51181/muzgenrebot/sp&"+text;
            var data = await client.GetStringAsync(link); //как сделать поиск по слову (так же и с википедией)
            await botClient.SendTextMessageAsync(chatId, data, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}
