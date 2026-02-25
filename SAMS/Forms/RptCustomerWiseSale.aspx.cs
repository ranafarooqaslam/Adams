using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For Business Analysis Report
/// </summary>
public partial class Forms_RptCustomerWiseSale : System.Web.UI.Page
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
            this.LoadWarehouse();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
        }
    }
    private void LoadWarehouse()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        DataRow[] dr = dt.Select("SUBZONE_ID = 2 OR SUBZONE_ID = 3");
        DataTable dt1 = dr.CopyToDataTable();
        if (dt1.Rows.Count > 0)
        {
            clsWebFormUtil.FillListBox(this.ChbWarehouse, dt1, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
        }
    }
    private void LoadDistributor()
    {
        DataTable finalDt = new DataTable();
        finalDt.Columns.Add("DISTRIBUTOR_ID", typeof(Int32));
        finalDt.Columns.Add("DISTRIBUTOR_NAME", typeof(string));
        System.Text.StringBuilder sbWarehouse = new System.Text.StringBuilder();
        foreach (ListItem li in ChbWarehouse.Items)
        {
            if (li.Selected)
            {
                UserController mUserController = new UserController();
                DataTable foundDt = mUserController.SelectDistributorAssignment(
                    int.Parse(li.Value.ToString()),
                    6, 3, int.Parse(this.Session["CompanyId"].ToString()));

                foreach (DataRow item in foundDt.Rows)
                {
                    DataRow dtrow = finalDt.NewRow();
                    dtrow["DISTRIBUTOR_ID"] = item["DISTRIBUTOR_ID"];
                    dtrow["DISTRIBUTOR_NAME"] = item["DISTRIBUTOR_NAME"];

                    finalDt.Rows.Add(dtrow);
                }
            }
        }

        if (finalDt.Rows.Count > 0)
        {
            clsWebFormUtil.FillListBox(this.ChbDistributor, finalDt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
        }
    }

    /// <summary>
    /// Shows Business Analysis Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    protected void ShowReport(int type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();

        System.Text.StringBuilder sbWarehouseId = new System.Text.StringBuilder();
        System.Text.StringBuilder sbDistributorId = new System.Text.StringBuilder();

        foreach (ListItem li in ChbWarehouse.Items)
        {
            if (li.Selected)
            {
                sbWarehouseId.Append(li.Value);
                sbWarehouseId.Append(",");
            }
        }

        foreach (ListItem li in ChbDistributor.Items)
        {
            if (li.Selected)
            {
                sbDistributorId.Append(li.Value);
                sbDistributorId.Append(",");
            }
        }

        var startDate = DateTime.Parse("1-" + txtStartDate.Text);
        var endDate = DateTime.Parse("1-" + txtEndDate.Text);

        var lastDayOfMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);

        endDate = DateTime.Parse(lastDayOfMonth + "-" + txtEndDate.Text);

        DataTable dt = mController.SelectReportTitle(int.Parse(Session["DISTRIBUTOR_ID"].ToString()));
        SAMSBusinessLayer.Reports.CrpCustomerWiseSale CrpReport = new SAMSBusinessLayer.Reports.CrpCustomerWiseSale();

        DataSet ds = RptSaleCtl.SelectCustomerWiseSale(sbDistributorId.ToString(), 
            startDate, endDate, 
            int.Parse(this.Session["UserId"].ToString()), txtNoOfCustomer.Text);

        ReportDocument SubReport = new ReportDocument();
        ReportDocument SubReport1 = new ReportDocument();

        DataSet ds1 = RptSaleCtl.GetMonthWiseSales(
           DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text),
           sbDistributorId.ToString(), "");

        SubReport = CrpReport.OpenSubreport("CustomerTotalSale");
        SubReport1 = CrpReport.OpenSubreport("CustomerTotalCount");
        SubReport.SetDataSource(ds1);
        SubReport1.SetDataSource(ds1);

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();


        CrpReport.SetParameterValue("Distributor", txtNoOfCustomer.Text);
        CrpReport.SetParameterValue("from_date", txtStartDate.Text);
        CrpReport.SetParameterValue("To_date", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    /// <summary>
    /// Shows Business Analysis Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    //    DocumentPrintController mController = new DocumentPrintController();
    //    RptSaleController RptSaleCtl = new RptSaleController();
    //    DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
    //    SAMSBusinessLayer.Reports.CrpBusinessAnalysis CrpReport = new SAMSBusinessLayer.Reports.CrpBusinessAnalysis();

    //    DataSet ds = RptSaleCtl.SelectBusinessAnalysis(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), int.Parse(this.Session["UserId"].ToString()),int.Parse(DrpPrincipal.SelectedValue.ToString()));
    //    CrpReport.SetDataSource(ds);
    //    CrpReport.Refresh();

    //    CrpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
    //    CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
    //    CrpReport.SetParameterValue("from_date", txtStartDate.Text);
    //    CrpReport.SetParameterValue("To_date", txtEndDate.Text);
    //    CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

    //    string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath() + "\\ExportedFile.xls";

    //    CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

    //    CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, path);

    //    System.IO.FileInfo file = new System.IO.FileInfo(path);

    //    if (file.Exists)
    //    {
    //        Response.Clear();

    //        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

    //        Response.AddHeader("Content-Length", file.Length.ToString());

    //        Response.ContentType = "application/octet-stream";

    //        Response.WriteFile(file.FullName);

    //        Response.End();

    //    }
    //    else
    //    {
    //        Response.Write("This file does not exist.");
    //    }        
    }
    protected void ChbAllWarehouse_CheckedChanged(object sender, EventArgs e)
    {
        CheckAll();
        LoadDistributor();
    }
    protected void ChbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistributor();
    }

    protected void CheckAll()
    {
        if (this.ChbAllWarehouse.Checked == true)
        {
            for (int i = 0; i < this.ChbWarehouse.Items.Count; i++)
            {
                this.ChbWarehouse.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ChbWarehouse.Items.Count; i++)
            {
                this.ChbWarehouse.Items[i].Selected = false;
            }
        }

    }
    protected void ChbAllDistributor_CheckedChanged(object sender, EventArgs e)
    {
        if (this.ChbAllDistributor.Checked == true)
        {
            for (int i = 0; i < this.ChbDistributor.Items.Count; i++)
            {
                this.ChbDistributor.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ChbDistributor.Items.Count; i++)
            {
                this.ChbDistributor.Items[i].Selected = false;
            }
        }

    }
}
