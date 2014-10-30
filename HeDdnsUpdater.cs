using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using HeDdnsUpdaterService.Properties;

namespace HeDdnsUpdaterService
{
    public partial class HeDdnsUpdater : ServiceBase
    {
        public HeDdnsUpdater()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Snag the configuration information for updating
            string key = Settings.Default.DdnsKey;
            string hostname = Settings.Default.Hostname;
            TimeSpan updateDelay = Settings.Default.UpdateDelay;

            Trace.TraceInformation("Starting HE DDNS Updater Service...");
            Trace.TraceInformation("Will update '{0}' every {1}", hostname, updateDelay);

            // Spin up a thread to do the updating

        }

        protected override void OnStop()
        {
        }
    }
}
