using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.implementations
{
    public class UserRepositoryImpl : UserRepository
    {
        private static List<User> users= new List<User>();
        public void Create(User user)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Users.Add(user);
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
                    User user = context.Users.SingleOrDefault(u => u.UserId == id);
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User FindByAccountId(int accountId)
        {
            User user = null;
            try
            {
                using (var context = new DBContext())
                {
                    user = context.Users.SingleOrDefault(a => a.AccountId == accountId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User FindByEmail(string email)
        {
            User user = null;
            try
            {
                using (var context = new DBContext())
                {
                    user = context.Users.SingleOrDefault(a => a.Email.Equals(email));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User FindById(int userId)
        {
            User user = null;
            try
            {
                using (var context = new DBContext())
                {
                    user = context.Users.SingleOrDefault(a => a.UserId == userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User FindByPhoneNumber(string phoneNumber)
        {
            User user = null;
            try
            {
                using (var context = new DBContext())
                {
                    user = context.Users.SingleOrDefault(a => a.Phone.Equals(phoneNumber));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public List<User> GetAll()
        {
            try
            {
                using (var context = new DBContext())
                {
                    users = context.Users.ToList();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public void Update(User user)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
