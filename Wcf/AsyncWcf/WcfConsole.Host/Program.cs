using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfConsoleMessages.Contracts;
using WcfConsoleMessages.Services;

namespace WcfConsole.Host
{
    class Program
    {
        /// <summary>
        /// NOT: YOU HAVE TO RUN THIS APP AS ADMINISTARTOR
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            Uri httpUrl = new Uri("http://localhost:8090/HelloFoo.asmx");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(FooWebService), httpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IFooService), new BasicHttpBinding(), "");

            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);
            host.Description.Behaviors.Add(new TrackInterceptor("jin-provider"));

            //Start the Service
            host.Open(); // IF YOU'RE GETTING ERROR AT HERE, TRY TO RUN APP AS ADMINISTRATOR

            Console.WriteLine($"Service is host at {DateTime.Now.ToString()} - {httpUrl}");
            Console.WriteLine("Host is running... Press  key to stop");
            Console.ReadLine();
        }
    }
}
