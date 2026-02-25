using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Take Order, Invoice And Sale Return(Step1)
/// </summary>
public partial class Forms_frmOrderEntryStep1 : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadArea();
            this.LoadOrderBooker();
            this.LoadDeliveryman();
            DateTime pOrderDate = DateTime.Parse(this.Session["CurrentWorkDate"].ToString()).AddDays(1);
            txtToDate.Text = pOrderDate.ToString("dd-MMM-yyyy");   
        }
    }

    /// <summary>
    /// Enables/Disables Orderbooker Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedIndex == 0)
        {
            DrpOrderBooker.Enabled = true;
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
            
            DateTime pOrderDate = CurrentWorkDate.AddDays(1);
            txtToDate.Text = pOrderDate.ToString("dd-MMM-yyyy");
            txtToDate.Enabled = true;
            ImgBntToDate.Enabled = true;
        }
        else
        {
            DateTime pOrderDate = (DateTime)this.Session["CurrentWorkDate"];
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

            txtToDate.Enabled = false;
            ImgBntToDate.Enabled = false;
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
    /// Loads Routes To Route Combo, Orderbookers To Orderbooker Combo And Deliverymen To Deliveryman Comob
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadOrderBooker();
        this.LoadDeliveryman();
        DrpDocumentType_SelectedIndexChanged(null, null);
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
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Orderbookers To Orderbooker Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrderBooker();
    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()),Constants.IntNullValue, null, null);
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }

    /// <summary>
    /// Loads Orderbookers To Orderbooker Combo And Deliverymen To Deliveryman Comob
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrderBooker();
        this.LoadDeliveryman();
    }

    /// <summary>
    /// Loads Orderbookers To Orderbooker Combo
    /// </summary>
    private void LoadOrderBooker()
    {
        if (drpDistributor.Items.Count > 0 && DrpPrincipal.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(DrpPrincipal.SelectedValue));
            clsWebFormUtil.FillDropDownList(this.DrpOrderBooker, m_dt, 0, 3, true);
        }
        else
        {
            DrpOrderBooker.Items.Clear();
        }
    }

    /// <summary>
    /// Loads Deliverymen To Deliveryman Combo
    /// </summary>
    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpDeliveryMan, m_dt, 0, 3, true);
        }
        else
        {
            DrpDeliveryMan.Items.Clear();
        }
    }

    /// <summary>
    /// Checks/UnChecks All Orders In Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAll.Checked == true)
        {
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                ChbInvoice.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                ChbInvoice.Checked = false;
            }
        }
    }

    /// <summary>
    /// Loads Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnGetOrder_Click(object sender, EventArgs e)
    {
        if (DrpOrderBooker.Items.Count > 0)
        {
            this.LoadPendingOrder();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Orderbooker');", true);
        }
    }

    /// <summary>
    /// Loads Order Grid
    /// </summary>
    private void LoadPendingOrder()
    {
        OrderEntryController or = new OrderEntryController();
        DataTable dtOrder = or.SelectPendingOrder(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,
            int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(DrpOrderBooker.SelectedValue.ToString()), int.Parse(DrpDeliveryMan.SelectedValue.ToString()),
            Constants.Order_Pending_Id, int.Parse(ddSearchType.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), Convert.ToDateTime(txtToDate.Text));
        GrdOrder.DataSource = dtOrder;
        GrdOrder.DataBind();
        
    }

    /// <summary>
    /// Stores Related Data To Session Variables And Redirects To Order/Invoice Step 2
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnNext_Click(object sender, EventArgs e)
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

        if (Convert.ToDateTime(txtToDate.Text) < CurrentWorkDate)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invalid Working Date');", true);
            return;
        }
        if (DrpOrderBooker.Items.Count > 0)
        {
            if (drpDistributor.Items.Count > 0 && DrpPrincipal.Items.Count > 0 && DrpDeliveryMan.Items.Count > 0)
            {
                this.Session.Add("DistributorId", int.Parse(drpDistributor.SelectedValue.ToString()));
                this.Session.Add("PrincipalId", int.Parse(DrpPrincipal.SelectedValue.ToString()));
                this.Session.Add("AreaId", "");
                this.Session.Add("Route","");
                this.Session.Add("SaleMan", DrpDeliveryMan.SelectedItem.Text);
                this.Session.Add("SaleMan2", DrpDeliveryMan.SelectedItem.Value);
                this.Session.Add("OrderDate", Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy"));
                this.Session.Add("UnitType", RbUnitType.SelectedIndex);

                if (DrpDocumentType.SelectedIndex == 0)
                {
                    this.Session.Add("OrderNo", 0);
                    this.Session.Add("OrderBookerId", int.Parse(DrpOrderBooker.SelectedValue.ToString()));
                    this.Session.Add("DeliveryManId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                }
                else if (DrpDocumentType.SelectedIndex == 1)
                {// spot Sale
                    this.Session.Add("OrderNo", -1);
                    this.Session.Add("OrderBookerId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                    this.Session.Add("DeliveryManId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                }
                else
                {
                    this.Session.Add("OrderNo", -2);
                    this.Session.Add("OrderBookerId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                    this.Session.Add("DeliveryManId", int.Parse(DrpDeliveryMan.SelectedValue.ToString()));
                }
                Response.Redirect("~/Forms/frmOrderEntry.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Orderbooker');", true);
        }
    }

    /// <summary>
    /// Converts Checked Orders In Order Grid To Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPost_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        int? locationType = null;
        if (DrpOrderBooker.Items.Count > 0)
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
                {
                    locationType = int.Parse(dr["SUBZONE_ID"].ToString());
                    if (dr["MaxDayClose"].ToString().Length > 0)
                    {
                        CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        break;
                    }
                }
            }

            CustomerDataController CDC = new CustomerDataController();
            OrderEntryController ORD = new OrderEntryController();
            UserController mUserController = new UserController();
            DataControl dc = new DataControl();
            DataTable dtStock;
            GrdFreeSKU.Visible = false;
            gvRateDifference.Visible = false;
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                if (ChbInvoice.Checked == true)
                {
                    if (CurrentWorkDate>= Convert.ToDateTime(ConvertDate.British_To_American2(dr.Cells[5].Text)))
                    {
                        if (int.Parse(ddSearchType.SelectedValue.ToString()) == Constants.Cash_Order_Id)
                        {

                            DataTable dtCustomerMap = null;
                            bool isSynced = true;
                            DateTime distributorCurrentWorkDate = Constants.DateNullValue;
                            if (locationType == 3) //warehouse
                            {
                                dtCustomerMap = mUserController.SelectDistributorMapping(6, int.Parse(drpDistributor.SelectedValue), long.Parse(dr.Cells[0].Text));
                                if (dtCustomerMap != null && dtCustomerMap.Rows.Count > 0)
                                {
                                    if (dtCustomerMap.Rows[0]["distributorWorkingDate"] != null)
                                    {
                                        distributorCurrentWorkDate = DateTime.Parse(dtCustomerMap.Rows[0]["distributorWorkingDate"].ToString());
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Distributor Working Date not found');", true);
                                        return;
                                    }

                                    if (CurrentWorkDate < distributorCurrentWorkDate)
                                    {
                                        string customerName = dr.Cells[3].Text.Replace("'", "\\'"); // Escape single quotes
                                        string script = "alert('Distributor Working Date should not be greater than Warehouse working Date for auto GRN insertion. Customer: " + customerName + "');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", script, true);
                                        return;
                                    }
                                    else if (CurrentWorkDate > distributorCurrentWorkDate)
                                    {
                                        isSynced = false;
                                    }
                                }
                            }

                            dtStock = ORD.ConvertOrder_to_Invoice(int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[12].Text, long.Parse(dr.Cells[0].Text), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                            long.Parse(dr.Cells[4].Text), CurrentWorkDate, decimal.Parse(dc.chkNull_0(dr.Cells[6].Text)), decimal.Parse(dc.chkNull_0(dr.Cells[7].Text)),
                            decimal.Parse(dc.chkNull_0(dr.Cells[8].Text)), 
                            decimal.Parse(dc.chkNull_0(dr.Cells[9].Text)), 
                            decimal.Parse(dc.chkNull_0(dr.Cells[10].Text)), 
                            Constants.Order_Posted_Id, Constants.Cash_Order_Id, 
                            int.Parse(this.Session["UserId"].ToString()), dr.Cells[11].Text,
                            Convert.ToInt32(dr.Cells[13].Text), dr.Cells[3].Text,
                            dr.Cells[14].Text, decimal.Parse(dc.chkNull_0(dr.Cells[16].Text)));

                            if (dtStock.Columns.Count > 1)
                            {
                                if (dtStock.Columns.Count == 7)
                                {
                                    gvRateDifference.DataSource = dtStock;
                                    gvRateDifference.DataBind();
                                    gvRateDifference.Visible = true;
                                }
                                else
                                {
                                    GrdFreeSKU.DataSource = dtStock;
                                    GrdFreeSKU.DataBind();
                                    GrdFreeSKU.Visible = true;
                                }
                                this.LoadPendingOrder();
                                return;
                            }
                            else
                            {
                                if (locationType == 3) //Warehouse
                                {
                                    if (dtCustomerMap != null && dtCustomerMap.Rows.Count > 0)
                                    {
                                        InsertGRN(dtCustomerMap.Rows[0]["DISTRIBUTOR_ID"].ToString(), dtStock.Rows[0][0].ToString(),
                                            dr.Cells[0].Text, drpDistributor.SelectedValue, long.Parse(dr.Cells[4].Text),
                                            decimal.Parse(dc.chkNull_0(dr.Cells[6].Text)), isSynced,
                                            int.Parse(DrpPrincipal.SelectedValue.ToString()), dr.Cells[12].Text);
                                    }
                                }
                            }

                        }
                        else
                        {

                            DataTable dtCustomerMap = null;
                            bool isSynced = true;
                            DateTime distributorCurrentWorkDate = Constants.DateNullValue;
                            if (locationType == 3) //warehouse
                            {
                                dtCustomerMap = mUserController.SelectDistributorMapping(6, int.Parse(drpDistributor.SelectedValue), long.Parse(dr.Cells[0].Text));
                                if (dtCustomerMap != null && dtCustomerMap.Rows.Count > 0)
                                {
                                    if (dtCustomerMap.Rows[0]["distributorWorkingDate"] != null)
                                    {
                                        distributorCurrentWorkDate = DateTime.Parse(dtCustomerMap.Rows[0]["distributorWorkingDate"].ToString());
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Distributor Working Date not found');", true);
                                        return;
                                    }

                                    if (CurrentWorkDate < distributorCurrentWorkDate)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Distributor Working Date should not be greater than Warehouse working Date for auto GRN insertion. Customer: ' " + dr.Cells[3].Text + ");", true);
                                        return;
                                    }
                                    else if (CurrentWorkDate > distributorCurrentWorkDate)
                                    {
                                        isSynced = false;
                                    }
                                }
                            }

                            DataTable dt = CDC.SelectCustomerCreditBalance(long.Parse(dr.Cells[0].Text), int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(ddSearchType.SelectedValue.ToString()));
                            if (decimal.Parse(dc.chkNull_0(dt.Rows[0][0].ToString())) >= decimal.Parse(dr.Cells[10].Text))
                            {
                                dtStock = ORD.ConvertOrder_to_Invoice(int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[12].Text, long.Parse(dr.Cells[0].Text), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                                   long.Parse(dr.Cells[4].Text), CurrentWorkDate, decimal.Parse(dc.chkNull_0(dr.Cells[6].Text)), decimal.Parse(dc.chkNull_0(dr.Cells[7].Text)),
                                   decimal.Parse(dc.chkNull_0(dr.Cells[8].Text)), 
                                   decimal.Parse(dc.chkNull_0(dr.Cells[9].Text)),
                                   decimal.Parse(dc.chkNull_0(dr.Cells[10].Text)), 
                                   Constants.Order_Posted_Id, 
                                   int.Parse(ddSearchType.SelectedValue.ToString()), 
                                   int.Parse(this.Session["UserId"].ToString()), dr.Cells[11].Text, 
                                   Convert.ToInt32(dr.Cells[13].Text), dr.Cells[3].Text,
                                   dr.Cells[14].Text, decimal.Parse(dc.chkNull_0(dr.Cells[16].Text)));

                                if (dtStock.Columns.Count > 1)
                                {
                                    if (dtStock.Columns.Count == 7)
                                    {
                                        gvRateDifference.DataSource = dtStock;
                                        gvRateDifference.DataBind();
                                        gvRateDifference.Visible = true;
                                    }
                                    else
                                    {
                                        GrdFreeSKU.DataSource = dtStock;
                                        GrdFreeSKU.DataBind();
                                        GrdFreeSKU.Visible = true;
                                    }
                                    this.LoadPendingOrder();
                                    return;
                                }
                                else
                                {
                                    if (locationType == 3) //Warehouse
                                    {
                                        if (dtCustomerMap != null && dtCustomerMap.Rows.Count > 0)
                                        {
                                            InsertGRN(dtCustomerMap.Rows[0]["DISTRIBUTOR_ID"].ToString(), dtStock.Rows[0][0].ToString(),
                                                dr.Cells[0].Text, drpDistributor.SelectedValue, long.Parse(dr.Cells[4].Text),
                                                decimal.Parse(dc.chkNull_0(dr.Cells[6].Text)), isSynced, 
                                                int.Parse(DrpPrincipal.SelectedValue.ToString()), dr.Cells[12].Text);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error in Order#  " + dr.Cells[4].Text + " Customer Credit Limit " + dc.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Order Date Can not be greater than current working date');", true);
                        break;
                    }
                }
            }
            this.LoadPendingOrder();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select Orderbooker');", true);
        }
    }
    public void InsertGRN(string distributorId, string saleInvoiceId, string customerId, string wareHouseId, long orderId,
        decimal totalAmount, bool isSynced, int principalId, string billBookNo)
    {
        DateTime CurrentWorkDate = DateTime.Now;
        DataControl DC = new DataControl();
        OrderEntryController ORD = new OrderEntryController();
        PurchaseController _purchaseCtrl = new PurchaseController();

        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == wareHouseId)
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }

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

        DataTable PurchaseSKU = ORD.SelectOrderDetail(int.Parse(wareHouseId), orderId, int.Parse(distributorId));
        decimal totalOrderAmount = 0;

        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            DataRow dr1 = PurchasedPSKU.NewRow();

            decimal amount = decimal.Parse(DC.chkNull_0(dr["DISTRIBUTOR_PRICE_GRN"].ToString())) *
                decimal.Parse(DC.chkNull_0(dr["QUANTITY_UNIT"].ToString()));

            totalOrderAmount += amount;

            dr1["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"].ToString());
            dr1["BATCH_NO"] = "N/A"; //txtBatchNo.Text;
            dr1["FREE_SKU"] = 0;
            dr1["Quantity"] = int.Parse(dr["QUANTITY_UNIT"].ToString());
            dr1["PRICE"] = decimal.Parse(DC.chkNull_0(dr["DISTRIBUTOR_PRICE_GRN"].ToString()));
            dr1["Order_Quantity"] = int.Parse(dr["QUANTITY_UNIT"].ToString());
            dr1["AMOUNT"] = totalOrderAmount;
            dr1["TIME_STAMP"] = CurrentWorkDate;
            dr1["MFG_DATE"] = CurrentWorkDate;
            dr1["TAX_PERCENT"] = decimal.Parse(dr["GST_RATE"].ToString());
            PurchasedPSKU.Rows.Add(dr1);
        }

        if (PurchasedPSKU.Rows.Count > 0)
        {
            if (CurrentWorkDate != Constants.DateNullValue)
            {
                string voucherNo;
                long mResult = _purchaseCtrl.InsertPurchaseDocument2(int.Parse(distributorId), orderId.ToString(), 2
            , CurrentWorkDate, int.Parse(distributorId), 0
            , totalOrderAmount, false, PurchasedPSKU, 0, "", int.Parse(Session["UserId"].ToString()), principalId, Constants.LongNullValue, "",
            CurrentWorkDate, billBookNo, "", "", "", "", "", out voucherNo, long.Parse(saleInvoiceId), isSynced);
                if (mResult != -2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('GRN saved successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
        }
    }
}