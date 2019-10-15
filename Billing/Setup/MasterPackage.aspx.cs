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
                List<MasProduct> lst = new List<MasProduct>();
                string ID = Request.QueryString.Get("ID");
                hddID.Value = ID;
                Session["DetailProdcut"] = lst;
                BindDataGrid();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

        }


        #region Detail
        protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
        {

            List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];
            ImageButton imb = (ImageButton)sender;

            if (imb != null)
            {

                MasProduct Data = lst.FirstOrDefault(x => x.ProductCode == imb.CommandArgument);

                hddProductID.Value = Data.ProductID.ToString();
                txtProductCode.Text = Data.ProductCode;
                txtProductName.Text = Data.ProductName;
                txtProductSellPrice.Text = Data.SellPrice.ToString("#,###0.00");
                txtProductAmount.Text = Data.Amount.ToString();
                hddProductMode.Value = "Edit";

                //CleartxtDetail();

                //BindDataGrid();
            }
        }
        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];
            ImageButton imb = (ImageButton)sender;

            if (imb != null)
            {

                lst.RemoveAll(x => x.ProductCode == imb.CommandArgument);

                CleartxtDetail();
                BindDataGrid();
            }
        }
        protected void imgbtnChooseItem_Click(object sender, ImageClickEventArgs e)
        {

            var dal = ProductDal.Instance;

            ImageButton imb = (ImageButton)sender;
            if (imb != null)
            {
                int objCode = Convert.ToInt32(imb.CommandArgument);

                MasProduct ModelData = new MasProduct();


                ModelData = dal.GetSearchProductCode(gvItemSearch.Rows[objCode].Cells[0].Text);

                if (ModelData.ProductName != null)
                {
                    hddProductID.Value = ModelData.ProductID.ToString();
                    txtProductCode.Text = ModelData.ProductCode;
                    txtProductName.Text = ModelData.ProductName;
                    txtProductSellPrice.Text = ModelData.SellPrice.ToString("#,###0.00");
                }
            }
        }

        protected void imgbtnSearchProduct_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender3.Show();
            BindDataProduct();
        }

        protected void btnAddModal_Click(object sender, EventArgs e)
        {
            List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];
            MasProduct ModelDataAdd = new MasProduct();

            if (hddProductID.Value == "")
            {
                ShowMessageBox("กรุณาระบุ สินค้า !!!");
                return;
            }
            if (txtProductAmount.Text == "")
            {
                ShowMessageBox("กรุณาระบุ จำนวน !!!");
                return;
            }

            if (hddProductMode.Value == "Add")
            {
                if (lst.Where(x => x.ProductCode == txtProductCode.Text).ToList().Count > 0)
                {
                    ShowMessageBox("รหัสสินค้านี้ มีอยู่ใน list แล้ว!!!");
                    return;
                }
            }

            if (hddProductMode.Value != "Add")
            {
                ModelDataAdd = lst.FirstOrDefault(x => x.ProductCode == txtProductCode.Text);
            }

            ModelDataAdd.ProductID = Convert.ToInt32(hddProductID.Value);
            ModelDataAdd.ProductCode = txtProductCode.Text;
            ModelDataAdd.ProductName = txtProductName.Text;
            ModelDataAdd.SellPrice = Convert.ToDouble(txtProductSellPrice.Text);
            ModelDataAdd.Amount = Convert.ToInt32(txtProductAmount.Text);

            if (hddProductMode.Value == "Add")
            {
                lst.Add(ModelDataAdd);
            }
            CleartxtDetail();

            BindDataGrid();
        }

        protected void BindDataProduct()
        {
            try
            {
                var dal = ProductDal.Instance;
                List<MasProduct> lst = new List<MasProduct>();

                MasProduct ModelSer = new MasProduct();

                ModelSer.ProductCode = txtSearchProductCode.Text;
                ModelSer.ProductName = txtSearchProductName.Text;
                lst = dal.GetSearchProduct(ModelSer);

                if (lst != null && lst.Count > 0)
                    gvItemSearch.DataSource = lst;
                else
                    gvItemSearch.DataSource = null;

                gvItemSearch.DataBind();
            }
            catch (Exception ex)
            {
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        public void CleartxtDetail()
        {
            txtProductCode.Text = "";
            txtProductName.Text = "";
            txtProductSellPrice.Text = "";
            txtProductAmount.Text = "";
            hddProductID.Value = "";
            hddProductMode.Value = "Add";
        }

        protected void btnMItemSearch_Click(object sender, EventArgs e)
        {
            ModalPopupExtender3.Show();
            BindDataProduct();
        }

        protected void btnModalClose3_Click(object sender, EventArgs e)
        {
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
        }

        #endregion      
        
        protected void BindDataGrid()
        {
            List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];
            try
            {
                gvProdcutDetail.DataSource = lst;
                gvProdcutDetail.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

    }
}