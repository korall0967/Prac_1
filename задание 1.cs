public enum TrafficLightColor
{
    Red,
    Yellow,
    Green
}

public class TrafficLight
{
    // Событие, которое будет вызываться при изменении цвета
    public event Action<TrafficLightColor> LightChanged;

    private TrafficLightColor _currentColor;
    private readonly int[] _timings = { 3, 1, 3, 1 }; // Тайминги для Red, Yellow, Green, Yellow

    public TrafficLight()
    {
        _currentColor = TrafficLightColor.Red; // Начальный цвет
    }
    private void ChangeColor()
    {
        var initialColor = _currentColor;

        switch (_currentColor)
        {
            case TrafficLightColor.Red:
                _currentColor = TrafficLightColor.Yellow;
                break;
            case TrafficLightColor.Yellow:
                _currentColor = _previousColor == TrafficLightColor.Red
                    ? TrafficLightColor.Green
                    : TrafficLightColor.Red;
                break;
            case TrafficLightColor.Green:
                _currentColor = TrafficLightColor.Yellow;
                break;
        }

        _previousColor = initialColor;
        LightChanged?.Invoke(_currentColor);
    }

    private TrafficLightColor _previousColor;

    public void Run(int cycles = 3)
    {
        for (int i = 0; i < cycles; i++)
        {
            // Полный цикл: Red -> Yellow -> Green -> Yellow -> Red
            foreach (var timing in _timings)
            {
                ChangeColor();
                Thread.Sleep(timing * 1000); // Пауза в секундах
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var trafficLight = new TrafficLight();

        // Подписываемся на событие
        trafficLight.LightChanged += color =>
        {
            Console.ForegroundColor = GetConsoleColor(color);
            Console.WriteLine($"Светофор: {color}");
            Console.ResetColor();
        };

        Console.WriteLine("Запуск светофора на 3 цикла:");
        trafficLight.Run();
    }

    private static ConsoleColor GetConsoleColor(TrafficLightColor color)
    {
        return color switch
        {
            TrafficLightColor.Red => ConsoleColor.Red,
            TrafficLightColor.Yellow => ConsoleColor.Yellow,
            TrafficLightColor.Green => ConsoleColor.Green,
            _ => ConsoleColor.White
        };
    }
}
