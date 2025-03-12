using MvcWhatsUp.Models;

namespace MvcWhatsUp.Repositories
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
