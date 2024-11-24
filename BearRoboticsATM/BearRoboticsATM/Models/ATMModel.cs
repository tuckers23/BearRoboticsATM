namespace BearRoboticsATM.Models
{
    public class ATMModel
    {
        //todo: potential switch to BankModel if more information is needed beyond the bank's name
        private List<string> acceptedBanks = new List<string>();
        private string[] atmActions = new string[] { "See Balance", "Deposit", "Withdraw" };
        private BankCardModel? activeCard = null;
        private int cashBinAmount;

        public ATMModel(List<string> acceptedBanks, int cashBinAmount)
        {
            this.acceptedBanks = acceptedBanks;
            this.cashBinAmount = cashBinAmount;
        }

        public bool InsertCard(BankCardModel bankCard)
        {
            if(IsCardAccepted(bankCard))
            {
                if(bankCard.IsValidCard())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool InputPin(BankCardModel bankCard, int pin)
        {
            if(bankCard.CheckPIN(pin))
            {
                this.activeCard = bankCard;
                return true;
            }
            return false;
        }

        public List<AccountModel> SuccessfulLogin()
        {
            return this.activeCard.GetAccounts();
        }

        public void SelectAccount(AccountModel account)
        {
            this.activeCard.SelectAccount(account);
        }

        public int WithdrawCash(int amount)
        {
            if(amount> this.cashBinAmount)
            {
                //todo: logic for cashBin check
                return -1;
            }
            int newBalance= this.activeCard.GetActiveAccount().Withdraw(amount);
            this.cashBinAmount -= amount;
            return newBalance;
        }

        public int DepositCash(int amount)
        {
            int newBalance= this.activeCard.GetActiveAccount().Deposit(amount);
            this.cashBinAmount += amount;

            return newBalance;
        }

        public int ViewBalance()
        {
            return this.activeCard.GetActiveAccount().GetBalance();
        }

        public string[] GetATMActions()
        {
            return this.atmActions;
        }

        public BankCardModel ActiveBankCard()
        {
            return this.activeCard;
        }

        public int CashBinAmount()
        {
            return this.cashBinAmount;
        }

        public void AddBank(string bank)
        {
            this.acceptedBanks.Add(bank);
        }

        public void RemoveBank(string bank)
        {
            this.acceptedBanks.Remove(bank);
        }

        public List<string> AcceptedBanks()
        {
            return this.acceptedBanks;
        }

        private bool IsCardAccepted(BankCardModel bankCard)
        {
            return this.acceptedBanks.Contains(bankCard.GetBankName());
        }
    }
}
