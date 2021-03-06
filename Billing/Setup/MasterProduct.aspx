﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MasterProduct.aspx.cs" Inherits="Billing.Setup.MasterProduct" %>

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
            <div class="panel panel-info" style="min-height: 500px;">
                <div class="panel-heading panel-heading-dark text-left">
                    <h3 class="panel-title">
                        <strong>
                            <asp:Label ID="lblHeader" runat="server" Text="">Search Product</asp:Label>
                        </strong>
                    </h3>
                </div>
                <div class="panel-body" style="margin-top: 5px;">
                    <div style="padding: 0px 15px 15px 15px;">
                        <div class="row">
                            <div class="col-xs-2 headerData"><b>รหัสสินค้า :</b></div>
                            <div class="col-xs-4 rowData">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-xs-2 headerData"><b>สินค้า :</b></div>
                            <div class="col-xs-4 rowData">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 15px;">
                            <div class="col-xs-4"></div>
                            <div class="col-xs-2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-default" OnClick="btnSearch_Click" />
                            </div>
                            <div class="col-xs-2">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btnAdd_Click" />
                            </div>
                            <div class="col-xs-4">
                            </div>
                        </div>
                        <div class="row" style="margin-top: 15px; max-height: 350px; overflow: auto;">
                            <div class="col-xs-12">
                                <div class="alert alert-info dark text-left">
                                    <strong>List Product</strong>
                                </div>
                                <asp:GridView ID="gv" runat="server" Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField HeaderText="IDสินค้า" DataField="ProductID" Visible="False">
                                            <HeaderStyle CssClass="headerData text-center width15" />
                                            <ItemStyle CssClass="rowData text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="รหัสสินค้า" DataField="ProductCode">
                                            <HeaderStyle CssClass="headerData text-center width15" />
                                            <ItemStyle CssClass="rowData text-left paddingLeft5" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="สินค้า" DataField="ProductName">
                                            <HeaderStyle CssClass="headerData text-center width27" />
                                            <ItemStyle CssClass="rowData text-left paddingLeft5" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField HeaderText="ราคาซื้อ" DataField="PurchasePrice" DataFormatString="{0:N2}">
                                            <HeaderStyle CssClass="headerData text-center width10" />
                                            <ItemStyle CssClass="rowData text-right paddingRight5" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField HeaderText="คงเหลือ" DataField="Remaining" DataFormatString="{0:N0}">
                                            <HeaderStyle CssClass="headerData text-center width5" />
                                            <ItemStyle CssClass="rowData text-right paddingRight5" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField HeaderText="ราคาขาย" DataField="SellPrice" DataFormatString="{0:N2}">
                                            <HeaderStyle CssClass="text-center width10" />
                                            <ItemStyle CssClass="text-right" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Tools">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hddGID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ItemID").ToString()%>' />--%>
                                                <asp:ImageButton ID="imgbtnEdit" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID").ToString()%>'
                                                    OnClick="imgbtnEdit_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/icon_delete.gif"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID").ToString()%>'
                                                    OnClick="imgbtnDelete_Click" OnClientClick="return confirm('ยืนยันการลบข้อมูล?');" />
                                                <%-- OnClientClick="return confirm('คุณต้องการเปลี่ยนสถานะข้อมูลนี้หรือไม่ ?');" OnClick="imgbtnDelete_Click" ToolTip="เปลี่ยนสถานะ"
                                                    Visible='<%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Name").ToString()) ? false : true %>'--%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="headerData text-center width10" />
                                            <ItemStyle CssClass="rowData text-center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">No data.
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                                <%--<uc1:ucpaging ID="ucPaging1" runat="server" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-1"></div>
    </div>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel1" TargetControlID="lbl_modal_view">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" Height="450px" Width="800px" runat="server" Style="display: none;">
        <%--Style="display: none;"--%>
        <div class="panel panel-info-dark width98" style="min-height: 450px;">
            <div class="panel-heading text-left">
                <h3 class="panel-title">
                    <asp:Label ID="lbl_modal_view" runat="server" CssClass="modalHeader" Text="Manage Product"></asp:Label>
                </h3>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-1"></div>
                <div class="col-md-2 headerData"><b>รหัสสินค้า :</b></div>
                <div class="col-md-8 rowData">
                    <asp:TextBox ID="txtMCode" runat="server" Width="95%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2 headerData"><b>สินค้า :</b></div>
                <div class="col-md-8 rowData">
                    <asp:TextBox ID="txtMName" runat="server" Width="95%" CssClass="form-control"></asp:TextBox>
                    <asp:HiddenField ID="hddMode" runat="server" />
                    <asp:HiddenField ID="hddID" runat="server" />
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2 headerData"><b>กลุ่มสินค้า :</b></div>
                <div class="col-md-8 rowData">
                    <asp:DropDownList ID="ddlType" runat="server" AppendDataBoundItems="true" Width="95%" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2 headerData"><b>หน่วยสินค้า :</b></div>
                <div class="col-md-8 rowData">
                    <asp:DropDownList ID="ddlUnit" runat="server" AppendDataBoundItems="true" Width="95%" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2 headerData"><b>S/N :</b></div>
                <div class="col-md-8 rowData">
                    <asp:CheckBox ID="chkSN" runat="server" />
                </div>
                <div class="col-md-1"></div>
            </div>
            <%--<div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2 headerData"><b>ราคาซื้อ :</b></div>
                <div class="col-md-8 rowData">
                    <asp:TextBox ID="txtMPurchasePrice" runat="server" Width="95%" onKeyPress="keyintdot()"></asp:TextBox>
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2 headerData"><b>ราคาขาย :</b></div>
                <div class="col-md-8 rowData">
                    <asp:TextBox ID="txtMPrice" runat="server" Width="95%" onKeyPress="keyintdot()"></asp:TextBox>
                </div>
                <div class="col-md-1"></div>
            </div>--%>
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnModalSave" runat="server" CssClass="btn btn-default" Text="Save" OnClick="btnModalSave_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnModalClose" runat="server" CssClass="btn btn-default" Text="Close"></asp:Button>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
