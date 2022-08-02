using Animal;

namespace Dog{
    public interface IDog : IAnimal{
        string FurColor{ get; set; }
        bool IsDomesticated{ get; set; }
        bool IsVaccinated{ get; set; }
    }

    public class Dog : Animal.Animal, IDog{
        public Dog(){
            NumberOfLegs = 4;
            Name = "default";
        }

        public string FurColor{ get; set; }
        public bool IsDomesticated{ get; set; }
        public bool IsVaccinated{ get; set; }

        public override void Eat(){
            //Eat
        }

        public override void Sleep(){
            //Sleep
        }

        public override void Reproduce(){
            //With your leg
        }
    }
}