using BlApi;
using System.Diagnostics;
namespace Simulator;
public static class Simulator
{
    static IBl bl;
    static int processTime;
    static bool continueThread;
    static event Action InStopSimulator;
    static event Action<BO.Order, int> InUpdateChanged;

    static public BO.Order Order { get => order; }

    static BO.Order? order;

    static Random random = new Random();
    static Thread myThread { get; set; }
    public static void StartSimulator()
    {
        bl = Factory.Get() ?? null;
        continueThread = true;
        myThread = new Thread(Simulation);
        myThread.Start();
    }

    public static void StopSimulator()
    {
        if (InStopSimulator != null)
        {
            InStopSimulator();
        }
        continueThread = false;
    }

    private static void Simulation()
    {
        try
        {
            while (continueThread != false)
            {
                order = bl.iOrder.TheNextOrderToCareFor();
                int u = 5;
                if (order == null)
                {
                    continueThread = false;
                    break;
                }
                processTime = random.Next(1, 7);
                CareOrder();
            }
            StopSimulator();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void CareOrder()
    {
        if (InUpdateChanged != null)
        {
            InUpdateChanged(order, processTime);
        }
        Thread.Sleep((int)processTime * 1000);
        if (order.Ship_Date == DateTime.MinValue)
        {
            order = bl.iOrder.UpdateOrderShipped(order.OrderID);
        }
        else if (order.Delivery_Date == DateTime.MinValue)
        {
            order = bl.iOrder.UpdateOrderDelivered(order.OrderID);
        }
        try
        {
            bl.iOrder.Update(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void RegInStopSimulator(Action func)
    {
        InStopSimulator += func;
    }

    public static void RegInUpdateChanged(Action<BO.Order, int> func)
    {
        InUpdateChanged += func;
    }

}
