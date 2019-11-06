using Akka.Actor;
using Akka.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            var system = ActorSystem.Create("alcatraz");
            system.ActorOf(Props.Create(() => new EchoServer(9000)), "server");

            Console.ReadLine();
        }
    }
}
