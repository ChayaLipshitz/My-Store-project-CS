using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public static class Simulator
{
    static Thread myThread { get; set; }
    static Stopwatch myStopWatch { get; set; }

    static event EventHandler  stop;
    static event EventHandler  propsChanged;
    public static void StartSimulator()
    {
        myThread = new Thread(Simulation);
        myThread.Start();
       // myThread.
    }

    public static  void StopSimulator()
    {
        myThread.Abort();
        
    }

    private static void Simulation()
    {

    }
}
