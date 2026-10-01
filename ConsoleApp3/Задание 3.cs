class HistoryButton
{
    public event Action<string, DateTime> Clicked;
    public List<DateTime> ClickHistory = new List<DateTime>();
    public void Click(string name)
    {
        DateTime now = DateTime.Now;
        ClickHistory.Add(now);
        Clicked?.Invoke(name, now);
    }
    public void Subscribe(Action<string, DateTime> handler)
    {
        Clicked += handler;
    }
    public void Unsubscribe(Action<string, DateTime> handler)
    {
        Clicked -= handler;
    }
}
class Program
{
    static void Main(string[] args)
    {
        HistoryButton button = new HistoryButton();
        Action<string, DateTime> handler = (name, time) =>
        {
            Console.WriteLine($"Кнопка нажата: {time}. Подписчик: {name}");
        };
        button.Subscribe(handler);
        button.Click("Alexey");
        button.Unsubscribe(handler);
        button.Click("Alexey");
    }
}