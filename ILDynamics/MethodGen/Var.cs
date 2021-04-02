using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ILDynamics.MethodGen
{
    public class Var<T> : Var
    {
        public Var() : base(typeof(T))
        {

        }
    }

    public class Var : RefableObject
    {
        public Dictionary<Method, bool> Initialized;
        public Dictionary<Method, int> Index;

        public Var(Type type) 
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
                int index = Method.NewVar(this);
                Index[Method] = index;
            }
        }

        public override void LoadAddress(Method Method)
        {
            Init(Method);
            Method.OpCodes.Emit(OpCodes.Ldloca_S, Index[Method]);
        }

        public override void Load(Method Method)
        {
            Init(Method);
            Method.OpCodes.Emit(OpCodes.Ldloc_S, Index[Method]);
        }

        public override void Store(Method Method)
        {
            Init(Method);
            Method.OpCodes.Emit(OpCodes.Stloc_S, Index[Method]);
        }

    }
}