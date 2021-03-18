using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ILDynamics
{

    public class StaticMethod<T> : StaticMethod
    {
        public StaticMethod() : base(typeof(T))
        {

        }

        public new T this[params object[] objs]
        {
            get
            {
                return (T)base[objs];
            }
        }

    }

    public class StaticMethod
    {
        public Dictionary<Param, int> ParameterIndex { get; }
        public List<Type> ParameterTypes { get; }
        public Dictionary<Var, int> VariableIndex { get; }
        public List<Type> VariableTypes { get; }

        internal ILOpCodes OpCodes;

        public Type ReturnType;

        private MethodInfo methodInfo;

        public StaticMethod(Type returntype)
        {
            ParameterTypes = new List<Type>();
            ParameterIndex = new Dictionary<Param, int>();

            VariableIndex = new Dictionary<Var, int>();
            VariableTypes = new List<Type>();
            OpCodes = new ILOpCodes();

            this.ReturnType = returntype;
        }

        public object this[params object[] objs]
        {
            get
            {
                return methodInfo.Invoke(null, objs);
            }
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
            if (methodInfo != null)
                throw new Exception("The static method is already created!");

            AssemblyName asmName = new AssemblyName();
            asmName.Name = "DynamicILAssembly";

            var demoAssembly = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndCollect);

            ModuleBuilder demoModule = demoAssembly.DefineDynamicModule(asmName.Name);

            TypeBuilder demoType = demoModule.DefineType("DynamicType", TypeAttributes.Public);

            System.Reflection.Emit.MethodBuilder factory = demoType.DefineMethod("DynamicMethod",
                MethodAttributes.Public | MethodAttributes.Static,
                ReturnType,
                ParameterTypes.ToArray());

            ILGenerator il = factory.GetILGenerator();

            OpCodes.Generate(il);

            Type dt = demoType.CreateType();
            return methodInfo = dt.GetMethod("DynamicMethod");
        }

        public ILObject Constant<T>(T v)
        {
            return new Constant<T>(this, v);
        }

        public ILObject Sum(params ILObject[] objs)
        {
            return new OpPlus(this, objs);
        }

        public ILObject GetValueByRef(IReffable obj)
        {
            return new OpValueByRef(this, obj);
        }

        public ILObject GetRefByVar(IReffable obj)
        {
            return new OpRefByVar(obj);
        }

        public RefVar NewRefVar(IReffable v)
        {
            return new RefVar(this, v);
        }

        public virtual int NewParam(Param ilParameter)
        {
            if(ParameterIndex.ContainsKey(ilParameter))
                throw new Exception("You can't add the same parameter twice!");

            ParameterTypes.Add(ilParameter.Type);
            return ParameterIndex[ilParameter] = ParameterIndex.Count;
        }

        public Param NewParam(Type t)
        {
            return new Param(this, t);
        }

        public virtual int NewVar(Var iLVariable)
        {
            if (VariableIndex.ContainsKey(iLVariable))
                throw new Exception("You can't add the same variable twice!");

            OpCodes.DeclareVariable(iLVariable.Type);
            VariableTypes.Add(iLVariable.Type);
            return VariableIndex[iLVariable] = VariableIndex.Count;
        }

        public Var NewVar(Type t)
        {
            return new Var(this, t);
        }
    }
}