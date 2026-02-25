using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSBusinessLayer.Reports;

public partial class Forms_frmSaleOrderView : System.Web.UI.Page
{

    readonly OrderEntryController oeCtrl = new OrderEntryController();
   
    private DataTable m_SKUDt;
    private DataTable dtInvoice;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            Session.Remove("invoiceId");
            Session.Remove("orderId");
            LoadInvoice();
            LoadGrid();
            //if (Session["reportId"] != null)
            //{
            //    GetInvoice(Convert.ToInt64(Session["reportId"].ToString()));
            //}
            //Session.Remove("reportId");
        }
    }

    private void LoadInvoice()
    {
        DataTable dtInvoice = oeCtrl.SelectPendingOrder2(Convert.ToInt32(Session["Distributor_id"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.Order_Pending_Id, 2, int.Parse(Session["UserID"].ToString()), Constants.DateNullValue);

        Session.Add("dtInvoice", dtInvoice);

        grdInvoice.DataSource = dtInvoice;
        grdInvoice.DataBind();
    }
    
    private void LoadGrid()
    {
       
        dtInvoice = (DataTable)Session["dtInvoice"];

        if (dtInvoice != null)
        {
            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dtInvoice.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dtInvoice.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dtInvoice.DefaultView.RowFilter = "CUSTOMER_NAME" + " like '%" + "" + "%'";
                    break;
            }

            grdInvoice.DataSource = dtInvoice.DefaultView;
            grdInvoice.DataBind();
        }
        
    }

    
   

    protected void grdInvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Session.Add("invoiceId", grdInvoice.Rows[e.NewEditIndex].Cells[1].Text);
        Response.Redirect("frmDelieveryOrderAdd.aspx?LevelID="+ Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
    }

    protected void grdInvoice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // int m_sku_id = int.Parse(grdSaleOrder.Rows[e.RowIndex].Cells[0].Text);
       
        LoadGrid();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGrid();
        
    }

    private void GetInvoice(long sId)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}