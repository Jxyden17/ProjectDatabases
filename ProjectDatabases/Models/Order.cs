namespace ProjectDatabases.Models
{
    public class Order
    {
        public List<Drink> Drinks { get; set; }
        public List<Student> Students { get; set; }

        public int SelectedStudentId { get; set; }
        public int SelectedDrinkId { get; set; }
        public int DrinkAmount { get; set; }

        public string? ConfirmationMessage { get; set; }

        public string? ErrorMessage { get; set; }

        public Order()
        {
        }

        public Order(List<Drink> drinks, List<Student> students)
        {
            Drinks = drinks;
            Students = students;
        }
    }
}
