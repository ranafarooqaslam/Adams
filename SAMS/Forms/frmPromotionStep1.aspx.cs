using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit, Promotions
/// </summary>
public partial class Forms_frmPromotionStep1 : System.Web.UI.Page
{
    string[] cols = { "SCHEME_ID", "PROMOTION_ID", "SCHEME_DESC", "DISTRIBUTOR_ID", "PROMOTION_CODE", "PROMOTION_DESCRIPTION", "Principal", "START_DATE", "END_DATE", "IS_ACTIVE" };

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtFromdate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            txttoDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");

            this.LoadPrincipal();
            btnPromotion.Attributes.Add("onclick", "return ValidateForm()");
        }
    }
    
    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Promotions To Promotion Grid
    /// </summary>
    private void LoadGrid()
    {
        DataTable dt = (DataTable)this.Session["dt"];

        if (ddSearchType.SelectedIndex == 2)
        {
            dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " =" + txtSeach.Text;
        }
        else
        {
            dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        }

        DataView dw = dt.DefaultView;
        DataTable dt2 = dw.ToTable(true, cols);

        Grid_pricedetails.DataSource = dt2;
        Grid_pricedetails.DataBind();
    }
        
    /// <summary>
    /// Gets Promotions From Datatabse And Loads To Promotion Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPromotion_Click(object sender, EventArgs e)
    {
        lblExtend.Visible = false;
        txtExtendDate.Visible = false;
        imgExtendDate.Visible = false;
        btnExtend.Visible = false;
        PromotionController mController = new PromotionController();
        if (btnPromotion.Text == "Get Promotion")
        {
            string FromDate = txtFromdate.Text + " 00:00:00";
            string ToDate = txttoDate.Text + " 23:59:59";

            DataTable dt = mController.SelectPromotion(FromDate, ToDate, Convert.ToInt32(this.DrpPrincipal.SelectedValue), int.Parse(this.Session["UserId"].ToString()), ChbActive.Checked);
            this.Session.Add("dt", dt);

            DataView dw = dt.DefaultView;
            DataTable dt2 = dw.ToTable(true, cols);

            Grid_pricedetails.DataSource = dt2;
            Grid_pricedetails.DataBind();

            if (Grid_pricedetails.Rows.Count > 0)
            {
                lblExtend.Visible = true;
                txtExtendDate.Visible = true;
                imgExtendDate.Visible = true;
                btnExtend.Visible = true;
            }
        }
    }

    /// <summary>
    /// Redirects To Promotion Wizard Form.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.Session.Add("IsEdit", false);
        Response.Redirect("frmPromotionStep2.aspx?LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString(), true);
    }

    /// <summary>
    /// Filters Promotion Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (txtSeach.Text.Length > 0)
        {
            LoadGrid();
        }
    }

    protected void btnExtend_Click(object sender, EventArgs e)
    {        
        if (txtExtendDate.Text.Trim().Length > 0)
        {
            bool flag = false;
            PromotionController mController = new PromotionController();
            if (Grid_pricedetails.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in Grid_pricedetails.Rows)
                {
                    CheckBox cbExtend = (CheckBox)gvr.FindControl("cbExtend");
                    if (cbExtend.Checked)
                    {
                        flag = mController.ExtendPromotion(Convert.ToDateTime(txtExtendDate.Text), Convert.ToInt32(gvr.Cells[1].Text), Convert.ToInt32(Session["UserID"]));
                    }
                }
                lblExtend.Visible = false;
                txtExtendDate.Visible = false;
                imgExtendDate.Visible = false;
                btnExtend.Visible = false;
                if (btnPromotion.Text == "Get Promotion")
                {
                    string FromDate = txtFromdate.Text + " 00:00:00";
                    string ToDate = txttoDate.Text + " 23:59:59";
                    DataTable dt = mController.SelectPromotion(FromDate, ToDate, Convert.ToInt32(this.DrpPrincipal.SelectedValue), int.Parse(this.Session["UserId"].ToString()), ChbActive.Checked);
                    this.Session.Add("dt", dt);
                    DataView dw = dt.DefaultView;
                    DataTable dt2 = dw.ToTable(true, cols);
                    Grid_pricedetails.DataSource = dt2;
                    Grid_pricedetails.DataBind();
                    if (Grid_pricedetails.Rows.Count > 0)
                    {
                        lblExtend.Visible = true;
                        txtExtendDate.Visible = true;
                        imgExtendDate.Visible = true;
                        btnExtend.Visible = true;
                    }
                }
                if (flag)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Promotion Extend Successfully!.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No promotion selected or some error occured.')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Extend Date')", true);
        }
    }

    protected void Grid_pricedetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow &&
            (e.Row.RowState == DataControlRowState.Normal ||
            e.Row.RowState == DataControlRowState.Alternate))
            {
                CheckBox cbExtend = (CheckBox)e.Row.Cells[0].FindControl("cbExtend");
                CheckBox cbHeader = (CheckBox)this.Grid_pricedetails.HeaderRow.FindControl("cbHeader");
                cbExtend.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');",cbHeader.ClientID);
            }
    }
    
    protected void Grid_pricedetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            PromotionController mController = new PromotionController();
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int PromotionId = Convert.ToInt32(row.Cells[1].Text);
            int SchemeId = Convert.ToInt32(row.Cells[9].Text);
            if (mController.UpdatePromotion(PromotionId, SchemeId, int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), null, null, Constants.DateNullValue, false, Constants.DateNullValue, Constants.DateNullValue, false, Constants.IntNullValue, Constants.IntNullValue) == "Record Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Promotion Deleted Successfully!.')", true);
                string FromDate = txtFromdate.Text + " 00:00:00";
                string ToDate = txttoDate.Text + " 23:59:59";
                DataTable dt = mController.SelectPromotion(FromDate, ToDate, Convert.ToInt32(this.DrpPrincipal.SelectedValue), int.Parse(this.Session["UserId"].ToString()), ChbActive.Checked);
                this.Session.Add("dt", dt);
                DataView dw = dt.DefaultView;
                DataTable dt2 = dw.ToTable(true, cols);
                Grid_pricedetails.DataSource = dt2;
                Grid_pricedetails.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.')", true);
            }
        }
        else
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int NewEditIndex = gvr.RowIndex;
            bool IsEditing = true;
            string flow = "f";
            string PromotionId = Grid_pricedetails.Rows[NewEditIndex].Cells[1].Text;
            this.Session.Add("PromotionId", PromotionId);
            this.Session.Add("IsEdit", IsEditing);
            this.Session.Add("Flow", flow);
            Response.Redirect("frmPromotionStep2.aspx?LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString(), true);
        }
    }    
}