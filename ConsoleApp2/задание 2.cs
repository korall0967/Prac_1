public class TemperatureChangedEventsArgs: EventArgs
{
    public double Temperature {  get; }
    public string Message { get; }
    public TemperatureChangedEventsArgs(double Temperature, string Message)
    {
        this.Temperature = Temperature;
        this.Message = Message;
    }
}

public class SmartTermostat
{
    public event EventHandler<TemperatureChangedEventsArgs> TemperatureAlert;
    public void UpdateTemperature(double Temperature)
    {
        if (Temperature > 30)
        {
            var args = new TemperatureChangedEventsArgs(Temperature, "Hot");
            TemperatureAlert?.Invoke(this, args);
        }
        else if (Temperature < 15)
        {
            var args = new TemperatureChangedEventsArgs(Temperature, "Cold");
            TemperatureAlert?.Invoke(this, args);
        }
        else if (Temperature < 22 && Temperature > 18)
        {
            var args = new TemperatureChangedEventsArgs(Temperature, "Comfort");
            TemperatureAlert?.Invoke(this, args);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var Termostat = new SmartTermostat();
        Termostat.TemperatureAlert += (sender, e) =>
        {
            Console.WriteLine($"Temperature: {e.Temperature}, is {e.Message}");
        };
        Termostat.UpdateTemperature(35);
    }
}