using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverySystem.Tests
{
    [TestFixture]
    public class VehicleTests
    {
        [Test]
        public void Vehicle_Constructor_InitializesFieldsCorrectly()
        {
            // Arrange & Act
            var vehicle = new Vehicle("TestBrand", 2020, 1000, 100);

            // Assert
            Assert.AreEqual("TestBrand (2020), Mileage: 1000 km", vehicle.GetInfo());
            Assert.AreEqual(100, vehicle.GetMaxSpeed());
        }

        [Test]
        public void Vehicle_GetInfo_ReturnsCorrectFormat()
        {
            // Arrange
            var vehicle = new Vehicle("Generic", 2020, 5000, 80);

            // Act
            var result = vehicle.GetInfo();

            // Assert
            Assert.AreEqual("Generic (2020), Mileage: 5000 km", result);
        }

        [Test]
        public void Vehicle_GetMaxSpeed_ReturnsMaxSpeedField()
        {
            // Arrange
            var vehicle = new Vehicle("Generic", 2020, 5000, 80);

            // Act
            var result = vehicle.GetMaxSpeed();

            // Assert
            Assert.AreEqual(80, result);
        }

        [Test]
        public void Vehicle_Move_IncreasesMileageAndPrintsMessage()
        {
            // Arrange
            var vehicle = new Vehicle("TestBrand", 2020, 1000, 80);
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            vehicle.Move(50);

            // Assert
            Assert.AreEqual("TestBrand (2020), Mileage: 1050 km", vehicle.GetInfo());
            Assert.AreEqual("TestBrand drove 50 km.\r\n", sw.ToString());
        }
    }
}
