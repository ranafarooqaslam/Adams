using System;
using System.Data;
using System.Web.UI;

using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmDistributorMapping : System.Web.UI.Page
{
    readonly UserController mUserController = new UserController();

    private void LoadLocation()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        DataTable distinctChild = dt.AsEnumerable().Where(r => r.Field<int>("SUBZONE_ID") == 2 || r.Field<int>("SUBZONE_ID") == 3).CopyToDataTable();
        clsWebFormUtil.FillDropDownList(this.ddlLocation, distinctChild, 0, 2, true);
    }
    private void DistributorType()
    {
        try
        {
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectDistributorAssignment(int.Parse(ddlLocation.SelectedValue), 6, 3, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.ddDistributorType, dt, 0, 1, true);
        }
        catch (Exception)
        {
            
        }

    }
    protected void GrdDistributor_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dt = mUserController.SelectDistributorMapping(4, int.Parse(ddDistributorType.SelectedValue.ToString()), long.Parse(ddCustomer.SelectedValue));
        if (dt.Rows.Count == 0)
        {
            ddDistributorType.SelectedValue = GrdDistributor.Rows[e.NewEditIndex].Cells[0].Text;
            ddCustomer.SelectedValue = GrdDistributor.Rows[e.NewEditIndex].Cells[3].Text;
            btnSave.Text = "Update";

            ddDistributorType.Enabled = false;
        }
        if (dt.Rows.Count > 0)
        {
            lblmsg.Text = "Transaction exist. Select another record.";
        }
    }

    /// <summary>
    /// Deletes Account MainType
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdDistributor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //DataTable dt = mUserController.SelectDistributorMapping(4, int.Parse(ddDistributorType.SelectedValue.ToString()), long.Parse(ddCustomer.SelectedValue));
        //if (dt.Rows.Count == 0)
        //{
            if(mUserController.DistributorMapping(5, int.Parse(GrdDistributor.Rows[e.RowIndex].Cells[0].Text),
                long.Parse(GrdDistributor.Rows[e.RowIndex].Cells[3].Text), int.Parse(ddlLocation.SelectedValue)) > 0)
            {
                lblmsg.Text = string.Empty;
                LoadGrid();
            }
        //}
        //if (dt.Rows.Count > 0)
        //{
        //    lblmsg.Text = "Transaction exist. Can not delete.";
        //}        
    }

    private void LoadGrid()
    {
        try
        {
            DataTable dt = mUserController.SelectDistributorMapping(3, int.Parse(ddlLocation.SelectedValue.ToString()), long.Parse(ddCustomer.SelectedValue));
            GrdDistributor.DataSource = dt;
            GrdDistributor.DataBind();
        }
        catch (Exception ex)
        {
            
        }        
    }

    private void LoadCustomer()
    {
            ddCustomer.Items.Clear();        
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(Convert.ToInt32(ddlLocation.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(ddCustomer, dt, 0, 4);      
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadLocation();
            this.DistributorType();
            this.LoadCustomer();
            this.LoadGrid();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddDistributorType.Items.Count > 0 && ddCustomer.Items.Count > 0)
            {
                int message = 0;
                if (btnSave.Text == "Save")
                {
                    message = mUserController.DistributorMapping(1, int.Parse(ddDistributorType.SelectedValue.ToString()), 
                        long.Parse(ddCustomer.SelectedValue), int.Parse(ddlLocation.SelectedValue));

                    if (message == 1)
                    {
                        lblmsg.Text = "Customer already exist against another location";
                        return;
                    }
                }
                else
                {
                    message = mUserController.DistributorMapping(2, int.Parse(ddDistributorType.SelectedValue.ToString()),
                        long.Parse(ddCustomer.SelectedValue), int.Parse(ddlLocation.SelectedValue));
                }
                lblmsg.Text = "";
                LoadGrid();
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DistributorType();
        this.LoadCustomer();
        btnSave.Text = "Save";
        LoadGrid();
    }

    protected void ddDistributorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}