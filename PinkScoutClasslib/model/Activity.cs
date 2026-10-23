namespace PinkScoutClasslib;

public class Activity
{
    private string Title { get; set; }
    private string Description { get; set; }
    private DateTime starttime { get; set; }
    private DateTime endtime { get; set; }
    private int minimumAge { get; set; }
    private int maximumParticipantCount { get; set; }

    public List<User> Users { get; set;} = new List<User>
    { };



}