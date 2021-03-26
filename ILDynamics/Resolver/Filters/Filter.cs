using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.Resolver.Filters
{
    public abstract class Filter
    {
        public MethodInfo Info;
        public ILGenerator IL;
        public bool Initialized { get; protected set; }

        public Filter()
        {
            Initialized = false;
        }

        public virtual void Initialize(MethodInfo info, ILGenerator il)
        {
            this.Info = info;
            this.IL = il;
            Initialized = true;
        }

        public abstract bool Apply(OpCode opcode, int opsize, Span<byte> operands);
    }
}
