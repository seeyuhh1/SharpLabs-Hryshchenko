using HospitalManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementTests
{
    [TestFixture]
    public class DoctorTests
    {
        [Test]
        public void Doctor_Constructor_ShouldSetAllProperties()
        {
            // Arrange & Act
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");

            // Assert
            Assert.AreEqual(1, doctor.Id);
            Assert.AreEqual("Іванов Іван", doctor.Name);
            Assert.AreEqual("Терапевт", doctor.Specialization);
        }

        [Test]
        public void Doctor_Properties_CanBeModified()
        {
            // Arrange
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");

            // Act
            doctor.Id = 2;
            doctor.Name = "Петров Петро";
            doctor.Specialization = "Хірург";

            // Assert
            Assert.AreEqual(2, doctor.Id);
            Assert.AreEqual("Петров Петро", doctor.Name);
            Assert.AreEqual("Хірург", doctor.Specialization);
        }

        [Test]
        public void Doctor_CanCreateMultipleDoctors()
        {
            // Arrange & Act
            var doctor1 = new Doctor(1, "Іванов Іван", "Терапевт");
            var doctor2 = new Doctor(2, "Петрова Марія", "Хірург");

            // Assert
            Assert.AreNotEqual(doctor1.Id, doctor2.Id);
            Assert.AreNotEqual(doctor1.Name, doctor2.Name);
        }
    }
}
