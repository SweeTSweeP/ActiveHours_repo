using ActiveHours.Infrastructure.ActiveHours;
using ActiveHours.Infrastructure.Input;
using ActiveHours.Infrastructure.Output;

namespace ActiveHours.Infrastructure.Services;

public static class ActiveHoursServiceFactory
{
    public static IInputPeriodsData InputPeriodsData => new InputPeriodsData();
    
    public static IOutputActiveHoursData OutputActiveHoursData => new OutputActiveHoursData();

    public static IActiveHoursCounter ActiveHoursData => new ActiveHoursCounter();
}