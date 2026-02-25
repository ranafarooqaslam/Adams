using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit Customer Claim
/// </summary>
public partial class Forms_frmDemangedClaim : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadClaimType();
            this.LoadAccountDetail();
            this.LoadPrincipal();
            this.CreatTable();
            this.LoadArea();
            this.LoadCustomer();
            this.LoadGrid();
            btnAddNew.Attributes.Add("onclick", "return ValidateForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            lblRowId.Text = "-1";
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }
    
    /// <summary>
    /// Loads Claim Types To RadioButtonList
    /// </summary>
    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Debit Claim", Constants.DebitClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Credit Claim", Constants.CreditClaim.ToString()));
        RbdClaimType.SelectedIndex = 0;         
    }
    
    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHeadAssignedLocation(Constants.AC_AccountHeadId, Constants.LongNullValue, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(this.DrpAccountHead, dtHead, 0, 11, true);
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
            int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }
    
    /// <summary>
    /// Creates Datatable For Customer Claim Data
    /// </summary>
    private void CreatTable()
    {
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();  

    }
    
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {
        DistributorAreaController mController = new DistributorAreaController();
        DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        DrpRoute.Enabled = true;        
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpPrincipal.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4, true);
            Session.Add("dtCustomer", dt);
        }
        else
        {
            DrpCustomer.Items.Clear();   
        }
    }
    
    /// <summary>
    /// Loads Customer Claims To Grid
    /// </summary>
    private void LoadGrid()
    {
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
            DataTable dt = LController.SelectClaimDetail(int.Parse(drpDistributor.SelectedValue.ToString()),int.Parse(RbdClaimType.SelectedValue.ToString()),
                CurrentWorkDate, CurrentWorkDate);
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }

    /// <summary>
    /// Loads Routes And Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadCustomer();
        this.LoadAccountDetail();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCustomer(); 
    }

    /// <summary>
    /// Deletes Customer Claim
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LController = new LedgerController();
        DataControl dc = new DataControl();

        LController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[10].Text)), long.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[9].Text)));
        this.LoadGrid();
        
    }

    /// <summary>
    /// Saves Customer Claim
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAddNew_Click(object sender, EventArgs e)
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
        long LedgerID = 0;
        string p_Voucher_NO;
        string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        {
            LedgerID = LController.InsertCustomerClaim(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
               CurrentWorkDate, txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.DebitClaim, "");
            p_Voucher_NO = InsertGL(LedgerID, Constants.DebitClaim);
        }
        else
        {
            LedgerID = LController.InsertCustomerClaim(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                CurrentWorkDate, txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                 null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.CreditClaim, "");
            p_Voucher_NO = InsertGL(LedgerID, Constants.CreditClaim);
        }
        txtAmount.Text = "";
        txtRemarks.Text = "";
        this.LoadGrid();
        PrintReport(p_Voucher_NO);
    }

    /// <summary>
    /// Loads Account Heads And Claim Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RbdClaimType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountDetail();
        this.LoadGrid();
    }

    private string InsertGL(long LedgerID, int TypeID)
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

        int ChannelTypeID = Constants.IntNullValue;
        DataTable dtCustomer = Session["dtCustomer"] as DataTable;
        DataRow[] foundrows = dtCustomer.Select("CUSTOMER_ID = '" + DrpCustomer.SelectedValue + "'");
        if (foundrows.Length > 0)
        {
            ChannelTypeID = Convert.ToInt32(foundrows[0]["CHANNEL_TYPE_ID"]);
        }
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        DataRow drClaim = dtVoucher.NewRow();
        drClaim["ACCOUNT_HEAD_ID"] = DrpAccountHead.SelectedValue;
        drClaim["REMARKS"] = txtRemarks.Text;
        if (TypeID == Constants.CreditClaim)
        {
            drClaim["CREDIT"] = 0;
            drClaim["DEBIT"] = Convert.ToDecimal(txtAmount.Text);
        }
        else
        {
            drClaim["DEBIT"] = 0;
            drClaim["CREDIT"] = Convert.ToDecimal(txtAmount.Text);
        }
        drClaim["Principal_Id"] = Convert.ToInt32(DrpPrincipal.SelectedValue);
        dtVoucher.Rows.Add(drClaim);

        CustomerDataController CustCtl = new CustomerDataController();
        DataTable dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, ChannelTypeID);
        if (dtChannel.Rows.Count > 0)
        {
            DataRow drClaim2 = dtVoucher.NewRow();
            drClaim2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"].ToString();
            drClaim2["REMARKS"] = txtRemarks.Text;
            if (TypeID == Constants.CreditClaim)
            {
                drClaim2["CREDIT"] = Convert.ToDecimal(txtAmount.Text);
                drClaim2["DEBIT"] = 0;
            }
            else
            {
                drClaim2["DEBIT"] = Convert.ToDecimal(txtAmount.Text);
                drClaim2["CREDIT"] = 0;
            }
            drClaim2["Principal_Id"] = Convert.ToInt32(DrpPrincipal.SelectedValue);
            dtVoucher.Rows.Add(drClaim2);
        }
        LedgerController LController = new LedgerController();
        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), CurrentWorkDate);
        bool isInsert = false;
        if (dtVoucher.Rows.Count > 0)
        {
           isInsert=  LController.Add_Voucher2(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, Constants.Journal_Voucher, CurrentWorkDate, Constants.CashPayment, "N/A", "Customer Claim Voucher", Constants.DateNullValue, null, LedgerID, TypeID, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue);
        }
        if (isInsert)
        {
            return MaxDocumentId;
        }
        else
        {
            return null;
        }
    }

    private void PrintReport(string p_Voucher_No)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), p_Voucher_No, Constants .Journal_Voucher );
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}