namespace Employees
{
    public class Employee
    {
        private static int idCount { get; set; }
        public string id { get; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public double experience { get; set; }
        public Adress adress { get; set; }

        public Employee(string firstName, string lastName, int age, double experience, Adress adress)
        {
            this.id = (++idCount).ToString();
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.experience = experience;
            this.adress = adress;
        }

        public virtual double getValue() => experience;
    }
}