using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Domain.Entities
{
    public class vw_report
    {
        public string konum { get; set; }

        public int kayitli_kisi { get; set; } = 0;

        public int tel_sayisi { get; set; } = 0;
    }
}
