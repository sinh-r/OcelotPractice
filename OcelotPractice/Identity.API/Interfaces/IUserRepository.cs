using Identity.API.Models.Entities;

namespace Identity.API.Interfaces
{
    public interface IUserRepository
    {
        public bool AddUser(User user);
        public User? FindUser(string userName, string password);
        public User? FindUser(string userName);
    }
}
