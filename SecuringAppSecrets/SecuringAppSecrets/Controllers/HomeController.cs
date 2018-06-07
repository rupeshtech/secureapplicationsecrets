using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SecuringAppSecrets.Helper;
using SecuringAppSecrets.Models;

namespace SecuringAppSecrets.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       
        public IActionResult Index()
        {
            var test = Configuration["Apikey"];
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var valueFromKeyVault = KeyVaultHelper.GetValueAsync(Configuration["AuthorizationkeyFromKeyVault"]).GetAwaiter().GetResult();
            ViewData["AuthorizationKey"] = valueFromKeyVault;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            ViewData["ApiKey"] = KeyVaultHelper.GetKeyVaultValueAsync(Configuration["ApikeyFromKeyVault"]).GetAwaiter().GetResult();
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
