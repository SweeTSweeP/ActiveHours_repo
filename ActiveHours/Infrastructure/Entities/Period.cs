namespace ActiveHours.Infrastructure.Entities;

public class Period
{
    public string[] ActiveDays { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StartHour { get; set; }
    public int EndHour { get; set; }
}