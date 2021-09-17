using System;
using System.ComponentModel;
using AutoMapper;

namespace HRE.Core.Mappers
{
    // https://dotnettutorials.net/lesson/ignore-using-automapper-in-csharp/
    public static class IgnoreNoMapExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreNoMap<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var destinationType = typeof(TDestination);
            foreach (var property in destinationType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(destinationType)[property.Name];
                NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)];
                if (attribute != null) expression.ForMember(property.Name, opt => opt.Ignore());
            }

            return expression;
        }

        public static IMappingExpression IgnoreNoMap(this IMappingExpression expression, Type destinationType)
        {
            foreach (var property in destinationType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(destinationType)[property.Name];
                NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)];
                if (attribute != null) expression.ForMember(property.Name, opt => opt.Ignore());
            }

            return expression;
        }
    }
}
