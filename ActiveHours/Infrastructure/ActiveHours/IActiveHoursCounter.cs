using ActiveHours.Infrastructure.Entities;

namespace ActiveHours.Infrastructure.ActiveHours;

public interface IActiveHoursCounter
{
    int CountActiveHours(List<Period> periods);
}