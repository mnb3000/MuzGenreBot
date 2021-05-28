using TelegramBot.Models;
using TelegramBot.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Models
{
    public class Bot
    {
        private static TelegramBotClient bot;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task Initialize(CancellationTokenSource cts)
        {
            bot = new TelegramBotClient(Configuration.BotToken);

            var me = await bot.GetMeAsync();
            Console.Title = me.Username;

            // Send cancellation request to stop bot
            commandsList = new List<Command>();
            commandsList.Add(new RandomGenreCommand());
            commandsList.Add(new StartCommand());
            commandsList.Add(new RandomStoryCommand());
            commandsList.Add(new SpotifyPlaylistsCommand());
            commandsList.Add(new WikiInfoCommand());
            //TODO: Add more commands
            
            bot.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsynс, HandleErrorAsync),
                cts.Token
            );
            
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            
            cts.Cancel();
        }
        public static async Task HandleUpdateAsynс(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
            var commands = Commands;

            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update.Message),
                UpdateType.EditedMessage => BotOnMessageReceived(update.Message),
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        private static async Task BotOnMessageReceived(Message message)
        {
            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;
            
            var commands = Commands;
            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, bot);
                    break;
                }
            }

        }
        
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
        }
    }
    
    
}