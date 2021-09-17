using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HRE.Core.DI;

namespace HRE.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static List<Type> GetTypesAssignableFrom<T>(this Assembly assembly)
        {
            return assembly.GetTypesAssignableFrom(typeof(T));
        }

        public static List<Type> GetTypesAssignableFrom(this Assembly assembly, Type compareType)
        {
            List<Type> ret = new List<Type>();
            foreach (var type in assembly.DefinedTypes)
            {
                if (compareType.IsAssignableFrom(type) && compareType != type)
                {
                    ret.Add(type);
                }
            }
            return ret;
        }

        public static List<KeyValuePair<Type, List<Type>>> GetTypesAssignableWithInterfacesFrom<T>(this Assembly assembly)
        {
            return assembly.GetTypesAssignableWithInterfacesFrom(typeof(T));
        }

        public static List<KeyValuePair<Type, List<Type>>> GetTypesAssignableWithInterfacesFrom(this Assembly assembly, Type compareType)
        {
            List<KeyValuePair<Type, List<Type>>> ret = new List<KeyValuePair<Type, List<Type>>>();
            foreach (var type in assembly.DefinedTypes)
            {
                if (compareType.IsAssignableFrom(type) && compareType != type)
                {
                    ret.Add(new KeyValuePair<Type, List<Type>>(type, type.GetInterfaces().Where(x => x.Name != nameof(IScopedDependency) && x.Name != nameof(ITransientDependency) && x.Name != nameof(ISingletonDependency)).ToList()));
                }
            }
            return ret;
        }
    }
}
