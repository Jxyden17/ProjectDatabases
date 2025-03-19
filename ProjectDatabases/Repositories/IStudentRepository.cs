using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(int student_number);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
    }
}
