using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.DTO;
using DAL;
using Entities;

namespace Billing.Stock
{
    public partial class ManageStockHeadQ : Billing.Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = "";
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                hddType.Value = Request.QueryString["t"].ToString();
                switch (hddType.Value)
                {
                    case "i":
                        type = "รับเข้า";
                        break;
                    case "o":
                        type = "HeadQ ขายสินค้า";
                        break;
                    case "t":
                        type = "ย้ายเข้าคลังขาย";
                        break;
                    default:
                        break;
                }
                lblHeader.Text = "คลังใหญ่ : " + type; // (hddType.Value.ToLower() == "i" ? "รับเข้า" : "");
                Session["StockDetailHeadQ"] = null;
                Session["StockDetailHeadQSerial"] = null;
                Session["StockProductHeadQList"] = null;
                Session["StockDetailHeadQProduct"] = null;
                BindDataDetail();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string err = "", type = "", result = "", url = "../Stock/StockLogs.aspx";
                List<StockDetail> lst = new List<StockDetail>();
                StockHeader Header = new StockHeader();
                var dal = StockDal.Instance;
                if (Session["StockDetailHeadQ"] != null)
                {
                    lst = (List<StockDetail>)Session["StockDetailHeadQ"];
                    if(lst == null && lst.Count == 0 )
                    {
                        ShowMessageBox("กรุณาเพิ่ม สินค้า ที่ต้องการบันทึก!!");
                        return;
                    }

                    switch (hddType.Value)
                    {
                        case "i":
                            type = "IN";
                            url = "../Stock/StockHeadQLogs.aspx";
                            break;
                        case "o":
                            type = "OUT";
                            url = "../Stock/StockHeadQLogs.aspx";
                            break;
                        case "t":
                            type = "TRANSFER";
                            url = "../Stock/StockLogs.aspx";
                            break;
                        default:
                            type = "";
                            break;
                    }

                    //Process Save 
                    Header.StockType = type;// hddType.Value.ToLower() == "i" ? "IN" : "OUT";
                    Header.StockTime = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                    Header.Remark = txtRemark.Text;
                    Header.detail = lst;
                    err = dal.InsertStockHeadQ(Header, GetUsername(), ref result);
                }
                
                if (!string.IsNullOrEmpty(err))
                {
                    ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.!!" + err);
                    return;
                }

                if (!string.IsNullOrEmpty(result))
                {
                    ShowMessageBox(result);
                    return;
                }

                ShowMessageBox("บันทึกสำเร็จ.", this.Page, url);
            }
            catch (Exception ex)
            {
                ShowMessageBox("เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ.");
                SendMailError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        protected void btnAddModal_Click(object sender, EventArgs e)
        {
            try
            {
                ClearTextSearchItem();
                ModalPopupExtender3.Show();
                SearchItem();
            }
            catch (Exception ex)
            {

            }
        }

        #region Event Detail
        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;
                if (imb != null)
                {
                    int objID = ToInt32(imb.CommandArgument);
                    List<StockDetail> lst = new List<StockDetail>();
                    if (Session["StockDetailHeadQ"] != null)
                    {
                        lst = (List<StockDetail>)Session["StockDetailHeadQ"];
                        if (lst != null && lst.Count > 0)
                        {
                            StockDetail obj = lst.FirstOrDefault(w => w.ProductID.Equals(objID));
                            if (obj != null)
                                lst.Remove(obj);
                            Session["InBoundDetail"] = lst;
                        }
                    }

                    BindDataDetail();
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
        
        protected void BindDataDetail()
        {
            try
            {
                List<StockDetail> lst = new List<StockDetail>();
                if (Session["StockDetailHeadQ"] != null)
                {
                    lst = (List<StockDetail>)Session["StockDetailHeadQ"];
                }

                if (lst == null || lst.Count() == 0)
                {
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }

                gvItem.DataSource = lst;
                gvItem.DataBind();
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
                string ProductCode = txtSearchItemCode.Text.ToLower();
                string ProductName = txtSearchItemName.Text.ToLower();
                MasPackageHeader o = new MasPackageHeader() { PackageCode = ProductCode, PackageName = ProductName };
                List<MasPackageHeader> lst = new List<MasPackageHeader>();
                var dal = PackageDal.Instance; // ItemDal.Instance;
                lst = dal.GetSearchPackageHeader(o);
                if (lst != null)
                {
                    //if (!string.IsNullOrEmpty(ProductCode))
                    //    lst = lst.Where(w => w.ProductCode.ToLower().Contains(ProductCode)).ToList();

                    //if (!string.IsNullOrEmpty(ProductName))
                    //{
                    //    lst = lst.Where(w => w.ProductName.ToLower().Contains(ProductName)).ToList();
                    //}

                    lst = lst.OrderBy(od => od.PackageName).ToList();
                    Session["StockProductHeadQList"] = lst;
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

        protected void imgbtnChooseItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;
                HiddenField hdd = new HiddenField();
                if (imb != null)
                {
                    hdd = (HiddenField)Panel5.FindControl("hddm5Index");
                    if (hdd != null)
                    {
                        txtm5Amount.Text = "";
                        hdd.Value = imb.CommandName;
                        ModalPopupExtender3.Show();
                        ModalPopupExtender5.Show();
                    } 
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnModalClose3_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender1.Show();
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

        #region Modal 5 Amount
        protected void btnm5OK_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 Amt = ToInt32(txtm5Amount.Text);
                Int32 ProductID = ToInt32(hddm5Index.Value);

                if (Session["StockProductHeadQList"] != null)
                {
                    List<MasPackageHeader> lstProduct = (List<MasPackageHeader>)Session["StockProductHeadQList"];
                    if (lstProduct != null && lstProduct.Count > 0)
                    {
                        MasPackageHeader o = lstProduct.FirstOrDefault(w => w.PackageHeaderID.Equals(ProductID));
                        if (o != null)
                        {
                            //Get Package Detail
                            var bal = PackageDal.Instance;
                            Int32 pSN = 0;
                            List<MasProduct> lstDet = bal.GetSearchPackageDetail(o);
                            if(lstDet != null && lstDet.Count > 0)
                            {
                                Session["StockDetailHeadQProduct"] = lstDet;
                                pSN = lstDet.Count(w => w.ProductSN.ToLower().Equals("y"));
                                if (pSN > 0 && hddType.Value.ToLower() == "t")
                                {
                                    //List<StockDetail> lst = new List<StockDetail>();
                                    //if (Session["StockDetailHeadQ"] != null)
                                    //    lst = (List<StockDetail>)Session["StockDetailHeadQ"];

                                    //StockDetail s = lst.FirstOrDefault(w => w.ProductID.Equals(ProductID));
                                    //if (s != null)
                                    //{
                                    //    s.lstSerial = new List<TransProductSerial>();
                                    //    s.Amount = Amt;
                                    //}

                                    ClearModal6();

                                    lbM6Header.Text = o.PackageName + " (" + Amt.ToString() + ")";
                                    hddM6ProductID.Value = hddm5Index.Value;

                                    ModalPopupExtender6.Show();
                                }
                                else
                                {
                                    #region Comment
                                    //List<StockDetail> lst = new List<StockDetail>();
                                    //if (Session["StockDetailHeadQ"] != null)
                                    //    lst = (List<StockDetail>)Session["StockDetailHeadQ"];

                                    //StockDetail s = lst.FirstOrDefault(w => w.ProductID.Equals(ProductID));
                                    //if (s == null)
                                    //{
                                    //    lst.Add(new StockDetail()
                                    //    {
                                    //        ProductID = ProductID,
                                    //        ProductCode = o.ProductCode,
                                    //        ProductName = o.ProductName,
                                    //        ProductTypeID = o.TypeID,
                                    //        ProductTypeName = o.TypeName,
                                    //        Amount = Amt,
                                    //    });
                                    //}
                                    //else // Found In List
                                    //{
                                    //    s.Amount = s.Amount + Amt;
                                    //}
                                    #endregion

                                    AddDetailFromSession(Amt);
                                }
                            }
                            else
                            {
                                Session["StockDetailHeadQProduct"] = null;
                                ShowMessageBox("Detail Not found. !!");
                            }
                        }
                    }
                }
                else
                {
                    ShowMessageBox("Session Timeout. !!", this, "../Index.aspx");
                }

                #region Comment
                //List<StockDetail> lst = new List<StockDetail>();
                //if(Session["StockDetailHeadQ"] != null)
                //    lst = (List<StockDetail>)Session["StockDetailHeadQ"];

                //Int32 Amt = ToInt32(txtm5Amount.Text);
                //Int32 ProductID = ToInt32(hddm5Index.Value);

                //if(Session["StockProductHeadQList"] != null)
                //{
                //    List<MasProduct> lstProduct = (List<MasProduct>)Session["StockProductHeadQList"];
                //    if(lstProduct != null && lstProduct.Count > 0)
                //    {
                //        MasProduct o = lstProduct.FirstOrDefault(w => w.ProductID.Equals(ProductID));
                //        if(o != null)
                //        {
                //            StockDetail s = lst.FirstOrDefault(w => w.ProductID.Equals(ProductID));
                //            if(s == null)
                //            {
                //                lst.Add(new StockDetail()
                //                {
                //                    ProductID = ProductID,
                //                    ProductCode = o.ProductCode,
                //                    ProductName = o.ProductName,
                //                    Amount = Amt,
                //                });
                //            }
                //            else // Found In List
                //            {
                //                s.Amount = s.Amount + Amt;
                //            }

                //            Session["StockDetailHeadQ"] = lst;
                //            BindDataDetail();
                //        }
                //    }
                //}
                //else
                //{
                //    //ShowMessageBox("Session Timeout. !!", this, "../Index.aspx");
                //}
                #endregion
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnm5Cancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender3.Show();
        }

        #endregion

        #region Modal6 S/N
        protected void btnM6Add_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate
                if (string.IsNullOrEmpty(txtM6SN.Text))
                {
                    ShowMessageBox("Add Serial Number First. !!!");
                    return;
                }

                Int32 Amt = ToInt32(txtm5Amount.Text);
                TransProductSerial tps = new TransProductSerial();
                List<TransProductSerial> lst = new List<TransProductSerial>();
                if (Session["StockDetailHeadQSerial"] != null)
                    lst = (List<TransProductSerial>)Session["StockDetailHeadQSerial"];

                #region Validate
                //Check Amount
                if (Amt <= lst.Count)
                {
                    ShowMessageBox("Serial Number Too much.");
                    ModalPopupExtender6.Show();
                    return;
                }

                //Check S/N Duplicate
                tps = lst.FirstOrDefault(w => w.SerialNumber.Trim().Equals(txtM6SN.Text.Trim()));
                if (tps != null)
                {
                    ShowMessageBox("Serial Number is duplicate.");
                    ModalPopupExtender6.Show();
                    return;
                }
                #endregion

                //Pass All Validate
                tps = new TransProductSerial();
                tps.SerialNumber = txtM6SN.Text;
                lst.Add(tps);
                BindGridSerial(lst);

                #region Comment
                //List<StockDetail> lst = new List<StockDetail>();
                //List<TransProductSerial> lstTPS = new List<TransProductSerial>();
                //TransProductSerial tps = new TransProductSerial();
                //if (Session["StockDetailHeadQ"] != null)
                //{
                //    lst = (List<StockDetail>)Session["StockDetailHeadQ"];
                //    StockDetail o = lst.FirstOrDefault(w => w.ProductID.Equals(productID));
                //    if (o != null)
                //    {
                //        if (o.lstSerial == null)
                //        {
                //            o.lstSerial = new List<TransProductSerial>();
                //        }
                //        else
                //        {
                //            if (o.Amount <= o.lstSerial.Count)
                //            {
                //                ShowMessageBox("Serial Number Too much.");
                //                ModalPopupExtender6.Show();
                //                return;
                //            }
                //        }

                //        //Check S/N Duplicate
                //        tps = o.lstSerial.FirstOrDefault(w => w.SerialNumber.Trim().Equals(txtM6SN.Text.Trim()));
                //        if (tps != null)
                //        {
                //            ShowMessageBox("Serial Number is duplicate.");
                //            ModalPopupExtender6.Show();
                //            return;
                //        }

                //        tps = new TransProductSerial();
                //        tps.ProductID = productID;
                //        tps.SerialNumber = txtM6SN.Text.Trim();
                //        o.lstSerial.Add(tps);

                //        //Binding
                //        BindGridSerial(o.lstSerial);
                //    }
                //    else
                //    {
                //        if (Session["StockProductHeadQList"] != null)
                //        {
                //            List<MasProduct> lstProduct = (List<MasProduct>)Session["StockProductHeadQList"];
                //            if (lstProduct != null && lstProduct.Count > 0)
                //            {
                //                MasProduct pd = lstProduct.FirstOrDefault(w => w.ProductID.Equals(productID));
                //                if (pd != null)
                //                {
                //                    lstTPS = new List<TransProductSerial>();

                //                    tps = new TransProductSerial();
                //                    tps.ProductID = productID;
                //                    tps.SerialNumber = txtM6SN.Text.Trim();
                //                    lstTPS.Add(tps);

                //                    lst.Add(new StockDetail()
                //                    {
                //                        ProductID = productID,
                //                        ProductCode = pd.ProductCode,
                //                        ProductName = pd.ProductName,
                //                        ProductTypeID = pd.TypeID,
                //                        ProductTypeName = pd.TypeName,
                //                        Amount = Amt,
                //                        lstSerial = lstTPS,
                //                    });

                //                    //Binding
                //                    BindGridSerial(lstTPS);
                //                }
                //            }
                //        }

                //    }
                //}
                //else
                //{
                //    if (Session["StockProductHeadQList"] != null)
                //    {
                //        List<MasProduct> lstProduct = (List<MasProduct>)Session["StockProductHeadQList"];
                //        if (lstProduct != null && lstProduct.Count > 0)
                //        {
                //            MasProduct pd = lstProduct.FirstOrDefault(w => w.ProductID.Equals(productID));
                //            if (pd != null)
                //            {
                //                lstTPS = new List<TransProductSerial>();
                //                tps = new TransProductSerial();
                //                tps.ProductID = productID;
                //                tps.SerialNumber = txtM6SN.Text.Trim();
                //                lstTPS.Add(tps);

                //                //Binding
                //                BindGridSerial(lstTPS);

                //                lst.Add(new StockDetail()
                //                {
                //                    ProductID = productID,
                //                    ProductCode = pd.ProductCode,
                //                    ProductName = pd.ProductName,
                //                    ProductTypeID = pd.TypeID,
                //                    ProductTypeName = pd.TypeName,
                //                    Amount = Amt,
                //                    lstSerial = lstTPS,
                //                });
                //            }
                //        }
                //    }
                //}
                #endregion

                //Clear Txt
                txtM6SN.Text = "";

                //Save Session
                Session["StockDetailHeadQSerial"] = lst;

                ModalPopupExtender6.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnM6Save_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 Amt = ToInt32(txtm5Amount.Text);
                AddDetailFromSession(Amt);

                //BindDataDetail();
                //ModalPopupExtender6.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnM6Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender5.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void imgbtnDeleteSN_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;
                List<StockDetail> lst = new List<StockDetail>();
                if (Session["StockDetailHeadQ"] != null && imb != null)
                {
                    lst = (List<StockDetail>)Session["StockDetailHeadQ"];
                    int productID = ToInt32(hddM6ProductID.Value);
                    StockDetail o = lst.FirstOrDefault(w => w.ProductID.Equals(productID));
                    if (o != null)
                    {
                        TransProductSerial tps = o.lstSerial.FirstOrDefault(w => w.SerialNumber.Equals(imb.CommandName));
                        if (tps != null)
                            o.lstSerial.Remove(tps);

                        //Binding
                        BindGridSerial(o.lstSerial);

                        //Save Session
                        Session["StockDetailHeadQ"] = lst;
                    }
                }
                else
                {
                    ShowMessageBox("Session Timeout. !!", this, "../Index.aspx");
                }


                ModalPopupExtender6.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void BindGridSerial(List<TransProductSerial> lst)
        {
            try
            {
                if (lst != null)
                {
                    gvSerial.DataSource = lst;
                    gvSerial.DataBind();
                }
                else
                {
                    gvSerial.DataSource = null;
                    gvSerial.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ClearModal6()
        {
            try
            {
                hddM6ProductID.Value = "";
                txtM6SN.Text = "";
                BindGridSerial(new List<TransProductSerial>());
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        private void AddDetailFromSession(Int32 Amt)
        {
            try
            {
                if(Session["StockDetailHeadQProduct"] != null)
                {
                    List<MasProduct> lstDet = (List<MasProduct>)Session["StockDetailHeadQProduct"];

                    if(lstDet != null && lstDet.Count > 0)
                    {
                        List<TransProductSerial> lstTPS = new List<TransProductSerial>();
                        StockDetail s = new StockDetail();
                        List<StockDetail> lst = new List<StockDetail>();
                        if (Session["StockDetailHeadQ"] != null)
                            lst = (List<StockDetail>)Session["StockDetailHeadQ"];

                        foreach (MasProduct item in lstDet)
                        {
                            s = lst.FirstOrDefault(w => w.ProductID.Equals(item.ProductID));
                            if (s == null)
                            {
                                s = new StockDetail();
                                s.ProductID = item.ProductID;
                                s.ProductCode = item.ProductCode;
                                s.ProductName = item.ProductName;
                                s.ProductTypeID = item.TypeID;
                                s.ProductTypeName = item.TypeName;
                                s.Amount = item.Amount * Amt;
                                if(item.ProductSN.ToLower() == "y" && Session["StockDetailHeadQSerial"] != null)
                                {
                                    lstTPS = (List<TransProductSerial>)Session["StockDetailHeadQSerial"];
                                    foreach (TransProductSerial tps in lstTPS)
                                    {
                                        tps.ProductID = item.ProductID;
                                    }
                                    s.lstSerial = lstTPS;
                                }

                                lst.Add(s);
                            }
                            else // Found In List
                            {
                                s.Amount = s.Amount + (item.Amount * Amt);
                            }
                        }

                        Session["StockDetailHeadQ"] = lst;
                        BindDataDetail();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Stock/ManageStockHeadQ.aspx?t=" + hddType.Value);
        }
    }
}