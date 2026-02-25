using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports;
using SAMSCommon.Classes;

/// <summary>
/// From To Adjust Stock
/// </summary>
public partial class Forms_frmOpeningStock : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptInventoryController _rptInventoryCtl = new RptInventoryController();
    readonly RptAccountController _rptAccountCtl = new RptAccountController();
    PhaysicalStockController mController = new PhaysicalStockController();
    readonly DataControl _dc = new DataControl();
    DataControl dc = new DataControl();
    private static int _rowNo;
    private static int PrivouseQty, FreePrivousQty; 
    DataTable _purchaseSku;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreatTable();
            LoadAccountDetail();
            GetDocumentNo();
            LoadDistributor();
            LoadPrincipal();
            LoadSkuDetail();
        }
    }

    /// <summary>
    /// Creates Datatable For Document
    /// </summary>
    private void CreatTable()
    {
        _purchaseSku = new DataTable();
        _purchaseSku.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        _purchaseSku.Columns.Add("SKU_ID", typeof(int));
        _purchaseSku.Columns.Add("SKU_Code", typeof(string));
        _purchaseSku.Columns.Add("SKU_Name", typeof(string));
        _purchaseSku.Columns.Add("BATCH_NO", typeof(string));
        _purchaseSku.Columns.Add("PRICE", typeof(decimal));
        _purchaseSku.Columns.Add("Quantity", typeof(int));
        _purchaseSku.Columns.Add("FREE_SKU", typeof(int));
        _purchaseSku.Columns.Add("AMOUNT", typeof(decimal));
        Session.Add("PurchaseSKU", _purchaseSku);
    }

    /// <summary>
    /// Gets Document Nos
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
        lblAccountHead.Visible = true;
        ddlAccountHead.Visible = true;
        if (DrpDocumentType.SelectedValue == Constants.Document_Opening.ToString())
        {
            lblAccountHead.Visible = false;
            ddlAccountHead.Visible = false;
        }
    }

    /// <summary>
    /// Gets Document Nos
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        
        var mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);

       
    }

    /// <summary>
    /// Loads Document Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            CreatTable();
            LoadGird();
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            DrpDocumentType.Enabled = true;
            ClearAll();
            txtDocumentNo.Text = "";
            DisAbaleOption(false);
        }
        else
        {
            LoadDocumentDetail();
            LoadSkuDetail();
        }
    }

    /// <summary>
    /// Loads Principal Combo
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

        DataTable mDt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 
            int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0,CurrentWorkDate, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(drpPrincipal, mDt, 0, 1, true);
    }

    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
    }    
    
    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }

    /// <summary>
    /// Loads Document Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSkuDetail();
    }    
    
    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadSkuDetail()
    {
        if (drpPrincipal.Items.Count > 0)
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
            DataTable dtskuPrice = _pController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue.ToString()),
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()),
                Constants.IntNullValue, 2, CurrentWorkDate, Constants.LongNullValue);
            clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 9, true);
            Session.Add("Dtsku_Price", dtskuPrice);
        }
    }
       
    private void LoadGird()
    {
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = _purchaseSku;
        GrdPurchase.DataBind();
    }

    /// <summary>
    /// Deletes Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    ///     
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        if (_purchaseSku.Rows.Count > 0)
        {
            _purchaseSku.Rows.RemoveAt(e.RowIndex);
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
        }
    }
        
    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        var mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
            drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            txtDocumentNo.Text = dt.Rows[0][2].ToString();
            _purchaseSku = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
        }
    }

    /// <summary>
    /// Checks Duplicate SKU in Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSku()
    {
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
       // DataRow[] foundRows = PurchaseSKU.Select("SKU_CODE  = '" + txtskuCode.Text + "'");
        DataRow[] foundRows = _purchaseSku.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }    
      
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>  
    protected void btnSave_Click(object sender, EventArgs e)
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
        DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length > 0)
        {         
            #region check Stock Limit
            if (int.Parse (DrpDocumentType.SelectedValue)== 8)// short
            {
                DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue), int.Parse(ddlSKuCde.SelectedValue.ToString()), "",CurrentWorkDate);
                if (dtstock.Rows.Count > 0)
                {
                    if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
                    {

                        if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dc.chkNull_0(txtQuantity.Text)) + int.Parse(dc.chkNull_0(txtFreeSKU.Text)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + foundRows[0]["SKU_CODE"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                            return;
                        }
                    }
                    else
                    {

                        if ((int.Parse(dtstock.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty) < int.Parse(dc.chkNull_0(txtQuantity.Text)) + int.Parse(dc.chkNull_0(txtFreeSKU.Text)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + foundRows[0]["SKU_CODE"].ToString() + " Current Stock is " + (int.Parse(dtstock.Rows[0][0].ToString()) + FreePrivousQty + PrivouseQty).ToString() + "');", true);
                            return;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + foundRows[0]["SKU_CODE"].ToString() + "No Stock Found');", true);
                    return;
                }
            }
            #endregion

            if (btnSave.Text == "Add Sku")
            {
                if (CheckDublicateSku())
                {
                    DataRow dr = _purchaseSku.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["BATCH_NO"] = txtBatchNo.Text;
                    dr["FREE_SKU"] = int.Parse(_dc.chkNull_0(txtFreeSKU.Text));
                    dr["PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                    dr["Quantity"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                    dr["AMOUNT"] = decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
                    _purchaseSku.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('SKU Already Exists')", true); 
                    return;
                }
            }
            else if (btnSave.Text == "Update Sku")
            {
                    DataRow dr = _purchaseSku.Rows[_rowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["BATCH_NO"] = txtBatchNo.Text;
                    dr["PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                    dr["Quantity"] = int.Parse(txtQuantity.Text);
                    dr["FREE_SKU"] = int.Parse(_dc.chkNull_0(txtFreeSKU.Text));
                    dr["AMOUNT"] = decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));
            }
            Session.Add("PurchaseSKU", _purchaseSku);
            ClearAll();
            LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong SKU please check in list');", true); 
        }
    }    
     
    /// <summary>
    /// Saves Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();

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

        if (CurrentWorkDate != Constants.DateNullValue)
        {
                PurchaseController mController = new PurchaseController();
                DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKU"];
                decimal mTotalAmount = dtPurchaseDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));

            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
                {
                    string voucherNo;
                    long mresult = mController.InsertPurchaseDocument3(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                          , CurrentWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(ddlAccountHead.SelectedValue), ddlAccountHead.SelectedItem.Text, out voucherNo);

                    if (mresult == -2)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again !');", true);
                    }
                    else
                    {
                        if (DrpDocumentType.SelectedValue == "7")
                        {
                            PrintReport(voucherNo);
                        }
                        else
                        {

                            PrintReport(voucherNo);
                            Print(mresult);
                        }
                    }
                }
                else
                {
                    bool mResult = mController.UpdatePurchaseDocumentStockAdjustment(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                       , CurrentWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                       , mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()),Convert.ToInt32(ddlAccountHead.SelectedValue), ddlAccountHead.SelectedItem.Text);
                    if (!mResult)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again !');", true);
                    }
                }
                _purchaseSku = (DataTable)Session["PurchaseSKU"];
                _purchaseSku.Rows.Clear();
                Session.Add("PurchaseSKU", _purchaseSku);
                LoadGird();
                GetDocumentNo();
                drpDistributor.Enabled = true;
                drpPrincipal.Enabled = true;
                DrpDocumentType.Enabled = true;
                ClearAll();
                txtDocumentNo.Text = "";
                DisAbaleOption(false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location or Unassigned');", true);
        }
    }
   
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CreatTable();
        LoadGird();
        drpDistributor.Enabled = true;
        drpPrincipal.Enabled = true;
        DrpDocumentType.Enabled = true;
        ClearAll();
        txtDocumentNo.Text = "";
        DisAbaleOption(false);
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="IsDisable">bool</param>
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            DrpDocumentType.Enabled = false;
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;

        }
        else
        {

            DrpDocumentType.Enabled = true;
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
            
        }
    }

    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(ddlAccountHead, dtHead, 0, 11, true);
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
       // txtskuCode.Text = "";
       // txtskuName.Text = "";
        
        txtQuantity.Text = "";
        txtFreeSKU.Text = "0";
        txtBatchNo.Text = "N/A";
        ddlSKuCde.Enabled = true;
        btnSave.Text = "Add Sku";
    }
    private void PrintReport(string p_Voucher_No)
    {
        var crpReport = new crpVoucherView();
        
        DataSet ds = null;
        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = _rptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), p_Voucher_No, Constants.Journal_Voucher);
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        crpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
       // string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
       // Type cstype = GetType();
       // ClientScriptManager cs = Page.ClientScript;
        //cs.RegisterStartupScript(cstype, "OpenWindow", script);
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
    }
    private void Print(long mResult)
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
        var crpReport = new CrpPurchaseDocument2();
        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = _rptInventoryCtl.SelectPurchaseDocument(mResult, int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue), CurrentWorkDate, DateTime.Parse(Session["CurrentWorkDate"].ToString()),int.Parse(DrpDocumentType.SelectedValue));
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Document");
        crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport2", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default2.aspx'";
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
    }
    protected void GrdPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        _rowNo = NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[NewEditIndex].Cells[3].Text;
        PrivouseQty = int.Parse(GrdPurchase.Rows[NewEditIndex].Cells[3].Text);
        FreePrivousQty = int.Parse(GrdPurchase.Rows[NewEditIndex].Cells[4].Text);
        txtFreeSKU.Text = GrdPurchase.Rows[NewEditIndex].Cells[4].Text;
        txtBatchNo.Text = GrdPurchase.Rows[NewEditIndex].Cells[5].Text;
        ddlSKuCde.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update Sku";
    }
}