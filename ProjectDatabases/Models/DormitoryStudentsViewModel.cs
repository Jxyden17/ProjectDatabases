
namespace ProjectDatabases.Models
{
	public class DormitoryStudentsViewModel
	{
		public Room? Room { get; set; }
		public List<Student>? AssignedStudents { get; set; }
		public List<Student>? UnassignedStudents { get; set; }

		public DormitoryStudentsViewModel() { }
		public DormitoryStudentsViewModel(Room room, List<Student> assignedStudents, List<Student> unassignedStudents)
		{
			Room = room;
			AssignedStudents = assignedStudents;
			UnassignedStudents = unassignedStudents;
		}
	}
}
