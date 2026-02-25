using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;  
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For Print Sale Document Report
/// </summary>
public partial class Forms_frmDocumentPrinting : System.Web.UI.Page
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
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadSaleForce();
            this.LoadRoute();
            this.LoadCustomer();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
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
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Sale Forces To Sale Force Combo
    /// </summary>
    private void LoadSaleForce()
    {
        DrpArea.Items.Clear();
        SaleForceController mDController = new SaleForceController();
        DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(int.Parse(DrpLedgerType.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()));
        DrpArea.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpArea, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
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
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

        }
    }

    /// <summary>
    /// Loads Sale Forces And Routes
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
        this.LoadRoute();
        this.LoadCustomer();
    }

    /// <summary>
    /// Loads Sale Forces
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
    }

    /// <summary>
    /// Loads Sale Forces
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpLedgerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    /// <summary>
    /// Shows Print Sale Document in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        if (DrpLedgerType.SelectedValue == "5" || DrpLedgerType.SelectedValue == "6")
        {
            ShowReportSpecial(0);
        }
        else if (DrpLedgerType.SelectedValue == "8" || DrpLedgerType.SelectedValue == "9")
        {
            ShowReportDC(0);
        }
        else
        {
            ShowReport(0);
        }
    }

    /// <summary>
    /// Gets Print Sale Document And Shows Either in Excel or PDF
    /// </summary>
    /// <param name="p_ReportType">Type</param>
    private void ShowReport(int p_ReportType)
    {
        int p_CustomerType = Constants.IntNullValue;
        if (rblCustomerType.SelectedValue != "-1")
        {
            p_CustomerType = Convert.ToInt32(rblCustomerType.SelectedValue);
        }
        RptSaleController RptSaleCtl = new RptSaleController();
        DocumentPrintController DPrint = new DocumentPrintController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            if (DrpLedgerType.SelectedIndex == 4)
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpArea.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 5, Constants.LongNullValue, p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue), 0);
                ReportDocument SubReport = new ReportDocument();
                ReportDocument CrpReport = new ReportDocument();
                CrpReport = new SAMSBusinessLayer.Reports.CrpDeliveryChallan();
                CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;
                SubReport = CrpReport.OpenSubreport("Subreport1");
                CrpReport.SetDataSource(ds);
                SubReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("CompanyAddress", "Branch Address: " + dt.Rows[0]["ADDRESS1"].ToString());
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else if (DrpLedgerType.SelectedIndex == 7)
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectCSDUSCDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpArea.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 1, Constants.LongNullValue, p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue));
                ReportDocument CrpReport = new ReportDocument();
                CrpReport = new SAMSBusinessLayer.Reports.CrpPrintUSDInvoice();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;
                CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("CompanyAddress", "Branch Address: " + dt.Rows[0]["ADDRESS1"].ToString());
                CrpReport.SetParameterValue("TAXREGISTERATION_NO", dt.Rows[0]["GST_NUMBER"].ToString());
                CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.SelectDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpArea.SelectedValue), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                    DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpLedgerType.SelectedValue.ToString()), Constants.LongNullValue, p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue), 0);
                ReportDocument SubReport = new ReportDocument();
                ReportDocument CrpReport = new ReportDocument();
                if (DrpLedgerType.SelectedValue == "2")
                {
                    CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocument2();
                }
                else
                {
                    CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocument();
                }
                CrpReport.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;
                SubReport = CrpReport.OpenSubreport("SUBREPORT1");
                CrpReport.SetDataSource(ds);
                SubReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
                CrpReport.SetParameterValue("NtnNumber", "NTN :" + dt.Rows[0]["NTN_NO"].ToString());
                CrpReport.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
                CrpReport.SetParameterValue("BranchAddress", "Branch Address: " + dt.Rows[0]["ADDRESS1"].ToString());
                CrpReport.SetParameterValue("TAXREGISTERATION_NO", dt.Rows[0]["GST_NUMBER"].ToString());
                CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
                CrpReport.SetParameterValue("CONTACT_NUMBER", "Ph: " + dt.Rows[0]["CONTACT_NUMBER"].ToString());
                CrpReport.SetParameterValue("NTN_NO2", "NTN(" + dt.Rows[0]["NTN_NO2"].ToString() + ")");
                CrpReport.SetParameterValue("PrintType", "0");
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", p_ReportType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
    }

    private void ShowReportSpecial(int p_ReportType)
    {
        RptSaleController RptSaleCtl = new RptSaleController();
        DocumentPrintController DPrint = new DocumentPrintController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DataControl dc = new DataControl();
            ReportDocument CrpReport = new ReportDocument();
            if (DrpLedgerType.SelectedValue == "5")
            {
                ds = RptSaleCtl.GetSpecialDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpArea.SelectedValue), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), Constants.IntNullValue, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue), 0);
                CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentDCMetro();
            }
            else
            {
                ds = RptSaleCtl.GetSpecialDocumentforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpArea.SelectedValue), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), Constants.IntNullValue, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue), 0);
                CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentMAF();
            }
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
            CrpReport.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            CrpReport.SetParameterValue("LocationPhone", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            this.Session.Add("ReportType", p_ReportType);
            this.Session.Add("CrpReport", CrpReport);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    private void ShowReportDC(int p_ReportType)
    {
        RptSaleController RptSaleCtl = new RptSaleController();
        DocumentPrintController DPrint = new DocumentPrintController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DataControl dc = new DataControl();
            ReportDocument CrpReport = new ReportDocument();
            ds = RptSaleCtl.GetDeliveryChallanforPrint(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpArea.SelectedValue), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), Constants.IntNullValue, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue), 0);
            if (DrpLedgerType.SelectedValue == "8")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentDCTax();
            }
            else
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpPrintDocumentDCNonTax();
            }
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            if (DrpLedgerType.SelectedValue == "8")
            {
                CrpReport.SetParameterValue("LocationName", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
                CrpReport.SetParameterValue("LocationAddress", dt.Rows[0]["ADDRESS1"].ToString());
                CrpReport.SetParameterValue("LocationPhone", dt.Rows[0]["CONTACT_NUMBER"].ToString());
                CrpReport.SetParameterValue("LocationFax", dt.Rows[0]["CONTACT_PERSON"].ToString());
            }
            CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());
            this.Session.Add("ReportType", p_ReportType);
            this.Session.Add("CrpReport", CrpReport);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Print Sale Document in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        if (DrpLedgerType.SelectedValue == "5" || DrpLedgerType.SelectedValue == "6")
        {
            ShowReportSpecial(1);
        }
        else if (DrpLedgerType.SelectedValue == "8" || DrpLedgerType.SelectedValue == "9")
        {
            ShowReportDC(1);
        }
        else
        {
            ShowReport(1);
        }
    }
}