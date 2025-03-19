namespace ProjectDatabases.Models
{
    public class Student
    {
        public int StudentNumber { get; set; }
        public int RoomId { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }   
        public string PhoneNumber { get; set; }
        public string ClassNumber { get; set; }


        public Student()
        {

        }

        public Student(int studentNumber, int roomId, string firstName, string lastName, string phoneNumber, string classNumber)
        {
            StudentNumber = studentNumber;
            RoomId = roomId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            ClassNumber = classNumber;
        }
    }
}
