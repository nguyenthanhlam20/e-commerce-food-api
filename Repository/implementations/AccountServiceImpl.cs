using BusinessObject;
using Repository;
using Repository.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementations
{
    public class AccountServiceImpl : AccountService
    {
        private static AccountRepository accountRepository = new AccountRepositoryImpl();

        public void Create(Account account)
        {
            accountRepository.Create(account);
        }

        public void Delete(int id)
        {
            accountRepository.Delete(id);
        }

        public Account FindById(int id)
        {
            return accountRepository.FindById(id);
        }

        public Account FindByUsername(string username)
        {
            return accountRepository.FindByUsername(username);
        }

        public Account FindByUsernameAndPassword(string username, string password)
        {
            return accountRepository.FindByUsernameAndPassword(username, password);
        }

        public List<Account> GetAll()
        {
            return accountRepository.GetAll();
        }

        public void Update(Account account)
        {
            accountRepository.Update(account);
        }
    }
}
