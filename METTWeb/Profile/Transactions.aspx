<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="MEWeb.Profile.Transactions" %>

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
                            var ContainerTab = PageTab.AddTab("Transaction History");
                            {
                                var RowContentDiv = ContainerTab.Helpers.DivC("row");
                                {
                                    //RowContentDiv.Helpers.HTML().Heading1("Transaction History");

                                    #region Left Column / Data
                                    var LeftColRight = RowContentDiv.Helpers.DivC("col-md-9");
                                    {


                                        var AnotherCardDiv = LeftColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                        {
                                            var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                            {
                                                CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                CardTitleDiv.Helpers.HTML().Heading5("Transactions");
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

                                            var TransactionDiv = AnotherCardDiv.Helpers.TableFor<MELib.Transaction.Transaction>((c) => c.TransactionList, false, false);
                                            {
                                                TransactionDiv.AddClass("table table-striped table-bordered table-hover");

                                                var FirstRow = TransactionDiv.FirstRow;
                                                {
                                                    var TransactionDateTitle = FirstRow.AddColumn("Transaction Date");
                                                    {
                                                        var TansactionAMTText = TransactionDateTitle.Helpers.Span(c => c.CreatedDate);
                                                        TansactionAMTText.Style.Width = "250px";
                                                    }

                                                    var TransactionTitle = FirstRow.AddColumn("Transaction");
                                                    {
                                                        var TransTitleText = TransactionTitle.Helpers.Span(c => c.TransactionTypeName);
                                                        TransactionTitle.Style.Width = "250px";
                                                    }

                                                    var TransactionAmtTitle = FirstRow.AddColumn("Transaction Amount");
                                                    {
                                                        var TansactionAMTText = TransactionAmtTitle.Helpers.Span(c => "R" + c.Amount);
                                                        TansactionAMTText.Style.Width = "250px";
                                                    }
                                                }
                                            }

                                        }
                                    }



                                    #endregion

                                    #region Right Column / Filters
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
                                                        RightColContentDiv.Helpers.LabelFor(c => ViewModel.TransactionTypeID);
                                                        var ReleaseFromDateEditor = RightColContentDiv.Helpers.EditorFor(c => ViewModel.TransactionTypeID);
                                                        ReleaseFromDateEditor.AddClass("form-control marginBottom20 ");

                                                        var FilterBtn = RightColContentDiv.Helpers.Button("Apply Filter", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                        {
                                                            FilterBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "FilterTransaction($data)");
                                                            FilterBtn.AddClass("btn btn-primary btn-outline");
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion
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

        var FilterTransaction = function (obj) {
            ViewModel.CallServerMethod('FilterTransaction', { TransactionTypeID: obj.TransactionTypeID(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Transactions filtered successfully.", 'center', 'info', 1000);
                    ViewModel.TransactionList.set(result.Data);
                    
                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            })
        }

    </script>
</asp:Content>
