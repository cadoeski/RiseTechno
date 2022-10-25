using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Domain.Common
{
    public class EntityBase
    {
        [Key]
        public Guid id { get; set; } = new Guid();
    }
}
