using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dal.DO;

namespace DalApi
{
    public interface IorderItem : Icrud<OrderItem>
    {
        public OrderItem Read_item_by_product_order(int order_id, int product_id);
        public IEnumerable<OrderItem> all_order_items();
    }
}
