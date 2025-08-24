using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Botyara
{
    
    internal class Program
    {

        public static async Task Main()
        {
            var bot = new TelegramBotClient("8307985542:AAFJCGpnGzt1jO7gADeNHRQe-QaUcyQoO2g");
            var me = await bot.GetMe();
           
            bot.StartReceiving(Update, Error);
            Console.ReadLine();
            
        }




        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token) 
        {
            
            var message = update.Message;
            var ID = message.Chat.Id;
            if (message.Text != null) {
                if (message.Text.ToLower().Contains("хай"))
                {
                    await client.SendMessage(ID, "бибки");
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
                await client.SendPhoto(ID, "https://drive.google.com/file/d/11tDZCwURJeOeAarhfvgEj5nfIaRkZQ3D/view?usp=drive_link"); 
                return;
            }
            if (message.Sticker != null) 
            {
                await client.SendSticker(ID, message.Sticker, message.Id);
            }

        }
       



        private static async Task Error(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
        {
          
            throw new NotImplementedException();
        }
    
    
    
    }
}
