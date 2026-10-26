namespace PinkScoutClasslib;

public class Activity
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string DayOfWeek { get; set; }
    
    public string TimeSlot { get; set; }
    public int minimumAge { get; set; }
    public int maximumParticipantCount { get; set; }

    public List<User> Users { get; set; } = new List<User>
    { };

    public Activity(string id, string title, string description, string dayOfWeek, string timeSlot, int minimumAge, int maximumParticipantCount)
    {
        Id = id;
        Title = title;
        Description = description;
        DayOfWeek = dayOfWeek;
        TimeSlot = timeSlot;
        this.minimumAge = minimumAge;
        this.maximumParticipantCount = maximumParticipantCount;
    }

}