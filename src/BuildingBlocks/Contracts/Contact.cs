using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Contact
{
      public record UserCreated(string id,string userName,DateTime createdDate);

    public record ContactCreated(string id, string userName, DateTime createdDate);


    public record LocationCreated(string id, string userName, DateTime createdDate);

    public record LocationComplated(string id, string url, string data);
}
