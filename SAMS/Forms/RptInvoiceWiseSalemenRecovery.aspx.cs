using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_RptInvoiceWiseSalemenRecovery : System.Web.UI.Page
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
            this.LoadChannelType();
            this.LoadTown();
            this.LoadDeliveryman();
            this.LoadCustomer();            
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
    }
    
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        drpChannelType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpChannelType, dt, 0, 2);
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
        
    private void LoadDeliveryman()
    {
        DrpDeliveryMan.Items.Clear();
        DrpDeliveryMan.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

        if (drpDistributor.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpDeliveryMan, m_dt, 0, 3);
        }
        
    }
   
    protected void LoadTown()
    {
        DrpRoute.Items.Clear();
        DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0)
        {
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
            
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 1);
        }
    }
   
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (DrpRoute.SelectedValue != Constants.IntNullValue.ToString() || drpChannelType.SelectedValue != Constants.IntNullValue.ToString())
        {
            int p_CustomerType = Constants.IntNullValue;
            if (rblCustomerType.SelectedValue != "-1")
            {
                p_CustomerType = Convert.ToInt32(rblCustomerType.SelectedValue);
            }

            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(drpChannelType.SelectedValue), p_CustomerType);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

        }
    }
    
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTown();
        LoadDeliveryman();
        LoadCustomer();
    }
       
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDeliveryman();
        LoadCustomer();
    }
    
    protected void drpChannelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }
    
    protected void rblCustomerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }
    
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        
            ShowReport(0);
        
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
            if (DrpDocumentType.SelectedValue == "0")
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.InvoiceWiseSalesmanRecovery(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpDeliveryMan.SelectedValue.ToString()),
                   int.Parse(drpChannelType.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue), p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue));


                ReportDocument CrpReport = new ReportDocument();
                CrpReport = new SAMSBusinessLayer.Reports.CrpInvoicewiseSalemanRecovery();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());

                CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("DateFrom", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("DateTo", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("Saleman", DrpDeliveryMan.SelectedItem.Text);
                CrpReport.SetParameterValue("Channel", drpChannelType.SelectedItem.Text);
                CrpReport.SetParameterValue("Town", DrpRoute.SelectedItem.Text);
                CrpReport.SetParameterValue("IsRegister", rblCustomerType.SelectedItem.Text);

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", p_ReportType);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                DataControl dc = new DataControl();
                ds = RptSaleCtl.InvoiceWiseSalesmanRecovery1(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpDeliveryMan.SelectedValue.ToString()),
                   int.Parse(drpChannelType.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue), p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue));

                ReportDocument CrpReport = new ReportDocument();
                CrpReport = new SAMSBusinessLayer.Reports.CrpInvoicewiseSalemanRecovery1();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());

                CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("DateFrom", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("DateTo", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("Saleman", DrpDeliveryMan.SelectedItem.Text);
                CrpReport.SetParameterValue("Channel", drpChannelType.SelectedItem.Text);
                CrpReport.SetParameterValue("Town", DrpRoute.SelectedItem.Text);
                CrpReport.SetParameterValue("IsRegister", rblCustomerType.SelectedItem.Text);

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", p_ReportType);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
    }
    
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        int intTypeID = Constants.IntNullValue;
        int p_CustomerType = Constants.IntNullValue;
        if (rblCustomerType.SelectedValue != "-1")
        {
            p_CustomerType = Convert.ToInt32(rblCustomerType.SelectedValue);
        }
        if (DrpDocumentType.SelectedValue == "0")
        {
            intTypeID = 3;
        }
        else
        {
            intTypeID = 4;
        }
        RptSaleController RptSaleCtl = new RptSaleController();
        DataSet ds = null;

        DataControl dc = new DataControl();
        ds = RptSaleCtl.InvoiceWiseSalesmanRecovery2(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpDeliveryMan.SelectedValue.ToString()),
           int.Parse(drpChannelType.SelectedValue), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), intTypeID, p_CustomerType, Convert.ToInt32(DrpCustomer.SelectedValue));

        DataSetToExcel dsexcel = new DataSetToExcel();
        string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath() + "\\InvoiceWiseSalesmanRecovery.xls";
        DataSetToExcel.exportToExcel(ds, path);
        System.IO.FileInfo file = new System.IO.FileInfo(path);
        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
        else
        {
            Response.Write("This file does not exist.");
        }
    }
}