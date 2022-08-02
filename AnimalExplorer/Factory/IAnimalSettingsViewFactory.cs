using System;
using Animal;

namespace AnimalExplorer.Factory{
    public interface IAnimalSettingsViewFactory{
        IAnimalSettingsView Create(string name);
        void Release(IAnimalSettingsView view);
    }
}