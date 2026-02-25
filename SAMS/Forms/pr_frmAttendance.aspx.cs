using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using MKB.TimePicker;
using System.Globalization;
using SAMSBusinessLayer.Models;
public partial class pr_frmAttendance : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    AttendanceController atd = new AttendanceController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            ResetForm();
            rblTime.SelectedValue = "0";
            btnSave.Attributes.Add("onclick", "return ValidateForm()");                        
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedValue != Constants.LongNullValue.ToString())
        {
            SaveAttendance();
        }
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
        fillrptEmployee(DepartmentID, LocationID,Convert.ToInt64(ddlEmployee.SelectedValue));

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
        fillrptEmployee(DepartmentID, LocationID,Convert.ToInt64(ddlEmployee.SelectedValue));
    }
    protected void fillddlDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);
    }
    private void fillddlLocation()
    {
        //DataTable dt = cc.SelectCompany_lOCATIONS(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue);
        //ddlLocation.Items.Clear();
        //ddlLocation.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);


        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, false);




    }
    private void ResetForm()
    {
        hfAttendanceID.Value = string.Empty;
        txtDate.Attributes.Add("readonly", "readonly");
        fillddlDepartment();
        fillddlLocation();
        this.fillddlEmployee(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue));
        if (ddlEmployee.Items.Count > 0)
        {
            fillrptEmployee(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt64(ddlEmployee.SelectedValue));
        }
    }
    private void fillrptEmployee( int DepartmentID, int LocationID, long EmployeeID)
    {
            DataTable dt = new DataTable();
            dt = atd.SelectAttendance(Constants.LongNullValue, EmployeeID, Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, DepartmentID, LocationID, false, 0, System.DateTime.Now);
            rptEmployee.DataSource = dt;
            rptEmployee.DataBind();
    }
    protected void SaveAttendance()
    {
        if ((ddlEmployee.SelectedValue != Constants.IntNullValue.ToString()))
        {
            AttendanceModel atm = new AttendanceModel();
            atm.DayofMonth = Convert.ToDateTime(txtDate.Text);
            atm.Document_Date = DateTime.Parse(Session["CurrentWorkDate"].ToString());
            atm.EmployeeID = Int32.Parse(ddlEmployee.SelectedValue);
            string strTime = tsTime.Hour.ToString() + ":" + tsTime.Minute.ToString() + ":" + tsTime.Second.ToString();
            DataTable dtShift = ec.GetEmployeeShift(Constants.IntNullValue, Convert.ToInt32(ddlEmployee.SelectedValue));
            if (hfAttendanceID.Value != "")
            {
                atm.AttendanceID = Convert.ToInt64(hfAttendanceID.Value);
            }
            if (rblTime.SelectedValue == "0")
            {
                if (IsFirstTimeIn())
                {
                    TimeSpan ts = new TimeSpan(0, Convert.ToInt32(dtShift.Rows[0]["LateMinutes"]), 0);
                    if (Convert.ToDateTime(dtShift.Rows[0]["ShiftTimeFrom"]).TimeOfDay.Add(ts) >= Convert.ToDateTime(strTime).TimeOfDay)
                    {
                        atm.IsLate = false;
                    }
                    else
                    {
                        atm.IsLate = true;
                    }
                }
                atm.AttendanceType = 0;
            }
            else
            {
                atm.AttendanceType = 1;
            }
            atm.TimeOfDay = strTime;           
            atm.TimeSheetID = Convert.ToInt32(dtShift.Rows[0]["ShiftID"]);
            atm.User_ID = Int32.Parse(Session["UserID"].ToString());
            if (hfAttendanceID.Value != "")
            {
                atm.IsLate = true;
                atd.UpdateAttendeance(atm);
                hfAttendanceID.Value = string.Empty;
            }
            else
            {
                atd.InsertAttendeance(atm);
            }
            fillrptEmployee(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt64(ddlEmployee.SelectedValue));
        }      
    }
    protected void fillddlEmployee(int LocationID, int DepartmentID)
    {
        //DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, DepartmentID, LocationID, false, 0);
        //ddlEmployee.Items.Clear();
        ////this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.LongNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(ddlEmployee, dt, 0, 1, false);
        Distributor_UserController du = new Distributor_UserController();
        DataTable dt = du.SelectDistributorUser(Constants.IntNullValue, LocationID, int.Parse(this.Session["CompanyId"].ToString()),DepartmentID);

        ddlEmployee.Items.Clear();
       this.ddlEmployee.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlEmployee, dt, 0, 6, false);



    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillrptEmployee(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt64(ddlEmployee.SelectedValue));        
    }
    protected string SetClass(bool IsLate)
    {
        if (IsLate)
        {
            return "RedRow";
        }
        else
        {
            return "WhiteRow";
        }
    }
    protected string GetAction(bool IsLate)
    {
        if (IsLate)
        {
            return "Remove Late";
        }
        else
        {
            return "";
        }
    }
    protected void rptEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "remove")
        {            
            AttendanceModel atm = new AttendanceModel();
            atm.AttendanceID = Convert.ToInt64(e.CommandArgument);
            atm.IsLate = false;
            atm.DayofMonth = Constants.DateNullValue;
            atm.Document_Date = Constants.DateNullValue;
            atm.EmployeeID = Constants.IntNullValue;
            atm.TimeOfDay = null;
            atm.AttendanceType = Constants.IntNullValue;
            atm.TimeSheetID = Constants.IntNullValue;
            atm.User_ID = Int32.Parse(Session["UserID"].ToString());
            atm.IS_ACTIVE = true;
            atm.IS_DELETED = false;
            if (atd.UpdateAttendeance(atm))
            {
                fillrptEmployee(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt64(ddlEmployee.SelectedValue));
            }
        }
        else if (e.CommandName == "edit")
        {
            DataTable dt = new DataTable();
            dt = atd.SelectAttendance(Convert.ToInt64(e.CommandArgument), Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(Session["WorkingCompanyID"]), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, false, 0, System.DateTime.Now);
            if (dt.Rows.Count > 0)
            {
                rblTime.SelectedValue = dt.Rows[0]["AttendanceType"].ToString();
                txtDate.Text = Convert.ToDateTime(dt.Rows[0]["DayofMonth"]).ToString("dd-MMM-yyyy");
                DateTime dtTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dt.Rows[0]["TimeOfDay"]);
                tsTime.Date = new DateTime(dtTime.Year, dtTime.Month, dtTime.Day, dtTime.Hour, dtTime.Minute, dtTime.Second);                
                hfAttendanceID.Value = dt.Rows[0]["AttendanceID"].ToString();                
            }
        }
        else if (e.CommandName == "del")
        {
            AttendanceModel atm = new AttendanceModel();
            atm.AttendanceID = Convert.ToInt64(e.CommandArgument);
            atm.IsLate = true;
            atm.DayofMonth = Constants.DateNullValue;
            atm.Document_Date = Constants.DateNullValue;
            atm.EmployeeID = Constants.IntNullValue;
            atm.TimeOfDay = null;
            atm.TimeSheetID = Constants.IntNullValue;
            atm.AttendanceType = Constants.IntNullValue;
            atm.User_ID = Int32.Parse(Session["UserID"].ToString());
            atm.IS_ACTIVE = true;
            atm.IS_DELETED = true;
            if (atd.UpdateAttendeance(atm))
            {
                fillrptEmployee(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt64(ddlEmployee.SelectedValue));
            }
        }
    }
    private bool IsFirstTimeIn()
    {
        bool flag = true;
        foreach (RepeaterItem ri in rptEmployee.Items)
        {
            HiddenField hfDayofMonth = (HiddenField)ri.FindControl("hfDayofMonth");
            if (Convert.ToDateTime(hfDayofMonth.Value) == Convert.ToDateTime(txtDate.Text))
            {
                flag = false;
                break;
            }
        }

        return flag;
    }
}