using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DO;

public class NotExistExceptions : Exception
{
    public override string Message => "object does not exist";

}

public class DuplicateIdExceptions : Exception
{
    public override string Message => "ID already exists";

}

