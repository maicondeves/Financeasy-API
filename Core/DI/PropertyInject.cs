using System;
using System.Linq;
using System.Reflection;
using SimpleInjector.Advanced;

namespace Financeasy.Api.Core.DI
{
    public class PropertyInject<TAttribute> : IPropertySelectionBehavior where TAttribute : Attribute
    {
        public bool SelectProperty(Type implementationType, PropertyInfo propertyInfo) =>
            propertyInfo.GetCustomAttributes(typeof(TAttribute)).Any();
    }
}