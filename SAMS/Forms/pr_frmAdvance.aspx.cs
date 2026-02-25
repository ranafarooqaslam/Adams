using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using System.Collections.Generic;

public partial class pr_frmAdvance : System.Web.UI.Page
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
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            if (SaveAdvance() == 1)
            {
               // Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
            
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
        Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    protected void fillddlEmployee(int LocationID)
    {
        //DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue,LocationID,false);
        //ddlEmployee.Items.Clear();
        //this.ddlEmployee.Items.Add(new ListItem("--Select--", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(ddlEmployee, dt,0, 1, false);


        Distributor_UserController du = new Distributor_UserController();
        DataTable dt = du.SelectDistributorUser(Constants.IntNullValue, LocationID, int.Parse(this.Session["CompanyId"].ToString()));

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
    private void fillmcbManagers()
    {
        DataTable dt = ec.SelectEmployee(Constants.LongNullValue, Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, false);
        mcbManagers.ClearAll();
        mcbManagers.AddItems(dt, 0, 1);
    }
    private void ResetForm()
    {
        fillddlEmployee(Constants.IntNullValue);
        fillddlLocation();
        fillmcbManagers();        
        txtApproveDate.Attributes.Add("readonly", "readyonly");
        txtAmount.Text = "";
        txtApproveDate.Text = "";
        fillrptAdvance(3);//TypeID = 3 (for Advance)
        hdnAdvanceID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlAdvanceContent.Visible = true;
            pnlAdvanceGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
        else if (Session["Edit"] != null)
        {
            pnlAdvanceContent.Visible = true;
            pnlAdvanceGrid.Visible = false;

            DataTable dt = llc.SelectLoan_Lease(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["LoanLeaseID"].ToString()),3);
            hdnAdvanceID.Value = dt.Rows[0]["Loan_LeaseID"].ToString();
            ddlLocation.SelectedValue = dt.Rows[0]["EmployeeLocationID"].ToString();
            ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeID"].ToString();            
            txtAmount.Text = String.Format("{0:0.00}", dt.Rows[0]["Amount"]);            
            mcbManagers.Text = dt.Rows[0]["ApprovedBy"].ToString();

            string[] mangertext = dt.Rows[0]["ApprovedBy"].ToString().Split(',');

            for (int i = 0; i < mangertext.Length; i++)
            {
                mcbManagers.selectItem(mangertext[i].Trim());
            }

            if (dt.Rows[0]["ApprovedDate"].ToString().Length > 0)
            {
                txtApproveDate.Text = Convert.ToDateTime(dt.Rows[0]["ApprovedDate"]).ToString("dd-MMM-yyyy");
            }
            imgAdvance.ImageUrl = "../images/btn-update.png";

            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
        else
        {
            pnlAdvanceContent.Visible = false;
            pnlAdvanceGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
    }
    protected int SaveAdvance()
    {
        try
        {
            DataTable dt = CreatedtInstallment();
            DataRow drInst = dt.NewRow();
            drInst["AmountToBePaid"] = decimal.Parse(txtAmount.Text);
            drInst["InstallmentDate"] = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            drInst["AmountPaid"] = 0;
            dt.Rows.Add(drInst);

            List<ListItem> managers = mcbManagers.getSelectedItems();
            DataTable dtManagers = CreatedtManagers();
            foreach (ListItem manager in managers)
            {
                DataRow dr = dtManagers.NewRow();
                dr["ManagerID"] = manager.Value;
                dr["LoanID"] = 0;
                dtManagers.Rows.Add(dr);
            }

            Loan_LeaseModel llm = fillLoanModel();
            if (hdnAdvanceID.Value == "" || hdnAdvanceID.Value == null)
            {
                llc.InsertLoan_Lease(llm, dt, dtManagers);
            }
            else
            {
                llc.UpdateLoan_Lease(llm, dt, dtManagers);
            }
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private Loan_LeaseModel fillLoanModel()
    {
        Loan_LeaseModel llm = new Loan_LeaseModel();
        if (hdnAdvanceID.Value != "")
        {
            llm.Loan_LeaseID = Convert.ToInt32(hdnAdvanceID.Value);
        }

        llm.Amount = Convert.ToDecimal(txtAmount.Text);
        llm.ApprovedDate = Convert.ToDateTime(txtApproveDate.Text).Date;
        llm.ApprovelID = Constants.IntNullValue;
        llm.Comments = null;
        llm.CompanyID = Convert.ToInt32(Session["WorkingCompanyID"].ToString());
        llm.DateFrom = Constants.DateNullValue;
        llm.DateTo = Constants.DateNullValue;
        llm.Document_Date = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
        llm.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
        llm.Loan_Lease_TypeID = 0;
        int noofmonth = 0;
        llm.NoOfMonth = noofmonth;
        llm.TypeID = 3;//Advance
        llm.IS_DELETED = false;
        llm.IS_ACTIVE = true;
        llm.User_ID = Convert.ToInt32(Session["UserID"]);
        return llm;
    }    
    private DataTable CreatedtInstallment()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("AmountToBePaid", typeof(decimal));
        dt.Columns.Add("AmountPaid", typeof(decimal));
        dt.Columns.Add("InstallmentDate", typeof(DateTime));
        return dt;
    }
    private DataTable CreatedtManagers()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ManagerID", typeof(int));
        dt.Columns.Add("LoanID", typeof(int));
        return dt;
    }
    public List<string> MonthsBetween(DateTime startDate, DateTime endDate)
    {
        List<string> lstDates = new List<string>();
        if (endDate > startDate)
        {
            while (startDate <= endDate)
            {
                lstDates.Add(startDate.ToShortDateString());
                startDate = startDate.AddMonths(1);
            }
        }
        else if (endDate == startDate)
        {
            lstDates.Add(startDate.ToShortDateString());
        }
        return lstDates;
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlEmployee(Int32.Parse(ddlLocation.SelectedValue));
    }
    protected void btnShowpnAdvanceContent_Click(object sender, EventArgs e)
    {
        Session.Add("Add", "Add");
     //   Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());

        Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
   
    }
    protected void rptAdvance_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("LoanLeaseID", e.CommandArgument.ToString());
        //    Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());

            Response.Redirect("pr_frmAdvance.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
        
        }
        else if (e.CommandName == "delete")
        {
            Loan_LeaseModel llm = new Loan_LeaseModel();
            llm.Loan_LeaseID = Convert.ToInt32(e.CommandArgument);
            llm.Amount = Constants.DecimalNullValue;
            llm.CompanyContributation = Constants.DecimalNullValue;
            llm.EmployeeContributation = Constants.DecimalNullValue;
            llm.ApprovedDate = Constants.DateNullValue;
            llm.ApprovelID = Constants.IntNullValue;
            llm.Comments = null;
            llm.CompanyID = Constants.IntNullValue;
            llm.DateFrom = Constants.DateNullValue;
            llm.DateTo = Constants.DateNullValue;
            llm.Document_Date = Constants.DateNullValue;
            llm.EmployeeID = Constants.IntNullValue;
            llm.Loan_Lease_TypeID = Constants.IntNullValue;
            llm.AssetID = Constants.IntNullValue;
            llm.NoOfMonth = Constants.IntNullValue;
            llm.TypeID = Constants.IntNullValue;
            llm.IS_DELETED = true;
            llm.IS_ACTIVE = true;
            llm.User_ID = Convert.ToInt32(Session["UserID"]);
            if (llc.UpdateLoan_Lease(llm))
            {
                fillrptAdvance(3);//TypeID = 3 (for Advance)
            }
        }
    }
    private void fillrptAdvance(int TypeID)
    {
        DataTable dt = llc.SelectLoan_Lease(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Constants.IntNullValue,TypeID);
        rptAdvance.DataSource = dt;
        rptAdvance.DataBind();
    }
}