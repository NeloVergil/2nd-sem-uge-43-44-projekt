namespace PinkScoutClasslib;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DayOfWeek { get; set; }
    public string TimeSlot { get; set; }
    public int MinimumAge { get; set; }
    public int MaxParticipantNumber { get; set; }

    // public List<User> Users { get; set; }

    public Activity(int id, string name, string description, string dayOfWeek, string timeSlot, int minimumAge, int maxParticipantNumber/*, List<User> users*/)
    {
        Id = id;
        Name = name;
        Description = description;
        DayOfWeek = dayOfWeek;
        TimeSlot = timeSlot;
        MinimumAge = minimumAge;
        MaxParticipantNumber = maxParticipantNumber;
        // Users = users;
    }

}