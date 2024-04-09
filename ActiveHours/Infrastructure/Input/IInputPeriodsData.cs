using ActiveHours.Infrastructure.Entities;

namespace ActiveHours.Infrastructure.Input;

public interface IInputPeriodsData
{
    string TryGetFilePath(string[] args);
    bool CheckPeriodsIsInitialized(List<Period>? periods);
    List<Period>? LoadPeriods(string? filePath);
}