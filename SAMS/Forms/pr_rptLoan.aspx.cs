using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class pr_rptLoan : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadLocation();
            fillddlEmployee(Constants.IntNullValue,Constants.IntNullValue);
            fillddlParentDepartment();
            fillddlType();
            fillddlAssetType();
            this.txtFromDate.Text = System.DateTime.Today.ToString("MMM-yyyy");
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
        Int32 LocationID = Constants.IntNullValue;
        Int32 DepartmentID = Constants.IntNullValue;
        if (ddlDepartment.SelectedIndex != 0)
        {
            DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
        }
        fillddlEmployee(LocationID, DepartmentID);
    }
    
    protected void fillddlParentDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);

    }

    private void fillddlAssetType()
    {
        Loan_LeaseController llc = new Loan_LeaseController();
        DataTable dt = llc.SelectAssetType(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlAssetsType.Items.Clear();
        this.ddlAssetsType.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlAssetsType, dt, 0, 1, false);
    }

    private void fillddlType()
    {
        this.ddlType.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        this.ddlType.Items.Add(new ListItem("Loan", "1"));
        this.ddlType.Items.Add(new ListItem("Lease", "2"));
    }

    protected void fillddlEmployee(int LocationID , int DepartmentID)
    {
        
        //DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue,DepartmentID, LocationID, false);
        //ddlEmployee.Items.Clear();
        //this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(ddlEmployee, dt, 0, 1, false);






        Distributor_UserController du = new Distributor_UserController();
        DataTable dt = du.SelectDistributorUser(Constants.IntNullValue, LocationID, int.Parse(this.Session["CompanyId"].ToString()));
        ddlEmployee.Items.Clear();
        this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlEmployee, dt, 0, 6, false);
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
            Loan_LeaseController llc = new Loan_LeaseController();
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();            
            DataTable dt = DPrint.SelectReportTitle(int.Parse("0"));
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            DateTime dtFromMonth = DateTime.Parse(txtFromDate.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

            DateTime dtToMonth = DateTime.Parse(txtFromDate.Text);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            dtTo = dtTo.AddMonths(1).AddDays(-1);


            int AssetsType = Constants.IntNullValue;
            if (ddlType.SelectedValue == "3")
            {
                AssetsType = Convert.ToInt32(ddlAssetsType.SelectedValue);
            }

            DataTable dtAttendance = llc.GetLoan(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlType.SelectedValue), AssetsType, dtFrom, dtTo);

            SAMSBusinessLayer.Reports.PayrollReports.CrpLoan CrpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpLoan();
            CrpReport.SetDataSource(dtAttendance);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Branch", ddlLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("Department", ddlDepartment.SelectedItem.Text);
            CrpReport.SetParameterValue("Employee", ddlEmployee.SelectedItem.Text);
            CrpReport.SetParameterValue("ForMonth", this.txtFromDate.Text);            
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

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "2")
        {
            lblAssetsType.Visible = true;
            ddlAssetsType.Visible = true;
        }
        else
        {
            lblAssetsType.Visible = false;
            ddlAssetsType.Visible = false;
        }
    }
}
