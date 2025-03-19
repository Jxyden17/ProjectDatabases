using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface IActivityRepository 
    {
        List<Activity> GetAll();
        List<Activity> Search(string inputSearch);
        Activity? GetById(int ActivityId);
        void Add(Activity activity);
        void Update(Activity activity);
        void Delete(Activity activity);

    }
}