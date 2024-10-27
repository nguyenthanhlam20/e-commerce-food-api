using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.implementations
{
    public class AccountRepositoryImpl : AccountRepository
    {
        private static List<Account> accounts = new List<Account>();
        public void Create(Account account)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Accounts.Add(account);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var context = new DBContext())
                {
                    Account account = context.Accounts.SingleOrDefault(a => a.AccountId == id);
                    context.Accounts.Remove(account);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Account FindById(int id)
        {
            Account account = null;
            try
            {
                using (var context = new DBContext())
                {
                    account = context.Accounts.SingleOrDefault(a => a.AccountId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public Account FindByUsername(string username)
        {
            Account account = null;
            try
            {
                using (var context = new DBContext())
                {
                    account = context.Accounts.SingleOrDefault(a => a.Username.Equals(username));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public Account FindByUsernameAndPassword(string username, string password)
        {
            Account account = null;
            try
            {
                using (var context = new DBContext())
                {
                    account = context.Accounts.SingleOrDefault(a => a.Username.Equals(username) && a.Password.Equals(password) && a.IsActive == true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public List<Account> GetAll()
        {
            try
            {
                using (var context = new DBContext())
                {
                    accounts = context.Accounts.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return accounts;
        }

        public void Update(Account account)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry<Account>(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
