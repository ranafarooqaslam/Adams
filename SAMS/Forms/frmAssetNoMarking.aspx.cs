using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;

public partial class Forms_frmAssetNoMarking : System.Web.UI.Page
{
    AssetsController mController = new AssetsController();
    DataControl dc = new DataControl();
    private static int DistributorId;
    private static int CompanyId;
    private static int RowNo;

    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            CreatTable();
            GetPurchasedAssets();
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }

    /// <summary>
    /// Loads Locations To Location Grid
    /// </summary>
    private void LoadGird()
    {
       var _purchaseSkus = (DataTable)Session["AssetPurchase"];

        if (_purchaseSkus != null)
        {
            GridAssetType.DataSource = _purchaseSkus;
            GridAssetType.DataBind();
            //txtTotalQuantity.Text = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", totalValue);
        }
    }

    #region Click Operations

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                if (drpDistributor.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select Location');", true);
                    drpDistributor.Focus();
                    return;
                }

                DateTime CurrentWorkDate = Constants.DateNullValue;
                DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
                foreach (DataRow dr in dtLocationInfo.Rows)
                {
                    if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue.ToString())
                    {
                        if (dr["MaxDayClose"].ToString().Length > 0)
                        {
                            CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                            break;
                        }
                    }
                }

                foreach (GridViewRow row in GridAssetType.Rows)
                {
                    var box8 = (TextBox)row.Cells[9].FindControl("txtCompanyAssetNo");
                    for (int i = 0; i < GridAssetType.Rows.Count - 1; i++)
                    {
                        if (row.RowIndex != i)
                        {
                            var box8Compare = (TextBox)GridAssetType.Rows[i].Cells[9].FindControl("txtCompanyAssetNo");
                            if (!string.IsNullOrEmpty(box8Compare.Text) && !string.IsNullOrEmpty(box8.Text))
                            {
                                if (box8Compare.Text == box8.Text)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Asset No must be unique');", true);
                                    return;
                                }
                            }
                        }
                    }
                }

                if (CheckDublicateSerial())
                {
                    foreach (GridViewRow row in GridAssetType.Rows)
                    {
                        var box8 = (TextBox)row.Cells[9].FindControl("txtCompanyAssetNo");

                        if (!string.IsNullOrEmpty(box8.Text))
                        {
                            spAssetNoMarkingCRUD detail = new spAssetNoMarkingCRUD();
                            detail.Asset_Marking_ID = long.Parse(row.Cells[0].Text);
                            detail.Asset_PurchaseDetail_ID = long.Parse(row.Cells[1].Text);
                            detail.AssetTypeID = int.Parse(row.Cells[2].Text);
                            detail.SerialNo1 = row.Cells[4].Text;
                            detail.SerialNo2 = row.Cells[5].Text;
                            detail.Rate = decimal.Parse(dc.chkNull_0(row.Cells[6].Text));
                            detail.Color = row.Cells[7].Text;
                            detail.MfgYear = row.Cells[8].Text;
                            detail.CompanyAssetNo = box8.Text.Trim();
                            detail.IsActive = true;
                            detail.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                            detail.User_ID = int.Parse(this.Session["UserId"].ToString());
                            detail.DISTRIBUTOR_ID = int.Parse(drpDistributor.SelectedValue);
                            detail.DOCUMENT_DATE = CurrentWorkDate;
                            detail.Remarks = txtRemarks.Text;

                            mController.InsertOrUpdateAssetMarkingNo(detail);
                        }
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);

                    ClearAll();
                    CreatTable();
                    LoadDistributor();
                    GetPurchasedAssets();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Asset No must be unique');", true);
                    return;
                }
            }
            else
            {
                //mPopUpLocation.Show();
                //LoadApprovalBy();
            }
        }

        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "CatchMsg", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    private bool CheckDublicateSerial()
    {
        var PurchaseSKU = mController.SelectAssetNo(Constants.LongNullValue);
        if (PurchaseSKU != null && PurchaseSKU.Rows.Count > 0)
        {
            foreach (GridViewRow item in GridAssetType.Rows)
            {
                var box8 = (TextBox)item.Cells[9].FindControl("txtCompanyAssetNo");
                if (!string.IsNullOrEmpty(box8.Text))
                {
                    DataRow[] foundRows = PurchaseSKU.Select("CompanyAssetNo  = '" + box8.Text.Trim() + "'");
                    if (foundRows.Length == 0)
                    {
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return true;
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
       // this.LoadGird();
    }
    #endregion
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
        var _purchaseSkus = (DataTable)Session["AssetPurchase"];
        if (_purchaseSkus.Rows.Count > 0)
        {
            _purchaseSkus.Rows.RemoveAt(e.RowIndex);
            DataRow dr = _purchaseSkus.NewRow();
            LoadGird();
        }
    }

    /// <summary>
    /// Clears All The Fields.
    /// </summary>
    private void ClearAll()
    {
        txtRemarks.Text = "";
    }

    protected void GridAssetType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowNo = NewEditIndex;
    }
    private void CreatTable()
    {
        var PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("Asset_Marking_ID", typeof(long));
        PurchaseSKU.Columns.Add("Asset_PurchaseDetail_ID", typeof(long));
        PurchaseSKU.Columns.Add("AssetTypeID", typeof(int));
        PurchaseSKU.Columns.Add("SerialNo1", typeof(string));
        PurchaseSKU.Columns.Add("AssetTypeName", typeof(string));
        PurchaseSKU.Columns.Add("SerialNo2", typeof(string));
        PurchaseSKU.Columns.Add("Rate", typeof(decimal));
        PurchaseSKU.Columns.Add("CompanyAssetNo", typeof(string));
        PurchaseSKU.Columns.Add("MfgYear", typeof(string));
        PurchaseSKU.Columns.Add("Color", typeof(string));
        this.Session.Add("AssetPurchase", PurchaseSKU);

    }
    public void GetPurchasedAssets()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        var PurchaseSKU = (DataTable)this.Session["AssetPurchase"];

        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue.ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }

        var result = mController.SelectPurchasedAssetsForAssetNoMarking(int.Parse(drpDistributor.SelectedValue), chkWithAssetNo.Checked);

        if (result.Rows.Count > 0)
        {
            foreach (DataRow item in result.Rows)
            {
                DataRow dr = PurchaseSKU.NewRow();
                dr["Asset_Marking_ID"] = item["Asset_Marking_ID"].ToString();
                dr["Asset_PurchaseDetail_ID"] = item["Asset_PurchaseDetail_ID"].ToString();
                dr["AssetTypeID"] = item["AssetTypeID"].ToString();
                dr["AssetTypeName"] = item["AssetTypeName"].ToString();
                dr["SerialNo1"] = item["SerialNo1"].ToString();
                dr["SerialNo2"] = item["SerialNo2"].ToString();
                dr["Rate"] = item["Rate"].ToString();
                dr["Color"] = item["Color"].ToString();
                dr["MfgYear"] = item["MfgYear"].ToString();
                dr["CompanyAssetNo"] = item["CompanyAssetNo"].ToString();

                PurchaseSKU.Rows.Add(dr);
            }

            this.Session.Add("AssetPurchase", PurchaseSKU);
            this.LoadGird();

            foreach (DataRow item in result.Rows)
            {
                var box8 = (TextBox)GridAssetType.Rows[result.Rows.IndexOf(item)].Cells[9].FindControl("txtCompanyAssetNo");

                if (!string.IsNullOrEmpty(item["CompanyAssetNo"].ToString()))
                {
                    box8.Text = item["CompanyAssetNo"].ToString();
                }
            }
        }
        else
        {
            this.LoadGird();
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        CreatTable();
        GetPurchasedAssets();
    }
    protected void WithAssetCheck_Changed(object sender, EventArgs e)
    {
        CreatTable();
        GetPurchasedAssets();
    }
}