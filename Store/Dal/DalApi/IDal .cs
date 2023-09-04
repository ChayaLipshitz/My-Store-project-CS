
namespace DalApi
{
    public interface IDal 
    {
        public Iorder iorder { get;  }
        public IorderItem iorderItem { get;  }
        public Iproduct iproduct { get;  }
    }
}
