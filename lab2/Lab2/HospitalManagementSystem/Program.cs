using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HospitalManagementSystem
{
    // Етап 1. Створення базових класів
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public Doctor(int id, string name, string specialization)
        {
            Id = id;
            Name = name;
            Specialization = specialization;
        }
    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Patient(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }

    // Етап 2. Реалізація класів для палат і медичних записів
    public class HospitalRoom
    {
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public List<Patient> Patients { get; set; }

        public HospitalRoom(int roomNumber, int capacity)
        {
            RoomNumber = roomNumber;
            Capacity = capacity;
            Patients = new List<Patient>();
        }

        public void AddPatient(Patient patient)
        {
            if (Patients.Count < Capacity)
            {
                Patients.Add(patient);
                Console.WriteLine($"Пацієнт {patient.Name} доданий у палату №{RoomNumber}");
            }
            else
            {
                Console.WriteLine($"Палата №{RoomNumber} переповнена! Неможливо додати пацієнта.");
            }
        }
    }

    public class MedicalRecord
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public MedicalRecord(Patient patient, Doctor doctor, DateTime date, string description)
        {
            Patient = patient;
            Doctor = doctor;
            Date = date;
            Description = description;
        }
    }

    // Етап 3. Створення головного класу
    public class Hospital
    {
        public List<Doctor> Doctors { get; set; }
        public List<Patient> Patients { get; set; }
        public List<HospitalRoom> Rooms { get; set; }
        public List<MedicalRecord> Records { get; set; }

        public Hospital()
        {
            Doctors = new List<Doctor>();
            Patients = new List<Patient>();
            Rooms = new List<HospitalRoom>();
            Records = new List<MedicalRecord>();
        }

        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
            Console.WriteLine($"Лікар {doctor.Name} ({doctor.Specialization}) доданий до системи");
        }

        public void RegisterPatient(Patient patient)
        {
            Patients.Add(patient);
            Console.WriteLine($"Пацієнт {patient.Name}, {patient.Age} років, зареєстрований");
        }

        public void CreateRoom(HospitalRoom room)
        {
            Rooms.Add(room);
            Console.WriteLine($"Палата №{room.RoomNumber} створена (місткість: {room.Capacity})");
        }

        public void HospitalizePatient(int patientId, int roomNumber)
        {
            Patient patient = Patients.FirstOrDefault(p => p.Id == patientId);
            HospitalRoom room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (patient == null)
            {
                Console.WriteLine($"Пацієнт з ID {patientId} не знайдений!");
                return;
            }

            if (room == null)
            {
                Console.WriteLine($"Палата №{roomNumber} не знайдена!");
                return;
            }

            foreach (var r in Rooms)
            {
                if (r.Patients.Contains(patient))
                {
                    r.Patients.Remove(patient);
                    break;
                }
            }

            room.AddPatient(patient);
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            Records.Add(record);
            Console.WriteLine($"Медичний запис створено: {record.Patient.Name} -> {record.Doctor.Name}");
        }

        public List<MedicalRecord> GetPatientHistory(int patientId)
        {
            return Records.Where(r => r.Patient.Id == patientId).ToList();
        }

        public string GetStatistics()
        {
            int totalPatientsInRooms = Rooms.Sum(r => r.Patients.Count);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n=== СТАТИСТИКА ЛІКАРНІ ===");
            sb.AppendLine($"Кількість лікарів: {Doctors.Count}");
            sb.AppendLine($"Кількість зареєстрованих пацієнтів: {Patients.Count}");
            sb.AppendLine($"Кількість палат: {Rooms.Count}");
            sb.AppendLine($"Кількість пацієнтів у палатах: {totalPatientsInRooms}");
            sb.Append($"Кількість медичних записів: {Records.Count}\n");

            return sb.ToString();
        }
    }

    // Етап 4. Демонстрація роботи програми
    public class HospitalDemo
    {
        public void Run()
        {
            Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ ЛІКАРНЕЮ ===\n");

            Hospital hospital = new Hospital();

            Console.WriteLine("\n--- ДОДАВАННЯ ЛІКАРІВ ---");
            Doctor doc1 = new Doctor(1, "Петренко Ігор", "Терапевт");
            Doctor doc2 = new Doctor(2, "Сидорова Олена", "Хірург");
            hospital.AddDoctor(doc1);
            hospital.AddDoctor(doc2);

            Console.WriteLine("\n--- РЕЄСТРАЦІЯ ПАЦІЄНТІВ ---");
            Patient pat1 = new Patient(1, "Коваленко Тарас", 35);
            Patient pat2 = new Patient(2, "Мельник Ірина", 60);
            Patient pat3 = new Patient(3, "Іванов Денис", 19);
            hospital.RegisterPatient(pat1);
            hospital.RegisterPatient(pat2);
            hospital.RegisterPatient(pat3);

            Console.WriteLine("\n--- СТВОРЕННЯ ПАЛАТ ---");
            HospitalRoom room101 = new HospitalRoom(101, 2);
            HospitalRoom room102 = new HospitalRoom(102, 1);
            hospital.CreateRoom(room101);
            hospital.CreateRoom(room102);

            Console.WriteLine("\n--- ГОСПІТАЛІЗАЦІЯ ---");
            hospital.HospitalizePatient(pat1.Id, room101.RoomNumber);
            hospital.HospitalizePatient(pat2.Id, room101.RoomNumber);
            hospital.HospitalizePatient(pat3.Id, room102.RoomNumber);
            hospital.HospitalizePatient(1, 102);

            Console.WriteLine("\n--- МЕДИЧНІ ЗАПИСИ ---");
            MedicalRecord rec1 = new MedicalRecord(pat1, doc1, new DateTime(2025, 10, 5), "ГРВІ.");
            MedicalRecord rec2 = new MedicalRecord(pat2, doc2, new DateTime(2025, 10, 6), "Травма коліна.");
            MedicalRecord rec3 = new MedicalRecord(pat1, doc1, new DateTime(2025, 10, 7), "Покращення.");
            hospital.AddMedicalRecord(rec1);
            hospital.AddMedicalRecord(rec2);
            hospital.AddMedicalRecord(rec3);

            Console.WriteLine("\n--- ІСТОРІЯ ПАЦІЄНТА ---");
            var history = hospital.GetPatientHistory(pat1.Id);
            Console.WriteLine($"Історія пацієнта {pat1.Name}:");
            foreach (var record in history)
            {
                Console.WriteLine($"  Дата: {record.Date.ToShortDateString()}");
                Console.WriteLine($"  Лікар: {record.Doctor.Name}");
                Console.WriteLine($"  Опис: {record.Description}\n");
            }

            Console.WriteLine(hospital.GetStatistics());
        }
    }
}

// Етап 5. Ініціалізація програми
public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        HospitalManagementSystem.HospitalDemo demo = new HospitalManagementSystem.HospitalDemo();
        demo.Run();

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}