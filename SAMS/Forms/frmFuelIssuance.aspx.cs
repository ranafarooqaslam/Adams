using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmFuelIssuance : System.Web.UI.Page
{
    private static int RowNo;
    PurchaseController mController = new PurchaseController();
    private static long FUEL_ISSUANCE_Id;
    private static decimal Stock;
    PurchaseController PC = new PurchaseController();
    DataControl dc = new DataControl();
    readonly DocumentPrintController _dPrint = new DocumentPrintController();
    readonly RptAccountController _rptAccountCtl = new RptAccountController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtToDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            LoadDistributor();
            LoadPrincipal();
            LoadAccountDetail();
            LoadVehicleNO();
            LoadDeliveryman();
            SelectFuelPurchaseDetail();
            LoadGridView();
           btnSaveDocument.Attributes.Add("onclick", "return ValidateForm();");
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }
    
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
    
    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.drpCreditTo, dtHead, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.drpCreditFrom, dtHead, 0, 11, true);
    }
    
    private void LoadVehicleNO()
    {
        DistributorController DC_ctrl = new DistributorController();


        DataTable v_dt = DC_ctrl.SelectVehicleNO(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(DrpVehicleNo, v_dt, 0, 1, true);


    }
    
    private void LoadDeliveryman()
    {
        if(DrpVehicleNo .Items .Count >0 && this.Session ["CompanyId"] !="")
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedAreaVehicle(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()),long.Parse (DrpVehicleNo .SelectedValue .ToString ()));
            clsWebFormUtil.FillDropDownList(this.DrpSalesPerson, m_dt, 0, 3, true);
        }
        else
        {
            DrpSalesPerson .Items .Clear ();
        }
      
    }
    
    private void SelectFuelPurchaseDetail()
    {
       
        Grdfuel.DataSource = null;
        Grdfuel.DataBind();
        if (DrpFuelType.Items.Count > 0 && this.Session["UserID"]!="") 
        {
            DataTable dtCredit = PC.SelectFuelPurchaseDetail(int.Parse (drpDistributor .SelectedValue),int.Parse (DrpPrincipal .SelectedValue), int.Parse(DrpFuelType.SelectedValue.ToString()), int.Parse(this.Session["UserID"].ToString()), Constants.DateNullValue, Constants.LongNullValue);
            Grdfuel.DataSource = dtCredit;
            Grdfuel.DataBind();
        }
    }
    
    private void LoadGridView()
    {
        DateTime date = SAMSCommon.Classes.Configuration.SystemCurrentDateTime;
        var shortDate = date.ToString("yyyy-MM-dd");
        PurchaseController Pc = new PurchaseController();
        GrdFuelIssuance.DataSource = null;
        GrdFuelIssuance.DataBind();

        DataTable v_dt = Pc.Selectfuelissuance(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), int.Parse(DrpFuelType.SelectedValue.ToString()), Constants.LongNullValue, DateTime.Parse(txtToDate.Text), Constants.LongNullValue);
        GrdFuelIssuance.DataSource = v_dt;
        GrdFuelIssuance.DataBind();


    }

    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        bool mResult;
        decimal TotalQty = decimal.Parse(txtQty.Text);
        if (TotalQty <= Stock)
        {
            string FuelReading = "0";
            try
            {
                Convert.ToDecimal(txtVehicleReading.Text);
            }
            catch (Exception ex)
            {
                FuelReading = "0";
            }
            if (txtVehicleReading.Text.Length > 0)
            {
                FuelReading = txtVehicleReading.Text;
            }

            DataTable dtMaxVehicleReading = mController.GetMaxVehicleReading(Convert.ToInt32(DrpVehicleNo.SelectedValue), Convert.ToDateTime(txtToDate.Text), 0);
            if (dtMaxVehicleReading.Rows.Count > 0)
            {
                decimal MaxReading = Convert.ToDecimal(dtMaxVehicleReading.Rows[0][0]);
                if (Convert.ToDecimal(FuelReading) < MaxReading)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Vehicle Reading can not be less than last vehicle reading (" + MaxReading.ToString() + ")');", true);
                    return;
                }
            }

            if (btnSaveDocument.Text == "Save")
            {
                long fuel_issuance_Id = mController.InsertFuelIssuanceDocument(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), int.Parse(DrpFuelType.SelectedValue), long.Parse(DrpVehicleNo.SelectedValue), long.Parse(DrpSalesPerson.SelectedValue), FuelReading, decimal.Parse(txtRate.Text), decimal.Parse(txtQty.Text), decimal.Parse(txtQty.Text) * decimal.Parse(txtRate.Text), DateTime.Parse(txtToDate.Text), int.Parse(this.Session["UserId"].ToString()), int.Parse(drpCreditTo.SelectedValue.ToString()), int.Parse(drpCreditFrom.SelectedValue.ToString()));
                if (fuel_issuance_Id != Constants.LongNullValue)
                {
                    string VoucherNo = InsertGL(fuel_issuance_Id, int.Parse(DrpFuelType.SelectedValue));
                    if (VoucherNo != null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Record Updated');", true);
                        PrintReport(VoucherNo);
                        this.ClearAll();
                        SelectFuelPurchaseDetail();
                        this.LoadGridView();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error');", true);
                }
            }
            else if (btnSaveDocument.Text == "Update")
            {
                mResult = mController.UpdateFuelIssuanceDocument(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), FUEL_ISSUANCE_Id, int.Parse(DrpFuelType.SelectedValue), long.Parse(DrpVehicleNo.SelectedValue), long.Parse(DrpSalesPerson.SelectedValue), FuelReading, decimal.Parse(txtRate.Text), decimal.Parse(txtQty.Text), decimal.Parse(txtQty.Text) * decimal.Parse(txtRate.Text), DateTime.Parse(txtToDate.Text), int.Parse(this.Session["UserId"].ToString()), int.Parse(drpCreditTo.SelectedValue.ToString()), int.Parse(drpCreditFrom.SelectedValue.ToString()), false);
                if (mResult)
                {
                    string voucherNo = UpdateGL(FUEL_ISSUANCE_Id, int.Parse(DrpFuelType.SelectedValue));
                    if (voucherNo != null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Record Updated');", true);
                        PrintReport(voucherNo);
                        ClearAll();
                        SelectFuelPurchaseDetail();
                        this.LoadGridView();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Stock Limit is =" + Stock + " ' );", true);
            txtQty.Focus();
        }
    }
    
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        this.ClearAll();
    }
    
    private void ClearAll()
    {

        txtQty.Text = "";
        txtRate.Text = "";
        txtAmount.Text = "";
        txtVehicleReading.Text = "";
        btnSaveDocument.Text = "Save";
        this.LoadGridView();

    }
    
    protected void DrpSalesPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
     
    }
    
    protected void chbStockRate_CheckedChanged(object sender, EventArgs e)
    {
        txtRate.Text = "";
        ////
        //Clear the existing selected row 
        foreach (GridViewRow oldrow in Grdfuel.Rows)
        {
            ((RadioButton)oldrow.FindControl("chbStockRate")).Checked = false;
        }

        //Set the new selected row
        RadioButton rb = (RadioButton)sender;
        GridViewRow row1 = (GridViewRow)rb.NamingContainer;
        ((RadioButton)row1.FindControl("chbStockRate")).Checked = true;

        /////



        GridViewRow row = ((GridViewRow)((RadioButton)sender).NamingContainer);
        int index = row.RowIndex;
        RadioButton cb1 = (RadioButton)Grdfuel.Rows[index].FindControl("chbStockRate");
        string checkboxstatus = "";
        if (cb1.Checked == true)
            checkboxstatus = "YES";
        else if (cb1.Checked == false)
            checkboxstatus = "NO";
        if (checkboxstatus == "YES")
        {
            txtRate.Text = Grdfuel.Rows[index].Cells[1].Text;
            Stock = decimal.Parse(Grdfuel.Rows[index].Cells[2].Text);
            
            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "CalculateAmount(event);", true);
        }
        //Here Write the code to connect to your database and update the status by 
        //sending the checkboxstatus as variable and update in the database.
    }
    
    protected void DrpFuelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectFuelPurchaseDetail();
        LoadGridView();
    }
    
    protected void DrpVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDeliveryman();

    }
    
    protected void Grdfuel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GrdFuelIssuance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bool mResult;
        FUEL_ISSUANCE_Id = long.Parse(GrdFuelIssuance.Rows[e.RowIndex].Cells[0].Text);

        mResult = mController.UpdateFuelIssuanceDocument(Constants .IntNullValue ,Constants .IntNullValue , FUEL_ISSUANCE_Id, Constants.IntNullValue, Constants.LongNullValue, Constants.LongNullValue,null, Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.DecimalNullValue , Constants.DateNullValue,Constants .IntNullValue ,Constants .IntNullValue ,Constants .IntNullValue ,true );
        if (mResult)
        {
            DateTime date = Configuration.SystemCurrentDateTime;
            var shortDate = date.ToString("yyyy-MM-dd");
            string MaxDocumentId = "";
            var LController = new LedgerController();
            DataTable dt = LController.SelectVoucherNo(DateTime.Parse(shortDate), int.Parse(drpDistributor.SelectedValue), Constants.Journal_Voucher, int.Parse(this.Session["UserId"].ToString()), null, FUEL_ISSUANCE_Id, Constants.Document_Fuel_Issuance);

            foreach (DataRow drs in dt.Rows)
            {

                MaxDocumentId = (drs["VOUCHER_NO"].ToString());

            }

            bool isInsert = LController.Update_Fuel_VoucherLedger(int.Parse(drpDistributor.SelectedValue), MaxDocumentId, Constants.Journal_Voucher);


            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Record Deleted');", true);
            ClearAll();
            SelectFuelPurchaseDetail();
            this.LoadGridView();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error');", true);
        }
        
    }
    
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectFuelPurchaseDetail();
        LoadGridView();
    }
    
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        SelectFuelPurchaseDetail();
        LoadGridView();
    }
    
    private string InsertGL(long p_Fuel_Issuance_Id, int Fuel_Type_ID)
    {
        string MaxDocumentId = null;
        LedgerController LController = new LedgerController();
         MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue),DateTime .Parse(txtToDate .Text ));

         if (LController.Add_Fuel_Voucer(int.Parse(drpDistributor.SelectedValue), 1, MaxDocumentId, Constants.Journal_Voucher, DateTime.Parse(txtToDate.Text), Constants.CashPayment, "N/A", "Fuel Issuance Voucher", Constants.DateNullValue, null, p_Fuel_Issuance_Id, Constants.Document_Fuel_Issuance, null, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue, long.Parse(drpCreditFrom.SelectedValue), long.Parse(drpCreditTo.SelectedValue), decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text), decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text)))
        {
            return MaxDocumentId ;

        }
        else
        {
            MaxDocumentId=null;
            return MaxDocumentId;
        }
    }

    private string UpdateGL(long p_Fuel_Issuance_Id, int Fuel_Type_ID)
    {DateTime date = SAMSCommon.Classes.Configuration.SystemCurrentDateTime;
        var shortDate = date.ToString("yyyy-MM-dd");
        string MaxDocumentId = "";
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherNo(Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.Journal_Voucher, int.Parse(this.Session["UserId"].ToString()), null, p_Fuel_Issuance_Id, Constants.Document_Fuel_Issuance);
        foreach (DataRow drs in dt.Rows)
        {
            MaxDocumentId = (drs["VOUCHER_NO"].ToString());
        }
        if (LController.Update_Fuel_Voucher(int.Parse(drpDistributor.SelectedValue), 1, MaxDocumentId, Constants.Journal_Voucher, DateTime.Parse(txtToDate.Text), Constants.CashPayment, "N/A", "Fuel Issuance Voucher", Constants.DateNullValue, null, p_Fuel_Issuance_Id, Constants.Document_Fuel_Issuance, null, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue, long.Parse(drpCreditFrom.SelectedValue), long.Parse(drpCreditTo.SelectedValue), decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text), decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text)))
        {
            return MaxDocumentId;
        }
        else
        {
            MaxDocumentId = null;
            return MaxDocumentId;
        }
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
        // Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        ClearAll();
        LoadGridView();
    }

    protected void GrdFuelIssuance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        decimal IssuedQty = 0;
        this.ClearAll();
        RowNo = NewEditIndex;
        FUEL_ISSUANCE_Id = long.Parse(GrdFuelIssuance.Rows[NewEditIndex].Cells[0].Text);
        DrpFuelType.SelectedValue = GrdFuelIssuance.Rows[NewEditIndex].Cells[1].Text;
        DrpVehicleNo.SelectedValue = GrdFuelIssuance.Rows[NewEditIndex].Cells[2].Text;
        if (GrdFuelIssuance.Rows[NewEditIndex].Cells[6].Text == "&nbsp;")
        {
            txtVehicleReading.Text = "0";
        }
        else
        {
            txtVehicleReading.Text = GrdFuelIssuance.Rows[NewEditIndex].Cells[6].Text;
        }
        txtQty.Text = Math.Round(decimal.Parse(GrdFuelIssuance.Rows[NewEditIndex].Cells[7].Text), 2).ToString();
        IssuedQty = Math.Round(decimal.Parse(GrdFuelIssuance.Rows[NewEditIndex].Cells[7].Text), 2);
        txtRate.Text = Math.Round(decimal.Parse(GrdFuelIssuance.Rows[NewEditIndex].Cells[8].Text), 2).ToString();
        txtAmount.Text = Math.Round(decimal.Parse(GrdFuelIssuance.Rows[NewEditIndex].Cells[9].Text), 2).ToString();
        drpCreditTo.SelectedValue = GrdFuelIssuance.Rows[NewEditIndex].Cells[10].Text;
        drpCreditFrom.SelectedValue = GrdFuelIssuance.Rows[NewEditIndex].Cells[11].Text;
        drpDistributor.SelectedValue = GrdFuelIssuance.Rows[NewEditIndex].Cells[12].Text;
        DrpPrincipal.SelectedValue = GrdFuelIssuance.Rows[NewEditIndex].Cells[13].Text;
        txtQty.Focus();
        btnSaveDocument.Text = "Update";
        DrpVehicleNo_SelectedIndexChanged(null, null);
        foreach (GridViewRow oldrow in Grdfuel.Rows)
        {
            ((RadioButton)oldrow.FindControl("chbStockRate")).Checked = false;
            ((RadioButton)oldrow.FindControl("chbStockRate")).Enabled = false;
        }
        DataTable dtCredit = PC.SelectFuelPurchaseDetail(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), int.Parse(DrpFuelType.SelectedValue.ToString()), int.Parse(this.Session["UserID"].ToString()), Constants.DateNullValue, Constants.LongNullValue);
        DataRow[] drs = dtCredit.Select("Price='" + decimal.Parse(txtRate.Text) + "'");
        if (drs.Length > 0)
        {
            Stock = decimal.Parse(drs[0]["Quantity"].ToString()) + IssuedQty;
        }
        else
        {
            Stock = IssuedQty;
        }
    }
}