using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmReturnableReplaceDispatch : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    DataControl dc = new DataControl();
    PhaysicalStockController mController = new PhaysicalStockController();
    private static int RowNo;
    
    private static int PrivouseQty, FreePrivousQty;
    DataTable PurchaseSKU;

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();            
            this.CreatTable();
            this.GetDocumentNo();
            DrpDocumentType_SelectedIndexChanged(null, null);
        }
    }

    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("CurrentDispatchQty", typeof(int));
        this.Session.Add("PurchaseSKU", PurchaseSKU);
    }
    private bool CalculatePurchase(DateTime MWorkDate,DataTable dtDispatchDetail)
    {
        decimal mTotalAmount = 0;
        int typeID = 15;
        string Voucher_No = null;
        var mController = new PurchaseController();
        foreach (DataRow dr in dtDispatchDetail.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());
        }
        //Returnable Replace Send
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            long DocumentNO = mController.InsertPurchaseDocumentReturnable(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
            , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
            , mTotalAmount, false, dtDispatchDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(ddlAccountHead.SelectedValue), ddlAccountHead.SelectedItem.Text, out Voucher_No);
            if (DocumentNO != Constants.LongNullValue)
            {
                PrintVoucher(Voucher_No);
                PrintReport(DocumentNO, typeID, DrpDocumentType.SelectedItem.Text);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            bool mResult = mController.UpdatePurchaseDocumentReturnable(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
            , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
            , mTotalAmount, false, dtDispatchDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(ddlAccountHead.SelectedValue), ddlAccountHead.SelectedItem.Text, out Voucher_No);
            PrintReport(int.Parse(drpDocumentNo.SelectedValue), typeID, DrpDocumentType.SelectedItem.Text);
            PrintVoucher(Voucher_No);
            return mResult;
        }
    }
    private int CheckStockStatus(int SKU_ID)
    {
        return -1;
    }
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
        }
        else
        {
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }

    #region Load
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        int type = 15;
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(type, Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
    }
    /// <summary>
    /// Loads Principals To Principal Comb
    /// </summary>
    private void LoadPrincipal()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, 
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, 0, 1, true);
    }
    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }

    /// <summary>
    /// Loads Locations To Location To Combo
    /// </summary>
    private void LoadToDistributor()
    {
        DrpTransferFor.Items.Clear();
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, 6, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpTransferFor, dt, 0, 2);        
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            txtDocumentNo.Text = dt.Rows[0][2].ToString();
            txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            PurchaseSKU = mPurchase.SelectPurchaseDetail(long.Parse(dt.Rows[0][0].ToString()),Convert.ToInt16(DrpTransferFor.SelectedValue),Convert.ToInt16(drpDistributor.SelectedValue));
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        int TotalValue = 0;
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalValue += int.Parse(dr["CurrentDispatchQty"].ToString());

        }
        txtTotalQuantity.Text = TotalValue.ToString();
    }

    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtHead, 0, 11);
    }

    private void LoadPendingDispatch()
    {
        PurchaseController mPurchase = new PurchaseController();
        PurchaseSKU = mPurchase.SelectPurchaseOrderDocumentNo2(Constants.LongNullValue, Convert.ToInt32(DrpTransferFor.SelectedValue), 3, Constants.DateNullValue, Convert.ToInt32(drpDistributor.SelectedValue));
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        LoadGird();
    }
    
    #endregion

    #region Sel/Index Change

    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblAccountHead.Visible = false;
        ddlAccountHead.Visible = false;
        ddlAccountHead.Items.Clear();
        lbltoLocation.Text = "Principal";
        lblfromLocation.Text = "Return From";
        drpDistributor.Enabled = true;
        drpPrincipal.Enabled = true;
        this.LoadToDistributor();
        LoadPendingDispatch();
        this.DrpTransferFor.Visible = true;
        Label4.Visible = true;
        Label4.Text = "Distributor";
        lblAccountHead.Visible = true;
        ddlAccountHead.Visible = true;
        this.LoadAccountDetail();
        this.GetDocumentNo();
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTable();
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            this.ClearAll();
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
        }
        else
        {
            txtBuiltyNo.Text = "";
            txtDocumentNo.Text = "";
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            this.LoadDocumentDetail();
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadToDistributor();
    }
    protected void DrpTransferFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPendingDispatch();
    }
    #endregion

    #region Click Operations

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        
        DataTable dtDispatchSKU = new DataTable();
        dtDispatchSKU.Columns.Add("SKU_ID", typeof(int));
        dtDispatchSKU.Columns.Add("BATCH_NO", typeof(string));
        dtDispatchSKU.Columns.Add("PRICE", typeof(decimal));
        dtDispatchSKU.Columns.Add("QUANTITY", typeof(int));
        dtDispatchSKU.Columns.Add("FREE_SKU", typeof(int));
        dtDispatchSKU.Columns.Add("AMOUNT", typeof(decimal));

        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }

        int i = 0;
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtCurrentDispatchQty = gvr.FindControl("txtCurrentDispatchQty") as TextBox;
            TextBox txtRemainingDispatchQty = gvr.FindControl("txtRemainingDispatchQty") as TextBox;
            TextBox txtPrevDispatchQty = gvr.FindControl("txtPrevDispatchQty") as TextBox;

            int OriginalQty = Convert.ToInt32(dc.chkNull_0(txtRemainingDispatchQty.Text));
            int PrevDispatchQty = Convert.ToInt32(dc.chkNull_0(txtPrevDispatchQty.Text));
            if (txtCurrentDispatchQty.Text.Length > 0)
            {
                if (Convert.ToInt32(dc.chkNull_0(txtCurrentDispatchQty.Text)) > OriginalQty + PrevDispatchQty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Dispatch quantity should not be greater then received quantity.');", true);
                    return;
                }
                i += 1;
            }

            DataTable dtstock = mController.SelectSKUClosingStock(
                int.Parse(drpDistributor.SelectedValue),
                int.Parse(gvr.Cells[0].Text.ToString()),
                "", CurrentWorkDate);

            if (dtstock.Rows.Count > 0)
            {
                if (int.Parse(dtstock.Rows[0][0].ToString()) < Convert.ToInt32(dc.chkNull_0(txtCurrentDispatchQty.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + gvr.Cells[1].Text.ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + gvr.Cells[1].Text.ToString() + "No Stock Found');", true);
                return;
            }
        }
        
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtCurrentDispatchQty = gvr.FindControl("txtCurrentDispatchQty") as TextBox;
            if (txtCurrentDispatchQty.Text.Length > 0)
            {
                DataRow dr = dtDispatchSKU.NewRow();
                dr["SKU_ID"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[0].Text));
                dr["BATCH_NO"] = "N/A";
                dr["FREE_SKU"] = 0;
                dr["Quantity"] = Convert.ToInt32(dc.chkNull_0(txtCurrentDispatchQty.Text));
                dr["PRICE"] = Convert.ToDecimal(dc.chkNull_0(gvr.Cells[7].Text));
                dr["AMOUNT"] = Convert.ToDecimal(dc.chkNull_0(gvr.Cells[7].Text)) * Convert.ToDecimal(dc.chkNull_0(txtCurrentDispatchQty.Text));
                dtDispatchSKU.Rows.Add(dr);
            }
        }
        if (dtDispatchSKU.Rows.Count > 0)
        {
            if (CurrentWorkDate != Constants.DateNullValue)
            {
                if (CalculatePurchase(CurrentWorkDate, dtDispatchSKU))
                {
                    PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
                    PurchaseSKU.Rows.Clear();
                    this.Session.Add("PurchaseSKU", PurchaseSKU);
                    this.LoadGird();
                    this.GetDocumentNo();
                    LoadPendingDispatch();
                    drpDistributor.Enabled = true;
                    drpPrincipal.Enabled = true;
                    this.ClearAll();
                    txtBuiltyNo.Text = "";
                    txtDocumentNo.Text = "";
                    txtDocumentNo.Text = "";
                    DisAbaleOption(false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Try Again');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Day close not found for Location');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' At least one SKU must enter');", true);

        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        this.CreatTable();
        this.LoadGird();
        this.ClearAll();
        txtDocumentNo.Text = "";
        txtBuiltyNo.Text = "";
        txtDocumentNo.Text = "";
    }

    #endregion

    private void ClearAll()
    {
        PrivouseQty = 0;
        FreePrivousQty = 0;
    }

    private void PrintReport(long p_Document_ID, int p_typeID, string p_DocumentTitle)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }

        if (p_typeID == 15)
        {
            var mController = new DocumentPrintController();
            var rptInventoryCtl = new RptInventoryController();
            var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument4();
            DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            DataSet ds = rptInventoryCtl.SelectPurchaseDocument(p_Document_ID, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, CurrentWorkDate, CurrentWorkDate, p_typeID);
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Document");
            crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        }
        else
        {
            var mController = new DocumentPrintController();
            var rptInventoryCtl = new RptInventoryController();
            var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument3();
            var dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
            var ds = rptInventoryCtl.SelectPurchaseDocument(p_Document_ID, int.Parse(drpDistributor.SelectedValue),
                Constants.IntNullValue, CurrentWorkDate, CurrentWorkDate, p_typeID);
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("DocumentType", p_DocumentTitle + " Document");
            if (p_typeID == 15 || p_typeID == 16)
            {
                crpReport.SetParameterValue("Principal", "Trade Price");
            }
            else
            {
                crpReport.SetParameterValue("Principal", "Purchase Price");
            }
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            this.Session.Add("CrpReport", crpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        }
    }

    private void PrintVoucher(string p_Voucher_No)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint2(int.Parse(drpDistributor.SelectedValue), p_Voucher_No, Constants.Journal_Voucher);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        this.Session.Add("CrpReport2", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default2.aspx'";
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
    }    
}