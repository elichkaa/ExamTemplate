using Exam.Data.Models;
using Exam.InputModels;
using Exam.ViewModels;

namespace Exam.Services.Contracts
{
    public interface IClientService
    {
        Task AddRequst(AddRequestClientInputModel input, User user, int roomId);

        Task CancelRequest(int requestId);

        List<RoomViewModel> GetAllRooms();

        List<RequestViewModel> GetAllRequests(User user, string status);
    }
}
