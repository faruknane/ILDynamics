using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ILDynamics
{
    public class ILMethod
    {
        public Dictionary<ILParameter, int> ParameterIndex { get; }
        public List<Type> ParameterTypes { get; }
        public Dictionary<ILVariable, int> VariableIndex { get; }
        public List<Type> VariableTypes { get; }

        internal ILOpCodes OpCodes;

        public Type ReturnType;

        public ILMethod(Type returntype)
        {
            ParameterTypes = new List<Type>();
            ParameterIndex = new Dictionary<ILParameter, int>();

            VariableIndex = new Dictionary<ILVariable, int>();
            VariableTypes = new List<Type>();
            OpCodes = new ILOpCodes();

            this.ReturnType = returntype;
        }

        public void Return()
        {
            OpCodes.Emit(System.Reflection.Emit.OpCodes.Ret);
        }

        public void Return(ILObject v)
        {
            v.Load();
            OpCodes.Emit(System.Reflection.Emit.OpCodes.Ret);
        }

        public MethodInfo Create()
        {
            AssemblyName asmName = new AssemblyName();
            asmName.Name = "DynamicILAssembly";

            var demoAssembly = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndCollect);

            ModuleBuilder demoModule = demoAssembly.DefineDynamicModule(asmName.Name);

            TypeBuilder demoType = demoModule.DefineType("DynamicType", TypeAttributes.Public);

            MethodBuilder factory = demoType.DefineMethod("DynamicMethod",
                MethodAttributes.Public | MethodAttributes.Static,
                ReturnType,
                ParameterTypes.ToArray());

            ILGenerator il = factory.GetILGenerator();

            OpCodes.Generate(il);

            Type dt = demoType.CreateType();
            return dt.GetMethod("DynamicMethod");
        }

        public ILObject Constant<T>(T v)
        {
            return ILConstant.From(this, v);
        }

        public ILObject Sum(params ILObject[] objs)
        {
            return new ILOperatorPlus(this, objs);
        }

        public ILObject GetValueByRef(ILObject obj, Type t)
        {
            return new ILGetValueByRef(this, obj, t);
        }

        public virtual int NewParameter(ILParameter ilParameter)
        {
            if(ParameterIndex.ContainsKey(ilParameter))
                throw new Exception("You can't add the same parameter twice!");

            ParameterTypes.Add(ilParameter.Type);
            return ParameterIndex[ilParameter] = ParameterIndex.Count;
        }

        public ILParameter NewParameter(Type t)
        {
            return new ILParameter(this, t);
        }

        public virtual int NewVariable(ILVariable iLVariable)
        {
            if (VariableIndex.ContainsKey(iLVariable))
                throw new Exception("You can't add the same variable twice!");

            OpCodes.DeclareVariable(iLVariable.Type);
            VariableTypes.Add(iLVariable.Type);
            return VariableIndex[iLVariable] = VariableIndex.Count;
        }

        public ILVariable NewVariable(Type t)
        {
            return new ILVariable(this, t);
        }
    }
}