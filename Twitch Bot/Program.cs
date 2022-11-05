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
        static string botPass = ConfigurationManager.AppSettings.Get("botPass");
        static string channelName = ConfigurationManager.AppSettings.Get("channelName");
        static string botUsername = ConfigurationManager.AppSettings.Get("botUsername");
        static async Task Main(string[] args)
        {
            var bot_start = new bot_start(botUsername, botPass, channelName);
            await bot_start.Start();
            await Task.Delay(-1);
            Console.WriteLine("This is never run");    
        }
    }
}