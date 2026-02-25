using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;

/// <summary>
/// Form For Account Head Assignment
/// </summary>
public partial class Forms_frmAccountHeadAssignment : System.Web.UI.Page
{
    AccountHeadController MController = new AccountHeadController();

    /// <summary>
    /// Page_Load Function Populates All Combos And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadAccountHead();
            this.LoadAccountHeadDetail();
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Loads AccountHeads And Checks/UnChecks ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>    
    protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountHeadDetail();
        this.LoadGrid();
    }

    private void LoadGrid()
    {
        if (ddlForm.SelectedValue != "2")
        {
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("Field", typeof(string));

            DataRow dr = dtGrid.NewRow();
            if (ddlForm.SelectedValue == "0")
            {
                dr["Field"] = "Gross Amount (Credit)";
            }
            else if (ddlForm.SelectedValue == "1")
            {
                dr["Field"] = "Gross Amount (Debit)";
            }
            dtGrid.Rows.Add(dr);

            DataRow dr2 = dtGrid.NewRow();
            if (ddlForm.SelectedValue == "0")
            {
                dr2["Field"] = "Discount Amount (Debit)";
            }
            else if (ddlForm.SelectedValue == "1")
            {
                dr2["Field"] = "Discount Amount (Credit)";
            }
            dtGrid.Rows.Add(dr2);

            DataRow dr3 = dtGrid.NewRow();
            if (ddlForm.SelectedValue == "0")
            {
                dr3["Field"] = "GST Amount (Credit)";
            }
            else if (ddlForm.SelectedValue == "1")
            {
                dr3["Field"] = "GST Amount (Debit)";
            }
            dtGrid.Rows.Add(dr3);

            gvAccountHead.DataSource = dtGrid;
            gvAccountHead.DataBind();
        }
        else
        {
            gvAccountHead.DataSource = null;
            gvAccountHead.DataBind();
        }
    }

    protected void gvAccountHead_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlAccountHeadGrd = (DropDownList)e.Row.FindControl("ddlAccountHead");
            CheckBox cbField = (CheckBox)e.Row.FindControl("cbField");

            DataTable dt = (DataTable)Session["dtAccount"];
            clsWebFormUtil.FillDropDownList(ddlAccountHeadGrd, dt, 0, 4, true);

            string strScript = "SelectDeSelectHeader(" + ((CheckBox)e.Row.Cells[0].FindControl("cbField")).ClientID + ");";
            ((CheckBox)e.Row.Cells[0].FindControl("cbField")).Attributes.Add("onclick", strScript);

            DataTable dtAccountDetail = (DataTable)Session["dtAccountDetail"];
            if (dtAccountDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAccountDetail.Rows)
                {
                    if (dr["FIELD_ID"].ToString() == "0" && e.Row.Cells[1].Text.Contains("Gross Amount"))
                    {
                        cbField.Checked = true;
                        ddlAccountHeadGrd.SelectedValue = dr["ACCOUNT_HEAD_ID2"].ToString();
                    }

                    if (dr["FIELD_ID"].ToString() == "1" && e.Row.Cells[1].Text.Contains("Discount Amount"))
                    {
                        cbField.Checked = true;
                        ddlAccountHeadGrd.SelectedValue = dr["ACCOUNT_HEAD_ID2"].ToString();
                    }

                    if (dr["FIELD_ID"].ToString() == "2" && e.Row.Cells[1].Text.Contains("GST Amount"))
                    {
                        cbField.Checked = true;
                        ddlAccountHeadGrd.SelectedValue = dr["ACCOUNT_HEAD_ID2"].ToString();
                    }
                }
            }
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        DataTable dtDetail = new DataTable();
        dtDetail.Columns.Add("FIELD_ID", typeof(int));
        dtDetail.Columns.Add("ACCOUNT_HEAD_ID", typeof(int));

        foreach (GridViewRow gvr in gvAccountHead.Rows)
        {
            CheckBox cbField = (CheckBox)gvr.FindControl("cbField");
            if (cbField.Checked)
            {
                DropDownList ddlAccountHeadGrd = (DropDownList)gvr.FindControl("ddlAccountHead");

                DataRow dr = dtDetail.NewRow();
                if (gvr.Cells[1].Text.Contains("Gross Amount"))
                {
                    dr["FIELD_ID"] = 0;
                }
                else if (gvr.Cells[1].Text.Contains("Discount Amount"))
                {
                    dr["FIELD_ID"] = 1;
                }
                else if (gvr.Cells[1].Text.Contains("GST Amount"))
                {
                    dr["FIELD_ID"] = 2;
                }

                dr["ACCOUNT_HEAD_ID"] = Convert.ToInt32(ddlAccountHeadGrd.SelectedValue);
                dtDetail.Rows.Add(dr);
            }
        }

        if (MController.SaveAccountHeadAssignment(Convert.ToInt32(this.ddlAccountHead.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlForm.SelectedValue), Convert.ToInt32(Session["UserID"]), dtDetail))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Account assignment saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some erro occurred.');", true);
        }
    }

    private void LoadAccountHeadDetail()
    {
        DataTable dtAccountDetail = new DataTable();
        dtAccountDetail = MController.GetAccountHeadAssignmentDetail(Convert.ToInt32(ddlForm.SelectedValue));
        if (dtAccountDetail.Rows.Count > 0)
        {
            ddlAccountHead.SelectedValue = dtAccountDetail.Rows[0]["ACCOUNT_HEAD_ID"].ToString();
        }
        Session.Add("dtAccountDetail", dtAccountDetail);
    }

    private void LoadAccountHead()
    {
        DataTable dtAccount = MController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtAccount, 0, 4, true);
        Session.Add("dtAccount", dtAccount);
    }
}
