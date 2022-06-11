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
        public IActionResult Index([FromForm] string status)
        {
            var userOrders = this.clientService.GetAllOrders(this.userManager.GetUserId(this.User), status);
            return View(userOrders);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrderInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            this.clientService.AddOrder(input, await this.userManager.GetUserAsync(this.User));
            return RedirectToAction("Index", "Client");
        }

        public IActionResult Delete(int orderId)
        {
            this.clientService.DeleteOrder(orderId);
            return RedirectToAction("Index", "Client");
        }

        [HttpGet]
        public IActionResult Edit(int orderId)
        {
            var order = this.clientService.GetSingleOrder(orderId);
            ViewData["Filled"] = order;
            TempData["OrderId"] = orderId;
            return View(new EditOrderInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditOrderInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            this.clientService.EditOrder(input, (int)TempData["OrderId"]);
            return RedirectToAction("Index", "Client");
        }
    }
}
