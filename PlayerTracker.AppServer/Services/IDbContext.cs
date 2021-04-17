using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerTracker.AppServer.Services
{
    interface IDbContext<T>
        where T: DbConnection
    {

        T Connection { get; set; }

    }
}
