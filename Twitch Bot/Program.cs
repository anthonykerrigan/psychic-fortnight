using System; 
using System.Net.Sockets; 


namespace HelloWorld
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string ipAddress = "irc.chat.twitch.tv"; 
            int port = 6667; 
            string password = "oauth:";
            string botUsername = "The_Guild_Bot"; 

            var tcpClient = new TcpClient(); 
            await tcpClient.ConnectAsync(ipAddress, port); 


            var streamReader = new StreamReader(tcpClient.GetStream()); 
            var streamWriter = new StreamWriter(tcpClient.GetStream()) {NewLine = "\r\n", AutoFlush = true};
            // NewLine = "\r\n" automatically puts \r\n after every line which marks the end of a message
            // Autoflush = true will call streamWriter.Flush() after every write call
            // Means we don't have to write a streamWriter.FlushAsync() after every call 

            await streamWriter.WriteLineAsync($"PASS {password}"); 
            await streamWriter.WriteLineAsync($"NICK {botUsername}");
            await streamWriter.WriteLineAsync($"JOIN #Kabaneku");
            await streamWriter.WriteLineAsync($"PRIVMSG #Kabaneku :HAVE NO FEAR! I AM HERE!");

            while (true) 
            {
                string line = await streamReader.ReadLineAsync(); 
                System.Console.WriteLine(line);

                string[] split = line.Split(" "); 
                //PING :tmi.twitch.tv
                //Respond with PONG :tmi.twitch
                if (line.StartsWith("PING"))
                {
                    System.Console.WriteLine("PING");
                    await streamWriter.WriteLineAsync($"PONG {split[1]}");
                    System.Console.WriteLine("PONG");
                }
            }
        }
    }
}