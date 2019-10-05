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
    <script>
        $(document).ready(function () {
            SetPayType();

            $("#ddlPay").change(function () {
                SetPayType();
            });
            
        });
        function SetPayType() {
            if ($("#ddlPay").val() == "5") {
                $("#dvTransfer").show();
                $("#dvInst").hide();
                $("#dvNull").hide();
                $("#txtTimeTransfer").prop("disabled", false);
                $("#ddlAccountInst").val('');
            } else if ($("#ddlPay").val() == "8") {
                $("#dvNull").hide();
                $("#dvTransfer").hide();
                $("#dvInst").show();
                $("#txtTimeTransfer").prop("disabled", true);
                $("#txtTimeTransfer").val('');
                $("#ddlAccount").val('');
            } else {
                $("#dvNull").show();
                $("#dvTransfer").hide();
                $("#dvInst").hide();
                $("#txtTimeTransfer").prop("disabled", true);
                $("#txtTimeTransfer").val('');
                $("#ddlAccount").val('');
                $("#ddlAccountInst").val('');
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
                            <asp:Label ID="lblHeader" runat="server" Text="">Package</asp:Label>
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
                                <div class="col-xs-2 headerData"><b>Code :</b></div>
                                <div class="col-xs-4 rowData">
                                    <asp:TextBox ID="txtSaleNumber" runat="server" Width="90%"></asp:TextBox>
                                </div> 
                                <div class="col-xs-2 headerData"><b>Name :</b></div>
                                <div class="col-xs-4 rowData">
                                    <asp:TextBox ID="txtTel" runat="server" Width="90%"></asp:TextBox>
                                </div>                    
                            </div> 
                            <div class="row width99" style="padding-left:35px;">                                
                                <div class="col-xs-2 headerData"><b>Sell Price :</b></div>
                                <div class="col-xs-4 rowData">
                                    <asp:TextBox ID="txtSellPrice" runat="server" Width="90%"></asp:TextBox>
                                </div> 
                                <div class="col-xs-2 headerData"><b></b></div>
                                <div class="col-xs-4 rowData">
                                </div>                    
                            </div> 
                        </div>

                        <div class="row tab tab-border">
                            <div class="row">
                                <div class="text-left col-xs-6" style="height:25px; padding-left:35px;">
                                    <strong>Package Detail</strong>                                    
                                </div>
                                <div class="text-right col-xs-6" style="height:25px; padding-right:40px;">
                                    <asp:Button ID="btnAddModal" runat="server" Text="Add Detail" CssClass="btn btn-default" OnClick="btnAddModal_Click"/>
                                </div>
                            </div>
                            <div class="row width99" style="padding-left:35px; padding-top:20px;">                              
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvItem" runat="server" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField HeaderText="Order" DataField="">
                                                <HeaderStyle CssClass="text-center width5 headerData" />
                                                <ItemStyle CssClass="text-right rowData"/>
                                            </asp:BoundField> 
                                            <asp:BoundField HeaderText="Product Code" DataField="">
                                                <HeaderStyle CssClass="text-center width25 headerData" />
                                                <ItemStyle CssClass="text-left rowData"/>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Product Name" DataField="">
                                                <HeaderStyle CssClass="text-center width25 headerData" />
                                                <ItemStyle CssClass="text-left rowData"/>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount" DataField="">
                                                <HeaderStyle CssClass="text-center width7 headerData" />
                                                <ItemStyle CssClass="text-center rowData"/>
                                            </asp:BoundField>                                     
                                            <asp:TemplateField HeaderText="Tools">
                                                <ItemTemplate>
                                                    <%--<asp:ImageButton ID="imgbtnEdit" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SaleDetailID").ToString()%>'
                                                        OnClick="imgbtnEdit_Click" />
                                                    &nbsp;
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/icon_delete.gif" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SaleDetailID").ToString()%>'
                                                        OnClick="imgbtnDelete_Click" OnClientClick="return confirm('ยืนยันการลบข้อมูล?');"/>--%>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="text-center width9 headerData" Height="30px"/>
                                                <ItemStyle CssClass="text-center rowData" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="1" style="width:100%; padding:5px;">
                                                <tr>
                                                    <td>Order</td>
                                                    <td>Product Code</td>
                                                    <td>Product Name</td>
                                                    <td>Amount</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:left;">
                                                        No data.
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
                               <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default" PostBackUrl="/Setup/MasterPackageList.aspx"/>
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

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel1" TargetControlID="lbl_modal_view">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" Height="90%" Width="90%" runat="server" Style="display: none;">
        <%--Style="display: none;"--%>
        <div class="panel panel-info-dark width100" style="min-height: 90%;">
            <div class="panel-heading text-left">
                <h3 class="panel-title">
                    <asp:Label ID="lbl_modal_view" runat="server" CssClass="modalHeader" Text="Description"></asp:Label>
                </h3>
            </div>
            <div class="row" style="margin-top: 20px;">    
                <div class="col-md-1"></div>            
                <div class="col-md-1 headerData"><b>สินค้า :</b></div>
                <div class="col-md-4 rowData">
                    <asp:HiddenField ID="hddDetailID" runat="server" />
                    <%--<asp:DropDownList ID="ddlMItem" runat="server" AutoPostBack="true" class="form-control" 
                        OnSelectedIndexChanged="ddlMItem_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                    <asp:HiddenField ID="hddItemID" runat="server" />
                    <asp:HiddenField ID="hddStockID" runat="server" />
                    <asp:TextBox ID="txtMItem" runat="server" Width="92%" ReadOnly="true"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnSearchItem_Click" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"                                        
                        OnClick="imgbtnSearchItem_Click_Click" />
                </div>
                <div class="col-md-1 headerData"><b>ราคา/หน่วย :</b></div>
                <div class="col-md-4 rowData">
                    <asp:TextBox ID="txtMPrice" runat="server" onKeyPress="keyintdot()"></asp:TextBox>
                </div>                
                <div class="col-md-1"></div>
            </div>      
            <div class="row">    
                <div class="col-md-1"></div>            
                <div class="col-md-1 headerData"><b>จำนวน :</b></div>
                <div class="col-md-4 rowData">
                    <asp:TextBox ID="txtMAmount" runat="server" onKeyPress="kteeyintNodot()"></asp:TextBox>
                </div>
                <div class="col-md-1 headerData">                    
                    <%--<b>ส่วนลด (%) :</b>--%>
                </div>
                <div class="col-md-4 rowData">
                    <%--<asp:TextBox ID="txtMDiscountPer" runat="server" onKeyPress="keyintNodot()"></asp:TextBox>--%>
                </div>
                <div class="col-md-1"></div>
            </div> 
            <div class="row">    
                <div class="col-md-1"></div>            
                <div class="col-md-1 headerData"><b>S / N :</b></div>
                <div class="col-md-4 rowData">
                    <asp:TextBox ID="txtMSN" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1 headerData">                    
                    <b>ส่วนลด (บาท) :</b>
                </div>
                <div class="col-md-4 rowData">
                    <asp:TextBox ID="txtMDiscount" runat="server" onKeyPress="keyintdot()"></asp:TextBox>
                </div>
                <div class="col-md-1"></div>
            </div> 
            <%--<div class="row">    
                <div class="col-md-1"></div>            
                <div class="col-md-2 headerData"><b></b></div>
                <div class="col-md-3 rowData">
                    
                </div>
                
            </div>--%>
            <div class="row" style="height:155px;">    
                <div class="col-md-1"></div>            
                <div class="col-md-1 headerData" style="height:200px;"><b>รายละเอียด :</b></div>
                <div class="col-md-9 rowData" style="height:200px;">
                    <asp:TextBox ID="txtMDescription" runat="server" TextMode="MultiLine" Rows="7" Width="99%"></asp:TextBox>
                </div>                
                <div class="col-md-1"></div>
            </div>
            <div class="row">&nbsp;</div> 
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnMSave" runat="server" CssClass="btn btn-save" Text="Save" OnClick="btnMSave_Click"/>
                    <asp:Button ID="btnModalClose" runat="server" CssClass="btn btn-save" Text="Close"/>
                </div>
            </div>
        </div>
    </asp:Panel>

    

    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel3" TargetControlID="lbl_modal_view">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel3" Height="600px" Width="1100px" runat="server" Style="display: none;">
        <%--Style="display: none;"--%>
        <div class="panel panel-info-dark width100" style="min-height: 600px;">
            <div class="panel-heading text-left">
                <h3 class="panel-title">
                    <asp:Label ID="Label2" runat="server" CssClass="modalHeader" Text="List Item"></asp:Label>
                </h3>
            </div>
            <div class="row" style="margin-top: 20px;">    
                <div class="col-md-1"></div>            
                <div class="col-md-2 headerData"><b>รหัสสินค้า :</b></div>
                <div class="col-md-3 rowData">
                    <asp:TextBox ID="txtSearchItemCode" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2 headerData"><b>ชื่อสินค้า :</b></div>
                <div class="col-md-3 rowData">
                    <asp:TextBox ID="txtSearchItemName" runat="server"></asp:TextBox>                    
                </div>                
                <div class="col-md-1"></div>
            </div>                  
            <div class="row" style="height:400px !important;">    
                <div class="col-md-1"></div>            
                <div class="col-md-10 rowData" style="overflow:auto; height:400px !important;">
                    <asp:GridView ID="gvItemSearch" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField HeaderText="รหัสสินค้า" DataField="ItemCode">
                                <HeaderStyle CssClass="text-center width15 headerData" />
                                <ItemStyle CssClass="text-left rowData"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ชื่อสินค้า" DataField="itemName">
                                <HeaderStyle CssClass="text-center width25 headerData" />
                                <ItemStyle CssClass="text-left rowData"/>
                            </asp:BoundField>
                            <%--<asp:BoundField HeaderText="Serial" DataField="Serial">
                                <HeaderStyle CssClass="text-center width10 headerData" />
                                <ItemStyle CssClass="text-left rowData"/>
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="ราคา" DataField="ItemPrice">
                                <HeaderStyle CssClass="text-center width15 headerData" />
                                <ItemStyle CssClass="text-center rowData"/>
                            </asp:BoundField>      
                            <%--<asp:BoundField HeaderText="รายละเอียด" DataField="ItemDesc" Visible="false">
                                <HeaderStyle CssClass="text-center width15 headerData" />
                                <ItemStyle CssClass="text-right rowData"/>
                            </asp:BoundField>   --%>                           
                            <asp:TemplateField HeaderText="Tools">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hddItemID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ItemID").ToString()%>' />
                                    <asp:HiddenField ID="hddStockID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "StockID").ToString()%>' />
                                    <asp:Label ID="lbItemDesc" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ItemDesc").ToString()%>'></asp:Label>
                                    <asp:ImageButton ID="imgbtnChooseItem" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"                                        
                                        CommandArgument='<%# Container.DataItemIndex.ToString()%>'
                                        OnClick="imgbtnChooseItem_Click" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="text-center width5 headerData" Height="30px"/>
                                <ItemStyle CssClass="text-center rowData" />
                            </asp:TemplateField>                            
                        </Columns>
                        <HeaderStyle BackColor="#ff7777" />
                        <EmptyDataTemplate>
                            <table border="1" style="width:100%; padding:5px;">
                                <tr>
                                    <td>รหัสสินค้า</td>
                                    <td>ชื่อสินค้า</td>
                                    <td>Serial</td>
                                    <td>ราคา</td>                                    
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align:left;">
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
                    <asp:Button ID="btnMItemSearch" runat="server" CssClass="btn btn-save" Text="Search" OnClick="btnMItemSearch_Click"/>
                    <asp:Button ID="btnModalClose3" runat="server" CssClass="btn btn-save" Text="Close" OnClick="btnModalClose3_Click"/>
                </div>
            </div>
        </div>
    </asp:Panel>
    
</asp:Content>
