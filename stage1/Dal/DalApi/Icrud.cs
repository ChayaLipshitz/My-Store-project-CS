using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface Icrud<T> where T : struct
    {
       IEnumerable<T> ReadByFilter(Func<T, bool> f = null);
        T ReadSingle(Func<T, bool> f );   
        int Create(T obj);
        T Read(int id);
        bool Update(T obj);
        void Delete(int id);

    }
}
