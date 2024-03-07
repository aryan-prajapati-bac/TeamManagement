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
        #region DBContext
        private readonly MyDBContext _dbContext;
        #endregion

        #region DI
        public UserRepository(MyDBContext context)
        {
            _dbContext = context;
        }
        #endregion

        #region Method-Implementations
        public async Task<User> GetUser(string userEmail)
        {            
           return await _dbContext.Users.FindAsync(userEmail);                   
           
        }

        public async Task<User> GetUserById(int id)
        {
           return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
           
        }

        public async Task<User> GetUserByRoleId(int id)
        { 
           return await _dbContext.Users.FirstOrDefaultAsync(x => x.RoleId == id);
        }

        public async Task<List<User>> GetUsersListById(int id)
        {
           return await _dbContext.Users.Where(x => x.RoleId == id).ToListAsync();
        }

        public async Task<Login> GetLoginUser(string loginEmail)
        {
          return await _dbContext.Login.FindAsync(loginEmail);
        }
        public async Task AddUser(User user)
        {
          await _dbContext.Users.AddAsync(user);
          await _dbContext.SaveChangesAsync();            
        }

        public async Task AddLoginUser(Login login)
        {
          await _dbContext.Login.AddAsync(login);
          await _dbContext.SaveChangesAsync();            
        }
        public async Task SaveUser(User user)
        {
           await _dbContext.SaveChangesAsync();            
        }

        public async Task SaveLoginUser(Login user)
        {
          await _dbContext.SaveChangesAsync();           
        }
        #endregion
    }
}
