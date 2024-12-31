using InfnetEcommerceContext.User.API.Models.DTOs;
using InfnetEcommerceContext.User.API.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InfnetEcommerceContext.User.API.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserEntityDTO GetById(Guid userId)
        {
            return new UserEntityDTO(userRepository.GetById(userId));
        }
        public async Task<UserEntityDTO> GetByEmail(string userEmail)
        {
            return new UserEntityDTO(await userRepository.GetByEmail(userEmail));
        }
    }
}
