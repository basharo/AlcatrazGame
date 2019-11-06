using System;
using Akka.Actor;

namespace RemoteDeploy.Shared
{
    /// <summary>
    /// Actor that just replies the message that it received earlier
    /// </summary>
    public class EchoActor : ReceiveActor
    {
        public EchoActor()
        {
            Receive<Hello>(hello =>
            {
                Console.WriteLine("[{0}]: {1}", Sender, hello.Message);
                Sender.Tell(hello);
            });

            Receive<Client>(client =>
            {
                Console.WriteLine("[{0}]: {1}", Sender, client.ipAddress + "---" + client.port + "---" + client.preferred_group_size + "---" + client.sequenceID + "---" + client.unique_name);
                Sender.Tell(client);
            });

        }
    }
}
