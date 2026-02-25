using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using System.Collections.Generic;
using SAMSDatabaseLayer.Classes;

public partial class pr_frmSalaryIncrement : System.Web.UI.Page
{

    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    Loan_LeaseController llc = new Loan_LeaseController();
    DataControl dc = new DataControl();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetForm();
        }
    }
    protected void fillddlDepartment()
    {
        DataTable dt = ccc.SELECT_ParentDepartments(Int32.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add("--All--");
        clsWebFormUtil.FillDropDownList(ddlDepartment, dt, 1, 2, false);
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
        fillddlEmployee(LocationID,DepartmentID);
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            if (SaveIncrement() == 1)
            {
               // Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                Response.Redirect("pr_frmSalaryIncrement.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
            
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Please Try Again!');", true);
                return;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('" + ex.Message + "');", true);
        }
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        //Response.Redirect("pr_frmResignation.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
        Response.Redirect("pr_frmSalaryIncrement.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
    }
    protected void fillddlEmployee(int LocationID, int DepartmentID)
    {
        //DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue,LocationID,false);
        //ddlEmployee.Items.Clear();
        //this.ddlEmployee.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(ddlEmployee, dt,0, 1, false);


        Distributor_UserController du = new Distributor_UserController();
        DataTable dt = du.SelectDistributorUser(Constants.IntNullValue, LocationID, int.Parse(this.Session["CompanyId"].ToString()), DepartmentID);

        ddlEmployee.Items.Clear();
       this.ddlEmployee.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlEmployee, dt, 0, 6, false);



    }
    private void fillddlLocation()
    {
        //DataTable dt = cc.SelectCompany_lOCATIONS(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue);
        //ddlLocation.Items.Clear();
        //this.ddlLocation.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, false);




        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        ddlLocation.Items.Clear(); 
        this.ddlLocation.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, false);
        

    }    
   
    private void ResetForm()
    {
        fillddlDepartment();
        fillddlEmployee(Constants.IntNullValue,Constants .IntNullValue );
        fillddlLocation();
            
        txtIncrementDate.Attributes.Add("readonly", "readyonly");
        txtApplicableDate.Attributes.Add("readonly", "readyonly");
        txtRemarks.Text = "";
        txtIncrementDate.Text = "";
        txtApplicableDate.Text = "";
        fillrptIncrement();
        hdnIncrementID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlAdvanceContent.Visible = true;
            pnlAdvanceGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("IncrementID");
        }
        else if (Session["Edit"] != null)
        {
            pnlAdvanceContent.Visible = true;
            pnlAdvanceGrid.Visible = false;

            DataTable dt = llc.SelectSalary_Increment(Int32.Parse(Session["CompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["IncrementID"].ToString()),Constants .DateNullValue );
            hdnIncrementID.Value = dt.Rows[0]["INCREMENT_ID"].ToString();
            ddlLocation.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            ddlEmployee.SelectedValue = dt.Rows[0]["Employee_ID"].ToString();
            txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            txtPreviousSalary .Text = dt.Rows[0]["PREVIOUS_SALARY"].ToString();
            txtIncrement .Text=  dt.Rows[0]["INCREMENT_AMOUNT"].ToString();
            txtNewSalary .Text = dt.Rows[0]["NEW_SALARY"].ToString();
            try{
                 ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();
            }
            catch{
                ddlDepartment.SelectedIndex =0;
            }


            if (dt.Rows[0]["INCREMENT_DATE"].ToString().Length > 0)
            {
                txtIncrementDate.Text = Convert.ToDateTime(dt.Rows[0]["INCREMENT_DATE"]).ToString("dd-MMM-yyyy");
            }

            
            if (dt.Rows[0]["APPLICABLE_DATE"].ToString().Length > 0)
            {
                txtApplicableDate.Text = Convert.ToDateTime(dt.Rows[0]["APPLICABLE_DATE"]).ToString("dd-MMM-yyyy");
            }
            imgAdvance.ImageUrl = "../images/btn-update.png";

            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("IncrementID");
        }
        else
        {
            pnlAdvanceContent.Visible = false;
            pnlAdvanceGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("IncrementID");
        }
    }
    protected int SaveIncrement()
    {
        try
        {

            SALARY_INCREMENTModel llm = fillIncrementModel();
            if (hdnIncrementID.Value == "" || hdnIncrementID.Value == null)
            {

                llc.InsertSalaryIncrement(llm);
            }
            else
            {
                llc.UpdateSalaryIncrement(llm);
            }
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private SALARY_INCREMENTModel fillIncrementModel()
    {
        SALARY_INCREMENTModel llm = new SALARY_INCREMENTModel();
        if (hdnIncrementID.Value != "")
        {
            llm.INCREMENT_ID = Convert.ToInt32(hdnIncrementID.Value);
        }

        llm.REMARKS = txtRemarks.Text;
        llm.INCREMENT_DATE = Convert.ToDateTime(txtIncrementDate.Text);
        llm.APPLICABLE_DATE = Convert.ToDateTime(txtApplicableDate.Text);
        llm.COMPANY_ID = Convert.ToInt32(Session["CompanyId"].ToString());
        llm.DISTRIBUTOR_ID = Convert.ToInt32(ddlLocation.SelectedValue);
        llm.DOCUMENT_DATE = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
        llm.LASTUPDATE_DATE = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
        llm.Employee_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
        llm.PREVIOUS_SALARY = Convert.ToDecimal(dc.chkNull_0(txtPreviousSalary .Text));
        llm.INCREMENT_AMOUNT = Convert.ToDecimal(dc.chkNull_0(txtIncrement.Text));
        llm.NEW_SALARY = Convert.ToDecimal(dc.chkNull_0(txtNewSalary.Text));
        llm.IS_DELETED = false;
        llm.USER_ID = Convert.ToInt32(Session["UserID"]);
        return llm;
    }    

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
       // fillddlEmployee(Int32.Parse(ddlLocation.SelectedValue));


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
        fillddlEmployee(LocationID,DepartmentID);
        
    }
    protected void btnShowpnAdvanceContent_Click(object sender, EventArgs e)
    {
        Session.Add("Add", "Add");
     //   Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());

        Response.Redirect("pr_frmSalaryIncrement.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
   
    }
    protected void rptAdvance_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("IncrementID", e.CommandArgument.ToString());
        //    Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());

            Response.Redirect("pr_frmSalaryIncrement.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
        
        }
        else if (e.CommandName == "delete")
        {
            SALARY_INCREMENTModel llm = new SALARY_INCREMENTModel();
            llm.INCREMENT_ID = Convert.ToInt32(e.CommandArgument);
            llm.REMARKS = null;
            llm.DISTRIBUTOR_ID = Constants.IntNullValue;
            llm.Employee_ID = Constants.IntNullValue;
            llm.PREVIOUS_SALARY = Constants.DecimalNullValue;
            llm.INCREMENT_AMOUNT = Constants.DecimalNullValue;
            llm.NEW_SALARY = Constants.DecimalNullValue;
            llm.INCREMENT_DATE = Constants.DateNullValue; 
            llm.APPLICABLE_DATE= Constants.DateNullValue;
            llm.COMPANY_ID = Constants.IntNullValue;
            llm.DOCUMENT_DATE = Constants.DateNullValue;
            
            llm.LASTUPDATE_DATE = Convert.ToDateTime(this.Session["CurrentWorkDate"]);
            llm.USER_ID = Convert.ToInt32(Session["UserID"]);
            llm.IS_DELETED = true;
            
            
            if (llc.UpdateSalaryIncrement(llm))
            {
                fillrptIncrement();
            }
        }
    }
    private void fillrptIncrement()
    {
        DataTable dt = llc.SelectSalary_Increment(Int32.Parse(Session["CompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue,Constants .DateNullValue);

        if (dt != null)
        {
            rptAdvance.DataSource = dt;
            rptAdvance.DataBind();
        }
    }
}