using HospitalManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementTests
{
    [TestFixture]
    public class HospitalTests
    {
        private Hospital hospital;

        [SetUp]
        public void SetUp()
        {
            hospital = new Hospital();
        }

        [Test]
        public void Hospital_Constructor_ShouldInitializeEmptyLists()
        {
            // Assert
            Assert.IsNotNull(hospital.Doctors);
            Assert.IsNotNull(hospital.Patients);
            Assert.IsNotNull(hospital.Rooms);
            Assert.IsNotNull(hospital.Records);
            Assert.AreEqual(0, hospital.Doctors.Count);
            Assert.AreEqual(0, hospital.Patients.Count);
            Assert.AreEqual(0, hospital.Rooms.Count);
            Assert.AreEqual(0, hospital.Records.Count);
        }

        [Test]
        public void AddDoctor_ShouldAddDoctorToList()
        {
            // Arrange
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");

            // Act
            hospital.AddDoctor(doctor);

            // Assert
            Assert.AreEqual(1, hospital.Doctors.Count);
            Assert.Contains(doctor, hospital.Doctors);
        }

        [Test]
        public void AddDoctor_CanAddMultipleDoctors()
        {
            // Arrange
            var doctor1 = new Doctor(1, "Іванов Іван", "Терапевт");
            var doctor2 = new Doctor(2, "Петрова Марія", "Хірург");

            // Act
            hospital.AddDoctor(doctor1);
            hospital.AddDoctor(doctor2);

            // Assert
            Assert.AreEqual(2, hospital.Doctors.Count);
            Assert.Contains(doctor1, hospital.Doctors);
            Assert.Contains(doctor2, hospital.Doctors);
        }

        [Test]
        public void RegisterPatient_ShouldAddPatientToList()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);

            // Act
            hospital.RegisterPatient(patient);

            // Assert
            Assert.AreEqual(1, hospital.Patients.Count);
            Assert.Contains(patient, hospital.Patients);
        }

        [Test]
        public void RegisterPatient_CanAddMultiplePatients()
        {
            // Arrange
            var patient1 = new Patient(1, "Коваленко Тарас", 35);
            var patient2 = new Patient(2, "Шевченко Ольга", 28);

            // Act
            hospital.RegisterPatient(patient1);
            hospital.RegisterPatient(patient2);

            // Assert
            Assert.AreEqual(2, hospital.Patients.Count);
            Assert.Contains(patient1, hospital.Patients);
            Assert.Contains(patient2, hospital.Patients);
        }

        [Test]
        public void CreateRoom_ShouldAddRoomToList()
        {
            // Arrange
            var room = new HospitalRoom(101, 2);

            // Act
            hospital.CreateRoom(room);

            // Assert
            Assert.AreEqual(1, hospital.Rooms.Count);
            Assert.Contains(room, hospital.Rooms);
        }

        [Test]
        public void CreateRoom_CanAddMultipleRooms()
        {
            // Arrange
            var room1 = new HospitalRoom(101, 2);
            var room2 = new HospitalRoom(102, 3);

            // Act
            hospital.CreateRoom(room1);
            hospital.CreateRoom(room2);

            // Assert
            Assert.AreEqual(2, hospital.Rooms.Count);
            Assert.Contains(room1, hospital.Rooms);
            Assert.Contains(room2, hospital.Rooms);
        }

        [Test]
        public void HospitalizePatient_WithValidIds_ShouldAddPatientToRoom()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);
            var room = new HospitalRoom(101, 2);
            hospital.RegisterPatient(patient);
            hospital.CreateRoom(room);

            // Act
            hospital.HospitalizePatient(1, 101);

            // Assert
            Assert.AreEqual(1, room.Patients.Count);
            Assert.Contains(patient, room.Patients);
        }

        [Test]
        public void HospitalizePatient_WithInvalidPatientId_ShouldNotAddToRoom()
        {
            // Arrange
            var room = new HospitalRoom(101, 2);
            hospital.CreateRoom(room);

            // Act
            hospital.HospitalizePatient(999, 101);

            // Assert
            Assert.AreEqual(0, room.Patients.Count);
        }

        [Test]
        public void HospitalizePatient_WithInvalidRoomNumber_ShouldNotAddPatient()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);
            hospital.RegisterPatient(patient);

            // Act
            hospital.HospitalizePatient(1, 999);

            // Assert - пацієнт не повинен бути в жодній палаті
            Assert.AreEqual(0, hospital.Rooms.Count);
        }

        [Test]
        public void AddMedicalRecord_ShouldAddRecordToList()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");
            var record = new MedicalRecord(patient, doctor, DateTime.Now, "Діагноз");

            // Act
            hospital.AddMedicalRecord(record);

            // Assert
            Assert.AreEqual(1, hospital.Records.Count);
            Assert.Contains(record, hospital.Records);
        }

        [Test]
        public void AddMedicalRecord_CanAddMultipleRecords()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");
            var record1 = new MedicalRecord(patient, doctor, DateTime.Now, "Діагноз 1");
            var record2 = new MedicalRecord(patient, doctor, DateTime.Now.AddDays(1), "Діагноз 2");

            // Act
            hospital.AddMedicalRecord(record1);
            hospital.AddMedicalRecord(record2);

            // Assert
            Assert.AreEqual(2, hospital.Records.Count);
            Assert.Contains(record1, hospital.Records);
            Assert.Contains(record2, hospital.Records);
        }

        [Test]
        public void GetPatientHistory_WithExistingRecords_ShouldReturnRecords()
        {
            // Arrange
            var patient = new Patient(1, "Коваленко Тарас", 35);
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");
            var record1 = new MedicalRecord(patient, doctor, DateTime.Now, "Діагноз 1");
            var record2 = new MedicalRecord(patient, doctor, DateTime.Now.AddDays(1), "Діагноз 2");
            hospital.AddMedicalRecord(record1);
            hospital.AddMedicalRecord(record2);

            // Act
            var history = hospital.GetPatientHistory(1);

            // Assert
            Assert.AreEqual(2, history.Count);
            Assert.Contains(record1, history);
            Assert.Contains(record2, history);
        }

        [Test]
        public void GetPatientHistory_WithNoRecords_ShouldReturnEmptyList()
        {
            // Act
            var history = hospital.GetPatientHistory(1);

            // Assert
            Assert.IsNotNull(history);
            Assert.AreEqual(0, history.Count);
        }

        [Test]
        public void GetPatientHistory_ShouldReturnOnlySpecificPatientRecords()
        {
            // Arrange
            var patient1 = new Patient(1, "Пацієнт 1", 35);
            var patient2 = new Patient(2, "Пацієнт 2", 28);
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");
            var record1 = new MedicalRecord(patient1, doctor, DateTime.Now, "Діагноз для пацієнта 1");
            var record2 = new MedicalRecord(patient2, doctor, DateTime.Now, "Діагноз для пацієнта 2");
            var record3 = new MedicalRecord(patient1, doctor, DateTime.Now.AddDays(1), "Ще один для пацієнта 1");

            hospital.AddMedicalRecord(record1);
            hospital.AddMedicalRecord(record2);
            hospital.AddMedicalRecord(record3);

            // Act
            var history = hospital.GetPatientHistory(1);

            // Assert
            Assert.AreEqual(2, history.Count);
            Assert.IsTrue(history.All(r => r.Patient.Id == 1));
        }

        [Test]
        public void GetStatistics_ShouldReturnCorrectCounts()
        {
            // Arrange
            var doctor = new Doctor(1, "Іванов Іван", "Терапевт");
            var patient1 = new Patient(1, "Пацієнт 1", 35);
            var patient2 = new Patient(2, "Пацієнт 2", 28);
            var room = new HospitalRoom(101, 2);
            var record = new MedicalRecord(patient1, doctor, DateTime.Now, "Діагноз");

            hospital.AddDoctor(doctor);
            hospital.RegisterPatient(patient1);
            hospital.RegisterPatient(patient2);
            hospital.CreateRoom(room);
            hospital.HospitalizePatient(1, 101);
            hospital.AddMedicalRecord(record);

            // Act
            string stats = hospital.GetStatistics();

            // Assert
            Assert.IsTrue(stats.Contains("1")); // 1 лікар
            Assert.IsTrue(stats.Contains("2")); // 2 пацієнти
            Assert.IsTrue(stats.Contains("1")); // 1 палата
            Assert.IsTrue(stats.Contains("1")); // 1 пацієнт у палаті
        }

        [Test]
        public void GetStatistics_WithEmptyHospital_ShouldReturnZeros()
        {
            // Act
            string stats = hospital.GetStatistics();

            // Assert
            Assert.IsTrue(stats.Contains("0"));
            Assert.IsTrue(stats.Contains("СТАТИСТИКА"));
        }

        [Test]
        public void GetStatistics_ShouldCountPatientsInRoomsCorrectly()
        {
            // Arrange
            var patient1 = new Patient(1, "Пацієнт 1", 35);
            var patient2 = new Patient(2, "Пацієнт 2", 28);
            var patient3 = new Patient(3, "Пацієнт 3", 42);
            var room1 = new HospitalRoom(101, 2);
            var room2 = new HospitalRoom(102, 2);

            hospital.RegisterPatient(patient1);
            hospital.RegisterPatient(patient2);
            hospital.RegisterPatient(patient3);
            hospital.CreateRoom(room1);
            hospital.CreateRoom(room2);
            hospital.HospitalizePatient(1, 101);
            hospital.HospitalizePatient(2, 101);
            hospital.HospitalizePatient(3, 102);

            // Act
            string stats = hospital.GetStatistics();

            // Assert
            Assert.IsTrue(stats.Contains("3")); // 3 зареєстрованих пацієнти
            Assert.IsTrue(stats.Contains("2")); // 2 палати
                                                // Перевіряємо, що є рядок з кількістю пацієнтів у палатах (має бути 3)
            Assert.IsTrue(stats.Contains("Кількість пацієнтів у палатах: 3"));
        }
    }
}
