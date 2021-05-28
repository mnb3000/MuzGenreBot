using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class WikiInfoCommand : Command
    {
        private static readonly HttpClient client = new HttpClient();

        //public static Regex regex = new Regex(@"/find(\s\S+)+");
        
        

        public override string Name => "/find ";

        public static string text { get; set; } = "";
        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return false;

            text = (message.Text).Replace("/find ", "");

            if (text.Contains(" "))
            {
                text.Replace(" ", "_");
            }

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            
            string link = $"http://localhost:51181/muzgenrebot/f&"+text;
            Console.WriteLine(link);
            var data = await client.GetStringAsync(link); 
            await botClient.SendTextMessageAsync(chatId, data, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}
