using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface IActivityRepository 
    {
        List<Activity> GetActivityList();
        List<Activity> GetActivityList(string searchInput);
        Activity? GetById(int ActivityId);
        void Add(Activity activity);
        void Update(Activity activity);
        void Delete(Activity activity);

    }
}