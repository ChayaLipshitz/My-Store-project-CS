
using DalApi;
using Dal.DO;


namespace Dal
{
    sealed public class DalXml : IDal

    {
        private static DalXml instance = new DalXml();
        public static DalXml Instance { get { return GetInstence(); } }

        public static DalXml GetInstence()
        {
            lock (instance) // thread safe
            {
                if (instance == null)
                    instance = new DalXml();
                return instance;
            }
        }
        private DalXml() { }
        public Iorder iorder => new DalOrder();

        public IorderItem iorderItem => new DalOrderItem();

        public Iproduct iproduct => new DalProduct();
    }
}
