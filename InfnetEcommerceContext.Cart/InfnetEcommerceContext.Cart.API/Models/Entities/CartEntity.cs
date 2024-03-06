using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfnetEcommerceContext.Cart.API.Models.Entities
{
    [Table("Person")]
    public class CartEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
