using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Singular.Web;
using MELib.Carts;




namespace MEWeb.Products
{
    public partial class Products : MEPageBase<ProductsVM>
    {

    }
    public class ProductsVM : MEStatelessViewModel<ProductsVM>
    {
        //Properties & Objects
        public MELib.Products.ProductsList ProductList { get; set;}

        public MELib.Products.Products Products { get; set; }

        public DateTime ReleaseFromDate { get; set; }
        public DateTime ReleaseToDate { get; set; }

        public MELib.Carts.CartsList CartList  { get; set; }
        public MELib.Carts.Carts Carts { get; set; }

        
        //public MELib.Products.Products Products { get; set; }
    
        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.ProductCategoriesList), UnselectedText = "Select", ValueMember = "ProductCategoriessID", DisplayMember = "CategoryName")]
        [Display(Name = "Category")]

        public int ProductCategoryID { get; set;}
        public int ProductID { get; set; }

        public bool DisableProductInd { get; set; }

        //public ProductsVM()
        //{
        //}
        protected override void Setup()
        {
            base.Setup();

            ProductList = MELib.Products.ProductsList.GetProductsList();
            CartList = MELib.Carts.CartsList.GetCartsList();
            Carts = CartList.FirstOrDefault();
            DisableProductInd = false;


            // On page load initiate/set your data/variables and or properties here
            // Should pass in criteria for the specific user that is viewing the page, however using current identity

        }


        [WebCallable]
        public Result FilterProducts(int ProductCategoryID)
        {
            Result sr = new Result();
            try
            {
                sr.Data = MELib.Products.ProductsList.GetProductsList(ProductCategoryID, 0);

                sr.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page:Products.aspx | Method: FilterProducts", $"(int ProductCategoriesID, ({ProductCategoryID})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter Products by category.";
                sr.Success = false;
            }
            return sr;
        }

        [WebCallable]
        public static Result AddToCart(int ProductID)
        {
            Result sr = new Result();

            var price = MELib.Products.ProductsList.GetProductsList().Where(c => c.ProductID == ProductID).Select(c => c.Price).FirstOrDefault();
            var productlist= MELib.Products.ProductsList.GetProductsList();
            int StockQuantity = MELib.Products.ProductsList.GetProductsList().Where(c => c.ProductID == ProductID).Select(c => c.Quantity).FirstOrDefault();

            if (StockQuantity > 0)
            {
                Carts basket = Carts.NewCarts();

                //MELib.Carts.CartsList Cart = new MELib.Carts.CartsList();

                basket.ProductID = ProductID;
                basket.IsActiveInd = true;
                basket.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                basket.Price = price;
                basket.TrySave(typeof(CartsList));
            }
            else
            {
                return new Singular.Web.Result() { ErrorText = "No stock available for this Product.", Success = false };
            }
            //foreach (var item in CartsList)
            //{
            //    if (basket.ProductID == item.ProductID)
            //    {
            //        return new Singular.Web.Result() { ErrorText = "Item already exists in the Basket", Success = false };
            //    }

            //}
            //Cart.Add(basket);
           

            var test = CartsList.GetCartsList();

            

            return new Singular.Web.Result() { Success = true };

        }
    }
}