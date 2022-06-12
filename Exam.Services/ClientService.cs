using Exam.Data;
using Exam.Data.Models;
using Exam.InputModels;
using Exam.Services.Contracts;
using Exam.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Exam.Services
{
    public class ClientService : IClientService
    {
        private readonly ExamDbContext dbContext;

        public ClientService(ExamDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddRequst(AddRequestClientInputModel input, User user, int roomId)
        {
            var request = new Request()
            {
                StartDate = input.StartDate,
                FinalDate = input.FinalDate,
                FinalPrice = input.FinalPrice,
                Status = DetermineStatus(input.StartDate),
                Room = await this.dbContext.Rooms.FirstOrDefaultAsync(x => x.Id == roomId),
                RoomId = roomId,
                User = user,
                UserId = user.Id
            };
            await this.dbContext.Requests.AddAsync(request);
            await dbContext.SaveChangesAsync();
        }

        public async Task CancelRequest(int requestId)
        {
            var request = await this.dbContext.Requests.FirstOrDefaultAsync(x => x.Id == requestId);
            if(DateTime.Now < request.StartDate)
            {
                request.Status = "Cancelled";
            }
            else
            {
                throw new ArgumentException("Your escape room has already started and you cannot cancel it.");
            }

            dbContext.Update(request);
            dbContext.SaveChanges();
        }

        public List<RequestViewModel> GetAllRequests(User user, string status)
        {
            var requests = new List<Request>();
            if (string.IsNullOrEmpty(status))
            {
                requests = this.dbContext.Requests.Include(x => x.Room).Include(x => x.User).ToList();
            }
            else
            {
                requests = this.dbContext.Requests.Include(x => x.Room).Include(x => x.User).Where(x => x.Status!.ToLower() == status.ToLower()).ToList();
            }

            if (requests == null || requests.Count == 0)
            {
                return null;
            }
            return requests.Select(x => new RequestViewModel
            {
                Id = x.Id,
                StartDate = x.StartDate,
                FinalDate = x.FinalDate,
                RoomName = x.Room!.Name,
                Username = user.UserName,
                FinalPrice = x.FinalPrice,
                Status = x.Status!
            }).ToList();
        }

        public List<RoomViewModel> GetAllRooms()
        {
            //var rooms = new List<Room?>();
            //if(start == default(DateTime) || end == default(DateTime))
            //{
            //    rooms = null;
            //}
            //else
            //{
            var rooms = this.dbContext.Rooms.Include(x => x.Image).ToList();
        //}
            

            if (rooms == null || rooms.Count == 0)
            {
                return null;
            }

            return rooms.Select(x => new RoomViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Location = x.Location,
                Price = x.Price,
                Image = x.Image != null ? x.Image.Url : null,
            }).ToList();
        }

        private string DetermineStatus(DateTime startDate)
        {
            if(DateTime.Now < startDate)
            {
                return "Pending";
            }
            else if (DateTime.Now >= startDate && DateTime.Now.Hour < startDate.Hour + 1)
            {
                return "Active";
            }
            else
            {
                return "Used";
            }
        }

    }
}
