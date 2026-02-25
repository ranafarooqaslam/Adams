using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Customer Invoice Wise Sales Report
/// </summary>
public partial class Forms_RptBalanceConfirmationLetter : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadChannelType();
            txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");            
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0,
            DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.LongNullValue);
        DrpPrincipal.Items.Add(new ListItem("All", "0"));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        drpDistributor.Items.Clear();
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }

    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        drpChannelType.Items.Add(new ListItem("All", "0"));
        clsWebFormUtil.FillDropDownList(drpChannelType, dt, 0, 2);
    }

    /// <summary>
    /// Shows Customer Invoice Wise Sales Either in PDF Or in Excel
    /// </summary>
    /// <param name="p_Report_Type">ReportType</param>
    private void ShowReport(int p_Report_Type)
    {
        if (Grid_users.Rows.Count > 0)
        {
            string CustomerId = "0";
            foreach (GridViewRow gvr in Grid_users.Rows)
            {
                CheckBox ChbCustomer = (CheckBox)gvr.FindControl("ChbCustomer");
                if (ChbCustomer.Checked)
                {
                    CustomerId += gvr.Cells[1].Text + ",";
                }
            }
            CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
            RptCustomerController RptCustomerCtl = new RptCustomerController();
            DataSet ds = RptCustomerCtl.GetBalanceConfirmationLetterTemplate(Convert.ToInt32(drpDistributor.SelectedValue), CustomerId, DateTime.Parse(txtDate.Text + " 00:00:00"), Convert.ToInt32(rblType.SelectedValue));
            DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            if (rblType.SelectedValue == "0")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpBalanceConfirmationLetter();
            }
            else
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpBalanceConfirmationLetterDetail();
            }
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("ContactPerson", txtContactPerson.Text.Trim());
            CrpReport.SetParameterValue("Designation", txtDesignation.Text.Trim());
            CrpReport.SetParameterValue("Date", txtDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_Report_Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Customer Invoice Wise Sales in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void BtnViewPdf_Click(object sender, EventArgs e)
    {
        ShowReport(0);
        
    }

    /// <summary>
    /// Shows Customer Invoice Wise Sales in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    
    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        CustomerDataController mController = new CustomerDataController();
        DataTable dt = mController.GetCreditCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), ddSearchType.SelectedValue.ToString(), txtSeach.Text, Convert.ToInt32(drpChannelType.SelectedValue), Convert.ToInt32(ddlType.SelectedValue));
        Grid_users.DataSource = dt;
        Grid_users.DataBind();
    }
}