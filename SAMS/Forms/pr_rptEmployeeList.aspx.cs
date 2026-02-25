using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class pr_rptEmployeeList : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();
            FillddlContractType();
            FillddlStatus();
            fillddlParentDepartment();
        }
    }

    protected void LoadLocation()
    {
        try
        {
            //DataTable dt = cc.SelectCompany_lOCATIONS(Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, Constants.IntNullValue);
            //ddlLocation.Items.Clear();
            //ddlLocation.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
            //clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);


            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            ddlLocation.Items.Clear();
            this.ddlLocation.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, false);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads OrderBookers
    /// </summary>
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {        
    }
    
    protected void fillddlParentDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);

    }
    
    /// <summary>
    /// Shows Order Booker Reports in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Order Booker Reports in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    
    protected void ShowReport(int ReportType)
    {
        try
        {
            EmployeController EC = new EmployeController();
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();            
            DataTable dt = DPrint.SelectReportTitle(int.Parse("0"));

            DataTable dtList = ec.GetEmployeeList(Convert.ToInt32(Session["WorkingCompanyID"]), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlContractType.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue));

            SAMSBusinessLayer.Reports.PayrollReports.CrpEmployeeList CrpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpEmployeeList();

            CrpReport.SetDataSource(dtList);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Branch", ddlLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("Department", ddlDepartment.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", ReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void FillddlContractType()
    {
        DataTable dt = ec.SelectEmployee_Contract(Constants.IntNullValue, (Int32.Parse(Session["WorkingCompanyID"].ToString())));
        ddlContractType.Items.Clear();
        ddlContractType.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlContractType, dt, 0, 2, false);
    }

    private void FillddlStatus()
    {
        ddlStatus.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        ddlStatus.Items.Add(new ListItem("Active", "1"));
        ddlStatus.Items.Add(new ListItem("InActive", "0"));
    }
}
