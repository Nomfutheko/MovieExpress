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
using System.ComponentModel.DataAnnotations;

namespace MEWeb.MyBasket
{

    public partial class MyBasket : MEPageBase<MyBasketVM>
    {

    }
    public class MyBasketVM : MEStatelessViewModel<MyBasketVM>
    {
        public MELib.Carts.ShoppingCartList ShoppingCartList { get; set; }
        public MELib.Carts.ShoppingCart ShoppingCart;
        public MELib.Carts.CartsList CartList { get; set; }
        public MELib.Carts.Carts Carts { get; set; }

        public MELib.Movies.MovieList MovieList { get; set; }
        public MELib.Products.Products Products { get; set; }
        public ProductsList ProductList { get; set; }

        public MELib.Products.ProductsList MynewProductList { get; set; }

        //public TransactionList TransactionList { get; set; }
        public MELib.Transaction.Transaction Transaction { get; set; }
        public MELib.Transaction.TransactionList TransactionList { get; set; }
        public MELib.Delivery.Delivery Delivery { get; set; }
        public MELib.Delivery.DeliveryList DeliveryList { get; set; }

        public MELib.Delivery.DeliveryType DeliveryType { get; set; }
        public MELib.Delivery.DeliveryTypeList DeliveryTypeList { get; set; }


        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.Delivery.DeliveryTypeList), UnselectedText = "Select", ValueMember = "DeliveryTypeID", DisplayMember = "DeliveryTypeName")]
        [Display(Name = "Delivery Type")]

        public int? DeliveryTypeID { get; set; }

        public string CartID { get; set; }
        public int? Quantity { get; set; }
        public int ProductID { get; set; }
        public int? AccountType { get; set; }
        public int? ProductCategory { get; set; }
        public int UserID { get; set; }
        public const string CartSessionkey = "CartID";
         public  decimal Total { get; set; }
       


        public System.DateTime DateCreated { get; set; }



        public MyBasketVM()
        {

        }

        protected override void Setup()
        {
            base.Setup();
            CartList = CartsList.GetCartsList(UserID);
            //Total = MELib.Carts.CartsList.GetCartsList(UserID).Sum(x => x.Price);
            MynewProductList = MELib.Products.ProductsList.GetProductsList();
            TransactionList = MELib.Transaction.TransactionList.GetTransactionList();
            //DeliveryTypeList = MELib.Delivery.DeliveryList.GetDeliveryList();
            ShoppingCartList = MELib.Carts.ShoppingCartList.GetShoppingCartList();

            foreach (var item in CartList)
            {
               
                
                    Total += item.Price;
                
            }

            


        }

        [WebCallable]
        public static Result SaveQuantity( int ProductID, CartsList CartsList, ProductsList ProductList, Decimal Total, int DeliveryTypeID)
        {


            Result sr = new Result();

            var ProductQuantity = ProductsList.GetProductsList(0, ProductID).Select(c => c.Quantity).FirstOrDefault();
            var product = ProductsList.GetProductsList();
            // Product.Price;
           // int StockQuantity = MELib.Products.ProductsList.GetProductsList().Where(c => c.ProductID == ProductID).Select(c => c.Quantity).FirstOrDefault();


            foreach (var item in CartsList)
                {
               
                    var OrderProduct = product.GetItem(item.ProductID);
                
                if (item.Quantity <= OrderProduct.Quantity)
                {

                    item.Price = OrderProduct.Price * item.Quantity;
                    

                    var savereult = CartsList.TrySave();
                }
                else
                {
                    return new Singular.Web.Result() { ErrorText = "not enough stock available,please change quantity.", Success = false };
                }
               
                }



            if (CartsList.IsValid)
            {
                var save = CartsList.TrySave();
            }
            else
            {
                sr.ErrorText = "Not enough stock to support purchase";
            }

            return new Result() { Success = true };
           


        }


        [WebCallable]
        public static Result Checkout(CartsList CartsList,int UserID,int ProductID ,ProductsList ProductsList, int DeliveryTypeID)
        {
            Result sr = new Result();

            decimal CartTotal;
            var OldBalance = MELib.Accounts.AccountList.GetAccountList().Select(c => c.Balance).FirstOrDefault();
            
               CartTotal = CartsList.GetCartsList().Sum(c => c.Price);
            

            if (CartTotal <= OldBalance)
            {
                var NewBalance = OldBalance - CartTotal;

                var AccountList = MELib.Accounts.AccountList.GetAccountList(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();
                AccountList.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                AccountList.Balance = NewBalance;
                AccountList.TrySave(typeof(MELib.Accounts.AccountList));


                MELib.Carts.ShoppingCart ShoppingCart = new MELib.Carts.ShoppingCart();
                MELib.Carts.ShoppingCartList ShoppingCartList = new MELib.Carts.ShoppingCartList();

                ShoppingCart.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                ShoppingCart.CartID = CartsList.Select(c => c.CartID).FirstOrDefault();
                ShoppingCart.Amount = CartTotal;
                ShoppingCart.TransactionTypeID = 1;

                // ShoppingCart.IsActiveInd = true; 
                //ShoppingCartList.Add(ShoppingCart);
                //var results = ShoppingCartList.TrySave();
                var results = ShoppingCart.TrySave(typeof(MELib.Carts.ShoppingCartList));

                

                MELib.Transaction.Transaction Transact = new MELib.Transaction.Transaction();
                MELib.Transaction.TransactionList TransactionList = new MELib.Transaction.TransactionList();


                //Transaction Transact = Transaction.NewTransaction();
                ShoppingCartList = MELib.Carts.ShoppingCartList.GetShoppingCartList();
                Transact.TransactionTypeID = 5;
                Transact.UserID = Singular.Security.Security.CurrentIdentity.UserID;

                if (DeliveryTypeID == 4)
                {
                    Transact.Amount = CartTotal + 25;
                }
                else
                {
                    Transact.Amount = CartTotal;
                }

                Transact.IsActiveInd = true;
                ///Transact.ShoppingCartID = ShoppingCart.ShoppingCartID;
                Transact.ShoppingCartID = ShoppingCartList.Select(c => c.ShoppingCartID).LastOrDefault();
                var transactResults = Transact.TrySave(typeof(MELib.Transaction.TransactionList));
                var test = MELib.Transaction.TransactionList.GetTransactionList();



                var ProductQuantity = ProductsList.GetProductsList(0, ProductID).Select(c => c.Quantity).FirstOrDefault();
                var Cartquantitty = CartsList.GetCartsList().Select(c => c.Quantity).FirstOrDefault();

                var Newproducts = MELib.Products.ProductsList.GetProductsList(0, ProductID);

                //ProductQuantity -= Cartquantitty;
                //ProductsList.TrySave();
                var product = ProductsList.GetProductsList();

                foreach (var item in CartsList)
                {

                    var OrderProduct = product.GetItem(item.ProductID);
                    OrderProduct.Quantity = OrderProduct.Quantity - item.Quantity;
                    OrderProduct.TrySave(typeof(ProductsList));

                }

                CartsList = CartsList.GetCartsList();
                CartsList.ToList().ForEach(c => { c.IsActiveInd = false; });
                CartsList.TrySave();
            }
            else
            {
                sr.ErrorText = "Insufficient funds";
            }

           
            return new Singular.Web.Result() { Success = true };


        }
    }
}