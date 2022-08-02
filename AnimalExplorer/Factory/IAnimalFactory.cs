using System;
using Animal;

namespace AnimalExplorer.Factory{
    public interface IAnimalFactory{
        IAnimal CreateAnimal(Type animalType);

        void Release(IAnimal animal);
    }
}