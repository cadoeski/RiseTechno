using Contact.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Entities
{
    public  class Contact : EntityBase
    {
      

        public string phone_number { get; set; }

        public string email { get; set; }

        public string konum { get; set; }

        public Guid userid { get; set; }

        public User user { get; set; }
    }
}
