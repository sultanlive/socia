using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocioLiftPay.Models;
using SocioLiftPay.Services;

namespace SocioLiftPay.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPayService _payTest;
        private readonly PayService _pay;

        public HomeController(IPayService payTest, PayService pay)
        {
            _payTest = payTest;
            _pay = pay;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Pay(string desc, float sum, string backUrl = "http://sociolift.kz", string requestUrl = "http://sociolift.kz")
        {
            try
            {
                var operationUrl = await _pay.Pay(desc, sum, backUrl, requestUrl);
                return Json(operationUrl);
            }
            catch(System.Exception exp)
            {
                return Json(exp.Message);
            }
        }

        public async Task<IActionResult> PayTest(string desc, float sum, string backUrl = "http://sociolift.kz", string requestUrl = "http://sociolift.kz")
        {
            try
            {
                var operationUrl = await _payTest.Pay(desc, sum, backUrl, requestUrl);
                return Json(operationUrl);
            }
            catch(System.Exception exp)
            {
                return Json(exp.Message);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
