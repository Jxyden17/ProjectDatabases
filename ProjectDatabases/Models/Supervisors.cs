namespace ProjectDatabases.Models
{
    public class Supervisors
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public int TeacherId { get; set; } 
        public Teacher Teacher { get; set; }

        public Supervisors() { }
        public Supervisors(int teacherId, int activityId)
        {
            TeacherId = teacherId;
            ActivityId = activityId;
        }

        public Supervisors(int activityId, Activity activity, int teacherId, Teacher teacher)
        {
            ActivityId = activityId;
            Activity = activity;
            TeacherId = teacherId;
            Teacher = teacher;
        }
    }
}
