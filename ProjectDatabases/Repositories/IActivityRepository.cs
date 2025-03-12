using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface IActivityRepository
    {
        List<Activity> GetAll();
        Activity? GetById(int ActivityId);
        void Add(Activity activity);
        void Update(Activity activity);
        void Delete(Activity activity);

    }
}
