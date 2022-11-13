using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface Icrud<T> where T : struct
    {
        int Create(T obj);
        T Read(int id);
        bool Update(T obj);
        void Delete(int id);

    }
}
