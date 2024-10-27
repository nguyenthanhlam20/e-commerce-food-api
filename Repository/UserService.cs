using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface UserService
    {
        List<User> GetAll();
        void Create(User user);
        void Update(User user);
        void Delete(int id);
        User FindByEmail(string email);
        User FindByPhoneNumber(string phoneNumber);
        User FindById(int userId);
        User FindByAccountId(int accountId);
    }
}
