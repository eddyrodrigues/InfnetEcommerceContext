using InfnetEcommerceContext.Order.API.Models.Entities;
using InfnetEcommerceContext.Order.API.Repository.DataContext;

namespace InfnetEcommerceContext.Order.API.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext context;

        public OrderRepository(OrderContext context)
        {
            this.context = context;
        }

        public List<OrderEntity> GetAll()
        {
            return context.Orders.ToList();
        }

        public void Add(OrderEntity order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }
        public void Update(OrderEntity order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }
        public void Delete(OrderEntity order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public OrderEntity GetById(Guid orderId)
        {
            return context.Orders.Find(orderId);
        }

        //public CartEntity GetByUserId(Guid userId)
        //{
        //    return context.Orders.Where(c => c.UserId == userId).Include(c => c.Products).FirstOrDefault();
        //}

        //public CartEntity GetById(Guid cartId)
        //{
        //    return context.Orders.Where(c => c.Id == cartId).Include(c => c.Products).FirstOrDefault();
        //}
    }

    public interface IOrderRepository
    {
        void Add(OrderEntity cart);
        void Delete(OrderEntity cart);
        List<OrderEntity> GetAll();
        void Update(OrderEntity cart);
        OrderEntity GetById(Guid orderId);
        //CartEntity GetByUserId(Guid userId);
    }
}
