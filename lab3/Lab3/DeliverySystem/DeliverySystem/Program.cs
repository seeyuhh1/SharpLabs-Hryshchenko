using System;

namespace DeliverySystem
{
    public class Vehicle
    {
        protected string brand;
        protected int year;
        protected double mileage;
        protected double maxSpeed;

        public Vehicle(string brand, int year, double mileage, double maxSpeed)
        {
            this.brand = brand;
            this.year = year;
            this.mileage = mileage;
            this.maxSpeed = maxSpeed;
        }

        public virtual string GetInfo()
        {
            return $"{brand} ({year}), Mileage: {mileage} km";
        }

        public virtual double GetMaxSpeed()
        {
            return this.maxSpeed;
        }

        public virtual void Move(double distance)
        {
            this.mileage += distance;
            Console.WriteLine($"{brand} drove {distance} km.");
        }
    }

    public class Scooter : Vehicle
    {
        private int batteryCapacity;
        private double batteryLevel;

        public Scooter(string brand, int year, double mileage, int batteryCapacity)
            : base(brand, year, mileage, 45.0)
        {
            this.batteryCapacity = batteryCapacity;
            this.batteryLevel = 100;
        }

        public override string GetInfo()
        {
            return $"Scooter: {brand} ({year}), Battery: {batteryLevel}% of {batteryCapacity}Ah";
        }

        public override void Move(double distance)
        {
            base.Move(distance);
            this.batteryLevel -= distance * 0.5;
            if (this.batteryLevel < 0)
            {
                this.batteryLevel = 0;
            }
        }

        public void Charge()
        {
            this.batteryLevel = 100;
            Console.WriteLine($"{brand} has been fully charged.");
        }
    }

public class Car : Vehicle
    {

        protected int doors;
        protected double fuelLevel;

        public Car(string brand, int year, double mileage, int doors)
            : this(brand, year, mileage, doors, 180.0)
        {
        }

        protected Car(string brand, int year, double mileage, int doors, double maxSpeed)
            : base(brand, year, mileage, maxSpeed)
        {
            this.doors = doors;
            this.fuelLevel = 50;
        }

        public override string GetInfo()
        {
            return $"Car: {brand} ({year}), Doors: {doors}, Fuel: {fuelLevel}L";
        }

        public override void Move(double distance)
        {
            base.Move(distance);
            this.fuelLevel -= distance * 0.1;
            if (this.fuelLevel < 0)
            {
                this.fuelLevel = 0;
            }
        }

        public void Refuel(double liters)
        {
            this.fuelLevel += liters;
            if (this.fuelLevel > 50)
            {
                this.fuelLevel = 50;
            }
        }
    }

    public class Van : Car
    {
        private double loadCapacity;
        private double currentLoad;

        public Van(string brand, int year, double mileage, int doors, double loadCapacity)
            : base(brand, year, mileage, doors, 140.0)
        {
            this.loadCapacity = loadCapacity;
            this.currentLoad = 0;
        }

        public override string GetInfo()
        {
            return $"Van: {brand} ({year}), Doors: {doors}, Load: {currentLoad}/{loadCapacity}kg, Fuel: {fuelLevel}L";
        }

        public void LoadCargo(double weight)
        {
            if (this.currentLoad + weight <= this.loadCapacity)
            {
                this.currentLoad += weight;
                Console.WriteLine($"{weight} kg loaded into the van.");
            }
            else
            {
                Console.WriteLine("Too heavy! Cannot load more cargo.");
            }
        }

        public void UnloadCargo()
        {
            this.currentLoad = 0;
            Console.WriteLine("Van unloaded.");
        }
    }

    public class Program
    {
        public static void Main()
        {
            Vehicle scooter = new Scooter("Xiaomi", 2023, 1200, 30);
            Vehicle car = new Car("Toyota", 2021, 34000, 4);
            Vehicle van = new Van("Ford", 2020, 56000, 5, 1000);

            Console.WriteLine(scooter.GetInfo());
            Console.WriteLine($"Max speed: {scooter.GetMaxSpeed()} km/h");
            scooter.Move(20);
            ((Scooter)scooter).Charge();

            Console.WriteLine(car.GetInfo());
            Console.WriteLine($"Max speed: {car.GetMaxSpeed()} km/h");
            car.Move(50);

            Console.WriteLine(van.GetInfo());
            Console.WriteLine($"Max speed: {van.GetMaxSpeed()} km/h");
            ((Van)van).LoadCargo(800);
            Console.WriteLine(van.GetInfo());
            ((Van)van).LoadCargo(300);
            ((Van)van).UnloadCargo();
        }
    }
}