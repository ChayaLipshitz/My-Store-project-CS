using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

internal class NotExistExceptions : Exception
{
    public override string Message => "object does not exist";

}

internal class DuplicateIdExceptions : Exception
{
    public override string Message => "ID already exists";

}

