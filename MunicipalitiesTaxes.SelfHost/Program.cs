using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace MunicipalitiesTaxes.SelfHost
{
    class Program
    {
        public static void Main(string[] args)
        {
            Assembly.LoadFrom(Path.Combine(Environment.CurrentDirectory, "MunicipalitiesTaxes.dll"));
            Assembly.LoadFrom(Path.Combine(Environment.CurrentDirectory, "MunicipalitiesTaxes.Service.dll"));
            Assembly.LoadFrom(Path.Combine(Environment.CurrentDirectory, "MunicipalitiesTaxes.Contract.dll"));
            Assembly.LoadFrom(Path.Combine(Environment.CurrentDirectory, "MunicipalitiesTaxes.Repositories.dll"));
            using (WebApp.Start<Startup>("http://localhost:51286"))
            {
                Console.WriteLine("Web Server started");
                Console.ReadLine();
            }
        }
    }
}
