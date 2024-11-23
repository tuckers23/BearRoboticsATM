namespace BearRoboticsATM.Models
{
    public class ATMModel
    {
        //todo: potential switch to BankModel if more information is needed beyond the bank's name
        private List<string> acceptedBanks = new List<string>();
        private string[] atmActions = new string[] { "See Balance", "Deposit", "Withdraw" };
        private BankCardModel? activeCard = null;

        public ATMModel(List<string> acceptedBanks)
        {
            this.acceptedBanks = acceptedBanks;
        }

        public void AddBank(string bank)
        {
            this.acceptedBanks.Add(bank);
        }

        public void InsertCard(BankCardModel bankCard)
        {
            if(IsCardAccepted(bankCard))
            {
                if(bankCard.IsValidCard())
                {
                    //todo: prompt for pin
                    //if pin match
                    SuccessfulLogin(bankCard);
                }
                else
                {
                    //todo: prompt for non valid card or locked out card
                }
            }
        }

        public bool InputPin(BankCardModel bankCard, int pin)
        {
            return bankCard.CheckPIN(pin);
        }

        public void SuccessfulLogin(BankCardModel bankCard)
        {
            this.activeCard = bankCard;
        }

        private bool IsCardAccepted(BankCardModel bankCard)
        {
            return this.acceptedBanks.Contains(bankCard.GetBankName());
        }
    }
}
