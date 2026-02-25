using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports;
using SAMSCommon.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseReceivedEntry: System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly RptInventoryController _rptInventoryCtl = new RptInventoryController();
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptAccountController _rptAccountCtl = new RptAccountController();
    readonly PurchaseController _purchaseCtrl = new PurchaseController();

    DataTable _orderedSku;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            GetDocumentNo();
            LoadDocumentDetail();
            txtDispatchDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtDispatchDate.Attributes.Add("readonly", "readonly");
        }
    }

    #region Load
    
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2,true);
        Session.Add("dtLocationInfo", dt);
    }
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();        
        DataTable dt = _purchaseCtrl.SelectPurchaseOrderDocumentNo2(Constants.LongNullValue, Convert.ToInt32(drpDistributor.SelectedValue), 1, Constants.DateNullValue, Constants.IntNullValue);
        drpDocumentNo.Items.Add(new ListItem("-------Select-------", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 12, 0, false);
    }
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        bool flag = true;
        
        if (drpDocumentNo.Items.Count > 0)
        {
            if (drpDocumentNo.SelectedItem.Text != "-------Select-------")
            {
              var arr = drpDocumentNo.SelectedItem.Text.Split('-');
              var arr2 = drpDocumentNo.SelectedItem.Value.Split('-');                
              DataTable dt = _purchaseCtrl.SelectPurchaseOrderDocumentNo2(Convert.ToInt64(arr[0]), Constants.IntNullValue, 1, Constants.DateNullValue, Constants.IntNullValue);            
                if (dt.Rows.Count > 0)
            {
                    drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
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
                    
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(arr2[1]) != "")
                    {
                        var arr3 = Convert.ToString(dr["PURCHASE_MASTER_ID"]).Split('-');

                        if (Convert.ToString(arr3[1]) == Convert.ToString(arr2[1]))
                        {
                            hf_ID.Value = Convert.ToString(arr3[1]);
                            hf_PID.Value = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["SOLD_FROM"].ToString()));
                            hfPrincipalName.Value= Convert.ToString(_dc.chkNull_0(dt.Rows[0]["PRINCIPAL_NAME"].ToString()));
                            txtDispatchDate.Text = Convert.ToString(dt.Rows[0]["DESPATCH_DATE"].ToString());
                            txtDispatchNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["DESPATCH_NO"].ToString()));
                            txtSealNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["SEAL_NO"].ToString()));
                            txtGatePassNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["GATEPASS_NO"].ToString()));
                            txtDriverName.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["DRIVER_NAME"].ToString()));
                            txtVehicleNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["VEHICLE_NO"].ToString()));
                            txtTemperature.Text= Convert.ToString(_dc.chkNull_0(dt.Rows[0]["TEMP"].ToString()));
                            _orderedSku = _purchaseCtrl.SelectPurchaseOrderDetail2(long.Parse(arr3[1]), Constants.IntNullValue, CurrentWorkDate);
                        }
                        flag = true;
                        break;
                    }
                    else
                    {
                        hf_ID.Value = Convert.ToString(Constants.LongNullValue);
                        hf_PID.Value = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["SOLD_FROM"].ToString()));
                        _orderedSku = _purchaseCtrl.SelectPurchaseOrderDetail(long.Parse(arr[0]), Constants.IntNullValue);
                        flag = false;

                    }
                }
                Session.Add("OrderedSKU", _orderedSku);
                LoadGird();
            }
            }
            else
            {
                _orderedSku = new DataTable();
                Session.Add("OrderedSKU", _orderedSku);
                LoadGird();
            }
        }
        else
        {
            _orderedSku = new DataTable();
            Session.Add("OrderedSKU", _orderedSku);
            LoadGird();
        }
    }
        
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        int TotalValue = 0;
        _orderedSku = (DataTable)Session["OrderedSKU"];
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();
        GrdPurchase.DataSource = _orderedSku;
        GrdPurchase.DataBind();
        foreach (DataRow dr in _orderedSku.Rows)
        {
           
           TotalValue += int.Parse(dr["RcdQty"].ToString());
           TotalValue += int.Parse(dr["CurrentRcdQty"].ToString());
           
        }
        txtTotalQuantity.Text = TotalValue.ToString(); 
    }

    #endregion

    #region Click OPerations

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        DataTable PurchasedPSKU = new DataTable();
        PurchasedPSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchasedPSKU.Columns.Add("SKU_ID", typeof(int));
        PurchasedPSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchasedPSKU.Columns.Add("PRICE", typeof(decimal));
        PurchasedPSKU.Columns.Add("Order_Quantity", typeof(int));
        PurchasedPSKU.Columns.Add("Quantity", typeof(int));
        PurchasedPSKU.Columns.Add("FREE_SKU", typeof(int));
        PurchasedPSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchasedPSKU.Columns.Add("TIME_STAMP", typeof(DateTime));
        PurchasedPSKU.Columns.Add("MFG_DATE", typeof(DateTime));
        PurchasedPSKU.Columns.Add("TAX_PERCENT", typeof(decimal));

        decimal OdereQty = 0;
        decimal reciveiveQTY = 0;
        int i = 0;
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            if (txtCurrentRecQty.Text.Length > 0)
            {
                _orderedSku = (DataTable)Session["OrderedSKU"];
                OdereQty = decimal.Parse(_orderedSku.Rows[i]["Quantity"].ToString());
                reciveiveQTY = Convert.ToDecimal(txtRecQty.Text) + Convert.ToDecimal(txtCurrentRecQty.Text);

                if (reciveiveQTY > OdereQty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Received quantity should not be greater then Order quantity.');", true);
                    return;
                }
                i += 1;
            }
        }

        DataTable priceDt = new DataTable();
        if (GrdPurchase.Rows.Count > 0)
        {
            var priceController = new SKUPriceDetailController();
            priceDt = priceController.SelectSKuCurrentPrice(Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue,
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
        }

        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            TextBox txtFreeRecQty = gvr.FindControl("txtFreeRecQty") as TextBox;
            TextBox txtCurrentFreeRecQty = gvr.FindControl("txtCurrentFreeRecQty") as TextBox;
            TextBox txtBatchNo = gvr.FindControl("txtBatchNo") as TextBox;
            TextBox txtExpDate = gvr.FindControl("txtExpDate") as TextBox;
            TextBox txtMfgDate = gvr.FindControl("txtMfgDate") as TextBox;

            DateTime dtExpDate = Constants.DateNullValue;
            DateTime dtMfgDate = Constants.DateNullValue;

            decimal taxPercentage = 0;
            DataRow[] foundPrice = priceDt.Select("SKU_ID  = '" + gvr.Cells[0].Text + "'");
            if (foundPrice != null && foundPrice.Length > 0)
            {
                taxPercentage = Convert.ToDecimal(_dc.chkNull_0(foundPrice[0]["TAX_PRICE_PURCHASE"].ToString()));
            }

            if (txtExpDate.Text.Length > 0)
            {
                try
                {
                    dtExpDate = Convert.ToDateTime(txtExpDate.Text);
                }
                catch (Exception ex)
                {
                }
            }
            if (txtMfgDate.Text.Length > 0)
            {
                try
                {
                    dtMfgDate = Convert.ToDateTime(txtMfgDate.Text);
                }
                catch (Exception ex)
                {
                }
            }

            DataRow dr = PurchasedPSKU.NewRow();

            dr["SKU_ID"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[0].Text));
            dr["BATCH_NO"] = "N/A"; //txtBatchNo.Text;
            dr["FREE_SKU"] = Convert.ToInt32(_dc.chkNull_0(txtCurrentFreeRecQty.Text));
            dr["Quantity"] = Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text));
            dr["PRICE"] = Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[10].Text));
            dr["Order_Quantity"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[3].Text));
            dr["AMOUNT"] = Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[10].Text)) * Convert.ToDecimal(_dc.chkNull_0(txtCurrentRecQty.Text));
            dr["TIME_STAMP"] = dtExpDate;
            dr["MFG_DATE"] = dtMfgDate;
            dr["TAX_PERCENT"] = taxPercentage;
            PurchasedPSKU.Rows.Add(dr);
        }


        if (PurchasedPSKU.Rows.Count > 0)
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
            if (CurrentWorkDate != Constants.DateNullValue)
            {
                long voucherNo = CalculatePurchase(CurrentWorkDate, PurchasedPSKU);
                if (voucherNo != -2)
                {
                    LoadDistributor();
                    GetDocumentNo();
                    GrdPurchase.DataSource = null;
                    GrdPurchase.DataBind();
                    ClearAll();
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('GRN saved successfully.');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location');", true);
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
        LoadGird();
    }

    #endregion

    private long CalculatePurchase(DateTime mWorkDate, DataTable purchasedPsku)
    {
        decimal mTotalAmount = purchasedPsku.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["AMOUNT"].ToString()));
        var arr = drpDocumentNo.SelectedItem.Text.Split('-');
        string voucherNo;
        if (Convert.ToInt64(hf_ID.Value) > 0)
        {
            long mResult = _purchaseCtrl.UpdatePurchaseDocument2(long.Parse(hf_ID.Value), int.Parse(drpDistributor.SelectedValue), arr[1], 2
                , mWorkDate, int.Parse(drpDistributor.SelectedValue), 0
                , mTotalAmount, false, purchasedPsku, 0, "", int.Parse(Session["UserId"].ToString()),int.Parse(hf_PID.Value), "",
                 DateTime.Parse(txtDispatchDate.Text), txtDispatchNo.Text, txtSealNo.Text, txtGatePassNo.Text, txtDriverName.Text, txtVehicleNo.Text, txtTemperature.Text, out voucherNo);
            if (mResult == -2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again !');", true);
            }
            else
            {

                    PrintReport(voucherNo);
                    Print(mResult);
                
            }
            return mResult;
           
        }
        else
        {
            long mResult = _purchaseCtrl.InsertPurchaseDocument2(int.Parse(drpDistributor.SelectedValue), arr[1], 2
            , mWorkDate, int.Parse(drpDistributor.SelectedValue), 0
            , mTotalAmount, false, purchasedPsku, 0, "", int.Parse(Session["UserId"].ToString()), int.Parse(hf_PID.Value), Convert.ToInt64(arr[0]), "",
            DateTime.Parse(txtDispatchDate.Text), txtDispatchNo.Text, txtSealNo.Text, txtGatePassNo.Text, txtDriverName.Text, txtVehicleNo.Text, txtTemperature.Text, out voucherNo);
            if (mResult == -2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Try Again !');", true);
            }
            else
            {

                PrintReport(voucherNo);
                Print(mResult);

            }
            return mResult;
           
        }
        
    }

    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
        {
            _orderedSku = new DataTable();
            Session.Add("OrderedSKU", _orderedSku);
            LoadGird();
            
        }
        else
        {
            LoadDocumentDetail();
          
        }
    }
    private void ClearAll()
    {
        txtDispatchDate.Text = "";
        txtDispatchNo.Text = "";
        txtSealNo.Text = "";
        txtGatePassNo.Text = "";
        txtDriverName.Text = "";
        txtVehicleNo.Text = "";
        txtTemperature.Text = "";
        txtTotalQuantity.Text = "";

    }
    private void PrintReport(string pVoucherNo)
    {

        var crpReport = new crpVoucherView();

        DataSet ds = null;
        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        ds = _rptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue), pVoucherNo, Constants.Journal_Voucher);
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        crpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        //const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        //   Type cstype = GetType();
        // ClientScriptManager cs = Page.ClientScript;
        // cs.RegisterStartupScript(cstype, "OpenWindow", script);
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
        var crpReport = new CrpPurchaseDocument();
        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = _rptInventoryCtl.SelectPurchaseDocument(mResult, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, CurrentWorkDate, CurrentWorkDate, 2);
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("DocumentType", "Purchase Invoice");
        crpReport.SetParameterValue("Principal", hfPrincipalName.Value);
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        Session.Add("CrpReport2", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default2.aspx'";
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
        LoadDocumentDetail();
    }
}