using System;
using Singular.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web.Data;
using MELib.RO;
using MELib.Security;
using Singular;
using MELib.Products;
using MELib.Carts;
using MELib.Transaction;

namespace MEWeb.MyBasket
{
    public partial class Delivery : MEPageBase<DeliveryVM>
    {
    }
    public class DeliveryVM : MEStatelessViewModel<DeliveryVM>
    {
        public TransactionList TransactionList { get; set; }
        public MELib.Transaction.Transaction Transaction { get; set; }


        public string CartID { get; set; }
        public int UserID { get; set; }
        public DeliveryVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            TransactionList = MELib.Transaction.TransactionList.GetTransactionList();


        }



        [WebCallable]
        public static Result DeliveryOption(TransactionList TransactionList)
        {
#pragma warning disable CS0168 // The variable 'Amount' is declared but never used
            decimal Amount;
#pragma warning restore CS0168 // The variable 'Amount' is declared but never used
            
            return new Result() { Success = true };
        }
    }
}
