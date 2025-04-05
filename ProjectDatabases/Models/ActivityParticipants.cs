namespace ProjectDatabases.Models
{
    public class ActivityParticipants
    {
        public Activity? Activity { get; set; }
        public List<Student>? Participants { get; set; }
        public List<Student>? NonParticipants { get; set; }

        public ActivityParticipants()
        {
        }

        public ActivityParticipants(Activity activity, List<Student> participants, List<Student> nonParticipants)
        {
            Activity = activity;
            Participants = participants;
            NonParticipants = nonParticipants;
        }
    }
}
