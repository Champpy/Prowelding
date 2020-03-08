using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Billing.Stock
{
    public partial class StockTransProductSerial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                BindData();
            }
        }

        protected void BindData()
        {
            try
            {              
                List<TransProductSerial> lst = new List<TransProductSerial>();
               
                var bal = StockDal.Instance;
                lst = bal.GetSearchTransProductSerial(txtProductName.Text, txtSerialNumber.Text);

                if (lst != null && lst.Count > 0)
                {
                    string Status = chkStatus.Checked ? "Y" : "N";
                    lst = lst.Where(w => w.Status.Equals(Status)).ToList();
                    gv.DataSource = lst;
                }
                else
                {
                    gv.DataSource = null;
                }

                gv.DataBind();
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

            }
        }
    }
}