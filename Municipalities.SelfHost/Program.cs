using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Municipalities.SelfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:51286"))
            {
                Console.WriteLine("Web Server started");
                Console.ReadLine();
            }
        }
    }
}
