using BearRoboticsATM.Models;

namespace BearRoboticsATMTest.Tests
{
    [TestClass]
    public sealed class AccountModelTest
    {
        protected int accountNumber = 123;
        protected int balance = 100;
        protected int depositAmount = 20;
        protected int withdrawAmountSucced = 50;
        protected int withdrawAmountFail = 150;

        [TestMethod]
        public void AccountModel_GetBalance()
        {
            var account = new AccountModel(accountNumber, balance);
            Assert.AreEqual(balance, account.GetBalance());
        }

        [TestMethod]
        public void AccountModel_Deposit()
        {
            var account = new AccountModel(accountNumber, balance);
            account.Deposit(depositAmount);
            Assert.AreEqual(balance + depositAmount, account.GetBalance());
        }

        [TestMethod]
        public void AccountModel_WithdrawSucceed()
        {
            var account = new AccountModel(accountNumber, balance);
            account.Withdraw(withdrawAmountSucced);
            Assert.AreEqual(balance - withdrawAmountSucced, account.GetBalance());
        }

        [TestMethod]
        public void AccountModel_WithdrawFail()
        {
            var account = new AccountModel(accountNumber, balance);
            account.Withdraw(withdrawAmountFail);
            Assert.AreEqual(balance, account.GetBalance());
        }
    }
}
