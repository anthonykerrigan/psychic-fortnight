using System; 
using System.IO; 
using System.Net.Sockets; 
using System.Threading.Tasks; 
using System.Configuration; 
using System.Collections.Specialized; 
namespace The_Guild_Bot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string password = "oauth:";
            string botUsername = "the_guild_bot";
            string channelName = "kabaneku"; 

            var bot_start = new bot_start(botUsername, password, channelName);
            await bot_start.Start();
            await Task.Delay(-1);
            Console.WriteLine("This is never run");    
        }
    }
}