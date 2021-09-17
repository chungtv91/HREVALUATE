using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using HRE.Core.Shared.Mappers;

namespace HRE.Web.UI.Customization
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.StartsWith("HRE."))
                {
                    ApplyMappingsFromAssembly(assembly);
                }
            }
        }

        /// <summary>
        /// https://www.ezzylearning.net/tutorial/a-step-by-step-guide-of-using-automapper-in-asp-net-core
        /// </summary>
        /// <param name="assembly"></param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .Where(x => !x.IsAbstract)
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping") ??
                                 type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}