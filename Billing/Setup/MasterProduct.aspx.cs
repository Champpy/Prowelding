using Billing.AppData;
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
                //txtMPurchasePrice.Text = "";
                //txtMPrice.Text = "";
                //txtMPrice.Text = "";

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

                MasProduct ModelData = dal.GetSearchProductCode(txtMCode.Text.Trim());

                if (ModelData.ProductCode != null && hddMode.Value == "Add")
                {
                    ShowMessageBox("รหัสสินค้า นี้ซ้ำ !!!");
                    ModalPopupExtender1.Show();
                    return;
                }

                MasProduct o = new MasProduct();
                if (hddMode.Value == "Add") // Add
                {
                    o = new MasProduct();
                    o.ProductCode = txtMCode.Text;
                    o.ProductName = txtMName.Text;
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

                    MasProduct ModelData = dal.GetSearchProductID(obj);

                    if (ModelData != null)
                    {
                        hddID.Value = imb.CommandArgument;
                        txtMCode.Text = ModelData.ProductCode;
                        txtMName.Text = ModelData.ProductName;
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