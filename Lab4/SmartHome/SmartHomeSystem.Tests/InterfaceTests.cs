using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSystem.Tests
{
    [TestFixture]
    public class InterfaceTests
    {
        // Перевіряє, що інтерфейс ISwitchable існує та має необхідні члени
        [Test]
        public void ISwitchable_InterfaceExists()
        {
            var type = typeof(ISwitchable);
            Assert.IsTrue(type.IsInterface, "ISwitchable має бути інтерфейсом");

            var turnOnMethod = type.GetMethod("TurnOn");
            var turnOffMethod = type.GetMethod("TurnOff");
            var isOnProperty = type.GetProperty("IsOn");

            Assert.IsNotNull(turnOnMethod, "ISwitchable має містити метод TurnOn()");
            Assert.IsNotNull(turnOffMethod, "ISwitchable має містити метод TurnOff()");
            Assert.IsNotNull(isOnProperty, "ISwitchable має містити властивість IsOn");
            Assert.AreEqual(typeof(bool), isOnProperty.PropertyType, "IsOn має бути типу bool");
        }

        // Перевіряє, що інтерфейс IEnergyConsumer існує та має необхідні члени
        [Test]
        public void IEnergyConsumer_InterfaceExists()
        {
            var type = typeof(IEnergyConsumer);
            Assert.IsTrue(type.IsInterface, "IEnergyConsumer має бути інтерфейсом");

            var deviceNameProperty = type.GetProperty("DeviceName");
            var powerConsumptionProperty = type.GetProperty("PowerConsumption");
            var getEnergyUsageMethod = type.GetMethod("GetEnergyUsage");

            Assert.IsNotNull(deviceNameProperty, "IEnergyConsumer має містити властивість DeviceName");
            Assert.IsNotNull(powerConsumptionProperty, "IEnergyConsumer має містити властивість PowerConsumption");
            Assert.IsNotNull(getEnergyUsageMethod, "IEnergyConsumer має містити метод GetEnergyUsage(int hours)");

            Assert.AreEqual(typeof(string), deviceNameProperty.PropertyType, "DeviceName має бути типу string");
            Assert.AreEqual(typeof(int), powerConsumptionProperty.PropertyType, "PowerConsumption має бути типу int");
            Assert.AreEqual(typeof(double), getEnergyUsageMethod.ReturnType, "GetEnergyUsage має повертати double");
        }
    }
}
