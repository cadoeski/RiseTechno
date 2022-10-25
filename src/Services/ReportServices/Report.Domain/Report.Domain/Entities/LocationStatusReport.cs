using Report.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Domain.Entities
{
    public class LocationStatusReport : EntityBase
    {
        public DateTime created_date { get; set; } = DateTime.Now;

        public string status { get; set; }

        public string report { get; set; }
    }
}
