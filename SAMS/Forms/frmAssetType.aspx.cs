using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;

public partial class Forms_frmAssetType : System.Web.UI.Page
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
            LoadAssetCategory();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }

    /// <summary>
    /// Loads Locations To Location Grid
    /// </summary>
    private void LoadGird()
    {
        DataTable dtDistributor = mController.SelectAssetType();

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

    private void LoadAssetCategory()
    {
        DataTable dtDistributor = mController.SelectAssetCategory();
        if (dtDistributor != null && dtDistributor.Rows.Count > 0)
        {
            DataRow[] foundRows = dtDistributor.Select("IS_DELETED  = 'Active'");
            if (foundRows.Length > 0)
            {
                clsWebFormUtil.FillDropDownList(this.drpCategory, dtDistributor, "AssetCategoryID", "CategoryName", true);
            }
        }
    }

    /// <summary>
    /// Saves Or Updates A Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        AssetsTypeModel model = new AssetsTypeModel();
        model.User_ID = int.Parse(this.Session["UserId"].ToString());
        model.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
        model.Brand = txtBrand.Text;
        model.Description = txtDescription.Text;
        model.AssetTypeName = txtAssetType.Text;
        model.Capacity = txtCapacity.Text;
        model.Model = txtModel.Text;
        model.IS_DELETED = chkIsActive.Checked;
        model.IsSerialNoBased = chkSerial.Checked;
        model.AssetCategoryID = int.Parse(drpCategory.SelectedValue);

        if (btnSave.Text == "Save")
        {
            mController.InsertAssetsType(model);

        }
        else
        {
            model.AssetTypeID = int.Parse(txtRecordID.Value);
            mController.UpdateAssetsType(model);
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
        btnSave.Text = "Save";
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
        AssetsTypeModel model = new AssetsTypeModel();
        model.IS_DELETED = true;
        model.User_ID = int.Parse(this.Session["UserId"].ToString());
        model.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
        model.Brand = GridAssetType.Rows[e.RowIndex].Cells[5].Text;
        model.Description = GridAssetType.Rows[e.RowIndex].Cells[4].Text;
        model.AssetTypeName = GridAssetType.Rows[e.RowIndex].Cells[3].Text;
        model.Capacity = GridAssetType.Rows[e.RowIndex].Cells[4].Text;
        model.Model = GridAssetType.Rows[e.RowIndex].Cells[7].Text;
        model.AssetTypeID = int.Parse(GridAssetType.Rows[e.RowIndex].Cells[0].Text);
        model.AssetCategoryID = int.Parse(GridAssetType.Rows[e.RowIndex].Cells[1].Text);
        if (string.IsNullOrEmpty(GridAssetType.Rows[e.RowIndex].Cells[9].Text))
            GridAssetType.Rows[e.RowIndex].Cells[9].Text = "false";

        model.IsSerialNoBased = Convert.ToBoolean(GridAssetType.Rows[e.RowIndex].Cells[9].Text);

        mController.UpdateAssetsType(model);

        this.LoadGird();
    }

    /// <summary>
    /// Clears All The Fields.
    /// </summary>
    private void ClearAll()
    {
        txtAssetType.Text = "";
        txtDescription.Text = "";
        txtBrand.Text = "";
        txtCapacity.Text = "";
        txtModel.Text = "";
        chkIsActive.Checked = true;
    }

    protected void GridAssetType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        txtAssetType.Text = GridAssetType.Rows[NewEditIndex].Cells[3].Text;
        txtDescription.Text = GridAssetType.Rows[NewEditIndex].Cells[4].Text;
        txtBrand.Text = GridAssetType.Rows[NewEditIndex].Cells[5].Text;
        txtCapacity.Text = GridAssetType.Rows[NewEditIndex].Cells[6].Text;
        txtModel.Text = GridAssetType.Rows[NewEditIndex].Cells[7].Text;
        drpCategory.SelectedValue = GridAssetType.Rows[NewEditIndex].Cells[1].Text;
        txtRecordID.Value = GridAssetType.Rows[NewEditIndex].Cells[0].Text;
        if (GridAssetType.Rows[NewEditIndex].Cells[8].Text == "Active")
        {
            chkIsActive.Checked = true;
        }
        else
        {
            chkIsActive.Checked = false;
        }
        chkSerial.Checked = Convert.ToBoolean(GridAssetType.Rows[NewEditIndex].Cells[9].Text);
        btnSave.Text = "Update";
    }
}