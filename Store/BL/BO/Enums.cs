using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public enum eCategory
{
    FRUITS,
    SNACKS,
    FROZEN
}

public enum eOrderStatus
{

   ORDERED,
   SHIPPED,
   DELIVERED
}
public enum OUT_CHOICE
{
    EXIT,
    PRODUCT,
    ORDER,
    CART
}
public enum INNER_CHOICE
{
    EXIT,
    CREATE,
    READ,
    READ_ALL,
    UPDATE,
    DELETE,
    ITEMS_IN_ORDER,
    ITEM_BY_ORDER_PRODUCT = 6
}



