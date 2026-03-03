using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_RptCrateAndBasket : System.Web.UI.Page
{
    AssetsController mController = new AssetsController();
    DataControl dc = new DataControl();
    private static int DistributorId;
    private static int CompanyId;
    private static int RowNo;

    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       ShowReport(1);
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
   
    public void ShowReport(int Type)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            AssetsController RptInventoryCtl = new AssetsController();

            SAMSBusinessLayer.Reports.DsReport2 ds = new SAMSBusinessLayer.Reports.DsReport2();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

            DataControl dc = new DataControl();
            DataTable result = mController.SelectLocationWiseAssetDetail(
                int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtDate.Text), 0, "");
            foreach (DataRow dr in result.Rows)
            {
                ds.Tables["RptLocationWiseAssetDetail"].ImportRow(dr);
            }

            ReportDocument CrpReport = new ReportDocument();
            
            CrpReport = new SAMSBusinessLayer.Reports.CrpAssetDetailLocationWise();
           
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("DocumentType", "Location Wise Asset Detail");
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Date", txtDate.Text);
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", Type);
            const string url = "'Default.aspx'";
            //const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            //Type cstype = this.GetType();
            //ClientScriptManager cs = Page.ClientScript;
            //cs.RegisterStartupScript(cstype, "OpenWindow", script);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openpage", "window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}