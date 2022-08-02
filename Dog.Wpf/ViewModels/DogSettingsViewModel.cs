using Caliburn.Micro;
using Dog;

namespace Animal.WpfSettings.ViewModels{
    public class DogSettingsViewModel : Screen, IAnimalSettings{
        private readonly IDog _dog;
        private string _furColor;
        private bool _isDomesticated;
        private bool _isVaccinated;

        public DogSettingsViewModel(IDog dog){
            _dog = dog;

            FurColor = _dog.FurColor;
            IsDomesticated = _dog.IsDomesticated;
            IsVaccinated = _dog.IsVaccinated;
        }

        public bool IsDomesticated{
            get => _isDomesticated;
            set{
                if (value == _isDomesticated) return;
                _isDomesticated = value;
                NotifyOfPropertyChange(() => IsDomesticated);
            }
        }

        public bool IsVaccinated{
            get => _isVaccinated;
            set{
                if (value == _isVaccinated) return;
                _isVaccinated = value;
                NotifyOfPropertyChange(() => IsVaccinated);
            }
        }


        public string FurColor{
            get => _furColor;
            set{
                if (value == _furColor) return;
                _furColor = value;
                NotifyOfPropertyChange(() => FurColor);
            }
        }
    }
}