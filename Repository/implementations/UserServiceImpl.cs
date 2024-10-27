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
    public class UserServiceImpl : UserService
    {
        private static UserRepository userRepository = new UserRepositoryImpl();
        public void Create(User user)
        {
            userRepository.Create(user);
        }

        public void Delete(int id)
        {
            userRepository.Delete(id);
        }

        public User FindByAccountId(int accountId)
        {
            return userRepository.FindByAccountId(accountId);
        }

        public User FindByEmail(string email)
        {
            return userRepository.FindByEmail(email);
        }

        public User FindById(int userId)
        {
            return userRepository.FindById(userId);
        }

        public User FindByPhoneNumber(string phoneNumber)
        {
            return userRepository.FindByPhoneNumber(phoneNumber);
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public void Update(User user)
        {
            userRepository.Update(user);
        }
    }
}
