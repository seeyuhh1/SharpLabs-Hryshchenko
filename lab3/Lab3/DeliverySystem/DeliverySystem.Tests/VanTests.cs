using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverySystem.Tests
{
    [TestFixture]
    public class VanTests
    {
        [Test]
        public void Van_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            var van = new Van("Ford", 2020, 56000, 5, 1000);

            // Assert
            Assert.AreEqual("Van: Ford (2020), Doors: 5, Load: 0/1000kg, Fuel: 50L", van.GetInfo());
            Assert.AreEqual(140.0, van.GetMaxSpeed());
        }

        [Test]
        public void Van_GetInfo_ReturnsCorrectFormat()
        {
            // Arrange
            var van = new Van("Ford", 2020, 56000, 5, 1000);

            // Act
            var result = van.GetInfo();

            // Assert
            Assert.AreEqual("Van: Ford (2020), Doors: 5, Load: 0/1000kg, Fuel: 50L", result);
        }

        [Test]
        public void Van_GetMaxSpeed_Returns140()
        {
            // Arrange
            var van = new Van("Ford", 2020, 56000, 5, 1000);

            // Act
            var result = van.GetMaxSpeed();

            // Assert
            Assert.AreEqual(140.0, result);
        }

        [Test]
        public void Van_LoadCargo_SuccessfullyLoadsWhenWithinCapacity()
        {
            // Arrange
            var van = new Van("Ford", 2020, 56000, 5, 1000);
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            van.LoadCargo(800);

            // Assert
            Assert.AreEqual("Van: Ford (2020), Doors: 5, Load: 800/1000kg, Fuel: 50L", van.GetInfo());
            Assert.AreEqual("800 kg loaded into the van.\r\n", sw.ToString());
        }

        [Test]
        public void Van_LoadCargo_RejectsWhenExceedsCapacity()
        {
            // Arrange
            var van = new Van("Ford", 2020, 56000, 5, 1000);
            van.LoadCargo(800);
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            van.LoadCargo(300);

            // Assert
            Assert.AreEqual("Van: Ford (2020), Doors: 5, Load: 800/1000kg, Fuel: 50L", van.GetInfo());
            Assert.AreEqual("Too heavy! Cannot load more cargo.\r\n", sw.ToString());
        }

        [Test]
        public void Van_LoadCargo_AcceptsExactCapacity()
        {
            // Arrange
            var van = new Van("Ford", 2020, 56000, 5, 1000);
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            van.LoadCargo(1000);

            // Assert
            Assert.AreEqual("Van: Ford (2020), Doors: 5, Load: 1000/1000kg, Fuel: 50L", van.GetInfo());
            Assert.AreEqual("1000 kg loaded into the van.\r\n", sw.ToString());
        }

        [Test]
        public void Van_UnloadCargo_RemovesAllCargo()
        {
            // Arrange
            var van = new Van("Ford", 2020, 56000, 5, 1000);
            van.LoadCargo(800);
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            van.UnloadCargo();

            // Assert
            Assert.AreEqual("Van: Ford (2020), Doors: 5, Load: 0/1000kg, Fuel: 50L", van.GetInfo());
            Assert.AreEqual("Van unloaded.\r\n", sw.ToString());
        }

        [Test]
        public void Van_InheritsFromCar_CanUseCarMethods()
        {
            // Arrange
            var van = new Van("Ford", 2020, 56000, 5, 1000);
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            van.Move(100);
            van.Refuel(20);

            // Assert
            Assert.AreEqual("Van: Ford (2020), Doors: 5, Load: 0/1000kg, Fuel: 50L", van.GetInfo());
        }
    }
}
