using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface AccountService
    {
        List<Account> GetAll();
        void Create(Account account);
        void Update(Account account);
        void Delete(int id);
        Account FindByUsernameAndPassword(string username, string password);
        Account FindByUsername(string username);
        Account FindById(int id);
    }
}
