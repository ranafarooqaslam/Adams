using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For NCS vs Bank Deposit Report
/// </summary>
public partial class Forms_RptVehiclLogBook : System.Web.UI.Page
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
            this.LoadDistributor();
            this.LoadVehicleNO();
            this.LoadDeliveryman();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadVehicleNO()
    {
        ddlVehicle.Items.Clear();
        DistributorController DC_ctrl = new DistributorController();
        DataTable v_dt = DC_ctrl.SelectVehicleNO(Constants.IntNullValue);
        this.ddlVehicle.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlVehicle, v_dt, "VEHICLE_ID", "VEHICLE_NO2");
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    private void LoadDeliveryman()
    {
        this.ddlSalesman.Items.Clear();
        if (ddlVehicle.SelectedValue == Constants.IntNullValue.ToString())
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            this.ddlSalesman.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlSalesman, m_dt, 0, 3);
        }
        else
        {
        if (ddlVehicle.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedAreaVehicle(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), long.Parse(ddlVehicle.SelectedValue.ToString()));
            this.ddlSalesman.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlSalesman, m_dt, 0, 3);
        }
            }
    }

    /// <summary>
    /// Shows NCS vs Bank Deposit in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows NCS vs Bank Deposit in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDeliveryman();
    }

    private void ShowReport(int ReportType)
    {
        DocumentPrintController DPrint = new DocumentPrintController();

        RptAccountController RptAccountCtl = new RptAccountController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        DataControl dc = new DataControl();
        ds = RptAccountCtl.GetVehiclLogBook(Convert.ToInt32(drpDistributor.SelectedValue),Convert.ToInt32(ddlVehicle.SelectedValue),Convert.ToInt32(ddlSalesman.SelectedValue),
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),Convert.ToInt32(rblViewType.SelectedValue),Convert.ToInt32(rblDataType.SelectedValue));

        CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        if (rblViewType.SelectedValue == "0")
        {
            if (rblDataType.SelectedValue == "0")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpVehiclLogSummaryBoth();
            }
            else if (rblDataType.SelectedValue == "1")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpVehiclLogSummaryFuel();
            }
            else
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpVehiclLogSummaryMaintenance();
            }
        }
        else
        {            
            if (rblDataType.SelectedValue == "0")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpVehiclLogDetailBoth();
            }
            else if (rblDataType.SelectedValue == "1")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpVehiclLogDetailFuel();
            }
            else
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpVehiclLogDetailMaintenance();
            }
        }
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DistributorName", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("VehiclNo", ddlVehicle.SelectedItem.Text);
        CrpReport.SetParameterValue("Salesman", ddlSalesman.SelectedItem.Text);        
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
        CrpReport.SetParameterValue("ReportType", rblDataType.SelectedItem.Text);

        this.Session.Add("ReportType", ReportType);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDeliveryman();
    }
}