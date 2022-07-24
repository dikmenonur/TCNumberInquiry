using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCNumberInquiry.Core.Entities
{
    public enum CacheExpiryStrategy : int
    {
        Default = 0,
        FavorWrite = 1,
        Balanced = 2,
        FavorRead = 3
    }
}
