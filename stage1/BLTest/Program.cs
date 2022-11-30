
using BO;
using BlApi;
using BlImplementation;
class Program
{
    public IBl bl = new BlImplementation.BL();

    public void ProductMenu()
    {
        int inner_choice;
    }
    public void OrderMenu()
    {

    }
    public void CartMenu()
    {

    }
    public void Main()
    {
        int out_choice;
        do
        {
            Console.WriteLine("enter 0 to stop the program\nenter 1 for product\nenter 2 for order\nenter 3 for cart\n");
            out_choice = Console.Read();
            switch ((OUT_CHOICE)out_choice)
            {
                case OUT_CHOICE.EXIT:
                    break;
                case OUT_CHOICE.PRODUCT:
                    ProductMenu();
                    break;
                case OUT_CHOICE.ORDER:
                    OrderMenu();
                    break;
                case OUT_CHOICE.CART:
                    CartMenu();
                    break;
            }
        } while (out_choice != 0);

    }
}
