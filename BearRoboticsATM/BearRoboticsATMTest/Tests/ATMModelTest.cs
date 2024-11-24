using BearRoboticsATM.Models;

namespace BearRoboticsATMTest.Tests
{
    [TestClass]
    public sealed class ATMModelTest
    {
        protected List<string> acceptedBanks = new List<string>{"ABank", "BBank", "CBank" };
        protected string[] atmActions = new string[] { "See Balance", "Deposit", "Withdraw" };
        protected BankCardModel? activeCard = null;
        protected int cashBinAmount = 50;
        protected int withdrawAmount = 20;
        protected int overWithdrawAmount = 100;
        protected int depositAmount = 10;
        protected BankCardModel acceptedBankCard = new BankCardModel("ABank", new List<AccountModel>() { new AccountModel(123, 123)});
        protected List<AccountModel> accetpedBankCardAccounts = new List<AccountModel>() { new AccountModel(123, 123) };
        protected BankCardModel nonAcceptedBankCard = new BankCardModel("DBank", new List<AccountModel>());
        protected int dummyPin = 1234;
        protected AccountModel accountModel = new AccountModel(123, 123);

        [TestMethod]
        public void ATMModel_InputPin_True()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            Assert.AreEqual(true, atm.InputPin(acceptedBankCard, dummyPin));
        }

        [TestMethod]
        public void ATMModel_InputPin_False()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            Assert.AreEqual(false, atm.InputPin(acceptedBankCard, 0));
        }

        [TestMethod]
        public void ATMModel_SuccesfulLogin()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            Assert.AreEqual(accetpedBankCardAccounts[0].GetAccountNumber(), atm.SuccessfulLogin()[0].GetAccountNumber());
        }

        [TestMethod]
        public void ATMModel_SelectAccount()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            atm.SelectAccount(accountModel);
            Assert.AreEqual(accountModel, atm.ActiveBankCard().GetActiveAccount());
        }

        [TestMethod]
        public void ATMModel_WithdrawCash_CashBinAmount()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            atm.SelectAccount(accountModel);
            atm.WithdrawCash(withdrawAmount);
            Assert.AreEqual(cashBinAmount- withdrawAmount, atm.CashBinAmount());
        }

        [TestMethod]
        public void ATMModel_WithdrawCash_AccountAmount()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            atm.SelectAccount(accountModel);
            Assert.AreEqual(accountModel.GetBalance()- withdrawAmount, atm.WithdrawCash(withdrawAmount));
        }

        [TestMethod]
        public void ATMModel_WithdrawCash_Fail()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            atm.SelectAccount(accountModel);
            Assert.AreEqual(-1, atm.WithdrawCash(overWithdrawAmount));
        }

        [TestMethod]
        public void ATMModel_DepositCash_CashBinAmount()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            atm.SelectAccount(accountModel);
            atm.DepositCash(depositAmount);
            Assert.AreEqual(cashBinAmount + depositAmount, atm.CashBinAmount());
        }

        [TestMethod]
        public void ATMModel_DepositCash_AccountAmount()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            atm.SelectAccount(accountModel);
            Assert.AreEqual(accountModel.GetBalance()+ depositAmount, atm.DepositCash(depositAmount));
        }

        [TestMethod]
        public void ATMModel_GetATMActions()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            Assert.AreEqual(atmActions[0], atm.GetATMActions()[0]);
        }

        [TestMethod]
        public void ATMModel_ActiveBankCard()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            Assert.AreEqual(acceptedBankCard, atm.ActiveBankCard());
        }

        [TestMethod]
        public void ATMModel_AddBank()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.AddBank(nonAcceptedBankCard.GetBankName());
            Assert.AreEqual(nonAcceptedBankCard.GetBankName(), atm.AcceptedBanks()[atm.AcceptedBanks().Count - 1]);
        }

        [TestMethod]
        public void ATMModel_RemoveBank()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.RemoveBank(acceptedBankCard.GetBankName());
            Assert.AreEqual(-1, atm.AcceptedBanks().IndexOf(acceptedBankCard.GetBankName()));
        }

        [TestMethod]
        public void ATMModel_InsertCard_Accepted()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            Assert.AreEqual(true, atm.InsertCard(acceptedBankCard));
        }

        [TestMethod]
        public void ATMModel_InsertCard_Declined()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            Assert.AreEqual(false, atm.InsertCard(nonAcceptedBankCard));
        }

        [TestMethod]
        public void ATMModel_ViewBalance()
        {
            var atm = new ATMModel(acceptedBanks, cashBinAmount);
            atm.InputPin(acceptedBankCard, dummyPin);
            atm.SelectAccount(accountModel);
            Assert.AreEqual(accountModel.GetBalance(), atm.ViewBalance());
        }
    }
}