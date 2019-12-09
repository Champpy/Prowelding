<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageStockHeadQ.aspx.cs" Inherits="Billing.Stock.ManageStockHeadQ" %>
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
                            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
                        </strong>
                    </h3>
                </div>
                <div class="panel-body text-center">
                    <div style="padding: 0px 25px 5px 25px;">
                        
                        <%--Header--%>  
                        <div class="row tab tab-border">
                            <div class="row">
                                <div class="text-left" style="height:25px; padding-left:35px;">
                                    <strong>Header</strong>
                                    <asp:HiddenField ID="hddType" runat="server" />
                                </div>
                            </div>
                            <div class="row width99" style="padding-left:35px;">
                                <div class="col-xs-2 headerData"><b>วันที่ :</b></div>
                                <div class="col-xs-4 rowData">
                                    <asp:TextBox ID="txtDate" runat="server" placeholder="กดเพื่อเปิดปฏิทิน" Width="90%" 
                                        AutoPostBack="true"></asp:TextBox>
                                    <asp:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate" PopupPosition="TopLeft"></asp:CalendarExtender> 
                                </div> 
                                <div class="col-xs-2 headerData" style="height:100px;"><b>หมายเหตุ :</b></div>
                                <div class="col-xs-4 rowData" style="height:100px;">
                                    <asp:TextBox ID="txtRemark" runat="server" Rows="3" TextMode="MultiLine" Width="100%" class="form-control"></asp:TextBox>
                                </div>                          
                            </div>
                        </div>  
                        
                        <%--Detail--%>   
                        <div class="row tab tab-border">
                            <div class="row">
                                <div class="text-left col-xs-6" style="height:25px; padding-left:35px;">
                                    <strong>Detail</strong>                                    
                                </div>
                                <div class="text-right col-xs-6" style="height:25px; padding-right:40px;">
                                    <asp:Button ID="btnAddModal" runat="server" Text="Add Detail" CssClass="btn btn-default" OnClick="btnAddModal_Click"/>
                                </div>
                            </div>
                            <div class="row width99" style="padding-left:35px; padding-top:20px;">                              
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvItem" runat="server" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField HeaderText="รหัสสินค้า" DataField="ProductCode">
                                                <HeaderStyle CssClass="text-center width15 headerData" />
                                                <ItemStyle CssClass="text-left rowData"/>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="สินค้า" DataField="ProductName">
                                                <HeaderStyle CssClass="text-center width45 headerData" />
                                                <ItemStyle CssClass="text-left rowData"/>
                                            </asp:BoundField> 
                                            <%--<asp:BoundField HeaderText="หน่วย" DataField="UnitName">
                                                <HeaderStyle CssClass="text-center width10 headerData" />
                                                <ItemStyle CssClass="text-center rowData"/>
                                            </asp:BoundField>  
                                            <asp:BoundField HeaderText="คงเหลือ" DataField="RemainingStr">
                                                <HeaderStyle CssClass="text-center width10 headerData" />
                                                <ItemStyle CssClass="text-center rowData"/>
                                            </asp:BoundField>--%>
                                            <asp:BoundField HeaderText="จำนวน" DataField="AmountStr">
                                                <HeaderStyle CssClass="text-center width10 headerData" />
                                                <ItemStyle CssClass="text-center rowData"/>
                                            </asp:BoundField>              
                                            <asp:TemplateField HeaderText="Tools">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/icon_delete.gif" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID").ToString()%>'
                                                        OnClick="imgbtnDelete_Click" OnClientClick="return confirm('ยืนยันการลบข้อมูล?');"/>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="text-center width10 headerData" Height="30px"/>
                                                <ItemStyle CssClass="text-center rowData" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="1" style="width:100%; padding:5px; height:50px;">
                                                <%--<tr>
                                                    <td>สินค้า</td>
                                                    <td>รหัสสินค้า</td>
                                                    <td>จำนวน</td>
                                                </tr>--%>
                                                <tr>
                                                    <td colspan="3" style="text-align:left;" class="rowData">
                                                        No data.
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>   

                        <%--Button--%>                                         
                        <div class="row" style="margin-top: 15px;">
                            <div class="col-xs-2"></div>
                            <div class="col-xs-8">
                                <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn btn-default" OnClick="btnSave_Click" />
                                &nbsp;&nbsp;&nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn btn-default" OnClick="btnCancel_Click" />
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

    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel3" TargetControlID="Label2">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel3" Height="500px" Width="1100px" runat="server" Style="display: none;">
        <%--Style="display: none;"--%>
        <div class="panel panel-info-dark width100" style="min-height: 500px;">
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
            <div class="row" style="height:300px !important;">    
                <div class="col-md-1"></div>            
                <div class="col-md-10 rowData" style="overflow:auto; height:300px !important;">
                    <asp:GridView ID="gvItemSearch" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField HeaderText="รหัสสินค้า" DataField="PackageCode">
                                <HeaderStyle CssClass="text-center width25 headerData" />
                                <ItemStyle CssClass="text-left rowData"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ชื่อสินค้า" DataField="PackageName">
                                <HeaderStyle CssClass="text-center width55 headerData" />
                                <ItemStyle CssClass="text-left rowData"/>
                            </asp:BoundField>  
                            <%--<asp:BoundField HeaderText="เหลือ" DataField="RemainingHeadQ">
                                <HeaderStyle CssClass="text-center width10 headerData" />
                                <ItemStyle CssClass="text-right rowData"/>
                            </asp:BoundField>   --%>                    
                            <asp:TemplateField HeaderText="Choose">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnChooseItem" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/b_edit.png"                                        
                                        CommandArgument='<%# Container.DataItemIndex.ToString()%>'
                                        CommandName='<%# DataBinder.Eval(Container.DataItem, "PackageHeaderID").ToString()%>'
                                        OnClick="imgbtnChooseItem_Click" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="text-center width10 headerData" Height="30px"/>
                                <ItemStyle CssClass="text-center rowData" />
                            </asp:TemplateField>                            
                        </Columns>
                        <HeaderStyle BackColor="#ff7777" />
                        <EmptyDataTemplate>
                            <table border="1" style="width:100%; padding:5px;">
                                <tr>
                                    <td>รหัสสินค้า</td>
                                    <td>ชื่อสินค้า</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align:left;">
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

    <%--Amount--%>
    <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel5" TargetControlID="Label4">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel5" Height="180px" Width="400px" runat="server" Style="display: none;">
        <div class="panel panel-info-dark width100" style="min-height: 180px;">
            <div class="panel-heading text-left">
                <h3 class="panel-title">
                    <asp:Label ID="Label4" runat="server" CssClass="modalHeader" Text="Amount"></asp:Label>
                </h3>
            </div>
            <div class="row">    
                <div class="col-md-1"></div>            
                <div class="col-md-3 headerData"><b>จำนวน :</b></div>
                <div class="col-md-7 rowData">
                    <asp:TextBox ID="txtm5Amount" runat="server" autocomplete="off" onKeyPress="keyintNodot()" Width="85%"></asp:TextBox>
                    <asp:HiddenField id="hddm5Index" runat="server" />
                </div>               
                <div class="col-md-1"></div>
            </div>     
            <div class="row">&nbsp;</div> 
            <div class="row" style="margin-top: 15px;">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnm5OK" runat="server" CssClass="btn btn-save" Text="OK" OnClick="btnm5OK_Click"/>
                    <asp:Button ID="btnm5Cancel" runat="server" CssClass="btn btn-save" Text="Cancel" OnClick="btnm5Cancel_Click"/>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--S/N--%>
    <asp:ModalPopupExtender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="Panel6" TargetControlID="Label4">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel6" Height="350px" Width="400px" runat="server" Style="display: none;">
        <div class="panel panel-info-dark width100" style="min-height: 350px;">
            <div class="panel-heading text-left">
                <h3 class="panel-title">
                    <asp:Label ID="lbM6Header" runat="server" CssClass="modalHeader" Text="S/N"></asp:Label>
                </h3>
            </div>
            <div class="row">    
                <div class="col-md-1"></div>            
                <div class="col-md-3 headerData"><b>S/N :</b></div>
                <div class="col-md-7 rowData">
                    <asp:TextBox ID="txtM6SN" autocomplete="off" runat="server" Width="75%"></asp:TextBox>
                    <asp:HiddenField id="hddM6ProductID" runat="server" />
                    <asp:Button ID="btnM6Add" runat="server" CssClass="btn btn-save" Text="Add" OnClick="btnM6Add_Click"/>
                </div>               
                <div class="col-md-1"></div>
            </div> 
            <br />
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10" style="height:120px; overflow:auto;">
                    <asp:GridView ID="gvSerial" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField HeaderText="Serial" DataField="SerialNumber">
                                <HeaderStyle CssClass="text-center width75" />
                                <ItemStyle CssClass="text-left"/>
                            </asp:BoundField>
                            <%--<asp:BoundField HeaderText="ชื่อสินค้า" DataField="ProductName">
                                <HeaderStyle CssClass="text-center width55 headerData" />
                                <ItemStyle CssClass="text-left rowData"/>
                            </asp:BoundField>  
                            <asp:BoundField HeaderText="เหลือ" DataField="Remaining">
                                <HeaderStyle CssClass="text-center width10 headerData" />
                                <ItemStyle CssClass="text-right rowData"/>
                            </asp:BoundField>  --%>                     
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDeleteSN" runat="server" Height="20px" Width="20px" ImageUrl="~/img/icon/icon_delete.gif"                                        
                                        CommandArgument='<%# Container.DataItemIndex.ToString()%>'
                                        CommandName='<%# DataBinder.Eval(Container.DataItem, "SerialNumber").ToString()%>'
                                        OnClick="imgbtnDeleteSN_Click" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="text-center width10" Height="30px"/>
                                <ItemStyle CssClass="text-center" />
                            </asp:TemplateField>                            
                        </Columns>
                        <HeaderStyle BackColor="#ff7777" />
                        <EmptyDataTemplate>
                            <table border="1" style="width:100%; padding:5px;">
                                <tr>
                                    <td colspan="2" style="text-align:left;">
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
                    <asp:Button ID="btnM6Save" runat="server" CssClass="btn btn-save" Text="Save" OnClick="btnM6Save_Click"/>
                    <asp:Button ID="btnM6Cancel" runat="server" CssClass="btn btn-save" Text="Cancel" OnClick="btnM6Cancel_Click"/>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
