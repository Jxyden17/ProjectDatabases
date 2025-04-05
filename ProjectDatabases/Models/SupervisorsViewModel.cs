namespace ProjectDatabases.Models
{
    public class SupervisorsViewModel
    {
        public Activity Activity { get; set; }
        public List<Teacher> Supervisors { get; set; }
        public List<Teacher> NonSupervisors { get; set; }
    }
}
