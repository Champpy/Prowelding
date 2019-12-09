<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MasterPackage.aspx.cs" Inherits="Billing.Setup.MasterPackage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function keyintdot() {
            var key = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if ((key < 48 || key > 57) && key != 46) { //46 = "."
                event.returnValue = false;
            }
        }
        function keyintNodot() {
            var key = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if ((key < 48 || key > 57)) {
                event.returnValue = false;
            }
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-1"></div>
        <div class="col-xs-10">
            <div class="panel panel-info panel-info-dark" style="min-height: 370px;">
                <div class="panel-heading panel-heading-dark text-left">
                    <h3 class="panel-title">
                        <strong>
                            <asp:Label ID="lblHeader" runat="server" Text="">Package </asp:Label>
                        </strong>
                    </h3>
                </div>
                <div class="panel-body text-center">
                    <div style="padding: 0px 25px 5px 25px;">
                        <div class="row tab tab-border">
                            <div class="row">
                                <div class="text-left" style="height: 25px; padding-left: 35px;">
                                    <strong>Header</strong>
                                    <asp:HiddenField ID="hddID" runat="server" />
                                    <asp:HiddenField ID="hddHeaderMode" Value="Add" runat="server" />
                                </div>
                            </div>
                            <div class="row width99" style="padding-left: 35px;">
                                <div class="col-xs-2 headerData"><b>รหัส Package :</b></div>
                                <div class="col-xs-4 rowData">
                                    <asp:TextBox ID="txtPackageCode" runat="server" Width="90%"></asp:TextBox>
                                </div>
                                <div class="col-xs-2 headerData"><b>ชื่อ Package :</b></div>
                                <div class="col-xs-4 rowData">
                                    <asp:TextBox ID="txtPackageName" runat="server" Width="90%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row width99" style="padding-left: 35px;">
                                <div class="col-xs-2 headerData"><b>ราคาขาย :</b></div>
                                <div class="col-xs-4 rowData">
                                    <asp:TextBox ID="txtPackageSellPrice" runat="server" Width="90%" onKeyPress="keyintNodot()"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row tab tab-border">
                            <div class="row">
                                <asp:HiddenField ID="hddProductMode" Value="Add" runat="server" />
                                <div class="row">
                                    <div class="text-left" style="height: 25px; padding-left: 35px;">
                                        <strong>Detail</strong>

                                    </div>
                                </div>

                                <div class="text-right col-xs-12" style="height: 25px; padding-right: 40px; margin-top: 10px;">
                                    <asp:Button ID="btnAddModal" runat="server" Text="Add Detail" CssClass="btn btn-default" OnClick="btnAddModal_Click" />
                                </div>
                            </div>
                            <div class="row width99" style="padding-left: 35px; padding-top: 20px;">
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvProdcutDetail" runat="server" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField HeaderText="รหัสสินค้า" DataField="ProductCode">
                                                <HeaderStyle CssClass="text-center width25 headerData" />
                                                <ItemStyle CssClass="text-left rowData" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="ชื่อสินค้า" DataField="ProductName">
                                                <HeaderStyle CssClass="text-center width25 headerData" />
                                                <ItemStyle CssClass="text-left rowData" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField HeaderText="ราคา/หน่วย" DataField="SellPrice" DataFormatString="{0:N2}">
                                                <HeaderStyle CssClass="text-center width10 headerData" />
                                                <ItemStyle CssClass="text-right rowData" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField HeaderText="จำนวน" DataField="Amount">
                                                <HeaderStyle CssClass="text-center width7 headerData" />
                                                <ItemStyle CssClass="text-center rowData" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="CanChange" DataField="CanChange">
                                                <HeaderStyle CssClass="text-center width7 headerData" />
                                                <ItemStyle CssClass="text-center rowData" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Tools">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID").ToString()%>'
                                                        OnClick="imgbtnEdit_Click" />
                                                    &nbsp;
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/icon_delete.gif"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID").ToString()%>'
                                                        OnClick="imgbtnDelete_Click" OnClientClick="return confirm('ยืนยันการลบข้อมูล?');" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="text-center width9 headerData" Height="30px" />
                                                <ItemStyle CssClass="text-center rowData" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="1" style="width: 100%; padding: 5px;">
                                                <tr>
                                                    <td>สินค้า</td>
                                                    <td>ราคา</td>
                                                    <td>จำนวน</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align: left;">No data.
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 15px;">
                            <div class="col-xs-2"></div>
                            <div class="col-xs-8">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click" />
                                &nbsp;&nbsp;&nbsp;
                               <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default" PostBackUrl="/Setup/MasterPackageList.aspx" />
                            </div>
                            <div class="col-xs-2">
                            </div>
                        </div>
                        <div class="row">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-1"></div>
    </div>



    <%--List Item (Package)--%>
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel3" TargetControlID="lbl_modal_view">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel3" Height="600px" Width="1100px" runat="server" Style="display: none;">
        <%--Style="display: none;"--%>
        <div class="panel panel-info-dark width100" style="min-height: 600px;">
            <div class="panel-heading text-left">
                <h3 class="panel-title">
                    <asp:Label ID="lbl_modal_view" runat="server" CssClass="modalHeader" Text="List Item"></asp:Label>
                </h3>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-1"></div>
                <div class="col-md-2 headerData"><b>รหัสสินค้า :</b></div>
                <div class="col-md-3 rowData">
                    <asp:TextBox ID="txtSearchProductCode" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2 headerData"><b>ชื่อสินค้า :</b></div>
                <div class="col-md-3 rowData">
                    <asp:TextBox ID="txtSearchProductName" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row" style="height: 400px !important;">
                <div class="col-md-1"></div>
                <div class="col-md-10 rowData" style="overflow: auto; height: 400px !important;">
                    <asp:GridView ID="gvItemSearch" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField HeaderText="รหัสสินค้า" DataField="ProductCode">
                                <HeaderStyle CssClass="text-center width15 headerData" />
                                <ItemStyle CssClass="text-left rowData" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ชื่อสินค้า" DataField="ProductName">
                                <HeaderStyle CssClass="text-center width25 headerData" />
                                <ItemStyle CssClass="text-left rowData" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ราคาซื้อ" DataField="PurchasePrice" DataFormatString="{0:N2}">
                                <HeaderStyle CssClass="text-center width15 headerData" />
                                <ItemStyle CssClass="text-center rowData" />
                            </asp:BoundField>
                            <%--<asp:BoundField HeaderText="ราคาขาย" DataField="SellPrice" DataFormatString="{0:N2}">
                                <HeaderStyle CssClass="text-center width15 headerData" />
                                <ItemStyle CssClass="text-center rowData" />
                            </asp:BoundField>--%>

                            <asp:TemplateField HeaderText="Tools">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hddItemID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductID").ToString()%>' />

                                    <asp:ImageButton ID="imgbtnChooseItem" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        OnClick="imgbtnChooseItem_Click" />

                                </ItemTemplate>
                                <HeaderStyle CssClass="text-center width5 headerData" Height="30px" />
                                <ItemStyle CssClass="text-center rowData" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#ff7777" />
                        <EmptyDataTemplate>
                            <table border="1" style="width: 100%; padding: 5px;">
                                <tr>
                                    <td>รหัสสินค้า</td>
                                    <td>ชื่อสินค้า</td>
                                    <td>ราคา</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left;">No data.
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnMItemSearch" runat="server" CssClass="btn btn-save" Text="Search" OnClick="btnMItemSearch_Click" />
                    <asp:Button ID="btnModalClose3" runat="server" CssClass="btn btn-save" Text="Close" OnClick="btnModalClose3_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>


    <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel4" TargetControlID="lbl_modal_view">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel4" Height="300px" Width="1100px" runat="server" Style="display: none;">
        <%--Style="display: none;"--%>
        <div class="panel panel-info-dark width100" style="min-height: 300px;">
            <div class="panel-heading text-left">
                <h3 class="panel-title">
                    <asp:Label ID="Label1" runat="server" CssClass="modalHeader" Text="List Item"></asp:Label>
                </h3>
            </div>
            <div class="row width99" style="padding-left: 35px;">
                <div class="col-xs-2 headerData"><b>รหัสสินค้า :</b></div>
                <div class="col-xs-4 rowData">
                    <asp:HiddenField ID="hddProductID" runat="server" />
                    <asp:TextBox ID="txtProductCode" runat="server" Width="90%" Enabled="False"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnSearchProduct" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"
                        OnClick="imgbtnSearchProduct_Click" />
                </div>
                <div class="col-xs-2 headerData"><b>ชื่อสินค้า:</b></div>
                <div class="col-xs-4 rowData">
                    <asp:TextBox ID="txtProductName" runat="server" Width="90%" Enabled="False"></asp:TextBox>
                </div>
            </div>
            <div class="row width99" style="padding-left: 35px;">
                <%--<div class="col-xs-2 headerData"><b>ราคาขาย :</b></div>
                <div class="col-xs-4 rowData">
                    <asp:TextBox ID="txtProductSellPrice" runat="server" Width="90%" Enabled="False"></asp:TextBox>
                </div>--%>
                <div class="col-xs-2 headerData"><b>CanChange :</b></div>
                <div class="col-xs-4 rowData">
                    <asp:CheckBox ID="ChkCanChange" runat="server" />
                </div>
                <div class="col-xs-2 headerData"><b>จำนวน :</b></div>
                <div class="col-xs-4 rowData">
                    <asp:TextBox ID="txtProductAmount" runat="server" Width="90%" onKeyPress="keyintNodot()"></asp:TextBox>
                </div>
            </div>
            <%--<div class="row width99" style="padding-left: 35px;">
                <div class="col-xs-2 headerData"><b>CanChange :</b></div>
                <div class="col-xs-4 rowData">
                    <asp:CheckBox ID="ChkCanChange" runat="server" />
                </div>
            </div>--%>

            <div class="row">&nbsp;</div>
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSaveProductDetail" runat="server" CssClass="btn btn-save" Text="Save" OnClick="BtnSaveProductDetail_Click" />
                    <asp:Button ID="BtnCloseProductDetail" runat="server" CssClass="btn btn-save" Text="Close" OnClick="BtnCloseProductDetail_Click"  />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

