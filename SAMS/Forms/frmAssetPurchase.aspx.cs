using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_frmAssetPurchase : System.Web.UI.Page
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
            GetDocumentNo();
            LoadSupplier();
            LoadAsset();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            txtQty.Attributes.Add("readonly", "readonly");
            txtQty.Text = "1";
            CreatTable();
            MAXDATE();
            ShowHideSerialBased();
        }
    }
    
    private void MAXDATE()
    {
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];

        if (drpDistributor.SelectedIndex != -1)
        {
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue.ToString())
                {
                    if (dr["MaxDayClose"].ToString().Length > 0)
                    {
                        txtPurDate.Text = Convert.ToDateTime(dr["MaxDayClose"])
                            .ToString("dd-MMM-yyyy");

                        //CEEndDate.EndDate = Convert.ToDateTime(txtPurDate.Text);
                        break;
                    }
                }
            }
        }

        txtPurDate.Attributes.Add("readonly", "readonly");
    }
    private void ShowHideSerialBased()
    {
        if (ddlAsset.SelectedIndex != -1 && ddlAsset.Items.Count > 0)
        {
            DataTable dt = mController.SelectAssetType();
            DataRow[] foundRow = dt.Select("AssetTypeID  = '" + ddlAsset.SelectedValue + "'");

            if (foundRow != null && foundRow[0] != null && foundRow.Length > 0)
            {
               bool isSerialBased = Convert.ToBoolean(foundRow[0]["IsSerialNoBased"].ToString());
                if (isSerialBased == false)
                {
                    txtSerial1.Attributes.Add("readonly", "readonly");
                    txtSerial2.Attributes.Add("readonly", "readonly");
                    txtYear.Attributes.Add("readonly", "readonly");
                    txtColor.Attributes.Add("readonly", "readonly");
                    txtQty.Attributes.Remove("readonly");
                }
                else
                {
                    txtSerial1.Attributes.Remove("readonly");
                    txtSerial2.Attributes.Remove("readonly");
                    txtYear.Attributes.Remove("readonly");
                    txtColor.Attributes.Remove("readonly");
                    txtQty.Attributes.Add("readonly", "readonly");
                    txtQty.Text = "1";
                }

                Session["SerialBased"] = isSerialBased;
            }
            else
            {
                txtSerial1.Attributes.Remove("readonly");
                txtSerial2.Attributes.Remove("readonly");
                txtYear.Attributes.Remove("readonly");
                txtQty.Attributes.Add("readonly", "readonly");
                txtQty.Text = "1";
                Session["SerialBased"] = true;
            }
        }
    }
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
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
        var distributorID = Constants.IntNullValue;
        if (drpDistributor.Items.Count > 0)
        {
            distributorID = Convert.ToInt32(drpDistributor.SelectedValue);
        }

        DataTable dt = mController.SelectPurchaseDocumentNo(CurrentWorkDate, distributorID, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));

        if (dt.Rows.Count > 0)
        {
            clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }
    private void LoadSupplier()
    {
        DataTable dtDistributor = mController.SelectSupplier();
        //dtDistributor = dtDistributor.Select()
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, dtDistributor, "Supplier_ID", "SupplierName", true);
    }
    private void LoadAsset()
    {
        DataTable dtDistributor = mController.SelectAssetType();
        //dtDistributor = dtDistributor.Select()
        clsWebFormUtil.FillDropDownList(this.ddlAsset, dtDistributor, "AssetTypeID", "AssetTypeName", true);
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

            decimal QtySum = 0;
            var count = 0;
            foreach (GridViewRow item in GridAssetType.Rows)
            {
                QtySum = Convert.ToDecimal(item.Cells[8].Text) + QtySum;
                count++;
            }

            txtTotalQuantity.Text = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", QtySum);
        }
    }

    #region Click Operations

    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var PurchaseSKU = (DataTable)this.Session["AssetPurchase"];
        var isSerialBased = true;
        var serialBased = Session["SerialBased"].ToString();
        if (!string.IsNullOrEmpty(serialBased))
        {
            isSerialBased = Convert.ToBoolean(serialBased);
        }

        foreach (GridViewRow item in GridAssetType.Rows)
        {
            if (Convert.ToBoolean(item.Cells[10].Text) != isSerialBased)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Serial Based Item cannot be added with Non Serial Based Item.');", true);
                ddlAsset.Focus();
                return;
            }
        }

        if (ddlAsset.SelectedIndex == -1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select Asset');", true);
            ddlAsset.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtSerial1.Text) && string.IsNullOrEmpty(txtSerial2.Text) && isSerialBased == true)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter Serial #.1 or Serial #.2');", true);
            txtSerial1.Focus();
            return;
        }

        if (decimal.Parse(dc.chkNull_0(txtRate.Text)) <= 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter Rate');", true);
            txtRate.Focus();
            return;
        }

        if (CheckDublicateSerial() && isSerialBased == true)
        {
            if (btnSave.Text == "Add")
            {

                foreach (GridViewRow item in GridAssetType.Rows)
                {
                    if (item.Cells[3].Text.ToString().Trim() == txtSerial1.Text)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Serial 1 Already Exists');", true);
                        txtRate.Focus();
                        return;
                    }
                    else if (item.Cells[4].Text.ToString().Trim() == txtSerial2.Text)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Serial 2 Already Exists');", true);
                        txtRate.Focus();
                        return;
                    }
                }

                DataRow dr = PurchaseSKU.NewRow();
                dr["Asset_PurchaseDetail_ID"] = 0;
                dr["AssetTypeID"] = ddlAsset.SelectedValue;
                dr["AssetTypeName"] = ddlAsset.SelectedItem.Text;
                dr["SerialNo1"] = txtSerial1.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
                dr["SerialNo2"] = txtSerial2.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
                dr["Qty"] = Convert.ToDecimal(dc.chkNull_0(txtQty.Text));
                dr["Rate"] = Convert.ToDecimal(dc.chkNull_0(txtRate.Text));
                dr["Color"] = txtColor.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
                dr["MfgYear"] = txtYear.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
                dr["Purchase_Date"] = txtPurDate.Text;
                dr["IsSerialNoBased"] = isSerialBased;
                PurchaseSKU.Rows.Add(dr);

            }
            else if (btnSave.Text == "Update")
            {
                DataRow dr = PurchaseSKU.Rows[RowNo];
                dr["AssetTypeID"] = ddlAsset.SelectedValue;
                dr["AssetTypeName"] = ddlAsset.SelectedItem.Text;
                dr["SerialNo1"] = txtSerial1.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["SerialNo2"] = txtSerial2.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["Qty"] = decimal.Parse(dc.chkNull_0(txtQty.Text));
                dr["Rate"] = decimal.Parse(dc.chkNull_0(txtRate.Text));
                dr["Color"] = txtColor.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["MfgYear"] = txtYear.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["Purchase_Date"] = txtPurDate.Text;
                dr["IsSerialNoBased"] = isSerialBased;

                btnSave.Text = "Add";
            }
        }
        else if (isSerialBased == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Serial #.1 or Serial #.2 Already Exists ');", true);
            return;
        }
        else if (isSerialBased == false)
        {
            if (btnSave.Text == "Add")
            {

                foreach (GridViewRow item in GridAssetType.Rows)
                {
                    if (item.Cells[2].Text.ToString().Trim() == ddlAsset.SelectedItem.Text)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Asset already exists');", true);
                        txtRate.Focus();
                        return;
                    }
                }

                DataRow dr = PurchaseSKU.NewRow();
                dr["Asset_PurchaseDetail_ID"] = 0;
                dr["AssetTypeID"] = ddlAsset.SelectedValue;
                dr["AssetTypeName"] = ddlAsset.SelectedItem.Text;
                dr["SerialNo1"] = txtSerial1.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
                dr["SerialNo2"] = txtSerial2.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
                dr["Qty"] = Convert.ToDecimal(dc.chkNull_0(txtQty.Text));
                dr["Rate"] = Convert.ToDecimal(dc.chkNull_0(txtRate.Text));
                dr["Color"] = txtColor.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
                dr["MfgYear"] = txtYear.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
                dr["Purchase_Date"] = txtPurDate.Text;
                dr["IsSerialNoBased"] = isSerialBased;

                PurchaseSKU.Rows.Add(dr);

            }
            else if (btnSave.Text == "Update")
            {
                DataRow dr = PurchaseSKU.Rows[RowNo];
                dr["AssetTypeID"] = ddlAsset.SelectedValue;
                dr["AssetTypeName"] = ddlAsset.SelectedItem.Text;
                dr["SerialNo1"] = txtSerial1.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["SerialNo2"] = txtSerial2.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["Qty"] = decimal.Parse(dc.chkNull_0(txtQty.Text));
                dr["Rate"] = decimal.Parse(dc.chkNull_0(txtRate.Text));
                dr["Color"] = txtColor.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["MfgYear"] = txtYear.Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["Purchase_Date"] = txtPurDate.Text;
                dr["IsSerialNoBased"] = isSerialBased;

                btnSave.Text = "Add";
            }
        }
        this.Session.Add("AssetPurchase", PurchaseSKU);
        this.ClearAll();
        this.LoadGird();
        ScriptManager.GetCurrent(Page).SetFocus(ddlAsset);
    }
    private bool CheckDublicateSerial()
    {
        var PurchaseSKU = mController.SelectAssetPurchaseDetail(Constants.LongNullValue);
        if (PurchaseSKU != null && PurchaseSKU.Rows.Count > 0)
        {
            if (drpDocumentNo.Items.Count > 0 && drpDocumentNo.SelectedIndex != -1)
            {
                DataRow[] dr = PurchaseSKU.Select("Asset_Purchase_ID  <> '" + drpDocumentNo.SelectedValue + "'");
                PurchaseSKU = dr.CopyToDataTable();
            }
            DataRow[] foundRows = null;
            DataRow[] foundRows1 = null;

            if (!string.IsNullOrEmpty(txtSerial1.Text.Trim()))
                foundRows = PurchaseSKU.Select("SerialNo1  = '" + txtSerial1.Text.Trim() + "'");

            if (!string.IsNullOrEmpty(txtSerial2.Text.Trim()))
                foundRows1 = PurchaseSKU.Select("SerialNo2  = '" + txtSerial2.Text.Trim() + "'");

            if ((foundRows == null || foundRows.Length == 0) && (foundRows1 == null || foundRows1.Length == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

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
                if (ddlSupplier.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select Supplier');", true);
                    ddlSupplier.Focus();
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

                var purchase_Date = !string.IsNullOrEmpty(txtPurDate.Text) ? Convert.ToDateTime(txtPurDate.Text) : Constants.DateNullValue;

                if (purchase_Date > CurrentWorkDate)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Purchase Date cannot be greater than Location's Working Date');", true);
                    ddlSupplier.Focus();
                    return;
                }

                bool flag = true;
                long savedRecordID = 0;

                var assetPurchase = new spAssetPurchaseMaster();
                assetPurchase.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                assetPurchase.User_ID = int.Parse(this.Session["UserId"].ToString());
                assetPurchase.DISTRIBUTOR_ID = int.Parse(drpDistributor.SelectedValue);
                assetPurchase.Supplier_ID = int.Parse(ddlSupplier.SelectedValue);
                assetPurchase.Remarks = txtRemarks.Text;
                assetPurchase.DOCUMENT_DATE = CurrentWorkDate;
                assetPurchase.Purchase_Date = Convert.ToDateTime(txtPurDate.Text);
                assetPurchase.IsActive = true;

                if (drpDocumentNo.SelectedItem.Text == "New")
                {
                    savedRecordID = mController.InsertAssetPurchase(assetPurchase);

                    if (savedRecordID > 0)
                    {
                        foreach (GridViewRow row in GridAssetType.Rows)
                        {
                            spAssetPurchaseDetail detail = new spAssetPurchaseDetail();
                            detail.Asset_Purchase_ID = savedRecordID;
                            detail.AssetTypeID = int.Parse(row.Cells[1].Text);
                            detail.SerialNo1 = row.Cells[3].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                            detail.SerialNo2 = row.Cells[4].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                            detail.Rate = decimal.Parse(dc.chkNull_0(row.Cells[5].Text));
                            detail.Color = row.Cells[6].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                            detail.MfgYear = row.Cells[7].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                            detail.Qty = decimal.Parse(dc.chkNull_0(row.Cells[8].Text));
                            detail.IsActive = true;
                            detail.CompanyID = assetPurchase.CompanyID;
                            detail.User_ID = assetPurchase.User_ID;
                            detail.Purchase_Date = Convert.ToDateTime(txtPurDate.Text);

                            mController.InsertAssetPurchaseDetail(detail);
                        } 
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);
                    flag = true;
                }
                else
                {
                    assetPurchase.Asset_Purchase_ID = long.Parse(drpDocumentNo.SelectedValue);
                    savedRecordID = assetPurchase.Asset_Purchase_ID;
                    assetPurchase.Purchase_Date = Convert.ToDateTime(txtPurDate.Text);
                    mController.UpdateAssetPurchase(assetPurchase);

                    foreach (GridViewRow row in GridAssetType.Rows)
                    {
                        spAssetPurchaseDetail detail = new spAssetPurchaseDetail();
                        detail.Asset_Purchase_ID = assetPurchase.Asset_Purchase_ID;
                        detail.AssetTypeID = int.Parse(row.Cells[1].Text);
                        detail.SerialNo1 = row.Cells[3].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                        detail.SerialNo2 = row.Cells[4].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                        detail.Rate = decimal.Parse(dc.chkNull_0(row.Cells[5].Text));
                        detail.Color = row.Cells[6].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                        detail.MfgYear = row.Cells[7].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                        detail.Qty = decimal.Parse(dc.chkNull_0(row.Cells[8].Text));
                        detail.IsActive = true;
                        detail.CompanyID = assetPurchase.CompanyID;
                        detail.User_ID = assetPurchase.User_ID;
                        detail.Purchase_Date = Convert.ToDateTime(txtPurDate.Text);

                        mController.InsertAssetPurchaseDetail(detail);
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record updated successfully.');", true);
                    flag = true;
                }
                if (flag)
                {
                    var remarks = txtRemarks.Text;

                    var _purchaseSkus = (DataTable)Session["AssetPurchase"];
                    _purchaseSkus.Rows.Clear();
                    Session.Add("AssetPurchase", _purchaseSkus);
                    CreatTable();
                    LoadGird();
                    ClearAll();
                    GetDocumentNo();
                    GridAssetType.SelectedIndex = 0;
                    MAXDATE();
                    ShowHideSerialBased();

                    ShowPopUpReport(savedRecordID, purchase_Date, drpDistributor, ddlSupplier, remarks);
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
            LedgerController LController = new LedgerController();
            DataTable dt = LController.AssignAssetPermission(Convert.ToInt32(Session["UserID"].ToString()));
            if (dt.Rows.Count > 0)
            {
                bool canDelete = Convert.ToBoolean(dt.Rows[0]["CanDelete"].ToString());

                if (canDelete)
                {
                    _purchaseSkus.Rows.RemoveAt(e.RowIndex);
                    DataRow dr = _purchaseSkus.NewRow();
                    LoadGird();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('You have no permission to access this feature.');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('You have no permission to access this feature.');", true);
                return;
            }
        }
    }

    /// <summary>
    /// Clears All The Fields.
    /// </summary>
    private void ClearAll()
    {
        txtSerial1.Text = "";
        txtSerial2.Text = "";
        txtRate.Text = "";
        txtYear.Text = "";
        txtColor.Text = "";
        txtQty.Text = "1";
    }

    protected void GridAssetType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowNo = NewEditIndex;
        ddlAsset.SelectedValue = GridAssetType.Rows[NewEditIndex].Cells[1].Text;
        txtSerial1.Text = GridAssetType.Rows[NewEditIndex].Cells[3].Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "");
        txtSerial2.Text = GridAssetType.Rows[NewEditIndex].Cells[4].Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
        txtRate.Text = GridAssetType.Rows[NewEditIndex].Cells[5].Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
        txtColor.Text = GridAssetType.Rows[NewEditIndex].Cells[6].Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
        txtYear.Text = GridAssetType.Rows[NewEditIndex].Cells[7].Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
        txtQty.Text = GridAssetType.Rows[NewEditIndex].Cells[8].Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
        txtPurDate.Text = GridAssetType.Rows[NewEditIndex].Cells[9].Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");

        btnSave.Text = "Update";
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTable();
            LoadGird();
            this.ClearAll();
        }
        else
        {
            CreatTable();
            LedgerController LController = new LedgerController();
            DataTable dt = LController.AssignAssetPermission(Convert.ToInt32(Session["UserID"].ToString()));
            if (dt.Rows.Count > 0)
            {
                bool canEdit = Convert.ToBoolean(dt.Rows[0]["CanEdit"].ToString());

                if (canEdit)
                {
                    LoadAssetDetails(Convert.ToInt64(drpDocumentNo.SelectedValue));
                }
                else
                {
                    drpDocumentNo.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('You have no permission to access this feature.');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('You have no permission to access this feature.');", true);
                return;
            }
        }
    }
    public void LoadAssetDetails(long Asset_Purchase_ID)
    {
        var result = mController.SelectAssetPurchaseDetail(Asset_Purchase_ID);

        if (result.Rows.Count > 0)
        {

            var PurchaseSKU = (DataTable)this.Session["AssetPurchase"];

            foreach (DataRow item in result.Rows)
            {
                DataRow dr = PurchaseSKU.NewRow();
                dr["Asset_PurchaseDetail_ID"] = item["Asset_PurchaseDetail_ID"].ToString();
                dr["AssetTypeID"] = item["AssetTypeID"].ToString();
                dr["AssetTypeName"] = item["AssetTypeName"].ToString();
                dr["SerialNo1"] = item["SerialNo1"].ToString().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["SerialNo2"] = item["SerialNo2"].ToString().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["Qty"] = item["Qty"].ToString();
                dr["Rate"] = item["Rate"].ToString();
                dr["Color"] = item["Color"].ToString().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["MfgYear"] = item["MfgYear"].ToString().Replace("nbsp;", "").Replace("&nbsp;", "")
                                        .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                dr["Purchase_Date"] = item["Purchase_Date"].ToString();
                dr["IsSerialNoBased"] = item["IsSerialNoBased"].ToString();

                PurchaseSKU.Rows.Add(dr);
            }

            this.Session.Add("AssetPurchase", PurchaseSKU);
            this.LoadGird();
        }
    }
    private void CreatTable()
    {
        var PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("Asset_PurchaseDetail_ID", typeof(long));
        PurchaseSKU.Columns.Add("AssetTypeID", typeof(int));
        PurchaseSKU.Columns.Add("SerialNo1", typeof(string));
        PurchaseSKU.Columns.Add("AssetTypeName", typeof(string));
        PurchaseSKU.Columns.Add("SerialNo2", typeof(string));
        PurchaseSKU.Columns.Add("Rate", typeof(decimal));
        PurchaseSKU.Columns.Add("Qty", typeof(decimal));
        PurchaseSKU.Columns.Add("MfgYear", typeof(string));
        PurchaseSKU.Columns.Add("Color", typeof(string));
        PurchaseSKU.Columns.Add("Purchase_Date", typeof(string));
        PurchaseSKU.Columns.Add("IsSerialNoBased", typeof(string));

        this.Session.Add("AssetPurchase", PurchaseSKU);
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
        MAXDATE();
        CreatTable();
        LoadGird();
    }
    protected void ddlAsset_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowHideSerialBased();
        txtColor.Text = "";
        txtQty.Text = "1";
        txtRate.Text = "";
        txtSerial1.Text = "";
        txtSerial2.Text = "";
        txtYear.Text = "";
    }
    public void ShowPopUpReport(long p_savedRecord_ID, DateTime p_workingDate, DropDownList distributor,
     DropDownList supplier, string Remarks)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            RptInventoryController RptInventoryCtl = new RptInventoryController();

            SAMSBusinessLayer.Reports.DsReport ds = new SAMSBusinessLayer.Reports.DsReport();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(distributor.SelectedValue.ToString()));

            DataControl dc = new DataControl();
            DataTable result = mController.SelectAssetPurchaseDetail(p_savedRecord_ID);
            foreach (DataRow dr in result.Rows)
            {
                ds.Tables["RptAssetStockDetail"].ImportRow(dr);
            }

            ReportDocument CrpReport = new ReportDocument();

            CrpReport = new SAMSBusinessLayer.Reports.CrpAssetPurchaseByID();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("FROM", distributor.SelectedItem.Text);
            CrpReport.SetParameterValue("TO", ddlSupplier.SelectedItem.Text);
            CrpReport.SetParameterValue("DocumentType", "Purchase Document");
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Remarks", Remarks);
            CrpReport.SetParameterValue("Date", p_workingDate);
            CrpReport.SetParameterValue("DocNo", p_savedRecord_ID);


            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            //const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            //Type cstype = this.GetType();
            //ClientScriptManager cs = Page.ClientScript;
            //cs.RegisterStartupScript(cstype, "OpenWindow", script);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openpage", "window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}