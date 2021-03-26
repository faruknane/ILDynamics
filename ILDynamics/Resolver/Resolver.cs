using ILDynamics.Resolver.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILDynamics.Resolver
{
    public static class Resolver
    {
        public static void CopyMethodBody(MethodInfo info, ILGenerator il, IEnumerable<Filter> filters)
        {
            var methodbody = info.GetMethodBody();

            if (il != null)
                foreach (var item in methodbody.LocalVariables)
                    il.DeclareLocal(item.LocalType, item.IsPinned);

            byte[] arr = methodbody.GetILAsByteArray();

            for (int i = 0; i < arr.Length;)
            {
                var code = ILHelper.GetOpCode(arr.AsSpan(i), ref i);
                int size = code.GetOperandSize(arr.AsSpan(i));

                bool applied = false;
                foreach (var filter in filters)
                {
                    applied = applied || filter.Apply(code, size, arr.AsSpan(i));
                    if (applied)
                        break;
                }

                if (!applied)
                    throw new Exception("No filter is applied!");

                i += size;
            }
        }
        public static MethodInfo CopyMethod(MethodInfo m, params Filter[] filters)
        {
            return CopyMethod(m, (IEnumerable<Filter>)filters);
        }

        
        public static MethodInfo CopyMethod(MethodInfo m, IEnumerable<Filter> filters = null)
        {
            AssemblyName asmName = new AssemblyName();
            asmName.Name = "DynamicILAssembly";

            var demoAssembly = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndCollect);

            ModuleBuilder demoModule = demoAssembly.DefineDynamicModule(asmName.Name);

            TypeBuilder demoType = demoModule.DefineType("DynamicType", TypeAttributes.Public);

            var argsofm = m.GetParameters();
            Type[] args = new Type[argsofm.Length];
            for (int i = 0; i < args.Length; i++)
                args[i] = argsofm[i].ParameterType;

            MethodBuilder factory = demoType.DefineMethod("DynamicMethod",
                MethodAttributes.Public | MethodAttributes.Static,
                m.ReturnType,
                args);

            ILGenerator il = factory.GetILGenerator();

            if (filters == null)
                filters = new List<Filter>() { new NoFilter(m, il) };

            foreach (var item in filters)
            {
                if (!item.Initialized)
                    item.Initialize(m, il);
            }

            CopyMethodBody(m, il, filters);

            Type dt = demoType.CreateType();
            return dt.GetMethod("DynamicMethod");
        }


    }
}
