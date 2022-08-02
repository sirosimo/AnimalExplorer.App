using Caliburn.Micro;
using Human;

namespace Animal.WpfSettings.ViewModels{
    public class HumanSettingsViewModel : Screen, IAnimalSettings{
        private readonly IHuman _human;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;

        public HumanSettingsViewModel(IHuman human){
            _human = human;
            FirstName = _human.FirstName;
            LastName = _human.LastName;
            PhoneNumber = _human.PhoneNumber;
        }

        public string FirstName{
            get => _firstName;
            set{
                if (value == _firstName) return;
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
            }
        }

        public string LastName{
            get => _lastName;
            set{
                if (value == _lastName) return;
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
            }
        }

        public string PhoneNumber{
            get => _phoneNumber;
            set{
                if (value == _phoneNumber) return;
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);
            }
        }
    }
}