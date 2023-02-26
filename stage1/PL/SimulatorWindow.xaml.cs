using BlApi;
using simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{

    BackgroundWorker worker;

    IBl bl;

    TextBlock OrderTextBlock;

    // Prep stuff needed to remove close button on window.

    //------Prevent close button variables

    private const int GWL_STYLE = -16;

    private const int WS_SYSMENU = 0x80000;

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


    //-----Timer variables:

    private Stopwatch stopWatch;

    private bool isTimerRun;

    //-----ProgressBar variables

    Duration duration;

    DoubleAnimation doubleanimation;

    ProgressBar ProgressBar;


    public SimulatorWindow(IBl Bl)
    {
        InitializeComponent();
        Loaded += RemoveCloseButton;
        bl = Bl;
        workerStart();
        
    }

    void RemoveCloseButton(object sender, RoutedEventArgs e)
    {
        // Code to remove close box from window
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    void workerStart()
    {
        worker = new BackgroundWorker();
        worker.DoWork += WorkerDoWork;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        worker.ProgressChanged += workerProgressChanged;
        worker.RunWorkerCompleted += RunWorkerCompleted;
        TimerStart();
        worker.RunWorkerAsync();
    }

    void SimulationUpdated(BO.Order? order, int time)
    {
        if (order == null) return;
        try
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(SimulationUpdated, order, time);
            }
            else
            {
                Tuple<BO.Order, int, BO.eOrderStatus> ToUI = new(order, time, (BO.eOrderStatus)((int)order.Status + 1));

                DataContext = ToUI;
                ProgressBarStart(time);

            }
        }
        catch (Exception ex)
        {

        }
    }
    void TimerStart()
    {
        stopWatch = new Stopwatch();
        stopWatch.Restart();
        isTimerRun = true;
    }

    void ProgressBarStart(int time)
    {
        if(ProgressBar!=null)
        SBar.Items.Remove(ProgressBar);
        ProgressBar = new ProgressBar();
        ProgressBar.IsIndeterminate = false;
        ProgressBar.Orientation = Orientation.Horizontal;
        ProgressBar.Width = 500;
        ProgressBar.Height = 30;
        duration = new Duration(TimeSpan.FromSeconds(time*2));
        doubleanimation = new DoubleAnimation(200.0, duration);
        ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        SBar.Items.Add(ProgressBar);
    }

    void WorkerDoWork(object sender, DoWorkEventArgs e)
    {
        try
        {
            Simulator.RegInUpdateChanged(SimulationUpdated);
            Simulator.RegInStopSimulator(StopSimulator);
            Simulator.StartSimulator();
            while (!worker.CancellationPending)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        catch (Exception ex)
        {

        }
    }

    void workerProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        SimulatorTXTB.Text = timerText;
    }

    void ffworkerProgressChanged(object sender, ProgressChangedEventArgs e)
    {

    }

    private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
    {
        Simulator.StopSimulator();
    }

    private void StopSimulator()
    {
        if (worker.WorkerSupportsCancellation == true)
            // Cancel the asynchronous operation.
            worker.CancelAsync();
        if (isTimerRun)
        {
            stopWatch.Stop();
            isTimerRun = false;
        }
    }

    void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        this.Close();
    }
}