<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StockLogs.aspx.cs" Inherits="Billing.Stock.StockLogs" %>
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
                                <div class="col-md-2 headerData"><b>ตั้งแต่วันที่ :</b></div>
                                <div class="col-md-3 rowData">
                                    <asp:TextBox ID="txtDateFrom" runat="server" class="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="ceDateFrom" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom" PopupPosition="TopLeft"></asp:CalendarExtender> 
                                </div>    
                                <div class="col-md-2 headerData"><b>ถึงวันที่ :</b></div>
                                <div class="col-md-3 rowData">
                                    <asp:TextBox ID="txtDateTo" runat="server" class="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="ceDateTo" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo" PopupPosition="TopLeft"></asp:CalendarExtender> 
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
                                    <strong>ประวัติรายการ</strong>
                                </div>
                                <asp:GridView ID="gv" runat="server" Width="100%" AutoGenerateColumns="False" >
                                    <Columns>   
                                        <asp:BoundField HeaderText="รับเข้าวันที่" DataField="StockTimeStr">
                                            <HeaderStyle CssClass="headerData text-center width15 " />
                                            <ItemStyle CssClass="text-center rowData"/>
                                        </asp:BoundField>   
                                        <asp:BoundField HeaderText="วันที่ทำรายการ" DataField="CreatedDateStr">
                                            <HeaderStyle CssClass="headerData text-center width15 " />
                                            <ItemStyle CssClass="text-center rowData"/>
                                        </asp:BoundField>                                     
                                        <asp:BoundField HeaderText="ประเภท" DataField="StockTypeDesc">
                                            <HeaderStyle CssClass="headerData text-center width20 " />
                                            <ItemStyle CssClass="text-left rowData"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ผู้ทำรายการ" DataField="CreatedBy">
                                            <HeaderStyle CssClass="headerData text-center width10 " />
                                            <ItemStyle CssClass="text-left rowData"/>
                                        </asp:BoundField>
                                        <%--<asp:BoundField HeaderText="จำนวน" DataField="AmountStr">
                                            <HeaderStyle CssClass="headerData text-center width10 " />
                                            <ItemStyle CssClass="text-center rowData"/>
                                        </asp:BoundField>   --%>                                                                             
                                        <asp:TemplateField HeaderText="Tools">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnView" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/search32x32.png"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StockHeaderID").ToString()%>'
                                                    CommandName='<%# DataBinder.Eval(Container.DataItem, "StockFrom").ToString()%>'
                                                    OnClick="imgbtnView_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="headerData text-center width10" />
                                            <ItemStyle CssClass="text-center rowData" />
                                        </asp:TemplateField>
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

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel1" TargetControlID="Label2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" Height="500px" Width="1100px" runat="server" Style="display: none;">
        <%--Style="display: none;"--%>
        <div class="panel panel-info-dark width100" style="min-height: 500px;">
            <div class="panel-heading text-left">
                <h3 class="panel-title">
                    <asp:Label ID="Label2" runat="server" CssClass="modalHeader" Text="List Item"></asp:Label>
                </h3>
            </div>           
            <div class="row" style="height:370px !important;">    
                <div class="col-md-1"></div>            
                <div class="col-md-10 rowData" style="overflow:auto; height:370px !important;">
                    <asp:GridView ID="gvItemStock" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField HeaderText="รหัสสินค้า" DataField="ProductCode">
                                <HeaderStyle CssClass="text-center width25 headerData" />
                                <ItemStyle CssClass="text-left rowData"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ชื่อสินค้า" DataField="ProductName">
                                <HeaderStyle CssClass="text-center width55 headerData" />
                                <ItemStyle CssClass="text-left rowData"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="จำนวน" DataField="AmountStr">
                                <HeaderStyle CssClass="text-center width10 headerData" />
                                <ItemStyle CssClass="text-center rowData"/>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#ff7777" />
                        <EmptyDataTemplate>
                            <table border="1" style="width:100%; padding:5px;">
                                <tr>
                                    <td>รหัสสินค้า</td>
                                    <td>ชื่อสินค้า</td>
                                    <td>จำนวน</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align:left;">
                                        No data.
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
                    <asp:Button ID="btnModalClose1" runat="server" CssClass="btn btn-save" Text="Close" OnClick="btnModalClose1_Click"/>
                </div>
            </div>
        </div>
        </div>
    </asp:Panel>
</asp:Content>

