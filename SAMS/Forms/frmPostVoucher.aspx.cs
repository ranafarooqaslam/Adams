using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Post Vouchers
/// </summary>
public partial class Forms_frmPostVoucher : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
     string voucher_No = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
    }

    /// <summary>
    /// Loads Locations To Location Comob
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
       // this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        this.DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        this.DrpPrincipal.Items.Add(new ListItem("GENERAL ENTRY", "0"));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Vouchers To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnView_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        LedgerController mController = new LedgerController();
        DataTable dt = mController.SelectUnPostVoucherNo(int.Parse(DrpVoucherType.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()),int.Parse(DrpPrincipal.SelectedValue.ToString()),false, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),1);
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();

        DataTable dt2 = mController.SelectUnPostVoucherNo(int.Parse(DrpVoucherType.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), true, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 1);
        GrdOrder.DataSource = dt2;
        GrdOrder.DataBind(); 
    }
    
    /// <summary>
    /// Post Vouchers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPost_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        LedgerController lController = new LedgerController();
        foreach (GridViewRow dr in GrdLedger.Rows)
        {
            CheckBox ChbSelect = (CheckBox)dr.Cells[0].FindControl("ChbSelect");
            if (ChbSelect.Checked == true)
            {
               lController.PostSelectVoucher(int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[1].Text, int.Parse(dr.Cells[5].Text), 0, DateTime.Parse(dr.Cells[6].Text));
            }
        }
        DataTable dt = lController.SelectUnPostVoucherNo(int.Parse(DrpVoucherType.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()),int.Parse(DrpPrincipal.SelectedValue.ToString()),false , DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),1);
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();
        DataTable dt2 = lController.SelectUnPostVoucherNo(int.Parse(DrpVoucherType.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), true, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 1);
        GrdOrder.DataSource = dt2;
        GrdOrder.DataBind(); 
    }

    /// <summary>
    /// Checks/UnChecks All Vouchers in Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbSelect_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbSelect.Checked == true)
        {
            foreach (GridViewRow dr in GrdLedger.Rows)
            {
                CheckBox ChbSelect1 = (CheckBox)dr.Cells[0].FindControl("ChbSelect");
                ChbSelect1.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in GrdLedger.Rows)
            {
                CheckBox ChbSelect1 = (CheckBox)dr.Cells[0].FindControl("ChbSelect");
                ChbSelect1.Checked = false;
            }
        }
    }

    /// <summary>
    /// Sets Voucher Grid Footer Contorls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[3].Text = "Total";  
            //e.Row.Cells[4].Text = HF1.Value;
            //e.Row.Cells[5].Text = HF2.Value;
        }
    }
    
    protected void GrdLedger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), GrdLedger.Rows[NewEditIndex].Cells[1].Text, int.Parse(DrpVoucherType.SelectedValue.ToString()));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void GrdOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), GrdOrder.Rows[NewEditIndex].Cells[0].Text, int.Parse(DrpVoucherType.SelectedValue.ToString()));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}