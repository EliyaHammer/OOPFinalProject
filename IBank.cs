using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    interface IBank
    {
        string Name { get; }
        string Address { get; }
        int CustomerCounter { get; }
    }
}
