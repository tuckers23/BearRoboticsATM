namespace BearRoboticsATM.Models
{
    public class BankCardModel
    {
        private string bankName;
        private bool isValid;
        private List<AccountModel> accounts;
        private int pinAttempts;
        private bool lockedOut;
        private AccountModel? activeAccount= null;

        public BankCardModel(string bankName, List<AccountModel> accounts)
        {
            this.bankName = bankName;
            this.isValid = true;
            this.accounts = accounts;
            this.pinAttempts = 0;
            this.lockedOut = false;
        }

        public string GetBankName()
        {
            return this.bankName;
        }

        public List<AccountModel> GetAccounts()
        {
            return this.accounts;
        }

        public void CardExpired()
        {
            //todo: check expiration date etc
            this.isValid = false;
        }

        public bool IsValidCard()
        {
            return this.isValid && !this.lockedOut;
        }

        public bool CheckPIN(int pin)
        {
            //todo: call to send pin to Bank
            bool callToBank = false;

            if (pin == 1234)
            {
                callToBank = true;
                this.pinAttempts = 0;
            }
            else
            {
                this.pinAttempts++;
                if(this.pinAttempts == 3)
                {
                    //todo: notify bank of potential fraud
                    this.lockedOut = true;
                }
            }

            return callToBank;
        }

        public void AddAccount(AccountModel account)
        {
            this.accounts.Add(account);
        }

        public void SelectAccount(AccountModel account)
        {
            this.activeAccount = account;
        }

        public AccountModel? GetActiveAccount()
        {
            return this.activeAccount;
        }
    }
}
