using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverySystem.Tests
{
    [TestFixture]
    public class PolymorphismTests
    {
        [Test]
        public void Polymorphism_VehicleReferenceToScooter_CallsOverriddenMethods()
        {
            // Arrange
            Vehicle vehicle = new Scooter("Xiaomi", 2023, 1200, 30);

            // Act
            var info = vehicle.GetInfo();
            var speed = vehicle.GetMaxSpeed();

            // Assert
            Assert.AreEqual("Scooter: Xiaomi (2023), Battery: 100% of 30Ah", info);
            Assert.AreEqual(45.0, speed);
        }

        [Test]
        public void Polymorphism_VehicleReferenceToCar_CallsOverriddenMethods()
        {
            // Arrange
            Vehicle vehicle = new Car("Toyota", 2021, 34000, 4);

            // Act
            var info = vehicle.GetInfo();
            var speed = vehicle.GetMaxSpeed();

            // Assert
            Assert.AreEqual("Car: Toyota (2021), Doors: 4, Fuel: 50L", info);
            Assert.AreEqual(180.0, speed);
        }

        [Test]
        public void Polymorphism_VehicleReferenceToVan_CallsOverriddenMethods()
        {
            // Arrange
            Vehicle vehicle = new Van("Ford", 2020, 56000, 5, 1000);

            // Act
            var info = vehicle.GetInfo();
            var speed = vehicle.GetMaxSpeed();

            // Assert
            Assert.AreEqual("Van: Ford (2020), Doors: 5, Load: 0/1000kg, Fuel: 50L", info);
            Assert.AreEqual(140.0, speed);
        }
    }
}
