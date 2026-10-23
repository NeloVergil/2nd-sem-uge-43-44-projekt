
namespace PinkScoutClasslib;

public class ActivityService
{
    private readonly IActivityRepository _activityRepository;

    public ActivityService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    // her henter vi ennnnn aktivitet
    public async Task<Activity> GetActivityByIdAsync(int id)
    {
        return await _activityRepository.GetByIdAsync(id);
    }

    // henter alle 
    public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
    {
        return await _activityRepository.GetAllAsync();
    }

    // opret aktiviteter og har tilføjet logik for at sikre, at slut tidspunktet ikke er tidligere end start tidspunktet og at maksimum antal deltagere er større end 0 <333
    public async Task AddActivityAsync(Activity activity)
    {
        if (activity.EndTime <= activity.StartTime)
        {
            throw new ArgumentException("Slut tidspunktet kan ikke være tidligere end start tidspunktet min lille søde ven.");
        }

        if (activity.maximumParticipantCount <= 0)
        {
            throw new ArgumentException("Maksimum antal deltagere skal være større end 0.");
        }
    
        await _activityRepository.AddAsync(activity);
    }


    // opdaterer
    public async Task UpdateActivityAsync(Activity activity)
    {
        await _activityRepository.UpdateAsync(activity);
    }

    // sletter
    public async Task DeleteActivityAsync(int id)
    {
        await _activityRepository.DeleteAsync(id);
    }

    // tilmelder
    public bool RegisterScout(int scoutId, int activityId)
    {
        return true;
    }

    // afmelder
    public bool UnregisterScout(int scoutId, int activityId)
    {
        return true;
    }
}
