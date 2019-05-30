// ================   BAD CODE   ===========================

public class Class1
{
  public decimal Calculate(decimal amount, int type, int years)
  {
    decimal result = 0;
    decimal disc = (years > 5) ? (decimal)5/100 : (decimal)years/100; 
    if (type == 1)
    {
      result = amount;
    }
    else if (type == 2)
    {
      result = (amount - (0.1m * amount)) - disc * (amount - (0.1m * amount));
    }
    else if (type == 3)
    {
      result = (0.7m * amount) - disc * (0.7m * amount);
    }
    else if (type == 4)
    {
      result = (amount - (0.5m * amount)) - disc * (amount - (0.5m * amount));
    }
    return result;
  }
}

//=============================    GOOD CODE    ===============================
public class DiscountManager
{
  public decimal ApplyDiscount(decimal price, int accountStatus, int timeOfHavingAccountInYears)
  {
    decimal priceAfterDiscount = 0;
    decimal discountForLoyaltyInPercentage = (timeOfHavingAccountInYears > 5) ? (decimal)5/100 : (decimal)timeOfHavingAccountInYears/100; 
    if (accountStatus == 1)
    {
      priceAfterDiscount = price;
    }
    else if (accountStatus == 2)
    {
      priceAfterDiscount = (price - (0.1m * price)) - (discountForLoyaltyInPercentage * (price - (0.1m * price)));
    }
    else if (accountStatus == 3)
    {
      priceAfterDiscount = (0.7m * price) - (discountForLoyaltyInPercentage * (0.7m * price));
    }
    else if (accountStatus == 4)
    {
      priceAfterDiscount = (price - (0.5m * price)) - (discountForLoyaltyInPercentage * (price - (0.5m * price)));
    }
 
    return priceAfterDiscount;
  }
}

// Ambigues 1,2,3,4 accounts .....lets replace them by enums.....
public enum AccountStatus
{
  NotRegistered = 1,
  SimpleCustomer = 2,
  ValuableCustomer = 3,
  MostValuableCustomer = 4
}

//==============================  REFACTORED   ============================

public class DiscountManager
{
  public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
  {
    decimal priceAfterDiscount = 0;
    decimal discountForLoyaltyInPercentage = (timeOfHavingAccountInYears > 5) ? (decimal)5/100 : (decimal)timeOfHavingAccountInYears/100;
 
    if (accountStatus == AccountStatus.NotRegistered)
    {
      priceAfterDiscount = price;
    }
    else if (accountStatus == AccountStatus.SimpleCustomer)
    {
      priceAfterDiscount = (price - (0.1m * price)) - (discountForLoyaltyInPercentage * (price - (0.1m * price)));
    }
    else if (accountStatus == AccountStatus.ValuableCustomer)
    {
      priceAfterDiscount = (0.7m * price) - (discountForLoyaltyInPercentage * (0.7m * price));
    }
    else if (accountStatus == AccountStatus.MostValuableCustomer)
    {
      priceAfterDiscount = (price - (0.5m * price)) - (discountForLoyaltyInPercentage * (price - (0.5m * price)));
    }
    return priceAfterDiscount;
  }
}

// Even more readable if-else if statment replaced with a switch-case statment...
public class DiscountManager
{
  public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
  {
    decimal priceAfterDiscount = 0;
    decimal discountForLoyaltyInPercentage = (timeOfHavingAccountInYears > 5) ? (decimal)5/100 : (decimal)timeOfHavingAccountInYears/100;
    switch (accountStatus)
    {
      case AccountStatus.NotRegistered:
        priceAfterDiscount = price;
        break;
      case AccountStatus.SimpleCustomer:
        priceAfterDiscount = (price - (0.1m * price));
        priceAfterDiscount = priceAfterDiscount - (discountForLoyaltyInPercentage * priceAfterDiscount);
        break;
      case AccountStatus.ValuableCustomer:
        priceAfterDiscount = (0.7m * price);
        priceAfterDiscount = priceAfterDiscount - (discountForLoyaltyInPercentage * priceAfterDiscount);
        break;
      case AccountStatus.MostValuableCustomer:
        priceAfterDiscount = (price - (0.5m * price));
        priceAfterDiscount = priceAfterDiscount - (discountForLoyaltyInPercentage * priceAfterDiscount);
        break;
      default:
        throw new NotImplementedException();			// Throwing Exception prevents AccountStatus not supported by enums above from returning 0 for the final purchase!!
		
    }
    return priceAfterDiscount;
  }
}