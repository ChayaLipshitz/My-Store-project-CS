using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal.DO;

namespace Dal
{
    sealed public class DalList : IDal
        
    {
        private static DalList Instance=new DalList();
       // public  static DalList instance { get { return GetInstence(); } }
        public static DalList GetInstence()
        {
            lock (Instance) // thread safe
            {
                if (Instance == null)
                    Instance = new  DalList();
                return Instance;
            }
        }
        private DalList() { }
        public  Iorder iorder => new DalOrder();

        public IorderItem iorderItem =>  new DalOrderItem();

        public Iproduct iproduct =>  new DalProduct();
    }
}
