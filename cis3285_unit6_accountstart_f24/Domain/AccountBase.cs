using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Shared account state and behavior; each account type supplies its own reward rule.
    public abstract class AccountBase
    {
        // Create the requested concrete account while returning the common base type.
        public static AccountBase CreateAccount(AccountType type)
        {
            AccountBase account = null;
            switch(type)
            {
                case AccountType.Silver:
                    account = new SilverAccount();
                    break;
                case AccountType.Gold:
                    account = new GoldAccount();
                    break;
                case AccountType.Platinum:
                    account = new PlatinumAccount();
                    break;
            }
            return account;
        }

        public decimal Balance
        {
            get;
            private set;
        }

        public int RewardPoints
        {
            get;
            private set;
        }

        public void AddTransaction(decimal amount)
        {
            if (amount < 0)
            {
                // Withdrawals do not earn points, so just change the balance.
                Balance += amount;
            }
            else if (amount >= 0)
            {
                // Calculate points before changing Balance so account rules use the prior balance.
                RewardPoints += CalculateRewardPoints(amount);
                Balance += amount;
            }
        }

        // Subclasses implement the reward calculation for their account type.
        public abstract int CalculateRewardPoints(decimal amount);
    }
}
