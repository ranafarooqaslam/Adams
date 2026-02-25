using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseOrderEntry : System.Web.UI.Page
{
    readonly SKUPriceDetailController _pController = new SKUPriceDetailController();
    readonly PurchaseController _mPurchase = new PurchaseController();

    readonly DataControl _dc = new DataControl();
    private static int _rowNo;
    DataTable _purchaseSku;

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadSkuDetail();
            CreatTable();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtPODate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
        }
    }

    private void CreatTable()
    {
        _purchaseSku = new DataTable();
        _purchaseSku.Columns.Add("PURCHASE_ORDER_DETAIL_ID", typeof(long));
        _purchaseSku.Columns.Add("SKU_ID", typeof(int));
        _purchaseSku.Columns.Add("SKU_Code", typeof(string));
        _purchaseSku.Columns.Add("SKU_Name", typeof(string));
        _purchaseSku.Columns.Add("Quantity", typeof(int));
        _purchaseSku.Columns.Add("QUANTITY_CTN", typeof(int));
        _purchaseSku.Columns.Add("PRICE", typeof(decimal));
        _purchaseSku.Columns.Add("AMOUNT", typeof(decimal));
        Session.Add("PurchaseSKU", _purchaseSku);

    }

    #region Check_Duplication
    
    //Stop replication for ref no
    private bool IsBillBookNoExist()
    {
        bool flag = false;
        if ((long.Parse(drpDocumentNo.SelectedValue) == Constants.LongNullValue || Session["hfBillBookNo"].ToString() != txtRefNo.Text) && txtRefNo.Text.Trim().Length > 0)
        {
            OrderEntryController OEC = new OrderEntryController();
            DataTable dtBillBookNo = OEC.SelectBillBookNo(int.Parse(drpDistributor.SelectedValue), txtRefNo.Text, 0);
            if (dtBillBookNo.Rows.Count > 0)
            {
                flag = true;
            }
        }
        return flag;
    }

    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
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
    #endregion

    #region Load
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DataTable dt = _mPurchase.SelectPurchaseOrderDocumentNo2(Constants.LongNullValue, int.Parse(drpDistributor.SelectedValue), 0, Convert.ToDateTime(txtPODate.Text), Constants.IntNullValue);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
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
        DataTable mDt = _pController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, 
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(drpPrincipal, mDt, 0, 1, true);
    }
    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        DataTable dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DataTable dt = _mPurchase.SelectPurchaseOrderDocumentNo(Convert.ToInt64(drpDocumentNo.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue), 0, Convert.ToDateTime(txtPODate.Text));
        if (dt.Rows.Count > 0)
        {
            txtRefNo.Text = dt.Rows[0]["REF_NO"].ToString();
            txtBuiltNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            _purchaseSku = _mPurchase.SelectPurchaseOrderDetail(long.Parse(dt.Rows[0][0].ToString()), Convert.ToInt32(drpDistributor.SelectedValue));
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
        }
    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        int totalValue = 0;
        decimal totalAmountFc = 0;
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = _purchaseSku;
        GrdPurchase.DataBind();
        foreach (DataRow dr in _purchaseSku.Rows)
        {
            totalValue += int.Parse(dr["Quantity"].ToString());
            totalAmountFc += Convert.ToDecimal(dr["AMOUNT"]);

        }

        txtTotalQuantity.Text = totalValue.ToString();
        txtAmountFC.Text = Math.Round(totalAmountFc, 3).ToString();
        //txtAmountRs.Text = Math.Round((TotalAmountFc * Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text))), 3).ToString();
    }
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
            DataTable dtskuPrice = _pController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue),
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(drpDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()),
                Constants.IntNullValue, 4, CurrentWorkDate, Constants.LongNullValue);
            clsWebFormUtil.FillDropDownList(ddlSKuCde, dtskuPrice, 0, 10, true);
            Session.Add("Dtsku_Price", dtskuPrice);
        }
    }

    #endregion

    #region Grid Operations
    
    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
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

    #endregion

    #region Click OPerations

   //Grid Operation
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataTable dtskuPrice = (DataTable)Session["Dtsku_Price"];
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");

       // int mPackSize = int.Parse(_dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));

        if (foundRows.Length > 0)
        {
            if (btnSave.Text == "Add Sku")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = _purchaseSku.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];



                    //if (RbUnitType.SelectedValue == "0")
                    //{
                        dr["PRICE"] = decimal.Parse(_dc.chkNull_0(txtRate.Text));

                        dr["Quantity"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                        dr["QUANTITY_CTN"] = 0;

                        dr["AMOUNT"] = decimal.Parse(txtRate.Text) * Convert.ToInt32(dr["Quantity"]);
                    //}
                    //else
                    //{
                    //    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;

                    //    dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                    //    decimal rate = Convert.ToDecimal(txtRate.Text);

                    //    decimal qty = Convert.ToDecimal(dr["Quantity"]);

                    //    dr["PRICE"] = Convert.ToDecimal((txtAmount.Text)) / (qty);

                    //    dr["AMOUNT"] = Convert.ToDecimal(txtAmount.Text.ToString());
                    //}


                    _purchaseSku.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('SKU Already Exists ');", true);
                    return;
                }
            }
            else if (btnSave.Text == "Update Sku")
            {
                DataRow dr = _purchaseSku.Rows[_rowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];

                //if (RbUnitType.SelectedValue == "0")
                //{
                    dr["PRICE"] = decimal.Parse(_dc.chkNull_0(txtRate.Text));

                    dr["Quantity"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));

                    dr["AMOUNT"] = decimal.Parse(txtRate.Text) * Convert.ToInt32(dr["Quantity"]);
                //}
                //else
                //{
                //    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;

                //    decimal rate = Convert.ToDecimal(txtRate.Text);

                //    decimal qty = Convert.ToDecimal(dr["Quantity"]);

                //    dr["PRICE"] = Convert.ToDecimal((txtAmount.Text)) / (qty);

                //    dr["AMOUNT"] = Convert.ToDecimal(txtAmount.Text.ToString());


                //}


            }
            Session.Add("PurchaseSKU", _purchaseSku);
            ClearAll();
            LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong SKU Select');", true);

        }
    }
   
    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        _purchaseSku = (DataTable)Session["PurchaseSKU"];
        if (_purchaseSku.Rows.Count > 0)
        {
            if (CalculatePurchase(DateTime.Parse(txtPODate.Text)))
            {
                _purchaseSku = (DataTable)Session["PurchaseSKU"];
                _purchaseSku.Rows.Clear();
                Session.Add("PurchaseSKU", _purchaseSku);
                LoadGird();
                LoadDistributor();
                LoadPrincipal();
                LoadSkuDetail();
                GetDocumentNo();

                ClearAll();

                txtBuiltNo.Text = "";
                txtRefNo.Text = "";
                DisAbaleOption(false);
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record save successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);

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
        CreatTable();
        LoadGird();
        ClearAll();
        txtRefNo.Text = "";
        txtBuiltNo.Text = "";
    }

    #endregion

    #region Index/Change
   

   

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            CreatTable();
            Session.Add("PurchaseSKU", _purchaseSku);
            LoadGird();
            ClearAll();
            txtBuiltNo.Text = "";
            txtRefNo.Text = "";
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            
        }
        else
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
           
            LoadDocumentDetail();
            LoadSkuDetail();
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        LoadSkuDetail();
        GetDocumentNo();
       
    }

    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    /// 
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        LoadSkuDetail();
    }

    #endregion

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchase(DateTime CWorkDate)
    {
        bool flag = false;
        DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKU"];
        decimal mTotalAmount = dtPurchaseDetail.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));
        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            if (!IsBillBookNoExist())
            {
                long mResult = _mPurchase.InsertPurchaseOrder(int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue), txtRefNo.Text
                , txtBuiltNo.Text, CWorkDate, mTotalAmount, int.Parse(Session["UserId"].ToString()), dtPurchaseDetail, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                if (mResult != -2)
                {
                    Print(mResult);
                    flag = true;
                }
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('This Ref No already exist,Kindly enter different Ref No');", true);
            }
        }

        else
        {
            long mResult = _mPurchase.UpdatePurchaseOrderDetail(int.Parse(drpDocumentNo.SelectedValue), int.Parse(drpDistributor.SelectedValue), txtRefNo.Text
            , txtBuiltNo.Text, mTotalAmount, int.Parse(Session["UserId"].ToString()), dtPurchaseDetail);

            if (mResult != -2)
            {
                
            
                Print(mResult);
                flag = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again !');", true);
            }
        }
        return flag;
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="isDisable">bool</param>
    private void DisAbaleOption(bool isDisable)
    {
        if (isDisable)
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
           
            txtRefNo.Enabled = false;
           
        }
        else
        {
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
            txtRefNo.Enabled = true;
           
        }
    }
    private void ClearAll()
    {
        
        txtQuantity.Text = "";
        txtRate.Text = "0.00";
        txtAmount.Text = "0.00";
        //txtskuCode.Text = "";
       // txtskuName.Text = "";
      
        btnSave.Text = "Add Sku";
        //ddlSKuCde.SelectedIndex = 0;
    }
    private void Print(long mResult)
    {
        var mController = new DocumentPrintController();
        var rptInventoryCtl = new RptInventoryController();

        var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument2();
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = rptInventoryCtl.SelectPurchaseDocument(mResult, int.Parse(drpDistributor.SelectedValue), int.Parse(drpPrincipal.SelectedValue), DateTime.Parse(txtPODate.Text), DateTime.Parse(txtPODate.Text), 1);
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("DocumentType", "Purchase Order" + " Document");
        crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        //const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        //Type cstype = GetType();
        //ClientScriptManager cs = Page.ClientScript;
        //cs.RegisterStartupScript(cstype, "OpenWindow", script);
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
    }
    protected void txtPODate_TextChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
    }
    protected void GrdPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        _rowNo = NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[NewEditIndex].Cells[3].Text;
        txtRate.Text = GrdPurchase.Rows[NewEditIndex].Cells[4].Text;
        txtAmount.Text = GrdPurchase.Rows[NewEditIndex].Cells[5].Text;        
        txtQuantity.Focus();
        btnSave.Text = "Update Sku";
    }
}