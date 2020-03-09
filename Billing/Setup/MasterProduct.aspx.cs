//using Billing.AppData;
using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Billing.Setup
{
    public partial class MasterProduct : Billing.Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                BindDDL();
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
                var dal = ProductDal.Instance;
                List<MasProduct> lst = new List<MasProduct>();

                MasProduct ModelSer = new MasProduct();

                ModelSer.ProductCode = txtCode.Text;
                ModelSer.ProductName = txtName.Text;
                lst = dal.GetSearchProduct(ModelSer);

                if (lst != null && lst.Count > 0)
                    gv.DataSource = lst;
                else
                    gv.DataSource = null;

                gv.DataBind();
            }
            catch (Exception ex)
            {
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                txtMCode.Text = "";
                txtMName.Text = "";
                ddlType.SelectedIndex = 0;
                ddlUnit.SelectedIndex = 0;
                chkSN.Checked = false;
                //txtMPurchasePrice.Text = "";
                //txtMPrice.Text = "";
                //txtMPrice.Text = "";
                //BindDDL();

                ModalPopupExtender1.Show();
                hddMode.Value = "Add";
                hddID.Value = "";
                txtMCode.Enabled = true;
            }
            catch (Exception ex)
            {
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }

        }

        private void BindDDL()
        {
            try
            {
                var bal = ItemDal.Instance;
                List<MasItemType> lstType = bal.GetSearchItemType();
                if (lstType != null && lstType.Count > 0)
                {
                    lstType = lstType.Where(w => w.Active.ToLower().Equals("y")).ToList();
                    ddlType.DataSource = lstType;
                    ddlType.DataTextField = "ItemTypeName";
                    ddlType.DataValueField = "ItemTypeID";
                    ddlType.DataBind();
                }
                else
                {
                    ddlType.DataSource = null;
                    ddlType.DataBind();
                }
                ddlType.Items.Insert(0, new ListItem("", "0"));

                List<MasUnit> lstUnit = bal.GetSearchUnit();
                if (lstUnit != null && lstUnit.Count > 0)
                {
                    lstUnit = lstUnit.Where(w => w.Active.ToLower().Equals("y")).ToList();
                    ddlUnit.DataSource = lstUnit;
                    ddlUnit.DataTextField = "UnitName";
                    ddlUnit.DataValueField = "UnitID";
                    ddlUnit.DataBind();
                }
                else
                {
                    ddlUnit.DataSource = null;
                    ddlUnit.DataBind();
                }
                ddlUnit.Items.Insert(0, new ListItem("", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnModalSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dal = ProductDal.Instance;

                if (txtMName.Text == "")
                {
                    ShowMessageBox("กรุณาระบุ สินค้า !!!");
                    ModalPopupExtender1.Show();
                    return;
                }

                //MasProduct ModelData = dal.GetSearchProductCode(txtMCode.Text.Trim());
                MasProduct ModelData = dal.ValidateProductData(txtMCode.Text.Trim(), txtMName.Text.Trim(), ToInt32(hddID.Value));

                if (ModelData.CHK_CODE_NAME == 1)
                {
                    ShowMessageBox("รหัสสินค้าและชื่อสินค้า ห้ามเหมือนกัน !!!");
                    ModalPopupExtender1.Show();
                    return; 
                }

                if (ModelData.CHK_PRODUCT_CODE == 1)
                {
                    ShowMessageBox("รหัสสินค้า นี้ซ้ำ !!!");
                    ModalPopupExtender1.Show();
                    return;
                }
                else if (ModelData.CHK_PRODUCT_CODE == 2)
                {
                    ShowMessageBox("รหัสสินค้านี้ซ้ำกับชื่อสินค้าอื่น !!!");
                    ModalPopupExtender1.Show();
                    return;
                }

                if (ModelData.CHK_PRODUCT_NAME == 1)
                {
                    ShowMessageBox("ชื่อสินค้า นี้ซ้ำ !!!");
                    ModalPopupExtender1.Show();
                    return;
                }
                else if (ModelData.CHK_PRODUCT_NAME == 2)
                {
                    ShowMessageBox("ชื่อสินค้านี้ซ้ำกับรหัสสินค้าอื่น !!!");
                    ModalPopupExtender1.Show();
                    return;
                }

                //if (ModelData.ProductCode != null && hddMode.Value == "Add")
                //{
                //    ShowMessageBox("รหัสสินค้า นี้ซ้ำ !!!");
                //    ModalPopupExtender1.Show();
                //    return;
                //}

                MasProduct o = new MasProduct();
                if (hddMode.Value == "Add") // Add
                {
                    o = new MasProduct();
                    o.ProductCode = txtMCode.Text;
                    o.ProductName = txtMName.Text;
                    o.UnitID = ToInt32(ddlUnit.SelectedItem.Value);
                    o.TypeID = ToInt32(ddlType.SelectedItem.Value);
                    o.Remaining = 0;
                    o.RemainingHeadQ = 0;
                    o.ProductSN = chkSN.Checked ? "Y" : "N";
                    //o.PurchasePrice = ToDoudle(txtMPurchasePrice.Text);
                    //o.SellPrice = ToDoudle(txtMPrice.Text);
                    o.Active = "Y";
                    o.CreatedBy = GetUsername();
                    o.CreatedDate = DateTime.Now;
                    o.DMLFlag = "I".ToUpper();


                    dal.InsUpdDelMasProduct(o);
                }
                else //Edit
                {
                    o = new MasProduct();
                    o.ProductID = ToInt32(hddID.Value);
                    o.ProductCode = txtMCode.Text;
                    o.ProductName = txtMName.Text;
                    o.UnitID = ToInt32(ddlUnit.SelectedItem.Value);
                    o.TypeID = ToInt32(ddlType.SelectedItem.Value);
                    o.Remaining = 0;
                    o.RemainingHeadQ = 0;
                    o.ProductSN = chkSN.Checked ? "Y" : "N";
                    //o.PurchasePrice = ToDoudle(txtMPurchasePrice.Text);
                    //o.SellPrice = ToDoudle(txtMPrice.Text);
                    o.Active = "Y";
                    o.CreatedBy = GetUsername();
                    o.CreatedDate = DateTime.Now;
                    o.DMLFlag = "U".ToUpper();

                    dal.InsUpdDelMasProduct(o);

                }

                txtMCode.Enabled = true;
                BindData();
                ShowMessageBox("บันทึกข้อมูลสำเร็จ.");
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            var dal = ProductDal.Instance;

            try
            {
                ImageButton imb = (ImageButton)sender;

                if (imb != null)
                {
                    Int32 obj = ToInt32(imb.CommandArgument);
                    //BindDDL();

                    MasProduct ModelData = dal.GetSearchProductID(obj);
                    if (ModelData != null)
                    {
                        hddID.Value = imb.CommandArgument;
                        txtMCode.Text = ModelData.ProductCode;
                        txtMName.Text = ModelData.ProductName;
                        ddlUnit.SelectedIndex = ToInt32(ddlUnit.Items.FindByValue(ModelData.UnitID.ToString()).Value);
                        ddlType.SelectedIndex = ToInt32(ddlType.Items.FindByValue(ModelData.TypeID.ToString()).Value);
                        //ddlUnit.Items.FindByValue(ModelData.UnitID.ToString()).Selected = true;
                        //ddlType.Items.FindByValue(ModelData.TypeID.ToString()).Selected = true;
                        chkSN.Checked = ModelData.ProductSN == "Y" ? true : false;
                        //ddlUnit.SelectedIndex = ddlUnit.Items.FindByValue().Selected;
                        //txtMPurchasePrice.Text = ModelData.PurchasePrice.ToString("###,##0");
                        //txtMPrice.Text = ModelData.SellPrice.ToString("###,##0");
                        ModalPopupExtender1.Show();
                        hddMode.Value = "Edit";
                        txtMCode.Enabled = false;
                    }
                    else
                    {
                        SendMailError("obj is null, objID = " + imb.CommandArgument, System.Reflection.MethodBase.GetCurrentMethod());
                    }
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

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var dal = ProductDal.Instance;
                ImageButton imb = (ImageButton)sender;
                if (imb != null)
                {
                    MasProduct ModelData = new MasProduct();
                    string objCode = imb.CommandArgument;
                    ModelData.ProductCode = objCode;
                    ModelData.ProductID = Convert.ToInt32(objCode);
                    ModelData.Active = "N";
                    ModelData.CreatedBy = GetUsername();
                    ModelData.DMLFlag = "D";
                    dal.InsUpdDelMasProduct(ModelData);

                    BindData();
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
    }
}