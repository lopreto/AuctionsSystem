using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AuctionsSystemWorker
{
    public partial class AuctionsSystemWorker : ServiceBase
    {
        Timer tmr = new Timer();
        public Worker WorkerManager { get; set; }
        public AuctionsSystemWorker()
        {
            WorkerManager = new Worker();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            tmr.Interval = 1000;
            tmr.AutoReset = true;
            tmr.Enabled = true;
            tmr.Start();

            WorkerManager.Run();
        }

        protected override void OnStop()
        {
        }

        public void Run()
        {
            WorkerManager.Run();
        }
    }
}
