using API_Challenge.Modelos;

namespace API_Challenge.Repository
{
    public interface IUserRepository
    {
        Task<int> RegisterUser(User user, string password);
        Task<string> Login(string userName, string password);
        Task<bool> UserExiste(string username);
    }
}
