

namespace ProjectDatabases.Models
{
    public class Activity
    {
        // Properties
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime {  get; set; }

        // Empty constructor explictily needed because the constructor below overwrites the default
        public Activity()
        {

        }

        // Constructor
        public Activity(int activityId, string activityName, DateTime startTime, DateTime endTime)
        {
            ActivityId = activityId;
            ActivityName = activityName;
            StartTime = startTime;
            EndTime = endTime;
        }

        // Other methods
        // Returns the DateTime in the datetime-local format that HTML uses
        public string ConvertSqlToHtmlDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm");
        }
    }
}
