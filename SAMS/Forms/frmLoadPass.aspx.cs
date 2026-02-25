using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmLoadPass : System.Web.UI.Page
{
    DataTable PurchaseSKU;
    PhaysicalStockController mController = new PhaysicalStockController();
    private static int RowId;
    static long LOADPASS_ID;
    static int stock=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drpDocumentNo.Focus();
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadArea();
            this.LoadDeliveryman();
            this.LoadVehicleNO();
            this.LoadSKUDetail();
            this.CreatTable();            
            LoadPendingOrder();
            LoadCustomer();
            txtPriceGroup.Attributes.Add("Readonly", "Readonly");            
        }

    }
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All| ", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpRoute.SelectedValue != Constants.IntNullValue.ToString())
        {
            //CustomerDataController mController = new CustomerDataController();
            //DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue);
            //clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);


            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue), Constants.IntNullValue, Constants .IntNullValue);
           // clsWebFormUtil.FillListBox(this.ListCustomer, dtCustomer, "CUSTOMER_DETAIL2", "CUSTOMER_DETAIL2", true);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dtCustomer, "Customer_Id", "CUSTOMER_DETAIL3", false);
          //  this.Session.Add("dtCustomer", dtCustomer);
        }
    }
    /// <summary>
    /// combos load and selected index changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    #region Load
   
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
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
        txtToDate.Text = CurrentWorkDate.ToString("dd-MMM-yyyy");
    }
    
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
    
    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpDeliveryMan, m_dt, 0, 3, true);
        }
        else
        {
            DrpDeliveryMan.Items.Clear();
        }
    }
    
    private void LoadSalePerson()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            //Distributor_UserController mDController = new Distributor_UserController();
            //DataTable m_dt = mDController.SelectSaleForceAssignedArea(Constants.SALES_FORCE_SALESPERSON, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()));
            //clsWebFormUtil.FillDropDownList(this.DrpDeliveryMan, m_dt, 0, 3, true);
        }
        else
        {
            DrpDeliveryMan.Items.Clear();
        }
    }
    
    private void LoadPendingOrder()
    {
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        DataTable dtOrder = or.SelectPendingLoadPass_ByDate(Convert.ToDateTime(txtToDate.Text),Convert.ToInt32(Session["UserID"]));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtOrder, 0, 1);
        this.Session.Add("dtOrder", dtOrder);
    }
    
    private void ExistenOrderDetail(long OrderId)
    {
        OrderEntryController ord = new OrderEntryController();
        PurchaseSKU = ord.SelectLoadPassDetail(OrderId);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird(PurchaseSKU);

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
        if (txtPriceGroup.Text.Trim() == "Default")
        {
            DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(DrpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, CurrentWorkDate);
            clsWebFormUtil.FillDropDownList(this.ddlSKuCde, Dtsku_Price, 0, 10, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
        else if (txtPriceGroup.Text.Trim() != "")
        {
            ddlSKuCde.Items.Clear();

            DataTable Dtsku_Price = PController.GetCustomerSKU(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpCustomer.SelectedValue),CurrentWorkDate);
            clsWebFormUtil.FillDropDownList(ddlSKuCde, Dtsku_Price, "SKU_ID", "SkuPriceDetail3", true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
        else
        {
            DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(DrpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1,CurrentWorkDate);
            clsWebFormUtil.FillDropDownList(this.ddlSKuCde, Dtsku_Price, 0, 10, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
    }
    
    private void LoadVehicleNO()
    {
        if (DrpDeliveryMan.Items.Count > 0)
        {
            DistributorController DC_ctrl = new DistributorController();
            DataTable v_dt = DC_ctrl.SelectVehicleNO(int.Parse(DrpDeliveryMan.SelectedValue));
            clsWebFormUtil.FillDropDownList(DrpVehicleNo, v_dt, 0, 1, true);
            if (DrpVehicleNo.Items.Count > 0)
            {
                lblVehicleNo.Text = DrpVehicleNo.SelectedItem.Text;
            }
            else
            {
                lblVehicleNo.Text = "0";
            }
        }
        else
        {
            lblVehicleNo.Text = "0";
        }
    }
    
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
    }

    private void LoadGird(DataTable PurchaseSKU)
    {
        if (PurchaseSKU != null)
        {
            GrdPurchase.DataSource = PurchaseSKU;
            GrdPurchase.DataBind();
        }
        else
        {
            GrdPurchase.DataSource = null;
            GrdPurchase.DataBind();
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
                    DataRow[] foundRows = dt.Select("LOADPASS_ID  = '" + drpDocumentNo.SelectedValue + "'");
                    if (foundRows.Length > 0)
                    {
                        drpDistributor.SelectedValue = foundRows[0]["DISTRIBUTOR_ID"].ToString();
                        this.LoadArea();
                       
                        DrpRoute.SelectedValue = foundRows[0]["AREA_ID"].ToString();
                        try
                        {
                        LoadCustomer();
                        if (foundRows[0]["CUSTOMER_ID"].ToString() == "")
                        {
                            //Do nothing as Customer Not saved 
                            DrpCustomer.SelectedIndex = 0;
                        }
                        else
                        {
                            DrpCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
                            
                        }
                        }
                        catch (Exception EX)
                        {
                            DrpCustomer.SelectedIndex = 0;
                           
                        }
                       
                        DrpPrincipal.SelectedValue = foundRows[0]["PRINCIPAL_ID"].ToString();
                        this.LoadDeliveryman();
                        DrpDeliveryMan.SelectedValue = foundRows[0]["DELIVERY_MAN_ID"].ToString();
                        LoadVehicleNO();
                      
                        LOADPASS_ID = long.Parse(drpDocumentNo.SelectedValue);
                        this.LoadSKUDetail();
                        this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));
                        ClearAll();
                        EnableDisableController(false);

                    }
                }


            }
            else
            {

                DataTable dt = (DataTable)this.Session["dtOrder"];
                DataRow[] foundRows = dt.Select("LOAD_ID  = '" + drpDocumentNo.SelectedItem.Text + "'");
                if (foundRows.Length > 0)
                {
                    drpDistributor.SelectedValue = foundRows[0]["DISTRIBUTOR_ID"].ToString();
                    this.LoadArea();
                    this.LoadDeliveryman();
                    DrpRoute.SelectedValue = foundRows[0]["AREA_ID"].ToString();
                    LoadCustomer();
                    DrpPrincipal.SelectedValue = foundRows[0]["PRINCIPAL_ID"].ToString();
                    DrpDeliveryMan.SelectedValue = foundRows[0]["DELIVERY_MAN_ID"].ToString();

                    this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));

                    btnSaveOrder.Text = "Save";

                    //this.ClearAll();
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
                    EnableDisableController(true);
                }
            }
        }
        else
        {

            this.CreatTable();
            this.LoadGird();
            ClearMasterALL();
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadArea();
            this.LoadDeliveryman();
            btnSaveOrder.Text = "Save";
            EnableDisableController(true);

        }
    }
    
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadDeliveryman();
        this.LoadSKUDetail();
        LoadCustomer();
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
        txtToDate.Text = CurrentWorkDate.ToString("dd-MMM-yyyy");
    }
  
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDeliveryman();
        LoadVehicleNO();
        LoadCustomer();
    }
    
    protected void DrpDeliveryMan_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadVehicleNO();
    }
        
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataControl dc = new DataControl();
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        PurchaseSKU.Rows.RemoveAt(e.RowIndex);

        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird(PurchaseSKU);

    }

    #endregion

    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_CODE", typeof(string));
        PurchaseSKU.Columns.Add("SKU_NAME", typeof(string));
        PurchaseSKU.Columns.Add("SKU_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("DEMAND_QUANTITY", typeof(int));
        PurchaseSKU.Columns.Add("ISSUED_QUANTITY", typeof(int));
        PurchaseSKU.Columns.Add("RETURN_QUANTITY", typeof(int));
        PurchaseSKU.Columns.Add("BALANCE_QUANTITY", typeof(int));
        PurchaseSKU.Columns.Add("SALE_RETURN_QUANTITY", typeof(int));
        PurchaseSKU.Columns.Add("PURCHASE_RETURN_QUANTITY", typeof(int));
        PurchaseSKU.Columns.Add("IS_PENDING", typeof(bool));
        PurchaseSKU.Columns.Add("DOCUMENT_DATE", typeof(DateTime));
        PurchaseSKU.Columns.Add("GST_RATE_TP", typeof(decimal));
        this.Session.Add("PurchaseSKU", PurchaseSKU);



    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
      
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (btnSave.Text == "Add Sku")
        {
            if (CheckDublicateSKU())
            {
                if ((int.Parse(dc.chkNull_0(txtQuantity.Text)) >= Convert.ToInt32(dc.chkNull_0(txtReturn.Text))))
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_CODE"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_NAME"] = foundRows[0]["SKU_NAME"];
                    dr["SKU_RATE"] = foundRows[0]["TRADE_PRICE"];
                    dr["GST_RATE_TP"] = foundRows[0]["GST_RATE_TP"];
                    dr["DEMAND_QUANTITY"] = int.Parse(dc.chkNull_0(txtDemand.Text));
                    dr["ISSUED_QUANTITY"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["RETURN_QUANTITY"] = Constants.IntNullValue;
                    dr["BALANCE_QUANTITY"] = Constants.IntNullValue;
                    dr["SALE_RETURN_QUANTITY"] = Constants.IntNullValue;
                    dr["PURCHASE_RETURN_QUANTITY"] = Constants.IntNullValue;
                    dr["IS_PENDING"] = Constants.ByteNullValue;
                    dr["DOCUMENT_DATE"] = Constants.DateNullValue;
                    PurchaseSKU.Rows.Add(dr);
                    txtSold.Text = "";
                    drpDocumentNo.Enabled = true;
                    this.Session.Add("PurchaseSKU", PurchaseSKU);
                    this.LoadGird(PurchaseSKU);
                    this.ClearAll();
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Return Qty shuold be less than Issue Qty')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('SKU Already Exists')", true);
            }
        }
        else
        {
            if ((int.Parse(dc.chkNull_0(txtQuantity.Text)) >= Convert.ToInt32(dc.chkNull_0(txtReturn.Text))))
            {
                DataRow dr = PurchaseSKU.Rows[RowId];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_CODE"] = foundRows[0]["SKU_CODE"];
                dr["SKU_NAME"] = foundRows[0]["SKU_NAME"];
                dr["SKU_RATE"] = foundRows[0]["TRADE_PRICE"];
                dr["GST_RATE_TP"] = foundRows[0]["GST_RATE_TP"];
                dr["DEMAND_QUANTITY"] = int.Parse(dc.chkNull_0(txtDemand.Text));
                dr["ISSUED_QUANTITY"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                dr["RETURN_QUANTITY"] = Constants.IntNullValue;
                dr["BALANCE_QUANTITY"] = Constants.IntNullValue;
                dr["SALE_RETURN_QUANTITY"] = Constants.IntNullValue;
                dr["PURCHASE_RETURN_QUANTITY"] = Constants.IntNullValue;

                dr["IS_PENDING"] = Constants.ByteNullValue;
                dr["DOCUMENT_DATE"] = Constants.DateNullValue;
                txtSold.Text = "";
                drpDocumentNo.Enabled = true;
                this.Session.Add("PurchaseSKU", PurchaseSKU);
                this.LoadGird(PurchaseSKU);
                this.ClearAll();
                ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Return Qty shuold be less than Issue Qty')", true);
            }           
        }     
    }
    
    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        DataControl DC = new DataControl();
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        OrderEntryController mOrderController = new OrderEntryController();

        if (btnSaveOrder.Text != "Update")
        {
            LOADPASS_ID = Constants.LongNullValue;
        }
            if (GrdPurchase.Rows.Count > 0)
            {
                if (DrpVehicleNo.Items.Count > 0)
                {
                    foreach (DataRow dr in PurchaseSKU.Rows)
                    {
                        #region Stock calculation
                        // DataTable dtstock = mOrderController.GetOrderClosingStock2(int.Parse(drpDistributor.SelectedValue), int.Parse(dr["SKU_ID"].ToString()));
                        //DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue), int.Parse(dr["SKU_ID"].ToString()), "", DateTime.Parse(txtToDate.Text));
                        //if (dtstock.Rows.Count > 0)
                        //{
                        //    if (LOADPASS_ID == Constants.LongNullValue)
                        //    {

                        //        if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dr["ISSUED_QUANTITY"].ToString()) - int.Parse(dr["RETURN_QUANTITY"].ToString()) - int.Parse(dr["PURCHASE_RETURN_QUANTITY"].ToString()))
                        //        {
                        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                        //            return;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //stock
                        //        if ((int.Parse(dtstock.Rows[0][0].ToString())) < int.Parse(dr["ISSUED_QUANTITY"].ToString()) - int.Parse(dr["RETURN_QUANTITY"].ToString()) - int.Parse(dr["PURCHASE_RETURN_QUANTITY"].ToString()))
                        //        {
                        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Current Stock is " + (int.Parse(dtstock.Rows[0][0].ToString())).ToString() + "');", true);
                        //            return;
                        //        }

                        //    }




                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Code"].ToString() + "No Stock Found');", true);
                        //    return;
                        //}
                        #endregion
                        dr["RETURN_QUANTITY"] = Constants.IntNullValue;
                        dr["BALANCE_QUANTITY"] = Constants.IntNullValue;
                        dr["SALE_RETURN_QUANTITY"] = Constants.IntNullValue;
                        dr["PURCHASE_RETURN_QUANTITY"] = Constants.IntNullValue;

                        dr["IS_PENDING"] = Constants.ByteNullValue;
                        dr["DOCUMENT_DATE"] = Constants.DateNullValue;
                    }

                    bool IsValidInsert = false;
                    if (btnSaveOrder.Text != "Update")
                    {
                         IsValidInsert = mOrderController.InsertLoadPass(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), long.Parse(DrpRoute.SelectedValue), int.Parse(DrpDeliveryMan.SelectedValue), long.Parse(DrpVehicleNo.SelectedValue), PurchaseSKU, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(txtToDate.Text), int.Parse(DrpCustomer.SelectedValue));
                    }
                    else
                    {
                         IsValidInsert = mOrderController.UpdateLoadPass(long.Parse(drpDocumentNo.SelectedValue), int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), long.Parse(DrpRoute.SelectedValue), int.Parse(DrpDeliveryMan.SelectedValue), long.Parse(DrpVehicleNo.SelectedValue), PurchaseSKU, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(txtToDate.Text), int.Parse(DrpCustomer.SelectedValue));
                  
                    }
                    if (IsValidInsert)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Inserted.');", true);
                        this.ClearMasterALL();
                        this.ClearAll();
                        this.LoadPendingOrder();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz Try Again.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz Enter Vehicle NO.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz enter any Product first');", true);
            }
           
        }
     
    //Clear Operations
    private void ClearAll()
    {
     
        txtskuRate.Text = "";
        txtQuantity.Text = "";
        txtReturn.Text = "";
        txtSalesReturn.Text = "";
        txtPurchaseReturn.Text = "";
        txtDemand.Text = "";
        ddlSKuCde.Enabled = true;
       
        btnSaveOrder.Enabled = true;
        btnSave.Text = "Add Sku";
    }
    
    private void ClearMasterALL()
    {

        this.Session.Remove("PurchaseSKU");
     //   this.Session.Remove("dtOrder");
        this.CreatTable();
       
        this.LoadGird();
        
        LoadDistributor();
        LoadPrincipal();
        LoadArea();
        LoadDeliveryman();
        LoadVehicleNO();
        LoadSKUDetail();
        LoadCustomer();
      
        RowId = 0;
        EnableDisableController(true);

    }

    private void EnableDisableController(bool CValue)
    {
        if (CValue == true)
        {
            drpDistributor.Enabled = true;
            DrpPrincipal.Enabled = true;
            DrpRoute.Enabled = true;
            DrpDeliveryMan.Enabled = true;     
        }
        else
        {
            drpDistributor.Enabled = false;
            DrpPrincipal.Enabled = false;
            DrpRoute.Enabled = false;
            DrpDeliveryMan.Enabled = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Session.Remove("PurchaseSKU");
        this.Session.Remove("dtOrder");
        this.CreatTable();

        this.LoadGird();

        LoadDistributor();
        LoadPrincipal();
        LoadArea();
        LoadDeliveryman();
        LoadVehicleNO();
        LoadSKUDetail();
        LoadCustomer();
        RowId = 0;
        EnableDisableController(true);
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
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
        if (CurrentWorkDate> Convert.ToDateTime(txtToDate.Text))
        {            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invalid Working Date.');", true);
            txtToDate.Text = CurrentWorkDate.ToString("dd-MMM-yyyy");
            return;
        }
        else
        {
            drpDocumentNo.Items.Clear();
            this.ClearMasterALL();
            this.ClearAll();
            this.LoadPendingOrder();
        }
    }
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUDetail();
    }

    protected void GrdPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowId = NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[NewEditIndex].Cells[0].Text;
        txtskuRate.Text = GrdPurchase.Rows[NewEditIndex].Cells[3].Text;
        txtDemand.Text = GrdPurchase.Rows[NewEditIndex].Cells[4].Text;
        txtQuantity.Text = GrdPurchase.Rows[NewEditIndex].Cells[5].Text;
        ddlSKuCde.Enabled = false;
        btnSave.Text = "Update SKU";
    }
}