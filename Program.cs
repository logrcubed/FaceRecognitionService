using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Net.Mail;
using System.ServiceModel.Description;

namespace FaceRecogService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1");
            string baseAddress = "http://192.168.1.6:8000/Service";
            WebHttpBinding wb = new WebHttpBinding();
            wb.MaxBufferSize = 4194304;
            wb.MaxReceivedMessageSize = 4194304;
            wb.MaxBufferPoolSize = 4194304;

            ServiceHost host = new ServiceHost(typeof(ImageUploadService), new Uri(baseAddress));
            host.AddServiceEndpoint(typeof(IImageUpload), wb, "").Behaviors.Add(new WebHttpBehavior());
            //host.AddServiceEndpoint(typeof(IImageUpload), new WebHttpBinding(), "").Behaviors.Add(new WebHttpBehavior());
            host.Open();
            Console.WriteLine("Host opened"); 
            Console.ReadKey(true);
        }
    }
}
