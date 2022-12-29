using Dal.DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class DalOrder : Iorder
    {
        public int Create(Order order)
        {
            XElement? root = XDocument.Load(@"..\..\xml\Order.xml").Root;
            XElement o = new("Order",
           new XElement("ID", 3),
           new XElement("CustomerName", order.Customer_Name),
           new XElement("CustomerEmail", order.Customer_Email),
           new XElement("CustomerAddress", order.Customer_Address),
           new XElement("OrderDate", order.Order_Date),
           new XElement("ShipDate", order.Ship_Date),
           new XElement("DeliveryDate", order.Delivery_Date));
            root?.Add(o);
            root?.Save("../../xml/Order.xml");
            return 3;
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
