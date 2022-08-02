using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Animal;
using AnimalExplorer.Factory;
using Caliburn.Micro;

namespace AnimalExplorer.ViewModels{
    public interface IShellViewModel{ }

    public class ShellViewModel : Conductor<IAnimalSettings>, IShellViewModel{
        private readonly IAnimalFactory _animalFactory;
        private readonly IAnimalSettingsFactory _animalSettingsFactory;
        private readonly IAnimalSettingsViewFactory _animalSettingsViewFactory;
        private BindableCollection<AnimalModel> _animals;
        private BindableCollection<Type> _animalTypes;
        private AnimalModel _selectedAnimal;
        private string _tempName;
        private Type _tempType;

        public ShellViewModel(IAnimalFactory animalFactory,
            IAnimalSettingsFactory animalSettingsFactory,
            IAnimalSettingsViewFactory animalSettingsViewFactory){

            _animalFactory = animalFactory;
            _animalSettingsFactory = animalSettingsFactory;
            _animalSettingsViewFactory = animalSettingsViewFactory;

            AnimalTypes = new BindableCollection<Type>();
            Animals = new BindableCollection<AnimalModel>();
        }


        public string TempName{
            get => _tempName;
            set{
                if (value == _tempName) return;
                _tempName = value;
                NotifyOfPropertyChange(() => TempName);
                NotifyOfPropertyChange(() => CanCreateAnimal);
            }
        }

        public BindableCollection<AnimalModel> Animals{
            get => _animals;
            set{
                if (Equals(value, _animals)) return;
                _animals = value;
                NotifyOfPropertyChange(() => Animals);
            }
        }

        public AnimalModel SelectedAnimal{
            get => _selectedAnimal;
            set{
                if (Equals(value, _selectedAnimal)) return;
                _selectedAnimal = value;
                ActiveItem = value.Settings;
                NotifyOfPropertyChange(() => SelectedAnimal);
            }
        }

        public Type TempType{
            get => _tempType;
            set{
                if (value == _tempType) return;
                _tempType = value;
                NotifyOfPropertyChange(() => TempType);
                NotifyOfPropertyChange(() => CanCreateAnimal);
            }
        }

        public BindableCollection<Type> AnimalTypes{
            get => _animalTypes;
            set{
                if (Equals(value, _animalTypes)) return;
                _animalTypes = value;
                NotifyOfPropertyChange(() => AnimalTypes);
            }
        }


        public bool CanCreateAnimal => !string.IsNullOrEmpty(TempName) && TempType != null;


        protected override Task OnActivateAsync(CancellationToken cancellationToken){
            GetAnimalTypes();
            return base.OnActivateAsync(cancellationToken);
        }


        private void GetAnimalTypes(){
            AnimalTypes.Clear();
            var animalTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t != typeof(Animal.Animal) && typeof(IAnimal).IsAssignableFrom(t))
                .Where(x=> !x.Name.StartsWith("I"))
                .ToList();

            AnimalTypes.AddRange(animalTypes);
        }

        public void CreateAnimal(){
            var newAnimal = _animalFactory.CreateAnimal(TempType);

            //Using Factories to instantiate the View and ViewModel for the settings
            var settingsViewModel = _animalSettingsFactory.CreateAnimalSettings(newAnimal.GetType().Name);
            var settingsView = _animalSettingsViewFactory.Create(newAnimal.GetType().Name);
            ViewModelBinder.Bind(settingsViewModel, settingsView as ContentControl, null);
            ActivateItemAsync(settingsViewModel);


            newAnimal.Name = TempName;

            var newModel = new AnimalModel{
                Animal = newAnimal,
                Settings = settingsViewModel
            };

            Animals.Add(newModel);
        }
    }

    public class AnimalModel{
        public IAnimal Animal{ get; set; }
        public IAnimalSettings Settings{ get; set; }
    }
}