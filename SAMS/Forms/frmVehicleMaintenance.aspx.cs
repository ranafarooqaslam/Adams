using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;


/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmVehicleMaintenance : System.Web.UI.Page
{
    private static int _rowNo;
    readonly PurchaseController _mController = new PurchaseController();
    private static long _vehicleMaintenanceId;
    bool _mResult;

    readonly DataControl _dc = new DataControl();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSaveDocument.Attributes.Add("onclick", "return ValidateForm();");
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            DateTime pOrderDate = DateTime.Parse(this.Session["CurrentWorkDate"].ToString());
            txtToDate.Text = pOrderDate.ToString("dd-MMM-yyyy");
            LoadDistributor();
            LoadPrincipal();
            LoadAccountDetail();
            LoadVehicleNo();
            LoadDeliveryman();
            LoadGridView();

        
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
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate);
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }
       
    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.drpCreditTo, dtHead, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.drpCreditFrom, dtHead, 0, 11, true);
    }
   
    private void LoadDeliveryman()
    {
        if(DrpVehicleNo .Items .Count >0 && this.Session ["CompanyId"] !="")
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedAreaVehicle(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()),long.Parse (DrpVehicleNo .SelectedValue .ToString ()));
            clsWebFormUtil.FillDropDownList(this.DrpSalesPerson, m_dt, 0, 3, true);
            clsWebFormUtil.FillDropDownList(this.DrpDriver, m_dt, 5, 6, true);
            clsWebFormUtil.FillDropDownList(this.DrpLoader, m_dt, 7, 8, true);

        }
        else
        {
            DrpSalesPerson.Items.Clear();
            DrpDriver.Items.Clear();
            DrpLoader.Items.Clear();
        }
      
    }

    private void LoadVehicleNo()
    {
        var dcCtrl = new DistributorController();
        DataTable vDt = dcCtrl.SelectVehicleNO(Convert.ToInt32(drpDistributor.SelectedValue),4);
        clsWebFormUtil.FillDropDownList(DrpVehicleNo, vDt, 0, 1, true);
    }
    
    private void LoadGridView()
    {
        DateTime date = Configuration.SystemCurrentDateTime;
       // var shortDate = date.ToString("yyyy-MM-dd");
        var pc = new PurchaseController();
        GrdMaintenance.DataSource = null;
        GrdMaintenance.DataBind();

        DataTable vDt = pc.SelectVehicleMaintenance(int.Parse (drpDistributor .SelectedValue ),int.Parse (DrpPrincipal .SelectedValue ), int.Parse(DrpFuelType.SelectedValue), Constants.LongNullValue, DateTime.Parse(txtToDate.Text),Constants.LongNullValue);
        GrdMaintenance.DataSource = vDt;
        GrdMaintenance.DataBind();


    }

    protected void btnSaveDocument_Click(object sender, EventArgs e)
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
        if (btnSaveDocument.Text == "Save")
        {
            long vehicleMaintenanceId = _mController.InsertVehicleMaintenance(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), int.Parse(DrpFuelType.SelectedValue), decimal.Parse(txtAmount.Text), DateTime.Parse(txtToDate.Text), int.Parse(this.Session["UserId"].ToString()), int.Parse(drpCreditTo.SelectedValue.ToString()), int.Parse(drpCreditFrom.SelectedValue.ToString()), long.Parse(DrpVehicleNo.SelectedValue.ToString()), long.Parse(_dc.chkNull(DrpSalesPerson.SelectedValue.ToString())), FuelReading, txtRemarks.Text, int.Parse(_dc.chkNull(DrpDriver.SelectedValue)), int.Parse(_dc.chkNull(DrpLoader.SelectedValue)));
            if (vehicleMaintenanceId != Constants.LongNullValue)
            {
                InsertGl(vehicleMaintenanceId, int.Parse(DrpFuelType.SelectedValue));
                ClearAll();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error');", true);
            }
        }
        else if (btnSaveDocument.Text == "Update")
        {
            _mResult = _mController.UpdateVehicleMaintenance(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), _vehicleMaintenanceId, int.Parse(DrpFuelType.SelectedValue), decimal.Parse(txtAmount.Text), DateTime.Parse(txtToDate.Text), int.Parse(this.Session["UserId"].ToString()), int.Parse(drpCreditTo.SelectedValue.ToString()), int.Parse(drpCreditFrom.SelectedValue.ToString()), long.Parse(DrpVehicleNo.SelectedValue.ToString()), long.Parse(_dc.chkNull(DrpSalesPerson.SelectedValue.ToString())), FuelReading, txtRemarks.Text, false, int.Parse(_dc.chkNull(DrpDriver.SelectedValue)), int.Parse(_dc.chkNull(DrpLoader.SelectedValue)));
            if (_mResult)
            {
                UpdateGl(_vehicleMaintenanceId, int.Parse(DrpFuelType.SelectedValue));
                ClearAll();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error');", true);
            }
        }
        this.LoadGridView();
    }
    
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        this.ClearAll();
    }
    
    private void ClearAll()
    {

        txtAmount.Text = "";
        txtVehicleReading.Text = "";
        txtRemarks.Text = "";
        btnSaveDocument.Text = "Save";
        this.LoadGridView();

    }
    
    protected void DrpSalesPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    
    protected void DrpFuelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGridView();
    }
    
    protected void DrpVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDeliveryman();
    }
    
    protected void GrdMaintenance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        _vehicleMaintenanceId = long.Parse(GrdMaintenance.Rows[e.RowIndex].Cells[0].Text);
        
            _mResult = _mController.UpdateVehicleMaintenance(Constants .IntNullValue ,Constants .IntNullValue, _vehicleMaintenanceId,Constants.IntNullValue ,  Constants.DecimalNullValue  ,Constants .DateNullValue ,Constants .IntNullValue ,Constants .IntNullValue , Constants .IntNullValue ,Constants .LongNullValue, Constants .LongNullValue ,"","", true,int.Parse (_dc.chkNull (DrpDriver .SelectedValue )), int.Parse (_dc.chkNull (DrpLoader .SelectedValue )));
            if (_mResult)
            {
                DateTime date = Configuration.SystemCurrentDateTime;
                var shortDate = date.ToString("yyyy-MM-dd");
                string MaxDocumentId = "";
                var LController = new LedgerController();
                DataTable dt = LController.SelectVoucherNo(DateTime.Parse(shortDate), int.Parse(drpDistributor.SelectedValue), Constants.Journal_Voucher, int.Parse(this.Session["UserId"].ToString()), null, _vehicleMaintenanceId, Constants.Document_Vehicle_Maintenance);

                foreach (DataRow drs in dt.Rows)
                {

                    MaxDocumentId = (drs["VOUCHER_NO"].ToString());

                }

                bool isInsert = LController.Update_Fuel_VoucherLedger(int.Parse(drpDistributor.SelectedValue), MaxDocumentId, Constants.Journal_Voucher);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Record Deleted');", true);
                ClearAll();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error');", true);
            }
            this.LoadGridView();
    }
    
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGridView();
        LoadVehicleNo();
    }
    
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGridView();
    }
    
    protected void GrdMaintenance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "GetMedName('" + e.Row.Cells[0].Text + "');");
        }
    }
    
    private void InsertGl(long pVehicleMaintenanceId, int Fuel_Type_ID)
    {

        LedgerController lController = new LedgerController();
        string maxDocumentId = lController.SelectMaxVoucherId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue), Convert.ToDateTime(txtToDate.Text));

      bool isInsert=  lController.Add_Fuel_Voucer(int.Parse(drpDistributor.SelectedValue), 1, maxDocumentId, 
          Constants.Journal_Voucher, Convert.ToDateTime(txtToDate .Text), Constants.CashPayment,
          "N/A", "Vehicle Maintenance Voucher, " + Server.HtmlDecode(txtRemarks.Text),
          Constants.DateNullValue, null, pVehicleMaintenanceId, Constants.Document_Vehicle_Maintenance, null, 
          Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue, 
          long.Parse(drpCreditFrom.SelectedValue), long.Parse(drpCreditTo.SelectedValue),
          decimal.Parse(txtAmount.Text), decimal.Parse(txtAmount.Text));
      
        if (isInsert)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Record Updated');", true);
                           
          PrintVoucher(int.Parse(drpDistributor.SelectedValue), maxDocumentId, Constants.Journal_Voucher);
      }
    }
    
    private void UpdateGl(long pVehicleMaintenanceId, int Fuel_Type_ID)
    {
        DateTime date = SAMSCommon.Classes.Configuration.SystemCurrentDateTime;
        var shortDate = date.ToString("yyyy-MM-dd");
        string MaxDocumentId = "";
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherNo(DateTime.Parse(txtToDate.Text), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.Journal_Voucher, int.Parse(this.Session["UserId"].ToString()), null, pVehicleMaintenanceId , Constants.Document_Vehicle_Maintenance);
        foreach (DataRow drs in dt.Rows)
        {
            MaxDocumentId = (drs["VOUCHER_NO"].ToString());
        }

        bool isInsert = LController.Update_Fuel_Voucher(int.Parse(drpDistributor.SelectedValue), 1, MaxDocumentId, 
            Constants.Journal_Voucher, Convert.ToDateTime(txtToDate.Text), Constants.CashPayment,
            "N/A", "Vehicle Maintenance Voucher, " + Server.HtmlDecode(txtRemarks.Text), Constants.DateNullValue, null, pVehicleMaintenanceId,
            Constants.Document_Vehicle_Maintenance, null, Convert.ToInt32(Session["UserID"]), null, 
            Constants.DateNullValue, long.Parse(drpCreditFrom.SelectedValue), 
            long.Parse(drpCreditTo.SelectedValue), decimal.Parse(txtAmount.Text),
            decimal.Parse(txtAmount.Text));

        if (isInsert)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Record Updated');", true);
            PrintVoucher(int.Parse(drpDistributor.SelectedValue), MaxDocumentId, Constants.Journal_Voucher);
        }
    }

    private void PrintVoucher(int pDistribiutorId, string pVoucherNo,int pVoucherId )
    {

        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(pDistribiutorId, pVoucherNo, pVoucherId);
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

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        LoadGridView();
    }

    protected void GrdMaintenance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        _rowNo = NewEditIndex;
        _vehicleMaintenanceId = long.Parse(GrdMaintenance.Rows[NewEditIndex].Cells[0].Text);
        DrpVehicleNo.SelectedValue = GrdMaintenance.Rows[NewEditIndex].Cells[2].Text;
        if (GrdMaintenance.Rows[NewEditIndex].Cells[6].Text == "&nbsp;")
        {
            txtVehicleReading.Text = "0";
        }
        else
        {
            txtVehicleReading.Text = GrdMaintenance.Rows[NewEditIndex].Cells[6].Text;
        }
        if (GrdMaintenance.Rows[NewEditIndex].Cells[8].Text == "&nbsp;")
        {
            txtRemarks.Text = "";

        }
        else
        {
            txtRemarks.Text = GrdMaintenance.Rows[NewEditIndex].Cells[8].Text;
        }
        txtAmount.Text = Math.Round(decimal.Parse(GrdMaintenance.Rows[NewEditIndex].Cells[7].Text), 2).ToString();

        drpCreditTo.SelectedValue = GrdMaintenance.Rows[NewEditIndex].Cells[9].Text;
        drpCreditFrom.SelectedValue = GrdMaintenance.Rows[NewEditIndex].Cells[10].Text;
        drpDistributor.SelectedValue = GrdMaintenance.Rows[NewEditIndex].Cells[11].Text;
        DrpPrincipal.SelectedValue = GrdMaintenance.Rows[NewEditIndex].Cells[12].Text;
        txtAmount.Focus();
        btnSaveDocument.Text = "Update";
        DrpVehicleNo_SelectedIndexChanged(null, null);
    }
}