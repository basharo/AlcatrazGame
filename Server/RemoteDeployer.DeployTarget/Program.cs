using System;
using Akka.Actor;
using Akka.Configuration;

namespace RemoteDeployer.DeployTarget
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var system = ActorSystem.Create("Server", ConfigurationFactory.ParseString(@"
                akka {  
                    actor.provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                    remote {
                        helios.tcp {
		                    port = 8090
		                    hostname = localhost
                        }
                    }
                }")))
                {
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
            }
        }
    }
}
