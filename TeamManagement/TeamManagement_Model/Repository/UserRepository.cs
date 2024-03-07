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
        public async Task<User> GetUser(string userEmail) {
            try
            {
                return await _dbContext.Users.FindAsync(userEmail);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return null;
            }
           
         }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return null;
            }


        }

        public async Task<User> GetUserByRoleId(int id)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(x => x.RoleId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<User>> GetUsersListById(int id)
        {
            try
            {
                return await _dbContext.Users.Where(x => x.RoleId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Login> GetLoginUser(string loginEmail)
        {
            try
            {
                return await _dbContext.Login.FindAsync(loginEmail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task AddUser(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        public async Task AddLoginUser(Login login)
        {
            try
            {
                await _dbContext.Login.AddAsync(login);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
        public async Task SaveUser(User user)
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SaveLoginUser(Login user)
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
