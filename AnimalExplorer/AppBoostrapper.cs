using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Animal;
using AnimalExplorer.Factory;
using AnimalExplorer.ViewModels;
using Caliburn.Micro;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Component = Castle.MicroKernel.Registration.Component;

namespace AnimalExplorer{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        private WindsorContainer _container;

        protected override void Configure()
        {
            _container = new WindsorContainer();
            ConfigureContainer(_container);


        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var windowManager = _container.Resolve<IWindowManager>();
            var shellViewModel = _container.Resolve<IShellViewModel>();
            windowManager.ShowWindowAsync(shellViewModel);

            base.OnStartup(sender, e);
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                ? _container.Kernel.HasComponent(service)
                    ? _container.Resolve(service)
                    : base.GetInstance(service, key)
                : _container.Kernel.HasComponent(key)
                    ? _container.Resolve(key, service)
                    : base.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.ResolveAll(service).Cast<object>();
        }


        private void ConfigureContainer(WindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.AddFacility<TypedFactoryFacility>();
            container.Register(
                Component.For<IWindowManager>().ImplementedBy<WindowManager>(),
                Component.For<IShellViewModel>().ImplementedBy<ShellViewModel>(),

                //Register all the potential Animal dependency created by other users
                Types.FromAssemblyInDirectory(new AssemblyFilter("Animals"))
                    .BasedOn<IAnimal>()
                    .LifestyleTransient()
                    .Configure(component => component.Named(component.Implementation.Name))
                    .WithServiceFromInterface(),

                //Register the User Controls Views
                Types.FromAssemblyInDirectory(new AssemblyFilter("Animals"))
                    .BasedOn<UIElement>()
                    .LifestyleTransient()
                    .Configure(component => component.Named(component.Implementation.Name))
                    .WithServiceFromInterface(),

                //Register the User Controls ViewModels
                Types.FromAssemblyInDirectory(new AssemblyFilter("Animals"))
                    .BasedOn<IAnimalSettings>()
                    .LifestyleTransient()
                    .Configure(component => component.Named(component.Implementation.Name))
                    .WithServiceFromInterface(),

                

                Component.For<AnimalTypeSelector, ITypedFactoryComponentSelector>(),
                Component.For<IAnimalFactory>().AsFactory(c=>c.SelectedWith<AnimalTypeSelector>()),

                Component.For<AnimalSettingsViewSelector, ITypedFactoryComponentSelector>(),
                Component.For<IAnimalSettingsViewFactory>().AsFactory(c => c.SelectedWith<AnimalSettingsViewSelector>()),

                Component.For<AnimalSettingsSelector, ITypedFactoryComponentSelector>(),
                Component.For<IAnimalSettingsFactory>().AsFactory((c =>c.SelectedWith<AnimalSettingsSelector>()))

            );


        }

    }
}