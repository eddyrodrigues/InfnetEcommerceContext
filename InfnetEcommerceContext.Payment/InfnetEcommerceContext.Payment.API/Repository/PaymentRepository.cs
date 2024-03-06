using InfnetEcommerceContext.Payment.API.Models;
using InfnetEcommerceContext.Payment.API.Repository.DataContext;
using Microsoft.EntityFrameworkCore;

namespace InfnetEcommerceContext.Payment.API.Repository
{
    public class PaymentRepository
    {
        private readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public List<PaymentEntity> GetByUserId(Guid userId)
        {
            return _context.Payments.Where(p => p.UserId == userId).AsNoTracking().ToList();
        }

        public PaymentEntity Add(PaymentEntity entity)
        {
            _context.Payments.Add(entity);
            _context.SaveChanges();
            return entity;
        }

    }
}
