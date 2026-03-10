using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSystem.Tests
{
    [TestFixture]
    public class LightTests
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

        // Перевіряє, що Light наслідує Device та реалізує IEnergyConsumer
        [Test]
        public void Light_InheritsDeviceAndImplementsIEnergyConsumer()
        {
            var type = typeof(Light);
            Assert.IsTrue(typeof(Device).IsAssignableFrom(type), "Light має наслідувати Device");
            Assert.IsTrue(typeof(IEnergyConsumer).IsAssignableFrom(type), "Light має реалізувати IEnergyConsumer");
        }

        // Перевіряє правильність споживання енергії для Light (60 Вт)
        [Test]
        public void Light_PowerConsumptionIs60Watts()
        {
            var light = new Light { Name = "Test Light" };
            Assert.AreEqual(60, light.PowerConsumption, "Light має споживати 60 Вт");
        }

        // Перевіряє, що TurnOn() встановлює IsOn = true та виводить правильне повідомлення
        [Test]
        public void Light_TurnOn_SetsIsOnTrueAndPrintsMessage()
        {
            var light = new Light { Name = "Тестова лампа" };

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                light.TurnOn();

                Assert.IsTrue(light.IsOn, "IsOn має бути true після TurnOn()");
                Assert.That(sw.ToString(), Does.Contain("Тестова лампа засвітилася."),
                    "TurnOn() має вивести правильне повідомлення");
            }
        }

        // Перевіряє, що TurnOff() встановлює IsOn = false та виводить правильне повідомлення
        [Test]
        public void Light_TurnOff_SetsIsOnFalseAndPrintsMessage()
        {
            var light = new Light { Name = "Тестова лампа" };
            light.TurnOn();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                light.TurnOff();

                Assert.IsFalse(light.IsOn, "IsOn має бути false після TurnOff()");
                Assert.That(sw.ToString(), Does.Contain("Тестова лампа вимкнена."),
                    "TurnOff() має вивести правильне повідомлення");
            }
        }

        // Перевіряє розрахунок споживання енергії для увімкненої лампи
        [Test]
        public void Light_GetEnergyUsage_WhenOn_ReturnsCorrectValue()
        {
            var light = new Light { Name = "Test" };
            light.TurnOn();

            double energy = light.GetEnergyUsage(5);
            double expected = 60 * 5 / 1000.0; // 0.30 кВт·год

            Assert.AreEqual(expected, energy, 0.01, "Розрахунок енергії для увімкненої лампи неправильний");
        }

        // Перевіряє, що вимкнена лампа не споживає енергію
        [Test]
        public void Light_GetEnergyUsage_WhenOff_ReturnsZero()
        {
            var light = new Light { Name = "Test" };

            double energy = light.GetEnergyUsage(5);

            Assert.AreEqual(0, energy, "Вимкнена лампа не має споживати енергію");
        }

        // Перевіряє правильність виводу PrintStatus() для увімкненої лампи
        [Test]
        public void Light_PrintStatus_WhenOn_PrintsCorrectly()
        {
            var light = new Light { Name = "Тестова лампа" };
            light.TurnOn();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                light.PrintStatus();

                Assert.That(sw.ToString(), Does.Contain("Тестова лампа: увімкнено"),
                    "PrintStatus має показувати 'увімкнено'");
            }
        }

        // Перевіряє правильність виводу PrintStatus() для вимкненої лампи
        [Test]
        public void Light_PrintStatus_WhenOff_PrintsCorrectly()
        {
            var light = new Light { Name = "Тестова лампа" };

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                light.PrintStatus();

                Assert.That(sw.ToString(), Does.Contain("Тестова лампа: вимкнено"),
                    "PrintStatus має показувати 'вимкнено'");
            }
        }
    }
}
