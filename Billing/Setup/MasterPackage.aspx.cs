using Entities;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace Billing.Setup
{
    public partial class MasterPackage : Billing.Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ID = Request.QueryString.Get("ID");
                hddID.Value = ID;                
                Session["saleDetail"] = null;
                BindDDL();
                BindData();
            }
        }

        #region BindData
        protected void BindDDL()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void BindData()
        {
            try
            {
                if (!string.IsNullOrEmpty(hddID.Value))
                {
                    int tid = ToInt32(hddID.Value);
                    MasPackageHeader obj = new MasPackageHeader();
                    
                    
                }
                else
                {
                    
                }
                BindDataGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void BindDataGrid()
        {
            try
            {
                List<MasPackageDetail> lst = new List<MasPackageDetail>();
                //Get Data

                gvItem.DataSource = lst;
                gvItem.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate
                //if (txtCustName.Text == "")
                //{
                //    ShowMessageBox("กรุณาระบุ ชื่อลูกค้า");
                //    return;
                //}
                //if (txtDate.Text == "")
                //{
                //    ShowMessageBox("กรุณาระบุ วันที่");
                //    return;
                //}
                
                int hid = 0;
                
                if (string.IsNullOrEmpty(hddID.Value)) //New --> Insert
                {
                    
                }
                else // --> Update
                {
                    hid = ToInt32(hddID.Value);
                    
                }

                ShowMessageBox("บันทึกสำเร็จ.", this.Page, "MasterPackageList.aspx");
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        #region Event Detail
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

                //if (txtMDiscountPer.Text != "" && ToDoudle(txtMDiscountPer.Text) > 100)
                //{
                //    ShowMessageBox("ระบุส่วนลด % ไม่ถูกต้อง");
                //    ModalPopupExtender1.Show();
                //    return;
                //}

                ItemID = ToInt32(hddItemID.Value);
                //using (BillingEntities cre = new BillingEntities())
                //{
                //    Count = cre.TransStocks.Where(w => w.ItemID.Equals(ItemID) && w.Active.ToLower().Equals("y")).Count();
                //}
                //if(Count == 0)
                //{
                //    ShowMessageBox("สินค้า " + txtMItem.Text + " ไม่มีในสต๊อก กรุณาเพิ่มสินค้าก่อนทำรายการขาย !!");
                //    ModalPopupExtender1.Show();
                //    return;
                //}
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
                        o.StockID = ToInt32(hddStockID.Value);
                        o.ItemName = txtMItem.Text;
                        o.ItemPrice = ToDoudle(txtMPrice.Text);
                        o.Discount = ToDoudle(txtMDiscount.Text);
                        //o.DiscountPer = ToDoudle(txtMDiscountPer.Text);
                        o.Amount = ToDoudle(txtMAmount.Text);
                        o.SerialNumber = txtMSN.Text;
                        o.ItemDescription = GetItemDesc();// txtMDescription.Text;
                    }
                }
                else
                {
                    o = new SaleDetailDTO();
                    o.SaleDetailID = lst.Count == 0 ? 1 : ToInt32(lst.Max(m => m.SaleDetailID).ToString()) + 1;
                    o.ItemID = ItemID; // ToInt32(hddItemID.Value);
                    o.StockID = ToInt32(hddStockID.Value);
                    o.ItemName = txtMItem.Text;
                    o.ItemPrice = ToDoudle(txtMPrice.Text);
                    o.Discount = ToDoudle(txtMDiscount.Text);
                    //o.DiscountPer = ToDoudle(txtMDiscountPer.Text);
                    o.Amount = ToDoudle(txtMAmount.Text);
                    o.SerialNumber = txtMSN.Text;
                    o.ItemDescription = GetItemDesc();// txtMDescription.Text;
                    o.Status = "New";
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
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }     
        
        protected string GetItemDesc()
        {
            string result = "";
            try
            {
                result = txtMDescription.Text;
                //TextBox txt = new TextBox();
                //for (int i = 1; i <= 10; i++)
                //{
                //    txt = (TextBox)Panel1.FindControl("txtMDesc" + i.ToString());
                //    if (txt != null && txt.Visible)
                //    {
                //        result = result + txt.Text + "\r\n";
                //    }
                //}
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
            return result;
        }

        protected void BindItemDesc(string ItemDesc)
        {
            try
            {
                if(!string.IsNullOrEmpty(ItemDesc))
                {
                    string[] spl = ItemDesc.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    if(spl != null)
                    {
                        TextBox txt = new TextBox();
                        for (int i = 1; i <= 10; i++)
                        {
                            txt = (TextBox)Panel1.FindControl("txtMDesc" + i.ToString());
                            if (txt != null && !string.IsNullOrEmpty(spl[i-0]))
                            {
                                txt.Text = spl[i - 0];
                                txt.Visible = true;
                            }
                            else
                            {
                                txt.Visible = false;
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
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
            txtMDescription.Text = "";
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
                                //txtMDiscountPer.Text = obj.DiscountPerStr;
                                txtMSN.Text = obj.SerialNumber;
                                //BindItemDesc(obj.ItemDescription);
                                txtMDescription.Text = obj.ItemDescription;
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
                            obj.Status = "Delete";
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

        #endregion                

        
        #region Modal Item
        protected void btnMItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                ModalPopupExtender3.Show();
                SearchItem();
                //ClearTextSearchItem();
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
                HiddenField hddStock = new HiddenField();
                TextBox txt = new TextBox();
                Int32 i = 1;
                if (imb != null)
                {
                    Int32 index = ToInt32(imb.CommandArgument);
                    hdd = (HiddenField)gvItemSearch.Rows[index].FindControl("hddItemID");
                    hddStock = (HiddenField)gvItemSearch.Rows[index].FindControl("hddStockID");
                    if (hdd != null && hddStock != null)
                    {
                        hddItemID.Value = hdd.Value;
                        hddStockID.Value = hddStock.Value;
                        txtMItem.Text = gvItemSearch.Rows[index].Cells[1].Text;
                        txtMPrice.Text = gvItemSearch.Rows[index].Cells[2].Text;
                        //txtMSN.Text = gvItemSearch.Rows[index].Cells[2].Text;
                        //txtMDescription.Text = gvItemSearch.Rows[index].Cells[3].Text;
                        txtMDescription.Text = ((Label)gvItemSearch.Rows[index].FindControl("lbItemDesc")).Text;
                        #region ItemDetail
                        //var dal = ItemDal.Instance;
                        //List<MasItemDTO> lst = dal.GetSearchItemDetailByID(ToInt32(hddItemID.Value));
                        //if (lst != null)
                        //{
                        //    foreach (MasItemDTO item in lst)
                        //    {
                        //        txt = (TextBox)Panel1.FindControl("txtMDesc" + i.ToString());
                        //        if(txt != null)
                        //        {
                        //            txt.Text = item.ItemDetail;
                        //            txt.ReadOnly = item.CanChange == "N" ? true : false;
                        //            txt.Visible = !string.IsNullOrEmpty(item.ItemDetail);
                        //        }

                        //        i++;
                        //    }
                        //}
                        #endregion
                    }                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void imgbtnSearchItem_Click_Click(object sender, ImageClickEventArgs e)
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
                List<Entities.DTO.InventoryDTO> lst = new List<Entities.DTO.InventoryDTO>();
                var dal = StockDal.Instance;
                lst = dal.GetItemInStock();
                if (lst != null)
                {
                    if (!string.IsNullOrEmpty(ItemCode))
                        lst = lst.Where(w => w.ItemCode.ToLower().Contains(ItemCode)).ToList();

                    if (!string.IsNullOrEmpty(ItemName))
                    {
                        lst = lst.Where(w => w.ItemName.ToLower().Contains(ItemName)).ToList();
                    }

                    lst = lst.OrderBy(od => od.ItemID).OrderBy(od => od.StockID).ToList();
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
        #endregion
      
    }
}