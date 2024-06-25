using System.Threading.Tasks;
using System;
using IO.Swagger.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IO.Swagger.Repository
{
    public interface IUserRepository
    {
        void Add(User userInfo);
        Task<List<User>> Get();
        Task<User> getUserInfoByEmail(string email);
        void Remove(User userInfo);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<User> getUserInfoByEmail(string email)
        {
            try
            {
                return await _context.Users.Where(item => item.Email.Equals(email)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
        public void Add(User userInfo)
        {
            try
            {
                _context.Users.Add(userInfo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
        public void Remove(User userInfo)
        {
            try
            {
                _context.Users.Remove(userInfo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<User>> Get()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
