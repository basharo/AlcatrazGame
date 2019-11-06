using Akka.Actor;
using Akka.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class TelnetClient : UntypedActor
    {
        public TelnetClient(IActorRef manager, string host, int port)
        {
            try
            {

                var serverEndpoint = new IPEndPoint(IPAddress.Parse(host), port);
               
                manager.Tell(new Tcp.Connect(serverEndpoint));
            }
            catch(ActorInitializationException ie)
            {
                Console.Write(ie.Message);
            }
        }

        protected override void OnReceive(object message)
        {
            if (message is Tcp.Connected)
            {
                var connected = message as Tcp.Connected;
                Console.WriteLine("Connected to {0}", connected.RemoteAddress);

                // Register self as connection handler
                Sender.Tell(new Tcp.Register(Self));
                ReadConsoleAsync();
                Become(Connected(Sender));
            }
            else if (message is Tcp.CommandFailed)
            {
                Console.WriteLine("Connection failed");
            }
            else Unhandled(message);
        }

        private UntypedReceive Connected(IActorRef connection)
        {
            return message =>
            {
                if (message is Tcp.Received)  // data received from network
                {
                    var received = message as Tcp.Received;
                    Console.WriteLine(Encoding.ASCII.GetString(received.Data.ToArray()));
                }
                else if (message is string)   // data received from console
                {
                    connection.Tell(Tcp.Write.Create(ByteString.FromString((string)message + "\n")));
                    ReadConsoleAsync();
                }
                else if (message is Tcp.PeerClosed)
                {
                    Console.WriteLine("Connection closed");
                }
                else Unhandled(message);
            };
        }

        private void ReadConsoleAsync()
        {
            Task.Factory.StartNew(self => Console.In.ReadLineAsync().PipeTo((ICanTell)self), Self);
        }
    }
}
