using HospitalManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementTests
{
    [TestFixture]
    public class MedicalRecordTests
    {
        [Test]
        public void MedicalRecord_Constructor_ShouldSetAllProperties()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");
            var date = new DateTime(2025, 9, 30);
            var description = "Грип, призначено медикаменти";

            // Act
            var record = new MedicalRecord(patient, doctor, date, description);

            // Assert
            Assert.AreEqual(patient, record.Patient);
            Assert.AreEqual(doctor, record.Doctor);
            Assert.AreEqual(date, record.Date);
            Assert.AreEqual(description, record.Description);
        }

        [Test]
        public void MedicalRecord_Properties_CanBeModified()
        {
            // Arrange
            var patient1 = new Patient(1, "Коваленко Тарас", 35);
            var doctor1 = new Doctor(1, "Іванов Іван", "Терапевт");
            var record = new MedicalRecord(patient1, doctor1, DateTime.Now, "Опис 1");

            var patient2 = new Patient(2, "Шевченко Ольга", 28);
            var doctor2 = new Doctor(2, "Петрова Марія", "Хірург");
            var newDate = DateTime.Now.AddDays(1);

            // Act
            record.Patient = patient2;
            record.Doctor = doctor2;
            record.Date = newDate;
            record.Description = "Новий опис";

            // Assert
            Assert.AreEqual(patient2, record.Patient);
            Assert.AreEqual(doctor2, record.Doctor);
            Assert.AreEqual(newDate, record.Date);
            Assert.AreEqual("Новий опис", record.Description);
        }

        [Test]
        public void MedicalRecord_CanCreateMultipleRecords()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);
            var doctor1 = new Doctor(1, "Іванов Іван", "Терапевт");
            var doctor2 = new Doctor(2, "Петрова Марія", "Хірург");

            // Act
            var record1 = new MedicalRecord(patient, doctor1, DateTime.Now, "Візит 1");
            var record2 = new MedicalRecord(patient, doctor2, DateTime.Now.AddDays(1), "Візит 2");

            // Assert
            Assert.AreNotEqual(record1.Doctor, record2.Doctor);
            Assert.AreNotEqual(record1.Description, record2.Description);
        }
    }
}
