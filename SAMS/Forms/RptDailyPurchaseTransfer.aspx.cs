using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Date Wise Stock Report
/// </summary>
public partial class Forms_RptDailyPurchaseTransfer : System.Web.UI.Page
{
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptInventoryController _rptInventoryCtl = new RptInventoryController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadArea();
            LoadCreditCustomer();
            LoadSaleForce();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            divdrodDown.Visible = false;
        }
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0,
            DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1);
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    private void LoadSaleForce()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            ddlSaleForce.Items.Clear();
            var mDController = new SaleForceController();
            DataTable mDt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), null);
            ddlSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlSaleForce, mDt, 0, 3);
        }
    }
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            var mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }
    private void LoadCreditCustomer()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            var mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 4);
        }
        else
        {
            DrpCustomer.Items.Add(new ListItem("Customer Not Found", Constants.IntNullValue.ToString()));
        }
    }

    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedValue == "13" || DrpDocumentType.SelectedValue == "14" ||
            DrpDocumentType.SelectedValue == "15")
        {
            divdrodDown.Visible = true;
        }
        else
        {
            divdrodDown.Visible = false;
        }
    }
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadSaleForce();
        LoadCreditCustomer();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadSaleForce();
        LoadArea();
        LoadCreditCustomer();
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
   {
      
        DataSet ds = null;
        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        if (DrpDocumentType.SelectedValue == "13" || DrpDocumentType.SelectedValue == "14" ||
            DrpDocumentType.SelectedValue == "15")
        {
            ds = _rptInventoryCtl.SelectPurchaseTransferStock(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue),
           DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue), Convert.ToInt32(rblRate.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpCustomer.SelectedValue), int.Parse(ddlSaleForce.SelectedValue));

        }
        else
        {
            ds = _rptInventoryCtl.SelectPurchaseTransferStock(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue),
             DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue), Convert.ToInt32(rblRate.SelectedValue));
  
        }
       
        var crpReport = new CrpDailyPurchaseTransfer();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        crpReport.SetParameterValue("PRINCIPAL", DrpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("LOCATION", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("Area", DrpRoute.SelectedItem.Text);
        crpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
        crpReport.SetParameterValue("SalePerson", ddlSaleForce.SelectedItem.Text);
        crpReport.SetParameterValue("ReportTitle", "Date Wise " + DrpDocumentType.SelectedItem.Text + " Report");
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("Price", rblRate.SelectedItem.Text);

        Session.Add("ReportType", 0);
        Session.Add("CrpReport", crpReport);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        

        DataSet ds = null;
        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));

        if (DrpDocumentType.SelectedValue == "13" || DrpDocumentType.SelectedValue == "14" ||
            DrpDocumentType.SelectedValue == "15")
        {
            ds = _rptInventoryCtl.SelectPurchaseTransferStock(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue),
           DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue), Convert.ToInt32(rblRate.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpCustomer.SelectedValue), int.Parse(ddlSaleForce.SelectedValue));

        }
        else
        {
            ds = _rptInventoryCtl.SelectPurchaseTransferStock(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue),
             DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue), Convert.ToInt32(rblRate.SelectedValue));

        }

        var crpReport = new CrpDailyPurchaseTransfer();
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        crpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        crpReport.SetParameterValue("PRINCIPAL", DrpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("LOCATION", drpDistributor.SelectedItem.Text);
        crpReport.SetParameterValue("Area", DrpRoute.SelectedItem.Text);
        crpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
        crpReport.SetParameterValue("SalePerson", ddlSaleForce.SelectedItem.Text);
        crpReport.SetParameterValue("ReportTitle", "Date Wise " + DrpDocumentType.SelectedItem.Text + " Report");
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("Price", rblRate.SelectedItem.Text);

        Session.Add("ReportType", 1);
        Session.Add("CrpReport", crpReport);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    
}
