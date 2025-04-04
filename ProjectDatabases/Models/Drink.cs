namespace ProjectDatabases.Models
{
    public class Drink
    {
        public int DrinkId {  get; set; }
        public string Name { get; set; }
        public bool IsAlcoholic { get; set; }
        public int Stock {  get; set; }

        public Drink()
        {
        }

        public Drink(int drinkId, string name, bool isAlcoholic, int stock)
        {
            DrinkId = drinkId;
            Name = name;
            IsAlcoholic = isAlcoholic;
            Stock = stock;
        }
    }
}
