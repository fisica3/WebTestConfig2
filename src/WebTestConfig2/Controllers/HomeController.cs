using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebTestConfig2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigurationRoot _config;
        public HomeController(IConfigurationRoot config)
        {
            this._config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var DondeEstoy = HelperConfig.GetAppSettings(_config, "DondeEstoy");
            if (String.IsNullOrEmpty(DondeEstoy))
                ViewBag.Message = "No se pudo recuperar";
            else ViewBag.Message = "Nuestro entorno con DI y Helper es " + DondeEstoy;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
