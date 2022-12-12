
namespace BO;
public class OrderTracking
{
    public int ID { get; set; }
    public eOrderStatus? Status { get; set; }
    public List<(DateTime?, eOrderStatus?)>? dateAndStatus { get; set; } = new();
    public override string ToString()
    {
        string dateStatus = "";
        foreach((DateTime, eOrderStatus) i in dateAndStatus)
        {
            dateStatus += $"date: {i.Item1}, status: {i.Item2}\n";
        }
        return $"ID: {ID}\n" +
            $"Status: {Status}\n" +
            $"dateAndStatus history: {dateStatus}\n";
    }
}

