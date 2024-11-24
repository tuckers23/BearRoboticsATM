using BearRoboticsATM.Models;
using Microsoft.AspNetCore.Mvc;

namespace BearRoboticsATM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ATMController : Controller
    {
        private ATMModel atm;
        public ActionResult ATMStart()
        {
            atm = new ATMModel(new List<string> { "ABank", "BBank", "CBank" }, 200);
            return View();
        }

        public ActionResult CheckCard(BankCardModel bankCard)
        {
            bool acceptedCard= atm.InsertCard(bankCard);

            if(acceptedCard)
            {
                //todo: this View is asking for pin
                return View();
            }
            else
            {
                //todo: page for invalid cards or cards for nonaccepted banks
                return View();
            }
        }

        [HttpGet]
        public ActionResult<bool> InsertPIN(BankCardModel bankCard, int pin)
        {
            if(atm.InputPin(bankCard, pin))
            {
               List<AccountModel> accounts= atm.SuccessfulLogin();
                //todo: view for selecting account
                return View(accounts);
            }
            return View();
        }

        [HttpGet]
        public ActionResult<string[]> SelectAccount(AccountModel account)
        {
            atm.SelectAccount(account);
            //todo: View with buttons for each action this ATM can perfom (currently: View Balance, Withdraw, Deposit)
            return View(atm.GetATMActions());
        }

        [HttpGet]
        public ActionResult<int> SeeBalance()
        {
            int accountBalance = atm.ViewBalance();
            //todo: View for displaying balance of an account
            return View(accountBalance);
        }

        [HttpPost]
        public ActionResult Deposit(int amount)
        {
            atm.DepositCash(amount);
            //todo: View for successful Deposit
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(int amount)
        {
            if (atm.WithdrawCash(amount))
            {
                //todo: View for successful Withdraw
                return View();
            }
            else
            {
                //todo: View for if the ATM does not have enough cash in the bin
                return View();
            }
        }
    }
}
