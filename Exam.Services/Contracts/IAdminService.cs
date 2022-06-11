using Exam.Data.Models;
using Exam.InputModels;
using Exam.ViewModels;
using System.Security.Claims;

namespace Exam.Services.Contracts
{
    public interface IAdminService
    {
        List<UserOnAdminPageViewModel> GetAllUsers();

        Task AddUser(AddUserInputModel input);

        Task DeleteUser(string id);

        Task DeleteRole(User user, string name);

        Task AddRole(User user, string name);

        List<string> GetRoles();

        Task<EditUserInputModel> GetUserById(string userId);

        Task EditUser(EditUserInputModel input, string userId);

        List<OrderOnAdminPageViewModel> GetAllOrders(string username);

        Task<EditOrderByAdminInputModel> GetOrderById(int orderId);

        Task EditOrderByAdmin(EditOrderByAdminInputModel input);

        Task DeleteOrder(int id);
    }
}
