using Exam.Services.Contracts;
using Exam.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Exam.Data.Models;

namespace Exam.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly IAdminService adminService;

        public AdministrationController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = adminService.GetAllUsers().ToList();
            return View(users);
        }

        public IActionResult Orders([FromForm] string username)
        {
            var allOrders = this.adminService.GetAllOrders(username);
            return View(allOrders);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View(new AddUserInputModel()
            {
                Roles = this.adminService.GetRoles()
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserInputModel input)
        {
            if (!ModelState.IsValid)
            {
                input.Roles = this.adminService.GetRoles();
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            try
            {
                await this.adminService.AddUser(input);
            }
            catch (Exception ex)
            {
                input.Roles = this.adminService.GetRoles();
                this.TempData["Error"] = ex.Message;
                return this.View(input);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            await this.adminService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var filledUserData = await this.adminService.GetUserById(id);
            TempData["id"] = id;
            return View(new EditUserInputModel()
            {
                Username = filledUserData.Username,
                Password = filledUserData.Password,
                Roles = filledUserData.Roles,
                AllRoles = this.adminService.GetRoles()
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserInputModel input)
        {
            try
            {
                await this.adminService.EditUser(input, TempData["id"].ToString());
            }
            catch (Exception ex)
            {
                input.AllRoles = this.adminService.GetRoles();
                this.TempData["Error"] = ex.Message;
                return this.View(input);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditOrder(int id)
        {
            var orderDetails = await this.adminService.GetOrderById(id);
            return View(new EditOrderByAdminInputModel()
            {
                Id = id,
                SupervisorTechnicianName = orderDetails.SupervisorTechnicianName,
                CheckedByTechnicianOn = orderDetails.CheckedByTechnicianOn
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(EditOrderByAdminInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            await this.adminService.EditOrderByAdmin(input);

            return RedirectToAction("Orders");
        }

        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await this.adminService.DeleteOrder(orderId);
            return RedirectToAction("Orders");
        }
    }
}
