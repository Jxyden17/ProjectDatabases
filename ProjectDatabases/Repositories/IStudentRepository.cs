using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(int student_number);
        List<Student> Search(string inputSearch);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
		List<Student> GetParticipants(int activityId);
		List<Student> GetNonParticipants(int activityId);
		List<Student> GetAssignedStudents(int roomId);
        List<Student> GetUnassignedStudents(int roomId);
		void AddStudentToDormitory(int studentNumber, int roomId);
		void RemoveStudentFromDormitory(int studentNumber, int roomId);
	}
}
