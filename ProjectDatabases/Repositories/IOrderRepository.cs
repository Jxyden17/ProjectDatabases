using ProjectDatabases.Models;

namespace ProjectDatabases.Repositories
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
    }
}
