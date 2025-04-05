using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface ISupervisorsRepository
    {
        List<Teacher> GetSupervisorsByActivity(int activityId);
        List<Teacher> GetNonSupervisorsByActivity(int activityId);
        void AddSupervisor(int activityId, int teacherId);
        void RemoveSupervisor(int activityId, int teacherId);

    }
}
