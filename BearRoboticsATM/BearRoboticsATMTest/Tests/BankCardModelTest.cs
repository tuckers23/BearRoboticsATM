using BearRoboticsATM.Models;

namespace BearRoboticsATMTest.Tests
{
    [TestClass]
    public sealed class BankCardModelTest
    {
        protected string bankName= "ABank";
        protected bool validCard= true;
        protected bool invalidCard = false;
        protected List<AccountModel> accounts = new List<AccountModel> { new AccountModel(123, 123), new AccountModel(456, 456), new AccountModel(789, 789)};
        protected int pinAttemptsLocked = 3;
        protected bool lockedOut = false;
        protected bool notLockedOut= false;
        protected AccountModel? activeAccount = null;
        protected int dummyPin= 1234;
        protected AccountModel newAccount = new AccountModel(321, 321);

        [TestMethod]
        public void BankCardModel_GetBankName()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            Assert.AreEqual(bankName, bankCard.GetBankName());
        }

        [TestMethod]
        public void BankCardModel_GetAccounts()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            Assert.AreEqual(accounts, bankCard.GetAccounts());
        }

        [TestMethod]
        public void BankCardModel_CardExpired()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            bankCard.CardExpired();
            Assert.AreEqual(invalidCard, bankCard.IsValidCard());
        }

        [TestMethod]
        public void BankCardModel_IsValidCard_True()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            Assert.AreEqual(validCard, bankCard.IsValidCard());
        }

        [TestMethod]
        public void BankCardModel_IsValidCard_LockedOut()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            for(int i= 0; i<= pinAttemptsLocked; i++)
            {
                bankCard.CheckPIN(0);
            }
            Assert.AreEqual(lockedOut, bankCard.IsValidCard());
        }

        [TestMethod]
        public void BankCardModel_CheckPin_True()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            Assert.AreEqual(true, bankCard.CheckPIN(dummyPin));
        }

        [TestMethod]
        public void BankCardModel_CheckPin_False()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            Assert.AreEqual(false, bankCard.CheckPIN(0));
        }


        [TestMethod]
        public void BankCardModel_AddAcount()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            bankCard.AddAccount(newAccount);
            Assert.AreEqual(accounts.Count- 1, bankCard.GetAccounts().IndexOf(newAccount));
        }


        [TestMethod]
        public void BankCardModel_SelectAccount_GetActiveAccount()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            bankCard.SelectAccount(newAccount);
            Assert.AreEqual(newAccount, bankCard.GetActiveAccount());
        }


        [TestMethod]
        public void BankCardModel_GetActiveAccount_DefaultNull()
        {
            var bankCard = new BankCardModel(bankName, accounts);
            Assert.AreEqual(null, bankCard.GetActiveAccount());
        }
    }
}
