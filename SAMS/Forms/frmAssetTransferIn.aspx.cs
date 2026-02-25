using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_frmAssetTransferIn : System.Web.UI.Page
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
            LoadToLocation();
            LoadDocumentNo();
            CreatTable();
            LoadAssets();
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
                if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor1.SelectedValue.ToString())
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
        drpDistributor.Enabled = false;
        Session.Add("dtLocationInfo", dt);
    }
    private void LoadToLocation()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor1, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
        drpDistributor1.Enabled = false;
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

                var typeId = "4";
                if (GridAssetType.Rows.Count > 0)
                {
                    GridViewRow defaultRow = GridAssetType.Rows[0];
                    typeId = defaultRow.Cells[10].Text;
                }

                if (drpDistributor1.SelectedValue == drpDistributor.SelectedValue && typeId != "5")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('From & To Location cannot be same');", true);
                    drpDistributor.Focus();
                    return;
                }

                DateTime CurrentWorkDate = Constants.DateNullValue;
                DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
                foreach (DataRow dr in dtLocationInfo.Rows)
                {
                    if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor1.SelectedValue.ToString())
                    {
                        if (dr["MaxDayClose"].ToString().Length > 0)
                        {
                            CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                            break;
                        }
                    }
                }

                var transferDate = !string.IsNullOrEmpty(txtTransferDate.Text) ? Convert.ToDateTime(txtTransferDate.Text) : Constants.DateNullValue;
                //if (transferDate != null && CurrentWorkDate < transferDate)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Transfer In Date cannot be greater than Location's Working Date');", true);
                //    CEEndDate.Focus();
                //    return;
                //}

                foreach (GridViewRow row in GridAssetType.Rows)
                {
                    var transferOutDate = row.Cells[9].Text;
                    if (!string.IsNullOrEmpty(transferOutDate))
                    {
                        var transferOutDateConverted = Convert.ToDateTime(transferOutDate);

                        if (transferDate < transferOutDateConverted)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Transfer In Date should be greater than or equal to Transfer Out Date');", true);
                            CEEndDate.Focus();
                            return;
                        }
                    }
                }

                if (GridAssetType.Rows.Count > 0)
                {
                    spAssetTransferOut asset = new spAssetTransferOut();
                    asset.DOCUMENT_DATE = CurrentWorkDate;
                    asset.FROM_LOCATION = int.Parse(drpDistributor1.SelectedValue);
                    asset.TO_LOCATION = int.Parse(drpDistributor.SelectedValue);
                    asset.TYPE_ID = 2;
                    asset.IsActive = true;
                    asset.Remarks = txtRemarks.Text;
                    asset.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                    asset.User_ID = int.Parse(this.Session["UserId"].ToString());
                    asset.Transfer_In_Date = transferDate;
                    asset.Transfer_Date = Constants.DateNullValue;
                    asset.Return_Date = Constants.DateNullValue;
                    asset.Damage_Date = Constants.DateNullValue;

                    long savedID = mController.InsertAssetStock(asset);

                    if (savedID > 0)
                    {
                        foreach (GridViewRow row in GridAssetType.Rows)
                        {
                            spAssetTransferOutDetail detail = new spAssetTransferOutDetail();
                            detail.Asset_Marking_ID = long.Parse(row.Cells[0].Text);
                            detail.Asset_PurchaseDetail_ID = long.Parse(row.Cells[11].Text);
                            detail.Qty = decimal.Parse(row.Cells[12].Text);
                            detail.AssetTypeID = int.Parse(row.Cells[1].Text);
                            detail.SerialNo1 = Server.HtmlDecode(row.Cells[3].Text);
                            detail.SerialNo2 = Server.HtmlDecode(row.Cells[4].Text);
                            detail.Rate = decimal.Parse(dc.chkNull_0(row.Cells[5].Text));
                            detail.Color = Server.HtmlDecode(row.Cells[6].Text);
                            detail.MfgYear = Server.HtmlDecode(row.Cells[7].Text);
                            detail.CompanyAssetNo = Server.HtmlDecode(row.Cells[8].Text);
                            detail.IsActive = true;
                            detail.CompanyID = int.Parse(this.Session["CompanyId"].ToString());
                            detail.User_ID = int.Parse(this.Session["UserId"].ToString());
                            detail.TYPE_ID = 2;
                            detail.IS_ISSUED = false;
                            detail.ISSUED_DATE = Constants.DateNullValue;
                            detail.IS_DAMAGED = false;
                            detail.ASSET_STOCK_ID = savedID;
                            detail.REF_ID = long.Parse(row.Cells[13].Text);

                            mController.InsertOrUpdateStockDetail(detail);
                        }

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Record added successfully.');", true);

                        var remarks = txtRemarks.Text;

                        ClearAll();
                        CreatTable();
                        LoadDocumentNo();
                        LoadAssets();

                        ShowPopUpReport(savedID, transferDate, drpDistributor, drpDistributor1, remarks);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "alert('Grid is empty, cannot save');", true);
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
        PurchaseSKU.Columns.Add("Qty", typeof(decimal));
        PurchaseSKU.Columns.Add("CompanyAssetNo", typeof(string));
        PurchaseSKU.Columns.Add("MfgYear", typeof(string));
        PurchaseSKU.Columns.Add("Color", typeof(string));
        PurchaseSKU.Columns.Add("Transfer_Date", typeof(string));
        PurchaseSKU.Columns.Add("TYPE_ID", typeof(string));
        PurchaseSKU.Columns.Add("REF_ID", typeof(long));
        this.Session.Add("AssetPurchase", PurchaseSKU);

    }
    public void LoadDocumentNo()
    {
        try
        {
            var userId = int.Parse(this.Session["UserId"].ToString());

            //DateTime CurrentWorkDate = Constants.DateNullValue;
            //DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
            //foreach (DataRow dr in dtLocationInfo.Rows)
            //{
            //    if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor1.SelectedValue.ToString())
            //    {
            //        if (dr["MaxDayClose"].ToString().Length > 0)
            //        {
            //            CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
            //            break;
            //        }
            //    }
            //}

            var result = mController.SelectTransferOutAssets(Constants.DateNullValue, userId, Constants.LongNullValue);

            if (result.Rows.Count > 0)
            {
                //var x = (from r in result.AsEnumerable() select r["ASSET_STOCK_ID"]).Distinct().ToList();
                //var dt = ConvertListToDataTable.ToDataTable<object>(x);

                DataTable cdt = new DataTable();
                cdt.Columns.Add("ASSET_STOCK_ID");
                cdt.Columns.Add("ASSET_STOCK_NAME");

                foreach (DataRow item in result.Rows)
                {
                    DataRow dr = cdt.NewRow();
                    dr["ASSET_STOCK_ID"] = item["ASSET_STOCK_ID"].ToString();
                    dr["ASSET_STOCK_NAME"] = item["ASSET_STOCK_ID"].ToString() + " - " +
                        item["FROM_LOCATION_NAME"].ToString() + " ~ " + item["TO_LOCATION_NAME"].ToString() + " - (" +
                        item["Transfer_Date"].ToString() + ")";
                    cdt.Rows.Add(dr);
                }
                DataView view = new DataView(cdt);
                DataTable distinctValues = view.ToTable(true);

                clsWebFormUtil.FillDropDownList(this.drpDocumentNo, distinctValues, "ASSET_STOCK_ID", "ASSET_STOCK_NAME", true);   
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadAssets()
    {
        var PurchaseSKU = (DataTable)this.Session["AssetPurchase"];

        var userId = int.Parse(this.Session["UserId"].ToString());
        //DateTime CurrentWorkDate = Constants.DateNullValue;
        //DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        //foreach (DataRow dr in dtLocationInfo.Rows)
        //{
        //    if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor1.SelectedValue.ToString())
        //    {
        //        if (dr["MaxDayClose"].ToString().Length > 0)
        //        {
        //            CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
        //            break;
        //        }
        //    }
        //}

        if (!string.IsNullOrEmpty(drpDocumentNo.SelectedValue))
        {
            var result = mController.SelectTransferOutAssets(Constants.DateNullValue, userId, long.Parse(drpDocumentNo.SelectedValue));

            if (result.Rows.Count > 0)
            {

                if (result.Rows.Count > 0)
                {
                    drpDistributor.SelectedValue = result.Rows[0]["FROM_LOCATION"].ToString();
                    drpDistributor1.SelectedValue = result.Rows[0]["TO_LOCATION"].ToString();
                }

                foreach (DataRow item in result.Rows)
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["Asset_Marking_ID"] = item["Asset_Marking_ID"].ToString();
                    dr["Asset_PurchaseDetail_ID"] = item["Asset_PurchaseDetail_ID"].ToString();
                    dr["Qty"] = item["Qty"].ToString();
                    dr["AssetTypeID"] = item["AssetTypeID"].ToString();
                    dr["AssetTypeName"] = item["AssetTypeName"].ToString();
                    dr["SerialNo1"] = Server.HtmlDecode(item["SerialNo1"].ToString());
                    dr["SerialNo2"] = Server.HtmlDecode(item["SerialNo2"].ToString());
                    dr["Rate"] = item["Rate"].ToString();
                    dr["Color"] = Server.HtmlDecode(item["Color"].ToString());
                    dr["MfgYear"] = item["MfgYear"].ToString();
                    dr["CompanyAssetNo"] = Server.HtmlDecode(item["CompanyAssetNo"].ToString());
                    dr["Transfer_Date"] = item["Transfer_Date"].ToString();
                    dr["TYPE_ID"] = item["TYPE_ID"].ToString();
                    dr["REF_ID"] = item["ID"].ToString();

                    PurchaseSKU.Rows.Add(dr);
                }

                this.Session.Add("AssetPurchase", PurchaseSKU);
                this.LoadGird();
            }
            else
            {
                this.LoadGird();
            }
        }
        else
        {
            this.LoadGird();
        }
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CreatTable();
        LoadGird();
        LoadAssets();
        MAXDATE();
    }
    public void ShowPopUpReport(long p_savedRecord_ID, DateTime p_workingDate, DropDownList distributor,
     DropDownList toLocation, string Remarks)
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

            CrpReport = new SAMSBusinessLayer.Reports.CrpStockRegisterByIDTransferIn();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("FROM", distributor.SelectedItem.Text);
            CrpReport.SetParameterValue("TO", toLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("DocumentType", "Transfer In Note");
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Remarks", Remarks);
            CrpReport.SetParameterValue("Date", p_workingDate);


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