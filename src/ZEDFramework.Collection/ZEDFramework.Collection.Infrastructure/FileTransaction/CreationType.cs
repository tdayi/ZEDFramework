using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.FileTransaction
{
    public enum CreationType
    {
        CreatesNewfileAlways = 1,
        CreatesNewfileIfNotExist = 2,
        OpensFile = 3,
        OpensFileAndTruncate = 4,
        OpensFileOrCreate = 5
    }
}
