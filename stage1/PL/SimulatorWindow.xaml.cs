using BlApi;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        IBl bl;

        BackgroundWorker worker;
        // Prep stuff needed to remove close button on window.
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private Stopwatch stopWatch;
        private bool isTimerRun;
        BackgroundWorker timerworker;
        Duration duration;
        DoubleAnimation doubleanimation;
        ProgressBar ProgressBar;

        
        public SimulatorWindow(IBl Bl)
        {
            InitializeComponent();
            bl = Bl;
            Loaded += ToolWindow_Loaded;
            workerStart();
            TimerStart();
            ProgressBarStart();
        }
        void ProgressBarStart()
        {
            ProgressBar = new ProgressBar();
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Orientation = Orientation.Horizontal;
            ProgressBar.Width = 500;
            ProgressBar.Height = 30;
            duration = new Duration(TimeSpan.FromSeconds(20));
            doubleanimation = new DoubleAnimation(200.0, duration);
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            SBar.Items.Add(ProgressBar);
        }

        void TimerStart()
        {
            stopWatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += TimerDoWork;
            timerworker.ProgressChanged += TimerProgressChanged;
            timerworker.WorkerReportsProgress = true;
            Simulator.Simulator.StartSimulator();
            stopWatch.Restart();
            isTimerRun = true;
            timerworker.RunWorkerAsync();
        }
        void workerStart()
        {
            worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.ProgressChanged += workerProgressChanged;
            worker.RunWorkerCompleted += RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
        void TimerDoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        void TimerProgressChanged(object sender, ProgressChangedEventArgs e) {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            SimulatorTXTB.Text = timerText;
        }
        void workerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
                // Cancel the asynchronous operation.
                worker.CancelAsync();
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
            this.Close();
        }

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.StopSimulator();
            this.Close();
        }
    }
}
