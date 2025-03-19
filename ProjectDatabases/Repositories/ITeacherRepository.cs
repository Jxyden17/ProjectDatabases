using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface ITeacherRepository
    {
        List<Teacher> GetAll();
        Teacher? GetById(int TeacherId);

        List<Teacher> Search(string inputSearch);
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(int TeacherId);

    }
}
