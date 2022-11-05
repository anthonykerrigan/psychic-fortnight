using System; 
using System.IO; 
using System.Net.Sockets; 
using System.Threading.Tasks; 

namespace The_Guild_Bot
{

    public class bot_start
    {
        const string ip = "irc.chat.twitch.tv"; 
        const int port = 6667; 
        private string nick; 
        private string password;
        private string channelName; 
        public StreamReader streamReader; 
        public StreamWriter streamWriter; 

        private TaskCompletionSource<int> connected = new TaskCompletionSource<int>();

        public bot_start(string nick, string password, string channelName)
        {
            this.nick = nick;
            this.password = password;
            this.channelName = channelName; 
        }

        public async Task Start()
        {
            var tcpClient = new TcpClient(); 
            await tcpClient.ConnectAsync(ip, port); 
            streamReader = new StreamReader(tcpClient.GetStream()); 
            streamWriter = new StreamWriter(tcpClient.GetStream()) { NewLine = "\r\n", AutoFlush = true};
            
            await streamWriter.WriteLineAsync($"PASS {password}");
            await streamWriter.WriteLineAsync($"NICK {nick}");
            await streamWriter.WriteLineAsync($"JOIN #{channelName}"); 
            await streamWriter.WriteLineAsync($"PRIVMSG #{channelName} :HAVE NO FEAR! I AM HERE!");
            System.Console.WriteLine("Check Chat for All Might!");
            connected.SetResult(0);
            //
            //

            while (true)
            {
                string line = await streamReader.ReadLineAsync();
                System.Console.WriteLine(line);
                
                string[] split = line.Split(" ");
                //PING :tmi.twitch.tv
                //Respond with PONG :tmi.twitch.tv
                if (line.StartsWith("PING"))
                {
                    await streamWriter.WriteLineAsync($"PONG {split[1]}");
                    System.Console.WriteLine($"PONG {split[1]}");
                    //await streamWriter.WriteLineAsync($"PRIVMSG #Kabaneku :I'm still there. Kab make sure you remove this line after testing"); 
                }

                if (split.Length > 1 && split[1] == "PRIVMSG")
                {
                    //:mytwitchchannel!mytwitchchannel@mytwitchchannel.tmi.twitch.tv 
                    // ^^^^^^^^
                    //Grab this name here
                    int exclamationPointPosition = split[0].IndexOf("!");
                    string username = split[0].Substring(1, exclamationPointPosition - 1);
                    //Skip the first character, the first colon, then find the next colon
                    int secondColonPosition = line.IndexOf(':', 1);//the 1 here is what skips the first character
                    string message = line.Substring(secondColonPosition + 1);//Everything past the second colon
                    
                }
                
                
            }
        }
    }
}