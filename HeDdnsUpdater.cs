using System;
using System.Diagnostics;
using System.ServiceProcess;
using HeDdnsUpdaterService.Properties;

namespace HeDdnsUpdaterService
{
    public partial class HeDdnsUpdater : ServiceBase
    {
        private UpdaterThread _updateThread;

        public HeDdnsUpdater()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the service starts. Instantiates the update thread and starts it.
        /// </summary>
        /// <param name="args">Arguments to the service. Not used.</param>
        protected override void OnStart(string[] args)
        {
            // Snag the configuration information for updating
            string key = Settings.Default.DdnsKey;
            string hostname = Settings.Default.Hostname;
            TimeSpan updateDelay = Settings.Default.UpdateDelay;

            Trace.TraceInformation("Starting HE DDNS Updater Service...");
            Trace.TraceInformation("Will update '{0}' every {1}", hostname, updateDelay);

            // Spin up a thread to do the updating
            _updateThread = new UpdaterThread
            {
                Hostname = hostname,
                Key = key,
                UpdateDelay = updateDelay
            };
            _updateThread.RunThread();
        }

        protected override void OnStop()
        {
            Trace.TraceInformation("Shutting down HE DDNS Updater Service...");

            // Clean up the timer thread
            _updateThread.Dispose();
        }
    }
}
