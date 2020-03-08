<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StockTransProductSerial.aspx.cs" Inherits="Billing.Stock.StockTransProductSerial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-1"></div>
        <div class="col-xs-10">
            <div class="panel panel-info panel-info-dark" style="min-height: 370px;">
                <div class="panel-heading panel-heading-dark text-left">
                    <h3 class="panel-title">
                        <strong>
                            <asp:Label ID="lblHeader" runat="server" Text="">ประวัติสินค้า เข้า-ออก คลังขาย</asp:Label>
                        </strong>
                    </h3>
                </div>
                <div class="panel-body text-center">
                    <div style="padding: 0px 25px 5px 25px;">
                        <div class="row tab tab-border">
                            <div class="row">
                                <div class="text-left" style="height:25px; padding-left:35px;">
                                    <strong>Header</strong>
                                    <asp:HiddenField ID="hddID" runat="server" />
                                </div>
                            </div>     
                            <div class="row width99" style="padding-left:35px;">
                                <div class="col-md-1 headerData"><b>ชื่อเครื่อง  :</b></div>
                                <div class="col-md-3 rowData">
                                    <asp:TextBox ID="txtProductName" runat="server" class="form-control"></asp:TextBox>                                  
                                </div>    
                                <div class="col-md-1 headerData"><b>S/N</b></div>
                                <div class="col-md-3 rowData">
                                    <asp:TextBox ID="txtSerialNumber" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1 headerData"><b>Status</b></div>
                                <div class="col-md-3 rowData">
                                    <asp:CheckBox ID="chkStatus" runat="server" Checked="true" />
                                </div>
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
                                    <strong>Serial</strong>
                                </div>
                                <asp:GridView ID="gv" runat="server" Width="100%" AutoGenerateColumns="False" >
                                    <Columns>   
                                        <asp:BoundField HeaderText="ชื่อเครื่อง" DataField="ProductName">
                                            <HeaderStyle CssClass="headerData text-center width25 " />
                                            <ItemStyle CssClass="text-left rowData"/>
                                        </asp:BoundField>   
                                        <asp:BoundField HeaderText="Serial Number" DataField="SerialNumber">
                                            <HeaderStyle CssClass="headerData text-center width5 " />
                                            <ItemStyle CssClass="text-center rowData"/>
                                        </asp:BoundField>                                     
                                        <asp:BoundField HeaderText="Status" DataField="Status">
                                            <HeaderStyle CssClass="headerData text-center width5 " />
                                            <ItemStyle CssClass="text-center rowData"/>
                                        </asp:BoundField>    
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

