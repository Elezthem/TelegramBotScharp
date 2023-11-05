using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    private static TelegramBotClient botClient;

    static async Task Main(string[] args)
    {
        botClient = new TelegramBotClient("YOUR_BOT_TOKEN");

        botClient.OnMessage += Bot_OnMessage;
        botClient.OnCallbackQuery += Bot_OnCallbackQuery;

        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Hello! I am {me.Username}");

        botClient.StartReceiving();

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();

        botClient.StopReceiving();
    }

    private static async void Bot_OnMessage(object sender, MessageEventArgs e)
    {
        if (e.Message.Text != null && e.Message.Text.Contains("/start"))
        {
            await SendWelcomeMessage(e.Message.Chat.Id);
        }
    }

    private static async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
    {
        var chatId = e.CallbackQuery.Message.Chat.Id;

        if (e.CallbackQuery.Data == "button1")
        {
            await botClient.SendTextMessageAsync(chatId, "You have selected Button 1");
        }
        else if (e.CallbackQuery.Data == "button2")
        {
            await botClient.SendTextMessageAsync(chatId, "You have selected Button 2");
        }
    }

    private static async Task SendWelcomeMessage(long chatId)
    {
        var keyboard = new ReplyKeyboardMarkup
        {
            Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton("Button 1"),
                    new KeyboardButton("Button 2")
                }
            }
        };

        await botClient.SendTextMessageAsync(
            chatId,
            "Hello! I am your bot. Choose an option:",
            replyMarkup: keyboard
        );
    }
}
