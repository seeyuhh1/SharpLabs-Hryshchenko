using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace SmartHomeSystem
{
    public interface ISwitchable
    {
        void TurnOn();
        void TurnOff();
        bool IsOn { get; }
    }

    public interface IEnergyConsumer
    {
        string DeviceName { get; }
        int PowerConsumption { get; }
        double GetEnergyUsage(int hours);
    }

    public abstract class Device : ISwitchable
    {
        public string Name { get; set; }
        public bool IsOn { get; protected set; }

        public abstract void TurnOn();
        public abstract void TurnOff();

        public void PrintStatus()
        {
            string status = IsOn ? "увімкнено" : "вимкнено";
            Console.WriteLine($"{Name}: {status}");
        }
    }

    public class Light : Device, IEnergyConsumer
    {
        public string DeviceName => Name;
        public int PowerConsumption => 60;

        public override void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{Name} засвітилася.");
        }

        public override void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{Name} вимкнена.");
        }

        public double GetEnergyUsage(int hours)
        {
            if (!IsOn)
            {
                return 0;
            }
            return (double)PowerConsumption * hours / 1000.0;
        }
    }

    public class AirConditioner : Device, IEnergyConsumer
    {
        public string DeviceName => Name;
        public int PowerConsumption => 2000;

        public override void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{Name} почав охолодження.");
        }

        public override void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{Name} зупинено.");
        }

        public double GetEnergyUsage(int hours)
        {
            if (!IsOn)
            {
                return 0;
            }
            return (double)PowerConsumption * hours / 1000.0;
        }
    }

    public class CoffeeMachine : Device, IEnergyConsumer
    {
        public string DeviceName => Name;
        public int PowerConsumption => 1000;

        public override void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{Name} почала готувати каву.");
        }

        public override void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{Name} завершила роботу.");
        }

        public double GetEnergyUsage(int hours)
        {
            if (!IsOn)
            {
                return 0;
            }
            return (double)PowerConsumption * hours / 1000.0;
        }
    }

    public class MotionSensor : Device
    {
        public override void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{Name} активовано.");
        }

        public override void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{Name} деактивовано.");
        }
    }

    public class SmartHomeController
    {
        private List<ISwitchable> _switchableDevices = new List<ISwitchable>();
        private List<IEnergyConsumer> _energyConsumers = new List<IEnergyConsumer>();

        public void AddDevice(ISwitchable device)
        {
            _switchableDevices.Add(device);
        }

        public void AddEnergyDevice(IEnergyConsumer device)
        {
            _energyConsumers.Add(device);
        }

        public void TurnAllOn()
        {
            foreach (var device in _switchableDevices)
            {
                device.TurnOn();
            }
        }

        public void TurnAllOff()
        {
            foreach (var device in _switchableDevices)
            {
                device.TurnOff();
            }
        }

        public void ShowEnergyReport(int hours)
        {
            Console.WriteLine($"\nЗвіт про споживання енергії за {hours} год:");

            double totalEnergy = 0;

            foreach (var device in _energyConsumers)
            {
                double energy = device.GetEnergyUsage(hours);
                totalEnergy += energy;

                Console.WriteLine($"{device.DeviceName}: {energy:F2} кВт·год (потужність: {device.PowerConsumption} Вт)");
            }

            double cost = totalEnergy * 4;

            Console.WriteLine($"Загальне споживання: {totalEnergy:F2} кВт·год");
            Console.WriteLine($"Вартість (~4 грн/кВт·год): {cost:F2} грн\n");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk-UA");

            var controller = new SmartHomeController();

            var lamp = new Light { Name = "Лампа у вітальні" };
            var ac = new AirConditioner { Name = "Кондиціонер у спальні" };
            var coffee = new CoffeeMachine { Name = "Кавомашина на кухні" };
            var motion = new MotionSensor { Name = "Датчик руху у коридорі" };

            controller.AddDevice(lamp);
            controller.AddDevice(ac);
            controller.AddDevice(coffee);
            controller.AddDevice(motion);

            controller.AddEnergyDevice(lamp);
            controller.AddEnergyDevice(ac);
            controller.AddEnergyDevice(coffee);

            controller.TurnAllOn();

            Console.WriteLine();
            lamp.PrintStatus();
            ac.PrintStatus();
            coffee.PrintStatus();
            motion.PrintStatus();

            controller.ShowEnergyReport(5);

            controller.TurnAllOff();
        }
    }
}