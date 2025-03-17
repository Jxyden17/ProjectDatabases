using SomerenMVC.Models;

namespace SomerenMVC.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetById(int UserId);
        void Add(User user);
        void Update(User user);
        void Delete(User user);

    }
}
