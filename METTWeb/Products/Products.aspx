<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="MEWeb.Products.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <!-- Add page specific styles and JavaScript classes below -->
  <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
  <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
  <!-- Placeholder not used in this example -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <%
      using (var h = this.Helpers)
      {
          var MainContent = h.DivC("row pad-top-10");
          {
              var MainContainer = MainContent.Helpers.DivC("col-md-12 p-n-lr");
              {
                  var PageContainer = MainContainer.Helpers.DivC("tabs-container");
                  {
                      var PageTab = PageContainer.Helpers.TabControl();
                      {
                          PageTab.Style.ClearBoth();
                          PageTab.AddClass("nav nav-tabs");
                          var ContainerTab = PageTab.AddTab("Available Products");
                          {
                              var RowContentDiv = ContainerTab.Helpers.DivC("row");
                              {
                                  var ColContentDiv = RowContentDiv.Helpers.DivC("col-md-9");
                                  {

                                      var ProductsDiv = ColContentDiv.Helpers.BootstrapTableFor<MELib.Products.Products>((c) => c.ProductList, false, false,"");
                                      {
                                          var FirstRow = ProductsDiv.FirstRow;
                                          {
                                              var ProductTitle = FirstRow.AddColumn("Title");
                                              {
                                                  var ProductTitleText = ProductTitle.Helpers.Span(c => c.ProductName);
                                                  ProductTitle.Style.Width = "250px";
                                              }
                                              var ProductDescription = FirstRow.AddColumn("Description");
                                              {
                                                  var ProductDescriptionText = ProductDescription.Helpers.Span(c => c.Description);
                                              }

                                              var QuantityDescription = FirstRow.AddColumn("Available stock");
                                              {
                                                  var QuantityDescriptionText = QuantityDescription.Helpers.Span(c => c.Quantity);
                                              }

                                              var ProductAction = FirstRow.AddColumn("");
                                              {
                                                  // Watch Movie
                                                  var AddtoCartButton = ProductAction.Helpers.Button("Add to Cart", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                  {
                                                      AddtoCartButton.AddBinding(Singular.Web.KnockoutBindingString.text, c => "Buy Now @ R " + c.Price);
                                                      AddtoCartButton.AddBinding(Singular.Web.KnockoutBindingString.click,"AddToCart($data)");
                                                      AddtoCartButton.AddBinding(Singular.Web.KnockoutBindingString.disable, c => ViewModel.DisableProductInd);
                                                      AddtoCartButton.AddClass("btn btn-primary btn-outline");
                                                  }
                                              }
                                          }
                                      }


                                  }
                                  var RowColRight = RowContentDiv.Helpers.DivC("col-md-3");
                                  {

                                      var AnotherCardDiv = RowColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                      {
                                          var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                          {
                                              CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                              CardTitleDiv.Helpers.HTML().Heading5("Filter Criteria");
                                          }
                                          var CardTitleToolsDiv = CardTitleDiv.Helpers.DivC("ibox-tools");
                                          {
                                              var aToolsTag = CardTitleToolsDiv.Helpers.HTMLTag("a");
                                              aToolsTag.AddClass("collapse-link");
                                              {
                                                  var iToolsTag = aToolsTag.Helpers.HTMLTag("i");
                                                  iToolsTag.AddClass("fa fa-chevron-up");
                                              }
                                          }
                                          var ContentDiv = AnotherCardDiv.Helpers.DivC("ibox-content");
                                          {
                                              var RightRowContentDiv = ContentDiv.Helpers.DivC("row");
                                              {
                                                  var RightColContentDiv = RightRowContentDiv.Helpers.DivC("col-md-12");
                                                  {
                                                      RightColContentDiv.Helpers.LabelFor(c => ViewModel.ProductCategoryID);
                                                      var ReleaseFromDateEditor = RightColContentDiv.Helpers.EditorFor(c => ViewModel.ProductCategoryID);
                                                      ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");

                                                      var FilterBtn = RightColContentDiv.Helpers.Button("Apply Filter", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                      {
                                                          FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterProducts($data)");
                                                          FilterBtn.AddClass("btn btn-primary btn-outline");
                                                      }

                                                  }
                                              }
                                          }
                                      }

                                      var FilterBtn2 = RowContentDiv.Helpers.Button("Go to Shopping Basket", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                      {
                                          FilterBtn2.AddBinding(Singular.Web.KnockoutBindingString.click, "PCheckout($data)");
                                          FilterBtn2.AddClass("btn btn-primary btn-outline");
                                      }
                                  }
                              }
                          }
                      }
                  }
              }
          }
      }
  %>
  <script type="text/javascript">
      // Place page specific JavaScript here or in a JS file and include in the HeadContent section
      Singular.OnPageLoad(function () {
          $("#menuItem1").addClass('active');
          $("#menuItem1 > ul").addClass('in');
      });

    

      var FilterProducts = function (obj) {
          ViewModel.CallServerMethod('FilterProducts', { ProductCategoryID: obj.ProductCategoryID() }, function (result) {
              if (result.Success) {
                  MEHelpers.Notification("Products filtered successfully.", 'center', 'info', 1000);
                  ViewModel.ProductList.Set(result.Data);
                  ViewModel.di
              }
              else {
                  MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
              }
          })
      }


      var AddToCart = function (obj) {
          ViewModel.CallServerMethod('AddToCart', { ProductID: obj.ProductID() }, function (result) {
              if (result.Success) {
                  MEHelpers.Notification("Product added to cart.", 'center', 'info', 1000);
                  //if (ProductID = 16) {
                  //    ViewModel.ProductList().ProductID.DisableProductInd(true);
                  //}
                
              }
              else {
                  MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000)
              }
          })
      }


      var PCheckout = function () {
          window.location = '../MyBasket/MyBasket.aspx';
      }

  </script>
</asp:Content>
