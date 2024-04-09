using ActiveHours.Infrastructure.Services;

namespace ActiveHours;

class Program
{
    static void Main(string[] args)
    {
        var inputData = ActiveHoursServiceFactory.InputPeriodsData;
        var activeHoursCounter = ActiveHoursServiceFactory.ActiveHoursData;
        var outputData = ActiveHoursServiceFactory.OutputActiveHoursData;
        var filePath = inputData.TryGetFilePath(args); 
        var periods = inputData.LoadPeriods(filePath);

        if (!inputData.CheckPeriodsIsInitialized(periods)) return;

        var activeHours = activeHoursCounter.CountActiveHours(periods);

        outputData.ShowResult(activeHours.ToString());
    }
}