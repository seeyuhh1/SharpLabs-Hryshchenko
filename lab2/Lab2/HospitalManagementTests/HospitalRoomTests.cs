using HospitalManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementTests
{
    [TestFixture]
    public class HospitalRoomTests
    {
        [Test]
        public void HospitalRoom_Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var room = new HospitalRoom(101, 2);

            // Assert
            Assert.AreEqual(101, room.RoomNumber);
            Assert.AreEqual(2, room.Capacity);
            Assert.IsNotNull(room.Patients);
            Assert.AreEqual(0, room.Patients.Count);
        }

        [Test]
        public void AddPatient_WhenRoomHasSpace_ShouldAddPatient()
        {
            // Arrange
            var room = new HospitalRoom(101, 2);
            var patient = new Patient(1, "Коваленко Тарас", 35);

            // Act
            room.AddPatient(patient);

            // Assert
            Assert.AreEqual(1, room.Patients.Count);
            Assert.Contains(patient, room.Patients);
        }

        [Test]
        public void AddPatient_WhenRoomIsFull_ShouldNotAddPatient()
        {
            // Arrange
            var room = new HospitalRoom(101, 1);
            var patient1 = new Patient(1, "Пацієнт 1", 30);
            var patient2 = new Patient(2, "Пацієнт 2", 40);

            // Act
            room.AddPatient(patient1);

            // Перенаправляємо консоль для перевірки повідомлення
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                room.AddPatient(patient2);
                string output = sw.ToString();

                // Assert
                Assert.AreEqual(1, room.Patients.Count);
                Assert.Contains(patient1, room.Patients);
                Assert.IsFalse(room.Patients.Contains(patient2));
                Assert.IsTrue(output.Contains("переповнена"));
            }

            // Відновлюємо стандартний вивід
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        [Test]
        public void AddPatient_CanAddMultiplePatients_UpToCapacity()
        {
            // Arrange
            var room = new HospitalRoom(102, 3);
            var patient1 = new Patient(1, "Пацієнт 1", 30);
            var patient2 = new Patient(2, "Пацієнт 2", 40);
            var patient3 = new Patient(3, "Пацієнт 3", 50);

            // Act
            room.AddPatient(patient1);
            room.AddPatient(patient2);
            room.AddPatient(patient3);

            // Assert
            Assert.AreEqual(3, room.Patients.Count);
            Assert.Contains(patient1, room.Patients);
            Assert.Contains(patient2, room.Patients);
            Assert.Contains(patient3, room.Patients);
        }

        [Test]
        public void HospitalRoom_WithZeroCapacity_ShouldNotAcceptPatients()
        {
            // Arrange
            var room = new HospitalRoom(103, 0);
            var patient = new Patient(1, "Пацієнт", 30);

            // Act
            room.AddPatient(patient);

            // Assert
            Assert.AreEqual(0, room.Patients.Count);
        }
    }
}
