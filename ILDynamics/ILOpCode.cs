using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ILDynamics
{

    public class ILPerformableOpCode
    {
        private Action<ILGenerator> Action;

        public ILPerformableOpCode(Action<ILGenerator> action)
        {
            this.Action = action;
        }

        public void Perform(ILGenerator il)
        {
            this.Action(il);
        }
    }

    public class ILOpCodes
    {
        public List<ILPerformableOpCode> OpList;

        public ILOpCodes()
        {
            OpList = new List<ILPerformableOpCode>();
        }
        
        public void Emit(OpCode op)
        {
            OpList.Add(new ILPerformableOpCode((ILGenerator il) =>
            {
                il.Emit(op);
            }));
        }

        public void Emit(OpCode op, byte arg)
        {
            OpList.Add(new ILPerformableOpCode((ILGenerator il) => {
                il.Emit(op, arg);
            }));
        }

        public void Emit(OpCode op, int arg)
        {
            OpList.Add(new ILPerformableOpCode((ILGenerator il) => {
                il.Emit(op, arg);
            }));
        }

        public void Emit(OpCode op, short arg)
        {
            OpList.Add(new ILPerformableOpCode((ILGenerator il) => {
                il.Emit(op, arg);
            }));
        }

        public void Emit(OpCode op, double arg)
        {
            OpList.Add(new ILPerformableOpCode((ILGenerator il) => {
                il.Emit(op, arg);
            }));
        }

        public void Emit(OpCode op, MethodInfo arg)
        {
            OpList.Add(new ILPerformableOpCode((ILGenerator il) => {
                il.Emit(op, arg);
            }));
        }

        public void DeclareVariable(Type t)
        {
            OpList.Add(new ILPerformableOpCode((ILGenerator il) => {
                il.DeclareLocal(t);
            }));
        }

        public void DeclareVariable(Type t, bool pinned)
        {
            OpList.Add(new ILPerformableOpCode((ILGenerator il) => {
                il.DeclareLocal(t, pinned);
            }));
        }

        public void Generate(ILGenerator il)
        {
            foreach (var item in OpList)
            {
                item.Perform(il);
            }
        }
    }
}