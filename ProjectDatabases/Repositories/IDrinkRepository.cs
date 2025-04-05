using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface IDrinkRepository
    {
        List<Drink> GetAll();

        Drink? GetById(int id);

    }
}
