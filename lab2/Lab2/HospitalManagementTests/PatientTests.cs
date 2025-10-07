using HospitalManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementTests
{
    [TestFixture]
    public class PatientTests
    {
        [Test]
        public void Patient_Constructor_ShouldSetAllProperties()
        {
            // Arrange & Act
            var patient = new Patient(1, "Коваленко Тарас", 35);

            // Assert
            Assert.AreEqual(1, patient.Id);
            Assert.AreEqual("Коваленко Тарас", patient.Name);
            Assert.AreEqual(35, patient.Age);
        }

        [Test]
        public void Patient_Properties_CanBeModified()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);

            // Act
            patient.Id = 2;
            patient.Name = "Шевченко Ольга";
            patient.Age = 28;

            // Assert
            Assert.AreEqual(2, patient.Id);
            Assert.AreEqual("Шевченко Ольга", patient.Name);
            Assert.AreEqual(28, patient.Age);
        }

        [Test]
        public void Patient_CanCreateMultiplePatients()
        {
            // Arrange & Act
            var patient1 = new Patient(1, "Коваленко Тарас", 35);
            var patient2 = new Patient(2, "Шевченко Ольга", 28);

            // Assert
            Assert.AreNotEqual(patient1.Id, patient2.Id);
            Assert.AreNotEqual(patient1.Name, patient2.Name);
        }

        [Test]
        public void Patient_Age_CanBeZero()
        {
            // Arrange & Act
            var patient = new Patient(1, "Немовля", 0);

            // Assert
            Assert.AreEqual(0, patient.Age);
        }
    }
}
