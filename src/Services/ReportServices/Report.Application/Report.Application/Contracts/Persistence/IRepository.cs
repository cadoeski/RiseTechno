using Report.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Contracts.Persistence
{
    public interface IRepository<T> where T : EntityBase
    {

    } 
}
