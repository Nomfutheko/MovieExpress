using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;
using Singular.Web.Data;
using MELib.RO;
using MELib.Security;
using Singular;
using MELib.Accounts;
using MELib.Products;
using MELib.Carts;
using MELib.Transaction;
#pragma warning disable CS0105 // The using directive for 'System.ComponentModel.DataAnnotations' appeared previously in this namespace
using System.ComponentModel.DataAnnotations;
#pragma warning restore CS0105 // The using directive for 'System.ComponentModel.DataAnnotations' appeared previously in this namespace


namespace MEWeb.Profile
{
  public partial class DepositFunds :MEPageBase<DepositFundsVM>
  {
  }
  public class DepositFundsVM : MEStatelessViewModel<DepositFundsVM>
  {

        public AccountList AccountList { get; set; }

        public MELib.Accounts.Account Account { get; set; }

        public MELib.Transaction.TransactionList TransactionList { get; set; }
        public MELib.Transaction.Transaction Transaction { get; set; }

        public String Amount111 { get; set; } = "235";
        public DateTime ReleaseFromDate { get; set; }
        public DateTime ReleaseToDate { get; set; }

        public int? AccountType { get; set; }


    public DepositFundsVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
            AccountList = MELib.Accounts.AccountList.GetAccountList();
    }

    [WebCallable]
    public static Singular.Web.Result SaveBalance(AccountList account, TransactionList transaction)
    {
            Result sr = new Result();

            var NewAccount = MELib.Accounts.AccountList.GetAccountList(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();


            NewAccount.UserID = Singular.Security.Security.CurrentIdentity.UserID;

            NewAccount.Balance += account.FirstOrDefault().Balance;

            NewAccount.TrySave(typeof(AccountList));

          

            //Transaction Transact = Transaction.NewTransaction();
            MELib.Transaction.Transaction Transact = new MELib.Transaction.Transaction();
            MELib.Transaction.TransactionList TransactionList = new MELib.Transaction.TransactionList();



            Transact.UserID = account.Select(c => c.UserID).FirstOrDefault();
            Transact.TransactionTypeID = 2;
            Transact.Amount = account.FirstOrDefault().Balance;
            // Transact.IsActiveInd = true;
            //Transact.TrySave(typeof(MELib.Transaction.TransactionList));
            //TransactionList.Add(Transaction);
            //TransactionList.TrySave();

            var transactResults = Transact.TrySave(typeof(MELib.Transaction.TransactionList));

            if (transactResults.Success)
            {
                sr.ErrorText = "Not enough stock to support purchase";
            }
            var test = TransactionList.GetTransactionList();

            return new Singular.Web.Result() { Success = true };

           
        }
  }
}

