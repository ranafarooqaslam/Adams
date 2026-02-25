using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For  Trial Balance Report
/// </summary>
public partial class Forms_RptTrialBalance : System.Web.UI.Page
{
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptAccountController _rptAccountCtl = new RptAccountController();
    readonly AccountHeadController _mAccountController = new AccountHeadController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();
            LoadAccount();
            LoadAccountDetail();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        var pController = new SKUPriceDetailController();
        DataTable mDt = pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0,
            DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.LongNullValue);
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //DrpPrincipal.Items.Add(new ListItem("General Entry", "0"));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, mDt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Main Account Heads To Main Account Combo
    /// </summary>
    private void LoadAccount()
    {

        DataTable dt = _mAccountController.SelectAccountHead(Constants.AC_MainTypeId, Constants.LongNullValue);
        DrpMainAccount.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpMainAccount , dt, 0, 10);   
    }

    /// <summary>
    /// Loads Account Heads To Account ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
       
        DataTable dtHead = _mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        DataView dv = new DataView(dtHead);
        dv.Sort = "ACCOUNT_DETAIL";
        dtHead = dv.ToTable(); 
        clsWebFormUtil.FillListBox(LstAccountHead, dtHead, "ACCOUNT_DETAIL", "ACCOUNT_DETAIL", true);
        Session.Add("dtHead", dtHead);
    }

    /// <summary>
    /// Shows Trial Balance in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
        
    }

    /// <summary>
    /// Shows Trial Balance in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {

        ShowReport(1);
      
    }

    /// <summary>
    /// Checks All Account Heads In Account Head ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbSelectAll.Checked)
        {
            txtFromAccount.Text = "B0000000000";
            txttoAccount.Text = "I9999999999";
           
            txtFromAccount.ReadOnly  = true;
            txttoAccount.ReadOnly = true;
        }
        else
        {
            txtFromAccount.Text = "";
            txttoAccount.Text = "";
            txtFromAccount.ReadOnly = false;
            txttoAccount.ReadOnly = false;
           
          //  ScriptManager.RegisterStartupScript(this, GetType(), "ShowListFrom", "ShowListFrom();", true);
            
        }
    }

    protected void ShowReport(int Type)
    {

        int PostedType = Constants.IntNullValue;
        if (int.Parse(rbPosted.SelectedValue) == -1)
        {
            PostedType = Constants.IntNullValue;

        }
        else
        {
             PostedType = int.Parse(rbPosted.SelectedValue);
        }
        

        DataSet ds = _rptAccountCtl.TrialBalance(int.Parse(DrpPrincipal.SelectedValue), int.Parse(drpDistributor.SelectedValue), int.Parse(DrpMainAccount.SelectedValue),
                  DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpLevel.SelectedValue), txtFromAccount.Text.Substring(0, 11), txttoAccount.Text.Substring(0, 11),PostedType,Convert.ToInt32(Session["UserID"]));

        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        var crpReport = new CrpTrialBalance();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
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
