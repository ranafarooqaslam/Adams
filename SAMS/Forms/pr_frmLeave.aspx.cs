using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using MKB.TimePicker;
using System.Globalization;
using SAMSBusinessLayer.Models;
public partial class pr_frmLeave : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    LeaveTransactionsController ltc = new LeaveTransactionsController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetForm();
            this.fillddlEmployee(Constants.IntNullValue, Constants.IntNullValue);
            this.fillLeaveType();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveLeave();
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        ResetForm();
    }
    protected void ddlLocation_Change(object sender, EventArgs e)
    {
        int DepartmentID = Constants.IntNullValue;
        int LocationID = Constants.IntNullValue;
        if (ddlDepartment.SelectedIndex != 0)
        {

            DepartmentID = Int32.Parse(ddlDepartment.SelectedValue);
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            LocationID = Int32.Parse(ddlLocation.SelectedValue);
        }
        fillddlEmployee(LocationID, DepartmentID);
        fillrptEmployee(DepartmentID, LocationID);

    }
    protected void ddlDepartment_Change(object sender, EventArgs e)
    {
        int DepartmentID = Constants.IntNullValue;
        int LocationID = Constants.IntNullValue;
        if (ddlDepartment.SelectedIndex != 0)
        {

            DepartmentID = Int32.Parse(ddlDepartment.SelectedValue);
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            LocationID = Int32.Parse(ddlLocation.SelectedValue);
        }
        fillddlEmployee(LocationID, DepartmentID);
        fillrptEmployee(DepartmentID, LocationID);
    }
    protected void fillddlDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add("--All--");
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);
    }
    private void fillddlLocation()
    {
        //DataTable dt = cc.SelectCompany_lOCATIONS(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue);
        //ddlLocation.Items.Clear();
        //ddlLocation.Items.Add("--All--");
        //clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);




        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add("--All--");
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, false);
    }
    private void ResetForm()
    {
        fillddlDepartment();
        fillddlLocation();
        fillrptEmployee(Constants.IntNullValue, Constants.IntNullValue);
        txtComments.Text = string.Empty;
        txtDateFrom.Text = string.Empty;
        txtDateTo.Text = string.Empty;
        hfLeaveTransactionsID.Value = string.Empty;
        imgLeave.ImageUrl = "../images/btn-save.png";
    }
    private void fillrptEmployee( int DepartmentID, int LocationID)
    {
            DataTable dt = new DataTable();
            dt = ltc.SelectLeave(Constants.LongNullValue,Constants.LongNullValue, Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, DepartmentID, LocationID, false, 0,System.DateTime.Now);
            rptEmployee.DataSource = dt;
            rptEmployee.DataBind();
    }
    protected void SaveLeave()
    {
        if (ddlEmployee.SelectedValue != Constants.IntNullValue.ToString() && ddlLeaveType.SelectedIndex != 0 && txtDateFrom.Text != "" && txtDateTo.Text != "" && txtTimeFrom.Date != null && txtTimeTo.Date != null)
        {
            string TimeFrom = txtTimeFrom.Date.TimeOfDay.ToString();
            string TimeTo = txtTimeTo.Date.TimeOfDay.ToString();
            DateTime LeaveFrom = DateTime.ParseExact(txtDateFrom.Text + " " + TimeFrom, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime LeaveTo = DateTime.ParseExact(txtDateTo.Text + " " + TimeTo, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            TimeSpan LeaveTime = LeaveTo - LeaveFrom;            
            DataTable dtShift = ccc.SelectShift(Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()),Convert.ToInt32(ddlEmployee.SelectedValue));
            if (dtShift.Rows.Count > 0)
            { }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('  " + "" + "Shift for selected employee not found.');", true);
                return;
            }
            DateTime ShiftTimeFrom = Convert.ToDateTime(dtShift.Rows[0]["ShiftTimeFrom"].ToString());
            DateTime ShiftTimeTo = Convert.ToDateTime(dtShift.Rows[0]["ShiftTimeTo"].ToString());
            TimeSpan shifetTime;
            if (ShiftTimeTo > ShiftTimeFrom)
            {
                shifetTime = ShiftTimeTo - ShiftTimeFrom;

            }
            else
            {
                shifetTime = (Convert.ToDateTime("23:59:59") - ShiftTimeFrom);
                shifetTime = shifetTime.Add(ShiftTimeTo.TimeOfDay);
            }
            decimal shifthour = (shifetTime.Hours + (shifetTime.Minutes / 59));
            decimal LeaveHour = (LeaveTime.Hours + (LeaveTime.Minutes / 59));
            decimal Leavedays = LeaveTime.Days + (LeaveHour / shifthour);

            LeaveTransactionsModel ltm = new LeaveTransactionsModel();
            if (hfLeaveTransactionsID.Value != "")
            {
                ltm.LeaveTransactionsID = Convert.ToInt32(hfLeaveTransactionsID.Value);
            }
            ltm.Document_Date = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
            ltm.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
            ltm.LeaveFrom = LeaveFrom;
            ltm.LeaveTo = LeaveTo;
            ltm.Note = txtComments.Text;
            ltm.NumberofDays = Leavedays;
            ltm.TimeSheetID = 0;
            ltm.User_ID = Int32.Parse(Session["UserID"].ToString());
            ltm.LeaveID = Int32.Parse(ddlLeaveType.SelectedValue);
            ltm.IS_ACTIVE = true;
            ltm.IS_DELETED = false;
            if (hfLeaveTransactionsID.Value != "")
            {
                ltc.UpdateLeaveTransactions(ltm);
            }
            else
            {
                ltc.InsertLeaveTransactions(ltm);
            }
        }
        ResetForm();
    }
    protected void fillddlEmployee(int LocationID, int DepartmentID)
    {
        //DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, DepartmentID, LocationID, false, 0);
        //ddlEmployee.Items.Clear();
        //this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(ddlEmployee, dt, 0, 1, false);


            Distributor_UserController du = new Distributor_UserController();
            DataTable dt = du.SelectDistributorUser(Constants.IntNullValue, LocationID, int.Parse(this.Session["CompanyId"].ToString()),DepartmentID);

            ddlEmployee.Items.Clear();
            this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlEmployee, dt, 0, 6, false);
        
    }
    private void fillLeaveType()
    {
        DataTable dt = ccc.SelectLeave(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        ListItem li = new ListItem();
        li.Text = "--Select--";
        li.Value = "0";
        li.Selected = true;
        ddlLeaveType.Items.Add(li);
        clsWebFormUtil.FillDropDownList(ddlLeaveType, dt, 0, 1);
    }    
    protected void rptEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            DataTable dt = new DataTable();
            dt = ltc.SelectLeave(Convert.ToInt64(e.CommandArgument),Constants.LongNullValue, Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, false, 0, System.DateTime.Now);
            if (dt.Rows.Count > 0)
            {
                hfLeaveTransactionsID.Value = dt.Rows[0]["LeaveTransactionsID"].ToString();                
                ddlLeaveType.SelectedValue = dt.Rows[0]["LeaveID"].ToString();
                
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeID"].ToString();
                txtDateFrom.Text = dt.Rows[0]["LeaveFrom"].ToString();
                txtDateTo.Text = dt.Rows[0]["LeaveTo"].ToString();
                txtComments.Text = dt.Rows[0]["Note"].ToString();
                if (Convert.ToDateTime(dt.Rows[0]["LeaveFrom0"]).Hour > 12)
                {
                    txtTimeFrom.SetTime(Convert.ToDateTime(dt.Rows[0]["LeaveFrom0"]).Hour, Convert.ToDateTime(dt.Rows[0]["LeaveFrom0"]).Minute, TimeSelector.AmPmSpec.PM);
                }
                else
                {
                    txtTimeFrom.SetTime(Convert.ToDateTime(dt.Rows[0]["LeaveFrom0"]).Hour, Convert.ToDateTime(dt.Rows[0]["LeaveFrom0"]).Minute, TimeSelector.AmPmSpec.AM);
                }

                if (Convert.ToDateTime(dt.Rows[0]["LeaveTo0"]).Hour > 12)
                {
                    txtTimeTo.SetTime(Convert.ToDateTime(dt.Rows[0]["LeaveTo0"]).Hour, Convert.ToDateTime(dt.Rows[0]["LeaveTo0"]).Minute, TimeSelector.AmPmSpec.PM);
                }
                else
                {
                    txtTimeTo.SetTime(Convert.ToDateTime(dt.Rows[0]["LeaveTo0"]).Hour, Convert.ToDateTime(dt.Rows[0]["LeaveTo0"]).Minute, TimeSelector.AmPmSpec.AM);
                }
                imgLeave.ImageUrl = "../images/btn-update.png";
            }
        }
        else if (e.CommandName == "delete")
        {
            LeaveTransactionsModel ltm = new LeaveTransactionsModel();
            ltm.LeaveTransactionsID = Convert.ToInt32(e.CommandArgument);
            ltm.LeaveID = Constants.IntNullValue;
            ltm.TimeSheetID = Constants.IntNullValue;
            ltm.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
            ltm.LeaveFrom = Constants.DateNullValue;
            ltm.LeaveTo = Constants.DateNullValue;            
            ltm.NumberofDays = Constants.IntNullValue;
            ltm.Note = null;
            ltm.User_ID = Int32.Parse(Session["UserID"].ToString());
            ltm.IS_ACTIVE = true;
            ltm.IS_DELETED = true;
            if (ltc.UpdateLeaveTransactions(ltm))
            {
                ResetForm();
            }            
        }
    }
}
