using Animal;

namespace AnimalExplorer.Factory{
    public interface IAnimalSettingsFactory{
        IAnimalSettings CreateAnimalSettings(string settingsName);
        void ReleaseSettings(IAnimalSettings settings);
    }
}