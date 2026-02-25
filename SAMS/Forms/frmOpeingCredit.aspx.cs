using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// Form To Add Opening Credit
/// </summary>
public partial class Forms_frmOpeingCredit : System.Web.UI.Page
{
    readonly OrderEntryController _orderEntryCtrl = new OrderEntryController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtFromdate.Text =Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            LoadPrincipal();
            LoadDistributor();
            LoadArea();
            LoadData();
            LoadDeliveryman();
            LoadOpeningCredit();
            txtFromdate.Attributes.Add("Readonly", "Readonly");
            hfCustomerID.Value = "";
        }
    }

    #region Load
  
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    /// <summary>
    /// Loads Markets To Market Combo
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }  
    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadData()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpRoute.SelectedValue), Constants.IntNullValue, int.Parse(DrpPrincipal.SelectedValue));
            clsWebFormUtil.FillListBox(ListCustomer, dt, "CUSTOMER_DETAIL", "CUSTOMER_DETAIL", true);
            Session.Add("dt", dt);
        }
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1, true);
    }
    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            var mDController = new SaleForceController();
            DataTable mDt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(DrpDeliveryMan, mDt, 0, 3, true);
        }
        else
        {
            DrpDeliveryMan.Items.Clear();
        }
    }

    private void LoadOpeningCredit()
    {
        if (drpDistributor.Items.Count > 0)
        {
            var cdc = new CustomerDataController();
            DataTable dt = cdc.SelectOpeningCredit(int.Parse(drpDistributor.SelectedValue),DateTime.Parse(txtFromdate.Text));
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }

    #endregion

    #region Index Change
  
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadData();
        LoadDeliveryman();
        LoadOpeningCredit();
        btnSave.Text = "Save";
    }
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
       LoadData();
       LoadDeliveryman(); 
    }
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        LoadOpeningCredit();
    }
   
    #endregion

    /// <summary>
    /// Checks Invoice No in System
    /// </summary>
    /// <param name="pType">Type</param>
    /// <returns>True</returns>
    /// 
    private bool IsBillBookNoExist(int pType)
    {
        bool flag = false;
        if (txtChequeNo.Text.Trim().Length > 0)
        {
            
            DataTable dtBillBookNo = _orderEntryCtrl.SelectBillBookNo(Convert.ToInt32(drpDistributor.SelectedValue), txtChequeNo.Text, pType);
            if (dtBillBookNo.Rows.Count > 0)
            {
                flag = true;
            }
        }

        return flag;
    }

    #region Grid Operations
    
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hfLegendID.Value = GrdOrder.Rows[e.RowIndex ].Cells[7].Text;
        hfPrincipalID.Value = GrdOrder.Rows[e.RowIndex].Cells[1].Text;
        hfCustomerID.Value = GrdOrder.Rows[e.RowIndex].Cells[0].Text;
        hfSaleInvoiceID.Value = GrdOrder.Rows[e.RowIndex].Cells[10].Text;

        var ledgerCtl = new LedgerController();
        if (ledgerCtl.DeleteOpeningCredit(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfPrincipalID.Value), Convert.ToInt32(hfLegendID.Value), Convert.ToDateTime(txtFromdate.Text), Convert.ToInt32(hfCustomerID.Value), Convert.ToInt64(hfSaleInvoiceID.Value), Convert.ToInt32(Session["UserId"])))
        {
            ClearAll();
            LoadOpeningCredit();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured. Opening Credit not deleted.');", true);
        }

    }

    #endregion

    #region Click OPerations

    protected void btnSave_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        var ledgerCtl = new LedgerController();
        var cdc = new CustomerDataController();
        var dc = new DataControl();
        if (IsDayClosed())
        {
            var userCtl = new UserController();

            userCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
        else
        {

            if (btnSave.Text == "Update")
            {
                ledgerCtl.DeleteOpeningCredit(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfPrincipalID.Value), Convert.ToInt32(hfLegendID.Value), Convert.ToDateTime(txtFromdate.Text), Convert.ToInt32(hfCustomerID.Value), Convert.ToInt64(hfSaleInvoiceID.Value), Convert.ToInt32(Session["UserId"]));
            }

            if (!IsBillBookNoExist(1))
            {
                if (hfCustomerID.Value == "")
                {
                    var dtCustomer = (DataTable) Session["dt"];

                    DataRow[] foundRows = dtCustomer.Select("CUSTOMER_CODE  = '" + txtOutletCode.Text.Trim() + "'");
                    if (foundRows.Length > 0)
                    {
                        DataTable dt =
                            cdc.SelectCustomerCreditBalance(long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()),
                                int.Parse(DrpPrincipal.SelectedValue), int.Parse(drpDistributor.SelectedValue),
                                Constants.Credit_Order_Id);
                        decimal netAmount = decimal.Parse(dc.chkNull_0(txtAmount.Text));

                        if (decimal.Parse(dc.chkNull_0(dt.Rows[0][0].ToString())) <= netAmount)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                                "alert('Customer Credit Limit/Advance Amount is " +
                                dc.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                            return;
                        }
                    }
               
                long invoiceId = ledgerCtl.OpeningCredit(int.Parse(drpDistributor.SelectedValue),
                        txtChequeNo.Text.ToUpper(), int.Parse(foundRows[0]["TOWN_ID"].ToString()),
                        int.Parse(DrpRoute.SelectedValue),
                        Convert.ToDateTime(txtFromdate.Text), int.Parse(DrpPrincipal.SelectedValue),
                        long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()),
                        long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()),
                        -1, int.Parse(DrpDeliveryMan.SelectedValue), decimal.Parse(txtAmount.Text),
                        int.Parse(Session["UserId"].ToString()), int.Parse(DrpCreditType.SelectedValue.ToString()));
                    if (invoiceId > 0)
                    {
                        if (DrpCreditType.SelectedValue == Constants.CashSales.ToString())
                        {
                            InsertGl(Convert.ToInt32(foundRows[0]["CHANNEL_TYPE_ID"]), invoiceId);
                        }
                        ClearAll();
                        LoadOpeningCredit();
                    }
                }
                 
                else
                {
                    DataTable dt =
                        cdc.SelectCustomerCreditBalance(long.Parse(hfCustomerID.Value),
                            int.Parse(DrpPrincipal.SelectedValue), int.Parse(drpDistributor.SelectedValue),
                            Constants.Credit_Order_Id);
                    decimal netAmount = decimal.Parse(dc.chkNull_0(txtAmount.Text));

                    if (decimal.Parse(dc.chkNull_0(dt.Rows[0][0].ToString())) <= netAmount)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                            "alert('Customer Credit Limit/Advance Amount is " +
                            dc.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                        return;
                    }
                    long invoiceId = ledgerCtl.OpeningCredit(int.Parse(drpDistributor.SelectedValue),
                       txtChequeNo.Text.ToUpper(), int.Parse(hfTownId.Value),
                       int.Parse(DrpRoute.SelectedValue),
                       DateTime.Parse(txtFromdate.Text), int.Parse(DrpPrincipal.SelectedValue),
                       long.Parse(hfCustomerID.Value),
                       long.Parse(hfCustomerID.Value),
                       -1, int.Parse(DrpDeliveryMan.SelectedValue), decimal.Parse(txtAmount.Text),
                       int.Parse(Session["UserId"].ToString()), int.Parse(DrpCreditType.SelectedValue.ToString()));
                    if (invoiceId > 0)
                    {
                        if (DrpCreditType.SelectedValue == Constants.CashSales.ToString())
                        {
                            InsertGl(Convert.ToInt32(hfChannelId.Value), invoiceId);
                        }
                        ClearAll();
                        LoadOpeningCredit();
                    }
                }
                txtOutletCode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('This Invoice No already exist,Kindly enter different Invoice No');", true);
            }
        }
       
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    #endregion

    private void InsertGl(int pChannelTypeId, long invoiceId)
    {
        var custCtl = new CustomerDataController();
        DataTable dtChannel = custCtl.GetChannelAccountDetail(Constants.IntNullValue, pChannelTypeId);

        if (dtChannel.Rows.Count > 0)
        {
            var dtVoucher = new DataTable();
            dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
            dtVoucher.Columns.Add("Debit", typeof(decimal));
            dtVoucher.Columns.Add("Credit", typeof(decimal));
            dtVoucher.Columns.Add("Remarks", typeof(string));
            dtVoucher.Columns.Add("Principal_Id", typeof(string));

            DataRow drChannel = dtVoucher.NewRow();
            drChannel["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
            drChannel["REMARKS"] = "Channel Credit";
            drChannel["DEBIT"] = decimal.Parse(txtAmount.Text);
            drChannel["CREDIT"] = 0;
            drChannel["Principal_Id"] = Convert.ToInt32(DrpPrincipal.SelectedValue);
            dtVoucher.Rows.Add(drChannel);

            if (dtVoucher.Rows.Count > 0)
            {
                var lController = new LedgerController();
                string maxDocumentId = lController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), DateTime.Parse(txtFromdate.Text));

                lController.Add_Voucher2Opening(Convert.ToInt32(drpDistributor.SelectedValue), 0, maxDocumentId, Constants.Journal_Voucher, DateTime.Parse(txtFromdate.Text), Constants.CashPayment, "N/A", "Default Opening Credit Voucher" + "," + DrpDeliveryMan.SelectedItem.Text + "," + txtOutletName.Text + "," + txtChequeNo.Text
                , Constants.DateNullValue, null, invoiceId, Constants.CashSales, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue);
            }
        }
    }

    private bool IsDayClosed()
    {
        bool flag;
        DistributorController distrCtl = new DistributorController();
        DataTable dtDayClose = distrCtl.MaxDayClose(Convert.ToInt32(drpDistributor.SelectedValue), 3);
        if (Convert.ToDateTime(Session["CurrentWorkDate"]) == Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]))
        {
            flag = false;
        }
        else
        {
            flag = true;
        }

        return flag;
    }

    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtOutletCode.Text = "";
        txtAmount.Text = "";
        txtOutletName.Text = "";
        txtRemarks.Text = "";
        btnSave.Text = "Save";
        txtOutletCode.Focus();
        hfCustomerID.Value = "";
    }

    protected void GrdOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        DrpCreditType.SelectedValue = GrdOrder.Rows[NewEditIndex].Cells[7].Text;
        DrpPrincipal.SelectedValue = GrdOrder.Rows[NewEditIndex].Cells[1].Text;
        txtFromdate.Text = Convert.ToDateTime(GrdOrder.Rows[NewEditIndex].Cells[9].Text).ToString("dd-MMM-yyyy");
        txtOutletCode.Text = GrdOrder.Rows[NewEditIndex].Cells[8].Text;
        txtOutletName.Text = GrdOrder.Rows[NewEditIndex].Cells[3].Text;
        txtChequeNo.Text = GrdOrder.Rows[NewEditIndex].Cells[5].Text;
        txtAmount.Text = GrdOrder.Rows[NewEditIndex].Cells[6].Text;
        btnSave.Text = "Update";
        hfLegendID.Value = GrdOrder.Rows[NewEditIndex].Cells[7].Text;
        hfPrincipalID.Value = GrdOrder.Rows[NewEditIndex].Cells[1].Text;
        hfCustomerID.Value = GrdOrder.Rows[NewEditIndex].Cells[0].Text;
        hfSaleInvoiceID.Value = GrdOrder.Rows[NewEditIndex].Cells[10].Text;
        hfTownId.Value = GrdOrder.Rows[NewEditIndex].Cells[11].Text;
        hfChannelId.Value = GrdOrder.Rows[NewEditIndex].Cells[12].Text;
        DrpRoute.SelectedValue = GrdOrder.Rows[NewEditIndex].Cells[14].Text;
        LoadData();
        LoadDeliveryman();
        DrpDeliveryMan.SelectedValue = GrdOrder.Rows[NewEditIndex].Cells[13].Text;
    }
}