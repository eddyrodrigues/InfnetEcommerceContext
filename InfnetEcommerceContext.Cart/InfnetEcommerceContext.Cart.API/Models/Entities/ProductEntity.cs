namespace InfnetEcommerceContext.Cart.API.Models.Entities
{
    public class ProductEntity : BaseEntity
    {
        public ProductEntity()
        {
            
        }
        public ProductEntity(Guid cartId, Guid productId)
        {
            CartId = cartId;
            ProductId = productId;
        }

        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
    }
}
