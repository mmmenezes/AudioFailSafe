using Topshelf;

namespace Audio_Fail_Safe
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(configurator =>
            {
                configurator.Service<RestartService>(s =>
                {

                    s.ConstructUsing(name => new RestartService());

                    s.WhenStarted((service, control) => service.Start(control));

                    s.WhenStopped((service, control) => service.Stop(control));
                });
                configurator.RunAsLocalSystem();
                configurator.SetDescription("Servico pra reiniciar audio");
                configurator.SetDisplayName("ResetAudio");
                configurator.SetServiceName("ResetAudio");
            });
        }
    }
}
