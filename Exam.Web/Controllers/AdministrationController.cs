using Exam.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Exam.Data.Models;
using Exam.InputModels;
using Microsoft.AspNetCore.Identity;

namespace Exam.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly IAdminService adminService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment environment;

        public AdministrationController(IAdminService adminService, UserManager<User> userManager, IWebHostEnvironment environment)
        {
            this.adminService = adminService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = adminService.GetAllUsers().ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Rooms()
        {
            var allRooms = this.adminService.GetAllRooms();
            return View(allRooms);
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
        public async Task<IActionResult> EditRoom(int roomId)
        {
            var oldRoom = await this.adminService.GetRoomById(roomId);
            TempData["Id"] = roomId;
            return View(oldRoom);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(EditRoomInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            input.Id = (int)TempData["Id"];
            await this.adminService.EditRoom(input, $"{this.environment.WebRootPath}\\img\\");

            return RedirectToAction("Rooms");
        }

        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            await this.adminService.DeleteRoom(roomId);
            return RedirectToAction("Rooms");
        }

        [HttpGet]
        public IActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(AddRoomInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            try
            {
                await this.adminService.AddRoom(input, $"{this.environment.WebRootPath}\\img\\", await this.userManager.GetUserAsync(this.User));
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Rooms");
        }

        public IActionResult Requests([FromForm] string status)
        {
            var allRequests = this.adminService.GetAllRequests(status);
            return View(allRequests);
        }

        [HttpGet]
        public IActionResult AddRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(AddRequestInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            try
            {
                await this.adminService.AddRequest(input);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Requests");
        }

        [HttpGet]
        public async Task<IActionResult> EditRequest(int requestId)
        {
            var oldRequest = await this.adminService.GetRequestById(requestId);
            TempData["Id"] = requestId;
            return View(oldRequest);
        }

        [HttpPost]
        public async Task<IActionResult> EditRequest(EditRequestInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }


            input.Id = (int)TempData["Id"];
            try
            {
                await this.adminService.EditRequest(input);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Requests");
        }

        public async Task<IActionResult> DeleteRequest(int requestId)
        {
            await this.adminService.DeleteRequest(requestId);
            return RedirectToAction("Requests");
        }
    }
}
