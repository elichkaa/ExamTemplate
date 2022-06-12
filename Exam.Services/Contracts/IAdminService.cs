using Exam.Data.Models;
using Exam.InputModels;
using Exam.ViewModels;

namespace Exam.Services.Contracts
{
    public interface IAdminService
    {
        List<UserViewModel> GetAllUsers();

        Task AddUser(AddUserInputModel input);

        Task DeleteUser(string id);

        Task DeleteRole(User user, string name);

        Task AddRole(User user, string name);

        List<string> GetRoles();

        Task<EditUserInputModel> GetUserById(string userId);

        Task EditUser(EditUserInputModel input, string userId);

        List<RoomViewModel> GetAllRooms();

        Task<EditRoomInputModel> GetRoomById(int roomId);

        Task EditRoom(EditRoomInputModel input, string basePath);

        Task DeleteRoom(int id);

        Task AddRoom(AddRoomInputModel input, string basePath, User user);

        Task AddRequest(AddRequestInputModel input);

        Task DeleteRequest(int id);

        Task EditRequest(EditRequestInputModel input);

        List<RequestViewModel> GetAllRequests(string status);

        Task<EditRequestInputModel> GetRequestById(int requestId);
    }
}
