using Billing.AppData;
using Billing.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Billing.Transaction
{
    public partial class TransactionSaleCancelList : Billing.Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["TransItemCancelList"] = null;
                DateTime dt = DateTime.Now;
                txtDateTo.Text = dt.ToString("dd/MM/yyyy");
                txtDateFrom.Text = dt.ToString("dd/MM/yyyy");
                BindDDL();
                BindData();
            }
        }

        protected void BindDDL()
        {
            try
            {
                List<MasItem> lst = new List<MasItem>();
                using (BillingEntities cre = new BillingEntities())
                {
                    lst = cre.MasItems.ToList();
                };

                if (lst != null)
                {
                    Session["TransItemCancelList"] = lst;
                    ddlItem.DataSource = lst;
                    ddlItem.DataValueField = "ItemID";
                    ddlItem.DataTextField = "ItemName";
                }
                else
                {
                    Session["TransItemCancelList"] = null;
                    ddlItem.DataSource = null;
                }

                ddlItem.DataBind();
                ddlItem.Items.Insert(0, "");
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        protected void BindData()
        {
            try
            {
                List<SaleHeaderDTO> lst = new List<SaleHeaderDTO>();
                List<SaleHeaderDTO> lstMod = new List<SaleHeaderDTO>();
                DateTime dateFrom = string.IsNullOrEmpty(txtDateFrom.Text) ? DateTime.MinValue : DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                DateTime dateTo = string.IsNullOrEmpty(txtDateTo.Text) ? DateTime.MaxValue : DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")).AddDays(1);
                //DateTime date = string.IsNullOrEmpty(txtDate.Text) ? DateTime.MinValue : DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                string AccName = txtCustName.Text;
                string SN = txtSN.Text;
                string Tel = txtTel.Text;
                Int32 ItemID = 0;
                ItemID = ToInt32(ddlItem.SelectedItem.Value);

                using (BillingEntities cre = new BillingEntities())
                {
                    lst = (from d in cre.GetTransSaleList(dateFrom, dateTo)
                           select new SaleHeaderDTO()
                           {
                               SaleHeaderID = d.SaleHeaderID,
                               CustomerName = d.CustomerName,                                  
                               Tel = d.tel,
                               ReceivedDate = d.ReceivedDate,
                               ReceivedBy = d.ReceivedBy,
                               SaleNumber = d.SaleNumber,
                               ItemCode = d.ItemCode,
                               ItemName = d.ItemName,
                               ItemID = d.ItemID.HasValue ? d.ItemID.Value : 0,
                               dAmount = d.Amount,
                               ItemPrice = d.ItemPrice.HasValue ? d.ItemPrice.Value : 0,
                               Discount = d.Discount.HasValue ? d.Discount.Value : 0,
                               SerialNumber = d.SerialNumber,
                               BillType = d.BillType,
                           }).ToList();

                };

                if (lst != null && lst.Count > 0)
                {
                    if (AccName != "")
                        lst = lst.Where(w => w.CustomerName.Contains(AccName)).ToList();

                    if (Tel != "")
                        lst = lst.Where(w => !string.IsNullOrEmpty(w.Tel) && w.Tel.Contains(Tel)).ToList();

                    if(SN != "")
                        lst = lst.Where(w => !string.IsNullOrEmpty(w.SerialNumber) && w.SerialNumber.Contains(SN)).ToList();

                    if (ItemID != 0)                        
                        lst = lst.Where(w => w.ItemID.Equals(ItemID)).ToList();

                    if (lst != null && lst.Count > 0)
                    {
                        lstMod = ModDataFromSale(lst);
                        gv.DataSource = lstMod;
                    }

                    if (ItemID != 0 || SN != "")
                        gv.Columns[5].Visible = false;
                    else
                        gv.Columns[5].Visible = true;
                }
                else
                    gv.DataSource = null;

                gv.DataBind();                

            }
            catch (Exception ex)
            {
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
        
        #region Image Button
        

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = new ImageButton();
                Int32 RowIndex = -1;
                imb = (ImageButton)sender;
                if (imb != null)
                {
                    hddHeaderID.Value = imb.CommandArgument;
                    RowIndex = ToInt32(imb.CommandName);
                    if(RowIndex >= 0)
                    {
                        ((Label)Panel1.FindControl("lbl_modal_view")).Text = "กรุณาระบุยอดเงินของรายการ [ " + gv.Rows[RowIndex].Cells[1].Text + " ]";
                        //lbl_modal_view.Text = 
                        hddSellPrice.Value = gv.Rows[RowIndex].Cells[5].Text;
                        txtmSellPrice.Text = "";
                    }
                }
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region Modal
        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                double SellPrice = ToDoudle(txtmSellPrice.Text.Replace(",", ""));
                double SellPriceHdd = ToDoudle(hddSellPrice.Value);
                Int32 ID = ToInt32(hddHeaderID.Value);
                TransSaleHeader o = new TransSaleHeader();
                using (BillingEntities cre = new BillingEntities())
                {
                    o = cre.TransSaleHeaders.FirstOrDefault(w => w.SaleHeaderID.Equals(ID));
                    if (o != null && SellPrice == SellPriceHdd)
                    {
                        o.Active = "0";
                        cre.SaveChanges();
                    }
                };
                
                ShowMessageBox("ยกเลิกรายการขายเรียบร้อยแล้ว.");
                BindData();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}