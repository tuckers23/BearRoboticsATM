using Microsoft.AspNetCore.Mvc;

namespace BearRoboticsATM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ATMController : ControllerBase
    {
        private static readonly string[] ATMOptions = new string[] { "See Balance", "Deposit", "Withdraw" };

        public ATMController()
        {
            //todo
        }

        [HttpGet]
        public bool ValidCard(string cardNumber)
        {
            //todo
            return false;
        }

        [HttpGet]
        public bool InsertPIN(int pin)
        {
            //todo
            return false;
        }

        [HttpGet]
        public string SelectAccount()
        {
            //todo
            return "Account Name";
        }

        [HttpGet]
        public int SeeBalance()
        {
            //todo
            return 0;
        }

        [HttpPost]
        public void Deposit()
        {
            //todo
        }

        [HttpPost]
        public void Withdraw()
        {
            //todo
        }
    }
}
