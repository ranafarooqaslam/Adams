using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;

public partial class Forms_frmSupplier : System.Web.UI.Page
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
        DataTable dtDistributor = mController.SelectSupplier();

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
        spSupplierCRUD model = new spSupplierCRUD();
        model.User_ID = int.Parse(this.Session["UserId"].ToString());
        model.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
        model.SupplierName = txtSupplier.Text;
        model.Address = txtAddress.Text;
        model.ContactPerson = txtContactPerson.Text;
        model.ContactNo = txtContactNo.Text;
        model.Email = txtEmail.Text;
        model.SalesTaxNo = txtSalesTaxNo.Text;
        model.NTN = txtNTN.Text;
        model.IsActive = chkIsActive.Checked;

        if (btnSave.Text == "Save")
        {
            mController.InsertSupplier(model);

        }
        else
        {
            model.Supplier_ID = int.Parse(txtRecordID.Value);
            mController.UpdateSupplier(model);
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
        spSupplierCRUD model = new spSupplierCRUD();
        model.IsActive = false;
        model.User_ID = int.Parse(this.Session["UserId"].ToString());
        model.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
        model.SupplierName = GridAssetType.Rows[e.RowIndex].Cells[1].Text;
        model.Address = GridAssetType.Rows[e.RowIndex].Cells[2].Text;
        model.ContactPerson = GridAssetType.Rows[e.RowIndex].Cells[3].Text;
        model.ContactNo = GridAssetType.Rows[e.RowIndex].Cells[4].Text;
        model.SalesTaxNo = GridAssetType.Rows[e.RowIndex].Cells[7].Text;
        model.NTN = GridAssetType.Rows[e.RowIndex].Cells[6].Text;
        model.Email = GridAssetType.Rows[e.RowIndex].Cells[5].Text;
        model.Supplier_ID = int.Parse(GridAssetType.Rows[e.RowIndex].Cells[0].Text);

        mController.UpdateSupplier(model);

        this.LoadGird();
    }

    /// <summary>
    /// Clears All The Fields.
    /// </summary>
    private void ClearAll()
    {
        txtSupplier.Text = "";
        txtAddress.Text = "";
        txtContactPerson.Text = "";
        txtContactNo.Text = "";
        txtEmail.Text = "";
        txtSalesTaxNo.Text = "";
        txtNTN.Text = "";
        chkIsActive.Checked = true;
    }

    protected void GridAssetType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        txtSupplier.Text = GridAssetType.Rows[NewEditIndex].Cells[1].Text;
        txtAddress.Text = GridAssetType.Rows[NewEditIndex].Cells[2].Text;
        txtContactPerson.Text = GridAssetType.Rows[NewEditIndex].Cells[3].Text;
        txtContactNo.Text = GridAssetType.Rows[NewEditIndex].Cells[4].Text;
        txtEmail.Text = GridAssetType.Rows[NewEditIndex].Cells[5].Text;
        txtNTN.Text = GridAssetType.Rows[NewEditIndex].Cells[6].Text;
        txtSalesTaxNo.Text = GridAssetType.Rows[NewEditIndex].Cells[7].Text;
        txtRecordID.Value = GridAssetType.Rows[NewEditIndex].Cells[0].Text;
        if (GridAssetType.Rows[NewEditIndex].Cells[8].Text == "Active")
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