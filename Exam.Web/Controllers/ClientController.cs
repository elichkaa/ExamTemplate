using Exam.Data.Models;
using Exam.InputModels;
using Exam.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
{
    [Authorize(Roles = "Customer,Administrator")]
    public class ClientController : Controller
    {
        private readonly IClientService clientService;
        private readonly UserManager<User> userManager;

        public ClientController(IClientService clientService, UserManager<User> userManager)
        {
            this.clientService = clientService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult RoomMenu()
        {
            var rooms = this.clientService.GetAllRooms();
            return View(rooms);
        }

        public async Task<IActionResult> MyRequests([FromForm] string status)
        {
            var requests = this.clientService.GetAllRequests(await this.userManager.GetUserAsync(this.User), status);
            return View(requests);
        }

        [HttpGet]
        public IActionResult AddRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(AddRequestClientInputModel input, int roomId)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            await this.clientService.AddRequst(input, await this.userManager.GetUserAsync(this.User), roomId);
            return RedirectToAction("RoomMenu");
        }

        public IActionResult CancelRequest(int requestId)
        {
            try
            {
                this.clientService.CancelRequest(requestId);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("MyRequests");
        }
    }
}
