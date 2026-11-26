using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public interface IHasGetNextKey
    {
        long GetNextKey();
    }
}
