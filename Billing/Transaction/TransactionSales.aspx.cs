using Billing.AppData;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace Billing.Transaction
{
    public partial class TransactionSales : Billing.Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Attributes.Add("readonly", "readonly");
                string ID = Request.QueryString.Get("ID");
                hddID.Value = ID;                
                Session["saleDetail"] = null;
                BindDDL();
                BindData();
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                TransSaleHeader o = new TransSaleHeader();
                using (BillingEntities cre = new BillingEntities())
                {
                    o = cre.TransSaleHeaders.Where(w => w.ReceivedDate.Value.Year == dt.Year && w.ReceivedDate.Value.Month == dt.Month)
                        .OrderByDescending(od => od.SaleNumber).FirstOrDefault();

                    if (o != null)
                    {
                        string[] spl = o.SaleNumber.Split('-');
                        if (spl != null)
                        {
                            int no = ToInt32(spl[1]);
                            no++;
                            txtSaleNumber.Text = dt.ToString("yyMM") + "-" + no.ToString("000");
                        }
                    }
                    else
                    {
                        txtSaleNumber.Text = dt.ToString("yyMM") + "-001";
                    }
                };
            }
            catch (Exception ex)
            {
                Logs(ex.Message);
            }
        }

        #region BindData
        protected void BindDDL()
        {
            try
            {
                List<MasValueList> lst = new List<MasValueList>();

                #region Pay Type
                using (BillingEntities cre = new BillingEntities())
                {
                    lst = cre.MasValueLists.Where(w => w.FUNC.Equals("PAYMENT") && w.ISACTIVE.Equals("Y")).OrderBy(od => od.TYPES).ToList();
                }
                if(lst != null && lst.Count > 0)
                {
                    lst = lst.OrderBy(od => ToInt32(od.CODE)).ToList();
                    ddlPay.DataSource = lst;
                    ddlPay.DataTextField = "Description";
                    ddlPay.DataValueField = "Code";
                }
                else
                {
                    ddlPay.DataSource = null;
                }
                ddlPay.DataBind();
                #endregion

                #region Account
                List<MasAccountTransfer> lstAcc = new List<MasAccountTransfer>();
                using (BillingEntities cre = new BillingEntities())
                {
                    lstAcc = cre.MasAccountTransfers.Where(w => w.ISACTIVE.Equals("Y")).OrderBy(od => od.AccountName).ToList();
                }
                if (lst != null && lst.Count > 0)
                {
                    ddlAccount.DataSource = lstAcc;
                    ddlAccount.DataTextField = "AccountName";
                    ddlAccount.DataValueField = "AccountTransferID";
                }
                else
                {
                    ddlAccount.DataSource = null;
                }
                ddlAccount.DataBind();
                #endregion

                #region Sale
                using (BillingEntities cre = new BillingEntities())
                {
                    lst = cre.MasValueLists.Where(w => w.FUNC.Equals("SALENAME") && w.ISACTIVE.Equals("Y")).OrderBy(od => od.CODE).ToList();
                }
                if (lst != null && lst.Count > 0)
                {
                    ddlSaleName.DataSource = lst;
                    ddlSaleName.DataTextField = "Description";
                    ddlSaleName.DataValueField = "Code";
                }
                else
                {
                    ddlSaleName.DataSource = null;
                }
                ddlSaleName.Items.Insert(0, new ListItem("", "0"));
                ddlSaleName.DataBind();
                #endregion

                #region Installment
                using (BillingEntities cre = new BillingEntities())
                {
                    lst = cre.MasValueLists.Where(w => w.FUNC.Equals("INSTALLMENT") && w.ISACTIVE.Equals("Y")).OrderBy(od => od.CODE).ToList();
                }
                if (lst != null && lst.Count > 0)
                {
                    ddlAccountInst.DataSource = lst;
                    ddlAccountInst.DataTextField = "Description";
                    ddlAccountInst.DataValueField = "Code";
                }
                else
                {
                    ddlAccountInst.DataSource = null;
                }
                ddlAccountInst.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                Logs(ex.Message);
            }
        }
        protected void BindData()
        {
            try
            {
                Entities.TransSaleHeader obj = new Entities.TransSaleHeader();
                List<Entities.DTO.SaleDetailDTO> lstDetail = new List<Entities.DTO.SaleDetailDTO>();
                if (!string.IsNullOrEmpty(hddID.Value)) // Mode Edit
                {
                    int tid = ToInt32(hddID.Value);
                    var bal = TransactionDal.Instance;
                    bal.GetTransSaleByID(tid, ref obj, ref lstDetail);

                    hddID.Value = obj.SaleHeaderID.ToString();
                    txtDate.Text = obj.ReceivedDate.HasValue ? obj.ReceivedDate.Value.ToString("dd/MM/yyyy") : "";
                    txtCustAddress.Text = obj.CustomerAddress;
                    txtCustDistrict.Text = obj.CustomerDistrict;
                    txtCustCountry.Text = obj.CustomerCountry;
                    txtCustProvince.Text = obj.CustomerProvince;
                    txtCustPostalCode.Text = obj.CustomerPostalCode;
                    txtDeliverAddress.Text = obj.DeliverAdd;
                    txtDeliverDistrict.Text = obj.DeliverDistrict;
                    txtDeliverCountry.Text = obj.DeliverCountry;
                    txtDeliverProvince.Text = obj.DeliverProvince;
                    txtDeliverPostalCode.Text = obj.DeliverPostalCode;
                    txtCustName.Text = obj.CustomerName;
                    txtDeliveryName.Text = obj.DeliveryName;
                    txtTel.Text = obj.Tel;
                    txtSaleNumber.Text = obj.SaleNumber;
                    txtRemark.Text = obj.Remark;
                    //chkCOD.Checked = string.IsNullOrEmpty(obj.COD) ? false : obj.COD.Equals("0") ? false : true;
                    txtWarranty.Text = obj.WarrantyDate.ToString("dd/MM/yyyy");
                    txtConsignmentNo.Text = obj.ConsignmentNo;

                    #region 201808
                    if (!string.IsNullOrEmpty(obj.BillType))
                    {
                        if (obj.BillType.Equals("Vat"))
                        {
                            rdbVat.Checked = true;
                        }
                        else //if (obj.BillType.Equals("Cash"))
                        {
                            rdbCash.Checked = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(obj.PayType))
                        ddlPay.SelectedIndex = ddlPay.Items.IndexOf(ddlPay.Items.FindByValue(obj.PayType)); //ToInt32(obj.PayType);
                    #endregion

                    #region 201901
                    ddlAccount.SelectedIndex = ddlAccount.Items.IndexOf(ddlAccount.Items.FindByText(obj.AccountTransfer));
                    ddlSaleName.SelectedIndex = ddlSaleName.Items.IndexOf(ddlSaleName.Items.FindByText(obj.SaleName));
                    txtTimeTransfer.Text = obj.TimeTransfer;
                    #endregion

                    #region 201903
                    ddlAccountInst.SelectedIndex = ddlAccountInst.Items.IndexOf(ddlAccountInst.Items.FindByText(obj.Installment));
                    #endregion

                    Session["saleDetail"] = lstDetail;

                    #region Button
                    btnAddModal.Visible = false;
                    #endregion

                    #region Comment
                    //TransSaleHeader obj = new TransSaleHeader();
                    //List<SaleDetailDTO> lst = new List<SaleDetailDTO>();
                    //List<Entities.MasPackageDetail> lstPackageDetail = new List<Entities.MasPackageDetail>();
                    //using (BillingEntities cre = new BillingEntities())
                    //{
                    //    obj = cre.TransSaleHeaders.FirstOrDefault(w => w.SaleHeaderID.Equals(tid));
                    //    if (obj != null)
                    //    {
                    //        hddID.Value = obj.SaleHeaderID.ToString();
                    //        txtDate.Text = obj.ReceivedDate.HasValue ? obj.ReceivedDate.Value.ToString("dd/MM/yyyy") : "";
                    //        txtCustAddress.Text = obj.CustomerAddress;
                    //        txtCustDistrict.Text = obj.CustomerDistrict;
                    //        txtCustCountry.Text = obj.CustomerCountry;
                    //        txtCustProvince.Text = obj.CustomerProvince;
                    //        txtCustPostalCode.Text = obj.CustomerPostalCode;
                    //        //txtCustAddress2.Text = obj.CustomerAddress2;
                    //        //txtCustAddress3.Text = obj.CustomerAddress3;
                    //        txtDeliverAddress.Text = obj.DeliverAdd;
                    //        txtDeliverDistrict.Text = obj.DeliverDistrict;
                    //        txtDeliverCountry.Text = obj.DeliverCountry;
                    //        txtDeliverProvince.Text = obj.DeliverProvince;
                    //        txtDeliverPostalCode.Text = obj.DeliverPostalCode;
                    //        //txtDeliverAddress2.Text = obj.DeliverAdd2;
                    //        //txtDeliverAddress3.Text = obj.DeliverAdd3;
                    //        txtCustName.Text = obj.CustomerName;
                    //        txtDeliveryName.Text = obj.DeliveryName;
                    //        txtTel.Text = obj.Tel;
                    //        txtSaleNumber.Text = obj.SaleNumber;
                    //        txtRemark.Text = obj.Remark;
                    //        //chkCOD.Checked = string.IsNullOrEmpty(obj.COD) ? false : obj.COD.Equals("0") ? false : true;
                    //        txtWarranty.Text = obj.WarrantyDate.HasValue ? obj.WarrantyDate.Value.ToString("dd/MM/yyyy") : "";
                    //        txtConsignmentNo.Text = obj.ConsignmentNo;

                    //        #region 201808
                    //        if(!string.IsNullOrEmpty(obj.BillType))
                    //        {
                    //            if (obj.BillType.Equals("Vat"))
                    //            {
                    //                rdbVat.Checked = true;
                    //            }
                    //            else //if (obj.BillType.Equals("Cash"))
                    //            {
                    //                rdbCash.Checked = true;
                    //            }
                    //        }
                    //        if (!string.IsNullOrEmpty(obj.PayType))
                    //            ddlPay.SelectedIndex = ddlPay.Items.IndexOf(ddlPay.Items.FindByValue(obj.PayType)); //ToInt32(obj.PayType);
                    //        #endregion

                    //        #region 201901
                    //        ddlAccount.SelectedIndex = ddlAccount.Items.IndexOf(ddlAccount.Items.FindByText(obj.AccountTransfer));
                    //        ddlSaleName.SelectedIndex = ddlSaleName.Items.IndexOf(ddlSaleName.Items.FindByText(obj.SaleName));
                    //        txtTimeTransfer.Text = obj.TimeTransfer;
                    //        #endregion

                    //        #region 201903
                    //        ddlAccountInst.SelectedIndex = ddlAccountInst.Items.IndexOf(ddlAccountInst.Items.FindByText(obj.Installment));

                    //        #endregion

                    //        lst = (from d in cre.TransSaleDetails
                    //               join i in cre.MasItems on d.ItemID equals i.ItemID
                    //               where d.SaleHeaderID.Equals(tid)
                    //               select new SaleDetailDTO()
                    //               {
                    //                   SaleDetailID = d.SaleDetailID,
                    //                   SaleHeaderID = d.SaleHeaderID,
                    //                   ItemID = d.ItemID.HasValue ? d.ItemID.Value : 0,
                    //                   ItemName = i.ItemName,
                    //                   ItemPrice = d.ItemPrice.HasValue ? d.ItemPrice.Value : 0,
                    //                   ItemDescription = d.ItemDetail,
                    //                   Amount = d.Amount,
                    //                   Discount = d.Discount,
                    //                   SerialNumber = d.SerialNumber,
                    //                   Status = "Old",

                    //               }).ToList();



                    //        Session["saleDetail"] = lst;
                    //    }
                    //}
                    #endregion
                }
                else  // Mode Add
                {
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtWarranty.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    rdbCash.Checked = true;

                    //Get Salenumber
                    string salenumber = DateTime.Now.ToString("yyMM");
                    string[] spl;
                    TransSaleHeader s = new TransSaleHeader();
                    List<TransSaleHeader> lstS = new List<TransSaleHeader>();
                    using (BillingEntities cre = new BillingEntities())
                    {
                        lstS = cre.TransSaleHeaders.Where(w => w.SaleNumber.Contains(salenumber)).ToList();
                        if(lstS != null && lstS.Count > 0)
                        {
                            s = lstS.OrderByDescending(o => o.SaleNumber).FirstOrDefault();
                            spl = s.SaleNumber.Split('-');
                            if (spl != null)
                                salenumber = salenumber + "-" + (ToInt32(spl[1]) + 1).ToString("000");

                            txtSaleNumber.Text = salenumber;
                        }
                        else
                        {
                            txtSaleNumber.Text = salenumber + "-001";
                        }
                    };
                }
                BindDataGrid();
            }
            catch (Exception ex)
            {
                Logs(ex.Message);
            }
        }

        protected void BindDataGrid()
        {
            try
            {
                List<SaleDetailDTO> lst = new List<SaleDetailDTO>();
                if (Session["saleDetail"] != null)
                {
                    lst = (List<SaleDetailDTO>)Session["saleDetail"];
                    lst = lst.Where(w => w.Status != "Delete").ToList();
                    double Total = 0;
                    foreach (SaleDetailDTO item in lst)
                    {                        
                        Total = Total + item.Total;
                    }
                    lbTotal.Text = (Total).ToString("###,##0.00");
                }
                else
                {
                    lst = null;
                }

                gvItem.DataSource = lst;
                gvItem.DataBind();
            }
            catch (Exception ex)
            {
                Logs(ex.Message);
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate
                if (txtCustName.Text == "")
                {
                    ShowMessageBox("กรุณาระบุ ชื่อลูกค้า");
                    return;
                }
                if (txtDate.Text == "")
                {
                    ShowMessageBox("กรุณาระบุ วันที่");
                    return;
                }

                string Message = "", result = "";
                var bal = TransactionDal.Instance;
                Entities.TransSaleHeader o = new Entities.TransSaleHeader();
                SaleHeaderDTO obj = new SaleHeaderDTO();
                List<SaleDetailDTO> lst = new List<SaleDetailDTO>();
                List<SaleDetailDTO> tmp = new List<SaleDetailDTO>();
                Int32 hid = 0;
                if (Session["saleDetail"] != null)
                {
                    lst = (List<SaleDetailDTO>)Session["saleDetail"];
                }

                if (string.IsNullOrEmpty(hddID.Value)) //New --> Insert
                {
                    #region Insert Header
                    o.Tel = txtTel.Text;
                    o.CustomerName = txtCustName.Text;
                    o.CustomerAddress = txtCustAddress.Text;
                    o.CustomerDistrict = txtCustDistrict.Text;
                    o.CustomerCountry = txtCustCountry.Text;
                    o.CustomerProvince = txtCustProvince.Text;
                    o.CustomerPostalCode = txtCustPostalCode.Text;
                    o.DeliveryName = txtDeliveryName.Text;
                    o.DeliverAdd = txtDeliverAddress.Text;
                    o.DeliverDistrict = txtDeliverDistrict.Text;
                    o.DeliverCountry = txtDeliverCountry.Text;
                    o.DeliverProvince = txtDeliverProvince.Text;
                    o.DeliverPostalCode = txtDeliverPostalCode.Text;
                    o.ReceivedDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                    //o.ReceivedBy = "เงินสด";
                    o.ReceivedBy = rdbCash.Checked ? "เงินสด" : "VAT";
                    o.SaleNumber = txtSaleNumber.Text;
                    o.Remark = txtRemark.Text;
                    o.CreatedBy = GetUsername();
                    o.CreatedDate = DateTime.Now;
                    o.Active = "1";
                    o.ConsignmentNo = txtConsignmentNo.Text;
                    o.COD = "0";
                    if (!string.IsNullOrEmpty(txtWarranty.Text))
                        o.WarrantyDate = DateTime.ParseExact(txtWarranty.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));

                    #region 201808
                    o.BillType = rdbVat.Checked ? "Vat" : "Cash";
                    o.PayType = ddlPay.SelectedValue;
                    #endregion

                    #region 201901
                    o.SaleName = ddlSaleName.SelectedItem.Text;
                    if (ddlPay.SelectedItem.Value == "5")
                    {
                        o.AccountTransfer = ddlAccount.SelectedItem == null ? "" : ddlAccount.SelectedItem.Text;
                        o.TimeTransfer = txtTimeTransfer.Text;
                    }
                    else
                    {
                        o.AccountTransfer = "";
                        o.TimeTransfer = "";
                    }
                    #endregion

                    #region 201903
                    if (ddlPay.SelectedItem.Value == "8")
                        o.Installment = ddlAccountInst.SelectedItem.Text;
                    else
                        o.Installment = "";
                    #endregion

                    Message = bal.InsertSaleHeader(o, lst);
                    #endregion

                    
                    #region Comment
                    //using (BillingEntities cre = new BillingEntities())
                    //{
                    //    #region Header
                    //    o.Tel = txtTel.Text;
                    //    o.CustomerName = txtCustName.Text;
                    //    o.CustomerAddress = txtCustAddress.Text;
                    //    o.CustomerDistrict = txtCustDistrict.Text;
                    //    o.CustomerCountry = txtCustCountry.Text;
                    //    o.CustomerProvince = txtCustProvince.Text;
                    //    o.CustomerPostalCode = txtCustPostalCode.Text;
                    //    //o.CustomerAddress2 = txtCustAddress2.Text;
                    //    //o.CustomerAddress3 = txtCustAddress3.Text;
                    //    o.DeliveryName = txtDeliveryName.Text;
                    //    o.DeliverAdd = txtDeliverAddress.Text;
                    //    o.DeliverDistrict = txtDeliverDistrict.Text;
                    //    o.DeliverCountry = txtDeliverCountry.Text;
                    //    o.DeliverProvince = txtDeliverProvince.Text;
                    //    o.DeliverPostalCode = txtDeliverPostalCode.Text;
                    //    //o.DeliverAdd2 = txtDeliverAddress2.Text;
                    //    //o.DeliverAdd3 = txtDeliverAddress3.Text;
                    //    o.ReceivedDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                    //    o.ReceivedBy = "เงินสด";
                    //    o.SaleNumber = txtSaleNumber.Text;
                    //    o.Remark = txtRemark.Text;
                    //    o.CreatedBy = GetUsername();
                    //    o.CreatedDate = DateTime.Now;
                    //    o.Active = "1";
                    //    o.ConsignmentNo = txtConsignmentNo.Text;
                    //    //o.COD = chkCOD.Checked ? "1" : "0";
                    //    o.COD = "0";
                    //    if (!string.IsNullOrEmpty(txtWarranty.Text))
                    //        o.WarrantyDate = DateTime.ParseExact(txtWarranty.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));

                    //    #region 201808
                    //    o.BillType = rdbVat.Checked ? "Vat" : "Cash";
                    //    o.PayType = ddlPay.SelectedValue;
                    //    #endregion

                    //    #region 201901
                    //    o.SaleName = ddlSaleName.SelectedItem.Text;
                    //    if (ddlPay.SelectedItem.Value == "5")
                    //    {
                    //        o.AccountTransfer = ddlAccount.SelectedItem.Text;
                    //        o.TimeTransfer = txtTimeTransfer.Text;
                    //    }
                    //    else
                    //    {
                    //        o.AccountTransfer = "";
                    //        o.TimeTransfer = "";
                    //    }
                    //    #endregion

                    //    #region 201903
                    //    if (ddlPay.SelectedItem.Value == "8")
                    //        o.Installment = ddlAccountInst.SelectedItem.Text;
                    //    else
                    //        o.Installment = "";
                    //    #endregion

                    //    cre.TransSaleHeaders.Add(o);
                    //    cre.SaveChanges();
                    //    hid = o.SaleHeaderID;
                    //    #endregion

                    //    #region Description
                    //    lst = lst.Where(w => w.Status == "New").ToList();
                    //    if (lst != null && lst.Count > 0)
                    //    {
                    //        TransSaleDetail oDetail = new TransSaleDetail();
                    //        //TransStock ts = new TransStock();
                    //        foreach (SaleDetailDTO item in lst)
                    //        {
                    //            oDetail = new TransSaleDetail();
                    //            oDetail.SaleHeaderID = hid;
                    //            oDetail.ItemID = item.ItemID;
                    //            oDetail.ItemPrice = item.ItemPrice;
                    //            oDetail.Amount = item.Amount;
                    //            oDetail.Discount = item.Discount;
                    //            //oDetail.DiscountPer = item.DiscountPer;
                    //            oDetail.SerialNumber = item.SerialNumber;
                    //            oDetail.ItemDetail = item.ItemDescription;
                    //            cre.TransSaleDetails.Add(oDetail);
                    //            cre.SaveChanges();
                    //            did = oDetail.SaleDetailID;

                    //            #region SaleDetailProduct
                    //            foreach (Entities.MasPackageDetail dp in item.ProductDetail)
                    //            {

                    //            }
                    //            #endregion

                    //            #region Stock Comment
                    //            //StockID = item.StockID;
                    //            //ts = cre.TransStocks.FirstOrDefault(w => w.StockID.Equals(StockID) && w.Active.ToLower().Equals("y"));
                    //            //if(ts != null)
                    //            //{
                    //            //    ts.Active = "N";
                    //            //    ts.StockType = "Sold";
                    //            //    ts.SaleHeaderID = hid;
                    //            //    ts.SaleDetailID = oDetail.SaleDetailID;
                    //            //    ts.UpdatedBy = GetUsername();
                    //            //    ts.UpdatedDate = DateTime.Now;
                    //            //    cre.SaveChanges();
                    //            //}
                    //            //else // Item Is Sold Already
                    //            //{
                    //            //    Message = Message + " " + item.ItemName + ",";
                    //            //    cre.TransSaleDetails.Remove(oDetail);
                    //            //    cre.SaveChanges();
                    //            //}
                    //            #endregion

                    //            //Change = ToInt32(item.Amount.Value.ToString()) * -1;
                    //            //ManageStock(ToInt32(item.ItemID.Value.ToString()), ToInt32(item.Amount.Value.ToString()), "Sale", GetUsername(), Change, oDetail.SaleDetailID);
                    //        }
                    //    }
                    //    cre.SaveChanges();
                    //    #endregion
                    //};
                    #endregion
                }
                else // --> Update
                {
                    #region Header 
                    o.SaleHeaderID = ToInt32(hddID.Value);
                    o.Tel = txtTel.Text;
                    o.CustomerName = txtCustName.Text;
                    o.CustomerAddress = txtCustAddress.Text;
                    o.CustomerDistrict = txtCustDistrict.Text;
                    o.CustomerCountry = txtCustCountry.Text;
                    o.CustomerProvince = txtCustProvince.Text;
                    o.CustomerPostalCode = txtCustPostalCode.Text;
                    o.DeliveryName = txtDeliveryName.Text;
                    o.DeliverAdd = txtDeliverAddress.Text;
                    o.DeliverDistrict = txtDeliverDistrict.Text;
                    o.DeliverCountry = txtDeliverCountry.Text;
                    o.DeliverProvince = txtDeliverProvince.Text;
                    o.DeliverPostalCode = txtDeliverPostalCode.Text;
                    o.ReceivedDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                    o.ReceivedBy = rdbCash.Checked ? "เงินสด" : "VAT";
                    o.SaleNumber = txtSaleNumber.Text;
                    o.Remark = txtRemark.Text;
                    o.UpdatedBy = GetUsername();
                    o.UpdatedDate = DateTime.Now;
                    o.ConsignmentNo = txtConsignmentNo.Text;
                    //o.COD = chkCOD.Checked ? "1" : "0";
                    o.COD = "0";
                    if (!string.IsNullOrEmpty(txtWarranty.Text))
                        o.WarrantyDate = DateTime.ParseExact(txtWarranty.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));

                    #region 201808
                    o.BillType = rdbVat.Checked ? "Vat" : "Cash";
                    o.PayType = ddlPay.SelectedValue;
                    #endregion

                    #region 201901
                    o.SaleName = ddlSaleName.SelectedItem.Text;
                    if (ddlPay.SelectedItem.Value == "5")
                    {
                        o.AccountTransfer = ddlAccount.SelectedItem.Text;
                        o.TimeTransfer = txtTimeTransfer.Text;
                    }
                    else
                    {
                        o.AccountTransfer = "";
                        o.TimeTransfer = "";
                    }
                    #endregion

                    #region 201903
                    if (ddlPay.SelectedItem.Value == "8")
                        o.Installment = ddlAccountInst.SelectedItem.Text;
                    else
                        o.Installment = "";
                    #endregion

                    Message = bal.UpdateSaleHeader(o, lst, ref result);
                    #endregion

                    #region Comment
                    //hid = ToInt32(hddID.Value);
                    //using (BillingEntities cre = new BillingEntities())
                    //{
                    //    o = cre.TransSaleHeaders.FirstOrDefault(w => w.SaleHeaderID.Equals(hid));
                    //    if (o != null)
                    //    {
                    //        #region Header 
                    //        o.Tel = txtTel.Text;
                    //        o.CustomerName = txtCustName.Text;
                    //        o.CustomerAddress = txtCustAddress.Text;
                    //        o.CustomerDistrict = txtCustDistrict.Text;
                    //        o.CustomerCountry = txtCustCountry.Text;
                    //        o.CustomerProvince = txtCustProvince.Text;
                    //        o.CustomerPostalCode = txtCustPostalCode.Text;
                    //        //o.CustomerAddress2 = txtCustAddress2.Text;
                    //        //o.CustomerAddress3 = txtCustAddress3.Text;
                    //        o.DeliveryName = txtDeliveryName.Text;
                    //        o.DeliverAdd = txtDeliverAddress.Text;
                    //        o.DeliverDistrict = txtDeliverDistrict.Text;
                    //        o.DeliverCountry = txtDeliverCountry.Text;
                    //        o.DeliverProvince = txtDeliverProvince.Text;
                    //        o.DeliverPostalCode = txtDeliverPostalCode.Text;
                    //        //o.DeliverAdd2 = txtDeliverAddress2.Text;
                    //        //o.DeliverAdd3 = txtDeliverAddress3.Text;
                    //        o.ReceivedDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                    //        o.SaleNumber = txtSaleNumber.Text;
                    //        o.Remark = txtRemark.Text;
                    //        o.UpdatedBy = GetUsername();
                    //        o.UpdatedDate = DateTime.Now;
                    //        o.ConsignmentNo = txtConsignmentNo.Text;
                    //        //o.COD = chkCOD.Checked ? "1" : "0";
                    //        o.COD = "0";
                    //        if (!string.IsNullOrEmpty(txtWarranty.Text)) 
                    //            o.WarrantyDate = DateTime.ParseExact(txtWarranty.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));

                    //        #region 201808
                    //        o.BillType = rdbVat.Checked ? "Vat" : "Cash";
                    //        o.PayType = ddlPay.SelectedValue;
                    //        #endregion

                    //        #region 201901
                    //        o.SaleName = ddlSaleName.SelectedItem.Text;
                    //        if(ddlPay.SelectedItem.Value == "5")
                    //        {
                    //            o.AccountTransfer = ddlAccount.SelectedItem.Text;
                    //            o.TimeTransfer = txtTimeTransfer.Text;
                    //        }
                    //        else
                    //        {
                    //            o.AccountTransfer = "";
                    //            o.TimeTransfer = "";
                    //        }
                    //        #endregion

                    //        #region 201903
                    //        if (ddlPay.SelectedItem.Value == "8")
                    //            o.Installment = ddlAccountInst.SelectedItem.Text;
                    //        else
                    //            o.Installment = "";
                    //        #endregion

                    //        cre.SaveChanges();
                    //        #endregion

                    //        #region Detail
                    //        if (lst != null && lst.Count > 0)
                    //        {                                
                    //            TransSaleDetail objDetail = new TransSaleDetail();
                    //            tmp = lst.Where(w => w.Status == "Old").ToList();
                    //            foreach (SaleDetailDTO item in tmp)
                    //            {
                    //                objDetail = cre.TransSaleDetails.FirstOrDefault(w => w.SaleDetailID.Equals(item.SaleDetailID));
                    //                if (objDetail != null)
                    //                {
                    //                    if (objDetail.Amount.Value - item.Amount.Value != 0)
                    //                        Change = ToInt32(objDetail.Amount.Value - item.Amount.Value);

                    //                    objDetail.ItemID = item.ItemID;
                    //                    objDetail.ItemPrice = item.ItemPrice;
                    //                    objDetail.Amount = item.Amount;
                    //                    objDetail.Discount = item.Discount;
                    //                    //objDetail.DiscountPer = item.DiscountPer;
                    //                    objDetail.SerialNumber = item.SerialNumber;
                    //                    objDetail.ItemDetail = item.ItemDescription;
                    //                }
                    //                cre.SaveChanges();

                    //                //ManageStock(ToInt32(item.ItemID.Value.ToString()), ToInt32(item.Amount.Value.ToString()), "Sale", GetUsername(), Change, item.SaleDetailID);
                    //            }

                    //            tmp = lst.Where(w => w.Status == "New").ToList();
                    //            foreach (SaleDetailDTO item in tmp)
                    //            {
                    //                Change = 0;
                    //                objDetail = new TransSaleDetail();
                    //                objDetail.SaleHeaderID = hid;
                    //                objDetail.ItemID = item.ItemID;
                    //                objDetail.ItemPrice = item.ItemPrice;
                    //                objDetail.Amount = item.Amount;
                    //                objDetail.Discount = item.Discount;
                    //                //objDetail.DiscountPer = item.DiscountPer;
                    //                objDetail.SerialNumber = item.SerialNumber;
                    //                objDetail.ItemDetail = item.ItemDescription;
                    //                cre.TransSaleDetails.Add(objDetail);
                    //                cre.SaveChanges();

                    //                //Change = ToInt32(item.Amount.Value.ToString()) * -1;
                    //                //ManageStock(ToInt32(item.ItemID.Value.ToString()), ToInt32(item.Amount.Value.ToString()), "Sale", GetUsername(), Change, item.SaleDetailID);
                    //            }

                    //            tmp = lst.Where(w => w.Status == "Delete").ToList();
                    //            foreach (SaleDetailDTO item in tmp)
                    //            {
                    //                Change = 0;
                    //                objDetail = new TransSaleDetail();
                    //                objDetail = cre.TransSaleDetails.FirstOrDefault(w => w.SaleDetailID.Equals(item.SaleDetailID));
                    //                if (objDetail != null)
                    //                {
                    //                    Change = ToInt32(objDetail.Amount.Value);
                    //                    cre.TransSaleDetails.Remove(objDetail);
                    //                }

                    //                cre.SaveChanges();

                    //                //ManageStock(ToInt32(item.ItemID.Value.ToString()), ToInt32(item.Amount.Value.ToString()), "Sale", GetUsername(), Change, item.SaleDetailID);
                    //            }
                    //        }
                    //        #endregion
                    //    }
                    //};
                    #endregion
                }

                if (!string.IsNullOrEmpty(result))
                {
                    ShowMessageBox(result);
                    return;
                }

                if (string.IsNullOrEmpty(Message))
                    ShowMessageBox("บันทึกสำเร็จ.", this.Page, "TransactionSaleList.aspx");
                else
                    ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                //SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                Logs(ex.Message);
            }
        }

        #region Event Detail Modal1
        protected void btnAddModal_Click(object sender, EventArgs e)
        {
            try
            {
                //BindMDDL();
                ModalPopupExtender1.Show();
                ClearTextDetail();
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        protected void btnMSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ItemID = 0, Count = 0;
                #region Validate
                if (txtMItem.Text == "")
                {
                    ShowMessageBox("กรุณาระบุ Item");
                    ModalPopupExtender1.Show();
                    return;
                }

                if (txtMAmount.Text == "" || txtMAmount.Text == "0")
                {
                    ShowMessageBox("กรุณาระบุ Amount");
                    ModalPopupExtender1.Show();
                    return;
                }

                if (hddSN.Value.ToLower() == "y" && string.IsNullOrEmpty(txtMSN.Text))
                {
                    ShowMessageBox("กรุณาระบุ S/N");
                    ModalPopupExtender1.Show();
                    return;
                }
                
                ItemID = ToInt32(hddItemID.Value);
                #endregion

                #region Save to Session
                List<SaleDetailDTO> lst = new List<SaleDetailDTO>();
                SaleDetailDTO o = new SaleDetailDTO();
                if (Session["saleDetail"] != null)
                {
                    lst = (List<SaleDetailDTO>)Session["saleDetail"];
                }

                if (hddDetailID.Value != "" && hddDetailID.Value != "0")
                {
                    Int32 did = ToInt32(hddDetailID.Value);
                    o = lst.FirstOrDefault(w => w.SaleDetailID.Equals(did));
                    if(o != null)
                    {
                        o.ItemID = ItemID;
                        //o.StockID = ToInt32(hddStockID.Value);
                        o.ItemName = txtMItem.Text;
                        o.ItemPrice = ToDoudle(txtMPrice.Text);
                        o.Discount = ToDoudle(txtMDiscount.Text);
                        //o.DiscountPer = ToDoudle(txtMDiscountPer.Text);
                        o.Amount = ToDoudle(txtMAmount.Text);
                        o.SerialNumber = txtMSN.Text;
                        o.ItemDescription = GetItemDesc();// txtMDescription.Text;
                        o.HaveSN = hddSN.Value;
                        o.SNID = hddMSNID.Value;

                        #region DetailProduct
                        if(gvDetail.Rows.Count > 0)
                        {
                            o.ProductDetail = new List<Entities.MasPackageDetail>();
                            HiddenField hdd = new HiddenField();
                            ImageButton imb = new ImageButton();
                            Entities.MasPackageDetail pd = new Entities.MasPackageDetail();
                            foreach (GridViewRow item in gvDetail.Rows)
                            {
                                pd = new Entities.MasPackageDetail();
                                hdd = (HiddenField)item.FindControl("hddProductID");
                                pd.ProductID = (hdd != null && !string.IsNullOrEmpty(hdd.Value)) ? ToInt32(hdd.Value) : 0;

                                hdd = (HiddenField)item.FindControl("hddSaleDetailID");
                                pd.PackageDetailID = (hdd != null && !string.IsNullOrEmpty(hdd.Value)) ? ToInt32(hdd.Value) : 0;
                                pd.Amount = ToInt32(item.Cells[2].Text);
                                pd.ProductCode = item.Cells[0].Text;
                                pd.ProductName = item.Cells[1].Text;

                                imb = (ImageButton)item.FindControl("imgbtnDelete");
                                pd.CanChange = (imb != null && imb.Visible == true) ? "Y" : "N";

                                hdd = (HiddenField)item.FindControl("hddIsFree");
                                pd.IsFree = (hdd != null && !string.IsNullOrEmpty(hdd.Value)) ? hdd.Value : "N";
                                o.ProductDetail.Add(pd);
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    o = new SaleDetailDTO();
                    o.SaleDetailID = lst.Count == 0 ? 1 : ToInt32(lst.Max(m => m.SaleDetailID).ToString()) + 1;
                    o.ItemID = ItemID; // ToInt32(hddItemID.Value);
                    //o.StockID = ToInt32(hddStockID.Value);
                    o.ItemName = txtMItem.Text;
                    o.ItemPrice = ToDoudle(txtMPrice.Text);
                    o.Discount = ToDoudle(txtMDiscount.Text);
                    //o.DiscountPer = ToDoudle(txtMDiscountPer.Text);
                    o.Amount = ToDoudle(txtMAmount.Text);
                    o.SerialNumber = txtMSN.Text;
                    o.ItemDescription = GetItemDesc();// txtMDescription.Text;
                    o.Status = "New";
                    o.HaveSN = hddSN.Value;
                    o.SNID = hddMSNID.Value;

                    #region DetailProduct
                    if (gvDetail.Rows.Count > 0)
                    {
                        o.ProductDetail = new List<Entities.MasPackageDetail>();
                        HiddenField hdd = new HiddenField();
                        ImageButton imb = new ImageButton();
                        Entities.MasPackageDetail pd = new Entities.MasPackageDetail();
                        foreach (GridViewRow item in gvDetail.Rows)
                        {
                            pd = new Entities.MasPackageDetail();
                            hdd = (HiddenField)item.FindControl("hddProductID");
                            pd.ProductID = (hdd != null && !string.IsNullOrEmpty(hdd.Value)) ? ToInt32(hdd.Value) : 0;

                            hdd = (HiddenField)item.FindControl("hddSaleDetailID");
                            pd.PackageDetailID = (hdd != null && !string.IsNullOrEmpty(hdd.Value)) ? ToInt32(hdd.Value) : 0;
                            pd.Amount = ToInt32(item.Cells[2].Text);
                            pd.ProductCode = item.Cells[0].Text;
                            pd.ProductName = item.Cells[1].Text;
                            
                            imb = (ImageButton)item.FindControl("imgbtnDelete");
                            pd.CanChange = (imb != null && imb.Visible == true) ? "Y" : "N";

                            hdd = (HiddenField)item.FindControl("hddIsFree");
                            pd.IsFree = (hdd != null && !string.IsNullOrEmpty(hdd.Value)) ? hdd.Value : "N";
                            o.ProductDetail.Add(pd);
                        }
                    }
                    #endregion

                    lst.Add(o);
                }
                                
                Session["saleDetail"] = lst;
                #endregion

                //ShowMessageBox("Save Succ");
                BindDataGrid();
                gvItem.Focus();
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                //SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
                Logs(ex.Message);
            }
        }     
        
        protected string GetItemDesc()
        {
            string result = "";
            try
            {
                int i = 1, c = 1;
                string ItemName = txtMItem.Text;
                foreach (GridViewRow item in gvDetail.Rows)
                {
                    if(ItemName.Trim() != item.Cells[1].Text)
                    {
                        result = result + c.ToString() + "." + i.ToString() + " "+ item.Cells[1].Text + " จำนวน " + item.Cells[2].Text + " \r\n";
                        i++;
                    } 
                }

                if(!string.IsNullOrEmpty(result))
                    result = result.Substring(0, result.Length - 2);
                
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return result;
        }
        

        protected void ClearTextDetail()
        {
            hddDetailID.Value = "0";
            hddItemID.Value = "0";
            txtMItem.Text = "";
            txtMPrice.Text = "";
            txtMAmount.Text = "1";
            txtMDiscount.Text = "0";
            //txtMDiscountPer.Text = "0";
            txtMSN.Text = "";
            //txtMDescription.Text = "";
            gvDetail.DataSource = null;
            gvDetail.DataBind();
            lkbm1Add.Visible = false;
            imgbtnSearchItem.Visible = true;
            imgbtnMSN.Visible = false;
        }

        protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();                
                ImageButton imb = (ImageButton)sender;
                if (imb != null)
                {
                    int objID = ToInt32(imb.CommandArgument);
                    List<SaleDetailDTO> lst = new List<SaleDetailDTO>();
                    SaleDetailDTO obj = new SaleDetailDTO();
                    if (Session["saleDetail"] != null)
                    {
                        lst = (List<SaleDetailDTO>)Session["saleDetail"];
                        if (lst != null && lst.Count > 0)
                        {
                            obj = lst.FirstOrDefault(w => w.SaleDetailID.Equals(objID));
                            if (obj != null)
                            {
                                hddDetailID.Value = obj.SaleDetailID.ToString();
                                hddItemID.Value = obj.ItemID.ToString();                                
                                txtMItem.Text = obj.ItemName;
                                txtMPrice.Text = obj.ItemPriceStr;
                                txtMAmount.Text = obj.AmountStr;
                                txtMDiscount.Text = obj.DiscountStr;
                                txtMSN.Text = obj.SerialNumber;
                                //txtMDescription.Text = obj.ItemDescription;
                                hddSN.Value = obj.HaveSN;

                                BindPackageDetail(obj.ProductDetail);
                                imgbtnSearchItem.Visible = false;
                                if (obj.HaveSN == "Y")
                                    imgbtnMSN.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;
                if (imb != null)
                {
                    int objID = ToInt32(imb.CommandArgument);
                    List<SaleDetailDTO> lst = new List<SaleDetailDTO>();
                    SaleDetailDTO obj = new SaleDetailDTO();
                    if (Session["saleDetail"] != null)
                    {
                        lst = (List<SaleDetailDTO>)Session["saleDetail"];
                        if (lst != null && lst.Count > 0)
                        {
                            obj = lst.FirstOrDefault(w => w.SaleDetailID.Equals(objID));
                            if(obj != null)
                                lst.Remove(obj);
                            //obj.Status = "Delete";
                            Session["saleDetail"] = lst;
                        }
                    }

                    BindDataGrid();
                }
                else
                {
                    SendMailError("imb is null", System.Reflection.MethodBase.GetCurrentMethod());
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
        protected void imgbtnMSN_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 PackageHeaderID = ToInt32(hddItemID.Value);
                SearchSNByPackageHeader(PackageHeaderID);
                
                ModalPopupExtender1.Show();
                ModalPopupExtender6.Show();
            }
            catch (Exception ex)
            {
                
            }
        }
        //Add gvDetail
        protected void lkbm1Add_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                ModalPopupExtender4.Show();
                SearchProduct();
                txtm5Amount.Text = "";
                hddm1AddStatus.Value = "Y";
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
        //Delete from gvDetail
        protected void imgbtngvDetailDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;
                if (imb != null)
                {
                    Int32 DetailID = ToInt32(hddDetailID.Value);
                    int objID = ToInt32(imb.CommandArgument);
                    List<SaleDetailDTO> lst = new List<SaleDetailDTO>();
                    SaleDetailDTO obj = new SaleDetailDTO();
                    Entities.MasPackageDetail pd = new Entities.MasPackageDetail();

                    if (Session["saleDetailProduct"] != null)
                    {
                        List<Entities.MasPackageDetail> lstPD = (List<Entities.MasPackageDetail>)Session["saleDetailProduct"];
                        if (lstPD != null && lstPD.Count > 0)
                        {
                            pd = lstPD.FirstOrDefault(w => w.PackageDetailID.Equals(objID));
                            if (pd != null)
                            {
                                lstPD.Remove(pd);
                                Session["saleDetailProduct"] = lstPD;
                                gvDetail.DataSource = lstPD;
                                gvDetail.DataBind();
                            }
                        }
                    }
                    else
                    {
                        ShowMessageBox("Session TimeOut. (SaleDetailProduct)");
                        return;

                    }
                    //else if (Session["saleDetail"] != null)
                    //{
                    //    lst = (List<SaleDetailDTO>)Session["saleDetail"];
                    //    if (lst != null && lst.Count > 0)
                    //    {
                    //        obj = lst.FirstOrDefault(w => w.SaleDetailID.Equals(DetailID));
                    //        if(obj != null && obj.ProductDetail != null && obj.ProductDetail.Count > 0)
                    //        {
                    //            pd = obj.ProductDetail.FirstOrDefault(w => w.PackageDetailID.Equals(objID));
                    //            if(pd != null)
                    //            {
                    //                obj.ProductDetail.Remove(pd);
                    //                gvDetail.DataSource = obj.ProductDetail;
                    //                gvDetail.DataBind();
                    //            }
                    //        }
                    //        Session["saleDetail"] = lst;
                    //    }
                    //}
                    
                    ModalPopupExtender1.Show();
                }
                else
                {
                    SendMailError("imb is null", System.Reflection.MethodBase.GetCurrentMethod());
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        protected void SearchSNByPackageHeader(Int32 PackageHeaderID)
        {
            try
            {
                var dal = ItemDal.Instance;
                List<Entities.TransProductSerial> lst = dal.GetSearchSNByPackageHeaderID(PackageHeaderID);
                if(lst != null)
                {
                    gvm6SN.DataSource = lst;
                    gvm6SN.DataBind();
                }
                else
                {
                    gvm6SN.DataSource = null;
                    gvm6SN.DataBind();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region Customer Modal2
        protected void btnMSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender2.Show();
                Session["pageindexSale"] = "1";
                BindSearch();
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ClearTextSearch();
                Session["pageindexSale"] = "1";
                BindSearch();
                ModalPopupExtender2.Show();
                
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void BindSearch()
        {
            try
            {
                string Name = txtSearchCust.Text;
                string Add = txtSearchAdd.Text;
                int PageSize = 10;
                int RowIndex = 0;
                int PageIndex = Session["pageindexSale"] == null ? 1 : Convert.ToInt32(Session["pageindexSale"]);
                List<CustomerDTO> lst = new List<CustomerDTO>();
                List<CustomerDTO> Temp = new List<CustomerDTO>();                
                using (BillingEntities cre = new BillingEntities())
                {
                    lst = (from h in cre.GetCustomerDist()                         
                           select new CustomerDTO()
                           {
                               CustomerName = h.CustomerName,
                               CustomerAddress = h.CustomerAddress,
                               CustomerDistrict = h.CustomerDistrict,
                               CustomerCountry = h.CustomerCountry,
                               CustomerProvince = h.CustomerProvince,
                               CustomerPostalCode = h.CustomerPostalCode,
                               //CustomerAddress2 = h.CustomerAddress2,
                               //CustomerAddress3 = h.CustomerAddress3,
                               DeliveryName = h.DeliveryName,
                               DeliverAdd = h.DeliverAdd,
                               DeliverDistrict = h.DeliverDistrict,
                               DeliverCountry = h.DeliverCountry,
                               DeliverProvince = h.DeliverProvince,
                               DeliverPostalCode = h.DeliverPostalCode,
                               Tel = h.Tel,
                           }).Distinct().ToList();

                    if (lst != null)
                    {
                        if (!string.IsNullOrEmpty(Name))
                        {
                            lst = lst.Where(w => w.CustomerName.ToLower().Contains(Name.ToLower()) || w.DeliveryName.ToLower().Contains(Name.ToLower())).ToList();
                        }
                        
                        if (!string.IsNullOrEmpty(Add))
                        {
                            lst = lst.Where(w => w.CustomerAddressAll.ToLower().Contains(Add.ToLower()) || w.DeliverAddressAll.ToLower().Contains(Add.ToLower())).ToList();
                        }

                        RowIndex = PageIndex == 1 ? 0 : (PageIndex - 1) * PageSize;
                        if (lst.Count < PageSize)
                        {
                            Temp = lst;
                        }
                        else
                        {
                            Temp = lst.GetRange(RowIndex, PageSize);
                        }
                        
                        gvCust.DataSource = Temp;
                        gvCust.DataBind();

                        if(PageIndex == 1)
                        {
                            btnMPrevious.Visible = false;
                        }
                        else
                        {
                            btnMPrevious.Visible = true;
                        }


                        if ((RowIndex + PageSize) > lst.Count)
                        {
                            btnMNext.Visible = false;
                        }
                        else
                        {
                            btnMNext.Visible = true;
                        }
                    }
                    else
                    {
                        gvCust.DataSource = null;
                        gvCust.DataBind();
                    }
                };
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void ClearTextSearch()
        {
            try
            {
                txtSearchAdd.Text = "";
                txtSearchCust.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        protected void imgbtnChoose_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;
                if(imb != null)
                {
                    Int32 index = ToInt32(imb.CommandArgument);
                    //ใบเสร็จ
                    txtCustName.Text = gvCust.Rows[index].Cells[1].Text;
                    txtCustAddress.Text = gvCust.Rows[index].Cells[2].Text;
                    txtCustDistrict.Text = gvCust.Rows[index].Cells[3].Text;
                    txtCustCountry.Text = gvCust.Rows[index].Cells[4].Text;
                    txtCustProvince.Text = gvCust.Rows[index].Cells[5].Text;
                    txtCustPostalCode.Text = gvCust.Rows[index].Cells[6].Text;
                    
                    //ส่งสินค้า
                    txtDeliveryName.Text = gvCust.Rows[index].Cells[7].Text;
                    txtDeliverAddress.Text = gvCust.Rows[index].Cells[8].Text;
                    txtDeliverDistrict.Text = gvCust.Rows[index].Cells[9].Text;
                    txtDeliverCountry.Text = gvCust.Rows[index].Cells[10].Text;
                    txtDeliverProvince.Text = gvCust.Rows[index].Cells[11].Text;
                    txtDeliverPostalCode.Text = gvCust.Rows[index].Cells[12].Text;
                    //txtCustAddress2.Text = gvCust.Rows[index].Cells[2].Text;
                    //txtCustAddress3.Text = gvCust.Rows[index].Cells[3].Text;
                    txtTel.Text = gvCust.Rows[index].Cells[13].Text == "&nbsp;" ? "" : gvCust.Rows[index].Cells[13].Text;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnCopyAdd_Click(object sender, EventArgs e)
        {
            try
            {
                txtDeliveryName.Text = txtCustName.Text;
                txtDeliverAddress.Text = txtCustAddress.Text;
                txtDeliverDistrict.Text = txtCustDistrict.Text;
                txtDeliverCountry.Text = txtCustCountry.Text;
                txtDeliverProvince.Text = txtCustProvince.Text;
                txtDeliverPostalCode.Text = txtCustPostalCode.Text;
                //txtDeliverAddress2.Text = txtCustAddress2.Text;
                //txtDeliverAddress3.Text = txtCustAddress3.Text;
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnMPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender2.Show();
                int PageIndex = Session["pageindexSale"] == null ? 1 : Convert.ToInt32(Session["pageindexSale"]);
                Session["pageindexSale"] = PageIndex - 1;
                BindSearch();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnMNext_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender2.Show();
                int PageIndex = Session["pageindexSale"] == null ? 1 : Convert.ToInt32(Session["pageindexSale"]);
                Session["pageindexSale"] = PageIndex + 1;
                BindSearch();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Modal Item Modal3
        protected void btnMItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                ModalPopupExtender3.Show();
                SearchItem();
                ClearTextSearchItem();
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void imgbtnChooseItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                ImageButton imb = (ImageButton)sender;
                HiddenField hdd = new HiddenField();
                TextBox txt = new TextBox();
                if (imb != null)
                {
                    Int32 index = ToInt32(imb.CommandArgument);
                    hdd = (HiddenField)gvItemSearch.Rows[index].FindControl("hddItemID");
                    if (hdd != null)
                    {
                        hddItemID.Value = hdd.Value;
                        txtMItem.Text = gvItemSearch.Rows[index].Cells[1].Text;
                        txtMPrice.Text = gvItemSearch.Rows[index].Cells[2].Text;
                        
                        #region ItemDetail
                        var dal = ItemDal.Instance;
                        List<Entities.MasPackageDetail> lst = dal.GetSearchItemDetailByID(ToInt32(hddItemID.Value));
                        if (lst != null)
                        {
                            BindPackageDetail(lst);
                            lkbm1Add.Visible = true;
                            if(lst.Count(w => w.ProductSN.ToLower().Equals("y")) > 0)
                            {
                                imgbtnMSN.Visible = true;
                                hddSN.Value = "Y";
                            }
                            else
                            {
                                imgbtnMSN.Visible = false;
                                hddSN.Value = "N";
                            }
                        }
                        #endregion
                    }                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void imgbtnSearchItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                ModalPopupExtender3.Show();
                SearchItem();                
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void SearchItem()
        {
            try
            {
                string ItemCode = txtSearchItemCode.Text.ToLower();
                string ItemName = txtSearchItemName.Text.ToLower();
                var dal = StockDal.Instance;
                List<Entities.MasPackageHeader> lst = new List<Entities.MasPackageHeader>();
                lst = dal.GetItemInStock();
                if (lst != null)
                {
                    if (!string.IsNullOrEmpty(ItemCode))
                        lst = lst.Where(w => w.PackageCode.ToLower().Contains(ItemCode)).ToList();

                    if (!string.IsNullOrEmpty(ItemName))
                    {
                        lst = lst.Where(w => w.PackageName.ToLower().Contains(ItemName)).ToList();
                    }

                    lst = lst.OrderBy(od => od.PackageHeaderID).ToList();
                    gvItemSearch.DataSource = lst;
                    gvItemSearch.DataBind();
                }
                else
                {
                    gvItemSearch.DataSource = null;
                    gvItemSearch.DataBind();
                }
            }
            catch (Exception ex)
            {
                gvItemSearch.DataSource = null;
                gvItemSearch.DataBind();
            }
        }

        protected void btnModalClose3_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void ClearTextSearchItem()
        {
            try
            {
                txtSearchItemCode.Text = "";
                txtSearchItemName.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        protected void BindPackageDetail(List<Entities.MasPackageDetail> lst)
        {
            try
            {
                Session["saleDetailProduct"] = lst;
                gvDetail.DataSource = lst;
                gvDetail.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region Search Product Model4
        protected void btnSearchItemProduct_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                ModalPopupExtender4.Show();
                SearchProduct();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnModalClose4_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void imgbtnChooseProduct_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                ModalPopupExtender4.Show();
                ModalPopupExtender5.Show();
                ImageButton imb = (ImageButton)sender;
                if(imb != null)
                {
                    hddm5Index.Value = imb.CommandArgument;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void SearchProduct()
        {
            try
            {
                var dal = ItemDal.Instance;
                List<Entities.MasProduct> lst = dal.GetSearchProductAll();
                if(lst != null && lst.Count > 0)
                {
                    string ProductName = txtProductName.Text.ToLower();
                    if (!string.IsNullOrEmpty(ProductName))
                    {
                        lst = lst.Where(w => w.ProductName.ToLower().Contains(ProductName)).ToList();
                    }
                    gvProduct.DataSource = lst;
                    gvProduct.DataBind();
                    Session["ProductList"] = lst;
                }
                else
                {
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Amount Modal5 
        protected void btnm5OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["ProductList"] != null)
                {
                    string FreeStatus = "N";
                    Int32 ProductID = ToInt32(hddm5Index.Value);
                    List<Entities.MasProduct> lst = new List<Entities.MasProduct>();
                    Entities.MasProduct masPro = new Entities.MasProduct();
                    lst = (List<Entities.MasProduct>)Session["ProductList"];
                    masPro = lst.FirstOrDefault(w => w.ProductID.Equals(ProductID));
                    if (masPro != null && Session["saleDetailProduct"] != null)
                    {
                        if(hddm1AddStatus.Value == "Y")
                        {
                            FreeStatus = "Y";
                            hddm1AddStatus.Value = "N";
                        }

                        List<Entities.MasPackageDetail> lstDetailProduct = (List<Entities.MasPackageDetail>)Session["saleDetailProduct"];
                        lstDetailProduct.Add(new Entities.MasPackageDetail()
                        {
                            ProductID = masPro.ProductID,
                            ProductCode = masPro.ProductCode,
                            ProductName = masPro.ProductName,
                            Amount = ToInt32(txtm5Amount.Text),
                            CanChange = "Y",
                            IsFree = FreeStatus,
                        });
                        BindPackageDetail(lstDetailProduct);
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        ShowMessageBox("Session Timeout. !!!", this, "../Index.aspx");
                    }
                }
                else
                {
                    ShowMessageBox("Session Timeout. !!!", this, "../Index.aspx");
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnm5Cancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
            ModalPopupExtender4.Show();
            hddm5Index.Value = "";
        }

        #endregion

        #region Modal6
        protected void btnm6OK_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 amt = ToInt32(txtMAmount.Text);
                Int32 i = 0;
                string sn = "", id = "", msg = "";
                HiddenField hdd = new HiddenField();
                CheckBox chk = new CheckBox();
                foreach (GridViewRow item in gvm6SN.Rows)
                {
                    hdd = (HiddenField)item.FindControl("hddTransID");
                    chk = (CheckBox)item.FindControl("chkm6SN");
                    if (chk != null && hdd != null)
                    {
                        if(chk.Checked)
                        {
                            id = id + hdd.Value + ",";
                            sn = sn + item.Cells[1].Text + ", ";
                            i++;
                        }
                    }
                    
                    if(i > amt)
                    {
                        msg = "Choose Serial Number Too much.";
                        break;
                    }
                }

                if(!string.IsNullOrEmpty(msg))
                {
                    ShowMessageBox(msg);
                }
                else
                {
                    sn = sn.Substring(0, sn.Length - 2);
                    id = id.Substring(0, id.Length - 1);
                    txtMSN.Text = sn;
                    hddMSNID.Value = id;
                }
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnm6Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                
            }
        }

        #endregion

        #region Modal 7 SN From Grid --> Edit Mode
        protected void imgbtnSN_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                if(img != null)
                {
                    hddm7DetailID.Value = img.CommandArgument;
                    Int32 PackageHeaderID = ToInt32(img.CommandName);
                    SearchSNByPackageHeaderM7(PackageHeaderID);

                    ModalPopupExtender7.Show();
                }
                else
                {
                    ShowMessageBox("Error --> Sender is Null.");
                    return;
                }
                
            }
            catch (Exception ex)
            {
                ShowMessageBox("Error --> " + ex.Message);
            }
        }

        protected void btnm7OK_Click(object sender, EventArgs e)
        {
            try
            {
                if(Session["saleDetail"] != null)
                {
                    List<Entities.DTO.SaleDetailDTO> lstDetail = new List<Entities.DTO.SaleDetailDTO>();
                    lstDetail = (List<Entities.DTO.SaleDetailDTO>)Session["saleDetail"];
                    if(lstDetail != null && lstDetail.Count > 0)
                    {
                        double Amt = 0;
                        Int32 SaleDetailID = 0, i = 0;
                        string oldSN = "";
                        SaleDetailID = ToInt32(hddm7DetailID.Value);
                        Entities.DTO.SaleDetailDTO o = lstDetail.FirstOrDefault(w => w.SaleDetailID.Equals(SaleDetailID));
                        if(o != null)
                        {
                            oldSN = o.SerialNumber;
                            Amt = o.Amount.HasValue ? o.Amount.Value : 0;
                            HiddenField hdd = new HiddenField();
                            CheckBox chk = new CheckBox();
                            string sn = "", id = "", msg = "";
                            foreach (GridViewRow item in gvm7SN.Rows)
                            {
                                hdd = (HiddenField)item.FindControl("hddm7TransID");
                                chk = (CheckBox)item.FindControl("chkm7SN");
                                if (chk != null && hdd != null)
                                {
                                    if (chk.Checked)
                                    {
                                        id = id + hdd.Value + ",";
                                        sn = sn + item.Cells[1].Text + ", ";
                                        i++;
                                    }
                                }

                                if (i > Amt)
                                {
                                    msg = "Choose Serial Number Too much.";
                                    break;
                                }
                            }

                            if (!string.IsNullOrEmpty(msg)) // --> Case Error
                            {
                                ShowMessageBox(msg);
                                return;
                            }
                            else // --> Case Success
                            {
                                // Get Old SN To Remark
                                txtRemark.Text = txtRemark.Text + " Old S/N --> " + oldSN;

                                //Sub String SN & ID
                                sn = sn.Substring(0, sn.Length - 2);
                                id = id.Substring(0, id.Length - 1);

                                //Save ID to HDD For btnSave
                                hddm7SNID.Value = id;

                                //Change Grid
                                o.SNID = id;
                                o.SerialNumber = sn;
                                o.UpdateSN = "Y";

                                BindDataGrid();
                            }
                        }
                    }
                    else
                    {
                        ShowMessageBox("List Detail is null. !!!");
                        return;
                    }
                }
                else
                {
                    ShowMessageBox("Session is null. !!!");
                    return;
                }

            }
            catch (Exception ex)
            {
                ShowMessageBox("Error --> " + ex.Message);
            }
        }

        protected void btnm7Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                ShowMessageBox("Error --> " + ex.Message);
            }
        }

        protected void SearchSNByPackageHeaderM7(Int32 PackageHeaderID)
        {
            try
            {
                var dal = ItemDal.Instance;
                List<Entities.TransProductSerial> lst = dal.GetSearchSNByPackageHeaderID(PackageHeaderID);
                if (lst != null)
                {
                    gvm7SN.DataSource = lst;
                    gvm7SN.DataBind();
                }
                else
                {
                    gvm7SN.DataSource = null;
                    gvm7SN.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox("Error --> " + ex.Message);
            }
        }
        #endregion
    }
}