using Exam.Data.Models;
using Exam.InputModels;
using Exam.ViewModels;

namespace Exam.Services.Contracts
{
    public interface IClientService
    {
        public List<OrderOnAllPageViewModel> GetAllOrders(string clientId, string status);

        public void AddOrder(AddOrderInputModel input, User user);

        public void EditOrder(EditOrderInputModel input, int id);

        public void DeleteOrder(int orderId);

        public OrderOnAllPageViewModel GetSingleOrder(int orderId);
    }
}
