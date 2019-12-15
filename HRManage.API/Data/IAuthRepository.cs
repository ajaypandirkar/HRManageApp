using System.Threading.Tasks;
using HRManage.API.Models;

namespace HRManage.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);    
    }
}