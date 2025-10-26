using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverySystem.Tests
{
    [TestFixture]
    public class CarTests
    {
        [Test]
        public void Car_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            var car = new Car("Toyota", 2021, 34000, 4);

            // Assert
            Assert.AreEqual("Car: Toyota (2021), Doors: 4, Fuel: 50L", car.GetInfo());
            Assert.AreEqual(180.0, car.GetMaxSpeed());
        }

        [Test]
        public void Car_GetInfo_ReturnsCorrectFormat()
        {
            // Arrange
            var car = new Car("Toyota", 2021, 34000, 4);

            // Act
            var result = car.GetInfo();

            // Assert
            Assert.AreEqual("Car: Toyota (2021), Doors: 4, Fuel: 50L", result);
        }

        [Test]
        public void Car_GetMaxSpeed_Returns180()
        {
            // Arrange
            var car = new Car("Toyota", 2021, 34000, 4);

            // Act
            var result = car.GetMaxSpeed();

            // Assert
            Assert.AreEqual(180.0, result);
        }

        [Test]
        public void Car_Move_DecreasesFuelLevel()
        {
            // Arrange
            var car = new Car("Toyota", 2021, 34000, 4);
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            car.Move(50);

            // Assert
            Assert.AreEqual("Car: Toyota (2021), Doors: 4, Fuel: 45L", car.GetInfo());
        }

        [Test]
        public void Car_Move_FuelLevelNotBelowZero()
        {
            // Arrange
            var car = new Car("Toyota", 2021, 34000, 4);

            // Act
            car.Move(600); // Виснажити паливо

            // Assert
            Assert.AreEqual("Car: Toyota (2021), Doors: 4, Fuel: 0L", car.GetInfo());
        }

        [Test]
        public void Car_Refuel_AddsFuel()
        {
            // Arrange
            var car = new Car("Toyota", 2021, 34000, 4);
            car.Move(100); // Використати трохи палива

            // Act
            car.Refuel(20);

            // Assert
            Assert.AreEqual("Car: Toyota (2021), Doors: 4, Fuel: 50L", car.GetInfo());
        }

        [Test]
        public void Car_Refuel_NotAbove50()
        {
            // Arrange
            var car = new Car("Toyota", 2021, 34000, 4);

            // Act
            car.Refuel(100); // Спроба переповнити бак

            // Assert
            Assert.AreEqual("Car: Toyota (2021), Doors: 4, Fuel: 50L", car.GetInfo());
        }
    }
}
