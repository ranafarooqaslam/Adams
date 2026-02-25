 using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

/// <summary>
/// Form To Add, Edit Cheques
/// </summary>
public partial class Forms_frmChequeEntry : System.Web.UI.Page
{
    DataControl dc = new DataControl();
    static int Status = 0;
    /// <summary>
    /// Page_Load Function Populates All Combos, ListBox And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hfAmount.Value = "0";
            Session.Remove("CHEQUE_NO");
            Status = 0;
            LoadAccountDetail();
            LoadAccountHead();
            LoadAccountHeadHO();
            LoadDeliveryman();
            LoadStatus();
            if (Session["ChequeID"] != null)
            {
                HFChqueProcessId.Value = Session["ChequeID"].ToString();
                if (Session["TypeID"].ToString() == "0")
                {
                    LoadDataEdit(Convert.ToInt64(Session["ChequeID"]));
                }

                LoadDataEdit2(Convert.ToInt64(Session["ChequeID"]));
                btnSave.Text = "Update";                
                txtTaxPer.Visible = false;
                lblTax.Visible = false;
                txtSalesTaxPer.Visible = false;
                lblSalesTaxPer.Visible = false;
                ChbAllCatagories.Visible = false;                
            }
            else
            {
                if (Session["TypeID"].ToString() == "0")
                {
                    LoadInvoiceData();
                }
                txtTaxPer.Visible = true;
                lblTax.Visible = true;
                txtSalesTaxPer.Visible = true;
                lblSalesTaxPer.Visible = true;
                ChbAllCatagories.Visible = true;
                txtChequeNo.Enabled = true;
            }
            if (Session["TypeID"].ToString() == "0")
            {
                txtAmount.Attributes.Add("Readonly", "Readonly");
            }
            try
            {
                DrpAccountHead.SelectedValue = "85";
                ddlSalesTax.SelectedValue = "85";
                DrpBankAccount.SelectedValue = "85";
            }
            catch (Exception ex)
            {
            }
        }
    }
    #region Load
    private void LoadInvoiceData()
    {

        DateTime dtFrom = Constants.DateNullValue;
        DateTime dtTo = Constants.DateNullValue;
        if (Session["DateFrom"] != null)
        {
            dtFrom = Convert.ToDateTime(Session["DateFrom"]);
        }
        if (Session["DateTo"] != null)
        {
            dtTo = Convert.ToDateTime(Session["DateTo"]);
        }
        gvInvoice.DataSource = null;
        gvInvoice.DataBind();
        DataTable dtCustomer = (DataTable)Session["dtCustomer"];
        System.Text.StringBuilder sbCustomerIDs = new System.Text.StringBuilder();
        foreach (DataRow dr in dtCustomer.Rows)
        {
            sbCustomerIDs.Append(dr["CUSTOMER_ID"].ToString() + ",");
        }

        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }

        LedgerController LedgerCtl = new LedgerController();
        DataTable dtCreditSession = LedgerCtl.SelectCreditPendingInvoice(int.Parse(Session["LocationID"].ToString()), int.Parse(Session["PrincipalID"].ToString()), sbCustomerIDs.ToString(), 0, dtFrom, dtTo, CurrentWorkDate, Constants.IntNullValue, Constants.IntNullValue);
        Session["dtCreditSession"] = dtCreditSession;
        UpdateInvoiceGridView();
        LoadInvoiceGrid();
    }
    private void LoadDataEdit(long ChequeID)
    {
        ChequeEntryController mController = new ChequeEntryController();
        DataSet dtCheque = mController.GetChequeCustomer(ChequeID);
        System.Text.StringBuilder sbCustomerIDs = new System.Text.StringBuilder();
        foreach (DataRow dr in dtCheque.Tables[1].Rows)
        {
            sbCustomerIDs.Append(dr["CUSTOMER_ID"].ToString() + ",");
        }

        LedgerController LedgerCtl = new LedgerController();
        DataTable dtCredit = new DataTable();
        if (Session["TypeID"].ToString() == "0")
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
                {
                    if (dr["MaxDayClose"].ToString().Length > 0)
                    {
                        CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        break;
                    }
                }
            }

            if (Session["StatusID"].ToString() == Constants.Cheque_Pending.ToString())
            {
                dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(Session["LocationID"].ToString()),
                                                                int.Parse(Session["PrincipalID"].ToString()),
                                                                sbCustomerIDs.ToString(), 1,
                                                                Convert.ToInt64(HFChqueProcessId.Value), CurrentWorkDate);
                Session.Add("dtCredit", dtCredit);
            }
            else
            {
                dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(Session["LocationID"].ToString()), int.Parse(Session["PrincipalID"].ToString()), sbCustomerIDs.ToString(), 3, Convert.ToInt64(HFChqueProcessId.Value), CurrentWorkDate);
                Session.Add("dtCredit", dtCredit);
            }
        }
    }
    private void LoadDataEdit2(long ChequeID)
    {
        ChequeEntryController mController = new ChequeEntryController();
        DataSet dtCheque = mController.GetChequeCustomer(ChequeID);
        System.Text.StringBuilder sbCustomerIDs = new System.Text.StringBuilder();
        foreach (DataRow dr in dtCheque.Tables[1].Rows)
        {
            sbCustomerIDs.Append(dr["CUSTOMER_ID"].ToString() + ",");
        }
        Session.Add("dtCustomer", dtCheque.Tables[1]);
        LedgerController LedgerCtl = new LedgerController();
        DataTable dtCredit2 = new DataTable();
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        if (Session["TypeID"].ToString() == "0")
        {
            if (Session["StatusID"].ToString() == Constants.Cheque_Pending.ToString())
            {
                dtCredit2 = LedgerCtl.SelectCreditPendingInvoice(int.Parse(Session["LocationID"].ToString()), int.Parse(Session["PrincipalID"].ToString()), sbCustomerIDs.ToString(), 1, Convert.ToInt64(HFChqueProcessId.Value), 50, CurrentPage + 1, CurrentWorkDate);
                Session.Add("dtCredit2", dtCredit2);
                LoadEditGrid();
            }
            else
            {
                dtCredit2 = LedgerCtl.SelectCreditPendingInvoice(int.Parse(Session["LocationID"].ToString()), int.Parse(Session["PrincipalID"].ToString()), sbCustomerIDs.ToString(), 3, Convert.ToInt64(HFChqueProcessId.Value), 50, CurrentPage + 1, CurrentWorkDate);
                Session.Add("dtCredit2", dtCredit2);
                LoadEditGrid();
            }
        }
        int i = 0;
        foreach (GridViewRow dr in gvInvoiceEdit.Rows)
        {
            if (Convert.ToInt64(gvInvoiceEdit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]) == Convert.ToInt64(dtCredit2.Rows[i]["SALE_INVOICE_ID"]) && (Convert.ToInt32(dtCredit2.Rows[i]["IS_Recived"]) != 0) && (Convert.ToDecimal(dtCredit2.Rows[i]["RCDAMOUNT"]) != 0))
            {
                CheckBox chRelized = (CheckBox)dr.FindControl("ChbIsAssigned");
                chRelized.Checked = true;
                chRelized.Enabled = false;
            }
            if (Convert.ToInt32(dtCredit2.Rows[i]["IS_CurrentRecived"]) != 0)
            {
                CheckBox chRelized = (CheckBox)dr.FindControl("ChbIsAssigned");
                chRelized.Checked = true;
                chRelized.Enabled = true;
            }
            i += 1;
            if (Convert.ToDateTime(dtCheque.Tables[0].Rows[0]["RECEIVED_DATE"]) <
                CurrentWorkDate || int.Parse(Session["StatusID"].ToString()) == Constants.Cheque_Deposit)
            {
                TextBox txtCrdAmount = (TextBox)dr.FindControl("txtCrdAmount");
                TextBox txtRecievedAmount = (TextBox)dr.FindControl("txtRecievedAmount");
                TextBox txtBalAmount = (TextBox)dr.FindControl("txtBalAmount");
                TextBox txtTaxAmount = (TextBox)dr.FindControl("txtTaxAmount");
                CheckBox chRelized = (CheckBox)dr.FindControl("ChbIsAssigned");
                TextBox txtRcdAmount = (TextBox)dr.FindControl("txtRcdAmount");
                TextBox txttax = (TextBox)dr.FindControl("txtTax");
                TextBox txttaxAmounttax = (TextBox)dr.FindControl("txtChequeAmountTax");
                chRelized.Enabled = false;
                txtRcdAmount.ReadOnly = true;
                txttax.ReadOnly = true;
                txttaxAmounttax.ReadOnly = true;

                decimal OpeningAmount = decimal.Parse(txtCrdAmount.Text) + decimal.Parse(txtRecievedAmount.Text);
                txtCrdAmount.Text = "";
                txtCrdAmount.Text = OpeningAmount.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "CalculateBalance(" + txtCrdAmount.ClientID + "," + txtRcdAmount.ClientID + "," + txtBalAmount.ClientID + "," + txtTaxAmount.ClientID + ");", true);
            }
        }
        if (dtCheque.Tables[1].Rows.Count > 0)
        {
            txtChequeNo.Text = dtCheque.Tables[0].Rows[0]["CHEQUE_NO"].ToString();
            Session.Add("CHEQUE_NO", dtCheque.Tables[0].Rows[0]["CHEQUE_NO"]);
            txtBankName.Text = dtCheque.Tables[0].Rows[0]["BANK_NAME"].ToString();
            txtStartDate.Text = Convert.ToDateTime(dtCheque.Tables[0].Rows[0]["CHEQUE_DATE"]).ToString("dd/MM/yyyy").ToString();
            txtReceivedDate.Text = dtCheque.Tables[0].Rows[0]["RECEIVED_DATE"].ToString();
            txtAmount.Text = dtCheque.Tables[0].Rows[0]["CHEQUE_AMOUNT"].ToString();
            txtSlipNo.Text = dtCheque.Tables[0].Rows[0]["SlipNo"].ToString();
            txtRemarks.Text = dtCheque.Tables[0].Rows[0]["Remarks"].ToString();
            try
            {
                if (dtCheque.Tables[0].Rows[0]["ACCONT_HEAD_ID_HO"].ToString() != "")
                {
                    ddlHeadOffice.SelectedValue = dtCheque.Tables[0].Rows[0]["ACCONT_HEAD_ID_HO"].ToString();
                }
                DrpBankAccount.SelectedValue = dtCheque.Tables[0].Rows[0]["ACCOUNT_HEAD_ID"].ToString();

            }
            catch (Exception ex)
            {

            }
            if (dtCheque.Tables[0].Rows[0]["DELIVERYMAN_ID"].ToString() != "")
            {
                DrpDeliveryMan.SelectedValue = dtCheque.Tables[0].Rows[0]["DELIVERYMAN_ID"].ToString();
            }
            try
            {
                if (dtCheque.Tables[0].Rows[0]["TAX_ACCOUNT_HEAD_ID"].ToString() != "")
                {
                    DrpAccountHead.SelectedValue = dtCheque.Tables[0].Rows[0]["TAX_ACCOUNT_HEAD_ID"].ToString();
                }
            }
            catch(Exception ex)
            {

            }

            if (Status == 0)
            {// mean on load
                DrpStatus.SelectedValue = Session["StatusID"].ToString();
            }
        }
        if (Session["TypeID"].ToString() == "0")
        {
            CalculateChequeAmount(2);
        }
    }
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtBankName.Text = "";
        txtStartDate.Text = "";
        btnSave.Text = "Save";
        txtReceivedDate.Text = "";
        txtSlipNo.Text = "";
        txtRemarks.Text = "";
        Session.Remove("dtCreditSession");
        Session.Remove("dtCreditview");
        Session.Remove("dtCredit");
        Session.Remove("dtCredit2");
    }
    private void LoadDeliveryman()
    {
        SaleForceController mDController = new SaleForceController();
        DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(Session["LocationID"].ToString()), Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDeliveryMan, m_dt, 0, 3, true);
    }
    private void LoadAccountHead()
    {
        SAMSCommon.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHeadAssignedLocation(Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["LocationID"].ToString()), Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(DrpBankAccount, dt, 0, 4, true);
    }
    private void LoadAccountHeadHO()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHeadAssignedLocation(Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["LocationID"].ToString()), Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddlHeadOffice, dt, 0, 4, true);
    }
    private void LoadAccountDetail()
    {

        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHeadAssignedLocation(Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["LocationID"].ToString()), Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dtHead, 0, 11, true);
        clsWebFormUtil.FillDropDownList(ddlSalesTax, dtHead, 0, 11, true);
        Session.Add("dtHead", dtHead);
    }
    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtRcdAmount = (TextBox)e.Row.FindControl("txtRcdAmount");
            CheckBox chRelized = (CheckBox)e.Row.FindControl("ChbIsAssigned");
            TextBox txtTaxAmount = (TextBox)e.Row.FindControl("txtTaxAmount");
            TextBox txtTax = (TextBox)e.Row.FindControl("txtTax");
            TextBox txtSalesTax = (TextBox)e.Row.FindControl("txtSalesTax");
            TextBox txtBalAmount = (TextBox)e.Row.FindControl("txtBalAmount");
            TextBox txtCrdAmount = (TextBox)e.Row.FindControl("txtCrdAmount");
            System.Text.StringBuilder atrBalance = new System.Text.StringBuilder();
            System.Text.StringBuilder atrReceivedAmount = new System.Text.StringBuilder();
            atrBalance.Append("CalculateBalance(");
            atrBalance.Append(txtCrdAmount.ClientID);
            atrBalance.Append(",");
            atrBalance.Append(txtRcdAmount.ClientID);
            atrBalance.Append(",");
            atrBalance.Append(txtBalAmount.ClientID);
            atrBalance.Append(",");
            atrBalance.Append(txtTaxAmount.ClientID);
            atrBalance.Append(");");

            txtRcdAmount.Attributes.Add("onchange", atrBalance.ToString());
            txtCrdAmount.Attributes.Add("onblur", atrBalance.ToString());
            
            txtTaxAmount.Attributes.Add("onblur", atrBalance.ToString());
            txtTaxAmount.Attributes.Add("onchange", atrBalance.ToString());
            txtTax.Attributes.Add("onblur", atrBalance.ToString());
            txtTax.Attributes.Add("onchange", atrBalance.ToString());
            txtSalesTax.Attributes.Add("onblur", atrBalance.ToString());
            txtSalesTax.Attributes.Add("onchange", atrBalance.ToString());

            atrReceivedAmount.Append("CheckReceivedAmount(");
            atrReceivedAmount.Append(chRelized.ClientID);
            atrReceivedAmount.Append(",");
            atrReceivedAmount.Append(txtCrdAmount.ClientID);
            atrReceivedAmount.Append(",");
            atrReceivedAmount.Append(txtRcdAmount.ClientID);
            atrReceivedAmount.Append(",");
            atrReceivedAmount.Append(txtBalAmount.ClientID);
            atrReceivedAmount.Append(",");
            atrReceivedAmount.Append(txtTaxAmount.ClientID);
            atrReceivedAmount.Append(");");
            chRelized.Attributes.Add("onchange", atrReceivedAmount.ToString());
        }
    }
    protected void LoadStatus()
    {
        DrpStatus.Items.Add(new ListItem("Cheque Received", "527"));
        DrpStatus.Items.Add(new ListItem("Cheque Deposit", "528"));
        DrpStatus.Items.Add(new ListItem("Cheque Realize", "529"));
        DrpStatus.Items.Add(new ListItem("Cheque Bounce", "530"));
        DrpStatus.Items.Add(new ListItem("Cheque Cancel", "560"));
    }
    #endregion
    #region Click Operations
    /// <summary>
    /// Save Or Updates a Cheque
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>   
    protected void btnSave_Click2(object sender, EventArgs e)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }

        if (Session["CHEQUE_NO"] == null)
        {
            if (!CheckChequeNo())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Cheque No " + txtChequeNo.Text.Trim() + " already exists.');", true);
                return;
            }
        }
        else
        {
            if (txtChequeNo.Text.Trim() == Session["CHEQUE_NO"].ToString())
            {
                //Do nothing.
            }
            else
            {
                if (!CheckChequeNo())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Cheque No " + txtChequeNo.Text.Trim() + " already exists.');", true);
                    return;
                }
            }
        }
        ChequeEntryController CController = new ChequeEntryController();
        DateTime ChequeDate;
        decimal CheckAmount = 0;
        if (txtStartDate.Text.Length == 10)
        {
            try
            {
                ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));
            }
            catch (Exception ex)
            {
                ChequeDate = DateTime.Now;
            }
        }
        else
        {
            ChequeDate = DateTime.Now;
        }
        if (btnSave.Text == "Save")
        {
            if (Session["TypeID"].ToString() == "0")
            {
                UpdateInvoiceSessionData();
            }
            else
            {
                CheckAmount = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
            }
            int InvoiceCount = Constants.IntNullValue;
            DataTable dtCreditSession = (DataTable)Session["dtCreditSession"];

            if (Session["TypeID"].ToString() == "0")
            {
                if (dtCreditSession != null)
                {
                    foreach (DataRow dr in dtCreditSession.Rows)
                    {
                        if (dr["is_Checked"].ToString() == "True")
                        {
                            CheckAmount += decimal.Parse(dr["CHEQUE_AMOUNT_TAX"].ToString());
                            InvoiceCount++;
                        }
                    }
                }
                if (InvoiceCount == Constants.IntNullValue)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No Invoice selected');", true);
                    return;
                }
            }
            if (DrpStatus.SelectedValue == Constants.Cheque_Pending.ToString())
            {
                HFChqueProcessId.Value = CController.InsertChequeEntry(int.Parse(Session["LocationID"].ToString()), int.Parse(Session["PrincipalID"].ToString()), 0, txtChequeNo.Text, txtBankName.Text, ChequeDate,
                    CurrentWorkDate, Constants.DateNullValue, Constants.DateNullValue, CheckAmount, int.Parse(DrpStatus.SelectedValue), DateTime.Now, Convert.ToInt32(Session["TypeID"]), txtSlipNo.Text, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()), long.Parse(DrpAccountHead.SelectedValue), long.Parse(DrpDeliveryMan.SelectedValue), Convert.ToInt32(ddlHeadOffice.SelectedValue));
                if (Session["TypeID"].ToString() == "0")
                {
                    foreach (DataRow dr in dtCreditSession.Rows)
                    {
                        if (dr["is_Checked"].ToString() == "True")
                        {
                            CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(dr["SALE_INVOICE_ID"].ToString()), decimal.Parse(dr["Current_Received"].ToString()), long.Parse(dr["CUSTOMER_ID"].ToString()), decimal.Parse(dr["Tax"].ToString()), decimal.Parse(dr["Tax2"].ToString()), decimal.Parse(dr["CHEQUE_AMOUNT_TAX"].ToString()));
                        }
                    }
                }
                else
                {
                    DataTable dtCustomer = (DataTable)Session["dtCustomer"];
                    foreach (DataRow dr in dtCustomer.Rows)
                    {
                        CController.InsertChequeCustomer(Convert.ToInt64(HFChqueProcessId.Value), Convert.ToInt64(dr["CUSTOMER_ID"]));
                    }
                }
                ClearAll();
                if (Session["TypeID"].ToString() == "0")
                {
                    LoadEditGrid();
                    LoadInvoiceGrid();
                }
                Session.Remove("CHEQUE_NO");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Cheque added successfully.');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "Redirect();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('First receive cheque.');", true);
            }
        }
        else
        {
            if (Session["TypeID"].ToString() == "0")
            {
                UpdateSessionTable();
            }
            else
            {
                CheckAmount = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
            }
            DataTable dtCredit = (DataTable)Session["dtCredit"];
            int InvoiceCount = 0;
            if (Session["TypeID"].ToString() == "0")
            {
                if (dtCredit != null)
                {
                    foreach (DataRow dr in dtCredit.Rows)
                    {
                        if (Convert.ToInt64(dr["IS_CurrentRecived"]) > 0)
                        {
                            InvoiceCount++;
                            CheckAmount += decimal.Parse(dr["CurrentReceivedAmount"].ToString());
                        }
                    }
                }
                if (InvoiceCount == Constants.IntNullValue)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No Invoice selected');", true);
                    return;
                }
            }
            if (int.Parse(DrpStatus.SelectedValue) == Constants.Cheque_Pending)
            {
                IDbConnection mConnection = null;
                IDbTransaction mTransection = null;
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransection = ProviderFactory.GetTransaction(mConnection);
                try
                {
                    bool Ischeque = CController.UpdateChequeEntry2(long.Parse(HFChqueProcessId.Value),
                                                                   int.Parse(Session["LocationID"].ToString()),
                                                                   int.Parse(Session["PrincipalID"].ToString()), 0,
                                                                   txtChequeNo.Text, txtBankName.Text, ChequeDate,
                                                                   Convert.ToDateTime(txtReceivedDate.Text),
                                                                   Constants.DateNullValue, Constants.DateNullValue,
                                                                   CheckAmount, int.Parse(DrpStatus.SelectedValue),
                                                                   Constants.DateNullValue, txtSlipNo.Text, Convert.ToInt32(Session["TypeID"]),
                                                                   txtRemarks.Text,
                                                                   int.Parse(DrpBankAccount.SelectedValue.ToString()),
                                                                   mTransection, mConnection, long.Parse(DrpAccountHead.SelectedValue), long.Parse(DrpDeliveryMan.SelectedValue));
                    if (!Ischeque)
                    {
                        throw new ArgumentNullException();
                    }
                    bool IsUpdate = CController.SelectChequeEntryInvoice2(long.Parse(HFChqueProcessId.Value), 1, mTransection, mConnection);
                    if (!IsUpdate)
                    {
                        throw new ArgumentNullException();
                    }
                    if (Session["TypeID"].ToString() == "0")
                    {
                        foreach (DataRow dr in dtCredit.Rows)
                        {
                            if (Convert.ToInt64(dr["IS_CurrentRecived"]) != 0) //Convert.ToInt64(dr["IS_Recived"]) != 0)
                            {
                                bool IsInsert = CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(dr["SALE_INVOICE_ID"]), decimal.Parse(dr["CurrentReceivedAmount"].ToString()), Convert.ToInt64(dr["CUSTOMER_ID"].ToString()), decimal.Parse(dr["TAX"].ToString()), decimal.Parse(dr["TAX2"].ToString()), decimal.Parse(dr["CHEQUE_AMOUNT_TAX"].ToString()), mTransection, mConnection);
                                if (!IsInsert)
                                {
                                    throw new ArgumentNullException();
                                }
                            }
                        }
                    }
                    else
                    {
                        DataTable dtCustomer = (DataTable)Session["dtCustomer"];
                        foreach (DataRow dr in dtCustomer.Rows)
                        {
                            CController.InsertChequeCustomer(Convert.ToInt64(HFChqueProcessId.Value), Convert.ToInt64(dr["CUSTOMER_ID"]), mTransection, mConnection);
                        }
                    }
                    mTransection.Commit();
                }
                catch (Exception exp)
                {
                    mTransection.Rollback();
                    ExceptionPublisher.PublishException(exp);
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                                                        "alert('  " + "" + "Some Error ", true);
                }
                finally
                {
                    if (mConnection != null && mConnection.State == ConnectionState.Open)
                    {
                        mConnection.Close();
                    }
                }
                ClearAll();
                if (Session["TypeID"].ToString() == "0")
                {
                    LoadEditGrid();
                }
                Session.Remove("CHEQUE_NO");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Cheque updated successfully.');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "Redirect();", true);
            }
            else if (int.Parse(DrpStatus.SelectedValue) == Constants.Cheque_Deposit)
            {
                CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, txtChequeNo.Text, null, Constants.DateNullValue, Constants.DateNullValue, CurrentWorkDate, Constants.DateNullValue,
                     Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue), Constants.DateNullValue, txtSlipNo.Text, Convert.ToInt32(Session["TypeID"]), txtRemarks.Text, int.Parse(DrpBankAccount.SelectedValue.ToString()), long.Parse(DrpAccountHead.SelectedValue), long.Parse(DrpDeliveryMan.SelectedValue));
                ClearAll();
                if (Session["TypeID"].ToString() == "0")
                {
                    LoadEditGrid();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Cheque updated successfully.');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "Redirect();", true);
            }
            else if (int.Parse(DrpStatus.SelectedValue) == Constants.Cheque_Bons || int.Parse(DrpStatus.SelectedValue) == Constants.Cheque_Cancel)
            {
                CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, txtChequeNo.Text, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, CurrentWorkDate,
                                                 Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue), Constants.DateNullValue, txtSlipNo.Text, Convert.ToInt32(Session["TypeID"]), txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));
                ClearAll();
                if (Session["TypeID"].ToString() == "0")
                {
                    LoadEditGrid();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Cheque updated successfully.');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "Redirect();", true);
            }
            else if (int.Parse(DrpStatus.SelectedValue) == Constants.Cheque_Clear)
            {
                string IsGl = null;
                string IsGl2 = null;
                string isInsert = null;
                /////
                IDbConnection mConnection = null;
                IDbTransaction mTransection = null;
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransection = ProviderFactory.GetTransaction(mConnection);
                try
                {
                    decimal TaxAmount = 0;
                    decimal TaxAmount2 = 0;
                    long p_Customer_ID = Constants.LongNullValue;
                    string CustomerNAme = "";
                    decimal RecivedAmount = 0;
                    bool IsCheque = CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, txtChequeNo.Text, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, CurrentWorkDate,
                                                    Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue), Constants.DateNullValue, txtSlipNo.Text, Convert.ToInt32(Session["TypeID"]), txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()), mTransection, mConnection, long.Parse(DrpAccountHead.SelectedValue), long.Parse(DrpDeliveryMan.SelectedValue));
                    if (!IsCheque)
                    {
                        throw new ArgumentNullException();
                    }
                    CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1, mTransection, mConnection);
                    if (Session["TypeID"].ToString() == "0")
                    {
                        foreach (DataRow dr in dtCredit.Rows)
                        {
                            if (Convert.ToInt64(dr["IS_CurrentRecived"]) != 0)//Convert.ToInt64(dr["IS_Recived"]) != 0 &&
                            {
                                CustomerNAme = dr["CUSTOMER_NAME"].ToString();
                                bool IsInsert = CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(dr["SALE_INVOICE_ID"]), decimal.Parse(dr["CurrentReceivedAmount"].ToString()), Convert.ToInt64(dr["CUSTOMER_ID"].ToString()), decimal.Parse(dr["TAX"].ToString()), decimal.Parse(dr["TAX2"].ToString()), decimal.Parse(dr["CHEQUE_AMOUNT_TAX"].ToString()), mTransection, mConnection);
                                if (!IsInsert)
                                {
                                    throw new ArgumentNullException();
                                }
                                TaxAmount = TaxAmount + (decimal.Parse(dr["CurrentReceivedAmount"].ToString()) * decimal.Parse(dr["TAX"].ToString())) / 100;
                                TaxAmount2 = TaxAmount2 + (decimal.Parse(dr["InvoiceTax"].ToString()) * decimal.Parse(dr["TAX2"].ToString())) / 100;
                                p_Customer_ID = Convert.ToInt64(dr["CUSTOMER_ID"]);
                                RecivedAmount = RecivedAmount + decimal.Parse(dr["CurrentReceivedAmount"].ToString());
                            }
                        }
                    }
                    else
                    {
                        DataTable dtCustomer = (DataTable)Session["dtCustomer"];
                        foreach (DataRow dr in dtCustomer.Rows)
                        {
                            CController.InsertChequeCustomer(Convert.ToInt64(HFChqueProcessId.Value), Convert.ToInt64(dr["CUSTOMER_ID"]), mTransection, mConnection);
                        }
                    }
                    if (Session["TypeID"].ToString() == "0")
                    {
                        isInsert = InsertGL(Convert.ToInt64(HFChqueProcessId.Value), Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), CustomerNAme, mTransection, mConnection, txtRemarks.Text, "Cheque Realized(Channel)");
                        if (isInsert == null)
                        {
                            throw new ArgumentNullException();
                        }
                    }
                    else
                    {
                        DataTable dtCustomer = (DataTable)Session["dtCustomer"];
                        isInsert = InsertGL(Convert.ToInt64(HFChqueProcessId.Value), Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), CustomerNAme, mTransection, mConnection, txtRemarks.Text, "Cheque Advance Realized(Channel)");
                        if (isInsert == null)
                        {
                            throw new ArgumentNullException();
                        }
                    }
                    if (Session["TypeID"].ToString() == "0")
                    {
                        if (TaxAmount > 0)
                        {
                            IsGl = InsertGLTax(Convert.ToInt64(HFChqueProcessId.Value), Constants.LongNullValue, TaxAmount, CustomerNAme, mTransection, mConnection, txtRemarks.Text);
                            if (IsGl == null)
                            {
                                throw new ArgumentNullException();
                            }
                        }
                        if (TaxAmount2 > 0)
                        {
                            IsGl2 = InsertGLTax2(Convert.ToInt64(HFChqueProcessId.Value), Constants.LongNullValue, TaxAmount2, CustomerNAme, mTransection, mConnection, txtRemarks.Text);
                            if (IsGl == null)
                            {
                                throw new ArgumentNullException();
                            }
                        }
                    }
                    if (Session["TypeID"].ToString() == "0")
                    {
                        bool Isreliaze = ChequeRealization(mTransection, mConnection);
                        if (!Isreliaze)
                        {
                            throw new ArgumentNullException();
                        }
                        mTransection.Commit();
                    }
                    else
                    {
                        bool Isreliaze = ChequeRealizationAdvance(mTransection, mConnection);
                        if (!Isreliaze)
                        {
                            throw new ArgumentNullException();
                        }
                        mTransection.Commit();
                    }
                }
                catch (Exception exp)
                {
                    mTransection.Rollback();
                    ExceptionPublisher.PublishException(exp);
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + "" + "Some Error ", true);
                }
                finally
                {
                    if (mConnection != null && mConnection.State == ConnectionState.Open)
                    {
                        mConnection.Close();
                    }
                }
                ClearAll();
                if (Session["TypeID"].ToString() == "0")
                {
                    LoadEditGrid();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Cheque updated successfully.');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", "Redirect();", true);
                PrintReport(isInsert + "," + IsGl + "," + IsGl2);
            }
        }
    }
    /// <summary>
    /// Cancels Cheque Entry
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmChequeEntryView.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
    }
    #endregion
    #region Realization
    private bool ChequeRealization(IDbTransaction mTransaction, IDbConnection mConnection)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        LedgerController LController = new LedgerController();
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        DataTable dtCredit = (DataTable)Session["dtCredit"];
        foreach (DataRow dr in dtCredit.Rows)
        {
            if (Convert.ToInt64(dr["IS_Recived"]) != 0 && Convert.ToInt64(dr["IS_CurrentRecived"]) != 0)
            {
                decimal CreditAmount = Convert.ToDecimal(Convert.ToDecimal(dr["CURRENT_CREDIT_AMOUNT"]) - Convert.ToDecimal(dr["RCDAMOUNT"]) + Convert.ToDecimal(dr["CurrentReceivedAmount"]));
                string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(Session["LocationID"].ToString()), mTransaction, mConnection);
                decimal OfferAmount2 = decimal.Parse(dr["CurrentReceivedAmount"].ToString());
                if (CreditAmount >= OfferAmount2)
                {
                    bool IsPosting = LController.PostingCash_Bank_Account3(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(Session["LocationID"].ToString()), 0, OfferAmount2,
                      CurrentWorkDate, "Cheque Realization", DateTime.Now, int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(Session["PrincipalID"].ToString()),
                      txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(dr["SALE_INVOICE_ID"]), dr["MANUAL_INVOICE_ID"].ToString(), Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, DrpDeliveryMan.SelectedValue.ToString(),Convert.ToInt32(ddlHeadOffice.SelectedValue), mTransaction, mConnection);
                    if (!IsPosting)
                    {
                        return false;
                    }
                    bool IsPosting2 = LController.PostingCash_Bank_Account3(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(Session["LocationID"].ToString()), OfferAmount2, 0,
                      CurrentWorkDate, "Cheque Realization", DateTime.Now, int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(Session["PrincipalID"].ToString()),
                      txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(dr["SALE_INVOICE_ID"]), dr["MANUAL_INVOICE_ID"].ToString(), Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, DrpDeliveryMan.SelectedValue.ToString(),Convert.ToInt32(ddlHeadOffice.SelectedValue), mTransaction, mConnection);
                    if (!IsPosting2)
                    {
                        return false;
                    }
                    OfferAmount2 = decimal.Parse(dr["CURRENT_CREDIT_AMOUNT"].ToString()) - OfferAmount2;
                    bool Isupdate = LController.UpdateSaleInvoice(Convert.ToInt64(dr["SALE_INVOICE_ID"]), int.Parse(Session["LocationID"].ToString()), OfferAmount2, mTransaction, mConnection);
                    if (!Isupdate)
                    {
                        return false;
                    }
                    //   break;
                }
                else if (CreditAmount <= OfferAmount2)
                {
                    bool IsPosting2 = LController.PostingCash_Bank_Account3(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(Session["LocationID"].ToString()), 0, CreditAmount,
                    CurrentWorkDate, "Cheque Realization", DateTime.Now, int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(Session["PrincipalID"].ToString()),
                    txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(dr["SALE_INVOICE_ID"]), dr["MANUAL_INVOICE_ID"].ToString(), Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, DrpDeliveryMan.SelectedValue.ToString(),Convert.ToInt32(ddlHeadOffice.SelectedValue), mTransaction, mConnection);
                    if (!IsPosting2)
                    {
                        return false;
                    }
                    IsPosting2 = LController.PostingCash_Bank_Account3(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(Session["LocationID"].ToString()), CreditAmount, 0,
                   CurrentWorkDate, "Cheque Realization", DateTime.Now, int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(Session["PrincipalID"].ToString()),
                   txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(dr["SALE_INVOICE_ID"]), dr["MANUAL_INVOICE_ID"].ToString(), Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, DrpDeliveryMan.SelectedValue.ToString(),Convert.ToInt32(ddlHeadOffice.SelectedValue), mTransaction, mConnection);
                    if (!IsPosting2)
                    {
                        return false;
                    }
                    OfferAmount2 = OfferAmount2 - CreditAmount;
                    bool Isupdate = LController.UpdateSaleInvoice(Convert.ToInt64(dr["SALE_INVOICE_ID"]), int.Parse(Session["LocationID"].ToString()), 0, mTransaction, mConnection);
                    if (!Isupdate)
                    {
                        return false;
                    }
                }

            }
        }
        return true;
    }

    private bool ChequeRealizationAdvance(IDbTransaction mTransaction, IDbConnection mConnection)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        DataTable dtCustomer = (DataTable)Session["dtCustomer"];
        LedgerController LController = new LedgerController();
        string MaxDocumentId = string.Empty;
        bool IsPosting = false;
        foreach (DataRow dr in dtCustomer.Rows)
        {
            MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(Session["LocationID"].ToString()), mTransaction, mConnection);

            IsPosting = LController.PostingCash_Bank_Account3(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(Session["LocationID"].ToString()), 0, decimal.Parse(txtAmount.Text),
                          CurrentWorkDate, "Cheque Realization (Advance)", DateTime.Now, int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(Session["PrincipalID"].ToString()),
                          txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, txtSlipNo.Text, Constants.DateNullValue, 20, DrpDeliveryMan.SelectedValue.ToString(), Convert.ToInt32(ddlHeadOffice.SelectedValue), mTransaction, mConnection);

            IsPosting = LController.PostingCash_Bank_Account3(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(Session["LocationID"].ToString()), decimal.Parse(txtAmount.Text), 0,
              CurrentWorkDate, "Cheque Realization (Advance)", DateTime.Now, int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(Session["PrincipalID"].ToString()),
              txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 20, DrpDeliveryMan.SelectedValue.ToString(), Convert.ToInt32(ddlHeadOffice.SelectedValue), mTransaction, mConnection);
        }
        return IsPosting;
    }
    private string InsertGL(long ChequeProcessID, decimal Amount, string p_Customer_Name, IDbTransaction mTransaction, IDbConnection mConenction, String p_Remarks,string p_Remarks2)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        CustomerDataController CustCtl = new CustomerDataController();
        DataTable dtChannel = new DataTable();
        string MaxDocumentId = null;      
        DataTable dtCustomer = (DataTable) Session["dtCustomer"];
        DataTable dt = CustCtl.GetCustomer(Convert.ToInt32(Session["LocationID"]),Convert.ToInt64(dtCustomer.Rows[0]["CUSTOMER_ID"]));
        dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, Convert.ToInt32(dt.Rows[0]["CHANNEL_TYPE_ID"]));

        if (dtChannel.Rows.Count > 0)
        {
            DataTable dtVoucher = new DataTable();
            dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
            dtVoucher.Columns.Add("Debit", typeof(decimal));
            dtVoucher.Columns.Add("Credit", typeof(decimal));
            dtVoucher.Columns.Add("Remarks", typeof(string));
            dtVoucher.Columns.Add("Principal_Id", typeof(string));

            DataRow drChannel = dtVoucher.NewRow();
            drChannel["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
            drChannel["REMARKS"] = p_Remarks2;
            drChannel["DEBIT"] = 0;
            drChannel["CREDIT"] = Amount;
            drChannel["Principal_Id"] = Convert.ToInt32(Session["PrincipalID"]);
            dtVoucher.Rows.Add(drChannel);

            DataRow drBank = dtVoucher.NewRow();
            drBank["ACCOUNT_HEAD_ID"] = DrpBankAccount.SelectedValue;
            drBank["REMARKS"] = "Cheque Realized(" + DrpBankAccount.SelectedItem.Text + ")";
            drBank["DEBIT"] = Amount;
            drBank["CREDIT"] = 0;
            drBank["Principal_Id"] = Convert.ToInt32(Session["PrincipalID"]);
            dtVoucher.Rows.Add(drBank);

            if (dtVoucher.Rows.Count > 0)
            {
                LedgerController LController = new LedgerController();
                MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(Session["LocationID"]), CurrentWorkDate, mTransaction, mConenction);

                bool isinsert = LController.Add_Voucher2(Convert.ToInt32(Session["LocationID"]), 0, MaxDocumentId, Constants.Bank_Voucher, CurrentWorkDate, Constants.CashPayment, "N/A", "Default Cheque Realization Voucher, " + p_Customer_Name + ", SP :" + DrpDeliveryMan.SelectedItem.Text + " , Cheque # :" + txtChequeNo.Text + " , Rs: " + Math.Round(Amount, 2) + " , " + p_Remarks + "", Constants.DateNullValue, null, ChequeProcessID, Constants.Cheque_Relization, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text.Trim(), Constants.DateNullValue, mTransaction, mConenction);
                if (!isinsert)
                {
                    return null;
                }
            }
        }
        return MaxDocumentId;
    }

    private string InsertGLAdvance(long ChequeProcessID, decimal Amount, string p_Customer_Name, IDbTransaction mTransaction, IDbConnection mConenction, String p_Remarks,int pChannelType)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        CustomerDataController CustCtl = new CustomerDataController();
        DataTable dtChannel = new DataTable();
        string MaxDocumentId = null;
        dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, pChannelType);

        if (dtChannel.Rows.Count > 0)
        {
            DataTable dtVoucher = new DataTable();
            dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
            dtVoucher.Columns.Add("Debit", typeof(decimal));
            dtVoucher.Columns.Add("Credit", typeof(decimal));
            dtVoucher.Columns.Add("Remarks", typeof(string));
            dtVoucher.Columns.Add("Principal_Id", typeof(string));

            DataRow drChannel = dtVoucher.NewRow();
            drChannel["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
            drChannel["REMARKS"] = "Cheque Advance Realized(Channel)";
            drChannel["DEBIT"] = 0;
            drChannel["CREDIT"] = Amount;
            drChannel["Principal_Id"] = Convert.ToInt32(Session["PrincipalID"]);
            dtVoucher.Rows.Add(drChannel);

            DataRow drBank = dtVoucher.NewRow();
            drBank["ACCOUNT_HEAD_ID"] = DrpBankAccount.SelectedValue;
            drBank["REMARKS"] = "Cheque Advance Realized(" + DrpBankAccount.SelectedItem.Text + ")";
            drBank["DEBIT"] = Amount;
            drBank["CREDIT"] = 0;
            drBank["Principal_Id"] = Convert.ToInt32(Session["PrincipalID"]);
            dtVoucher.Rows.Add(drBank);

            if (dtVoucher.Rows.Count > 0)
            {
                LedgerController LController = new LedgerController();
                MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(Session["LocationID"]), CurrentWorkDate, mTransaction, mConenction);

                bool isinsert = LController.Add_Voucher2(Convert.ToInt32(Session["LocationID"]), 0, MaxDocumentId, Constants.Bank_Voucher, CurrentWorkDate, Constants.CashPayment, "N/A", "Default Advance Cheque Realization Voucher, " + p_Customer_Name + ", SP :" + DrpDeliveryMan.SelectedItem.Text + " , Cheque # :" + txtChequeNo.Text + " , Rs: " + Math.Round(Amount, 2) + " , " + p_Remarks + "", Constants.DateNullValue, null, ChequeProcessID, Constants.Cheque_Relization, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text.Trim(), Constants.DateNullValue, mTransaction, mConenction);
                if (!isinsert)
                {
                    return null;
                }
            }
        }
        return MaxDocumentId;
    }
    private string InsertGLTax(long ChequeProcessID, long Customer_ID, decimal Amount, string p_Customer_Name, IDbTransaction mTransaction, IDbConnection mConnection, string p_Remarks)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        string MaxDocumentId = null;
        DataTable dtChannel = new DataTable();
        CustomerDataController CustCtl = new CustomerDataController();
        dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, Convert.ToInt32(gvInvoiceEdit.Rows[0].Cells[11].Text));
        if (dtChannel.Rows.Count > 0)
        {
            Customer_ID = long.Parse(dtChannel.Rows[0]["CREDIT_HEAD_ID"].ToString());
        }
        LedgerController LController = new LedgerController();
        MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(Session["LocationID"]), CurrentWorkDate, mTransaction, mConnection);

        bool IsTax = LController.Add_Tax_Voucer(Convert.ToInt32(Session["LocationID"]), 0, MaxDocumentId, Constants.Bank_Voucher,CurrentWorkDate, Constants.CashPayment, "N/A", "Cheque Realization Voucher(W.H. Income Tax), " + p_Customer_Name + ", SP :" + DrpDeliveryMan.SelectedItem.Text + ", Cheque # :" + txtChequeNo.Text + " Rs: " + Math.Round(Amount, 2) + " , " + p_Remarks + "", Constants.DateNullValue, null, ChequeProcessID, Constants.Cheque_Relization, null, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text.Trim(), Constants.DateNullValue, long.Parse(DrpAccountHead.SelectedValue), Customer_ID, Amount, Amount, mTransaction, mConnection);

        if (!IsTax)
        {
            return null;
        }
        return MaxDocumentId;
    }
    private string InsertGLTax2(long ChequeProcessID, long Customer_ID, decimal Amount, string p_Customer_Name, IDbTransaction mTransaction, IDbConnection mConnection, string p_Remarks)
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["LocationID"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        string MaxDocumentId = null;
        DataTable dtChannel = new DataTable();
        CustomerDataController CustCtl = new CustomerDataController();
        dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, Convert.ToInt32(gvInvoiceEdit.Rows[0].Cells[11].Text));
        if (dtChannel.Rows.Count > 0)
        {
            Customer_ID = long.Parse(dtChannel.Rows[0]["CREDIT_HEAD_ID"].ToString());
        }
        LedgerController LController = new LedgerController();
        MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(Session["LocationID"]), CurrentWorkDate, mTransaction, mConnection);

        bool IsTax = LController.Add_Tax_Voucer(Convert.ToInt32(Session["LocationID"]), 0, MaxDocumentId, Constants.Bank_Voucher, CurrentWorkDate, Constants.CashPayment, "N/A", "Cheque Realization Voucher(W.H. Sales Tax), " + p_Customer_Name + ", SP :" + DrpDeliveryMan.SelectedItem.Text + ", Cheque # :" + txtChequeNo.Text + " Rs: " + Math.Round(Amount, 2) + " , " + p_Remarks + "", Constants.DateNullValue, null, ChequeProcessID, Constants.Cheque_Relization, null, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text.Trim(), Constants.DateNullValue, long.Parse(ddlSalesTax.SelectedValue), Customer_ID, Amount, Amount, mTransaction, mConnection);

        if (!IsTax)
        {
            return null;
        }
        return MaxDocumentId;
    }
    #endregion
    #region Paging
    private void LoadEditGrid()
    {
        gvInvoiceEdit.DataSource = null;
        gvInvoiceEdit.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 50;

        DataTable dtCredit2 = new DataTable();
        dtCredit2 = (DataTable)Session["dtCredit2"];
        if (dtCredit2 != null)
        {
            if (dtCredit2.Rows.Count > 0)
            {
                PagedResults.CurrentPageIndex = CurrentPage;

                gvInvoiceEdit.DataSource = dtCredit2;
                gvInvoiceEdit.DataBind();

                linkbtnnext.Visible = true;
                linkbtnprev.Visible = true;
                lblTotalNoOfPages.Visible = true;
                lblCurrentPageNo.Visible = true;
                lblTotalNoOfRecords.Visible = true;
                lblDummy.Visible = true;
                lblOf.Visible = true;
                lblTotalNoOfRecords.Text = dtCredit2.Rows[0]["TotalRows"].ToString();
                lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
                lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dtCredit2.Rows[0]["TotalRows"]) / Convert.ToDecimal(50)));

                if (lblCurrentPageNo.Text == lblTotalNoOfPages.Text)
                {
                    linkbtnnext.Enabled = false;
                }
                else
                {
                    linkbtnnext.Enabled = true;
                }
                if (lblCurrentPageNo.Text == "1")
                {
                    linkbtnprev.Enabled = false;
                }
                else
                {
                    linkbtnprev.Enabled = true;
                }

            }
            else if (dtCredit2.Rows.Count == 0)
            {
                linkbtnnext.Visible = false;
                linkbtnprev.Visible = false;
                lblTotalNoOfPages.Visible = false;
                lblCurrentPageNo.Visible = false;
                lblTotalNoOfRecords.Visible = false;
                lblOf.Visible = false;
                lblDummy.Visible = false;
                lblTotalNoOfRecords.Text = "";
            }
        }

    }
    private void LoadInvoiceGrid()
    {
        gvInvoice.DataSource = null;
        gvInvoice.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 50;

        DataTable dtCreditview = new DataTable();
        dtCreditview = (DataTable)Session["dtCreditview"];
        if (dtCreditview != null)
        {
            if (dtCreditview.Rows.Count > 0)
            {
                PagedResults.CurrentPageIndex = CurrentPage;

                gvInvoice.DataSource = dtCreditview;
                gvInvoice.DataBind();
                linkbtnnext.Visible = true;
                linkbtnprev.Visible = true;
                lblTotalNoOfPages.Visible = true;
                lblCurrentPageNo.Visible = true;
                lblTotalNoOfRecords.Visible = true;
                lblDummy.Visible = true;
                lblOf.Visible = true;
                lblTotalNoOfRecords.Text = dtCreditview.Rows[0]["TotalRows"].ToString();
                lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
                lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dtCreditview.Rows[0]["TotalRows"]) / Convert.ToDecimal(50)));

                if (lblCurrentPageNo.Text == lblTotalNoOfPages.Text)
                {
                    linkbtnnext.Enabled = false;
                }
                else
                {
                    linkbtnnext.Enabled = true;
                }
                if (lblCurrentPageNo.Text == "1")
                {
                    linkbtnprev.Enabled = false;
                }
                else
                {
                    linkbtnprev.Enabled = true;
                }

            }
            else if (dtCreditview.Rows.Count == 0)
            {
                linkbtnnext.Visible = false;
                linkbtnprev.Visible = false;
                lblTotalNoOfPages.Visible = false;
                lblCurrentPageNo.Visible = false;
                lblTotalNoOfRecords.Visible = false;
                lblOf.Visible = false;
                lblDummy.Visible = false;
                lblTotalNoOfRecords.Text = "";
            }
        }
    }
    public int CurrentPage
    {
        get
        {
            object objview = ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            else
                return (int)objview;
        }
        set
        {
            ViewState["_CurrentPage"] = value;
        }
    }
    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        ChbAllCatagories.Checked = false;
        CurrentPage -= 1;
        if (Convert.ToInt64(Session["ChequeID"]) > 0)
        {
            UpdateSessionTable();
            LoadDataEdit2(Convert.ToInt64(Session["ChequeID"]));
            UpdateGridView();
        }
        else
        {
            // mean new cheque
            UpdateInvoiceSessionData();
            UpdateInvoiceGridView();
            LoadInvoiceGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "ChChequeListSelect();", true);

        }
    }
    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        ChbAllCatagories.Checked = false;
        CurrentPage += 1;
        if (Convert.ToInt64(Session["ChequeID"]) > 0)
        {
            UpdateSessionTable();
            LoadDataEdit2(Convert.ToInt64(Session["ChequeID"]));
        }
        else
        {
            UpdateInvoiceSessionData();
            UpdateInvoiceGridView();
            LoadInvoiceGrid();            
        }
    }
    private void UpdateSessionTable()
    {
        DataTable dtCredit = (DataTable)Session["dtCredit"];
        DataTable dtCredit2 = (DataTable)Session["dtCredit2"];

        foreach (GridViewRow gvr in gvInvoiceEdit.Rows)
        {
            foreach (DataRow dr in dtCredit.Rows)
            {
                if (dr["SALE_INVOICE_ID"].ToString() == gvr.Cells[17].Text)
                {
                    CheckBox ChbIsAssigned = (CheckBox)gvr.FindControl("ChbIsAssigned");
                    TextBox txtRcdAmount = (TextBox)gvr.FindControl("txtRcdAmount");
                    TextBox txtTax = (TextBox)gvr.FindControl("txtTax");
                    TextBox txtSalesTax = (TextBox)gvr.FindControl("txtSalesTax");
                    TextBox txtChequeAmountTax = (TextBox)gvr.FindControl("txtChequeAmountTax");
                    dr["CurrentReceivedAmount"] = txtRcdAmount.Text;
                    dr["TAX"] = txtTax.Text;
                    dr["TAX2"] = txtSalesTax.Text;
                    dr["CHEQUE_AMOUNT_TAX"] = txtChequeAmountTax.Text;
                    if (ChbIsAssigned.Checked)
                    {
                        dr["IS_Recived"] = 1;
                        dr["IS_CurrentRecived"] = 1;//  used this to update cheque in same status

                    }
                    else
                    {
                        dr["IS_Recived"] = 0;
                        dr["IS_CurrentRecived"] = 0; //  used this to update cheque in same status
                    }
                }
            }
        }
        Session.Add("dtCredit", dtCredit);
        CalculateChequeAmount(2);
    }
    private void UpdateGridView()
    {
        DataTable dtCredit = (DataTable)Session["dtCredit"];
        DataTable dtCredit2 = (DataTable)Session["dtCredit2"];

        foreach (DataRow dr in dtCredit.Rows)
        {
            foreach (GridViewRow gvr in gvInvoiceEdit.Rows)
            {
                if (dr["SALE_INVOICE_ID"].ToString() == gvr.Cells[14].Text)
                {
                    CheckBox ChbIsAssigned = (CheckBox)gvr.FindControl("ChbIsAssigned");
                    TextBox txtRcdAmount = (TextBox)gvr.FindControl("txtRcdAmount");
                    TextBox txtTax = (TextBox)gvr.FindControl("txtTax");
                    TextBox txtSalesTax = (TextBox)gvr.FindControl("txtSalesTax");
                    TextBox txtChequeAmountTax = (TextBox)gvr.FindControl("txtChequeAmountTax");
                    txtRcdAmount.Text = dr["CurrentReceivedAmount"].ToString();
                    txtTax.Text = dr["TAX"].ToString();
                    txtSalesTax.Text = dr["TAX2"].ToString();
                    txtChequeAmountTax.Text = dr["CHEQUE_AMOUNT_TAX"].ToString();

                    if (Convert.ToInt64(dr["IS_Recived"]) != 0)
                    {
                        ChbIsAssigned.Checked = true;
                    }
                    else
                    {
                        ChbIsAssigned.Checked = false;
                    }
                }
            }
        }
    }
    private void UpdateInvoiceGridView()
    {
        DataTable dtCreditSession = (DataTable)Session["dtCreditSession"];
        DataTable dtCreditview = dtCreditSession.Copy();
        dtCreditview.Clear();
        foreach (DataRow dr in dtCreditSession.Rows)
        {
            if (int.Parse(dr["RowID"].ToString()) > CurrentPage * 50 && int.Parse(dr["RowID"].ToString()) <= (CurrentPage + 1) * 50)
            {
                dtCreditview.ImportRow(dr);
            }
        }
        Session.Add("dtCreditview", dtCreditview);
    }
    private void UpdateInvoiceSessionData()
    {
        DataTable dtCreditSession = (DataTable)Session["dtCreditSession"];
        if (dtCreditSession != null)
        {
            foreach (GridViewRow gvr in gvInvoice.Rows)
            {
                foreach (DataRow dr in dtCreditSession.Rows)
                {
                    if (dr["MANUAL_INVOICE_ID"].ToString() == HttpUtility.HtmlDecode(gvr.Cells[2].Text))
                    {
                        CheckBox ChbIsAssigned = (CheckBox)gvr.FindControl("ChbIsAssigned");
                        TextBox txtRcdAmount = (TextBox)gvr.FindControl("txtRcdAmount");
                        TextBox txtInvoiceSalesTax = (TextBox)gvr.FindControl("txtInvoiceSalesTax");
                        TextBox txtTax = (TextBox)gvr.FindControl("txtTax");
                        TextBox txtSalesTax = (TextBox)gvr.FindControl("txtSalesTax");
                        TextBox txtChequeAmountTax = (TextBox)gvr.FindControl("txtChequeAmountTax");
                        
                        dr["Current_Received"] = txtRcdAmount.Text;
                        dr["TAX"] = txtTax.Text;
                        dr["TAX2"] = txtSalesTax.Text;                        
                        dr["CHEQUE_AMOUNT_TAX"] = decimal.Parse(txtRcdAmount.Text) - ((decimal.Parse(txtRcdAmount.Text) * decimal.Parse(txtTax.Text)) / 100) - ((decimal.Parse(txtInvoiceSalesTax.Text) * decimal.Parse(txtSalesTax.Text)) / 100);
                        if (ChbIsAssigned.Checked)
                        {
                            dr["is_Checked"] = 1;
                        }
                        else
                        {
                            dr["is_Checked"] = 0;
                        }
                    }
                }
                TextBox txtCrdAmount = (TextBox)gvr.FindControl("txtCrdAmount");
                TextBox txtRcdAmount2 = (TextBox)gvr.FindControl("txtRcdAmount");
                TextBox txtBalAmount = (TextBox)gvr.FindControl("txtBalAmount");
                txtBalAmount.Text = (Convert.ToDecimal(dc.chkNull_0(txtCrdAmount.Text)) - Convert.ToDecimal(dc.chkNull_0(txtRcdAmount2.Text))).ToString();
            }
        }
        Session.Add("dtCreditSession", dtCreditSession);
        CalculateChequeAmount(1);
    }
    #endregion
    protected void DrpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Status = 1;
        LoadDataEdit(Convert.ToInt64(Session["ChequeID"]));
        LoadDataEdit2(Convert.ToInt64(Session["ChequeID"]));
    }
    protected void ChbAllCatagories_CheckedChanged(object sender, EventArgs e)
    {
        UpdateInvoiceSessionData();
    }
    protected void ChbIsAssignedInvoice_CheckedChanged(object sender, EventArgs e)
    {
        UpdateInvoiceSessionData();
    }
    protected void ChbIsAssignedEdit_CheckedChanged(object sender, EventArgs e)
    {
        UpdateSessionTable();
    }
    protected void txtTaxPer_TextChanged(object sender, EventArgs e)
    {
        UpdateInvoiceSessionData();        
    }
    private void CalculateChequeAmount(int type)
    {
        decimal ChequeAmount = 0;
        DataTable dtCreditSession = (DataTable)Session["dtCreditSession"];
        DataTable dtCredit = (DataTable)Session["dtCredit"];
        if (type == 1)
        {
            if (dtCreditSession != null)
            {
                foreach (DataRow dr in dtCreditSession.Rows)
                {
                    if (dr["is_Checked"].ToString() == "True")
                    {
                        ChequeAmount = ChequeAmount + decimal.Parse(dr["CHEQUE_AMOUNT_TAX"].ToString());
                    }
                }
            }
        }
        else if (type == 2)
        {
            if (dtCredit != null)
            {
                foreach (DataRow dr in dtCredit.Rows)
                {
                    if (dr["IS_CurrentRecived"].ToString() != "0")
                    {
                        ChequeAmount = ChequeAmount + decimal.Parse(dr["CHEQUE_AMOUNT_TAX"].ToString());
                    }
                }
            }
        }
        txtAmount.Text = Math.Round(ChequeAmount, 2).ToString();
        hfAmount.Value = txtAmount.Text;
    }
    private void PrintReport(string p_Voucher_No)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(Session["LocationID"].ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint2(int.Parse(Session["LocationID"].ToString()), p_Voucher_No, Constants.Bank_Voucher);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        string url = "'Default.aspx'";        
        Response.Write("<script>window.open(" + url + ",'_blank');</script>");
    }
    protected void txtTaxInvoice_TextChanged(object sender, EventArgs e)
    {
        UpdateInvoiceSessionData();
    }
    protected void txtTaxEdit_TextChanged(object sender, EventArgs e)
    {
        UpdateSessionTable();
    }
    protected void txtRcdAmount_TextChanged(object sender, EventArgs e)
    {
        UpdateInvoiceSessionData();
    }
    protected void txtSalesTaxPer_TextChanged(object sender, EventArgs e)
    {
        UpdateInvoiceSessionData();        
    }
    private bool CheckChequeNo()
    {
        ChequeEntryController ChequeCtl = new ChequeEntryController();
        DataTable dt = ChequeCtl.CheckChequeNo(txtChequeNo.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}