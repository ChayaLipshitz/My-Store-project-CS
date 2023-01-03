using Dal.DO;
using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class DalOrderItem : IorderItem
{
    public int Create(OrderItem orderItem)
    {
        XElement? IDS = XDocument.Load("../../xml/ConfigData.xml").Root;
        int OrderItemId = Convert.ToInt32(IDS.Element("OrderItemId").Value);
        orderItem.OrderItem_ID = OrderItemId;
        OrderItemId++;
        IDS.Element("OrderItemId").Value = OrderItemId.ToString();
        IDS.Save("../../xml/ConfigData.xml");


        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader sread = new StreamReader("../../xml/OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        List<OrderItem> orderItemsList = (List<OrderItem>)ser.Deserialize(sread);
        sread.Close();
        orderItemsList.Add(orderItem);
        StreamWriter swrite = new StreamWriter("../../xml/OrderItem.xml"); 
        ser.Serialize(swrite, orderItemsList);
        swrite.Close();
        return orderItem.OrderItem_ID;

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem> ReadByFilter(Func<OrderItem, bool> f = null)
    {
        throw new NotImplementedException();
    }

    public OrderItem ReadSingle(Func<OrderItem, bool> f)
    {
        throw new NotImplementedException();
    }

    public OrderItem Read_item_by_product_order(int order_id, int product_id)
    {
        throw new NotImplementedException();
    }

    public bool Update(OrderItem obj)
    {
        throw new NotImplementedException();
    }
}

