using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface IActivityRepository 
    {
        List<Activity> GetAll();
        List<Activity> Search(string searchInput);
        Activity? GetById(int ActivityId);
        void Add(Activity activity);
        void Update(Activity activity);
        void Delete(Activity activity);
        void AddStudent(int activityId, int studentNumber);
        void RemoveStudent(int activityId, int studentNumber);
    }
}