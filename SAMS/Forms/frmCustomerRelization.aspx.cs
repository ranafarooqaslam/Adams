using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSBusinessLayer.Reports;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

/// <summary>
/// Form For Bank Transaction
/// </summary>
public partial class Forms_frmCustomerRelization : System.Web.UI.Page
{
    readonly LedgerController _ledgerCtrl= new LedgerController();
    readonly SaleForceController _saleForceCtrl = new SaleForceController();
    readonly DocumentPrintController DPrint = new DocumentPrintController();
    readonly RptAccountController RptAccountCtl = new RptAccountController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SAMSCommon.Classes.Configuration.GetAccountHead();
            LoadDistributor();
            LoadPrincipal();
            LoadDeliveryman();
            LoadArea();
            LoadData();
            LoadAccountHead();
            this.LoadAccountHeadHO();
            LoadGrid();
            SelectCreditInvoice();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            SetTableSorter();
        }
    }
  
    #region Load

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }
    
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {
        DistributorAreaController mController = new DistributorAreaController();
        DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadData()
    {
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            if (DrpAccountType.SelectedIndex == 1 || DrpAccountType.SelectedIndex == 5)
            {
                DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpPrincipal.SelectedValue.ToString()));
                clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 4, true);
                DrpRoute.Enabled = true;
            }
            else
            {
                LedgerController LedgerCtl = new LedgerController();
                DataTable dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), null, Constants.IntNullValue);
                clsWebFormUtil.FillDropDownList(DrpCustomer, dtCredit, 0, 1, true);
                Session.Add("dtCustomer", dtCredit);
                DrpRoute.Enabled = false;
            }
        }
    }

    /// <summary>
    /// Loads Bank Transactions To Grid
    /// </summary>
    private void LoadGrid()
    {
        if (DrpAccountType.SelectedIndex != 7)
        {
            string DrpAccountTypeSelectedValue = "";
            if (DrpAccountType.SelectedValue == "222")
            {
                DrpAccountTypeSelectedValue = "22";
            }
            else
            {
                DrpAccountTypeSelectedValue = DrpAccountType.SelectedValue;
            }
            if (drpDistributor.Items.Count > 0)
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
                LedgerController LController = new LedgerController();
                DataTable dt = LController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Convert.ToInt32(DrpAccountTypeSelectedValue),
                    CurrentWorkDate, CurrentWorkDate);
                GrdOrder.DataSource = dt;
                GrdOrder.DataBind();
            }
        }
    }
    
    /// <summary>
    /// Loads Account Heads To Account Combo
    /// </summary>
    private void LoadAccountHead()
    {
        if (DrpAccountType.SelectedIndex == 0 || DrpAccountType.SelectedIndex == 1 || DrpAccountType.SelectedIndex == 5)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.CashDefaultType));
            clsWebFormUtil.FillDropDownList(DrpAccountDetail, dt, 0, 4, true);
        }
        else if (DrpAccountType.SelectedIndex == 3)
        {
            DrpAccountDetail.Items.Clear();
            DrpAccountDetail.Items.Add(new ListItem("Tax Deducted By Parties", "127"));
        }
        else if (DrpAccountType.SelectedIndex == 4)
        {
            DrpAccountDetail.Items.Clear();
            DrpAccountDetail.Items.Add(new ListItem("Credit Transfer Out", "361"));
        }
        else if (DrpAccountType.SelectedIndex == 2 || DrpAccountType.SelectedIndex == 6)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
            clsWebFormUtil.FillDropDownList(DrpAccountDetail, dt, 0, 4, true);
        }
        else
        {
            DrpAccountDetail.Items.Clear();
        }

    }

    private void LoadAccountHeadHO()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHeadAssignedLocation(Constants.IntNullValue, Constants.LongNullValue,int.Parse(drpDistributor.SelectedValue),Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddlHeadOffice, dt, 0, 4, true);
    }

    /// <summary>
    /// Loads Principals To Principal Combo
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
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Sale Force Cash To Grid
    /// </summary>
    private void LoadSaleFoceCash()
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
        gvSaleForceCash.DataSource = null;
        gvSaleForceCash.DataBind();
        DataTable dt = _saleForceCtrl.GetSaleForceCash(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpPrincipal.SelectedValue), Convert.ToInt32(DrpDeliveryMan.SelectedValue), CurrentWorkDate, CurrentWorkDate);
        gvSaleForceCash.DataSource = dt;
        gvSaleForceCash.DataBind();
        gvSaleForceCash.Visible = true;
        GrdOrder.Visible = false;
    }

    #endregion

    /// <summary>
    /// Saves Cash Realization
    /// </summary>
    private bool CashRealization()
    {
        IDbConnection mConnection = null;
        IDbTransaction mTransaction = null;
        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        mConnection.Open();
        mTransaction = ProviderFactory.GetTransaction(mConnection);
        try
        {
        string voucherNo = null;
        var lController = new LedgerController();
    
        decimal offerAmount = decimal.Parse(txtAmount.Text);
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
            foreach (GridViewRow dr in GrdCredit.Rows)
        {
            string maxDocumentId = lController.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue),mTransaction ,mConnection);
           
            var chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                DataTable dtLedger = CreateTableHO();

                //Credit from Account Receivable (Party Wise)
                DataRow drLedger = dtLedger.NewRow();
                drLedger["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
                drLedger["VOUCHER_NO"] = maxDocumentId;
                drLedger["ACCOUNT_HEAD_ID"] = SAMSCommon.Classes.Configuration.AccountReceivable;
                drLedger["ACCOUNT_HEAD_ID_HO"] = ddlHeadOffice.SelectedValue;
                drLedger["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= offerAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = offerAmount;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = dr.Cells[3].Text;
                }
                drLedger["Ledger_Date"] = CurrentWorkDate;
                drLedger["Remarks"] = txtRemarks.Text;
                drLedger["TimeStamp"] = DateTime.Now;
                drLedger["Customer_ID"] = DrpCustomer.SelectedValue;
                drLedger["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger["Cheque_NO"] = txtChequeNo.Text;
                drLedger["UserID"] = Session["UserId"].ToString();
                drLedger["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger["SlipNo"] = txtSlipNo.Text;
                drLedger["ChequeDate"] = Constants.DateNullValue;
                drLedger["PaymentMode"] = 19;
                drLedger["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger);

                //Debit to selected Account Head
                DataRow drLedger2 = dtLedger.NewRow();
                drLedger2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
                drLedger2["VOUCHER_NO"] = maxDocumentId;
                drLedger2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
                drLedger2["ACCOUNT_HEAD_ID_HO"] = ddlHeadOffice.SelectedValue;
                drLedger2["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= offerAmount)
                {
                    drLedger2["DEBIT"] = offerAmount;
                    drLedger2["CREDIT"] = 0;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    drLedger2["DEBIT"] = dr.Cells[3].Text;

                    drLedger2["CREDIT"] = 0;
                }

                drLedger2["Ledger_Date"] = CurrentWorkDate;
                drLedger2["Remarks"] = txtRemarks.Text;
                drLedger2["TimeStamp"] = DateTime.Now;
                drLedger2["Customer_ID"] = DrpCustomer.SelectedValue;
                drLedger2["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger2["Cheque_NO"] = txtChequeNo.Text;
                drLedger2["UserID"] = Session["UserId"].ToString();
                drLedger2["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger2["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger2["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger2["SlipNo"] = txtSlipNo.Text;
                drLedger2["ChequeDate"] = Constants.DateNullValue;
                drLedger2["PaymentMode"] = 19;
                drLedger2["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger2);

                if (decimal.Parse(dr.Cells[3].Text) >= offerAmount)
                {

                    string IsInsert = InsertGl(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), offerAmount, mTransaction, mConnection);
                    if (IsInsert == null)
                    {
                        throw new ArgumentNullException();
                    }


                    offerAmount = decimal.Parse(dr.Cells[3].Text) - offerAmount;
                     
                     bool Isvalid = lController.PostingCash_Bank_AccountHO(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), offerAmount, mTransaction, mConnection);
                   
                    if (!Isvalid)
                    {
                        throw new ArgumentNullException();
                    }

                    voucherNo = voucherNo + "," + IsInsert;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    string IsInsert = InsertGl(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), Convert.ToDecimal(dr.Cells[3].Text), mTransaction, mConnection);
                    if (IsInsert == null)
                    {
                        throw new ArgumentNullException();
                    }
                    offerAmount = offerAmount - decimal.Parse(dr.Cells[3].Text);
                    
                     bool Isvalid = lController.PostingCash_Bank_AccountHO(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), 0, mTransaction, mConnection);

                    
                    if (!Isvalid)
                    {
                        throw new ArgumentNullException();
                    }
                    voucherNo = voucherNo + "," + IsInsert;
                }
            }           
        }
            mTransaction.Commit();
        PrintReport(voucherNo);
            return true;
        }
        catch (Exception exp)
        {
            mTransaction .Rollback();
            
            ExceptionPublisher.PublishException(exp);
            return false;


        }
    }
    
    /// <summary>
    /// Saves Cash Advance
    /// </summary>
    private void CashAdvance()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow drLoc in dtLocationInfo.Rows)
        {
            if (drLoc["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                if (drLoc["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(drLoc["MaxDayClose"]);
                    break;
                }
            }
        }
        LedgerController LController = new LedgerController();
        string MaxDocumentID = LController.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));

        DataTable dtLedger = CreateTable();

        //Credit from Account Receivable (Party Wise)
        DataRow dr = dtLedger.NewRow();
        
        dr["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
        dr["VOUCHER_NO"] = MaxDocumentID;
        dr["ACCOUNT_HEAD_ID"] = SAMSCommon.Classes.Configuration.AccountReceivable;
        dr["Distributor_ID"] = drpDistributor.SelectedValue;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = txtAmount.Text;
        dr["Ledger_Date"] = CurrentWorkDate;
        dr["Remarks"] = txtRemarks.Text;
        dr["TimeStamp"] = DateTime.Now;
        dr["Customer_ID"] = DrpCustomer.SelectedValue;
        dr["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr["Cheque_NO"] = txtChequeNo.Text;
        dr["UserID"] = Session["UserId"].ToString();
        dr["Document_ID"] = Constants.LongNullValue;
        dr["Manual_Document_ID"] = null;
        dr["DocumentTypeID"] = Constants.IntNullValue;
        dr["SlipNo"] = txtSlipNo.Text;
        dr["ChequeDate"] = Constants.DateNullValue;
        dr["PaymentMode"] = 21;
        dr["PayeesName"] = "";
        dtLedger.Rows.Add(dr);

        //Debit to selected Account Head
        DataRow dr2 = dtLedger.NewRow();
        
        dr2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
        dr2["VOUCHER_NO"] = MaxDocumentID;
        dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        dr2["Distributor_ID"] = drpDistributor.SelectedValue;
        dr2["DEBIT"] = txtAmount.Text;
        dr2["CREDIT"] = 0;
        dr2["Ledger_Date"] = CurrentWorkDate;
        dr2["Remarks"] = txtRemarks.Text;
        dr2["TimeStamp"] = DateTime.Now;
        dr2["Customer_ID"] = DrpCustomer.SelectedValue;
        dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr2["Cheque_NO"] = txtChequeNo.Text;
        dr2["UserID"] = Session["UserId"].ToString();
        dr2["Document_ID"] = Constants.LongNullValue;
        dr2["Manual_Document_ID"] = null;
        dr2["DocumentTypeID"] = Constants.IntNullValue;
        dr2["SlipNo"] = txtSlipNo.Text;
        dr2["ChequeDate"] = Constants.DateNullValue;
        dr2["PaymentMode"] = 21;
        dr2["PayeesName"] = "";
        dtLedger.Rows.Add(dr2);

        LController.PostingCash_Bank_Account(dtLedger);
    }
    
    /// <summary>
    /// Saves Bank Deposits For Branch And Deliveryman
    /// </summary>
    /// <param name="pSaleForceId"></param>
    private void BankDeposit(string pSaleForceId)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow drLoc in dtLocationInfo.Rows)
        {
            if (drLoc["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                if (drLoc["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(drLoc["MaxDayClose"]);
                    break;
                }
            }
        }
        string maxDocumentId = _ledgerCtrl.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(drpDistributor.SelectedValue));
        DataTable dtLedger = CreateTable();
        
        //Credit from NCS
        DataRow dr = dtLedger.NewRow();
        dr["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
        dr["VOUCHER_NO"] = maxDocumentId;
        dr["ACCOUNT_HEAD_ID"] = Configuration.CashDefault;
        dr["Distributor_ID"] = drpDistributor.SelectedValue;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = txtAmount.Text;
        dr["Ledger_Date"] = CurrentWorkDate;
        dr["Remarks"] = txtRemarks.Text;
        dr["TimeStamp"] = DateTime.Now;
        dr["Customer_ID"] = Constants.IntNullValue;
        dr["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr["Cheque_NO"] = txtChequeNo.Text;
        dr["UserID"] = Session["UserId"].ToString();
        dr["Document_ID"] = Constants.LongNullValue;
        dr["Manual_Document_ID"] = null;
        dr["DocumentTypeID"] = Constants.IntNullValue;
        dr["SlipNo"] = txtSlipNo.Text;
        dr["ChequeDate"] = Constants.DateNullValue;
        dr["PaymentMode"] = 22;
        dr["PayeesName"] = pSaleForceId;
        dtLedger.Rows.Add(dr);

        //Debit to selected Account Head
        DataRow dr2 = dtLedger.NewRow();       
        dr2["VOUCHER_TYPE_ID"] = Constants.Bank_Voucher;
        dr2["VOUCHER_NO"] = maxDocumentId;
        dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        dr2["Distributor_ID"] = drpDistributor.SelectedValue;
        dr2["DEBIT"] = txtAmount.Text;
        dr2["CREDIT"] = 0;
        dr2["Ledger_Date"] = CurrentWorkDate;
        dr2["Remarks"] = txtRemarks.Text;
        dr2["TimeStamp"] = DateTime.Now;
        dr2["Customer_ID"] = Constants.IntNullValue;
        dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr2["Cheque_NO"] = txtChequeNo.Text;
        dr2["UserID"] = Session["UserId"].ToString();
        dr2["Document_ID"] = Constants.LongNullValue;
        dr2["Manual_Document_ID"] = null;
        dr2["DocumentTypeID"] = Constants.IntNullValue;
        dr2["SlipNo"] = txtSlipNo.Text;
        dr2["ChequeDate"] = Constants.DateNullValue;
        dr2["PaymentMode"] = 22;
        dr2["PayeesName"] = pSaleForceId;
        dtLedger.Rows.Add(dr2);
        
        _ledgerCtrl.PostingCash_Bank_Account(dtLedger);
    }
    
    /// <summary>
    /// Saves Petty Cash
    /// </summary>
    private void PettyCash()
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
        string maxDocumentId = _ledgerCtrl.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue));
        _ledgerCtrl.PostingCash_Bank_Account(Constants.Cash_Voucher, long.Parse(maxDocumentId), long.Parse(Configuration.CashDefault), int.Parse(drpDistributor.SelectedValue), decimal.Parse(txtAmount.Text), 0,
                 CurrentWorkDate, txtRemarks.Text, DateTime.Now, Constants.IntNullValue, int.Parse(DrpPrincipal.SelectedValue),
                 txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue,null, Constants.IntNullValue, txtSlipNo.Text, Constants.DateNullValue, int.Parse(DrpAccountType.SelectedValue), "");
    }
    
    /// <summary>
    /// Loads Deliveryment To Deliveryman Combo
    /// </summary>
    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0)
        {

            DataTable mDt = _saleForceCtrl.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(DrpDeliveryMan, mDt, 0, 3, true);
        }
    }
    
    /// <summary>
    /// Loads Credit Invoices To Invoice Grid
    /// </summary>
    private void SelectCreditInvoice()
    {
        
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (DrpCustomer.Items.Count > 0 && DrpAccountType.SelectedIndex != 1 && DrpAccountType.SelectedIndex != 5)
        {
            DataTable dtCredit = _ledgerCtrl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), DrpCustomer.SelectedValue, 2);
            GrdCredit.DataSource = dtCredit;
            GrdCredit.DataBind();
        }
    }
    
    /// <summary>
    /// Saves Income Tax
    /// </summary>
    private void IncomeTax()
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
        string maxDocumentId = _ledgerCtrl.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        decimal offerAmount = decimal.Parse(txtAmount.Text);
        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                DataTable dtLedger = CreateTable();

                //Credit from Account Receivable (Party Wise)
                DataRow drLedger = dtLedger.NewRow();
                drLedger["VOUCHER_TYPE_ID"] = Constants.Journal_Voucher;
                drLedger["VOUCHER_NO"] = maxDocumentId;
                drLedger["ACCOUNT_HEAD_ID"] = Configuration.AccountReceivable;
                drLedger["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= offerAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = offerAmount;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    drLedger["DEBIT"] = 0;
                    drLedger["CREDIT"] = dr.Cells[3].Text;
                }
                drLedger["Ledger_Date"] = CurrentWorkDate;
                drLedger["Remarks"] = txtRemarks.Text;
                drLedger["TimeStamp"] = DateTime.Now;
                drLedger["Customer_ID"] = DrpCustomer.SelectedValue;
                drLedger["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger["Cheque_NO"] = txtChequeNo.Text;
                drLedger["UserID"] = Session["UserId"].ToString();
                drLedger["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger["SlipNo"] = txtSlipNo.Text;
                drLedger["ChequeDate"] = Constants.DateNullValue;
                drLedger["PaymentMode"] = DrpAccountType.SelectedValue;
                drLedger["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger);

                //Debit to selected Account Head
                DataRow drLedger2 = dtLedger.NewRow();
                drLedger2["VOUCHER_TYPE_ID"] = Constants.Journal_Voucher;
                drLedger2["VOUCHER_NO"] = maxDocumentId;
                drLedger2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
                drLedger2["Distributor_ID"] = drpDistributor.SelectedValue;
                if (decimal.Parse(dr.Cells[3].Text) >= offerAmount)
                {
                    drLedger2["DEBIT"] = offerAmount;
                    drLedger2["CREDIT"] = 0;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    drLedger2["DEBIT"] = dr.Cells[3].Text;
                    drLedger2["CREDIT"] = 0;
                }

                drLedger2["Ledger_Date"] = CurrentWorkDate;
                drLedger2["Remarks"] = txtRemarks.Text;
                drLedger2["TimeStamp"] = DateTime.Now;
                drLedger2["Customer_ID"] = DrpCustomer.SelectedValue;
                drLedger2["Principal_ID"] = DrpPrincipal.SelectedValue;
                drLedger2["Cheque_NO"] = txtChequeNo.Text;
                drLedger2["UserID"] = Session["UserId"].ToString();
                drLedger2["Document_ID"] = GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"].ToString();
                drLedger2["Manual_Document_ID"] = dr.Cells[1].Text;
                drLedger2["DocumentTypeID"] = Constants.Document_Invoice;
                drLedger2["SlipNo"] = txtSlipNo.Text;
                drLedger2["ChequeDate"] = Constants.DateNullValue;
                drLedger2["PaymentMode"] = DrpAccountType.SelectedValue;
                drLedger2["PayeesName"] = DrpDeliveryMan.SelectedValue;
                dtLedger.Rows.Add(drLedger2);

                if (decimal.Parse(dr.Cells[3].Text) >= offerAmount)
                {
                    offerAmount = decimal.Parse(dr.Cells[3].Text) - offerAmount;
                    _ledgerCtrl.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue), offerAmount);
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= offerAmount)
                {
                    offerAmount = offerAmount - decimal.Parse(dr.Cells[3].Text);
                    _ledgerCtrl.PostingCash_Bank_Account(dtLedger, Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue), 0);
                }
                break;
            }
        }
    }
    
    /// <summary>
    /// Saves Advance Return
    /// </summary>
    private void AdvanceReturn()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow drLoc in dtLocationInfo.Rows)
        {
            if (drLoc["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                if (drLoc["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(drLoc["MaxDayClose"]);
                    break;
                }
            }
        }
        string maxDocumentId = _ledgerCtrl.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue));
        DataTable dtLedger = CreateTable();

        //Debit to Account Receivable (Party Wise)
        DataRow dr = dtLedger.NewRow();
        dr["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
        dr["VOUCHER_NO"] = maxDocumentId;
        dr["ACCOUNT_HEAD_ID"] = Configuration.AccountReceivable;
        dr["Distributor_ID"] = drpDistributor.SelectedValue;
        dr["DEBIT"] = txtAmount.Text;
        dr["CREDIT"] = 0;
        dr["Ledger_Date"] = CurrentWorkDate;
        dr["Remarks"] = txtRemarks.Text;
        dr["TimeStamp"] = DateTime.Now;
        dr["Customer_ID"] = DrpCustomer.SelectedValue;
        dr["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr["Cheque_NO"] = txtChequeNo.Text;
        dr["UserID"] = Session["UserId"].ToString();
        dr["Document_ID"] = Constants.LongNullValue;
        dr["Manual_Document_ID"] = null;
        dr["DocumentTypeID"] = Constants.IntNullValue;
        dr["SlipNo"] = txtSlipNo.Text;
        dr["ChequeDate"] = Constants.DateNullValue;
        dr["PaymentMode"] = 29;
        dr["PayeesName"] = "";
        dtLedger.Rows.Add(dr);

        //Credit from selected Account Head
        DataRow dr2 = dtLedger.NewRow();
        dr2["VOUCHER_TYPE_ID"] = Constants.Cash_Voucher;
        dr2["VOUCHER_NO"] = maxDocumentId;
        dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        dr2["Distributor_ID"] = drpDistributor.SelectedValue;
        dr2["DEBIT"] = 0;
        dr2["CREDIT"] = txtAmount.Text;
        dr2["Ledger_Date"] = CurrentWorkDate;
        dr2["Remarks"] = txtRemarks.Text;
        dr2["TimeStamp"] = DateTime.Now;
        dr2["Customer_ID"] = DrpCustomer.SelectedValue;
        dr2["Principal_ID"] = DrpPrincipal.SelectedValue;
        dr2["Cheque_NO"] = txtChequeNo.Text;
        dr2["UserID"] = Session["UserId"].ToString();
        dr2["Document_ID"] = Constants.LongNullValue;
        dr2["Manual_Document_ID"] = null;
        dr2["DocumentTypeID"] = Constants.IntNullValue;
        dr2["SlipNo"] = txtSlipNo.Text;
        dr2["ChequeDate"] = Constants.DateNullValue;
        dr2["PaymentMode"] = 29;
        dr2["PayeesName"] = "";
        dtLedger.Rows.Add(dr2);

        _ledgerCtrl.PostingCash_Bank_Account(dtLedger);
    }
    
    #region Sel/Index Change

    /// <summary>
    /// Loads Routes, Customers, Deliverymen And Bank Transactions
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex != 7)
        {
            LoadArea();
            LoadData();
            LoadGrid();
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
        }
        else if (DrpAccountType.SelectedIndex == 7)
        {
            LoadSaleFoceCash();
        }
        LoadDeliveryman();
        SetTableSorter();
        LoadAccountHeadHO();
    }

    /// <summary>
    /// Loads Customers And Credit Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        SelectCreditInvoice();
        SetTableSorter();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpAccountDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
        SetTableSorter();
    }

    /// <summary>
    /// Loads Account Head, Customers, Deliverymen And Bank Transactions
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlHeadOffice.Enabled = false;
        if (DrpAccountType.SelectedValue == "19")
        {
            ddlHeadOffice.Enabled = true;
        }
        if (DrpAccountType.SelectedIndex == 2 || DrpAccountType.SelectedIndex == 7)
        {
            HideShowControls(false);
        }
        else
        {
            HideShowControls(true);
        }

        if (DrpAccountType.SelectedIndex != 7)
        {
            LoadData();
            LoadAccountHead();
            LoadGrid();
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
            
        }
        else
        {
            LoadSaleFoceCash();
        }
        SetTableSorter();
        
    }

    /// <summary>
    /// Loads Credit Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectCreditInvoice();
        SetTableSorter();
    }

    /// <summary>
    /// Loads Credit Invoices And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex != 7)
        {
            LoadData();
            SelectCreditInvoice();
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
        }
        else
        {
            LoadSaleFoceCash();
        }
        SetTableSorter();
    }

    /// <summary>
    /// Loads Bank Transactions
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDeliveryMan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedIndex == 7)
        {
            LoadSaleFoceCash();
        }
        else
        {
            gvSaleForceCash.Visible = false;
            GrdOrder.Visible = true;
        }
    }

    #endregion

    /// <summary>
    /// Creates Datatable For Bank Transaction
    /// </summary>
    /// <returns></returns>
    private DataTable CreateTable()
    {
        DataTable dtLedger = new DataTable();
        dtLedger.Columns.Add("VOUCHER_TYPE_ID", typeof(int));
        dtLedger.Columns.Add("VOUCHER_NO", typeof(long));
        dtLedger.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtLedger.Columns.Add("Distributor_ID", typeof(int));
        dtLedger.Columns.Add("Debit", typeof(decimal));
        dtLedger.Columns.Add("Credit", typeof(decimal));
        dtLedger.Columns.Add("Ledger_Date", typeof(DateTime));
        dtLedger.Columns.Add("Remarks", typeof(string));
        dtLedger.Columns.Add("TimeStamp", typeof(DateTime));
        dtLedger.Columns.Add("Customer_ID", typeof(int));
        dtLedger.Columns.Add("Principal_ID", typeof(int));
        dtLedger.Columns.Add("Cheque_NO", typeof(string));
        dtLedger.Columns.Add("UserId", typeof(int));
        dtLedger.Columns.Add("Document_ID", typeof(long));
        dtLedger.Columns.Add("Manual_Document_ID", typeof(string));
        dtLedger.Columns.Add("DocumentTypeID", typeof(int));
        dtLedger.Columns.Add("SlipNo", typeof(string));
        dtLedger.Columns.Add("ChequeDate", typeof(DateTime));
        dtLedger.Columns.Add("PaymentMode", typeof(int));
        dtLedger.Columns.Add("PayeesName", typeof(string));
        Session.Add("dtLedger", dtLedger);
        return dtLedger;
    }

    private DataTable CreateTableHO()
    {
        DataTable dtLedger = new DataTable();
        dtLedger.Columns.Add("VOUCHER_TYPE_ID", typeof(int));
        dtLedger.Columns.Add("VOUCHER_NO", typeof(long));
        dtLedger.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtLedger.Columns.Add("ACCOUNT_HEAD_ID_HO", typeof(long));
        dtLedger.Columns.Add("Distributor_ID", typeof(int));
        dtLedger.Columns.Add("Debit", typeof(decimal));
        dtLedger.Columns.Add("Credit", typeof(decimal));
        dtLedger.Columns.Add("Ledger_Date", typeof(DateTime));
        dtLedger.Columns.Add("Remarks", typeof(string));
        dtLedger.Columns.Add("TimeStamp", typeof(DateTime));
        dtLedger.Columns.Add("Customer_ID", typeof(int));
        dtLedger.Columns.Add("Principal_ID", typeof(int));
        dtLedger.Columns.Add("Cheque_NO", typeof(string));
        dtLedger.Columns.Add("UserId", typeof(int));
        dtLedger.Columns.Add("Document_ID", typeof(long));
        dtLedger.Columns.Add("Manual_Document_ID", typeof(string));
        dtLedger.Columns.Add("DocumentTypeID", typeof(int));
        dtLedger.Columns.Add("SlipNo", typeof(string));
        dtLedger.Columns.Add("ChequeDate", typeof(DateTime));
        dtLedger.Columns.Add("PaymentMode", typeof(int));
        dtLedger.Columns.Add("PayeesName", typeof(string));
        Session.Add("dtLedger", dtLedger);
        return dtLedger;
    }

    /// <summary>
    /// Saves/Uupdates Bank Transaction
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Update")
        {

            _saleForceCtrl.DeleteSaleForceCash(Convert.ToInt32(hfSALE_FORCE_CASH_ID.Value), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfPRINCIPAL_ID.Value), Convert.ToInt32(hfDELIVERYMAN_ID.Value));
        }

        if (DrpAccountType.SelectedIndex == 0)
        {
            int invoiceCount = Constants.IntNullValue;

            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {
                    invoiceCount++;
                    break;
                }
            }
            if (invoiceCount == Constants.IntNullValue)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Must Select Invoice');", true);
                return;
            }
            bool realize = CashRealization();
            if (!realize)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some Error Occured.Please Try again!');", true);
                return;
            }
            SelectCreditInvoice();
        }
        else if (DrpAccountType.SelectedIndex == 1)
        {
            CashAdvance();
        }
        else if (DrpAccountType.SelectedIndex == 2)
        {
            BankDeposit("");
        }
        else if (DrpAccountType.SelectedIndex == 6)
        {
            BankDeposit(DrpDeliveryMan.SelectedValue);
        }
        else if (DrpAccountType.SelectedIndex == 5)
        {
            AdvanceReturn();
        }
        else if (DrpAccountType.SelectedIndex == 3 || DrpAccountType.SelectedIndex == 4)
        {
            IncomeTax();
            SelectCreditInvoice();

        }
        else if (DrpAccountType.SelectedIndex == 7)
        {
            SaleForceCashReceived();
            LoadSaleFoceCash();

        }
        else
        {
            PettyCash();
        }
        ClearAll();
        LoadGrid();
        SetTableSorter();
    }

    /// <summary>
    /// Saves Sale Force Cash
    /// </summary>
    private void SaleForceCashReceived()
    {
        try
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
            Convert.ToDecimal(txtAmount.Text);
            _saleForceCtrl.InsertSaleForceCash(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpPrincipal.SelectedValue), Convert.ToInt32(DrpDeliveryMan.SelectedValue), CurrentWorkDate, Convert.ToDecimal(txtAmount.Text), Convert.ToInt32(Session["UserId"]));
            
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Amount must be decimal')", true); 
        }
    }
  
    #region Grid Operations
    /// <summary>
    /// Deletes Bank Transaction
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LController = new LedgerController();
        DataControl dc = new DataControl();

        LController.DeleteCashBankTransction(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[7].Text)), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[10].Text.Replace("&nbsp;", ""))), decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[8].Text)));
        LoadGrid();
        SelectCreditInvoice();
        SetTableSorter();
    }
    
    /// <summary>
    /// Deletes Sale Force Cash
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvSaleForceCash_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        if (_saleForceCtrl.DeleteSaleForceCash(Convert.ToInt32(gvSaleForceCash.Rows[e.RowIndex].Cells[0].Text), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(gvSaleForceCash.Rows[e.RowIndex].Cells[1].Text), Convert.ToInt32(gvSaleForceCash.Rows[e.RowIndex].Cells[3].Text)))
        {
            LoadSaleFoceCash();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured. Cash received not deleted.');", true);
        }
        SetTableSorter();
    }
    
    protected void gvSaleForceCash_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        DrpPrincipal.SelectedValue = gvSaleForceCash.Rows[NewEditIndex].Cells[1].Text;
        DrpDeliveryMan.SelectedValue = gvSaleForceCash.Rows[NewEditIndex].Cells[3].Text;
        txtAmount.Text = gvSaleForceCash.Rows[NewEditIndex].Cells[6].Text;
        hfSALE_FORCE_CASH_ID.Value = gvSaleForceCash.Rows[NewEditIndex].Cells[0].Text;
        hfPRINCIPAL_ID.Value = gvSaleForceCash.Rows[NewEditIndex].Cells[1].Text;
        hfDELIVERYMAN_ID.Value = gvSaleForceCash.Rows[NewEditIndex].Cells[3].Text;
        btnSave.Text = "Update";
        SetTableSorter();
    }

    #endregion

    /// <summary>
    /// Hides/Shows Controls
    /// </summary>
    /// <param name="Visible"></param>
    private void HideShowControls(bool Visible)
    {
        if (DrpAccountType.SelectedIndex == 2)
        {
            Label2.Enabled = Visible;
            DrpRoute.Enabled = Visible;
            Label4.Enabled = Visible;
            DrpCustomer.Enabled = Visible;
            Panel1.Enabled = Visible;
            Label7.Enabled = Visible;
            DrpAccountDetail.Enabled = true;
            Label10.Enabled = Visible;
            DrpDeliveryMan.Enabled = Visible;
        }
        else
        {
            Label2.Enabled = Visible;
            DrpRoute.Enabled = Visible;
            Label4.Enabled = Visible;
            DrpCustomer.Enabled = Visible;
            Panel1.Enabled = Visible;
            Label7.Enabled = Visible;
            DrpAccountDetail.Enabled = Visible;
            Label5.Enabled = Visible;
            Label3.Enabled = Visible;
            txtChequeNo.Enabled = Visible;
            txtSlipNo.Enabled = Visible;
            Label9.Enabled = Visible;
            txtRemarks.Enabled = Visible;
            Label10.Enabled = true;
            DrpDeliveryMan.Enabled = true;
        }
    }

    /// <summary>
    /// Sets Grids Columns For JQury Sorting
    /// </summary>
    private void SetTableSorter()
    {
        if (GrdOrder.Rows.Count > 1)
        {
            GrdOrder.UseAccessibleHeader = true;
            GrdOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvSaleForceCash.Rows.Count > 1)
        {
            gvSaleForceCash.UseAccessibleHeader = true;
            gvSaleForceCash.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtRemarks.Text = "";
        txtSlipNo.Text = "";
        btnSave.Text = "Save";
    }

    private string InsertGl(long invoiceId, decimal amount,IDbTransaction mTransaction ,IDbConnection mConnection )
    {
        string maxDocumentId = null;
        DataTable dtCustomer = (DataTable)Session["dtCustomer"];
        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_ID  = '" + DrpCustomer.SelectedValue + "'");

        var custCtl = new CustomerDataController();
        DataTable dtChannel = new DataTable();
        if (foundRows.Length > 0)
        {
            dtChannel = custCtl.GetChannelAccountDetail(Constants.IntNullValue, Convert.ToInt32(foundRows[0]["CHANNEL_TYPE_ID"]), mTransaction, mConnection);
        }

        if (dtChannel.Rows.Count > 0)
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

            DataTable dtVoucher = new DataTable();
            dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
            dtVoucher.Columns.Add("Debit", typeof(decimal));
            dtVoucher.Columns.Add("Credit", typeof(decimal));
            dtVoucher.Columns.Add("Remarks", typeof(string));
            dtVoucher.Columns.Add("Principal_Id", typeof(string));

            DataRow drChannel = dtVoucher.NewRow();
            drChannel["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
            drChannel["REMARKS"] = "Cash Realized(Channel)";
            drChannel["DEBIT"] = 0;
            drChannel["CREDIT"] = amount;
            drChannel["Principal_Id"] = Convert.ToInt32(DrpPrincipal.SelectedValue);
            dtVoucher.Rows.Add(drChannel);

            DataRow drBank = dtVoucher.NewRow();
            drBank["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
            drBank["REMARKS"] = "Cash Realized(" + DrpAccountDetail.SelectedItem.Text + ")";
            drBank["DEBIT"] = amount;
            drBank["CREDIT"] = 0;
            drBank["Principal_Id"] = Convert.ToInt32(DrpPrincipal.SelectedValue);
            dtVoucher.Rows.Add(drBank);

            if (dtVoucher.Rows.Count > 0)
            {
                
                 maxDocumentId = _ledgerCtrl.SelectMaxVoucherId(Constants.Cash_Voucher , Convert.ToInt32(drpDistributor.SelectedValue), CurrentWorkDate,mTransaction ,mConnection);

                 bool isInsert = _ledgerCtrl.Add_Voucher2(Convert.ToInt32(drpDistributor.SelectedValue), 0, maxDocumentId, Constants.Cash_Voucher, CurrentWorkDate, Constants.CashPayment, "N/A", "Default Cash Realization Voucher," + DrpCustomer.SelectedItem.Text + "", Constants.DateNullValue, null, invoiceId, Constants.Cash_Relization, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text.Trim(), Constants.DateNullValue, mTransaction, mConnection);
           
                if (!isInsert)
            {
                return null;
            }
            }
        }
        return maxDocumentId;
    }

    private void PrintReport(string pVoucherNo)
    {
       
        var crpReport = new crpVoucherView();

         
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = RptAccountCtl.SelectUnpostVoucherForPrint2(int.Parse(drpDistributor.SelectedValue), pVoucherNo, Constants.Cash_Voucher);
        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        crpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        crpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}