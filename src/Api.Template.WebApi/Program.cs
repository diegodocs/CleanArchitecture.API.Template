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

                var isWindowService = (!Debugger.IsAttached || args.Contains("--windowsservice"));
                var pathToContentRoot = Directory.GetCurrentDirectory();
                
                if (isWindowService)
                {
                    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                    pathToContentRoot = Path.GetDirectoryName(pathToExe);

                    var host = WebHost.CreateDefaultBuilder(args)
                        .UseKestrel()
                        .UseConfiguration(config)
                        .ConfigureServices(s => s.AddAutofac())
                        .UseContentRoot(pathToContentRoot)
                        .UseStartup<Startup>()
                        .UseNLog()
                        .Build();

                    host.RunAsService();
                }
                else
                {
                    var host = WebHost.CreateDefaultBuilder(args)
                        .UseConfiguration(config)
                        .ConfigureServices(s => s.AddAutofac())
                        .UseContentRoot(pathToContentRoot)
                        .UseStartup<Startup>()
                        .UseNLog()
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