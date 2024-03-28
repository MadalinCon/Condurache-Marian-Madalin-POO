#include "pch.h"
#include "LoyaltyScheme.h"
ref class CreditCardAccount
{
public:
    static int GetNumberOfAccounts();
    CreditCardAccount(long number, double limit);
    void SetCreditLimit(double amount);
    bool MakePurchase(double amount);
    void MakeRepayment(double amount);
    void PrintStatement();
    long GetAccountNumber();
    static CreditCardAccount();
    literal System::String^ name = "Super Platinum Card";
    void RedeemLoyaltyPoints();
private:
    LoyaltyScheme^ scheme;
    static int numberOfAccounts = 0;
    initonly long accountNumber;
    double currentBalance;
    double creditLimit;
    static double interestRate;

};