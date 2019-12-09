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
                var dal = PackageDal.Instance;
                List<MasProduct> lst = new List<MasProduct>();
                string PackageCode = Request.QueryString.Get("PackageCode");

                if (PackageCode != null)
                {
                    MasPackageHeader Modeldata = dal.GetSearchMasPackageHeaderByID(PackageCode);
                    txtPackageCode.Text = Modeldata.PackageCode;
                    txtPackageName.Text = Modeldata.PackageName;
                    txtPackageSellPrice.Text = Convert.ToDouble(Modeldata.SellPrice).ToString("#,###0.00");
                    hddID.Value = Modeldata.PackageHeaderID.ToString();
                    hddHeaderMode.Value = "Edit";
                    Modeldata = new MasPackageHeader();
                    Modeldata.PackageHeaderID = Convert.ToInt32(hddID.Value);
                    lst = dal.GetSearchPackageDetail(Modeldata);
                }

                Session["DetailProdcut"] = lst;
                BindDataGrid();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            var dal = PackageDal.Instance;

            try
            {


                if (txtPackageCode.Text == "")
                {
                    ShowMessageBox("กรุณาระบุ รหัส Packag !!!");
                    return;
                }
                if (txtPackageName.Text == "")
                {
                    ShowMessageBox("กรุณาระบุ ชื่อ Package  !!!");
                    return;
                }
                if (txtPackageSellPrice.Text == "")
                {
                    ShowMessageBox("กรุณาระบุ ราคาขาย !!!");
                    return;
                }

                MasPackageHeader DataHeader = new MasPackageHeader();

                if (hddHeaderMode.Value == "Edit")
                {
                    DataHeader.PackageHeaderID = Convert.ToInt32(hddID.Value);
                }

                DataHeader.PackageCode = txtPackageCode.Text;
                DataHeader.PackageName = txtPackageName.Text;
                DataHeader.SellPrice = Convert.ToDouble(txtPackageSellPrice.Text);
                DataHeader.DMLFlag = hddHeaderMode.Value != "Edit" ? "I" : "U";
                DataHeader.Active = "Y";
                DataHeader.CreatedBy = GetUsername();
                string ResultHearder = dal.InsUpdDelMasPackageHeader(DataHeader);
                List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];
                if (ResultHearder == "")
                {
                    dal.InsUpdDelMasPackageDetail(lst, txtPackageCode.Text, "D");

                    string ResultDetail = dal.InsUpdDelMasPackageDetail(lst, txtPackageCode.Text);

                    if (ResultDetail == "")
                    {
                        ShowMessageBox("บันทึกข้อมูลสำเร็จ.", this.Page, "MasterPackageList.aspx");
                    }
                    else
                    {
                        ShowMessageBox(ResultDetail);
                    }
                }
            }

            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }

        }


        #region Detail
        protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
        {

            List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];
            ImageButton imb = (ImageButton)sender;

            if (imb != null)
            {
                Int32 id = ToInt32(imb.CommandArgument);
                MasProduct Data = lst.FirstOrDefault(x => x.ProductID == id);

                hddProductID.Value = Data.ProductID.ToString();
                txtProductCode.Text = Data.ProductCode;
                txtProductName.Text = Data.ProductName;
                //txtProductSellPrice.Text = Data.SellPrice.ToString("#,###0.00");
                txtProductAmount.Text = Data.Amount.ToString();
                ChkCanChange.Checked = Data.CanChange == "Change" ? true : false;

                //ModelDataAdd.CanChange = ChkCanChange.Checked == true ? "Change" : "Fix";
                hddProductMode.Value = "Edit";
            }

            ModalPopupExtender4.Show();
        }
        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];
            ImageButton imb = (ImageButton)sender;

            if (imb != null)
            {
                Int32 id = ToInt32(imb.CommandArgument);
                lst.RemoveAll(x => x.ProductID == id);

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
                //int rowIndex = Convert.ToInt32(imb.CommandArgument);
                int objCode = ToInt32(imb.CommandArgument);
                HiddenField hdd = (HiddenField)gvItemSearch.Rows[objCode].FindControl("hddItemID");
                if(hdd != null)
                {
                    int pid = ToInt32(hdd.Value);
                    MasProduct ModelData = new MasProduct();
                    ModelData = dal.GetSearchProductID(pid);
                    if (ModelData.ProductName != null)
                    {
                        hddProductID.Value = ModelData.ProductID.ToString();
                        txtProductCode.Text = ModelData.ProductCode;
                        txtProductName.Text = ModelData.ProductName;
                        //txtProductSellPrice.Text = ModelData.SellPrice.ToString("#,###0.00");
                    }
                }
            }

            ModalPopupExtender4.Show();
        }

        protected void imgbtnSearchProduct_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender3.Show();
            BindDataProduct();
        }

        protected void btnAddModal_Click(object sender, EventArgs e)
        {
            ModalPopupExtender4.Show();
        }

        protected void BtnSaveProductDetail_Click(object sender, EventArgs e)
        {
            List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];

            bool IsCheckData = true;

            if (hddProductID.Value == "")
            {
                ShowMessageBox("กรุณาระบุ สินค้า !!!");
                ModalPopupExtender4.Show();
                return;
            }
            if (txtProductAmount.Text == "")
            {
                ShowMessageBox("กรุณาระบุ จำนวน !!!");
                ModalPopupExtender4.Show();
                return;
            }

            if (hddProductMode.Value == "Add")
            {
                int pid = ToInt32(hddProductID.Value);
                if (lst.Where(x => x.ProductID == pid).ToList().Count > 0)
                {
                    ShowMessageBox("รหัสสินค้านี้ มีอยู่ใน list แล้ว!!!");
                    ModalPopupExtender4.Show();
                    return;
                }
            }

            if (IsCheckData)
            {
                SaveProductDetail();
            }
            else
            {
                ModalPopupExtender4.Show();
            }
        }

        protected void BtnCloseProductDetail_Click(object sender, EventArgs e)
        {
            CleartxtDetail();
        }

        private void SaveProductDetail()
        {
            List<MasProduct> lst = (List<MasProduct>)Session["DetailProdcut"];
            MasProduct ModelDataAdd = new MasProduct();

            //if (hddProductMode.Value == "Add")
            //{
            //    if (lst.Where(x => x.ProductCode == txtProductCode.Text).ToList().Count > 0)
            //    {
            //        ShowMessageBox("รหัสสินค้านี้ มีอยู่ใน list แล้ว!!!");
            //        ModalPopupExtender4.Show();
            //        return;
            //    }
            //}

            if (hddProductMode.Value != "Add")
            {
                Int32 id = ToInt32(hddProductID.Value);
                ModelDataAdd = lst.FirstOrDefault(x => x.ProductID == id);
            }

            if (ModelDataAdd == null)
                ModelDataAdd = new MasProduct();

            ModelDataAdd.ProductID = Convert.ToInt32(hddProductID.Value);
            ModelDataAdd.ProductCode = txtProductCode.Text;
            ModelDataAdd.ProductName = txtProductName.Text;
            //ModelDataAdd.SellPrice = Convert.ToDouble(txtProductSellPrice.Text);
            ModelDataAdd.Amount = Convert.ToInt32(txtProductAmount.Text);
            ModelDataAdd.CanChange = ChkCanChange.Checked == true ? "Change" : "Fix";
            ModelDataAdd.DMLFlag = "I";
            ModelDataAdd.Active = "Y";
            ModelDataAdd.CreatedBy = GetUsername();
            //chkCOD.Checked = string.IsNullOrEmpty(obj.COD) ? false : obj.COD.Equals("0") ? false : true;

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
            //txtProductSellPrice.Text = "";
            txtProductAmount.Text = "";
            hddProductID.Value = "";
            hddProductMode.Value = "Add";
            ChkCanChange.Checked = false;
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
            ModalPopupExtender4.Show();
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