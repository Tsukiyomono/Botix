using Microsoft.Extensions.Logging;

namespace Botix
{

    internal class Program
    {

        public static void Main()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Information);
            });
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var botToken = configuration["BotConfiguration:BotToken"];
            var bot = new TelegramBotClient(botToken);
            bot.StartReceiving(Update, Error);
            //var builder = WebApp
            Console.ReadLine();

        }

        


        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var picture = configuration["Pictures:Popuga"];
            var incomeMsg = configuration["Messages:IncomeMsg"];
            var outMsg = configuration["Messages:OutMsg"];
            var message = update.Message;
            var ID = message.Chat.Id;
            if (message.Text != null)
            {
                if (message.Text.ToLower().Contains(incomeMsg))
                {
                    await client.SendMessage(ID, outMsg);
                    return;
                }
                else
                {
                    await client.SendMessage(ID, message.Text);
                    Console.Write(message.Text + "   :   ");
                    Console.WriteLine(message.Chat.Username);
                }

            }
            if (message.Photo != null)
            {
                await client.SendPhoto(ID, picture);
                Console.WriteLine("Picture send   :   " + message.Chat.Username);
            }
            if (message.Sticker != null)
            {
                await client.SendSticker(ID, message.Sticker, message.Id);
                Console.WriteLine("Sticker send   :   " + message.Chat.Username);
            }

        }




        private static async Task Error(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
        {

            throw new NotImplementedException();
        }



    }
}