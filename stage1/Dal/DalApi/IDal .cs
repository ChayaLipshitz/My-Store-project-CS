
namespace DalApi
{
    public interface IDal 
    {
        public Iorder order { get;  }
        public IorderItem orderItem { get;  }
        public Iproduct product { get;  }
    }
}
