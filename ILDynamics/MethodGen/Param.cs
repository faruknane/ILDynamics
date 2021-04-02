using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.MethodGen
{
    public class Param<T> : Param
    {
        public Param() : base(typeof(T))
        {
        }
    }

    public class Param : RefableObject
    {
        public Dictionary<Method, bool> Initialized;
        public Dictionary<Method, int> Index;

        public Param(Type type) 
        {
            this.Initialized = new Dictionary<Method, bool>();
            this.Index = new Dictionary<Method, int>();
            this.Type = type;
        }

        public virtual void Init(Method Method)
        {
            if (Initialized.ContainsKey(Method) && Initialized[Method] == true)
                return;
            else
            {
                Initialized[Method] = true;
                int index = Method.NewParam(this);
                Index[Method] = index;
            }
        }

        public override void LoadAddress(Method Method)
        {
            Init(Method);
            Method.OpCodes.Emit(OpCodes.Ldarga_S, Index[Method]);
        }

        public override void Load(Method Method)
        {
            Init(Method);
            Method.OpCodes.Emit(OpCodes.Ldarg_S, Index[Method]);
        }

        public override void Store(Method Method)
        {
            Init(Method);
            Method.OpCodes.Emit(OpCodes.Starg_S, Index[Method]);
        }

    }

}

