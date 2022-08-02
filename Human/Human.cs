using Animal;

namespace Human{
    public interface IHuman : IAnimal{
        string FirstName{ get; set; }
        string LastName{ get; set; }
        string PhoneNumber{ get; set; }
    }

    public class Human : Animal.Animal, IHuman{
        public Human(){
            NumberOfLegs = 2;
            Name = "Homo Sapiens Sapiens";
        }

        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string PhoneNumber{ get; set; }

        public override void Eat(){
            //Eat
        }

        public override void Sleep(){
            //Sleep
        }

        public override void Reproduce(){
            //Sometime For Fun
        }
    }
}