using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports;
using SAMSCommon.Classes;


public partial class Forms_rptLoadPass2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadLocation();
            LoadArea();
            this.LoadOrderBooker();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            this.txtFromDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            //this.txtToDate.Text = SAMS.Common.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
    }
    protected void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            this.drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    private void LoadOrderBooker()
    {
        DrpOrderBooker.Items.Clear();

        if ((DrpLocation.Items.Count > 0)&&(DrpRoute.Items.Count>0))
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpOrderBooker.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpOrderBooker, m_dt, 0, 3, false);
        }

    }
    protected void LoadLocation()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpLocation.DataSource = dt;
            this.DrpLocation.DataTextField = "DISTRIBUTOR_NAME";
            this.DrpLocation.DataValueField = "DISTRIBUTOR_ID";
            this.DrpLocation.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    private void LoadArea()
    {
        if (DrpLocation.Items.Count > 0)
        {
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            this.DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }
    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadOrderBooker();
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOrderBooker();

    }
  
    protected void Button1_Click(object sender, EventArgs e)
    {
        PrintReport(1);
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        PrintReport(0);
    }

    protected void PrintReport(int Type)
    {
        try
        {
            if (rdbReportType.SelectedIndex == 1)
            {

                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                DataTable dt = DPrint.SelectReportTitle(Convert.ToInt32(DrpLocation.SelectedValue));

                DataSet ds = null;
                ds = DPrint.LoadPass2(int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtFromDate.Text + " 00:00:00"), Constants.DateNullValue, int.Parse(DrpOrderBooker.SelectedValue.ToString()), 2);
                CrpLoadPass2 CrpReport = new CrpLoadPass2();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Distributor_Name", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Route", this.DrpRoute.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("SalemanName", this.DrpOrderBooker.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", Type);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                DataTable dt = DPrint.SelectReportTitle(Convert.ToInt32(DrpLocation.SelectedValue));

                DataSet ds = null;
                ds = DPrint.LoadPass2(int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtFromDate.Text + " 00:00:00"), Constants.DateNullValue, int.Parse(DrpOrderBooker.SelectedValue.ToString()), 1);
                CrpLoadPassIssue2 CrpReport = new CrpLoadPassIssue2();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Distributor_Name", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Route", this.DrpRoute.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("SalemanName", this.DrpOrderBooker.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", Type);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}