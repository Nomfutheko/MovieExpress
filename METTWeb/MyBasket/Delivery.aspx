<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delivery.aspx.cs" Inherits="MEWeb.MyBasket.Delivery" %>
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


                            //var Productist = RowCol.Helpers.TableFor<MELib.Carts.Carts>((c) => c.CartList, false, true);
                            //{

                            //    Productist.AddClass("table table-striped table-bordered table-hover");
                            //    var ProductistRow = Productist.FirstRow;
                            //    {
                            //        var ProductName = ProductistRow.AddColumn("Product Name");
                            //        {
                            //            var ProductNameText = ProductName.Helpers.Span(c => c.ProductName);
                            //        }

                            //        var ProductQuantity = ProductistRow.AddColumn("Quantity");
                            //        {
                            //            var ProductQuantityText = ProductQuantity.Helpers.EditorFor(c => c.Quantity);
                            //        }

                            //        var ProductPrice = ProductistRow.AddColumn("Price");
                            //        {
                            //            var ProductPriceText = ProductPrice.Helpers.Span(c => "R" + c.Price);
                            //        }


                            //        //var button1 = RowCol.Helpers.Button("Add Items", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                            //        //button.AddBinding(Singular.Web.KnockoutBindingString.click, "AddItems($data)");




                            //    }
                            var button2 = RowCol.Helpers.Button("Update Cart", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                            button2.AddBinding(Singular.Web.KnockoutBindingString.click, "SaveQuantity($data)");


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



                                            //RightColContentDiv.Helpers.LabelFor(c => ViewModel.Total);
                                            //var ReleaseFromDateEditor = RightColContentDiv.Helpers.Span(c => "R" + ViewModel.Total);
                                            //ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");


                                            //var button = RightColContentDiv.Helpers.Button(" Checkout", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                            //button.AddBinding(Singular.Web.KnockoutBindingString.click, "DeliveryOption($data)");

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

        //var AddItems = function () {
        //    window.location = '../Products/Products.aspx';
        //}

        function Checkout(data) {

            ViewModel.CallServerMethod('Delivery', { TTransactionList: ViewModel.TransactionList.Serialise() }, function (result) {
                if (result.Success) {

                    MEHelpers.Notification("Cart cleared.", 'center', 'info', 1000);
                    window.location = "../MyBasket/Checkout.aspx";
                }
                else {
                    Singular.AddMessage(1, 'Error', result.ErrorText).Fade(2000);
                }
            })
        }

        

        


    </script>
</asp:Content>
