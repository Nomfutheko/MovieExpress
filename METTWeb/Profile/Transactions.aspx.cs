using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;

namespace MEWeb.Profile
{
    public partial class Transactions : MEPageBase<TransactionsVM>
    {
    }
    public class TransactionsVM : MEStatelessViewModel<TransactionsVM>
    {
        public MELib.Transaction.TransactionList TransactionList { get; set; }
        public MELib.Transaction.TransactionTypeList TransactionTypeList { get; set; }

        public MELib.Carts.ShoppingCartList ShoppingcartList { get; set; }

        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.Transaction.TransactionTypeList), UnselectedText = "Select", ValueMember = "TransactionTypeID", DisplayMember = "TransactionTypeName")]
        [Display(Name = "Transaction Type")]

        public int? TransactionTypeID { get; set; }
        public string TransactionType { get; set; }
        public String Amount { get; set; }

        public int UserID { get; set; }
        public TransactionsVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            TransactionList = MELib.Transaction.TransactionList.GetTransactionList();
            ShoppingcartList = MELib.Carts.ShoppingCartList.GetShoppingCartList();
        }



        [WebCallable]
        public Result FilterTransaction(int TransactionTypeID)
        {
            Result sr = new Result();
            try
            {
                //sr.Data = MELib.Products.ProductsList.GetProductsList(ProductCategoryID,0);
                sr.Data = MELib.Transaction.TransactionList.GetTransactionList(0,TransactionTypeID);

                sr.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page:Transactions.aspx | Method: FilterTransaction", $"(int TransactionTypeID, ({TransactionTypeID})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter Products by category.";
                sr.Success = false;
            }
            return sr;
        }
    }
}

    




