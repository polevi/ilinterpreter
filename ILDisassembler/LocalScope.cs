using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace ILDisassembler
{
    public class LocalScope
    {
        private Assembly localAssembly;

        public LocalScope(Assembly localAssembly)
        {
            this.localAssembly = localAssembly;
            this.Addrs = new Dictionary<long, int>();
        }

        public Assembly LocalAssembly
        {
            get
            {
                return localAssembly;
            }
        }

        public Dictionary<long, int> Addrs { get; set; }
    }
}
