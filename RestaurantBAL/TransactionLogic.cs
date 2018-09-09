using RestaurantDAL;
using restaurantBAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBAL
{
    public class TransactionLogic
    {
        Database restaurantDAL = new Database();
        DatabaseBL restaurantBAL = new DatabaseBL();

        public int Pay(TransactionTable transaction,Wallet wallet)
        {
            try { 
            int flag = ProcessTransaction(transaction, "Pay");
            if (flag == 1)
            {
                int flagInternal = restaurantBAL.CreateTransactionsTableEntry(transaction);
                if (flagInternal == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int Refund(TransactionTable transaction,Wallet wallet)
        {
            try { 

            int flag= ProcessTransaction(transaction, "Refund");
            if (flag == 1)
            {
                int flagInternal= restaurantBAL.CreateTransactionsTableEntry(transaction);
                if (flagInternal == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int ProcessTransaction(TransactionTable transaction,string transactionType)
        {
            try { 
            if (transactionType.Equals("Pay"))
            {
                Wallet depositor = restaurantBAL.FindWallet((int)(restaurantBAL.FindCustomer((int)transaction.Trans_From_Id).wallet_id));
                Wallet accepter = restaurantBAL.FindWallet((int)(restaurantBAL.FindCustomer((int)transaction.Trans_To_Id).wallet_id));
                int flag=DeductMoney((decimal)transaction.Trans_Amount, depositor.Wallet_Id);
                if (flag == 1)
                {
                    AddMoney((decimal)transaction.Trans_Amount, accepter.Wallet_Id);
                    return 1;
                }
                else
                {
                    AddMoney((decimal)transaction.Trans_Amount, depositor.Wallet_Id);
                    return 0;
                }
            }
            if (transactionType.Equals("Refund"))
            {
                Wallet depositor = restaurantBAL.FindWallet((int)(restaurantBAL.FindCustomer((int)transaction.Trans_To_Id).wallet_id));
                Wallet accepter = restaurantBAL.FindWallet((int)(restaurantBAL.FindCustomer((int)transaction.Trans_From_Id).wallet_id));
                int flag = DeductMoney((decimal)transaction.Trans_Amount, depositor.Wallet_Id);
                if (flag == 1)
                {
                    AddMoney((decimal)transaction.Trans_Amount, accepter.Wallet_Id);
                    return 1;
                }
                else
                {
                    AddMoney((decimal)transaction.Trans_Amount, depositor.Wallet_Id);
                    return 0;
                }
            }
            return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int AddMoney(decimal amount,int walletId)
        {
            try { 
            decimal addAmount = 0;
            addAmount=(decimal)(amount + restaurantBAL.FindWallet(walletId).Wallet_Amount);
            int flag = restaurantBAL.EditWallet(new Wallet { Wallet_Amount = addAmount, Last_Trans_Date_And_Time = DateTime.Now });
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int DeductMoney(decimal amount,int walletId)
        {
            try { 
            decimal deductAmount = 0;
            decimal walletAmount = (decimal)restaurantBAL.FindWallet(walletId).Wallet_Amount;
            int flag;
            if ((walletAmount - amount) > 0)
            {
                deductAmount = (decimal)(walletAmount - amount);
                flag = restaurantBAL.EditWallet(new Wallet { Wallet_Amount = deductAmount, Last_Trans_Date_And_Time = DateTime.Now });
            }
            else
            {
                deductAmount = walletAmount;
                flag = restaurantBAL.EditWallet(new Wallet { Wallet_Amount = deductAmount, Last_Trans_Date_And_Time = DateTime.Now });
            }
            
            return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}
