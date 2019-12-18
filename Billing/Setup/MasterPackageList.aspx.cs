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
    public partial class MasterPackageList : Billing.Common.BasePage
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
            var dal = PackageDal.Instance;
            try
            {
                List<MasPackageHeader> lst = new List<MasPackageHeader>();
                MasPackageHeader ModelSer = new MasPackageHeader();
                ModelSer.PackageCode = txtCode.Text;
                ModelSer.PackageName = txtName.Text;

                lst = dal.GetSearchPackageHeader(ModelSer);

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

        protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;

                if (imb != null)
                {
                    string objCode = imb.CommandArgument;

                    if (objCode != null)
                    {

                        Response.Redirect("MasterPackage.aspx?PackageCode=" + objCode);

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
                var dal = PackageDal.Instance;

                ImageButton imb = (ImageButton)sender;
                MasPackageHeader DataHeader = new MasPackageHeader();
                if (imb != null)
                {
                    Int32 obj = ToInt32(imb.CommandArgument);

                    DataHeader.DMLFlag = "D";
                    DataHeader.PackageHeaderID = obj;
                    DataHeader.CreatedBy = GetUsername();

                    dal.InsUpdDelMasPackageHeader(DataHeader);
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