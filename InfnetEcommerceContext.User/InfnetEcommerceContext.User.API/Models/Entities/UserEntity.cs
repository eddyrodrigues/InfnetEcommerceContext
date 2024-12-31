using System.ComponentModel.DataAnnotations.Schema;

namespace InfnetEcommerceContext.User.API.Models.Entities
{
    [Table("users_logins")]
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; }
        public string Email => UserName;
        public string Password { get; set; }
    }

}
