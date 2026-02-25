using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;

public partial class Forms_frmAssetCategory : System.Web.UI.Page
{
    AssetsController mController = new AssetsController();
    private static int DistributorId;
    private static int CompanyId;

    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadGird();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }

    /// <summary>
    /// Loads Locations To Location Grid
    /// </summary>
    private void LoadGird()
    {
        DataTable dtDistributor = mController.SelectAssetCategory();

        //switch (ddSearchType.SelectedIndex)
        //{
        //    case 1:
        //        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    case 2:
        //        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    case 3:
        //        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    case 4:
        //        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    case 5:
        //        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
        //        break;
        //    default:
        //        dtDistributor.DefaultView.RowFilter = "Distributor_name" + " like '%" + "" + "%'";
        //        break;
        //}

        GridAssetType.DataSource = dtDistributor;
        GridAssetType.DataBind();
    }

    /// <summary>
    /// Saves Or Updates A Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        spInsertAssetTypes model = new spInsertAssetTypes();
        model.User_ID = int.Parse(this.Session["UserId"].ToString());
        model.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
        model.Description = txtDescription.Text;
        model.CategoryName = txtAssetType.Text;
        model.IS_DELETED = chkIsActive.Checked;

        if (btnSave.Text == "Save")
        {
            mController.InsertAssetCategory(model);

        }
        else
        {
            model.AssetCategoryID = int.Parse(txtRecordID.Value);
            mController.InsertAssetCategory(model);
        }
        ClearAll();
        this.LoadGird();
    }

    /// <summary>
    /// Clears All The Fields Through ClearAll() Function.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    /// <summary>
    /// Loads Locations To Location Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGird();
    }

    /// <summary>
    /// Sets PageIndex Of Location Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GridAssetType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridAssetType.PageIndex = e.NewPageIndex;
        this.LoadGird();
    }

    /// <summary>
    /// Actives/DeActives A Location.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GridAssetType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        spInsertAssetTypes model = new spInsertAssetTypes();
        model.IS_DELETED = true;
        model.User_ID = int.Parse(this.Session["UserId"].ToString());
        model.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
        model.Description = GridAssetType.Rows[e.RowIndex].Cells[2].Text;
        model.CategoryName = GridAssetType.Rows[e.RowIndex].Cells[1].Text;
        model.AssetCategoryID = int.Parse(GridAssetType.Rows[e.RowIndex].Cells[0].Text);

        mController.InsertAssetCategory(model);

        this.LoadGird();
    }

    /// <summary>
    /// Clears All The Fields.
    /// </summary>
    private void ClearAll()
    {
        txtAssetType.Text = "";
        txtDescription.Text = "";
        chkIsActive.Checked = true;
    }

    protected void GridAssetType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        txtAssetType.Text = GridAssetType.Rows[NewEditIndex].Cells[1].Text;
        txtDescription.Text = GridAssetType.Rows[NewEditIndex].Cells[2].Text;
        txtRecordID.Value = GridAssetType.Rows[NewEditIndex].Cells[0].Text;
        if (GridAssetType.Rows[NewEditIndex].Cells[3].Text == "Active")
        {
            chkIsActive.Checked = true;
        }
        else
        {
            chkIsActive.Checked = false;
        }
        btnSave.Text = "Update";
    }
}