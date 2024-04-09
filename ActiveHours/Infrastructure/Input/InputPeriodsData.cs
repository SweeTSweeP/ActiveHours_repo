using System.Globalization;
using ActiveHours.Infrastructure.Entities;

namespace ActiveHours.Infrastructure.Input;

public class InputPeriodsData : IInputPeriodsData
{
    public string TryGetFilePath(string[] args) => 
        args.Length == 0 ? Constants.FILE_PATH : args[0];

    public bool CheckPeriodsIsInitialized(List<Period>? periods)
    {
        if (periods != null) return true;
        
        Console.WriteLine("Error: Check your file path or data in a file");
        
        return false;
    }

    public List<Period>? LoadPeriods(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return null;
        
        if (!File.Exists(filePath)) return null;
        
        using var reader = new StreamReader(filePath);

        var provider = new CultureInfo("ru-RU");
        
        if (!TryGetCollectionLength(reader, out var collectionLength)) return null;

        var periods = new List<Period>(collectionLength);

        for (var i = 0; i < collectionLength; i++)
        {
            if (!TryFormPeriod(reader, provider, out var period)) return null;

            periods.Add(period);
        }

        return periods;
    }

    private bool TryGetCollectionLength(StreamReader reader, out int collectionLength)
    {
        var rawCollectionLength = reader.ReadLine();

        collectionLength = 0;
        
        return !string.IsNullOrEmpty(rawCollectionLength) && int.TryParse(rawCollectionLength, out collectionLength);
    }

    private bool TryFormPeriod(StreamReader reader, CultureInfo provider, out Period period)
    {
        period = default!;
        
        if (!TryFormActiveDays(reader, out var activeDays)) return false;
        
        if (!TryFormDate(reader, provider, out var startDate)) return false;
        
        if (!TryFormDate(reader, provider, out var endDate)) return false;
        
        if (!TryFormHour(reader, out var startHour)) return false;
        
        if (!TryFormHour(reader, out var endHour)) return false;
        
        period = new Period
        {
            ActiveDays = activeDays,
            StartDate = startDate,
            EndDate = endDate,
            StartHour = startHour,
            EndHour = endHour
        };

        return true;
    }

    private bool TryFormActiveDays(StreamReader reader, out string[] activeDays)
    {
        activeDays = default!;
        
        var rawDays = reader.ReadLine();
            
        if (string.IsNullOrEmpty(rawDays))
            return false;
        
        activeDays = rawDays.Split(',');
        
        return true;
    }

    private bool TryFormDate(StreamReader reader, CultureInfo provider, out DateTime date)
    {
        date = default;
        
        var rawDate = reader.ReadLine();
            
        return !string.IsNullOrEmpty(rawDate) && DateTime.TryParse(rawDate, provider, out date);
    }

    private bool TryFormHour(StreamReader reader, out int hour)
    {
        hour = default;
        
        var rawHour = reader.ReadLine();

        return !string.IsNullOrEmpty(rawHour) && int.TryParse(rawHour, out hour);
    }
}