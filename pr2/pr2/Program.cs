using System;

namespace DesignPatternsTasks
{
    // Завдання 1
    namespace SmartHome
    {
        public class TemperatureSensor
        {
            public event Action<float> OnTemperatureChanged;
            private float _temperature;

            public void SetTemperature(float newTemperature)
            {
                Console.WriteLine($"\n[Датчик] Температура змінилася на {newTemperature}°C");
                _temperature = newTemperature;
                OnTemperatureChanged?.Invoke(_temperature);
            }
        }

        public class Display
        {
            public void Update(float temperature) =>
                Console.WriteLine($"[Display] Поточна температура: {temperature}°C");
        }

        public class AirConditioner
        {
            public void Update(float temperature)
            {
                if (temperature < 17) Console.WriteLine("[AirConditioner] Режим: УВІМКНЕНО ОБІГРІВ");
                else if (temperature >= 17 && temperature <= 25) Console.WriteLine("[AirConditioner] Режим: ВИМКНЕНО (комфортна температура)");
                else if (temperature > 25) Console.WriteLine("[AirConditioner] Режим: УВІМКНЕНО ОХОЛОДЖЕННЯ");
            }
        }

        public class SecuritySystem
        {
            public void Update(float temperature)
            {
                if (temperature > 40) Console.WriteLine("[SecuritySystem] УВАГА! Перегрів системи!");
                else if (temperature < 5) Console.WriteLine("[SecuritySystem] ПОПЕРЕДЖЕННЯ! Ризик замерзання систем!");
            }
        }
    }

    // Завдання 2
    namespace GameDev
    {
        public class Player
        {
            public event Action<int, int> OnDamageTaken;
            public int HP { get; private set; } = 100;

            public void TakeDamage(int damage)
            {
                Console.WriteLine($"\n[Player] Отримано {damage} урону!");
                HP -= damage;
                if (HP < 0) HP = 0;
                OnDamageTaken?.Invoke(damage, HP);
            }
        }

        public class UIHealthBar
        {
            public void UpdateUI(int damage, int currentHp) =>
                Console.WriteLine($"[UIHealthBar] Поточне HP: {currentHp}/100");
        }

        public class SoundSystem
        {
            public void PlaySound(int damage, int currentHp)
            {
                Console.WriteLine("[SoundSystem] *Відтворення звуку отримання урону* (Oof!)");
                if (currentHp <= 20 && currentHp > 0) Console.WriteLine("[SoundSystem] *Відтворення звуку КРИТИЧНОГО СТАНУ* (Heartbeat sound)");
            }
        }

        public class AchievementSystem
        {
            private bool _halfHealthUnlocked = false;
            private bool _firstDeathUnlocked = false;

            public void CheckAchievements(int damage, int currentHp)
            {
                if (currentHp <= 50 && !_halfHealthUnlocked)
                {
                    Console.WriteLine("[AchievementSystem] ДОСЯГНЕННЯ РОЗБЛОКОВАНО: \"Half Health\"!");
                    _halfHealthUnlocked = true;
                }
                if (currentHp <= 0 && !_firstDeathUnlocked)
                {
                    Console.WriteLine("[AchievementSystem] ДОСЯГНЕННЯ РОЗБЛОКОВАНО: \"First Death\"!");
                    _firstDeathUnlocked = true;
                }
            }
        }

        public class GameLogger
        {
            public void LogDamage(int damage, int currentHp) =>
                Console.WriteLine($"[GameLogger] LOG: Гравець отримав {damage} dmg. Залишилось {currentHp} HP.");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("============= ЗАВДАННЯ 1: КЛІМАТ-КОНТРОЛЬ =============");
            RunSmartHomeTask();

            Console.WriteLine("\n\n============= ЗАВДАННЯ 2: ОТРИМАННЯ УРОНУ =============");
            RunGameDevTask();

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        // Завдання 1
        static void RunSmartHomeTask()
        {
            var sensor = new SmartHome.TemperatureSensor();
            var display = new SmartHome.Display();
            var ac = new SmartHome.AirConditioner();
            var security = new SmartHome.SecuritySystem();

            sensor.OnTemperatureChanged += display.Update;
            sensor.OnTemperatureChanged += ac.Update;
            sensor.OnTemperatureChanged += security.Update;

            sensor.SetTemperature(20f);
            sensor.SetTemperature(30f);
            sensor.SetTemperature(10f);
            sensor.SetTemperature(45f);
            sensor.SetTemperature(2f);
        }

        // Завдання 2
        static void RunGameDevTask()
        {
            var player = new GameDev.Player();
            var ui = new GameDev.UIHealthBar();
            var audio = new GameDev.SoundSystem();
            var achievements = new GameDev.AchievementSystem();
            var logger = new GameDev.GameLogger();

            player.OnDamageTaken += ui.UpdateUI;
            player.OnDamageTaken += audio.PlaySound;
            player.OnDamageTaken += achievements.CheckAchievements;
            player.OnDamageTaken += logger.LogDamage;

            player.TakeDamage(20);
            player.TakeDamage(40);
            player.TakeDamage(25);
            player.TakeDamage(20);
        }
    }
}