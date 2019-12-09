using Billing.AppData;
using Billing.Model;
using DAL;
using Entities;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Billing.Stock
{
    public partial class StockHeadQLogs : Billing.Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDateFrom.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                BindData();
            }
        }

        #region BindData
        protected void BindData()
        {
            try
            {
                if(string.IsNullOrEmpty(txtDateFrom.Text) || string.IsNullOrEmpty(txtDateTo.Text))
                {
                    ShowMessageBox("กรุณาระบุวันที่ที่ต้องการค้นหา. !!!");
                    return;
                }
                List<StockHeader> lst = new List<StockHeader>();
                DateTime dateFrom = string.IsNullOrEmpty(txtDateFrom.Text) ? DateTime.MinValue : DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                DateTime dateTo = string.IsNullOrEmpty(txtDateTo.Text) ? DateTime.MaxValue : DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")).AddDays(1);
                var bal = StockDal.Instance;
                lst = bal.GetStockHeaderHeadQ(dateFrom, dateTo);              

                if (lst != null && lst.Count > 0)
                {
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
        #endregion


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
        protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;
                if(imb != null)
                {
                    Int32 HeaderID = ToInt32(imb.CommandArgument);
                    ModalPopupExtender1.Show();
                    List<StockDetailDTO> lst = new List<StockDetailDTO>();
                    var bal = StockDal.Instance;
                    lst = bal.GetStockDetailHeadQ(HeaderID);
                    if(lst != null && lst.Count > 0)
                    {
                        gvItemStock.DataSource = lst;
                    }
                    else
                    {
                        gvItemStock.DataSource = null;
                    }
                    
                    gvItemStock.DataBind();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnModalClose1_Click(object sender, EventArgs e)
        {

        }
    }
}