using Dal.DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal
{
    internal class DalOrder : Iorder
    {
        //  getting the id from the xml and updating it
        public int getIDAndUpdateXml()
        {
            XmlRootAttribute IDSRoot = new XmlRootAttribute();
            IDSRoot.ElementName = "IDS";
            IDSRoot.IsNullable = true;
            StreamReader read = new("../../xml/ConfigData.xml");
            XmlSerializer serID = new XmlSerializer(typeof(IDSConfig), IDSRoot);
            IDSConfig allIDS = ((IDSConfig)serID.Deserialize(read));
            int orderID = allIDS.OrderId;
            allIDS.OrderId++;
            read.Close();
            StreamWriter write = new("../../xml/ConfigData.xml");
            serID.Serialize(write, allIDS);
            write.Close();
            return orderID;
        }
        /// <summary>
        /// creatnig a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public int Create(Order order)
        {

            order.ID = getIDAndUpdateXml();
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
        /// <summary>
        /// Deleting a certain order
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]

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
        /// <summary>
        /// reading the orders list by an optional given filter...
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<OrderItem> ProductsInOrder(int ID)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;
            StreamReader sread = new StreamReader("../../xml/OrderItem.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
            List<OrderItem> orderItemsList = (List<OrderItem>)ser.Deserialize(sread);
            sread.Close();
            return orderItemsList.Where(oi => oi.Order_ID == ID);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

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
        /// <summary>
        /// reading a certain order by a given condition
        /// </summary>
        /// <param name="f">filter function</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public Order ReadSingle(Func<Order, bool> f)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;
            StreamReader sread = new StreamReader("../../xml/Order.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
            List<Order> OrdersList = (List<Order>)ser.Deserialize(sread);
            sread.Close();
            try
            {
              Order  order = OrdersList.Where(f).First();
                return order;
            }
            catch (Exception ex)
            {
                throw new NotExistExceptions();
            }
           

        }
        /// <summary>
        /// updating a certain orderItem
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        /// <exception cref="NotExistExceptions"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]

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
