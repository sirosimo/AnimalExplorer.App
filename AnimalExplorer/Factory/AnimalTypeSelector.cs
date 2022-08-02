using System;
using System.Reflection;
using Castle.Facilities.TypedFactory;

namespace AnimalExplorer.Factory{
    public class AnimalTypeSelector : DefaultTypedFactoryComponentSelector
    {
        public AnimalTypeSelector() : base(fallbackToResolveByTypeIfNameNotFound: true)
        {
            
        }

        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            if (arguments.Length <= 0) return base.GetComponentType(method, arguments);
            var daqType = arguments?[0] as Type;
            return daqType;
        }

        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (arguments.Length <= 0) return base.GetComponentName(method, arguments);
            var daqType = arguments?[0] as Type;

            return daqType?.Name;
        }

    }
}