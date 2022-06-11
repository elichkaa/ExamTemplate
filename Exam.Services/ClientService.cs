using Exam.Data;
using Exam.Data.Models;
using Exam.InputModels;
using Exam.Services.Contracts;
using Exam.ViewModels;

namespace Exam.Services
{
    public class ClientService : IClientService
    {
        private readonly ExamDbContext dbContext;

        public ClientService(ExamDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddOrder(AddOrderInputModel input, User user)
        {
            dbContext.Orders.Add(new Order
            {
                Name = input.Name,
                Description = input.Description,
                Address = input.Address,
                Status = "Waiting",
                Image = input.Image,
                User = user,
                CreatedOn = DateTime.Now
            });
            dbContext.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var orderToDelete = this.dbContext.Orders.Where(x => x.Id == orderId).FirstOrDefault();
            if (orderToDelete != null)
            {
                dbContext.Orders.Remove(orderToDelete);
            }
            dbContext.SaveChanges();
        }

        public void EditOrder(EditOrderInputModel input, int id)
        {
            var dbOrder = this.dbContext.Orders.Where(x => x.Id == id).FirstOrDefault();
            dbOrder.Name = input.Name != dbOrder.Name ? input.Name : dbOrder.Name;
            dbOrder.Description = input.Description != dbOrder.Description ? input.Description : dbOrder.Description;
            dbOrder.Address = input.Address != dbOrder.Address ? input.Address : dbOrder.Address;
            dbOrder.Image = input.Image != dbOrder.Image ? input.Image : dbOrder.Image;
            dbContext.Update(dbOrder);
            dbContext.SaveChanges();
        }

        public List<OrderOnAllPageViewModel> GetAllOrders(string clientId, string status)
        {
            var usersOrders = new List<Order>();
            if (string.IsNullOrEmpty(status))
            {
                usersOrders = this.dbContext.Orders.Where(x => x.UserId == clientId).ToList();
            }
            else
            {
                usersOrders = this.dbContext.Orders.Where(x => x.UserId == clientId && x.Status.ToLower() == status.ToLower()).ToList();
            }
           
            var orders = usersOrders.Select(o => new OrderOnAllPageViewModel()
            {
                Address = o.Address,
                Name = o.Name,
                StatusName = o.Status,
                Description = o.Description!,
                Id = o.Id
            }).ToList();
            return orders;
        }

        public OrderOnAllPageViewModel GetSingleOrder(int orderId)
        {
            var dbOrders = this.dbContext.Orders.Where(x => x.Id == orderId).ToList();
            var orders = dbOrders.Select(o => new OrderOnAllPageViewModel()
            {
                Id = o.Id,
                Address = o.Address,
                Name = o.Name,
                Image = o.Image,
                StatusName = o.Status,
                Description = o.Description
            }).ToList();
            return orders.FirstOrDefault();
        }
    }
}
