using System; 
using System.IO; 
using System.Net.Sockets; 
using System.Threading.Tasks; 
namespace The_Guild_Bot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string password = "oauth:";
            string botUsername = "The_Guild_Bot";

            var bot_start = new bot_start(botUsername, password);
            bot_start.Start();
            await Task.Delay(-1);
            Console.WriteLine("This is never run");    
        }
    }
}