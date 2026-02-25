using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class pr_rptDisburesment : System.Web.UI.Page
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
    
    protected void fillddlEmployee(int LocationID , int DepartmentID)
    {
        
        //DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue,DepartmentID, LocationID, false);
        //ddlEmployee.Items.Clear();
        //this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.LongNullValue.ToString()));
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
            ProcessPayrollController PPC = new ProcessPayrollController();
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();            
            DataTable dt = DPrint.SelectReportTitle(int.Parse("0"));

            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            DateTime dtFromMonth = DateTime.Parse(txtFromDate.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

            DateTime dtToMonth = DateTime.Parse(txtFromDate.Text);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            dtTo = dtTo.AddMonths(1).AddDays(-1);


            DataSet dsSlip = PPC.GetDisburesmentStaff(Convert.ToInt32(Session["WorkingCompanyID"]), Convert.ToInt64(ddlEmployee.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), dtFrom, Convert.ToDateTime(dtTo.ToShortDateString() + " 23:59:59"));


            SAMSBusinessLayer.Reports.PayrollReports.CrpDisburesment CrpReport = new SAMSBusinessLayer.Reports.PayrollReports.CrpDisburesment();
            CrystalDecisions.CrystalReports.Engine.ReportDocument srAllownces = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrystalDecisions.CrystalReports.Engine.ReportDocument srDeduction = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            srAllownces = CrpReport.OpenSubreport("srAllownces");
            srDeduction = CrpReport.OpenSubreport("srDeduction");

            srAllownces.SetDataSource(dsSlip);
            srDeduction.SetDataSource(dsSlip);
            CrpReport.SetDataSource(dsSlip);
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
}
