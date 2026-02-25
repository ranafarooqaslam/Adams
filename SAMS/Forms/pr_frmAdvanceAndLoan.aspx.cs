using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using MKB.TimePicker;
using System.Globalization;
using SAMSBusinessLayer.Models;
using System.Collections.Generic;

public partial class pr_frmAdvanceAndLoan : System.Web.UI.Page
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
        if (rptInstallment.Items.Count > 0)
        {
            try
            {
                if (SaveLoan() == 1)
                {
              //      Response.Redirect("pr_frmAdvanceAndLoan.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                    Response.Redirect("pr_frmAdvanceAndLoan.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
              
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
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('First Calculate the Installments');", true);
        }
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
       // Response.Redirect("pr_frmAdvanceAndLoan.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
        Response.Redirect("pr_frmAdvanceAndLoan.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
   
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
        txtDateFrom.Attributes.Add("readonly", "readonly");
        txtDateTo.Attributes.Add("readonly", "readonly");
        rptInstallment.DataSource = null;
        rptInstallment.DataBind();
        txtAmount.Text = "";
        txtApproveDate.Text = "";
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        fillrptAdvanceAndLoan(Constants.IntNullValue,1);//TypeID = 1 (for Loan)
        hdnAdvanceLoanID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlAdvanceAndLoanContent.Visible = true;
            pnlAdvanceAndLoanGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
        else if (Session["Edit"] != null)
        {
            pnlAdvanceAndLoanContent.Visible = true;
            pnlAdvanceAndLoanGrid.Visible = false;

            DataTable dt = llc.SelectLoan_Lease(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue, Convert.ToInt32(Session["LoanLeaseID"].ToString()),1);
            hdnAdvanceLoanID.Value = dt.Rows[0]["Loan_LeaseID"].ToString();
            ddlLocation.SelectedValue = dt.Rows[0]["EmployeeLocationID"].ToString();
            ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeID"].ToString();
            txtAmount.Text = String.Format("{0:0.00}", dt.Rows[0]["Amount"]);
            if (dt.Rows[0]["DateFrom"].ToString().Length > 0)
            {
                txtDateFrom.Text = Convert.ToDateTime(dt.Rows[0]["DateFrom"]).ToString("MMMM-yyyy");
            }
            if (dt.Rows[0]["DateTo"].ToString().Length > 0)
            {
                txtDateTo.Text = Convert.ToDateTime(dt.Rows[0]["DateTo"]).ToString("MMMM-yyyy");
            }
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
            imgAdvanceLoan.ImageUrl = "../images/btn-update.png";

            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
        else
        {
            pnlAdvanceAndLoanContent.Visible = false;
            pnlAdvanceAndLoanGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("LoanLeaseID");
        }
    }
    protected int SaveLoan()
    {
        try
        {
            decimal amount = 0;
            foreach (RepeaterItem item in rptInstallment.Items)
            {
                TextBox txtamountTobePaid = (TextBox)item.FindControl("txtamountTobePaid");
                decimal currentamount = Convert.ToDecimal(txtamountTobePaid.Text);
                amount += currentamount;
            }
            amount =amount-Convert.ToDecimal(txtAmount.Text);
            if (amount<1)
            {
                DataTable dt = CreatedtInstallment();
                foreach (RepeaterItem item in rptInstallment.Items)
                {
                    TextBox txtamountTobePaid = (TextBox)item.FindControl("txtamountTobePaid");
                    TextBox txtInstallmentDate = (TextBox)item.FindControl("txtInstallmentDate");
                    TextBox txtAmountPaid = (TextBox)item.FindControl("txtAmountPaid");
                    DataRow dr = dt.NewRow();
                    dr["AmountToBePaid"] = decimal.Parse(txtamountTobePaid.Text);
                    dr["InstallmentDate"] = DateTime.Parse(txtInstallmentDate.Text);
                    dr["AmountPaid"] = decimal.Parse(txtAmountPaid.Text);
                    dt.Rows.Add(dr);
                }
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
                if (hdnAdvanceLoanID.Value == "" || hdnAdvanceLoanID.Value == null)
                {
                    llc.InsertLoan_Lease(llm, dt, dtManagers);
                }
                else
                {
                    llc.UpdateLoan_Lease(llm, dt, dtManagers);
                }
                return 1;
            }
            else
            {
                return 0;
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private Loan_LeaseModel fillLoanModel()
    {
        Loan_LeaseModel llm = new Loan_LeaseModel();
        if (hdnAdvanceLoanID.Value != "")
        {
            llm.Loan_LeaseID = Convert.ToInt32(hdnAdvanceLoanID.Value);
        }

        llm.Amount = Convert.ToDecimal(txtAmount.Text);
        llm.ApprovedDate = Convert.ToDateTime(txtApproveDate.Text).Date;
        llm.ApprovelID = Constants.IntNullValue;
        llm.Comments = null;
        llm.CompanyID = Convert.ToInt32(Session["WorkingCompanyID"].ToString());
        llm.DateFrom = Convert.ToDateTime(txtDateFrom.Text).Date;
        llm.DateTo = Convert.ToDateTime(txtDateTo.Text).Date;
        llm.Document_Date = Convert.ToDateTime(Session["CurrentWorkDate"].ToString());
        llm.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
        llm.Loan_Lease_TypeID = 1;
        int noofmonth = ((Convert.ToDateTime(txtDateTo.Text).Year - Convert.ToDateTime(txtDateFrom.Text).Year) * 12) + Convert.ToDateTime(txtDateTo.Text).Month - Convert.ToDateTime(txtDateFrom.Text).Month;
        llm.NoOfMonth = noofmonth;
        llm.TypeID = 1;
        llm.User_ID = Convert.ToInt32(Session["UserID"]);
        return llm;
    }
    protected void btnCalculateInstallments_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dateFrom = Convert.ToDateTime(txtDateFrom.Text).Date;
            DateTime dateTo = Convert.ToDateTime(txtDateTo.Text).Date;
            if (dateTo >= dateFrom && decimal.Parse(txtAmount.Text) > 0)
            {
                int year = dateFrom.Year;
                int noofmonth = ((dateTo.Year - dateFrom.Year) * 12) + dateTo.Month - dateFrom.Month;
                noofmonth = noofmonth + 1;
                decimal amount = decimal.Parse(txtAmount.Text) / noofmonth;
                amount = Math.Round(amount, 2);
                List<string> months = MonthsBetween(dateFrom, dateTo);
                DataTable dt = CreatedtInstallment();
                for (int i = 0; i < noofmonth; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["AmountToBePaid"] = amount.ToString();
                    dr["AmountPaid"] = 0;
                    dr["InstallmentDate"] = months[i];
                    dt.Rows.Add(dr);
                }
                rptInstallment.DataSource = dt;
                rptInstallment.DataBind();
            }
            else
            {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Wrong values please check values');", true); 
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Wrong values please check values');", true); 
        }
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
    protected void btnShowpnAdvanceAndLoanContent_Click(object sender, EventArgs e)
    {
        Session.Add("Add", "Add");
       // Response.Redirect("pr_frmAdvanceAndLoan.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());

        Response.Redirect("pr_frmAdvanceAndLoan.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
   
    }
    protected void rptAdvanceAndLoan_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("LoanLeaseID", e.CommandArgument.ToString());
           // Response.Redirect("pr_frmAdvanceAndLoan.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
            Response.Redirect("pr_frmAdvanceAndLoan.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
       
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
            llm.IS_ACTIVE = false;
            llm.User_ID = Convert.ToInt32(Session["UserID"]);
            if (llc.UpdateLoan_Lease(llm))
            {
                fillrptAdvanceAndLoan(Constants.IntNullValue, 1);//TypeID = 1 (for Loan)
            }
        }
    }
    private void fillrptAdvanceAndLoan(int LoanLeaseTypeID, int TypeID)
    {
        DataTable dt = llc.SelectLoan_Lease(Int32.Parse(Session["WorkingCompanyID"].ToString()), LoanLeaseTypeID, Constants.IntNullValue,TypeID);
        rptAdvanceAndLoan.DataSource = dt;
        rptAdvanceAndLoan.DataBind();
    }
}