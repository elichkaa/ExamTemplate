using Exam.Data;
using Exam.Data.Models;
using Exam.InputModels;
using Exam.Services.Contracts;
using Exam.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public List<UserOnAdminPageViewModel> GetAllUsers()
        {
            var usersFromDb = this.userManager.Users.ToList();
            var users = usersFromDb.Select(u => new UserOnAdminPageViewModel()
            {
                UserId = u.Id,
                UserName = u.UserName,
                CreatedOn = u.CreatedOn,
                Email = u.Email,
                Roles = userManager.GetRolesAsync(u).Result.ToList()
            }).ToList();
            return users;
        }
.
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
                UserName = input.Email,
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

        public async Task DeleteOrder(int id)
        {
            var orderToDelete = this.dbContext.Orders.Where(x => x.Id == id).FirstOrDefault();
            if (orderToDelete != null)
            {
                dbContext.Orders.Remove(orderToDelete);
                await dbContext.SaveChangesAsync();
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
            foreach(var role in newRoles)
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
        public async Task EditOrderByAdmin(EditOrderByAdminInputModel input)
        {
            var order = await this.dbContext.Orders.Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            order.SupervisorTechnicianName = order.SupervisorTechnicianName != input.SupervisorTechnicianName ? input.SupervisorTechnicianName : order.SupervisorTechnicianName;
            order.CheckedByTechnicianOn = order.CheckedByTechnicianOn != input.CheckedByTechnicianOn ? input.CheckedByTechnicianOn : order.CheckedByTechnicianOn;
            dbContext.Update(order);
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

        public async Task<EditOrderByAdminInputModel> GetOrderById(int orderId)
        {
            var order = await this.dbContext.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            return new EditOrderByAdminInputModel
            {
                SupervisorTechnicianName = order.SupervisorTechnicianName,
                CheckedByTechnicianOn = order.CheckedByTechnicianOn
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

        public List<OrderOnAdminPageViewModel> GetAllOrders(string username)
        {
            var userOrders = new List<Order>();
            if (!string.IsNullOrEmpty(username))
            {
                userOrders = this.dbContext.Orders.Include(x => x.User).Where(x => x.User.UserName.ToLower() == username.ToLower()).ToList();
            }
            else
            {
                userOrders = this.dbContext.Orders.Include(x => x.User).ToList();
            }
            
            return userOrders.Select(x => new OrderOnAdminPageViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Address = x.Address,
                Image = x.Image,
                StatusName = x.Status,
                CreatedOn = x.CreatedOn,
                CheckedByTechnicianOn = x.CheckedByTechnicianOn,
                SupervisorTechnicianName = x.SupervisorTechnicianName,
                Username = x.User.UserName
            }).ToList();
        }
    }
}
