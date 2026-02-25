using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;


public partial class Forms_frmFuelPurchase : System.Web.UI.Page
{

    PurchaseController mController = new PurchaseController();
    bool mResult;
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptAccountController _rptAccountCtl = new RptAccountController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
           SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
           txtToDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
           this.GetDocumentNo();
           LoadAccountDetail();
           btnSaveDocument.Attributes.Add("onclick", "return ValidateForm();");
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        int type = 0;
        type = int.Parse(DrpFuelType.SelectedValue.ToString());

        DataTable dt = mPurchase.SelectFuelPurchaseDocumentNo(int.Parse (drpDistributor .SelectedValue ),1,type, long.Parse(this.Session["UserId"].ToString()), DateTime.Parse(txtToDate.Text), Constants.LongNullValue);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
            }
        }
    }
    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.drpCreditTo, dtHead, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.drpCreditFrom, dtHead, 0, 11, true);
    }
    private void LoadDocumentDetail()
    {
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectFuelPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, Constants.DateNullValue, int.Parse(drpDocumentNo.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            drpDocumentNo.SelectedValue = dt.Rows[0]["FUEL_PURCHASE_ID"].ToString();
            DrpFuelType.SelectedValue = dt.Rows[0]["FUEL_TYPE"].ToString();
            drpCreditFrom.SelectedValue = dt.Rows[0]["CREDIT_FROM"].ToString();
            drpCreditTo.SelectedValue = dt.Rows[0]["CREDIT_TO"].ToString();
            txtQty.Text = Math.Round(decimal.Parse(dt.Rows[0]["QUANTITY"].ToString()), 2).ToString();
            txtRate.Text = Math.Round(decimal.Parse(dt.Rows[0]["PRICE"].ToString()), 2).ToString();
            txtAmount.Text = Math.Round(decimal.Parse(dt.Rows[0]["AMOUNT"].ToString()), 2).ToString();
            btnSaveDocument.Text = "Update";
            txtQty.Focus();
        }
    }

    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        if (btnSaveDocument.Text == "Save")
        {
            long Fuel_purchase_No = mController.InsertFuelPurchaseDocument(int.Parse(drpDistributor.SelectedValue), 1, int.Parse(DrpFuelType.SelectedValue), decimal.Parse(txtQty.Text), decimal.Parse(txtRate.Text), decimal.Parse(txtQty.Text) * decimal.Parse(txtRate.Text), DateTime.Parse(txtToDate.Text), int.Parse(this.Session["UserId"].ToString()), int.Parse(drpCreditTo.SelectedValue.ToString()), int.Parse(drpCreditFrom.SelectedValue.ToString()));
            if (Fuel_purchase_No != Constants.LongNullValue)
            {
                string VoucherNo = InsertGL(Fuel_purchase_No, int.Parse(DrpFuelType.SelectedValue));
                if (VoucherNo != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Record Updated');", true);
                    PrintReport(VoucherNo);
                    ClearAll();
                    drpDocumentNo.SelectedIndex = 0;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error');", true);
            }
        }
        else if (btnSaveDocument.Text == "Update")
        {
            mResult = mController.UpdateFuelPurchaseDocument(int.Parse(drpDistributor.SelectedValue), 1, long.Parse(drpDocumentNo.SelectedValue), int.Parse(DrpFuelType.SelectedValue), decimal.Parse(txtQty.Text), decimal.Parse(txtRate.Text), decimal.Parse(txtQty.Text) * decimal.Parse(txtRate.Text), DateTime.Parse(txtToDate.Text), int.Parse(this.Session["UserId"].ToString()), int.Parse(drpCreditTo.SelectedValue.ToString()), int.Parse(drpCreditFrom.SelectedValue.ToString()));
            if (mResult)
            {
                string Voucher_No = UpdateGL(long.Parse(drpDocumentNo.SelectedValue), int.Parse(DrpFuelType.SelectedValue));

                if (Voucher_No != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Record Updated');", true);
                    ClearAll();
                    drpDocumentNo.SelectedIndex = 0;
                    PrintReport(Voucher_No);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error');", true);
            }
        }
        GetDocumentNo();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    private void ClearAll()
    {

        txtQty.Text = "";
        txtRate.Text = "";
        txtAmount.Text = "";
        btnSaveDocument.Text = "Save";
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ClearAll();
        if (long.Parse(drpDocumentNo.SelectedValue) != Constants.LongNullValue)
        {
            this.LoadDocumentDetail();
        }
    }
    protected void DrpFuelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpDocumentNo.Items.Clear();
        ClearAll();
        GetDocumentNo();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        GetDocumentNo();
    }

    private string InsertGL(long p_Fuel_Purchase_Id, int Fuel_Type_ID)
    {
        string MaxDocumentId = null;
        LedgerController LController = new LedgerController();
         MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue), DateTime.Parse(txtToDate.Text));

        if (LController.Add_Fuel_Voucer(int.Parse(drpDistributor.SelectedValue), 1, MaxDocumentId, Constants.Journal_Voucher, DateTime.Parse(txtToDate.Text), Constants.CashPayment, "N/A", "Fuel Purchase Voucher", Constants.DateNullValue, null, p_Fuel_Purchase_Id, Constants.Document_Fuel_Purchase, null, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue, long.Parse(drpCreditFrom.SelectedValue), long.Parse(drpCreditTo.SelectedValue), decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text), decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text)))
        {
            return MaxDocumentId;
        }
        else
        {
            MaxDocumentId=null;
            return MaxDocumentId;
        }
    }
    private string UpdateGL(long p_Fuel_Purchase_Id, int Fuel_Type_ID)
    {
        string MaxDocumentId = "";
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherNo(Constants .DateNullValue , int.Parse(drpDistributor.SelectedValue.ToString()),Constants .Journal_Voucher, int.Parse(this.Session["UserId"].ToString()),null, p_Fuel_Purchase_Id,Constants.Document_Fuel_Purchase);       
        foreach (DataRow drs in dt.Rows)
        {

            MaxDocumentId = (drs["VOUCHER_NO"].ToString());
          
        }
        LController.Update_Fuel_Voucher(int.Parse(drpDistributor.SelectedValue), 1, MaxDocumentId, Constants.Journal_Voucher, DateTime.Parse (txtToDate .Text ), Constants.CashPayment, "N/A", "Fuel Purchase Voucher", Constants.DateNullValue, null, p_Fuel_Purchase_Id, Constants.Document_Fuel_Purchase, null, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue, long.Parse(drpCreditFrom.SelectedValue), long.Parse(drpCreditTo.SelectedValue), decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text), decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text));
        return MaxDocumentId;
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        ClearAll();
        GetDocumentNo();
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
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}