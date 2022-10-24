using Contact.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Entities
{
    [Table("user", Schema = "public")]
    public class ContactReport : EntityBase
    {
        public string name { get; set; }

        public string surname { get; set; }

        public string company { get; set; }

        public List<Contact> contactList { get; set; }

    }
}
