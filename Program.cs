using System.ServiceProcess;

namespace HeDdnsUpdaterService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] servicesToRun = { new HeDdnsUpdater() };
            ServiceBase.Run(servicesToRun);
        }
    }
}
