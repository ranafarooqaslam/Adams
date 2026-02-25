using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Deposit Slip Detail Report
/// </summary>
public partial class Forms_rptBankDepositSlipDetail : System.Web.UI.Page
{
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptAccountController _rptAccountCtl = new RptAccountController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadLocation();
            LoadAccount();
            LoadArea();
            LoadSaleForce();
            LoadCreditCustomer();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtFromDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtToDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            
            
        }
    }
    private void LoadArea()
    {
        if (DrpLocation.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            var mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    protected void LoadPrincipal()
    {
        try
        {
            var pController = new SKUPriceDetailController();
            DataTable mDt = pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, 
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0,
                DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.LongNullValue);

            drpPrincipal.Items.Add(new ListItem("All", "0"));
            clsWebFormUtil.FillDropDownList(drpPrincipal, mDt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void LoadCreditCustomer()
    {
        DrpCustomer.Items.Clear();
        if (DrpLocation.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            var mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(DrpLocation.SelectedValue), int.Parse(DrpRoute.SelectedValue), Constants.IntNullValue, Constants.IntNullValue);
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 4);
        }
        else
        {
            DrpCustomer.Items.Add(new ListItem("Customer Not Found", Constants.IntNullValue.ToString()));
        }
    }

    private void LoadSaleForce()
    {
        if (DrpLocation.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            ddlSaleForce.Items.Clear();
            var mDController = new SaleForceController();
            DataTable mDt = mDController.SelectSaleForceAssignedArea(int.Parse(DrpLocation.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(Session["CompanyId"].ToString()));
            ddlSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlSaleForce, mDt, 0, 3);
        }
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
        LoadCreditCustomer();
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        try
        {
            var dController = new DistributorController();
            DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            DrpLocation.DataSource = dt;
            DrpLocation.DataTextField = "DISTRIBUTOR_NAME";
            DrpLocation.DataValueField = "DISTRIBUTOR_ID";
            DrpLocation.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Account Heads To Account Combo
    /// </summary>
    protected void LoadAccount()
    {
        var mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, 43);
        //clsWebFormUtil.FillDropDownList(drpAccount, dt,0, 4, true);
        drpAccount.Items.Add(new ListItem("All", "0"));
        clsWebFormUtil.FillDropDownList(drpAccount, dt, 0, 4, false);
    }

    /// <summary>
    /// Shows Deposit Slip Detail in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        try
        {           
            string fromDate = null;
            string toDate = null;
            var crpReport = new CrpBankDepositSlipDetail2();
            DataSet ds = null;
            DataTable dt = _dPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue));
            DateTime parsedDateFromdate = DateTime.Parse(txtFromDate.Text);
            DateTime parsedDateTodate = DateTime.Parse(txtToDate.Text);
            fromDate = parsedDateFromdate.ToShortDateString();
            toDate = parsedDateTodate.ToShortDateString();
            ds = _rptAccountCtl.BankDepositSlipDetail(int.Parse(drpPrincipal.SelectedValue), int.Parse(DrpLocation.SelectedValue), Convert.ToDateTime(fromDate + " 00:00:00"), Convert.ToDateTime(toDate + " 23:59:59"), Convert.ToInt32(drpAccount.SelectedValue.ToString()),int.Parse(DrpCustomer.SelectedValue ),int.Parse(DrpRoute .SelectedValue ),int.Parse(ddlSaleForce.SelectedValue ));
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("Branch", DrpLocation.SelectedItem.Text);
            crpReport.SetParameterValue("FromDate", txtFromDate.Text);
            crpReport.SetParameterValue("ToDate", txtToDate.Text);
            crpReport.SetParameterValue("Account", drpAccount.SelectedItem.Text);
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Shows Deposit Slip Detail in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        try
        {

            string fromDate = null;
            string toDate = null;
            var crpReport = new CrpBankDepositSlipDetail2();
            DataSet ds = null;
            DataTable dt = _dPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue));

            DateTime parsedDateFromdate = DateTime.Parse(txtFromDate.Text);
            DateTime parsedDateTodate = DateTime.Parse(txtToDate.Text);
            fromDate = parsedDateFromdate.ToShortDateString();
            toDate = parsedDateTodate.ToShortDateString();

            ds = _rptAccountCtl.BankDepositSlipDetail(int.Parse(drpPrincipal.SelectedValue), int.Parse(DrpLocation.SelectedValue), Convert.ToDateTime(fromDate + " 00:00:00"), Convert.ToDateTime(toDate + " 23:59:59"), Convert.ToInt32(drpAccount.SelectedValue.ToString()), int.Parse(DrpCustomer.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(ddlSaleForce.SelectedValue));
            crpReport.SetDataSource(ds);
            crpReport.Refresh();

            crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("Branch", DrpLocation.SelectedItem.Text);
            crpReport.SetParameterValue("FromDate", txtFromDate.Text);
            crpReport.SetParameterValue("ToDate", txtToDate.Text);
            crpReport.SetParameterValue("Account", drpAccount.SelectedItem.Text);
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", 1);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    
    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        LoadSaleForce();
        LoadArea();
        LoadCreditCustomer();
    }
}