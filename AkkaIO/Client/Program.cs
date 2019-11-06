using Akka.Actor;
using Akka.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var system = ActorSystem.Create("alcatraz");
            var manager = system.Tcp();

            try
            {
                system.ActorOf(Props.Create(() => new TelnetClient(manager, "127.0.0.1", 9000)), "client");

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            while (true)
            {
                string message = Console.ReadLine();
                Console.WriteLine(message);
            }
            
        }
    }
}
