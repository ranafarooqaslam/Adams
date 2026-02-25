using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_frmAssetTransferOut : System.Web.UI.Page
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
            LoadLocationType();
            this.LoadDistributor();
            //LoadToLocation();
            CreatTable();
            GetDocumentNo();
            LoadAssets();
            customerRow.Visible = false;
            issuanceRow.Visible = false;
            chkDeliverCustomer.Checked = false;
            LoadCustomer();
            MAXDATE();
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
                        txtTransferDate.Text = Convert.ToDateTime(dr["MaxDayClose"])
                            .ToString("dd-MMM-yyyy");

                        break;
                    }
                }
            }
        }

        txtTransferDate.Attributes.Add("readonly", "readonly");
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);

        LoadAssignedDistributorToWareHouse();
    }
    private void LoadAssignedDistributorToWareHouse()
    {
        drpDistributor1.Enabled = true;
        drpDistributor1.Items.Clear();
        drpDistributor1.DataBind();

        DataTable dt = (DataTable)Session["dtLocationInfo"];
        if (dt.Rows.Count > 0 && drpDistributor.SelectedIndex != -1)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (item["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedItem.Value)
                {
                    if (item["SUBZONE_ID"].ToString() == "6" && drpDistributorType.SelectedItem.Text == "Distributor"
                        && chkDeliverCustomer.Checked == false)
                    {
                        DataRow[] dr = dt.Select("SUBZONE_ID = 6");
                        DataTable dt1 = dr.CopyToDataTable();
                        if (dt1.Rows.Count > 0)
                        {
                            clsWebFormUtil.FillDropDownList(this.drpDistributor1, dt1, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
                        }
                        break;
                    }
                    else if (item["SUBZONE_ID"].ToString() == "6" && drpDistributorType.SelectedItem.Text == "Warehouse")
                    {
                        DataRow[] dr = dt.Select("SUBZONE_ID = 2 OR SUBZONE_ID = 3");
                        DataTable dt1 = dr.CopyToDataTable();
                        if (dt1.Rows.Count > 0)
                        {
                            clsWebFormUtil.FillDropDownList(this.drpDistributor1, dt1, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
                        }
                        break;
                    }
                    else if ((item["SUBZONE_ID"].ToString() == "2" || item["SUBZONE_ID"].ToString() == "3") 
                        && drpDistributorType.SelectedItem.Text == "Distributor")
                    {
                        UserController mUserController = new UserController();
                        DataTable foundDt = mUserController.SelectDistributorAssignment(
                            int.Parse(drpDistributor.SelectedItem.Value),
                            6, 3, int.Parse(this.Session["CompanyId"].ToString()));

                        if (foundDt.Rows.Count > 0)
                        {
                            clsWebFormUtil.FillDropDownList(this.drpDistributor1, foundDt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
                        }
                        break;
                    }
                    else if (item["SUBZONE_ID"].ToString() == "6" && drpDistributorType.SelectedItem.Text == "Distributor"
                        && chkDeliverCustomer.Checked == true)
                    {

                        drpDistributor1.Items.Clear();
                        drpDistributor1.Items.Add(new clsListItems(drpDistributor.SelectedItem.Text, drpDistributor.SelectedValue));
                        drpDistributor1.DataBind();

                        drpDistributor1.SelectedValue = drpDistributor.SelectedValue;
                        drpDistributor1.Enabled = false;
                        break;
                    }
                    else
                    {
                        DistributorController DController = new DistributorController();
                        dt = DController.SelectDistributor(Constants.IntNullValue,
                            int.Parse(drpDistributorType.SelectedValue), int.Parse(this.Session["CompanyId"].ToString()));
                        clsWebFormUtil.FillDropDownList(this.drpDistributor1, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
                        break;
                    }
                }
            }
        }
    }
    private void LoadLocationType()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorTypeInfo(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(this.drpDistributorType, dt, "DISTRIBUTOR_TYPE_ID", "TYPENAME", true);
    }
    private void LoadToLocation()
    {
        try
        {
            drpDistributor1.Items.Clear();
            drpDistributor1.DataBind();

            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributor(Constants.IntNullValue,
                int.Parse(drpDistributorType.SelectedValue), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.drpDistributor1, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void LoadCustomer()
    {
        DataTable dt1 = new DataTable();

        if (drpDistributor1.SelectedIndex != -1)
        {
            CustomerDataController DController = new CustomerDataController();
            DataTable dt = DController.SelectAllCustomer(int.Parse(drpDistributor1.SelectedValue), Constants.IntNullValue, Constants.IntNullValue);
            if (dt.Rows.Count > 0)
            {
                DataRow[] foundRows = dt.Select("IS_ACTIVE  = '" + true + "'");
                dt1 = foundRows.CopyToDataTable();
                foreach (DataRow r in dt1.Rows)
                {
                    r["CUSTOMER_NAME"] = Convert.ToString(r["CUSTOMER_CODE"]) + " - " + Convert.ToString(r["CUSTOMER_NAME"]);
                }
            }
        }

        clsWebFormUtil.FillDropDownList(this.ddlCustomer, dt1, "CUSTOMER_ID", "CUSTOMER_NAME", true);

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

        DataTable dt = mController.SelectTransferOutDocumentNo(CurrentWorkDate, distributorID, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));

        if (dt.Rows.Count > 0)
        {
            clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
        }
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select From Location');", true);
                    drpDistributor.Focus();
                    return;
                }
                if (drpDistributor1.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select To Location');", true);
                    drpDistributor.Focus();
                    return;
                }
                if (chkDeliverCustomer.Checked == false && drpDistributorType.SelectedItem.Text != "Distributor")
                {
                    if (drpDistributor1.SelectedValue == drpDistributor.SelectedValue)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('From & To Location cannot be same');", true);
                        drpDistributor.Focus();
                        return;
                    }
                }
                if (chkDeliverCustomer.Checked == true && string.IsNullOrEmpty(txtIssuance.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Issuance On Account cannot be empty');", true);
                    drpDistributor.Focus();
                    return;
                }

                DateTime CurrentWorkDate = Constants.DateNullValue;
                DateTime LocationToWorkDate = Constants.DateNullValue;
                DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
                foreach (DataRow dr in dtLocationInfo.Rows)
                {
                    if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue.ToString())
                    {
                        if (dr["MaxDayClose"].ToString().Length > 0)
                        {
                            CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        }
                    }
                    if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor1.SelectedValue.ToString())
                    {
                        if (dr["MaxDayClose"].ToString().Length > 0)
                        {
                            LocationToWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        }
                    }
                }

                //if (LocationToWorkDate > CurrentWorkDate)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Cannot Save, To Location Working Date is greater than From Location');", true);
                //    drpDistributor.Focus();
                //    return;
                //}

                var transferDate = !string.IsNullOrEmpty(txtTransferDate.Text) ? Convert.ToDateTime(txtTransferDate.Text) : Constants.DateNullValue;
                //if (transferDate != null && CurrentWorkDate < transferDate)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Transfer Date cannot be greater than Location's Working Date');", true);
                //    CEEndDate.Focus();
                //    return;
                //}

                if (chkSerial.Checked == true)
                {
                    foreach (GridViewRow row in GridAssetType.Rows)
                    {
                        var box = (CheckBox)row.Cells[0].FindControl("chkRow");

                        if (box.Checked == true)
                        {
                            var purchaseDate = row.Cells[12].Text;
                            if (!string.IsNullOrEmpty(purchaseDate))
                            {
                                var purchaseDateConverted = Convert.ToDateTime(purchaseDate);

                                if (transferDate < purchaseDateConverted)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Transfer Date should be greater than or equal to Purchase Date');", true);
                                    CEEndDate.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow row in grdViewNonSerial.Rows)
                    {
                        var purchaseDate = row.Cells[13].Text;
                        if (!string.IsNullOrEmpty(purchaseDate))
                        {
                            var purchaseDateConverted = Convert.ToDateTime(purchaseDate);

                            if (transferDate < purchaseDateConverted)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Transfer Date should be greater than or equal to Purchase Date');", true);
                                CEEndDate.Focus();
                                return;
                            }
                        }
                    }
                }

                if (GridAssetType.Rows.Count > 0 || grdViewNonSerial.Rows.Count > 0)
                {
                    if (drpDocumentNo.SelectedItem.Text == "New")
                    {

                        int count = 0;
                        long savedID = 0;

                        if (chkSerial.Checked == false)
                        {
                            count = 1;
                        }
                        else
                        {
                            foreach (GridViewRow row in GridAssetType.Rows)
                            {
                                var box = (CheckBox)row.Cells[0].FindControl("chkRow");

                                if (box.Checked == true)
                                {
                                    count = count + 1;
                                }
                            }
                        }

                        if (count > 0)
                        {
                            spAssetTransferOut asset = new spAssetTransferOut();
                            asset.DOCUMENT_DATE = CurrentWorkDate;
                            asset.FROM_LOCATION = int.Parse(drpDistributor.SelectedValue);
                            asset.TO_LOCATION = int.Parse(drpDistributor1.SelectedValue);
                            asset.CUSTOMER_ID = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ?
                                long.Parse(ddlCustomer.SelectedValue) : Constants.LongNullValue;
                            asset.ISSUANCE_ACCOUNT = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ?
                                txtIssuance.Text : "";
                            asset.TYPE_ID = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ? 4 : 1;
                            asset.IsActive = true;
                            asset.Remarks = txtRemarks.Text;
                            asset.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                            asset.User_ID = int.Parse(this.Session["UserId"].ToString());
                            asset.Transfer_Date = transferDate;
                            asset.Transfer_In_Date = Constants.DateNullValue;
                            asset.Return_Date = Constants.DateNullValue;
                            asset.Damage_Date = Constants.DateNullValue;

                            savedID = mController.InsertAssetStock(asset);
                        }

                        if (savedID > 0)
                        {
                            if (chkSerial.Checked == true)
                            {
                                foreach (GridViewRow row in GridAssetType.Rows)
                                {
                                    var box = (CheckBox)row.Cells[0].FindControl("chkRow");
                                    if (box.Checked == true)
                                    {
                                        spAssetTransferOutDetail detail = new spAssetTransferOutDetail();
                                        detail.Asset_PurchaseDetail_ID = long.Parse(row.Cells[2].Text);
                                        detail.Asset_Marking_ID = long.Parse(row.Cells[1].Text);
                                        detail.AssetTypeID = int.Parse(row.Cells[3].Text);
                                        detail.SerialNo1 = row.Cells[5].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                                        detail.SerialNo2 = row.Cells[6].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                                        detail.Rate = decimal.Parse(dc.chkNull_0(row.Cells[7].Text));
                                        detail.Qty = 1;
                                        detail.Color = row.Cells[8].Text;
                                        detail.MfgYear = row.Cells[9].Text;
                                        detail.CompanyAssetNo = row.Cells[10].Text;
                                        detail.IsActive = true;
                                        detail.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                                        detail.User_ID = int.Parse(this.Session["UserId"].ToString());
                                        detail.TYPE_ID = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ? 4 : 1;
                                        detail.IS_ISSUED = false;
                                        detail.ISSUED_DATE = DateTime.Now;
                                        detail.IS_DAMAGED = false;
                                        detail.ASSET_STOCK_ID = savedID;
                                        detail.REF_ID = long.Parse(row.Cells[13].Text);

                                        mController.InsertOrUpdateStockDetail(detail);
                                    }
                                }
                            }
                            else
                            {
                                foreach (GridViewRow row in grdViewNonSerial.Rows)
                                {
                                    var splitAsset = row.Cells[3].Text.Split('-');
                                    spAssetTransferOutDetail detail = new spAssetTransferOutDetail();
                                    detail.Asset_PurchaseDetail_ID = long.Parse(row.Cells[2].Text);
                                    detail.Asset_Marking_ID = long.Parse(row.Cells[1].Text);
                                    detail.AssetTypeID = int.Parse(splitAsset[0].ToString());
                                    detail.SerialNo1 = "";
                                    detail.SerialNo2 = "";
                                    detail.Rate = decimal.Parse(dc.chkNull_0(row.Cells[7].Text));
                                    detail.Qty = decimal.Parse(dc.chkNull_0(row.Cells[8].Text));
                                    detail.Color = "";
                                    detail.MfgYear = "";
                                    detail.CompanyAssetNo = "";
                                    detail.IsActive = true;
                                    detail.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                                    detail.User_ID = int.Parse(this.Session["UserId"].ToString());
                                    detail.TYPE_ID = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ? 4 : 1;
                                    detail.IS_ISSUED = false;
                                    detail.ISSUED_DATE = DateTime.Now;
                                    detail.IS_DAMAGED = false;
                                    detail.ASSET_STOCK_ID = savedID;
                                    detail.REF_ID = long.Parse(row.Cells[14].Text);

                                    mController.InsertOrUpdateStockDetail(detail);
                                }
                            }

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');",
                                true);

                            var remarks = txtRemarks.Text;

                            ClearAll();
                            CreatTable();
                            LoadAssets();
                            GetDocumentNo();
                            CreatTableNonSerial();
                            LoadNonSerialGrid();
                            LoadNonSerialAsset();

                            ShowPopUpReport(savedID, transferDate, drpDistributor, drpDistributor1, ddlCustomer,
                               remarks);
                        }
                    }
                    else
                    {
                        spAssetTransferOut asset = new spAssetTransferOut();
                        asset.DOCUMENT_DATE = CurrentWorkDate;
                        asset.ASSET_STOCK_ID = long.Parse(drpDocumentNo.SelectedValue);
                        asset.FROM_LOCATION = int.Parse(drpDistributor.SelectedValue);
                        asset.TO_LOCATION = int.Parse(drpDistributor1.SelectedValue);
                        asset.CUSTOMER_ID = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ?
                                long.Parse(ddlCustomer.SelectedValue) : Constants.LongNullValue;
                        asset.TYPE_ID = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ? 4 : 1;
                        asset.ISSUANCE_ACCOUNT = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ?
                                txtIssuance.Text : "";
                        asset.IsActive = true;
                        asset.Remarks = txtRemarks.Text;
                        asset.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                        asset.User_ID = int.Parse(this.Session["UserId"].ToString());
                        asset.Transfer_Date = transferDate;
                        asset.Transfer_In_Date = Constants.DateNullValue;
                        asset.Return_Date = Constants.DateNullValue;
                        asset.Damage_Date = Constants.DateNullValue;

                        var savedID = mController.InsertAssetStock(asset);

                        if (savedID > 0)
                        {
                            if (chkSerial.Checked == true)
                            {
                                foreach (GridViewRow row in GridAssetType.Rows)
                                {
                                    var box = (CheckBox)row.Cells[0].FindControl("chkRow");

                                    spAssetTransferOutDetail detail = new spAssetTransferOutDetail();
                                    detail.Asset_Marking_ID = long.Parse(row.Cells[1].Text);
                                    detail.Asset_PurchaseDetail_ID = long.Parse(row.Cells[2].Text);
                                    detail.AssetTypeID = int.Parse(row.Cells[3].Text);
                                    detail.SerialNo1 = row.Cells[5].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                                    detail.SerialNo2 = row.Cells[6].Text.Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                                    detail.Rate = decimal.Parse(dc.chkNull_0(row.Cells[6].Text));
                                    detail.Qty = 1;
                                    detail.Color = row.Cells[8].Text;
                                    detail.MfgYear = row.Cells[9].Text;
                                    detail.CompanyAssetNo = row.Cells[10].Text;
                                    detail.ID = long.Parse(row.Cells[11].Text);
                                    detail.IsActive = box.Checked ? true : false;
                                    detail.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                                    detail.User_ID = int.Parse(this.Session["UserId"].ToString());
                                    detail.TYPE_ID = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ? 4 : 1;
                                    detail.IS_ISSUED = false;
                                    detail.ISSUED_DATE = DateTime.Now;
                                    detail.IS_DAMAGED = false;
                                    detail.ASSET_STOCK_ID = savedID;
                                    detail.REF_ID = long.Parse(row.Cells[13].Text);

                                    mController.InsertOrUpdateStockDetail(detail);
                                }
                            }
                            else
                            {
                                foreach (GridViewRow row in grdViewNonSerial.Rows)
                                {
                                    var splitAsset = row.Cells[3].Text.Split('-');
                                    spAssetTransferOutDetail detail = new spAssetTransferOutDetail();
                                    detail.Asset_Marking_ID = long.Parse(row.Cells[1].Text);
                                    detail.Asset_PurchaseDetail_ID = long.Parse(row.Cells[2].Text);
                                    detail.AssetTypeID = int.Parse(splitAsset[0].ToString());
                                    detail.SerialNo1 = "";
                                    detail.SerialNo2 = "";
                                    detail.Rate = decimal.Parse(dc.chkNull_0(row.Cells[7].Text));
                                    detail.Qty = decimal.Parse(dc.chkNull_0(row.Cells[8].Text));
                                    detail.Color = "";
                                    detail.MfgYear = "";
                                    detail.CompanyAssetNo = "";
                                    detail.ID = long.Parse(row.Cells[12].Text);
                                    detail.IsActive = true;
                                    detail.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                                    detail.User_ID = int.Parse(this.Session["UserId"].ToString());
                                    detail.TYPE_ID = chkDeliverCustomer.Checked == true && ddlCustomer.SelectedIndex > -1 ? 4 : 1;
                                    detail.IS_ISSUED = false;
                                    detail.ISSUED_DATE = DateTime.Now;
                                    detail.IS_DAMAGED = false;
                                    detail.ASSET_STOCK_ID = savedID;
                                    detail.REF_ID = long.Parse(row.Cells[14].Text);

                                    mController.InsertOrUpdateStockDetail(detail);
                                }
                            }
                        }

                        var remarks = txtRemarks.Text;

                        ClearAll();
                        CreatTable();
                        CreatTableNonSerial();
                        LoadAssetStockById();
                        chkSerial.Enabled = true;

                        ShowPopUpReport(savedID, CurrentWorkDate, drpDistributor, drpDistributor1, ddlCustomer,
                              remarks);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Grid is empty, cannot save');", true);
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
        PurchaseSKU.Columns.Add("ID", typeof(long));
        PurchaseSKU.Columns.Add("REF_ID", typeof(long));
        PurchaseSKU.Columns.Add("Purchase_Date", typeof(string));
        this.Session.Add("AssetPurchase", PurchaseSKU);
    }
    public void LoadAssets()
    {
        //DateTime CurrentWorkDate = Constants.DateNullValue;
        var PurchaseSKU = (DataTable)this.Session["AssetPurchase"];

        //DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        //foreach (DataRow dr in dtLocationInfo.Rows)
        //{
        //    if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue.ToString())
        //    {
        //        if (dr["MaxDayClose"].ToString().Length > 0)
        //        {
        //            CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
        //            break;
        //        }
        //    }
        //}

        var result = mController.SelectAssetsFromAssetNoMarking(int.Parse(drpDistributor.SelectedValue));

        if (result.Rows.Count > 0)
        {
            DataRow[] drFound = result.Select("IsSerialNoBased  = '" + true + "'");
            if (drFound != null && drFound.Length > 0)
            {
                result = drFound.CopyToDataTable();

                foreach (DataRow item in result.Rows)
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["Asset_Marking_ID"] = item["Asset_Marking_ID"].ToString();
                    dr["Asset_PurchaseDetail_ID"] = item["Asset_PurchaseDetail_ID"].ToString();
                    dr["AssetTypeID"] = item["AssetTypeID"].ToString();
                    dr["AssetTypeName"] = item["AssetTypeName"].ToString();
                    dr["SerialNo1"] = item["SerialNo1"].ToString().Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                    dr["SerialNo2"] = item["SerialNo2"].ToString().Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                    dr["Rate"] = item["Rate"].ToString();
                    dr["Color"] = item["Color"].ToString();
                    dr["MfgYear"] = item["MfgYear"].ToString();
                    dr["CompanyAssetNo"] = item["CompanyAssetNo"].ToString();
                    dr["ID"] = 0;
                    dr["REF_ID"] = item["ID"].ToString();
                    dr["Purchase_Date"] = item["Purchase_Date"].ToString();

                    PurchaseSKU.Rows.Add(dr);
                }
            }

            this.Session.Add("AssetPurchase", PurchaseSKU);
            this.LoadGird();
        }
        else
        {
            this.LoadGird();
        }
    }
    public void LoadAssetStockById()
    {
        var result = mController.SelectTransferOutAssetsByID(long.Parse(drpDocumentNo.SelectedValue));

        if (result.Rows.Count > 0)
        {
            bool isSerialNoBased = Convert.ToBoolean(result.Rows[0]["IsSerialNoBased"].ToString());

            if (isSerialNoBased == true)
            {
                var PurchaseSKU = (DataTable)this.Session["AssetPurchase"];

                foreach (DataRow item in result.Rows)
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["Asset_Marking_ID"] = item["Asset_Marking_ID"].ToString();
                    dr["Asset_PurchaseDetail_ID"] = item["Asset_PurchaseDetail_ID"].ToString();
                    dr["AssetTypeID"] = item["AssetTypeID"].ToString();
                    dr["AssetTypeName"] = item["AssetTypeName"].ToString();
                    dr["SerialNo1"] = item["SerialNo1"].ToString().Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                    dr["SerialNo2"] = item["SerialNo2"].ToString().Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");
                    dr["Rate"] = item["Rate"].ToString();
                    dr["Color"] = item["Color"].ToString();
                    dr["MfgYear"] = item["MfgYear"].ToString();
                    dr["CompanyAssetNo"] = item["CompanyAssetNo"].ToString();
                    dr["ID"] = item["ID"].ToString();
                    dr["REF_ID"] = item["ID"].ToString();
                    dr["Purchase_Date"] = item["Purchase_Date"].ToString();

                    PurchaseSKU.Rows.Add(dr);
                }

                this.Session.Add("AssetPurchase", PurchaseSKU);
                this.LoadGird();

                serialBasedRow.Visible = true;
                nonSerialBasedRow.Visible = false;
                nonSerialGridView.Visible = false;
                chkSerial.Checked = true;
            }
            else
            {
                int count = 0;
                var PurchaseSKU = (DataTable)this.Session["AssetPurchaseNonSerial"];

                foreach (DataRow item in result.Rows)
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["Asset_Marking_ID"] = 0;
                    dr["Asset_PurchaseDetail_ID"] = item["Asset_PurchaseDetail_ID"].ToString();
                    dr["AssetTypeID"] = item["AssetTypeID"].ToString() + "-" + count;
                    dr["AssetTypeName"] = item["AssetTypeName"].ToString();
                    dr["SerialNo1"] = "";
                    dr["SerialNo2"] = "";
                    dr["Rate"] = item["Rate"].ToString();
                    dr["Qty"] = Convert.ToDecimal(dc.chkNull_0(item["Qty"].ToString()));
                    dr["Color"] = "";
                    dr["MfgYear"] = "";
                    dr["CompanyAssetNo"] = "";
                    dr["ID"] = item["ID"].ToString();
                    dr["REF_ID"] = item["ID"].ToString();
                    dr["Purchase_Date"] = item["Purchase_Date"].ToString();

                    PurchaseSKU.Rows.Add(dr);
                    count++;
                }

                this.Session.Add("AssetPurchaseNonSerial", PurchaseSKU);
                this.LoadNonSerialGrid();
                chkSerial.Enabled = false;

                serialBasedRow.Visible = false;
                nonSerialBasedRow.Visible = true;
                nonSerialGridView.Visible = true;
                chkSerial.Checked = false;
            }

            drpDistributorType.SelectedValue = result.Rows[0]["DISTRIBUTOR_TYPE_ID"].ToString();

            LoadAssignedDistributorToWareHouse();

            foreach (GridViewRow item in GridAssetType.Rows)
            {
                var box = (CheckBox)item.Cells[0].FindControl("chkRow");
                box.Checked = true;
            }

            drpDistributor1.SelectedValue = result.Rows[0]["TO_LOCATION"].ToString();

        }
        else
        {
            this.LoadGird();
            this.LoadNonSerialGrid();
        }
    }
    protected void drpDistributorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAssignedDistributorToWareHouse();
        LoadCustomer();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
        CreatTable();
        LoadAssets();
        MAXDATE();

        LoadAssignedDistributorToWareHouse();
        LoadCustomer();
        LoadNonSerialAsset();
        CreatTableNonSerial();
        LoadNonSerialGrid();
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CreatTable();
        LoadGird();
        CreatTableNonSerial();
        LoadNonSerialGrid();

        if (drpDocumentNo.SelectedItem.Text == "New")
        {
            drpDistributor.Enabled = true;
            LoadAssets();
            LoadNonSerialAsset();
        }
        else
        {
            LoadAssetStockById();
            drpDistributor.Enabled = false;
        }
    }
    protected void chkDeliverCustomer_Changed(object sender, EventArgs e)
    {
        try
        {
            LoadAssignedDistributorToWareHouse();
            LoadCustomer();

            if (chkDeliverCustomer.Checked == true)
            {
                customerRow.Visible = true;
                issuanceRow.Visible = true;
            }
            else
            {
                customerRow.Visible = false;
                issuanceRow.Visible = false;
                drpDistributor1.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chkSerial_CheckChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkSerial.Checked == true)
            {
                serialBasedRow.Visible = true;
                nonSerialBasedRow.Visible = false;
                nonSerialGridView.Visible = false;
            }
            else
            {
                LoadNonSerialAsset();
                CreatTableNonSerial();
                LoadNonSerialGrid();
                nonSerialBasedRow.Visible = true;
                nonSerialGridView.Visible = true;
                serialBasedRow.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void CreatTableNonSerial()
    {
        var PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("Asset_Marking_ID", typeof(long));
        PurchaseSKU.Columns.Add("Asset_PurchaseDetail_ID", typeof(long));
        PurchaseSKU.Columns.Add("AssetTypeID", typeof(string));
        PurchaseSKU.Columns.Add("SerialNo1", typeof(string));
        PurchaseSKU.Columns.Add("AssetTypeName", typeof(string));
        PurchaseSKU.Columns.Add("SerialNo2", typeof(string));
        PurchaseSKU.Columns.Add("Rate", typeof(decimal));
        PurchaseSKU.Columns.Add("CompanyAssetNo", typeof(string));
        PurchaseSKU.Columns.Add("MfgYear", typeof(string));
        PurchaseSKU.Columns.Add("Color", typeof(string));
        PurchaseSKU.Columns.Add("Qty", typeof(string));
        PurchaseSKU.Columns.Add("ID", typeof(long));
        PurchaseSKU.Columns.Add("REF_ID", typeof(long));
        PurchaseSKU.Columns.Add("Purchase_Date", typeof(string));
        this.Session.Add("AssetPurchaseNonSerial", PurchaseSKU);
    }
    private void LoadNonSerialGrid()
    {
        var _purchaseSkus = (DataTable)Session["AssetPurchaseNonSerial"];

        if (_purchaseSkus != null)
        {
            grdViewNonSerial.DataSource = _purchaseSkus;
            grdViewNonSerial.DataBind();
            //txtTotalQuantity.Text = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", totalValue);
        }
    }
    protected void btnNonSerial_Click(object sender, EventArgs e)
    {
        try
        {
            var PurchaseSKU = (DataTable)this.Session["AssetPurchaseNonSerial"];

            if (DrpAsset.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select Asset');", true);
                DrpAsset.Focus();
                return;
            }

            if (decimal.Parse(dc.chkNull_0(txtQty.Text)) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter Qty');", true);
                txtQty.Focus();
                return;
            }
            foreach (GridViewRow item in grdViewNonSerial.Rows)
            {
                if (item.Cells[3].Text == DrpAsset.SelectedValue.ToString() && btnNonSerial.Text != "Update")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Cannot add duplicate Asset');", true);
                    DrpAsset.Focus();
                    return;
                }
            }

            var result = new DataTable();
            if (drpDocumentNo.Items.Count > 0 && drpDocumentNo.SelectedIndex != -1 && drpDocumentNo.SelectedItem.Text != "New")
            {
                result = mController.SelectTransferOutAssetsByID(long.Parse(drpDocumentNo.SelectedValue));
            }
            else
            {
                result = mController.SelectAssetsFromAssetNoMarking(int.Parse(drpDistributor.SelectedValue));
            }
            if (result.Rows.Count > 0)
            {
                DataRow[] drFound = result.Select("IsSerialNoBased  = '" + false + "'");
                if (drFound != null && drFound.Length > 0)
                {
                    DataTable dt = new DataTable();
                    dt = drFound.CopyToDataTable();

                    var splitAsset = DrpAsset.SelectedValue.Split('-');
                    DataRow[] assetDr = dt.Select("AssetTypeID = '" + splitAsset[0].ToString() + "'");
                    if (drFound != null && drFound.Length > 0)
                    {
                        if (Convert.ToDecimal(dc.chkNull_0(txtQty.Text)) > Convert.ToDecimal(dc.chkNull_0(drFound[0]["Purchase_Qty"].ToString())))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Entered Qty cannot be greater than Stock Qty');", true);
                            txtQty.Focus();
                            return;
                        }

                        if (btnNonSerial.Text == "Update")
                        {
                            DataRow dr = PurchaseSKU.Rows[RowNo];
                            dr["Asset_PurchaseDetail_ID"] = drFound[0]["Asset_PurchaseDetail_ID"].ToString();
                            dr["AssetTypeID"] = DrpAsset.SelectedValue.ToString();
                            dr["AssetTypeName"] = DrpAsset.SelectedItem.Text;
                            dr["SerialNo1"] = "";
                            dr["SerialNo2"] = "";
                            dr["Qty"] = Convert.ToDecimal(dc.chkNull_0(txtQty.Text));
                            dr["Rate"] = Convert.ToDecimal(dc.chkNull_0(drFound[0]["Rate"].ToString()));
                            dr["Color"] = "";
                            dr["MfgYear"] = "";
                            dr["Purchase_Date"] = drFound[0]["Purchase_Date"].ToString();
                            dr["Asset_Marking_ID"] = 0;
                            dr["CompanyAssetNo"] = "";
                            
                            dr["ID"] = grdViewNonSerial.Rows[RowNo].Cells[12].Text;
                            dr["REF_ID"] = dr["ID"].ToString();

                            btnNonSerial.Text = "Add";
                            DrpAsset.Enabled = true;
                        }

                        else
                        {
                            DataRow dr = PurchaseSKU.NewRow();
                            dr["Asset_PurchaseDetail_ID"] = drFound[0]["Asset_PurchaseDetail_ID"].ToString();
                            dr["AssetTypeID"] = DrpAsset.SelectedValue.ToString();
                            dr["AssetTypeName"] = DrpAsset.SelectedItem.Text;
                            dr["SerialNo1"] = "";
                            dr["SerialNo2"] = "";
                            dr["Qty"] = Convert.ToDecimal(dc.chkNull_0(txtQty.Text));
                            dr["Rate"] = Convert.ToDecimal(dc.chkNull_0(drFound[0]["Rate"].ToString()));
                            dr["Color"] = "";
                            dr["MfgYear"] = "";
                            dr["Purchase_Date"] = drFound[0]["Purchase_Date"].ToString();
                            dr["Asset_Marking_ID"] = 0;
                            dr["CompanyAssetNo"] = "";
                            dr["ID"] = 0;
                            dr["REF_ID"] = drFound[0]["ID"].ToString();
                            PurchaseSKU.Rows.Add(dr);
                        }
                    }
                 }
            }

            this.Session.Add("AssetPurchaseNonSerial", PurchaseSKU);
            this.LoadNonSerialGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void LoadNonSerialAsset()
    {
        DrpAsset.Items.Clear();
        DrpAsset.DataBind();

        try
        {
            var result = mController.SelectAssetsFromAssetNoMarking(int.Parse(drpDistributor.SelectedValue));
            if (result.Rows.Count > 0)
            {
                DataRow[] dr = result.Select("IsSerialNoBased  = '" + false + "' AND Purchase_Qty > 0");
                if (dr != null && dr.Length > 0)
                {
                    DataTable dt = new DataTable();
                    dt = dr.CopyToDataTable();
                    int count = 0;
                    foreach (DataRow r in dt.Rows)
                    {
                        r["AssetTypeName"] = Convert.ToString(r["Asset_Marking_ID"]) + " - " + Convert.ToString(r["AssetTypeName"])
                            + " (Qty: " + Convert.ToString(r["Purchase_Qty"]) + ")";

                        r["SerialNo2"] = r["AssetTypeID"].ToString() + "-" + count.ToString();

                        count++;
                    }
                    clsWebFormUtil.FillDropDownList(DrpAsset, dt, "SerialNo2", "AssetTypeName", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdNonSerial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int NewEditIndex = gvr.RowIndex;
            RowNo = NewEditIndex;
            if (DrpAsset.Items.Count > 0 && DrpAsset.SelectedIndex != -1)
            {
                DrpAsset.SelectedValue = grdViewNonSerial.Rows[NewEditIndex].Cells[3].Text;
            }

            txtQty.Text = grdViewNonSerial.Rows[NewEditIndex].Cells[8].Text.Trim().Replace("nbsp;", "").Replace("&nbsp;", "")
                                            .Replace("&amp;", "").Replace("amp;", "").Replace("&", "");

            if (drpDocumentNo.Items.Count > 0 && drpDocumentNo.SelectedIndex != -1 && drpDocumentNo.SelectedItem.Text != "New")
            {
                var result = mController.SelectTransferOutAssetsByID(long.Parse(drpDocumentNo.SelectedValue));

                if (result.Rows.Count > 0)
                {
                    bool isSerialNoBased = Convert.ToBoolean(result.Rows[0]["IsSerialNoBased"].ToString());

                    if (isSerialNoBased == false)
                    {
                        var splitAsset = grdViewNonSerial.Rows[NewEditIndex].Cells[3].Text.Split('-');
                        DataRow[] dr = result.Select("AssetTypeID  = '" + splitAsset[0].ToString() + "'");
                        if (dr != null && dr.Length > 0)
                        {
                            dr[0]["AssetTypeName"] = Convert.ToString(dr[0]["Asset_PurchaseDetail_ID"]) + " - " + Convert.ToString(dr[0]["AssetTypeName"])
                               + " (Qty: " + Convert.ToString(dr[0]["Purchase_Qty"]) + ")";

                            dr[0]["SerialNo2"] = dr[0]["AssetTypeID"].ToString() + "-1";

                            clsWebFormUtil.FillDropDownList(DrpAsset, dr.CopyToDataTable(), "SerialNo2", "AssetTypeName", true);

                            DrpAsset.SelectedValue = splitAsset[0].ToString() + "-1";
                        }
                    }
                }
            }

            btnNonSerial.Text = "Update";
            DrpAsset.Enabled = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void drpDistributor1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }
    public void ShowPopUpReport(long p_savedRecord_ID, DateTime p_workingDate, DropDownList distributor,
      DropDownList toLocation, DropDownList customer, string Remarks)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            RptInventoryController RptInventoryCtl = new RptInventoryController();

            SAMSBusinessLayer.Reports.DsReport ds = new SAMSBusinessLayer.Reports.DsReport();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(distributor.SelectedValue.ToString()));

            DataControl dc = new DataControl();
            DataTable result = mController.SelectAssetDetailByAssetStockID(p_savedRecord_ID);
            foreach (DataRow dr in result.Rows)
            {
                ds.Tables["RptAssetStockDetail"].ImportRow(dr);
            }

            ReportDocument CrpReport = new ReportDocument();
            
            CrpReport = new SAMSBusinessLayer.Reports.CrpStockRegisterByID();
           
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("FROM", distributor.SelectedItem.Text);
            CrpReport.SetParameterValue("TO", toLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("DocumentType", "Transfer Out Note");
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Remarks", Remarks);
            CrpReport.SetParameterValue("Date", p_workingDate);
            if (chkDeliverCustomer.Checked)
            {
                CrpReport.SetParameterValue("Customer", ddlCustomer.SelectedItem.Text);

                CustomerDataController DController = new CustomerDataController();
                DataTable dt1 = DController.SelectAllCustomer(int.Parse(drpDistributor1.SelectedValue), Constants.IntNullValue, Constants.IntNullValue);
                if (dt.Rows.Count > 0)
                {
                    DataRow[] foundRows = dt1.Select("IS_ACTIVE  = '" + true + "'");

                    if (foundRows != null && foundRows.Length > 0)
                    {
                        DataTable dt2 = foundRows.CopyToDataTable();
                        DataRow[] foundRows1 = dt2.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");
                        if (foundRows1 != null && foundRows1.Length > 0)
                        {
                            CrpReport.SetParameterValue("Route", foundRows1[0]["ROUTE_NAME"].ToString());
                            CrpReport.SetParameterValue("CUSTOMER_CODE", foundRows1[0]["CUSTOMER_CODE"].ToString());
                            CrpReport.SetParameterValue("Address", foundRows1[0]["ADDRESS"].ToString());
                        }
                    }
                    else
                    {
                        CrpReport.SetParameterValue("Customer", "");
                        CrpReport.SetParameterValue("Route", "");
                        CrpReport.SetParameterValue("CUSTOMER_CODE", "");
                        CrpReport.SetParameterValue("Address", "");
                    }
                }
                else
                {
                    CrpReport.SetParameterValue("Customer", "");
                    CrpReport.SetParameterValue("Route", "");
                    CrpReport.SetParameterValue("CUSTOMER_CODE", "");
                    CrpReport.SetParameterValue("Address", "");
                }

            }
            else
            {
                CrpReport.SetParameterValue("Customer", "");
                CrpReport.SetParameterValue("Route", "");
                CrpReport.SetParameterValue("CUSTOMER_CODE", "");
                CrpReport.SetParameterValue("Address", "");
            }


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