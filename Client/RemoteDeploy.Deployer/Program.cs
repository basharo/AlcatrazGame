using System;
using Akka.Actor;
using Akka.Configuration;
using RemoteDeploy.Shared;
using Akka.IO;
using System.Net;

namespace RemoteDeploy.Deployer
{
    //class Program
    //{
    //    class SayHello { }

    //    class HelloActor : ReceiveActor
        //{
        //    private IActorRef _remoteActor;
        //    private int _helloCounter;
        //    private ICancelable _helloTask;

        //    public HelloActor(IActorRef remoteActor)
        //    {
        //        _remoteActor = remoteActor;
        //        Context.Watch(_remoteActor);
        //        Receive<Hello>(hello =>
        //        {
        //            Console.WriteLine("Received {1} from {0}", Sender, hello.Message);
        //        });

        //        Receive<SayHello>(sayHello =>
        //        {
        //            _remoteActor.Tell(new Hello("hello"+_helloCounter++));
        //        });

        //        Receive<Terminated>(terminated =>
        //        {
        //            Console.WriteLine(terminated.ActorRef);
        //            Console.WriteLine("Was address terminated? {0}", terminated.AddressTerminated);
        //            _helloTask.Cancel();
        //        });
        //    }

        //    protected override void PreStart()
        //    {
        //        _helloTask = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
        //            TimeSpan.FromSeconds(1), Context.Self, new SayHello(), ActorRefs.NoSender);
        //    }

        //    protected override void PostStop()
        //    {
        //        _helloTask.Cancel();
        //    }
        //}

        internal class Program
        {
            private static void Main(string[] args)
            {
                var config = ConfigurationFactory.ParseString(@"
akka {  
    log-config-on-start = on
    stdout-loglevel = DEBUG
    loglevel = DEBUG
    actor {
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
        
        debug {  
          receive = on 
          autoreceive = on
          lifecycle = on
          event-stream = on
          unhandled = on
        }
    }
    remote {
        dot-netty.tcp {
		    port = 8080
		    hostname = localhost
        }
    }
}
");
                //testing connectivity
                using (ActorSystem.Create("system2", config))
                {
                    Console.ReadLine();
                }
            }
        }
    }



    //static void Main(string[] args)
    //    {

    //var system = ActorSystem.Create("example");
    //var manager = system.Tcp();
    //manager.Tell("test");
    //
    //var endpoint = new DnsEndPoint("localhost", 52222);
    //manager. System.Tcp().Tell(new Tcp.Connect(endpoint));

    //            TelnetClientxx client = new TelnetClientxx("localhost", 5222);

    //using (var system = ActorSystem.Create("Deployer", ConfigurationFactory.ParseString(@"
    //    akka {  
    //        actor{
    //            provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
    //            deployment {
    //                /remoteecho {
    //                    remote = ""akka.tcp://Client@localhost:8090""
    //                }
    //            }
    //        }
    //        remote {
    //            helios.tcp {
    //          port = 2500
    //          hostname = localhost
    //            }
    //        }

    //    }")))
    //{
    //    var remoteAddress = Address.Parse("akka.tcp://DeployTarget@localhost:8090");
    //    var remoteEcho1 = system.ActorOf(Props.Create(() => new EchoActor()), "remoteecho"); //deploy remotely via config
    //    //var remoteEcho2 =
    //    //    system.ActorOf(   
    //    //        Props.Create(() => new EchoActor())
    //    //            .WithDeploy(Deploy.None.WithScope(new RemoteScope(remoteAddress))), "coderemoteecho"); //deploy remotely via code

    //    //system.ActorOf(Props.Create(() => new HelloActor(remoteEcho1)));
    //    //system.ActorOf(Props.Create(() => new HelloActor(remoteEcho2)));

    //    //system.ActorSelection("/user/remoteecho").Tell(new Hello("hi from selection!"));
    //    system.ActorSelection("/user/remoteecho").Tell(new Client(1,"333",2,"localhost",5240));
    //          Console.ReadKey();
//}
        

