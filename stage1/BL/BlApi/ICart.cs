

namespace BlApi
{
    public interface ICart
    {
        public BO.Cart addOrderItem(BO.Cart cart,int id);
        public BO.Cart UpdateOrderItem(BO.Cart cart,int id,int quantity);
        public void SubmitOrder (BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress//customerDetails already in cart?
                                                                                                             );        
    }
}
