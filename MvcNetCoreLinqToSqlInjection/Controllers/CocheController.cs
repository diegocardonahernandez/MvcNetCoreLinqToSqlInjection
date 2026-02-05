using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class CocheController : Controller
    {

        private ICoche car;

        public CocheController(ICoche car)
        {
            this.car = car;
        }
        public IActionResult Index()
        {
            return View(this.car);
        }

        [HttpPost]
        public IActionResult Index(string action)
        {
            if (action.ToLower() == "acelerar")
            {
                this.car.Acelerar();
            }
            else
            {
                this.car.Frenar();
            }
            return View(this.car);
        }
    }
}
