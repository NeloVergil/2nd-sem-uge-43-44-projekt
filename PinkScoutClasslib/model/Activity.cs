namespace PinkScoutClasslib;

public class Activity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string DayOfWeek { get; set; }
    
    public string TimeSlot { get; set; }
    public int MinimumAge { get; set; }
    public int MaximumParticipantCount { get; set; }

    public List<User> Users { get; set; } = new List<User>
    { };

    public Activity(int id, string title, string description, string dayOfWeek, string timeSlot, int minimumAge, int maximumParticipantCount)
    {
        Id = id;
        Title = title;
        Description = description;
        DayOfWeek = dayOfWeek;
        TimeSlot = timeSlot;
        MinimumAge = minimumAge;
        MaximumParticipantCount = maximumParticipantCount;
    }

}