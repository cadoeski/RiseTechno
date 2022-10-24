using Contact.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Entities
{
    public class User : EntityBase
    {
         
        public string name { get; set; }

        public string surname { get; set; }

        public string company { get; set; }

        public List<Contact> contacts { get; set; } = new List<Contact>();

    }
}
