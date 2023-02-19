using BlApi;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        // Prep stuff needed to remove close button on window
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        public SimulatorWindow(IBl Bl)
        {
            InitializeComponent();
            bl = Bl;
            Loaded += ToolWindow_Loaded;
            worker = new BackgroundWorker();
            worker.DoWork += DoWork;
            worker.WorkerReportsProgress = true; 
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerCompleted += RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
        void DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.StartSimulator();
            while (!worker.CancellationPending)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        void Report_Progress(int num) { num = 3; }
        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           //e.ProgressPercentage
           //e.UserState
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
            this.Close();
        }

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
