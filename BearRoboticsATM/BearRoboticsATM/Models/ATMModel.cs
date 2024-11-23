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

        public void InsertCard(BankCardModel bankCard)
        {
            if(IsCardAccepted(bankCard))
            {
                if(bankCard.IsValidCard())
                {
                    //todo: prompt for pin
                }
                else
                {
                    //todo: prompt for non valid card or locked out card
                }
            }
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

        public void WithdrawCash(int amount)
        {
            if(amount> this.cashBinAmount)
            {
                //todo: logic for cashBin check
            }
            this.activeCard.GetActiveAccount().Withdraw(amount);
            this.cashBinAmount -= amount;
        }

        public void DepositCash(int amount)
        {
            this.activeCard.GetActiveAccount().Deposit(amount);
            this.cashBinAmount += amount;
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
