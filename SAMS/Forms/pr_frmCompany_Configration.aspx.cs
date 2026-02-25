using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// From To Assign/UnAssign Location, Principals And VoucherTypes To Users
/// </summary>
public partial class Forms_pr_frmCompany_Configration : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    SAMSCommon.Classes.DataControl dc = new DataControl();
    /// <summary>
    /// Page_Load Function Populates All Combos And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    /// 
    CompanyController cc = new CompanyController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetDepartment();
            ResetDesignation();
            ResetAllowances();
            ResetDeductions();
            ResetShift();
            ResetLeave();
            ResetException();
            ResetSalaryStructure();
            ResetPF();
        }
    }
    #region Department Tab
    protected void fillddlParentDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddlParentDepartment.Items.Clear();
        ddlParentDepartment.Items.Add("--Select--");
        clsWebFormUtil.FillDropDownList(ddlParentDepartment, dt, 1, 2, false);

    }
    protected void fillrptDepartments()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        rptDepartments.DataSource = dt;
        rptDepartments.DataBind();
    }
    protected void InsertUpdateDepartment()
    {
        try
        {
            int parentDepatment = 0;
            if (ddlParentDepartment.SelectedIndex != 0)
            {
                parentDepatment = Int32.Parse(ddlParentDepartment.SelectedValue);
            }
            else
            {
            }
            if (hdnDepartmentID.Value == "" || hdnDepartmentID.Value == null)
            {
                ccc.InsertDepartment(txtDepartmentName.Text, parentDepatment, Int32.Parse(Session["WorkingCompanyID"].ToString()), Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            }
            else
            {
                ccc.UpdateDepartment(Int32.Parse(hdnDepartmentID.Value), txtDepartmentName.Text, parentDepatment, Int32.Parse(Session["WorkingCompanyID"].ToString()), false, true, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));

            }
            ResetDepartment();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptDepartments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editdepartment")
        {
            DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["WorkingCompanyID"].ToString()), Int32.Parse(e.CommandArgument.ToString()));
            txtDepartmentName.Text = dt.Rows[0]["DepartmentName"].ToString();
            hdnDepartmentID.Value = dt.Rows[0]["DepartmentID"].ToString();
            imgDepartment.ImageUrl = "../images/btn-update.png";
            pnlDepartmentGrid.Visible = false;
            pnlDepartmentContent.Visible = true;
            if (dt.Rows[0]["ParentDepartment"] == null || dt.Rows[0]["ParentDepartment"].ToString() == "" || dt.Rows[0]["ParentDepartment"].ToString() == "0")
            {
                ddlParentDepartment.SelectedIndex = 0;
            }
            else
            {
                ddlParentDepartment.SelectedValue = dt.Rows[0]["ParentDepartment"].ToString();
            }
        }
        else if (e.CommandName == "deldepartment")
        {
            DataTable dtMessage = ccc.DeleteCompanyConfiguration(1, Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('" + dtMessage.Rows[0]["MESSAGE"].ToString() + "');", true);
            fillrptDepartments();
        }
    }
    protected void ResetDepartment()
    {
        hdnDepartmentID.Value = "";
        txtDepartmentName.Text = "";
        imgDepartment.ImageUrl = "../images/btn-save.png";
        ddlParentDepartment.SelectedIndex = 0;
        fillrptDepartments();
        fillddlParentDepartment();
        pnlDepartmentContent.Visible = false;
        pnlDepartmentGrid.Visible = true;
    }
    protected void btnSaveDepartment_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateDepartment();
        }
        catch
        {
        }
    }
    protected void btnDiscardDepartment_Click(object sender, EventArgs e)
    {
        try
        {
            ResetDepartment();
        }
        catch
        {
        }
    }
    protected void btnShowpnlDepartmentContent_Click(object sender, EventArgs e)
    {
        pnlDepartmentContent.Visible = true;
        pnlDepartmentGrid.Visible = false;
    }
    #endregion
    #region Designation Tab
    protected void fillrptDesignations()
    {
        DataTable dt = ccc.SelectDesignation(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        rptDesignation.DataSource = dt;
        rptDesignation.DataBind();
    }
    protected void InsertUpdateDesignation()
    {
        try
        {

            if (hdnDesignationID.Value == "" || hdnDesignationID.Value == null)
            {
                ccc.InsertDesignation(txtDesignationName.Text, Int32.Parse(Session["WorkingCompanyID"].ToString()), Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            }
            else
            {
                ccc.UpdateDesignation(Int32.Parse(hdnDesignationID.Value), txtDesignationName.Text, Int32.Parse(Session["WorkingCompanyID"].ToString()), false, true, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));

            }
            ResetDesignation();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptDesignation_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editdesignation")
        {
            DataTable dt = ccc.SelectDesignation(Int32.Parse(Session["WorkingCompanyID"].ToString()), Int32.Parse(e.CommandArgument.ToString()));
            txtDesignationName.Text = dt.Rows[0]["DesignationName"].ToString();
            hdnDesignationID.Value = dt.Rows[0]["DesignationID"].ToString();
            imgDesignation.ImageUrl = "../images/btn-update.png";
            pnlDesignationContent.Visible = true;
            pnlDesignationGrid.Visible = false;
        }
        else if (e.CommandName == "deldesignation")
        {
            DataTable dtMessage = ccc.DeleteCompanyConfiguration(2, Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('" + dtMessage.Rows[0]["MESSAGE"].ToString() + "');", true);
            fillrptDesignations();
        }

    }
    protected void ResetDesignation()
    {
        hdnDesignationID.Value = "";
        txtDesignationName.Text = "";
        fillrptDesignations();
        pnlDesignationContent.Visible = false;
        pnlDesignationGrid.Visible = true;
        imgDesignation.ImageUrl = "../images/btn-save.png";
    }
    protected void btnsaveDesignation_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateDesignation();
        }
        catch
        {
        }
    }
    protected void btnDiscardDesignation_Click(object sender, EventArgs e)
    {
        try
        {
            ResetDesignation();
        }
        catch
        {
        }
    }
    protected void btnShowpnlDesignationContent_Click(object sender, EventArgs e)
    {
        pnlDesignationContent.Visible = true;
        pnlDesignationGrid.Visible = false;
    }
    #endregion
    #region Allowances Tab
    protected void fillrptAllowances()
    {
        DataTable dt = ccc.SelectAllowances(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        rptAllowances.DataSource = dt;
        rptAllowances.DataBind();
    }
    protected void InsertUpdateAllowances()
    {
        try
        {
            int ratioType = 2;//for value
            if (rdbPercentage.Checked == true)
            {
                ratioType = 1;// for Percentage
            }
            if (hdnAllowanceID.Value == "" || hdnAllowanceID.Value == null)
            {
                ccc.InsertAllowances(Int32.Parse(Session["WorkingCompanyID"].ToString()), txtAllowanceDescription.Text, Decimal.Parse(txtAllowanceRatio.Text), ratioType, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            }
            else
            {
                ccc.UpdateAllowances(Int32.Parse(hdnAllowanceID.Value), Int32.Parse(Session["WorkingCompanyID"].ToString()), txtAllowanceDescription.Text, Decimal.Parse(txtAllowanceRatio.Text), ratioType, false, true, Int32.Parse(Session["UserID"].ToString()));
            }
            ResetAllowances();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptAllowances_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editallowance")
        {
            DataTable dt = ccc.SelectAllowances(Int32.Parse(e.CommandArgument.ToString()), Int32.Parse(Session["WorkingCompanyID"].ToString()));
            txtAllowanceDescription.Text = dt.Rows[0]["AllowanceDescription"].ToString();
            txtAllowanceRatio.Text = dt.Rows[0]["AllowanceRatio"].ToString();
            pnlAllowancesContent.Visible = true;
            pnlAllowancesGrid.Visible = false;
            if (dt.Rows[0]["RatioType"].ToString() == "1")
            {
                rdbPercentage.Checked = true;
            }
            else
            {
                rdbValue.Checked = true;
            }
            hdnAllowanceID.Value = dt.Rows[0]["AllowanceID"].ToString();
            imgAllowance.ImageUrl = "../images/btn-update.png";
        }
        else if (e.CommandName == "delallowance")
        {
            DataTable dtMessage = ccc.DeleteCompanyConfiguration(3, Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('" + dtMessage.Rows[0]["MESSAGE"].ToString() + "');", true);
            fillrptAllowances();
        }
    }
    protected void ResetAllowances()
    {
        hdnAllowanceID.Value = "";
        txtAllowanceRatio.Text = "";
        txtAllowanceDescription.Text = "";
        rdbPercentage.Checked = false;
        rdbValue.Checked = true;
        pnlAllowancesContent.Visible = false;
        pnlAllowancesGrid.Visible = true;
        fillrptAllowances();
        imgAllowance.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveAllowance_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateAllowances();
        }
        catch
        {
        }
    }
    protected void btnDiscardAllowance_Click(object sender, EventArgs e)
    {
        try
        {
            ResetAllowances();
        }
        catch
        {
        }
    }
    protected void btnShowAllowancesContent_Click(object sender, EventArgs e)
    {
        pnlAllowancesContent.Visible = true;
        pnlAllowancesGrid.Visible = false;
    }
    #endregion
    #region Deduction Tab
    protected void fillrptDeductions()
    {
        DataTable dt = ccc.SelectDeductions(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        rptDeductions.DataSource = dt;
        rptDeductions.DataBind();
    }
    protected void InsertUpdateDeductions()
    {
        try
        {
            int ratioType = 2;//for value
            if (rdbDeductionPercentage.Checked == true)
            {
                ratioType = 1;// for Percentage
            }
            if (hdnDeductionID.Value == "" || hdnDeductionID.Value == null)
            {
                ccc.InsertDeductions(Int32.Parse(Session["WorkingCompanyID"].ToString()), txtDeductionDescription.Text, Decimal.Parse(txtDeductionRatio.Text), ratioType, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            }
            else
            {
                ccc.UpdateDeductions(Int32.Parse(hdnDeductionID.Value), Int32.Parse(Session["WorkingCompanyID"].ToString()), txtDeductionDescription.Text, Decimal.Parse(txtDeductionRatio.Text), ratioType, false, true, Int32.Parse(Session["UserID"].ToString()));
            }
            ResetDeductions();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptDeductions_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editdeduction")
        {
            DataTable dt = ccc.SelectDeductions(Int32.Parse(e.CommandArgument.ToString()), Int32.Parse(Session["WorkingCompanyID"].ToString()));
            txtDeductionDescription.Text = dt.Rows[0]["DeductionDescription"].ToString();
            txtDeductionRatio.Text = dt.Rows[0]["DeductionRatio"].ToString();
            if (dt.Rows[0]["RatioType"].ToString() == "1")
            {
                rdbDeductionValue.Checked = false;
                rdbDeductionPercentage.Checked = true;
            }
            else
            {
                rdbDeductionPercentage.Checked = false;
                rdbDeductionValue.Checked = true;
            }
            hdnDeductionID.Value = dt.Rows[0]["DeductionID"].ToString();
            imgDeduction.ImageUrl = "../images/btn-update.png";
            pnlDeductionGrid.Visible = false;
            pnlDeductionContent.Visible = true;
        }
        else if (e.CommandName == "deldeduction")
        {
            DataTable dtMessage = ccc.DeleteCompanyConfiguration(4, Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('" + dtMessage.Rows[0]["MESSAGE"].ToString() + "');", true);
            fillrptDeductions();
        }
    }
    protected void ResetDeductions()
    {
        hdnDeductionID.Value = "";
        txtDeductionRatio.Text = "";
        txtDeductionDescription.Text = "";
        rdbDeductionPercentage.Checked = false;
        rdbDeductionValue.Checked = true;
        pnlDeductionGrid.Visible = true;
        pnlDeductionContent.Visible = false;
        fillrptDeductions();
        imgDeduction.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveDeduction_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateDeductions();
        }
        catch
        {
        }
    }
    protected void btnDiscardDeduction_Click(object sender, EventArgs e)
    {
        try
        {
            ResetDeductions();
        }
        catch
        {
        }
    }
    protected void btnShowDeductionContent_Click(object sender, EventArgs e)
    {
        pnlDeductionGrid.Visible = false;
        pnlDeductionContent.Visible = true;
    }

    #endregion
    #region Shift Tab
    protected void fillrptShift()
    {
        DataTable dt = ccc.SelectShift(Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        rptShift.DataSource = dt;
        rptShift.DataBind();
    }
    protected void InsertUpdateShift()
    {
        try
        {

            if (hdnShiftID.Value == "" || hdnShiftID.Value == null)
            {
                ccc.InsertShift(txtShiftName.Text, DateTime.Parse(txtShiftTimeFrom.Text), DateTime.Parse(txtShiftTimeTo.Text), (Int32.Parse(Session["WorkingCompanyID"].ToString())), Boolean.Parse(ddlActive.SelectedValue), Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()),Convert.ToInt32(dc.chkNull_0(txtLate2.Text)));
            }
            else
            {
                ccc.UpdateShift(Int32.Parse(hdnShiftID.Value.ToString()), txtShiftName.Text, DateTime.Parse(txtShiftTimeFrom.Text), DateTime.Parse(txtShiftTimeTo.Text), (Int32.Parse(Session["WorkingCompanyID"].ToString())), false, bool.Parse(ddlActive.SelectedValue), Int32.Parse(Session["UserID"].ToString()),Convert.ToInt32(dc.chkNull_0(txtLate2.Text)));
            }
            ResetShift();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptShift_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editshift")
        {
            DataTable dt = ccc.SelectShift(Int32.Parse(e.CommandArgument.ToString()), Constants.DateNullValue, Constants.DateNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
            txtShiftName.Text = dt.Rows[0]["ShiftName"].ToString();
            txtShiftTimeFrom.Text = Convert.ToDateTime(dt.Rows[0]["ShiftTimeFrom"].ToString()).TimeOfDay.ToString();
            txtShiftTimeTo.Text = Convert.ToDateTime(dt.Rows[0]["ShiftTimeTo"].ToString()).TimeOfDay.ToString();
            txtLate2.Text = dt.Rows[0]["LateMinutes"].ToString();
            pnlShiftContent.Visible = true;
            pnlShiftGrid.Visible = false;
            if (Convert.ToBoolean(dt.Rows[0]["IS_ACTIVE"].ToString()) == true)
            {
                ddlActive.SelectedIndex = 0;

            }
            else
            {
                ddlActive.SelectedIndex = 1;
            }
            hdnShiftID.Value = dt.Rows[0]["ShiftID"].ToString();
            imgShift.ImageUrl = "../images/btn-update.png";
        }
        else if (e.CommandName == "delshift")
        {
            DataTable dtMessage = ccc.DeleteCompanyConfiguration(5, Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('" + dtMessage.Rows[0]["MESSAGE"].ToString() + "');", true);
            fillrptShift();
        }
    }
    protected void ResetShift()
    {
        hdnShiftID.Value = "";
        txtShiftName.Text = "";
        txtLate2.Text = "";
        txtShiftTimeFrom.Text = "";
        txtShiftTimeTo.Text = "";
        ddlActive.SelectedIndex = 0;
        pnlShiftContent.Visible = false;
        pnlShiftGrid.Visible = true;
        fillrptShift();
        imgShift.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveShift_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateShift();
        }
        catch
        {
        }
    }
    protected void btnDiscardShift_Click(object sender, EventArgs e)
    {
        try
        {
            ResetShift();
        }
        catch
        {
        }
    }
    protected void btnShowpnlShiftContent_Click(object sender, EventArgs e)
    {
        pnlShiftContent.Visible = true;
        pnlShiftGrid.Visible = false;
    }

    #endregion
    #region Leave Tab
    protected void fillrptLeave()
    {
        DataTable dt = ccc.SelectLeave(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        rptLeave.DataSource = dt;
        rptLeave.DataBind();
    }
    protected void fillddlleavePaymentType()
    {

        DataTable dt = ccc.SelectLeavePaymentType(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlleavePaymentType, dt, 0, 1, true);
    }
    protected void InsertUpdateLeave()
    {
        try
        {
            int lateleave = Constants.IntNullValue;
            int shortleave = Constants.IntNullValue;
            int halfleave = Constants.IntNullValue;
            if (txtLate.Text.Length > 0)
            {
                try
                {
                    lateleave = Convert.ToInt32(txtLate.Text);
                }
                catch (Exception ex)
                {
                    lateleave = Constants.IntNullValue;
                }
            }

            if (txtShort.Text.Length > 0)
            {
                try
                {
                    shortleave = Convert.ToInt32(txtShort.Text);
                }
                catch (Exception ex)
                {
                    shortleave = Constants.IntNullValue;
                }
            }

            if (txtHalf.Text.Length > 0)
            {
                try
                {
                    halfleave = Convert.ToInt32(txtHalf.Text);
                }
                catch (Exception ex)
                {
                    halfleave = Constants.IntNullValue;
                }
            }

            if (hdnLeaveID.Value == "" || hdnLeaveID.Value == null)
            {
                ccc.InsertLeave(txtLeaveType.Text, (Int32.Parse(Session["WorkingCompanyID"].ToString())), Convert.ToInt32(txtMaximumAllowed.Text), int.Parse(ddlleavePaymentType.SelectedValue), Boolean.Parse(ddlOverride.SelectedValue), Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), lateleave, shortleave, halfleave);
            }
            else
            {
                ccc.UpdateLeave(Convert.ToInt32(hdnLeaveID.Value), txtLeaveType.Text, (Int32.Parse(Session["WorkingCompanyID"].ToString())), Convert.ToInt32(txtMaximumAllowed.Text), int.Parse(ddlleavePaymentType.SelectedValue), Boolean.Parse(ddlOverride.SelectedValue), false, true, Int32.Parse(Session["UserID"].ToString()), lateleave, shortleave, halfleave);
            }
            ResetLeave();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptLeave_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editleave")
        {
            DataTable dt = ccc.SelectLeave(Convert.ToInt32(e.CommandArgument), Int32.Parse(Session["WorkingCompanyID"].ToString()));
            txtLeaveType.Text = dt.Rows[0]["LeaveType"].ToString();
            txtMaximumAllowed.Text = dt.Rows[0]["MaximumAllowed"].ToString();
            pnlLeaveContent.Visible = true;
            pnlLeaveGrid.Visible = false;
            ddlleavePaymentType.SelectedValue = dt.Rows[0]["LeavePaymentTypeID"].ToString();
            txtLate.Text = dt.Rows[0]["LATE"].ToString();
            txtShort.Text = dt.Rows[0]["SHORT"].ToString();
            txtHalf.Text = dt.Rows[0]["HALF"].ToString();
            if (Convert.ToBoolean(dt.Rows[0]["OverRide"].ToString()) == true)
            {
                ddlOverride.SelectedIndex = 0;

            }
            else
            {
                ddlOverride.SelectedIndex = 1;
            }
            hdnLeaveID.Value = dt.Rows[0]["LeaveID"].ToString();
            imgLeave.ImageUrl = "../images/btn-update.png";
        }
        else if (e.CommandName == "delleave")
        {
            DataTable dtMessage = ccc.DeleteCompanyConfiguration(6, Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('" + dtMessage.Rows[0]["MESSAGE"].ToString() + "');", true);
            fillrptLeave();
        }
    }
    protected void ResetLeave()
    {
        hdnLeaveID.Value = "";
        txtMaximumAllowed.Text = "";
        txtLeaveType.Text = "";
        ddlOverride.SelectedIndex = 0;
        fillddlleavePaymentType();
        fillrptLeave();
        pnlLeaveContent.Visible = false;
        pnlLeaveGrid.Visible = true;
        imgLeave.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveLeave_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateLeave();
        }
        catch
        {
        }
    }
    protected void btnDiscardLeave_Click(object sender, EventArgs e)
    {
        try
        {
            ResetLeave();
        }
        catch
        {
        }
    }
    protected void btnShowpnlLeaveContent_Click(object sender, EventArgs e)
    {
        pnlLeaveContent.Visible = true;
        pnlLeaveGrid.Visible = false;
    }
    #endregion
    #region Exception Tab
    protected void fillrptException()
    {
        DataTable dt = ccc.SelectWorkHoursException(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        rptException.DataSource = dt;
        rptException.DataBind();
    }
    protected void InsertUpdateException()
    {
        try
        {
            if (hdnExceptionID.Value == "" || hdnExceptionID.Value == null)
            {
                ccc.InsertWorkHoursException(Int32.Parse(Session["WorkingCompanyID"].ToString()), 1, txtExceptionName.Text, Convert.ToDecimal(txtNumberOfHours.Text), Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            }
            else
            {
                ccc.UpdateWorkHoursException(Convert.ToInt32(hdnExceptionID.Value), Int32.Parse(Session["WorkingCompanyID"].ToString()), 1, txtExceptionName.Text, Convert.ToDecimal(txtNumberOfHours.Text), false, true, Int32.Parse(Session["UserID"].ToString()));
            }
            ResetException();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptException_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editexception")
        {
            DataTable dt = ccc.SelectWorkHoursException(Convert.ToInt32(e.CommandArgument), Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
            txtExceptionName.Text = dt.Rows[0]["ExceptionDescription"].ToString();
            txtNumberOfHours.Text = dt.Rows[0]["NumberOfHours"].ToString();
            hdnExceptionID.Value = dt.Rows[0]["ExceptionID"].ToString();
            imgException.ImageUrl = "../images/btn-update.png";
            pnlExceptionContent.Visible = true;
            pnlExceptionGrid.Visible = false;
        }
        else if (e.CommandName == "delexception")
        {
            DataTable dtMessage = ccc.DeleteCompanyConfiguration(7, Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('" + dtMessage.Rows[0]["MESSAGE"].ToString() + "');", true);
            fillrptException();
        }
    }
    protected void ResetException()
    {
        hdnExceptionID.Value = "";
        txtNumberOfHours.Text = "";
        txtExceptionName.Text = "";
        fillrptException();
        pnlExceptionContent.Visible = false;
        pnlExceptionGrid.Visible = true;
        imgException.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveException_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateException();
        }
        catch
        {
        }
    }
    protected void btnDiscardException_Click(object sender, EventArgs e)
    {
        try
        {
            ResetException();
        }
        catch
        {
        }
    }
    protected void btnShowpnlExceptionContent_Click(object sender, EventArgs e)
    {
        pnlExceptionContent.Visible = true;
        pnlExceptionGrid.Visible = false;
    }

    #endregion
    #region SalaryStructure Tab
    protected void fillrptSalaryStructure()
    {
        DataTable dt = ccc.SelectSalaryStructureMaster(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), 1);
        rptSalaryStructure.DataSource = dt;
        rptSalaryStructure.DataBind();
    }
    protected void fillAllAllowances()
    {
        DataTable dt = ccc.SelectAllowances(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        rptAllAllowances.DataSource = dt;
        rptAllAllowances.DataBind();
    }
    protected void fillAllDeductions()
    {
        DataTable dt = ccc.SelectDeductions(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        dt.Columns.Add("Comments", typeof(string));
        rptAllDeduction.DataSource = dt;
        rptAllDeduction.DataBind();
    }
    protected DataTable CreateDeductionTable()
    {
        DataTable dtDeduction = new DataTable();
        dtDeduction.Columns.Add("RatioType", typeof(int));
        dtDeduction.Columns.Add("DeductionRatio", typeof(decimal));
        dtDeduction.Columns.Add("DeductionID", typeof(int));
        dtDeduction.Columns.Add("DeductionDescription", typeof(string));
        dtDeduction.Columns.Add("Comments", typeof(string));
        return dtDeduction;
    }
    protected DataTable CreateAllowanceTable()
    {
        DataTable dtAllowance = new DataTable();
        dtAllowance.Columns.Add("RatioType", typeof(int));
        dtAllowance.Columns.Add("AllowanceRatio", typeof(decimal));
        dtAllowance.Columns.Add("AllowanceID", typeof(int));
        dtAllowance.Columns.Add("AllowanceDescription", typeof(string));
        return dtAllowance;

    }
    protected void InsertUpdateSalaryStructure()
    {
        try
        {
            if (hdnSalaryStructureID.Value == "" || hdnSalaryStructureID.Value == null)
            {
                int SalaryStructureID = ccc.InsertSalaryStructureMaster(txtSalaryStructureName.Text, Int32.Parse(Session["WorkingCompanyID"].ToString()), 1, Constants.DecimalNullValue, Constants.DecimalNullValue, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                InsertSalaryStructureDetail(SalaryStructureID);
            }
            else
            {
                ccc.UpdateSalaryStructureMaster(Convert.ToInt32(hdnSalaryStructureID.Value), txtSalaryStructureName.Text, (Int32.Parse(Session["WorkingCompanyID"].ToString())), 1, Constants.DecimalNullValue, Constants.DecimalNullValue, false, true, Int32.Parse(Session["UserID"].ToString()));
                InsertSalaryStructureDetail(Convert.ToInt32(hdnSalaryStructureID.Value));
            }
            ResetSalaryStructure();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void InsertSalaryStructureDetail(int SalaryStructureID)
    {
        try
        {
            ccc.DeleteSalaryStructureDetail(SalaryStructureID);
            foreach (RepeaterItem item in rptAllAllowances.Items)
            {
                TextBox txtAllowanceAmount = (TextBox)item.FindControl("txtAllowanceAmount");
                CheckBox chkAllowancePercentage = (CheckBox)item.FindControl("chkAllowancePercentage");
                HiddenField hdnAllAllownaceID = (HiddenField)item.FindControl("hdnAllAllownaceID");
                decimal AllowanceAmount = Decimal.Parse(txtAllowanceAmount.Text);
                int AllowanceID = Int32.Parse(hdnAllAllownaceID.Value);
                int amountTypeID = 2;
                if (chkAllowancePercentage.Checked == true)
                {
                    amountTypeID = 1;
                }
                ccc.InsertSalaryStructureDetail(SalaryStructureID, AllowanceID, Constants.IntNullValue, AllowanceAmount, null, amountTypeID);
            }
            foreach (RepeaterItem item in rptAllDeduction.Items)
            {
                TextBox txtDeductionAmount = (TextBox)item.FindControl("txtDeductionAmount");
                CheckBox chkDeductionPercentage = (CheckBox)item.FindControl("chkDeductionPercentage");
                HiddenField hdnAllDeductionID = (HiddenField)item.FindControl("hdnAllDeductionID");
                decimal DeductionAmount = Decimal.Parse(txtDeductionAmount.Text);
                int DeductionID = Int32.Parse(hdnAllDeductionID.Value);
                TextBox txtComments = (TextBox)item.FindControl("txtComments");
                string comments = txtComments.Text;
                int amountTypeID = 2;
                if (chkDeductionPercentage.Checked == true)
                {
                    amountTypeID = 1;
                }
                ccc.InsertSalaryStructureDetail(SalaryStructureID, Constants.IntNullValue, DeductionID, DeductionAmount, comments, amountTypeID);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rptSalaryStructure_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editsalarytemplate")
        {
            DataTable dt = ccc.SelectSalaryStructure(Convert.ToInt32(e.CommandArgument), null, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.IntNullValue);
            DataTable dtDeduction = CreateDeductionTable();
            DataTable dtAllowances = CreateAllowanceTable();
            pnlSalaryStructureContent.Visible = true;
            pnlSalaryStructureGrid.Visible = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["AllowanceID"].ToString() != null && dr["AllowanceID"].ToString() != "")
                {
                    DataRow drAllowance = dtAllowances.NewRow();
                    drAllowance["RatioType"] = dr["AmountTypeID"];
                    drAllowance["AllowanceRatio"] = dr["Amount"];
                    drAllowance["AllowanceID"] = dr["AllowanceID"];
                    drAllowance["AllowanceDescription"] = dr["AllowanceDescription"];
                    dtAllowances.Rows.Add(drAllowance);

                }
                if (dr["DeductionID"].ToString() != null && dr["DeductionID"].ToString() != "")
                {
                    DataRow drDeduction = dtDeduction.NewRow();
                    drDeduction["RatioType"] = dr["AmountTypeID"];
                    drDeduction["DeductionRatio"] = dr["Amount"];
                    drDeduction["DeductionID"] = dr["DeductionID"];
                    drDeduction["Comments"] = dr["Comment"];
                    drDeduction["DeductionDescription"] = dr["DeductionDescription"];
                    dtDeduction.Rows.Add(drDeduction);
                }
            }
            rptAllAllowances.DataSource = dtAllowances;
            rptAllAllowances.DataBind();
            rptAllDeduction.DataSource = dtDeduction;
            rptAllDeduction.DataBind();
            txtSalaryStructureName.Text = dt.Rows[0]["SalaryStructureName"].ToString();
            hdnSalaryStructureID.Value = dt.Rows[0]["SalaryStructureID"].ToString();
            imgSalaryStructure.ImageUrl = "../images/btn-update.png";
        }
        else if (e.CommandName == "delsalarytemplate")
        {
            DataTable dtMessage = ccc.DeleteCompanyConfiguration(8, Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:alert('" + dtMessage.Rows[0]["MESSAGE"].ToString() + "');", true);
            fillrptSalaryStructure();
        }
    }
    protected void ResetSalaryStructure()
    {
        fillrptSalaryStructure();
        fillAllDeductions();
        fillAllAllowances();
        hdnSalaryStructureID.Value = "";
        txtSalaryStructureName.Text = "";
        pnlSalaryStructureContent.Visible = false;
        pnlSalaryStructureGrid.Visible = true;
        imgSalaryStructure.ImageUrl = "../images/btn-save.png";
    }
    protected void btnSaveSalaryStructure_Click(object sender, EventArgs e)
    {
        try
        {
            InsertUpdateSalaryStructure();
            ResetSalaryStructure();
        }
        catch
        {
        }
    }
    protected void btnDiscardSalaryStructure_Click(object sender, EventArgs e)
    {
        try
        {
            ResetSalaryStructure();
        }
        catch
        {
        }
    }
    protected void btnShowpnlSalaryStructureContent_Click(object sender, EventArgs e)
    {
        pnlSalaryStructureContent.Visible = true;
        pnlSalaryStructureGrid.Visible = false;
    }

    #endregion
    #region Provident Fund
    protected void fillrptPF()
    {
        DataTable dt = ccc.GetCompanyPF(Constants.IntNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()));
        rptPF.DataSource = dt;
        rptPF.DataBind();
    }
    protected void btnSavePF_Click(object sender, EventArgs e)
    {
        try
        {
            decimal MaximumAmount = 0;
            decimal Percentage = 0;
            if (txtMaximumAmount.Text.Length > 0)
            {
                MaximumAmount = Convert.ToDecimal(txtMaximumAmount.Text);
            }
            if (txtPF.Text.Length > 0)
            {
                Percentage = Convert.ToDecimal(txtPF.Text);
            }

            if (MaximumAmount > 0 && Percentage > 0)
            {
                if (ccc.InsertCompanyPF(Convert.ToInt32(Session["WorkingCompanyID"]), Convert.ToDateTime(Session["CurrentWorkDate"]), MaximumAmount, Percentage, Convert.ToInt32(Session["UserID"])))
                {
                    ResetPF();
                }
            }
        }
        catch
        {
        }
    }
    protected void btnDiscardPF_Click(object sender, EventArgs e)
    {
        try
        {
            ResetPF();
        }
        catch
        {
        }
    }
    protected void rptPF_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = ccc.GetCompanyPF(Int32.Parse(e.CommandArgument.ToString()), Int32.Parse(Session["WorkingCompanyID"].ToString()));
        txtMaximumAmount.Text = dt.Rows[0]["MaximumAmount"].ToString();
        txtPF.Text = dt.Rows[0]["Percentage"].ToString();

        pnlPFContent.Visible = true;
        pnlPFGrid.Visible = false;

    }
    protected void ResetPF()
    {
        txtPF.Text = "";
        txtMaximumAmount.Text = "";
        pnlPFContent.Visible = false;
        pnlPFGrid.Visible = true;
        fillrptPF();
        if (rptPF.Items.Count > 0)
        {
            btnShowpnlPFContent.Visible = false;
        }
    }
    protected void btnShowpnlPFContent_Click(object sender, EventArgs e)
    {
        pnlPFContent.Visible = true;
        pnlPFGrid.Visible = false;
    }
    #endregion
}