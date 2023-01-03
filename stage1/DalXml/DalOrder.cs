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
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> ProductsInOrder(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ReadByFilter(Func<Order, bool> f = null)
        {
            throw new NotImplementedException();
        }

        public Order ReadSingle(Func<Order, bool> f)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
