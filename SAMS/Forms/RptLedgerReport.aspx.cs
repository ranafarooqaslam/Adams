using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For General Ledger Report
/// </summary>
public partial class Forms_RptLedgerReport : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadAccountDetail();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable mDt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, mDt, 0, 1);
    }
    
    /// <summary>
    /// Loads Accont Heads To Account ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        var mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        DataView dv = new DataView(dtHead);
        dv.Sort = "ACCOUNT_DETAIL";
        dtHead = dv.ToTable();        
        clsWebFormUtil.FillDropDownList(ddlAccountHeadFrom, dtHead, "ACCOUNT_DETAIL", "ACCOUNT_DETAIL", true);
        clsWebFormUtil.FillDropDownList(ddlAccountHeadTo, dtHead, "ACCOUNT_DETAIL", "ACCOUNT_DETAIL", true);
    }

    /// <summary>
    /// Shows General Ledger in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows General Ledger in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    
    protected void ShowReport(int Type)
    {
        var dPrint = new DocumentPrintController();
        var rptAccountCtl = new RptAccountController();


        DataSet ds = rptAccountCtl.GeneralLedger_ViewRange(int.Parse(DrpPrincipal.SelectedValue), ddlAccountHeadFrom.SelectedItem.Text.Substring(0, ddlAccountHeadFrom.SelectedItem.Text.IndexOf("-")), ddlAccountHeadTo.SelectedItem.Text.Substring(0, ddlAccountHeadTo.SelectedItem.Text.IndexOf("-")), int.Parse(drpDistributor.SelectedValue.ToString()),
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0,Convert.ToInt32(Session["UserID"]));

        DataTable dt = dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        var crpReport = new CrpLedgerViewRange();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
        crpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
        crpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);


        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", Type);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}