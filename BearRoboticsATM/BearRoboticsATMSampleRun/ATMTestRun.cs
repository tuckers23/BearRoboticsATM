using BearRoboticsATM.Controllers;
using BearRoboticsATM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.ProjectModel;
using NuGet.Protocol;

namespace BearRoboticsATM
{
    public class ATMTestRun
    {
        public static void Main()
        {
            try
            {
                ATMController controller = new ATMController();
                AccountModel account = new AccountModel(123, 100);
                AccountModel secondAccount = new AccountModel(456, 50);
                BankCardModel bankCard = new BankCardModel("ABank", new List<AccountModel> { account, secondAccount });

                controller.ATMStart();
                Console.WriteLine("Please Insert Card (Press any key)\n");
                Console.ReadLine();
                controller.CheckCard(bankCard);

                Console.WriteLine("Please Input Your PIN\n");
                string pin = Console.ReadLine();
                List<AccountModel> accounts = null;
                var returnedAccounts = controller.InputPIN(bankCard, int.Parse(pin)) as ViewResult;
                accounts = returnedAccounts.ViewData.Model as List<AccountModel>;

                while (accounts == null)
                {
                    Console.WriteLine("\nThat Pin Is Incorrect. Please Input Your PIN Again\n");
                    pin = Console.ReadLine();
                    returnedAccounts = controller.InputPIN(bankCard, int.Parse(pin)) as ViewResult;
                    accounts = returnedAccounts.ViewData.Model as List<AccountModel>;
                }

                AccountModel selectedAccount = null;
                while(selectedAccount == null)
                {
                    Console.WriteLine("\nPlease Select An Account From The List By Inputting The Number To The Left Of The Account Number");
                    for (int i = 0; i < accounts.Count; i++)
                    {
                        AccountModel acc = accounts[i];
                        Console.WriteLine($"{i + 1}: {acc.GetAccountNumber()}");
                    }
                    Console.WriteLine("");
                    int inputAccount = int.Parse(Console.ReadLine());
                    if(inputAccount< 0|| inputAccount> accounts.Count+ 1)
                    {
                        continue;
                    }
                    selectedAccount = accounts[inputAccount- 1];
                    controller.SelectAccount(selectedAccount);
                }

                Console.WriteLine($"\nPlease Choose An Action For Account {selectedAccount.GetAccountNumber()} By Inputting The Number To The Left Of The Option:\n" +
                    $"1. See Balance\n" +
                    $"2. Deposit\n" +
                    $"3. Withdraw\n" +
                    $"4. Exit\n");
                int option = int.Parse(Console.ReadLine());
                while (option != 4)
                {
                    switch (option)
                    {
                        case 1:
                            var balance = controller.SeeBalance() as ViewResult;
                            Console.WriteLine($"\nBalance For Account {selectedAccount.GetAccountNumber()} is ${balance.ViewData.Model}\n");
                            break;

                        case 2:
                            Console.WriteLine("\nHow Much Would You Like To Deposit? Please Enter A Whole Number\n");
                            int depositAmount = int.Parse(Console.ReadLine());
                            var balanceAfterDeposit = controller.Deposit(depositAmount) as ViewResult;
                            Console.WriteLine($"After Deposit The New Balance For Account {selectedAccount.GetAccountNumber()} is ${balanceAfterDeposit.ViewData.Model}\n");
                            break;

                        case 3:
                            Console.WriteLine("\nHow Much Would You Like To Withdraw? Please Enter A Whole Number\n");
                            int withdrawAmount = int.Parse(Console.ReadLine());
                            var balanceAfterWithdraw = controller.Withdraw(withdrawAmount) as ViewResult;
                            if(int.Parse(balanceAfterWithdraw.ViewData.Model.ToString())== -1)
                            {
                                Console.WriteLine($"Sorry, Account {selectedAccount.GetAccountNumber()} Does Not Have Enough Money To Withdraw That Amount.\n");
                            }
                            else
                                Console.WriteLine($"After Withdraw The New Balance For Account {selectedAccount.GetAccountNumber()} is ${balanceAfterWithdraw.ViewData.Model}\n");
                            break;
                    }

                    Console.WriteLine($"Would You Like To Do Anything Else With Account {selectedAccount.GetAccountNumber()}? Please Input The Number To The Left Of The Option:\n" +
                        $"1. See Balance\n" +
                        $"2. Deposit\n" +
                        $"3. Withdraw\n" +
                        $"4. Exit\n");

                    option = int.Parse(Console.ReadLine());
                }
                Console.WriteLine("Thank You For Using Bear Robitic's ATM. Goodbye. ");
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Sorry, That Is Not How The ATM Works. Place Start Over And Try Again.");
                Console.ReadLine();
            }
        }
    }
}
