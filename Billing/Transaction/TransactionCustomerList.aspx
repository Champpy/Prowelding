﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TransactionCustomerList.aspx.cs" Inherits="Billing.Transaction.TransactionCustomerList" %>
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
        function checkVat(rdb) {
            var txt = document.getElementById('ContentPlaceHolder1_txtVatNum');            
            if (rdb == "nonvat") {
                txt.disabled = 'true';
            } else {
                txt.disabled = '';
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
                            <asp:Label ID="lblHeader" runat="server" Text="">Sales History</asp:Label>
                        </strong>
                    </h3>
                </div>
                <div class="panel-body text-center" style="margin-top: 5px;">
                    <div style="padding: 0px 15px 15px 15px;">
                        <div class="row">
                            <div class="col-xs-2 headerData"><b>ชื่อลูกค้า :</b></div>
                            <div class="col-xs-4 rowData">
                                <asp:TextBox ID="txtCustName" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="col-xs-2 headerData"><b></b></div>
                            <div class="col-xs-4 rowData">
                                <%--<asp:TextBox ID="txtSN" runat="server" class="form-control"></asp:TextBox>--%>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 15px;">
                            <div class="col-xs-2"></div>
                            <div class="col-xs-8">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-default" OnClick="btnSearch_Click"/>
                                
                            </div>
                            <div class="col-xs-2">
                            </div>
                        </div>
                        <div class="row" style="margin-top: 15px; max-height:350px; overflow:auto;">
                            <div class="col-xs-12">
                                <div class="alert alert-info dark text-left">
                                    <strong>List Sales History</strong>
                                </div>
                                <asp:GridView ID="gv" runat="server" Width="100%" AutoGenerateColumns="False">
                                    <Columns>                                        
                                        <asp:BoundField HeaderText="วันที่" DataField="ReceivedDateStr">
                                            <HeaderStyle CssClass="text-center width8 headerData" />
                                            <ItemStyle CssClass="text-center rowData"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="เลขที่" DataField="SaleNumber">
                                            <HeaderStyle CssClass="text-center width8 headerData" />
                                            <ItemStyle CssClass="text-center rowData"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ชื่อลูกค้า" DataField="CustomerName">
                                            <HeaderStyle CssClass="text-center width20 headerData" />
                                            <ItemStyle CssClass="text-left rowData"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="เบอร์โทรศัพท์" DataField="Tel">
                                            <HeaderStyle CssClass="text-center width10 headerData" />
                                            <ItemStyle CssClass="text-left rowData"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="สินค้า" DataField="ItemCode">
                                            <HeaderStyle CssClass="text-center width20 headerData" />
                                            <ItemStyle CssClass="text-left rowData"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ยอดเงิน" DataField="SaleAmountStr">
                                            <HeaderStyle CssClass="text-center width9 headerData" />
                                            <ItemStyle CssClass="text-right rowData"/>
                                        </asp:BoundField>
                                        <%--<asp:TemplateField HeaderText="Tools">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/Print.jpg"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SaleHeaderID").ToString()%>'
                                                    CommandName='<%# DataBinder.Eval(Container.DataItem, "BillType").ToString()%>'
                                                    OnClick="imgbtnPrint_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="imgbtnEdit" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SaleHeaderID").ToString()%>'
                                                    OnClick="imgbtnEdit_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/icon_delete.gif" 
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SaleHeaderID").ToString()%>'                                                    
                                                    OnClick="imgbtnDelete_Click" OnClientClick="return confirm('ยืนยันการลบข้อมูล?');"/>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="text-center width10" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    No data.
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-1"></div>  
    </div>    
</asp:Content>
