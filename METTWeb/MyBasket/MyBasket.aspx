<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyBasket.aspx.cs" Inherits="MEWeb.MyBasket.MyBasket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/JSLINQ.js"></script>
    <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
    <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%
        using (var h = this.Helpers)
        {

            var MainHDiv = h.DivC("row pad-top-10");
            {
                var PanelContainer = MainHDiv.Helpers.DivC("col-md-12 p-n-lr");
                {
                    var RowCol = PanelContainer.Helpers.DivC("col-md-6");
                    {
                        RowCol.Helpers.HTML().Heading2("Shopping Basket");


                        var Productist = RowCol.Helpers.TableFor<MELib.Carts.Carts>((c) => c.CartList, false, true);
                        {

                            Productist.AddClass("table table-striped table-bordered table-hover");
                            var ProductistRow = Productist.FirstRow;
                            {
                                var ProductName = ProductistRow.AddColumn("Product Name");
                                {
                                    var ProductNameText = ProductName.Helpers.Span(c => c.ProductName);
                                }

                                var ProductQuantity = ProductistRow.AddColumn("Quantity");
                                {
                                    var ProductQuantityText = ProductQuantity.Helpers.EditorFor(c => c.Quantity);
                                }

                                var ProductPrice = ProductistRow.AddColumn("Price");
                                {
                                    var ProductPriceText = ProductPrice.Helpers.Span(c => "R" + c.Price);
                                }


                                var button1 = RowCol.Helpers.Button(" Continue Shoppinng ", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                button1.AddBinding(Singular.Web.KnockoutBindingString.click, "shopping($data)");

                                var button2 = RowCol.Helpers.Button("Update Cart", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                    button2.AddBinding(Singular.Web.KnockoutBindingString.click, "SaveQuantity($data)");




                            }



                        }

                        var RowColRight1 = PanelContainer.Helpers.DivC("col-md-3");
                        {

                            var AnotherCardDiv = RowColRight1.Helpers.DivC("ibox float-e-margins paddingBottom");
                            {
                                var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                {
                                    CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                    CardTitleDiv.Helpers.HTML().Heading5("Delivery Options");
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
                                    //{
                                    //    var thandi = RightRowContentDiv.Helpers.BootstrapTableFor<MELib.Delivery.DeliveryTypeList>((c) => c.DeliveryTypeList, false, false, "");
                                    //    {
                                    //var tt = thandi.FirstRow;
                                    //{
                                    var RightColContentDiv = RightRowContentDiv.Helpers.DivC("col-md-12");
                                    {

                                        //var RightRContentDiv = ContentDiv.Helpers.DivC("row");
                                        //{

                                        RightColContentDiv.Helpers.LabelFor(c => ViewModel.DeliveryTypeID);
                                        var MovieTitleEditor = RightColContentDiv.Helpers.EditorFor(c => ViewModel.DeliveryTypeID );
                                        MovieTitleEditor.AddClass("form-control marginBottom20");
                                        //    MovieTitleEditor.AddBinding(Singular.Web.KnockoutBindingString.id, "Delivery T");
                                        //}
                                    }
                                    //}
                                    //}
                                    RightRowContentDiv.Helpers.DivC("row");


                                    
                                    //var button = RightColContentDiv.Helpers.Button("Proceed to Checkout", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                    //button.AddBinding(Singular.Web.KnockoutBindingString.click, "Checkout($data)");
                                }



                            }
                        }

                    }


                    var RowColRight = PanelContainer.Helpers.DivC("col-md-3");
                    {

                        var AnotherCardDiv = RowColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                        {
                            var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                            {
                                CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                CardTitleDiv.Helpers.HTML().Heading5("Basket Summary");
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
                                        //var RightRContentDiv = ContentDiv.Helpers.DivC("row");
                                        //{

                                        //    RightColContentDiv.Helpers.LabelFor(c => ViewModel.DeliveryTypeID);
                                        //    var MovieTitleEditor = RightColContentDiv.Helpers.EditorFor(c => c.DeliveryTypeID);
                                        //    MovieTitleEditor.AddClass("form-control marginBottom20 filterBox");
                                        //    MovieTitleEditor.AddBinding(Singular.Web.KnockoutBindingString.id, "Delivery T");
                                        //}

                                        RightColContentDiv.Helpers.DivC("row");

                                        RightColContentDiv.Helpers.LabelFor(c => ViewModel.Total);
                                        var ReleaseFromDateEditor = RightColContentDiv.Helpers.Span(c => "R" + ViewModel.Total);
                                        ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");




                                        var button = RightColContentDiv.Helpers.Button(" Checkout", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                        button.AddBinding(Singular.Web.KnockoutBindingString.click, "Checkout($data)");

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
        Singular.OnPageLoad(function () {
            $("#menuItem5").addClass("active");
            $("#menuItem5 > ul").addClass("in");
        });

        var shopping = function () {

            window.location = '../Products/Products.aspx';
        }
       

        var Checkout =  function (obj) {

            ViewModel.CallServerMethod('Checkout', { CartsList: ViewModel.CartList.Serialise(), UserID: obj.UserID(), ProductsList: ViewModel.ProductsList, DeliveryTypeID: obj.DeliveryTypeID()}, function (result) {
                if (result.Success) {

                    MEHelpers.Notification("Cart cleared.", 'center', 'info', 1000);

                    Singular.ShowMessage('Checkout', 'Thank you for your Purchace,Your order is being preperd. You Total including delivery fee is  R' + obj.Total(), 20000);
                    //window.location = "../MyBasket/MyBasket.aspx";
                }
                else {
                    Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                }
            })
        }



        var SaveQuantity = function (obj) {
            ViewModel.CallServerMethod('SaveQuantity', { ProductID: obj.ProductID(), CartsList: ViewModel.CartList.Serialise(), ProductsList: ViewModel.ProductsList, Total: obj.Total(), DeliveryTypeID: obj.DeliveryTypeID(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification(".Cart updated", 'center', 'info', 1000);

                    window.location = "../MyBasket/MyBasket.aspx";

                   
                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000)
                }
            })
        }


    </script>
</asp:Content>
