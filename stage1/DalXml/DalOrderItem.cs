using Dal.DO;
using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class DalOrderItem : IorderItem
{
    public int getIDAndUpdate()
    {
        XmlRootAttribute IDSRoot = new XmlRootAttribute();
        IDSRoot.ElementName = "IDS";
        IDSRoot.IsNullable = true;
        StreamReader read = new("../../xml/ConfigData.xml");
        XmlSerializer serID = new XmlSerializer(typeof(IDSConfig), IDSRoot);
        IDSConfig allIDS = ((IDSConfig)serID.Deserialize(read));
        int orderID = allIDS.OrderItemId;
        allIDS.OrderItemId++;
        read.Close();
        StreamWriter write = new("../../xml/ConfigData.xml");
        serID.Serialize(write, allIDS);
        write.Close();
        return orderID;
    }


    public int Create(OrderItem orderItem)
    {

        orderItem.OrderItem_ID= getIDAndUpdate();   
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
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader sread = new StreamReader("../../xml/OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        List<OrderItem> OrderItemsList = (List<OrderItem>)ser.Deserialize(sread);
        sread.Close();
        OrderItem order = OrderItemsList.Where(o => o.OrderItem_ID == id).First();
        OrderItemsList.Remove(order);
        StreamWriter swrite = new("../../xml/OrderItem.xml");
        ser.Serialize(swrite, OrderItemsList);
        swrite.Close();
    }

    public IEnumerable<OrderItem> ReadByFilter(Func<OrderItem, bool> f = null)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader sread = new StreamReader("../../xml/OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        List<OrderItem> OrderItemsList = (List<OrderItem>)ser.Deserialize(sread);
        sread.Close();
        return f==null?OrderItemsList:OrderItemsList.Where(f);
    }

    public OrderItem ReadSingle(Func<OrderItem, bool> f)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader sread = new StreamReader("../../xml/OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        List<OrderItem> OrderItemsList = (List<OrderItem>)ser.Deserialize(sread);
        sread.Close();
        return OrderItemsList.Where(f).First();
    }

    public OrderItem Read_item_by_product_order(int order_id, int product_id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader sread = new StreamReader("../../xml/OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        List<OrderItem> OrderItemsList = (List<OrderItem>)ser.Deserialize(sread);
        sread.Close();
        return OrderItemsList.Where(oi=> oi.Order_ID==order_id && oi.Product_ID== product_id).First();
    }

    public bool Update(OrderItem orderItem)
    {
        

        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;
        StreamReader sread = new StreamReader("../../xml/OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        List<OrderItem> orderItemsList = (List<OrderItem>)ser.Deserialize(sread);
        sread.Close();
        int index = orderItemsList.FindIndex(oi => orderItem.OrderItem_ID == oi.OrderItem_ID);
        if(index == -1 ) throw new NotExistExceptions();
        orderItemsList[index]=orderItem;
        StreamWriter swrite = new StreamWriter("../../xml/OrderItem.xml");
        ser.Serialize(swrite, orderItemsList);
        swrite.Close();
        return true;
    }
}

