using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace HeDdnsUpdaterService
{
    class UpdaterThread : IDisposable
    {
        #region Properties

        /// <summary>
        /// The hostname to update
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// The authentication key to update the record with
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The amount of time to delay between calls to update the record
        /// </summary>
        public TimeSpan UpdateDelay { get; set; }

        /// <summary>
        /// The timer that will fire every so often to update the record
        /// </summary>
        private Timer InternalTimer { get; set; }

        #endregion

        /// <summary>
        /// Creates a new timer object to fire immediately and after the delay timespan that will
        /// update the DDNS record specified.
        /// </summary>
        public void RunThread()
        {
            // Set up the timer and let it go
            InternalTimer = new Timer(TimerCallback, null, TimeSpan.Zero, UpdateDelay);
        }

        /// <summary>
        /// Callback for when the timer fires. Makes a GET request to HE's servers to update the
        /// DDNS record with the information provided. The act of making the HTTP request sends
        /// the current IP address of the hostname to be updated.
        /// </summary>
        /// <param name="state">Not used. Only included to meet TimerCallback delegate definition</param>
        private void TimerCallback(object state)
        {
            // Build the URL that will be called to update the DDNS record
            string url = String.Format("http://{0}:{1}@dyn.dns.he.net/nic/update?hostname={0}", Hostname, Key);
            
            // Call the URL and see what we get back
            WebRequest updateRequest = WebRequest.Create(url);
            Stream responseStream = updateRequest.GetRequestStream();
            StreamReader responseReader = new StreamReader(responseStream);
            string response = responseReader.ReadToEnd();

            // Do some validation on the response we get back
            if (response.StartsWith("good", StringComparison.OrdinalIgnoreCase) ||
                response.StartsWith("nochg", StringComparison.OrdinalIgnoreCase))
            {
                Trace.TraceInformation("Successful update: {0}", response);
            }
            else
            {
                Trace.TraceError("Unsuccessful update: {0}", response);
            }
        }

        /// <summary>
        /// Disposes of the only disposable item in this class.
        /// </summary>
        public void Dispose()
        {
            InternalTimer.Dispose();
        }

    }
}
