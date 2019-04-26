using Autofac.Extensions.DependencyInjection;
using Api.Template.WebApi.Server;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NLog;
using NLog.Web;

namespace Api.Template.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                var builder = new ConfigurationBuilder();
                builder.AddCommandLine(args);

                var config = builder.Build();

                var isService = !(Debugger.IsAttached || args.Contains("--console"));
                var pathToContentRoot = Directory.GetCurrentDirectory();

                var webHostArgs = args.Where(arg => arg != "--console").ToArray();

                if (isService)
                {
                    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                    pathToContentRoot = Path.GetDirectoryName(pathToExe);

                    var host = WebHost.CreateDefaultBuilder(webHostArgs)
                        .UseKestrel()
                        .UseConfiguration(config)
                        .ConfigureServices(s => s.AddAutofac())
                        .UseContentRoot(pathToContentRoot)
                        .UseStartup<Startup>()
                        .Build();

                    host.RunAsService();
                }
                else
                {
                    var host = WebHost.CreateDefaultBuilder(webHostArgs)
                        .UseConfiguration(config)
                        .ConfigureServices(s => s.AddAutofac())
                        .UseContentRoot(pathToContentRoot)
                        .UseStartup<Startup>()
                        .Build();

                    host.Run();
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}