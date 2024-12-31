using InfnetEcommerceContext.User.API.Models.DTOs;
using InfnetEcommerceContext.User.API.Models.Entities;
using InfnetEcommerceContext.User.API.Repository.DataContext;
using Microsoft.EntityFrameworkCore;

namespace InfnetEcommerceContext.User.API.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            return await this.context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public List<UserEntity> GetAll()
        {
            return context.Users.ToList();
        }

        public void Add(UserEntity cart)
        {
            context.Users.Add(cart);
            context.SaveChanges();
        }
        public void Update(UserEntity cart)
        {
            context.Users.Update(cart);
            context.SaveChanges();
        }
        public void Delete(UserEntity cart)
        {
            context.Users.Remove(cart);
            context.SaveChanges();
        }

        public UserEntity GetById(Guid userId)
        {
            return context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }
    }

    public interface IUserRepository
    {
        UserEntity GetById(Guid userId);
        Task<UserEntity> GetByEmail(string email);
        List<UserEntity> GetAll();
        void Add(UserEntity cart);
        void Delete(UserEntity cart);
        void Update(UserEntity cart);
    }
}
