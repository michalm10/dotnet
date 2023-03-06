using NUnit.Framework;
using Employees;
using Employees.ConcreteEmployees;

namespace TestEmployees
{
    [TestFixture]
    public class TestEmployees
    {
        private Register register = new Register();

        private Adress ad1 = new Adress("asdfghjkl;", "London", "41C", "89");
        private Adress ad2 = new Adress("qwertyuiop", "London", "41C", "89");
        private Adress ad3 = new Adress("asdf", "London", "41C", "89");
        private Adress ad4 = new Adress("asdf", "Warsaw", "32A", "9");
        private Adress ad5 = new Adress("asdf", "Warsaw", "32A", "39");
        private Adress ad6 = new Adress("asdf", "Gdansk", "41G", "49");

        private Employee emp1, emp2, emp3, emp4, emp5, emp6, emp7, emp8, emp9, emp10;

        [OneTimeSetUp]
        public void setup()
        {
            emp1 = new OfficeEmployee("asdf", "bidf", 32, 7.3, ad1, 121);
            emp2 = new OfficeEmployee("asdf", "gfgs", 32, 7.3, ad2, 121);
            emp3 = new OfficeEmployee("asdf", "rhvs", 22, 7.3, ad3, 132);
            emp4 = new OfficeEmployee("asdf", "kyrs", 22, 7.3, ad4, 121);
            emp5 = new ManualWorker("jkl;", "edfid", 23, 3.1, ad3, 80);
            emp6 = new ManualWorker("jkl;", "beufs", 23, 3.1, ad4, 70);
            emp7 = new ManualWorker("jkl;", "wdbed", 23, 3.1, ad5, 80);
            emp8 = new Trader("qwer", "uiop", 41, 5.3, ad6, 132, Efficiency.MEDIUM);
            emp9 = new Trader("qwer", "trew", 41, 5.3, ad1, 132, Efficiency.HIGH);
            emp10 = new Trader("qwer", "vewr", 41, 5.3, ad3, 132, Efficiency.MEDIUM);
        }
        [Test]
        public void addEmployee()
        {
            register.add(emp1);
            register.add(emp5);
            register.add(emp8);
            Assert.AreEqual(emp1, register.find(emp1.id));
            Assert.AreEqual(emp5, register.find(emp5.id));
            Assert.AreEqual(emp8, register.find(emp8.id));
            register.clear();
        }

        [Test]
        public void removeEmployee()
        {
            register.add(emp1);
            register.remove(emp1.id);
            Assert.AreEqual(register.find(emp1.id), null);
            register.clear();
        }

        [Test]
        public void addAtOnce()
        {
            register.addMany(new List<Employee> { emp1, emp5, emp8 });
            Assert.AreEqual(emp1, register.find(emp1.id));
            Assert.AreEqual(emp5, register.find(emp5.id));
            Assert.AreEqual(emp8, register.find(emp8.id));
            register.clear();
        }

        [Test]
        public void sortExp()
        {
            register.addMany(new List<Employee> { emp1, emp2, emp3, emp4, emp5, emp6, emp7, emp8, emp9, emp10 });
            Assert.AreEqual(new List<Employee> { emp1, emp2, emp3, emp4, emp8, emp9, emp10, emp5, emp6, emp7 }, 
                register.getOrder(Comparators.CompareByExpDesc));
            register.clear();
        }

        [Test]
        public void sortAge()
        {
            register.addMany(new List<Employee> { emp1, emp2, emp3, emp4, emp5, emp6, emp7, emp8, emp9, emp10 });
            var t = register.getOrder(Comparators.CompareByAgeAsc);
            Assert.AreEqual(new List<Employee> { emp3, emp4, emp5, emp6, emp7, emp1, emp2, emp8, emp9, emp10 }, t);
            register.clear();
        }

        [Test]
        public void sortLastName()
        {
            register.addMany(new List<Employee> { emp1, emp2, emp3, emp4, emp5, emp6, emp7, emp8, emp9, emp10 });
            var t = register.getOrder(Comparators.CompareByLastNameAsc);
            Assert.AreEqual(new List<Employee> { emp6, emp1, emp5, emp2, emp4, emp3, emp9, emp8, emp10, emp7 }, 
                register.getOrder(Comparators.CompareByLastNameAsc));
            register.clear();
        }

        [Test]
        public void showCityEmployees()
        {
            register.addMany(new List<Employee> { emp1, emp2, emp3, emp4, emp5, emp6, emp7, emp8, emp9, emp10 });
            Assert.AreEqual(new List<Employee> { emp1, emp2, emp3, emp5, emp9, emp10 },
                register.filterByCity("London"));
            register.clear();
        }

        [Test]
        public void getValues()
        {
            register.addMany(new List<Employee> { emp1, emp2, emp3, emp4, emp5, emp6, emp7, emp8, emp9, emp10 });
            var temp = register.getValues();
            Assert.AreEqual(883.29999999999995, temp["1"].Item2);
            Assert.AreEqual(10.782608695652174, temp["5"].Item2);
            Assert.AreEqual(477, temp["8"].Item2);
            register.clear();
        }
    }
}