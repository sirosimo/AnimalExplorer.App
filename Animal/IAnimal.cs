namespace Animal{
    public interface IAnimal{
        int NumberOfLegs{ get; set; }
        string Name { get; set; }
        void Eat();
        void Sleep();
        void Reproduce();
    }
}