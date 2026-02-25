
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For  Customer Wise (DSR) Report
/// </summary>
public partial class Forms_RptSaleForceWiseDailySale : System.Web.UI.Page
{

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadArea();
            LoadOrderBooker();
            LoadChannelType();
            LoadDeliveryman();
            LoadCustomer();
            LoadCatagory();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Order Bookers To OrderBooker Combo
    /// </summary>
    private void LoadOrderBooker()
    {
        DrpOrderbooker.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpPrincipal.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()), Convert.ToInt32(DrpPrincipal.SelectedValue));
            DrpOrderbooker.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpOrderbooker, m_dt, 0, 3);
        }
    }

    /// <summary>
    /// Loads Deliverymen To Deliverman Combo
    /// </summary>
    private void LoadDeliveryman()
    {
        DrpSaleForce.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
            DrpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpSaleForce, m_dt, 0, 3);
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



    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpRoute.SelectedValue != Constants.IntNullValue.ToString())
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

        }
    }
    protected void LoadCatagory()
    {
        DrpCatagory.Items.Clear();
        SkuHierarchyController Hierarchy = new SkuHierarchyController();
        DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));
        DataView dv = new DataView(dtCatagory);
        dv.RowFilter = "Company_id = " + Convert.ToInt32(DrpPrincipal.SelectedValue.ToString());
        dtCatagory = dv.ToTable();
        this.DrpCatagory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpCatagory, dtCatagory, "Category_Id", "Category_Name");
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
    /// Loads Order Bookers, Routes And Delivermen
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>    
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadDeliveryman();
        LoadOrderBooker();
        LoadCustomer();
        LoadCatagory();
    }

    /// <summary>
    /// Loads Order Bookers And Deliverymen
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDeliveryman();
        LoadOrderBooker();
        LoadCustomer();
    }

    /// <summary>
    /// Enables/Disables Controls According To Report Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>


    /// <summary>
    /// Shows Customer Wise (DSR) in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {


        Showreport(0);


    }

    /// <summary>
    /// Shows Customer Wise (DSR) in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        Showreport(1);
    }

    /// <summary>
    /// Loads Order Bookers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOrderBooker();
        LoadCatagory();
    }


    protected void Showreport(int Type)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));


        DataControl dc = new DataControl();
        ds = RptCustomerCtl.SelectSaleForceDSRProDuctWise(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
           DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(drpChannelType.SelectedValue.ToString()), int.Parse(RbReportType.SelectedIndex.ToString()), int.Parse(DrpSaleForce.SelectedValue.ToString()), Convert.ToInt32(DrpOrderbooker.SelectedValue), Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpCatagory.SelectedValue));
        if (RbReportType.SelectedIndex == 0)
        {
            SAMSBusinessLayer.Reports.CrpSaleForceProductWiseDSR CrpReport = new SAMSBusinessLayer.Reports.CrpSaleForceProductWiseDSR();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("Area", DrpRoute.SelectedItem.Text);
            CrpReport.SetParameterValue("ChannelType", drpChannelType.SelectedItem.Text);
            CrpReport.SetParameterValue("ReportType", "Saleman Wise Date Wise Sale (" + RbReportType.SelectedItem.Text + ") Report");
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("SaleForce", DrpSaleForce.SelectedItem.Text);
            CrpReport.SetParameterValue("Orderbooker", DrpOrderbooker.SelectedItem.Text);


            CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("Category", DrpCatagory.SelectedItem.Text);

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            SAMSBusinessLayer.Reports.CrpSaleForceProductWiseDSRValue CrpReport = new SAMSBusinessLayer.Reports.CrpSaleForceProductWiseDSRValue();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("Area", DrpRoute.SelectedItem.Text);
            CrpReport.SetParameterValue("ChannelType", drpChannelType.SelectedItem.Text);
            CrpReport.SetParameterValue("ReportType", "Saleman Wise Date Wise Sale (" + RbReportType.SelectedItem.Text + ") Report");
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("SaleForce", DrpSaleForce.SelectedItem.Text);
            CrpReport.SetParameterValue("Orderbooker", DrpOrderbooker.SelectedItem.Text);


            CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("Category", DrpCatagory.SelectedItem.Text);

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType",Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }
}
