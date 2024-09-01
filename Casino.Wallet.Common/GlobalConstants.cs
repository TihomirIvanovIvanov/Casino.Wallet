namespace Casino.Wallet.Common
{
    public static class GlobalConstants
    {
        public const decimal MinBet = 1m;
        public const decimal MaxBet = 10m;
        public const int LoseChance = 50;
        public const int DoubleChance = 40;
        public const int HighMultiplierChance = 10;
        public const int MinBigWinMultiplier = 2;
        public const int MaxBigWinMultiplier = 10;

        public const string CurrentBalanceMsg = "Your current balance is:";
        public const string CongratsMsg = "Congrats - you won";
        public const string InsufficientFundsMsg = "Insufficient funds.";
        public const string GoodbyeMsg = "Thank you for playing! Hope to see you soon.";
        public const string UnknownCmd = "Unknown command.";
        public const string NegativeDepositAmountMsg = "Deposit amount must be positive.";
        public const string NegativeWithdrawAmountMsg = "Withdrawal amount must be positive.";
        public const string InvalidBetMsg = "Invalid bet amount. Please bet between";
        public const string NoLuckMsg = "No luck this time!";
    }
}
