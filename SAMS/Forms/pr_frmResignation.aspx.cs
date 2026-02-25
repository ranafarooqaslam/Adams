using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using System.Collections.Generic;
using SAMSDatabaseLayer.Classes;

public partial class pr_frmResignation : System.Web.UI.Page
{
    CompanyConfigrationController ccc = new CompanyConfigrationController();
    CompanyController cc = new CompanyController();
    EmployeeController ec = new EmployeeController();
    Loan_LeaseController llc = new Loan_LeaseController();
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
            if (SaveResignation() == 1)
            {
               // Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                Response.Redirect("pr_frmResignation.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
            
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('sum of installment should be equal to total amount!!');", true);
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
        Response.Redirect("pr_frmResignation.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
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
            
        txtResignDate.Attributes.Add("readonly", "readyonly");
        txtReason.Text = "";
        txtResignDate.Text = "";
        fillrptResignation();
        hdnResignationID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlAdvanceContent.Visible = true;
            pnlAdvanceGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("ResignationID");
        }
        else if (Session["Edit"] != null)
        {
            pnlAdvanceContent.Visible = true;
            pnlAdvanceGrid.Visible = false;

            DataTable dt = llc.SelectResignation(Int32.Parse(Session["CompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["ResignationID"].ToString()));
            hdnResignationID.Value = dt.Rows[0]["Resignation_ID"].ToString();
            ddlLocation.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            ddlEmployee.SelectedValue = dt.Rows[0]["Employee_ID"].ToString();
            txtReason.Text = dt.Rows[0]["RESIGNATION_REASON"].ToString();

            if (dt.Rows[0]["RESIGNATION_Date"].ToString().Length > 0)
            {
                txtResignDate.Text = Convert.ToDateTime(dt.Rows[0]["RESIGNATION_Date"]).ToString("dd-MMM-yyyy");
            }
            imgAdvance.ImageUrl = "../images/btn-update.png";

            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("ResignationID");
        }
        else
        {
            pnlAdvanceContent.Visible = false;
            pnlAdvanceGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("ResignationID");
        }
    }
    protected int SaveResignation()
    {
        try
        {
         
            ResignationModel llm = fillResignModel();
            if (hdnResignationID.Value == "" || hdnResignationID.Value == null)
            {
                
                llc.InsertResignation(llm);
            }
            else
            {
                llc.UpdateResignation(llm);
            }
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private ResignationModel fillResignModel()
    {
        ResignationModel llm = new ResignationModel();
        if (hdnResignationID.Value != "")
        {
            llm.Resignation_ID = Convert.ToInt32(hdnResignationID.Value);
        }

        llm.RESIGNATION_REASON = txtReason.Text;
        llm.RESIGNATION_Date = Convert.ToDateTime(txtResignDate.Text).Date;

        llm.COMPANY_ID = Convert.ToInt32(Session["CompanyId"].ToString());
        llm.DISTRIBUTOR_ID = Convert.ToInt32(ddlLocation.SelectedValue);
        
        llm.LASTUPDATE_DATE = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
        llm.Employee_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
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

        Response.Redirect("pr_frmResignation.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
   
    }
    protected void rptAdvance_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("ResignationID", e.CommandArgument.ToString());
        //    Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());

            Response.Redirect("pr_frmResignation.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
        
        }
        else if (e.CommandName == "delete")
        {
            ResignationModel llm = new ResignationModel();
            llm.Resignation_ID = Convert.ToInt32(e.CommandArgument);
            llm.RESIGNATION_REASON = null;
            llm.DISTRIBUTOR_ID = Constants.IntNullValue;
            llm.Employee_ID = Constants.IntNullValue;
            llm.RESIGNATION_Date = Constants.DateNullValue;
            llm.COMPANY_ID = Constants.IntNullValue;
            llm.TIME_STAMP = DateTime .Now ;
            llm.LASTUPDATE_DATE = Convert.ToDateTime(this.Session["CurrentWorkDate"]);
            llm.USER_ID = Convert.ToInt32(Session["UserID"]);
            llm.IS_DELETED = true;
            
            
            if (llc.UpdateResignation(llm))
            {
                fillrptResignation();
            }
        }
    }
    private void fillrptResignation()
    {
        DataTable dt = llc.SelectResignation(Int32.Parse(Session["CompanyID"].ToString()), Constants.IntNullValue,Constants .IntNullValue );
        rptAdvance.DataSource = dt;
        rptAdvance.DataBind();
    }
}