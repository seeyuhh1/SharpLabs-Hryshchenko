using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSystem.Tests
{
    [TestFixture]
    public class CoffeeMachineTests
    {
        private TextWriter _originalOut;

        [SetUp]
        public void SetUp()
        {
            _originalOut = Console.Out;
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_originalOut);
        }

        // Перевіряє, що CoffeeMachine наслідує Device та реалізує IEnergyConsumer
        [Test]
        public void CoffeeMachine_InheritsDeviceAndImplementsIEnergyConsumer()
        {
            var type = typeof(CoffeeMachine);
            Assert.IsTrue(typeof(Device).IsAssignableFrom(type), "CoffeeMachine має наслідувати Device");
            Assert.IsTrue(typeof(IEnergyConsumer).IsAssignableFrom(type), "CoffeeMachine має реалізувати IEnergyConsumer");
        }

        // Перевіряє правильність споживання енергії для CoffeeMachine (1000 Вт)
        [Test]
        public void CoffeeMachine_PowerConsumptionIs1000Watts()
        {
            var coffee = new CoffeeMachine { Name = "Test Coffee" };
            Assert.AreEqual(1000, coffee.PowerConsumption, "CoffeeMachine має споживати 1000 Вт");
        }

        // Перевіряє, що TurnOn() виводить правильне повідомлення
        [Test]
        public void CoffeeMachine_TurnOn_PrintsCorrectMessage()
        {
            var coffee = new CoffeeMachine { Name = "Тестова кавомашина" };

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                coffee.TurnOn();

                Assert.IsTrue(coffee.IsOn);
                Assert.That(sw.ToString(), Does.Contain("Тестова кавомашина почала готувати каву."));
            }
        }

        // Перевіряє, що TurnOff() виводить правильне повідомлення
        [Test]
        public void CoffeeMachine_TurnOff_PrintsCorrectMessage()
        {
            var coffee = new CoffeeMachine { Name = "Тестова кавомашина" };
            coffee.TurnOn();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                coffee.TurnOff();

                Assert.IsFalse(coffee.IsOn);
                Assert.That(sw.ToString(), Does.Contain("Тестова кавомашина завершила роботу."));
            }
        }

        // Перевіряє розрахунок споживання енергії для увімкненої кавомашини
        [Test]
        public void CoffeeMachine_GetEnergyUsage_ReturnsCorrectValue()
        {
            var coffee = new CoffeeMachine { Name = "Test" };
            coffee.TurnOn();

            double energy = coffee.GetEnergyUsage(5);
            double expected = 1000 * 5 / 1000.0; // 5.00 кВт·год

            Assert.AreEqual(expected, energy, 0.01);
        }
    }
}
