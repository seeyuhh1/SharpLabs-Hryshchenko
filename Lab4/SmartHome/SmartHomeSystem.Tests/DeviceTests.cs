using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSystem.Tests
{
    [TestFixture]
    public class DeviceTests
    {
        // Перевіряє, що Device є абстрактним класом та реалізує ISwitchable
        [Test]
        public void Device_IsAbstractClass()
        {
            var type = typeof(Device);
            Assert.IsTrue(type.IsAbstract, "Device має бути абстрактним класом");
            Assert.IsTrue(typeof(ISwitchable).IsAssignableFrom(type), "Device має реалізувати ISwitchable");
        }

        // Перевіряє, що Device має необхідні властивості Name та IsOn
        [Test]
        public void Device_HasRequiredProperties()
        {
            var type = typeof(Device);

            var nameProperty = type.GetProperty("Name");
            var isOnProperty = type.GetProperty("IsOn");

            Assert.IsNotNull(nameProperty, "Device має містити властивість Name");
            Assert.IsNotNull(isOnProperty, "Device має містити властивість IsOn");

            Assert.AreEqual(typeof(string), nameProperty.PropertyType, "Name має бути типу string");
            Assert.AreEqual(typeof(bool), isOnProperty.PropertyType, "IsOn має бути типу bool");
        }

        // Перевіряє, що метод PrintStatus існує
        [Test]
        public void Device_HasPrintStatusMethod()
        {
            var type = typeof(Device);
            var printStatusMethod = type.GetMethod("PrintStatus");

            Assert.IsNotNull(printStatusMethod, "Device має містити метод PrintStatus()");
            Assert.AreEqual(typeof(void), printStatusMethod.ReturnType, "PrintStatus має повертати void");
        }
    }
}
