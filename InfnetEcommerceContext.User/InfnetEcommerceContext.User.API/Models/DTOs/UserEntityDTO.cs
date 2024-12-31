using InfnetEcommerceContext.User.API.Models.Entities;

namespace InfnetEcommerceContext.User.API.Models.DTOs
{
    public class UserEntityDTO
    {
        public UserEntityDTO()
        {
        }

        public UserEntityDTO(UserEntity entity)
        {
            Id = entity?.Id.ToString();
            Name = entity.UserName;
            Email = entity.Email;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
