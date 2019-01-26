namespace BehaviorTracker.Repository.Implementations
{
    public class BaseRepository
    {
        internal BehaviorTrackerDatabaseContext _behaviorTrackerDatabaseContext;

        public BaseRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext)
        {
            _behaviorTrackerDatabaseContext = behaviorTrackerDatabaseContext;
        }
    }
}