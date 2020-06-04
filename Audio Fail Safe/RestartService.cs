using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
namespace Audio_Fail_Safe
{
    public class RestartService
    {
      
        public bool Start(HostControl hostControl)
        {

            Task.Run(() =>
            {
                int Contador = 0;
                
                while (true)
                {
                    
                    ServiceController service = new ServiceController("Windows Audio");
                    if (service.Status == ServiceControllerStatus.Stopped)
                    {
                        Contador++;
                            service.Start();
                            using (var tw = new StreamWriter(ConfigurationManager.AppSettings.Get("Caminho"), true))
                            {
                                tw.WriteLine("Serviço Reiniciado Data:" + DateTime.Now.ToString()+ " Contador: " + Contador);
                                tw.Close();
                            }                      
                    }
                    service.Dispose();
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                }
            });

            return true;

        }

        public bool Stop(HostControl hostControl)
        {         
            return true;
        }
    }
}
