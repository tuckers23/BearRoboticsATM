using BearRoboticsATM.Models;
using Microsoft.AspNetCore.Mvc;

namespace BearRoboticsATM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ATMController : Controller
    {
        public ATMController()
        {
            //todo
        }

        [HttpGet]
        public ActionResult<bool> ValidCard(string cardNumber)
        {
            //todo
            return false;
        }

        [HttpGet]
        public ActionResult<bool> InsertPIN(int pin)
        {
            return false;
        }

        [HttpGet]
        public ActionResult<string> SelectAccount()
        {
            //todo
            return "Account Name";
        }

        [HttpGet]
        public ActionResult<int> SeeBalance()
        {
            //todo
            return 0;
        }

        [HttpPost]
        public ActionResult Deposit()
        {
            //todo
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw()
        {
            //todo
            return View();
        }
    }
}
