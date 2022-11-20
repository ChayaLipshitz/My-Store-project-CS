using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class NotExistExceptions : Exception
    {
        public override string Message => "object does not exist";

    }
}