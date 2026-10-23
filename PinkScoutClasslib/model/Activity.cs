namespace PinkScoutClasslib;

public class Activity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime starttime { get; set; }
    public DateTime endtime { get; set; }
    public int minimumAge { get; set; }
    public int maximumParticipantCount { get; set; }

    public List<User> Users { get; set; } = new List<User>
    { };

    public Activity(string title, string description, DateTime starttime, DateTime endtime, int minimumAge, int maximumParticipantCount)
    {
        Title = title;
        Description = description;
        this.starttime = starttime;
        this.endtime = endtime;
        this.minimumAge = minimumAge;
        this.maximumParticipantCount = maximumParticipantCount;
    }

}