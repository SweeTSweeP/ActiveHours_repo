using System.Diagnostics;
using ActiveHours.Infrastructure.Entities;

namespace ActiveHours.Infrastructure.ActiveHours;

public class ActiveHoursCounter : IActiveHoursCounter
{
    public int CountActiveHours(List<Period> periods)
    {
        var timer = new Stopwatch();
        
        timer.Start();

        var activeHours = 0;
        var array = Enumerable.Repeat(false, 366 * 24).ToArray();
        var decomposedPeriodsByYear = DecomposePeriodsByYear(periods);

        foreach (var year in decomposedPeriodsByYear.Keys)
        {
            if (!decomposedPeriodsByYear.TryGetValue(year, out var yearPeriods)) continue;

            foreach (var period in yearPeriods)
                for (var dateTime = period.StartDate; dateTime <= period.EndDate; dateTime = dateTime.AddDays(1))
                {
                    if (!period.ActiveDays.Contains(dateTime.DayOfWeek.ToString().ToLower())) continue;

                    var hourInYear =
                        (dateTime - new DateTime(period.StartDate.Year, 1, 1)).TotalHours + period.StartHour + 1;

                    for (var i = 0; i < period.EndHour - period.StartHour; i++) 
                        array[(int)hourInYear + i] = true;
                }

            activeHours += array.Count(s => s);
            array = Enumerable.Repeat(false, 366 * 24).ToArray();
        }
        
        timer.Stop();

        return activeHours;
    }

    private Dictionary<int, List<Period>> DecomposePeriodsByYear(List<Period> periods)
    {
        var yearPeriods = new Dictionary<int, List<Period>>();

        foreach (var period in periods)
        {
            if (period.StartDate.Year != period.EndDate.Year)
            {
                for (var year = period.StartDate.Year; year <= period.EndDate.Year; year++)
                {
                    DateTime startDate;
                    DateTime endDate;

                    if (year == period.StartDate.Year)
                    {
                        startDate = period.StartDate;
                        endDate = new DateTime(period.StartDate.Year, 12, 31);
                    }
                    else if (year == period.EndDate.Year)
                    {
                        startDate = new DateTime(period.EndDate.Year, 1, 1);
                        endDate = period.EndDate;
                    }
                    else
                    {
                        startDate = new DateTime(year, 1, 1);
                        endDate = new DateTime(year, 12, 31);
                    }
                    
                    var newPeriod = new Period
                    {
                        ActiveDays = period.ActiveDays,
                        StartDate = startDate,
                        EndDate = endDate,
                        StartHour = period.StartHour,
                        EndHour = period.EndHour
                    };

                    AddYearPeriod(newPeriod);
                }
            }
            else
            {
                AddYearPeriod(period);
            }
        }

        return yearPeriods;

        void AddYearPeriod(Period? period)
        {
            if (period == null) return;
            
            if (yearPeriods.TryGetValue(period.StartDate.Year, out var foundListPeriod))
                foundListPeriod.Add(period);
            else
                yearPeriods.Add(period.StartDate.Year, [period]);
        }
    }
}