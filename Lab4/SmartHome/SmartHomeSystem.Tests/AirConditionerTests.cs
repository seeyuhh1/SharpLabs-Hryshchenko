using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSystem.Tests
{
    [TestFixture]
    public class AirConditionerTests
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

        // Перевіряє, що AirConditioner наслідує Device та реалізує IEnergyConsumer
        [Test]
        public void AirConditioner_InheritsDeviceAndImplementsIEnergyConsumer()
        {
            var type = typeof(AirConditioner);
            Assert.IsTrue(typeof(Device).IsAssignableFrom(type), "AirConditioner має наслідувати Device");
            Assert.IsTrue(typeof(IEnergyConsumer).IsAssignableFrom(type), "AirConditioner має реалізувати IEnergyConsumer");
        }

        // Перевіряє правильність споживання енергії для AirConditioner (2000 Вт)
        [Test]
        public void AirConditioner_PowerConsumptionIs2000Watts()
        {
            var ac = new AirConditioner { Name = "Test AC" };
            Assert.AreEqual(2000, ac.PowerConsumption, "AirConditioner має споживати 2000 Вт");
        }

        // Перевіряє, що TurnOn() виводить правильне повідомлення
        [Test]
        public void AirConditioner_TurnOn_PrintsCorrectMessage()
        {
            var ac = new AirConditioner { Name = "Тестовий кондиціонер" };

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ac.TurnOn();

                Assert.IsTrue(ac.IsOn);
                Assert.That(sw.ToString(), Does.Contain("Тестовий кондиціонер почав охолодження."));
            }
        }

        // Перевіряє, що TurnOff() виводить правильне повідомлення
        [Test]
        public void AirConditioner_TurnOff_PrintsCorrectMessage()
        {
            var ac = new AirConditioner { Name = "Тестовий кондиціонер" };
            ac.TurnOn();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ac.TurnOff();

                Assert.IsFalse(ac.IsOn);
                Assert.That(sw.ToString(), Does.Contain("Тестовий кондиціонер зупинено."));
            }
        }

        // Перевіряє розрахунок споживання енергії для увімкненого кондиціонера
        [Test]
        public void AirConditioner_GetEnergyUsage_ReturnsCorrectValue()
        {
            var ac = new AirConditioner { Name = "Test" };
            ac.TurnOn();

            double energy = ac.GetEnergyUsage(5);
            double expected = 2000 * 5 / 1000.0; // 10.00 кВт·год

            Assert.AreEqual(expected, energy, 0.01);
        }
    }
}
