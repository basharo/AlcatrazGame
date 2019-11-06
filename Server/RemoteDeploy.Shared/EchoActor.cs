using System;
using Akka.Actor;
using Akka.Configuration;
using Akka.IO;

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
                System.IO.File.WriteAllText(@"c:\Temp\" + client.unique_name + ".txt", client.ipAddress + "---" + client.port + "---" + client.preferred_group_size + "---" + client.sequenceID + "---" + client.unique_name);
                Self.Tell(new Client(1, "333", 2, "localhost", 5240));
                using (var system = ActorSystem.Create("Deployer", ConfigurationFactory.ParseString(@"
                akka {  
                    actor{
                        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                        deployment {
                            /remoteecho {
                                remote = ""akka.tcp://Client@localhost:2500""
                            }
                        }
                    }
                    remote {
                        helios.tcp {
		                    port = 8090
		                    hostname = localhost
                        }
                    }
                    
                }")))
                {

                }
                Sender.Tell(client);
            });
        }
    }
}
