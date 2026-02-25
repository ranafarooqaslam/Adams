using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;

public partial class Forms_frmGiftSKU2 : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable GiftSKU;
    static long GIFTSKU_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadSKUDetail();
            this.LoadAccountHead();
            CreateTable();
            this.LoadPendingOrder();
        }
    }
    private void CreateTable()
    {
        GiftSKU = new DataTable();

        GiftSKU.Columns.Add("GIFT_MASTER_ID", typeof(long));
        GiftSKU.Columns.Add("SKU_ID", typeof(int));
        GiftSKU.Columns.Add("SKU_Code", typeof(string));
        GiftSKU.Columns.Add("SKU_Name", typeof(string));
        GiftSKU.Columns.Add("GST_RATE", typeof(decimal));
        GiftSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        GiftSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        GiftSKU.Columns.Add("AMOUNT", typeof(decimal));

        this.Session.Add("GiftSKU", GiftSKU);

    }
    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];

        GiftSKU = (DataTable)this.Session["GiftSKU"];
        DataRow[] foundRows = GiftSKU.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Load

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
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate);
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        this.LoadSKUDetail();
    }
    private void LoadSKUDetail()
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
        SKUPriceDetailController PController = new SKUPriceDetailController();
        if (DrpPrincipal.Items.Count > 0)
        {
           
            DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(DrpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, CurrentWorkDate);
            clsWebFormUtil.FillDropDownList(this.ddlSKuCde, Dtsku_Price, "SKU_ID", "SkuPriceDetail2", true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
    }
    private void LoadGird()
    {
       
        GiftSKU = (DataTable)this.Session["GiftSKU"];
        GrdSKU.DataSource = GiftSKU;
        GrdSKU.DataBind();
       
    }
    private void LoadAccountHead()
    {

        SAMSCommon.Classes.Configuration.GetAccountHead();

        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);
        

    }
    private void LoadPendingOrder()
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
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        DataTable dtOrder = or.SelectPendingGiftSKU(CurrentWorkDate);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtOrder, 0, 0);

        this.Session.Add("dtOrder", dtOrder);
    }
    private void ExistenOrderDetail(long OrderId)
    {
        OrderEntryController ord = new OrderEntryController();
        GiftSKU = ord.SelectGiftSKUDetail(OrderId);

        this.Session.Add("GiftSKU", GiftSKU);
        this.LoadGird();

    }

    #endregion

    private int CheckStockStatus(int SKU_ID)
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
        PhaysicalStockController mController = new PhaysicalStockController();
            DataTable dt = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue.ToString()), SKU_ID, null,CurrentWorkDate);
            if (dt.Rows.Count > 0)
            {
                if (int.Parse(dt.Rows[0][0].ToString()) >= int.Parse(txtQuantity.Text))
                {
                    return -1;
                }
                else
                {
                    return int.Parse(dt.Rows[0][0].ToString());
                }
            }

        return 0;
    }

    #region Click Operations

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //this.LoadPendingOrder();
       // this.Clear();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        GiftSKU = (DataTable)this.Session["GiftSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
       
        if (foundRows.Length > 0)
        {
            decimal mStdDiscount = decimal.Parse(dc.chkNull_0(foundRows[0]["DISTRIBUTOR_DISCOUNT"].ToString()));
            decimal mGSTRate = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString()));
            int CurrentStock = CheckStockStatus(int.Parse(dc.chkNull_0(foundRows[0]["SKU_ID"].ToString())));

            if (btnSave.Text == "Add Sku")
            {
                if (CheckDublicateSKU())
                {
                    if (CurrentStock == -1)
                    {
                        DataRow dr = GiftSKU.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                        dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                        dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                        dr["GST_RATE"] = dc.chkNull_0(txtGstRate.Text);
                        dr["UNIT_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                        dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                        dr["AMOUNT"] = decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                        GiftSKU.Rows.Add(dr);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Current closing Stock is " + CurrentStock.ToString() + "');", true);
                        // txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Already Exists ');", true);
                    //   txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            else if (btnSave.Text == "Update Sku")
            {
                if (CurrentStock == -1)
                {
                    DataRow dr = GiftSKU.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["GST_RATE"] = dc.chkNull_0(txtGstRate.Text);
                    dr["UNIT_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                    dr["QUANTITY_UNIT"] = int.Parse(txtQuantity.Text);

                    dr["AMOUNT"] = decimal.Parse(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + "Current closing Stock is " + CurrentStock.ToString() + "');", true);
                    return;
                }
            }
            this.Session.Add("GiftSKU", GiftSKU);
            this.ClearAll();
            this.LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong SKU Select');", true);

        }
    }
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        string Voucher_No = null;
        programmaticModalPopup.Hide();
        if (drpDocumentNo.Items.Count > 0)
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
            GiftSKU = (DataTable)Session["GiftSKU"];
            var or = new OrderEntryController();

            if (GiftSKU.Rows.Count > 0)
            {
                if (btnSaveOrder.Text != "Update")
                {
                    GIFTSKU_ID = Constants.LongNullValue;
                }
                long document_No = or.InsertFreeSKU3(GIFTSKU_ID, int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), long.Parse(DrpAccountHead.SelectedValue), txtRemarks.Text, GiftSKU, int.Parse(this.Session["UserId"].ToString()), CurrentWorkDate, out Voucher_No);

                if (document_No != Constants.LongNullValue)
                {

                    ClearMasterALL();
                    ClearAll();
                    LoadPendingOrder();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record insert successfully.');", true);
                    PrintVoucher(Voucher_No);
                    PrintReport(document_No);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occurred.Please Try Again!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz enter any Product first');", true);
            }
        }
    }                 

    #endregion
  
    #region Grid Operations
    
    protected void GrdSKU_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GiftSKU = (DataTable)this.Session["GiftSKU"];
        if (GiftSKU.Rows.Count > 0)
        {
            GiftSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("GiftSKU", GiftSKU);
            this.LoadGird();
        }
    }

    #endregion

    #region Sel/Index Change

    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (long.Parse(drpDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            btnSaveOrder.Text = "Update";

            if (btnSaveOrder.Text == "Update")
            {
                if (drpDocumentNo.Items.Count > 0)
                {

                    DataTable dt = (DataTable)this.Session["dtOrder"];
                    DataRow[] foundRows = dt.Select("GIFT_MASTER_ID  = '" + drpDocumentNo.SelectedValue + "'");
                    if (foundRows.Length > 0)
                    {
                        GIFTSKU_ID =long.Parse(drpDocumentNo.SelectedValue);
                        drpDistributor.SelectedValue = foundRows[0]["DISTRIBUTOR_ID"].ToString();
                        DrpPrincipal.SelectedValue = foundRows[0]["PRINCIPAL_ID"].ToString();
                        DrpAccountHead.SelectedValue = foundRows[0]["ACCOUNT_HEAD_ID"].ToString();
                       
                        this.LoadSKUDetail();
                        this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));

                        ClearAll();
                        DisAbaleOption(true);
                    }
                }


            }
            //else
            //{

            //    DataTable dt = (DataTable)this.Session["dtOrder"];
            //    DataRow[] foundRows = dt.Select("LOAD_ID  = '" + drpDocumentNo.SelectedItem.Text + "'");
            //    if (foundRows.Length > 0)
            //    {
            //        drpDistributor.SelectedValue = foundRows[0]["DISTRIBUTOR_ID"].ToString();
                   
            //        DrpPrincipal.SelectedValue = foundRows[0]["PRINCIPAL_ID"].ToString();
            //        DrpAccountHead.SelectedValue = foundRows[0]["ACCOUNT_HEAD_ID"].ToString();

            //        this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));

            //        btnSaveOrder.Text = "Save";

            //        //this.ClearAll();
            //        ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
            //        DisAbaleOption(true);
            //    }
            //}
        }
        else
        {

            this.CreateTable();
            this.LoadGird();
            ClearMasterALL();
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadAccountHead();
            btnSaveOrder.Text = "Save";
            DisAbaleOption(false);

        }
    }
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        this.LoadSKUDetail();
    }

    #endregion

    private void ClearAll()
    {
      
        ddlSKuCde.SelectedIndex = 0;
        txtQuantity.Text = "";
        ddlSKuCde.Enabled = true;
        btnSave.Text = "Add Sku";
       
    }
    private void ClearMasterALL()
    {

        this.Session.Remove("GiftSKU");
       
        this.CreateTable();

        this.LoadGird();

        LoadDistributor();
        LoadPrincipal();
       
        LoadSKUDetail();
        this.LoadAccountHead();
        RowNo = 0;
        DisAbaleOption(false);

    }
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {

            DrpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
           
            DrpAccountHead.Enabled = false;
        }
        else
        {

            DrpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            DrpAccountHead.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }
    private void PrintReport(long p_Document_ID)
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
        var mController = new DocumentPrintController();
        var rptInventoryCtl = new RptInventoryController();
        var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument2();
        var dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        var ds = rptInventoryCtl.SelectPurchaseDocument(p_Document_ID, int.Parse(drpDistributor.SelectedValue),
                                                        Constants.IntNullValue, CurrentWorkDate,CurrentWorkDate, Constants.Document_FreeSKU);
        crpReport.SetDataSource(ds);
        crpReport.Refresh();
        crpReport.SetParameterValue("DocumentType", "Free Sku Document");
        crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", crpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        //string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
        //                ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        //Type cstype = this.GetType();
        //ClientScriptManager cs = Page.ClientScript;
        //cs.RegisterStartupScript(cstype, "OpenWindow", script);
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
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
    protected void GrdSKU_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowNo = NewEditIndex;
        ddlSKuCde.SelectedValue = GrdSKU.Rows[NewEditIndex].Cells[1].Text;
        txtQuantity.Text = GrdSKU.Rows[NewEditIndex].Cells[4].Text;
        txtUnitRate.Text = GrdSKU.Rows[NewEditIndex].Cells[5].Text;
        txtGstRate.Text = GrdSKU.Rows[NewEditIndex].Cells[6].Text;
        txtAmount.Text = GrdSKU.Rows[NewEditIndex].Cells[7].Text;
        ddlSKuCde.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update Sku";
    }
}