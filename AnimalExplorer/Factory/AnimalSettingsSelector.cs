using System;
using System.Reflection;
using Castle.Facilities.TypedFactory;

namespace AnimalExplorer.Factory{
    public class AnimalSettingsSelector : DefaultTypedFactoryComponentSelector
    {
        public AnimalSettingsSelector() : base(fallbackToResolveByTypeIfNameNotFound: true)
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
            var daqType = arguments?[0] as string;
            if(string.IsNullOrEmpty(daqType)) return base.GetComponentName(method, arguments);
            return $"{daqType}SettingsViewModel";
            //return daqType;
        }

    }
}