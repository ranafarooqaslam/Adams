using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From to Add, Edit Employee
/// </summary>
public partial class Forms_frmSaleForce : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    Distributor_UserController UController = new Distributor_UserController();
    static int UserId;
    static bool IsSalaryEntered = false;
    DataControl dc = new DataControl();
    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDISTRIBUTOR();
            this.LoadDesignation();
            fillddlDepartment();
            this.LoadGrid();
            LoadEmployee();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            fillddlSalaryTemplate();
            fillrptShift();
        }
        Response.Expires = 0;
        Response.Cache.SetNoStore();       
    }

    #region Salary Template

    protected void fillrptShift()
    {
        DataTable dt = ccc.SelectShift(Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, Int32.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlShift, dt, 0, 1, true);
    }   
    protected void fillddlSalaryTemplate()
    {
        DataTable dt = ccc.SelectSalaryStructureMaster(Constants.IntNullValue, Int32.Parse(Session["CompanyId"].ToString()), 1);
        ddlSalaryTemplate.Items.Clear();
        ListItem li = new ListItem();
        li.Text = "--Select--";
        li.Value = "0";
        li.Selected = true;
        ddlSalaryTemplate.Items.Add(li);
        clsWebFormUtil.FillDropDownList(ddlSalaryTemplate, dt, 0, 1, false);
    }

    protected void ddlSalaryTemplate_Change(object sender, EventArgs e)
    {
        DataTable dt = ccc.SelectSalaryStructure(Int32.Parse(ddlSalaryTemplate.SelectedValue), null, Constants.IntNullValue, Constants.IntNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.IntNullValue);
        fillSalaryStructure(dt);
        ModalPopupExtender.Show();
    }

    protected void fillSalaryStructure(DataTable dt)
    {
        DataTable dtDeduction = CreateDeductionTable();
        DataTable dtAllowances = CreateAllowanceTable();
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

    protected int InsertUpdateSalaryStructure()
    {
        try
        {
            decimal BasicSalary = 0;
            if (txtBasicSalary.Text != "")
            {
                BasicSalary = Decimal.Parse(txtBasicSalary.Text);
            }
            int SalaryStructureID = Constants.IntNullValue;
            if (hdnSalaryStructureID.Value == "" || hdnSalaryStructureID.Value == null || hdnSalaryStructureID.Value == "0")
            {
                string SalaryStructureName = txtUserName.Text + "/" + DrpDepartment.SelectedItem.Text + "/" + ddDesignation.SelectedItem.Text;
                SalaryStructureID = ccc.InsertSalaryStructureMaster(SalaryStructureName, Int32.Parse(Session["CompanyId"].ToString()), 2, BasicSalary, Constants.DecimalNullValue, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                ccc.InsertSalaryStructureArchiveMaster(SalaryStructureID, SalaryStructureName, Int32.Parse(Session["CompanyId"].ToString()), 2, BasicSalary, Constants.DecimalNullValue, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                InsertSalaryStructureDetail(SalaryStructureID);
            }
            else
            {
                string SalaryStructureName = txtUserName.Text + "/" + DrpDepartment.SelectedItem.Text + "/" + ddDesignation.SelectedItem.Text;
                ccc.UpdateSalaryStructureMaster(Convert.ToInt32(hdnSalaryStructureID.Value), SalaryStructureName, (Int32.Parse(Session["CompanyId"].ToString())), 2, BasicSalary, Constants.DecimalNullValue, false, true, Int32.Parse(Session["UserID"].ToString()));
                ccc.InsertSalaryStructureArchiveMaster(Convert.ToInt32(hdnSalaryStructureID.Value), SalaryStructureName, Int32.Parse(Session["CompanyId"].ToString()), 2, BasicSalary, Constants.DecimalNullValue, Int32.Parse(Session["UserID"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                InsertSalaryStructureDetail(Convert.ToInt32(hdnSalaryStructureID.Value));
                SalaryStructureID = Convert.ToInt32(hdnSalaryStructureID.Value);
            }
            return SalaryStructureID;
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
                int detailID = ccc.InsertSalaryStructureDetail(SalaryStructureID, AllowanceID, Constants.IntNullValue, AllowanceAmount, null, amountTypeID);
                ccc.InsertSalaryStructureArchiveDetail(detailID, SalaryStructureID, AllowanceID, Constants.IntNullValue, AllowanceAmount, null, amountTypeID);
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
                int detailID = ccc.InsertSalaryStructureDetail(SalaryStructureID, Constants.IntNullValue, DeductionID, DeductionAmount, comments, amountTypeID);
                ccc.InsertSalaryStructureArchiveDetail(detailID, SalaryStructureID, Constants.IntNullValue, DeductionID, DeductionAmount, comments, amountTypeID);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnSaveSalaryStructure_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Hide();
        IsSalaryEntered = true;
    }
    protected void btnSalaryStructure_Click(object sender, EventArgs e)
    {
    }
    #endregion
    protected void LoadEmployee()
    {
        if (ddDistributorId.Items.Count > 0 && DrpReportingTo.Items.Count > 0)
        {
            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectDistributorUser(int.Parse(DrpReportingTo.SelectedValue.ToString()), int.Parse(ddDistributorId.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpReportingToExmployee , dt, 0, 6, true);
        }
    }
    private void LoadDISTRIBUTOR()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        ddDistributorId.DataSource = dtDistributor;
        ddDistributorId.DataTextField = "DISTRIBUTOR_NAME";
        ddDistributorId.DataValueField = "DISTRIBUTOR_ID";
        ddDistributorId.DataBind();
    }
    protected void fillddlDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["CompanyID"].ToString()), Constants.IntNullValue);
        DrpDepartment.Items.Clear();
       // this.DrpDepartment.Items.Add(new ListItem("--All--", Constants.IntNullValue.ToString()));
           
        clsWebFormUtil.FillDropDownList(DrpDepartment, dt, 1, 2, false);
    }
    
    /// <summary>
    /// Loads Designations To Designation Combo
    /// </summary>
    private void LoadDesignation()
    {        
        DataTable dt = ccc.SelectDesignation(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        ddDesignation.DataSource = dt;
        ddDesignation.DataTextField = "DesignationName";
        ddDesignation.DataValueField = "DesignationID";
        ddDesignation.DataBind();



        DrpReportingTo.DataSource = dt;
        DrpReportingTo.DataTextField = "DesignationName";
        DrpReportingTo.DataValueField = "DesignationID";
        DrpReportingTo.DataBind();


    }
    
    /// <summary>
    /// Loads Active Employees To Employee Grid
    /// </summary>
    protected void LoadGrid()
    {
        if (ddDistributorId.Items.Count > 0)
        {
            DataTable dt = UController.SelectDistributorUserAll(Constants.IntNullValue, int.Parse(ddDistributorId.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.Grid_users.DataSource = dt;
            this.Grid_users.DataBind();
        }
    }

    /// <summary>
    /// Loads Employees To Employee Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
    }
    
    /// <summary>
    /// Sets PageIndex Of Employee Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void Grid_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_users.PageIndex = e.NewPageIndex;
        this.LoadGrid(); 
    }

    /// <summary>
    /// Saves Or Updates An Employee.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime JoingDate = Constants.DateNullValue;
            DateTime ProbationFrom = Constants.DateNullValue;
            DateTime ProbationTo = Constants.DateNullValue;
            if (txtJoiningDate.Text.Trim().Length > 0)
            {
                JoingDate = Convert.ToDateTime(txtJoiningDate.Text);
            }

            if (txtProbationFrom.Text.Trim().Length > 0)
            {
                ProbationFrom = Convert.ToDateTime(txtProbationFrom.Text);
            }

            if (txtProbationTo.Text.Trim().Length > 0)
            {
                ProbationTo = Convert.ToDateTime(txtProbationTo.Text);
            }
            if (btnSave.Text == "Save")
            {
                if (IsSalaryEntered)
                { }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Enter Salary Structure')", true);
                    return;
                }
                SETTINGS_TABLE_Controller mAutoCode = new SETTINGS_TABLE_Controller();
                string strcode = "";
                UserId = int.Parse(mAutoCode.GetAutoCustomerCode("EM", 0, Constants.LongNullValue).ToString());
                if (UserId.ToString().Length == 1)
                {
                    strcode = "EM" + "000" + UserId.ToString();
                }
                else if (UserId.ToString().Length == 2)
                {
                    strcode = "EM" + "00" + UserId.ToString();
                }
                else if (UserId.ToString().Length == 3)
                {
                    strcode = "EM" + "0" + UserId.ToString();
                }
                else
                {
                    strcode = "EM" + UserId.ToString();
                }
                int SalaryStructureID=0;
                 SalaryStructureID = InsertUpdateSalaryStructure();
                 if (SalaryStructureID != 0)
                 {
                     string SaleForceId = UController.InsertDistributor_User(int.Parse(this.Session["CompanyId"].ToString()), txtNICNo.Text, true, System.DateTime.Now, System.DateTime.Now, int.Parse(ddDesignation.SelectedValue.ToString()),
                     int.Parse(ddDistributorId.SelectedValue.ToString()), Constants.IntNullValue, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, "", ""
                     , txtMobileNo.Text, strcode, txtUserName.Text, txtPhoneNo.Text, txtFatherName.Text, txtHusbandName.Text, txtReligion.Text, txtNoOfDependents.Text, txtLastEducation.Text, txtLastEmployeeInfo.Text, decimal.Parse(dc.chkNull_0(txtLastSalaryDrawn.Text)), txtLastDesignation.Text, txtReasionOfResignation.Text, txtReferance.Text, txtLastContactNo.Text, DrpReportingTo.SelectedValue, int.Parse(DrpDepartment.SelectedValue), "", int.Parse(DrpGender.SelectedValue), txtEmergencyPersonName.Text, txtEmergencyContactNo.Text, bool.Parse(DrpMaritalStatus.SelectedValue), int.Parse(DrpPaymentMode.SelectedValue), txtBankName.Text, txtAccountTitle.Text, txtAccountNo.Text, int.Parse(DrpSalaryChargedTo.SelectedValue), DrpReportingToExmployee.SelectedValue, SalaryStructureID, int.Parse(ddlSalaryTemplate.SelectedValue), int.Parse(ddlShift.SelectedValue),JoingDate,ProbationFrom,ProbationTo,Convert.ToInt32(ddlEmployeeType.SelectedValue));
                     mAutoCode.GetAutoCustomerCode("EM", 1, long.Parse(UserId.ToString()));
                 }
                 else
                 {
                     return;
                 }
            }
            else if (btnSave.Text == "Update")
            {
                int SalaryStructureID = InsertUpdateSalaryStructure();
                UController.UpdateDistributor_User(UserId, int.Parse(this.Session["CompanyId"].ToString()), txtNICNo.Text, chkIsActive.Checked, System.DateTime.Now, System.DateTime.Now, int.Parse(ddDesignation.SelectedValue.ToString()),
                    int.Parse(ddDistributorId.SelectedValue.ToString()), Constants.IntNullValue, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, "",""
                    , txtMobileNo.Text, null, txtUserName.Text, txtPhoneNo.Text, txtFatherName.Text, txtHusbandName.Text, txtReligion.Text, txtNoOfDependents.Text, txtLastEducation.Text, txtLastEmployeeInfo.Text, decimal.Parse(dc.chkNull_0(txtLastSalaryDrawn.Text)), txtLastDesignation.Text, txtReasionOfResignation.Text, txtReferance.Text, txtLastContactNo.Text, DrpReportingTo.SelectedValue, int.Parse(DrpDepartment.SelectedValue), "", int.Parse(DrpGender.SelectedValue), txtEmergencyPersonName.Text, txtEmergencyContactNo.Text, bool.Parse(DrpMaritalStatus.SelectedValue), int.Parse(DrpPaymentMode.SelectedValue), txtBankName.Text, txtAccountTitle.Text, txtAccountNo.Text, int.Parse(DrpSalaryChargedTo.SelectedValue), DrpReportingToExmployee.SelectedValue, SalaryStructureID, int.Parse(ddlSalaryTemplate.SelectedValue), int.Parse(ddlShift.SelectedValue),JoingDate,ProbationFrom,ProbationTo,Convert.ToInt32(ddlEmployeeType.SelectedValue));
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Updated !')", true);
            LoadGrid();
            ClearControls();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Try Again !')", true);
        }
    }

    /// <summary>
    /// Cancels Save Or Update Transaction
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LoadGrid();
        ClearControls();
        btnSave.Text = "Save";
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    protected void ClearControls()
    {
        try
        {
            txtUserName.Text = null;
            txtPhoneNo.Text = null;
            txtMobileNo.Text = null;
            txtAddress1.Text = null;
            txtAddress2.Text = null;
            txtEmail.Text = null;
            txtNICNo.Text = null;
            lblErrorMsg.Text = null;
    #region Last Job Detail
        txtFatherName.Text = "";
        txtHusbandName.Text = "";            
        txtReligion.Text = null;
        txtNoOfDependents.Text = null;
        txtLastEducation.Text = null;
        txtLastEmployeeInfo.Text = null;
        txtLastSalaryDrawn.Text = null;
        txtLastDesignation.Text = null;           
        txtReasionOfResignation.Text = null;
        txtLastContactNo.Text = null;
        txtReferance.Text = null;
        DrpReportingTo.SelectedIndex = 0;
        DrpDepartment.SelectedIndex = 0;
        DrpGender.SelectedIndex = 0;
        DrpReportingToExmployee.SelectedIndex = 0;
        txtEmergencyPersonName.Text = null;
        txtEmergencyContactNo.Text = null;
        DrpMaritalStatus.SelectedIndex = 0;
        DrpPaymentMode.SelectedIndex = 0;
        txtBankName.Text = null;
        txtAccountNo.Text = null;
        txtAccountTitle.Text =null;
        DrpSalaryChargedTo.SelectedIndex = 0;
        txtJoiningDate.Text = string.Empty;
        txtProbationFrom.Text = string.Empty;
        txtProbationTo.Text = string.Empty;
    #endregion
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
   
    protected void DrpReportingTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployee();

    }

    protected void Grid_users_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr2 = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr2.RowIndex;
        try
        {
            btnSave.Text = "Update";
            GridViewRow gvr = this.Grid_users.Rows[NewEditIndex];
            UserId = int.Parse(gvr.Cells[0].Text);

            if (gvr.Cells[2].Text == "&nbsp;")
            {
                txtUserName.Text = null;
            }
            else
            {
                this.txtUserName.Text = gvr.Cells[2].Text;
            }
            if (gvr.Cells[3].Text == "&nbsp;")
            {
                txtNICNo.Text = null;
            }
            else
            {
                this.txtNICNo.Text = gvr.Cells[3].Text;
            }

            if (gvr.Cells[4].Text == "&nbsp;")
            {
                txtPhoneNo.Text = null;
            }
            else
            {
                this.txtPhoneNo.Text = gvr.Cells[4].Text;
            }
            if (gvr.Cells[5].Text == "&nbsp;")
            {
                txtMobileNo.Text = null;
            }
            else
            {
                this.txtMobileNo.Text = gvr.Cells[5].Text;
            }
            if (gvr.Cells[6].Text == "&nbsp;")
            {
                txtEmail.Text = null;
            }
            else
            {
                this.txtEmail.Text = gvr.Cells[6].Text;
            }
            if (gvr.Cells[7].Text == "&nbsp;")
            {
                txtAddress1.Text = null;
            }
            else
            {
                this.txtAddress1.Text = gvr.Cells[7].Text;
            }
            if (gvr.Cells[8].Text == "&nbsp;")
            {
                txtAddress2.Text = null;
            }
            else
            {
                this.txtAddress2.Text = gvr.Cells[8].Text;
            }

            this.chkIsActive.Checked = bool.Parse(gvr.Cells[10].Text);

            try
            {

                ddDesignation.SelectedValue = gvr.Cells[11].Text;

            }

            catch (Exception ex)
            {
                ddDesignation.SelectedIndex = 0;
            }
            #region Last Job Detail
            /////////////////////////////////////////
            if (gvr.Cells[13].Text == "&nbsp;")
            {
                txtFatherName.Text = null;
            }
            else
            {
                this.txtFatherName.Text = gvr.Cells[13].Text;
            }



            if (gvr.Cells[14].Text == "&nbsp;")
            {
                txtHusbandName.Text = null;
            }
            else
            {
                this.txtHusbandName.Text = gvr.Cells[14].Text;
            }
            if (gvr.Cells[15].Text == "&nbsp;")
            {
                txtReligion.Text = null;
            }
            else
            {
                this.txtReligion.Text = gvr.Cells[15].Text;
            }
            if (gvr.Cells[16].Text == "&nbsp;")
            {
                txtNoOfDependents.Text = null;
            }
            else
            {
                this.txtNoOfDependents.Text = gvr.Cells[16].Text;
            }
            if (gvr.Cells[17].Text == "&nbsp;")
            {
                txtLastEducation.Text = null;
            }
            else
            {
                this.txtLastEducation.Text = gvr.Cells[17].Text;
            }
            if (gvr.Cells[18].Text == "&nbsp;")
            {
                txtLastEmployeeInfo.Text = null;
            }
            else
            {
                this.txtLastEmployeeInfo.Text = gvr.Cells[18].Text;
            }
            if (gvr.Cells[19].Text == "&nbsp;")
            {
                txtLastSalaryDrawn.Text = null;
            }
            else
            {
                this.txtLastSalaryDrawn.Text = Math.Round(decimal.Parse(gvr.Cells[19].Text), 2).ToString();
            }
            if (gvr.Cells[20].Text == "&nbsp;")
            {
                txtLastDesignation.Text = null;
            }
            else
            {
                this.txtLastDesignation.Text = gvr.Cells[20].Text;
            }
            if (gvr.Cells[21].Text == "&nbsp;")
            {
                txtReasionOfResignation.Text = null;
            }
            else
            {
                this.txtReasionOfResignation.Text = gvr.Cells[21].Text;
            }
            if (gvr.Cells[22].Text == "&nbsp;")
            {
                txtLastContactNo.Text = null;
            }
            else
            {
                this.txtLastContactNo.Text = gvr.Cells[22].Text;
            }

            if (gvr.Cells[23].Text == "&nbsp;")
            {
                txtReferance.Text = null;
            }
            else
            {
                this.txtReferance.Text = gvr.Cells[23].Text;
            }
            try
            {
                if (gvr.Cells[24].Text == "&nbsp;")
                {
                    DrpReportingTo.SelectedIndex = 0;
                }
                else
                {
                    this.DrpReportingTo.SelectedValue = gvr.Cells[24].Text;
                }
            }
            catch (Exception ex)
            {
                this.DrpReportingTo.SelectedIndex = 0;
            }
            try
            {
                if (gvr.Cells[25].Text == "&nbsp;")
                {
                    DrpDepartment.SelectedIndex = 0;
                }
                else
                {
                    this.DrpDepartment.SelectedValue = gvr.Cells[25].Text;
                }

            }
            catch (Exception ex)
            {
                this.DrpDepartment.SelectedIndex = 0;
            }


            /////
            try
            {
                if (gvr.Cells[27].Text == "&nbsp;")
                {
                    DrpGender.SelectedIndex = 0;
                }
                else
                {
                    this.DrpGender.SelectedValue = gvr.Cells[27].Text;
                }

            }
            catch (Exception ex)
            {
                this.DrpGender.SelectedIndex = 0;
            }

            try
            {
                if (gvr.Cells[28].Text == "&nbsp;")
                {
                    txtEmergencyPersonName.Text = "";
                }
                else
                {
                    txtEmergencyPersonName.Text = gvr.Cells[28].Text;
                }

            }
            catch (Exception ex)
            {
                txtEmergencyPersonName.Text = "";
            }
            try
            {
                if (gvr.Cells[29].Text == "&nbsp;")
                {
                    txtEmergencyContactNo.Text = "";
                }
                else
                {
                    txtEmergencyContactNo.Text = gvr.Cells[29].Text;
                }

            }
            catch (Exception ex)
            {
                txtEmergencyContactNo.Text = "";
            }



            try
            {
                if (gvr.Cells[30].Text == "&nbsp;")
                {
                    DrpMaritalStatus.SelectedIndex = 0;
                }
                else
                {
                    DrpMaritalStatus.SelectedValue = gvr.Cells[30].Text;
                }

            }
            catch (Exception ex)
            {
                DrpMaritalStatus.SelectedIndex = 0;
            }


            try
            {
                if (gvr.Cells[31].Text == "&nbsp;")
                {
                    DrpPaymentMode.SelectedIndex = 0;
                }
                else
                {
                    DrpPaymentMode.SelectedValue = gvr.Cells[31].Text;
                }

            }
            catch (Exception ex)
            {
                DrpPaymentMode.SelectedIndex = 0;
            }



            try
            {
                if (gvr.Cells[32].Text == "&nbsp;")
                {
                    txtBankName.Text = "";
                }
                else
                {
                    txtBankName.Text = gvr.Cells[32].Text;
                }

            }
            catch (Exception ex)
            {
                txtBankName.Text = "";
            }
            try
            {
                if (gvr.Cells[33].Text == "&nbsp;")
                {
                    txtAccountTitle.Text = "";
                }
                else
                {
                    txtAccountTitle.Text = gvr.Cells[33].Text;
                }

            }
            catch (Exception ex)
            {
                txtAccountTitle.Text = "";
            }

            try
            {
                if (gvr.Cells[34].Text == "&nbsp;")
                {
                    txtAccountNo.Text = "";
                }
                else
                {
                    txtAccountNo.Text = gvr.Cells[34].Text;
                }

            }
            catch (Exception ex)
            {
                txtAccountNo.Text = "";
            }


            try
            {
                if (gvr.Cells[35].Text == "&nbsp;")
                {
                    DrpSalaryChargedTo.SelectedIndex = 0;
                }
                else
                {
                    DrpSalaryChargedTo.SelectedValue = gvr.Cells[34].Text;
                }

            }
            catch (Exception ex)
            {
                DrpSalaryChargedTo.SelectedIndex = 0;
            }


            try
            {
                if (gvr.Cells[36].Text == "&nbsp;")
                {
                    DrpReportingToExmployee.SelectedIndex = 0;
                }
                else
                {
                    DrpReportingToExmployee.SelectedValue = gvr.Cells[36].Text;
                }

            }
            catch (Exception ex)
            {
                DrpReportingToExmployee.SelectedIndex = 0;
            }

            #endregion

            #region salary Structure
            int SalaryStructureID = 0;


            try
            {
                if (gvr.Cells[37].Text == "&nbsp;")
                {
                    SalaryStructureID = 0;
                }
                else
                {
                    SalaryStructureID = Int32.Parse(gvr.Cells[37].Text);
                }

            }
            catch (Exception ex)
            {
                SalaryStructureID = 0;
            }

            try
            {
                if (gvr.Cells[38].Text == "&nbsp;")
                {
                    ddlSalaryTemplate.SelectedIndex = 0;
                }
                else
                {
                    ddlSalaryTemplate.SelectedValue = gvr.Cells[38].Text;
                }

            }
            catch (Exception ex)
            {
                ddlSalaryTemplate.SelectedIndex = 0;
            }

            try
            {
                if (gvr.Cells[39].Text == "&nbsp;")
                {
                    ddlShift.SelectedIndex = 0;
                }
                else
                {
                    ddlShift.SelectedValue = gvr.Cells[39].Text;
                }

            }
            catch (Exception ex)
            {
                ddlShift.SelectedIndex = 0;
            }

            try
            {
                if (gvr.Cells[40].Text == "&nbsp;")
                {
                    ddlEmployeeType.SelectedIndex = 0;
                }
                else
                {
                    ddlEmployeeType.SelectedValue = gvr.Cells[40].Text;
                }

            }
            catch (Exception ex)
            {
                ddlEmployeeType.SelectedIndex = 0;
            }

            try
            {
                if (gvr.Cells[41].Text == "&nbsp;" || gvr.Cells[41].Text == "")
                {

                }
                else
                {
                    txtJoiningDate.Text = Convert.ToDateTime(gvr.Cells[41].Text).ToString("dd-MMM-yyyy");
                }

            }
            catch (Exception ex)
            {

            }

            try
            {
                if (gvr.Cells[42].Text == "&nbsp;" || gvr.Cells[42].Text == "")
                {

                }
                else
                {
                    txtProbationFrom.Text = Convert.ToDateTime(gvr.Cells[42].Text).ToString("dd-MMM-yyyy");
                }

            }
            catch (Exception ex)
            {

            }

            try
            {
                if (gvr.Cells[43].Text == "&nbsp;" || gvr.Cells[43].Text == "")
                {

                }
                else
                {
                    txtProbationTo.Text = Convert.ToDateTime(gvr.Cells[43].Text).ToString("dd-MMM-yyyy");
                }

            }
            catch (Exception ex)
            {

            }
            DataTable dt1 = ccc.SelectSalaryStructure(SalaryStructureID, null, Constants.IntNullValue, 2, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.IntNullValue);
            if (dt1 != null)
            {
                fillSalaryStructure(dt1);
            }
            hdnSalaryStructureID.Value = SalaryStructureID.ToString();

            try
            {
                if (dt1.Rows[0]["BasicPay"] != null)
                {
                    txtBasicSalary.Text = dt1.Rows[0]["BasicPay"].ToString();
                }

            }
            catch (Exception ex)
            {
                txtBasicSalary.Text = "";
            }

            #endregion

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}