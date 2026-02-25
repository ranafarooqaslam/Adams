using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit, Promotions
/// </summary>
public partial class Forms_frmPromotionLocation : System.Web.UI.Page
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
            LoadPrincipal();
            LoadDistributor();
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
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
        PromotionController mController = new PromotionController();
        if (btnPromotion.Text == "Get Promotion")
        {
            DataTable dt = mController.SelectPromotion("", "", Convert.ToInt32(this.DrpPrincipal.SelectedValue), int.Parse(this.Session["UserId"].ToString()), ChbActive.Checked);
            this.Session.Add("dt", dt);

            DataView dw = dt.DefaultView;
            DataTable dt2 = dw.ToTable(true, cols);

            Grid_pricedetails.DataSource = dt2;
            Grid_pricedetails.DataBind();

            if (Grid_pricedetails.Rows.Count > 0)
            {
            }
        }
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        PromotionController mController = new PromotionController();
        var count = 0;

        if (drpDistributor.Items.Count > 0)
        {
            foreach (GridViewRow item in Grid_pricedetails.Rows)
            {
                CheckBox cbExtend = (CheckBox)item.FindControl("cbExtend");
                if (cbExtend.Checked)
                {
                    var insertNewPromotionData = mController
                        .GeneratePromotionForNewLocationAsPreviouslyCreated(
                        long.Parse(item.Cells[1].Text),
                        int.Parse(item.Cells[9].Text),
                        int.Parse(drpDistributor.SelectedValue),
                        int.Parse(this.Session["UserId"].ToString()),
                        Session["DISTRIBUTOR_ID"].ToString()
                        );

                    count++;
                }
            }
        }
        if (count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please select Promotion');", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Location Assigned Successfully');", true);
        }

        //if (btnPromotion.Text == "Get Promotion")
        //{
        //    DataTable dt = mController.SelectPromotion("", "", Convert.ToInt32(this.DrpPrincipal.SelectedValue), int.Parse(this.Session["UserId"].ToString()), ChbActive.Checked);
        //    this.Session.Add("dt", dt);

        //    DataView dw = dt.DefaultView;
        //    DataTable dt2 = dw.ToTable(true, cols);

        //    Grid_pricedetails.DataSource = dt2;
        //    Grid_pricedetails.DataBind();

        //    if (Grid_pricedetails.Rows.Count > 0)
        //    {
        //    }
        //}
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
}