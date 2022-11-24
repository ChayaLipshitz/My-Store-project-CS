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
        public void addOrderItem(Cart cart,int id);
        public void UpdateOrderItem(Cart cart,int id,int quantity);
        public void SubmitOrder (Cart cart,int id//customerDetails already in cart?
                                                 );
        
    }
}
