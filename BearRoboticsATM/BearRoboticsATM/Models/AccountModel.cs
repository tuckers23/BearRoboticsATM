namespace BearRoboticsATM.Models
{
    public class AccountModel
    {
        private int accountNumber;
        private int balance;
        
        public AccountModel(int accountNumber, int balance)
        {
            this.accountNumber = accountNumber;
            this.balance = balance;
        }
        public int GetAccountNumber()
        {
            return this.accountNumber;
        }

        public int GetBalance()
        { 
            return balance;
        }

        public int Deposit(int amount)
        {
            this.balance += amount;
            return balance;
        }

        public int Withdraw(int amount)
        {
            if(amount > balance)
            {
                return -1;
            }
            balance -= amount;
            return balance;
        }
    }
}
