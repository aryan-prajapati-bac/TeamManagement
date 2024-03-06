using TeamManagement.Interfaces;
using TeamManagement.Models;
using TeamManagement_Models.Database;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace TeamManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext _dbContext;        

        public UserRepository(MyDBContext context)
        {
            _dbContext = context;
        }

        public User GetUser(string userEmail) {
              return _dbContext.Users.Find(userEmail);
               
         }

        public User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserId == id);
            
        }

        public User GetUserByRoleId(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.RoleId == id);
        }

        public List<User> GetUsersListById(int id)
        {
            return _dbContext.Users.Where(x => x.RoleId == id).ToList();
        }

        public Login GetLoginUser(string loginEmail)
        {
            return _dbContext.Login.Find(loginEmail);
            
        }
        public void AddUser(User user)
        {
             _dbContext.Users.Add(user);
             _dbContext.SaveChanges();
        }

        public void AddLoginUser(Login login)
        {
            _dbContext.Login.Add(login);
            _dbContext.SaveChanges();
        }
        public void SaveUser(User user)
        {
             _dbContext.SaveChanges();
        }

        public void SaveLoginUser(Login user)
        {
            _dbContext.SaveChanges();
        }
    }
}
