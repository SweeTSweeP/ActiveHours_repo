namespace ActiveHours.Infrastructure.Output;

public class OutputActiveHoursData : IOutputActiveHoursData
{
    public void ShowResult(string data) => 
        Console.WriteLine($"Active hours: {data}");
}