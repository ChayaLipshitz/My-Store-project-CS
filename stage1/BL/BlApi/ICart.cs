using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface ICart
    {
        public void addOrderItem(BO.Cart cart,int id);
        public void UpdateOrderItem(BO.Cart cart,int id,int quantity);
        public void SubmitOrder (BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress//customerDetails already in cart?
                                                                                                             );
        
    }
}
