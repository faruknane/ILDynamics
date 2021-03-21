using ILDynamics.Ops;
using System;
using System.Reflection;

namespace ILDynamics
{
    public abstract class RefableObject : ILObject
    {
        public Type Type { get; protected set; }

        public RefableObject(Method m) : base(m)
        {

        }

        public ILObject Call(MethodInfo objm, params ILObject[] parameters)
        {
            return new OpCall(this.Method, this, objm, parameters);
        }

        public abstract void LoadAddress();
    }
    //todo need RefObject
}