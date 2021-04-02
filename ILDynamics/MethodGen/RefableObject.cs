using ILDynamics.MethodGen.Ops;
using ILDynamics.MethodGen;
using System;
using System.Reflection;

namespace ILDynamics.MethodGen
{
    public abstract class RefableObject : ILOp
    {
        public Type Type { get; protected set; }

        public RefableObject() 
        {

        }

        public ILOp Call(MethodInfo objm, params ILOp[] parameters)
        {
            return new OpCall(this, objm, parameters);
        }

        public abstract void LoadAddress(Method Method);
    }
    //todo need RefObject
}