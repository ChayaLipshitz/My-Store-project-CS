using Dal.DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal
{
    internal class DalOrder : Iorder
    {
        public int Create(Order order)
        {
            //XmlRootAttribute IDSRoot = new XmlRootAttribute();
            //IDSRoot.ElementName = "IDS";
            //IDSRoot.IsNullable = true;
            //StreamReader read = new("../../xml/ConfigData.xml");
            //XmlSerializer serID= new XmlSerializer(typeof(List<int>), IDSRoot);
            //List<int> IDSList=(List<int>)  serID.Deserialize(read);

            XElement? IDS =  XDocument.Load("../../xml/ConfigData.xml").Root;
            int orderId =Convert.ToInt32( IDS.Element("OrderId").Value);
            order.ID=orderId;
            orderId++;
            IDS.Element("OrderId").Value = orderId.ToString();
            IDS.Save("../../xml/ConfigData.xml");


            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            StreamReader sread = new StreamReader ("../../xml/Order.xml");
            XmlSerializer ser= new XmlSerializer(typeof(List<Order>), xRoot);
            List<Order> OrdersList = (List<Order>)ser.Deserialize(sread);
            sread.Close();
            OrdersList.Add(order);
            StreamWriter swrite = new("../../xml/Order.xml");
            ser.Serialize(swrite, OrdersList);
            swrite.Close();
            return order.ID;


        }

        public void Delete(int id)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            StreamReader sread = new StreamReader("../../xml/Order.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
            List<Order> OrdersList = (List<Order>)ser.Deserialize(sread);
            sread.Close();
            Order order = OrdersList.Where(o => o.ID == id).First();
            OrdersList.Remove(order);
            StreamWriter swrite = new("../../xml/Order.xml");
            ser.Serialize(swrite, OrdersList);
            swrite.Close();
        }

        public IEnumerable<OrderItem> ProductsInOrder(int ID)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItem";
            xRoot.IsNullable = true;
            StreamReader sread = new StreamReader("../../xml/OrderItem.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
            List<OrderItem> orderItemsList = (List<OrderItem>)ser.Deserialize(sread);
            sread.Close();
            return orderItemsList.Where(oi => oi.Order_ID == ID);
        }

        public IEnumerable<Order> ReadByFilter(Func<Order, bool> f = null)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            StreamReader sread = new StreamReader("../../xml/Order.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
            List<Order> OrdersList = (List<Order>)ser.Deserialize(sread);
            sread.Close();
            if (f == null)
                return OrdersList;
            return  OrdersList.Where(f);
        }

        public Order ReadSingle(Func<Order, bool> f)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            StreamReader sread = new StreamReader("../../xml/Order.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
            List<Order> OrdersList = (List<Order>)ser.Deserialize(sread);
            sread.Close();
            return OrdersList.Where(f).First();

        }

        public bool Update(Order obj)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            StreamReader sread = new StreamReader("../../xml/Order.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
            List<Order> OrdersList = (List<Order>)ser.Deserialize(sread);
            sread.Close();
            int index = OrdersList.FindIndex(o => o.ID==obj.ID);
            OrdersList[index]=obj;
            StreamWriter swrite= new StreamWriter("../../xml/Order.xml");
            ser.Serialize(swrite, OrdersList);
            swrite.Close();
            return true;

        }
    }
}
