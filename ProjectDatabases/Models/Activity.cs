

namespace MvcWhatsUp.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime {  get; set; }

        // Empty constructor explictily needed because the constructor below overwrites the default
        public Activity()
        {

        }
        public Activity(int activityId, string activityName, DateTime startTime, DateTime endTime)
        {
            ActivityId = activityId;
            ActivityName = activityName;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
