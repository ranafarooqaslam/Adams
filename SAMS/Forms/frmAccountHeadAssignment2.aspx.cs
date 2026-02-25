using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;

public partial class Forms_frmAccountHeadAssignment2 : System.Web.UI.Page
{
    AccountHeadController MController = new AccountHeadController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadChannelType();
            this.LoadAccountHead();
            this.LoadGrid();
        }
    }
    
    #region LOad

    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(DrpChannelType, dt, 0, 2, true);
    }
    private void LoadGrid()

    {
        DataTable dt= MController.SelectAssignAccountHead(Constants.IntNullValue, 1);
        GrdAccount.DataSource = dt;
        GrdAccount.DataBind();
        this.Session.Add("dt", dt);
    }
    private void LoadAccountHead()
    {

        SAMSCommon.Classes.Configuration.GetAccountHead();

        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId,Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpCreditHead, dt, 0, 4, true);
        clsWebFormUtil.FillDropDownList(DrpCashHead, dt, 0, 4, true);

    }

    #endregion

    #region Grid Operations
    protected void GrdAccount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        hf_ID.Value = GrdAccount.Rows[NewEditIndex].Cells[0].Text;
        DrpChannelType.SelectedValue = GrdAccount.Rows[NewEditIndex].Cells[1].Text;
        DrpCreditHead.SelectedValue = GrdAccount.Rows[NewEditIndex].Cells[2].Text;
        DrpCashHead.SelectedValue = GrdAccount.Rows[NewEditIndex].Cells[3].Text;
        DrpChannelType.Enabled = false;
        btnSave.Text = "Update";
    }

    protected void GrdAccount_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
       
            MController.SelectAssignAccountHead(int.Parse(GrdAccount.Rows[e.RowIndex].Cells[0].Text), 2);
            this.LoadGrid();
            ClearAll();
           //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Transaction exists unable to delete');", true);
        
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hf_ID.Value == "")
        {
            hf_ID.Value = Convert.ToString(Constants.IntNullValue);
        }
        bool flag = MController.SaveAccountAssignment(int.Parse(hf_ID.Value), int.Parse(this.DrpChannelType.SelectedValue),
            int.Parse(this.DrpCashHead.SelectedValue), int.Parse(this.DrpCreditHead.SelectedValue),
            DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(Session["UserID"].ToString()));

        if (flag)
        {
            ClearAll();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Account assignment saved successfully.');", true);
            this.LoadGrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occurred.');", true);
        }
    }

    private void ClearAll()
    {
        DrpChannelType.SelectedIndex = 0;
        DrpCreditHead.SelectedIndex = 0;
        DrpCashHead.SelectedIndex = 0;

        DrpChannelType.Enabled = true;
        btnSave.Text = "Save";
    }
}