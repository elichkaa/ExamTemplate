using Exam.Data;
using Exam.Data.Models;
using Exam.InputModels;
using Exam.Services.Contracts;
using Exam.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ExamDbContext dbContext;
        public AdminService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ExamDbContext dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }
        public List<UserViewModel> GetAllUsers()
        {
            var usersFromDb = this.userManager.Users.ToList();
            var users = usersFromDb.Select(u => new UserViewModel()
            {
                UserId = u.Id,
                UserName = u.UserName,
                CreatedOn = u.CreatedOn,
                Email = u.Email,
                Roles = userManager.GetRolesAsync(u).Result.ToList()
            }).ToList();
            return users;
        }

        public List<string> GetRoles()
        {
            var roles = this.roleManager.Roles.Select(x => x.Name).ToList();
            return roles;
        }

        public async Task AddUser(AddUserInputModel input)
        {
            var hasher = new PasswordHasher<User>();
            var user = new User()
            {
                Email = input.Email,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                UserName = input.Email.Split('@')[0],
                NormalizedUserName = input.Email.ToUpper(),
                NormalizedEmail = input.Email.ToUpper(),
                CreatedOn = DateTime.Now,
            };
            user.PasswordHash = hasher.HashPassword(user, input.Password);
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                throw new ArgumentException(string.Join("\n", result.Errors.Select(x => x.Description)));
            }
            foreach (var role in input.Roles)
            {
                await this.AddRole(user, role);
            }
        }

        public async Task DeleteUser(string id)
        {
            var userToDelete = this.userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            if (userToDelete != null)
            {
                await userManager.DeleteAsync(userToDelete);
            }
        }

        public async Task EditUser(EditUserInputModel input, string userId)
        {
            var user = this.dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            user.UserName = input.Username != user.UserName ? input.Username : user.UserName;
            var hasher = new PasswordHasher<User>();
            var updatedPassword = hasher.HashPassword(user, input.Password);
            user.PasswordHash = updatedPassword != user.PasswordHash ? updatedPassword : user.PasswordHash;
            var oldRoles = this.userManager.GetRolesAsync(user).Result.ToList();
            var newRoles = input.Roles.Except(oldRoles).ToList();
            foreach (var role in newRoles)
            {
                await this.userManager.AddToRoleAsync(user, role);
            }
            var rolesToDelete = oldRoles.Except(input.Roles).ToList();
            foreach (var role in rolesToDelete)
            {
                await this.userManager.RemoveFromRoleAsync(user, role);
            }
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditUserInputModel> GetUserById(string userId)
        {
            var user = this.dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            return new EditUserInputModel
            {
                Username = user.UserName,
                Password = user.PasswordHash,
                Roles = await userManager.GetRolesAsync(user)
            };
        }

        public async Task DeleteRole(User user, string name)
        {
            await this.userManager.RemoveFromRolesAsync(user, new List<string> { name });
        }

        public async Task AddRole(User user, string name)
        {
            await this.userManager.AddToRoleAsync(user, name);
        }

        public async Task AddRoom(AddRoomInputModel input, string basePath, User user)
        {
            var room = new Room();
            this.AddImage(basePath, room, input.Image);
            room.Name = input.Name;
            room.Description = input.Description;
            room.Location = input.Location;
            room.Price = input.Price;
            await this.dbContext.Rooms.AddAsync(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var roomToDelete = this.dbContext.Rooms.Where(x => x.Id == id).FirstOrDefault();
            if (roomToDelete != null)
            {
                dbContext.Rooms.Remove(roomToDelete);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task EditRoom(EditRoomInputModel input, string basePath)
        {
            var room = await this.dbContext.Rooms.Include(x => x.Image).Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            room.Name = room.Name != input.Name ? input.Name : room.Name;
            room.Location = room.Location != input.Location ? input.Location : room.Location;
            room.Description = room.Description != input.Description ? input.Description : room.Description;
            room.Price = room.Price != input.Price ? input.Price : room.Price;

            //if(string.IsNullOrEmpty(room.ImageId))
            //{
            //    this.AddImage(basePath, room, input.Image);
            //}
            //else
            //{
            //    File.Delete(room.Image.Url);
            //    this.dbContext.Images.Remove(room.Image);
            //    await dbContext.SaveChangesAsync();
            //}

            dbContext.Update(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditRoomInputModel> GetRoomById(int roomId)
        {
            var oldRoom = await this.dbContext.Rooms.Where(x => x.Id == roomId).FirstOrDefaultAsync();
            return new EditRoomInputModel
            {
                Name = oldRoom.Name,
                Description = oldRoom.Description,
                Location = oldRoom.Location,
                Price = oldRoom.Price,
            };
        }

        public List<RoomViewModel> GetAllRooms()
        {
            var rooms = this.dbContext.Rooms.Include(nameof(Room.Image)).ToList();

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
                Image = this.dbContext.Images.Where(img => img.Id == x.ImageId).FirstOrDefault() == null ? null : this.dbContext.Images.Where(img => img.Id == x.ImageId).FirstOrDefault().Url,
            }).ToList();
        }

        private void AddImage(string basePath, Room room, IFormFile input)
        {
            if (!Directory.Exists(basePath)) { Directory.CreateDirectory(basePath); }
            if (input != null)
            {
                room.Image = new Image
                {
                    Url = $"{basePath}{input.FileName}"
                };
                using Stream fileStream = new FileStream(room.Image.Url, FileMode.Create);
                input.CopyTo(fileStream);
            }
        }

        public List<RequestViewModel> GetAllRequests(string status)
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
                RoomName = x.Room.Name,
                Username = x.User.UserName,
                FinalPrice = x.FinalPrice,
                Status = x.Status
            }).ToList();
        }

        public async Task AddRequest(AddRequestInputModel input)
        {
            var user = await this.dbContext.Users.Where(x => x.UserName.ToLower() == input.Username.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("No user with this username exists.");
            }

            var room = await this.dbContext.Rooms.Where(x => x.Name == input.RoomName).FirstOrDefaultAsync();
            if (room == null)
            {
                throw new Exception("No room with this name exists.");
            }

            //to-do: check for valid status message
            
            var request = new Request()
            {
                StartDate = input.StartDate,
                FinalDate = input.FinalDate,
                FinalPrice = input.FinalPrice,
                Status = input.Status,
                Room = room,
                RoomId = room.Id,
                User = user,
                UserId = user.Id
            };
            await this.dbContext.Requests.AddAsync(request);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteRequest(int id)
        {
            var requestToDelete = this.dbContext.Requests.Where(x => x.Id == id).FirstOrDefault();
            if (requestToDelete != null)
            {
                dbContext.Requests.Remove(requestToDelete);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task EditRequest(EditRequestInputModel input)
        {
            var request = await this.dbContext.Requests.Include(x => x.Room).Include(x => x.User).Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            request.StartDate = request.StartDate != input.StartDate ? input.StartDate : request.StartDate;
            request.FinalDate = request.FinalDate != input.FinalDate ? input.FinalDate : request.FinalDate;
            request.FinalPrice = request.FinalPrice != input.FinalPrice ? input.FinalPrice : request.FinalPrice;
            request.Status = request.Status != input.Status ? input.Status : request.Status;

            var user = await this.dbContext.Users.Where(x => x.UserName.ToLower() == input.Username.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("No user with this username exists.");
            }
            else
            {
                request.User = request.User.UserName != input.Username ? user : request.User;
                request.UserId = request.User.UserName != input.Username ? user.Id : request.UserId;
            }

            var room = await this.dbContext.Rooms.Where(x => x.Name == input.RoomName).FirstOrDefaultAsync();
            if (room == null)
            {
                throw new Exception("No room with this name exists.");
            }
            else
            {
                request.Room = request.Room.Name != input.RoomName ? room : request.Room;
                request.RoomId = request.Room.Name != input.RoomName ? room.Id : request.RoomId;
            }

            dbContext.Update(request);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditRequestInputModel> GetRequestById(int requestId)
        {
            var oldRequest = await this.dbContext.Requests.Include(x => x.User).Include(x => x.Room).Where(x => x.Id == requestId).FirstOrDefaultAsync();
            return new EditRequestInputModel
            {
                StartDate = oldRequest.StartDate,
                FinalDate = oldRequest.FinalDate,
                FinalPrice = oldRequest.FinalPrice,
                Status = oldRequest.Status,
                RoomName = oldRequest.Room.Name,
                Username = oldRequest.User.UserName
            };
        }
    }
}