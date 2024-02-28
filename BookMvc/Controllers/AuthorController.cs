using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BookMvc.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _service;
        public IActionResult Index()
        {
            return View();
        }
    }
}
