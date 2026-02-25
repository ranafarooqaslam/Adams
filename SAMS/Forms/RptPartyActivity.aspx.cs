using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports;
using SAMSCommon.Classes;

/// <summary>
/// Form For  Credit Report
/// </summary>
public partial class Forms_RptPartyActivity : System.Web.UI.Page
{
    readonly RptCustomerController _rptCustomerCtl = new RptCustomerController();
    readonly DocumentPrintController _mController = new DocumentPrintController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadChannelType();
            LoadArea();
            LoadSaleForce();
            LoadCreditCustomer();
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
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    
    /// <summary>
    /// Loads Deliverymen To Sale Force Combo
    /// </summary>
    private void LoadSaleForce()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            ddlSaleForce.Items.Clear();
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
            ddlSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlSaleForce, m_dt, 0, 3);
        }        
    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }

    /// <summary>
    /// Loads Channel Types To ChannelType Combo
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        drpChannelType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpChannelType, dt, 0, 2);

    }

    /// <summary>
    /// Loads Credit Customers To Customer Combo
    /// </summary>
    private void LoadCreditCustomer()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.GetCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Convert.ToInt32(drpChannelType.SelectedValue));
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, "CUSTOMER_ID", "CUSTOMER_NAME");
        }
        else
        {
            DrpCustomer.Items.Add(new ListItem("Customer Not Found", Constants.IntNullValue.ToString()));
        }
    }

    /// <summary>
    /// Shows Credit Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Credit Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    
    /// <summary>
    /// Shows Credit Report Either in PDF Or in Excel
    /// </summary>
    /// <param name="pReportType">ReportType</param>
    private void ShowReport(int pReportType)
    {

        DataTable dt = _mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        CrystalDecisions.CrystalReports.Engine.ReportDocument crpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        if (rblType.SelectedValue == "0")
        {
            crpReport = new CrpPartyActivitySummary();
        }
        else if (rblType.SelectedValue == "1")
        {
            crpReport = new CrpPartyActivityDetail();
        }
        DataSet ds = _rptCustomerCtl.GetPartyActivityData(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), int.Parse(DrpCustomer.SelectedValue.ToString()), int.Parse(drpChannelType.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Convert.ToInt32(ddlSaleForce.SelectedValue),Convert.ToInt32(rblType.SelectedValue));
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("Area", DrpRoute.SelectedItem.Text);
        crpReport.SetParameterValue("ChannelType", drpChannelType.SelectedItem.Text);
        crpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
        crpReport.SetParameterValue("Salesman", ddlSaleForce.SelectedItem.Text);       
        crpReport.SetParameterValue("FromDate", txtStartDate.Text);
        crpReport.SetParameterValue("ToDate", txtEndDate.Text);

        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", pReportType);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Loads Order Bookers, Routes And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {        
        LoadArea();
        LoadSaleForce();
        LoadCreditCustomer();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSaleForce();
        LoadCreditCustomer();
    }

    protected void drpChannelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCreditCustomer();
    }
}