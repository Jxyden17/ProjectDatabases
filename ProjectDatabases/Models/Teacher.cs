namespace ProjectDatabases.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public int RoomId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        public Teacher  () { }
        public Teacher(int teacherId, int roomId, string firstName, string lastName, string phoneNumber, int age)
        {
            TeacherId = teacherId;
            RoomId = roomId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Age = age;
        }
    }
}
