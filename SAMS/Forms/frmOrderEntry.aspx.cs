using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Drawing;

public partial class Forms_frmOrderEntry : System.Web.UI.Page
{
    #region Variables

    DataTable PurchaseSKU;
    DataTable dtFreeSKU;
    private static int mCustomerTypeId;
    private static int mCustomerVolClassId;
    private int mTownId;
    private static int RowId;
    private static int UnitType;
    private static int OrderNo;
    private static int OrderNo2;
    private decimal mGst;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int OrderNo = (int)this.Session["OrderNo"];
            OrderNo2 = OrderNo;
            UnitType = (int)this.Session["UnitType"];
            txtDeliveryMan.Text = this.Session["SaleMan"].ToString();
            ScriptManager.GetCurrent(Page).SetFocus(txtBillBookNo);
            this.LoadArea();
            if (OrderNo == -1)
            {
                btnSaveOrder.Text = "Save Invoice";
                this.LoadInvoiceToEdit();
            }
            else if (OrderNo == -2)
            {
                drpDocumentNo.Enabled = false;
                btnSaveOrder.Text = "Sale Return";
                lblBillBook.Visible = false;
                txtBillBookNo.Visible = false;
                ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
                this.LoadPendingOrder();
            }
            else
            {
                this.LoadPendingOrder();
            }
            this.LoadVehicleNO();

            this.LoadCustomerData();
            ddlCustomer_SelectedIndexChange(null, null);
            this.CreatTable();
            this.LoadPromotion();
            this.CreateFreeSKU();
            this.LoadFreeGrid();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            txtBalance.Attributes.Add("Readonly", "Readonly");
            txtCreditLimit.Attributes.Add("Readonly", "Readonly");
            txtPriceGroup.Attributes.Add("Readonly", "Readonly");
        }
    }

    #region Load

    private void LoadInvoiceToEdit()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["DistributorId"].ToString())
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
        int AreaID = 0;
        if (DrpRoute.SelectedValue != null)
        {
            AreaID = int.Parse(this.DrpRoute.SelectedValue.ToString());
        }

        int PrincipalId = int.Parse(this.Session["PrincipalId"].ToString());
        DataTable dtDoc = or.GetDocumentNo2(int.Parse(this.Session["DistributorId"].ToString()), AreaID, int.Parse(this.Session["PrincipalId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), CurrentWorkDate, Convert.ToInt32(Session["UserID"]), 0, int.Parse(this.Session["OrderBookerId"].ToString()));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtDoc, "DocID", "DocNo");
        this.Session.Add("dtDoc", dtDoc);

    }
    private void LoadPendingOrder()
    {
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        DataTable dtOrder = or.SelectPendingOrder(int.Parse(this.Session["DistributorId"].ToString()), Constants.IntNullValue, int.Parse(this.Session["PrincipalId"].ToString()), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Convert.ToDateTime(this.Session["OrderDate"]));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtOrder, 0, 1);
        this.Session.Add("dtOrder", dtOrder);
    }
    private void LoadDocumentDetail(long DocID)
    {
        OrderEntryController ord = new OrderEntryController();
        PurchaseSKU = ord.GetDocumentDetail(DocID, 0);
        dtFreeSKU = ord.SelectInvoicePromotion(int.Parse(this.Session["DistributorId"].ToString()), DocID);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.Session.Add("dtDoc2", PurchaseSKU);
        this.Session.Add("dtFreeSKU", dtFreeSKU);
        this.LoadGird();
        this.LoadFreeGrid();
    }
    private void LoadCustomerData()
    {
        if (int.Parse(this.Session["DistributorId"].ToString()) > 0)
        {
            ddlCustomer.Items.Clear();
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(DrpRoute.SelectedValue), Constants.IntNullValue, int.Parse(this.Session["PrincipalId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_DETAIL2", true);
            this.Session.Add("dtCustomer", dtCustomer);
            if (ddlCustomer.Items.Count > 0)
            {
                txtPriceGroup.Text = ddlCustomer.SelectedItem.Text.Substring(ddlCustomer.SelectedItem.Text.IndexOf("=")).Replace("=", "");
            }
        }
        else
        {
            ddlCustomer.Items.Clear();
        }
    }
    private void LoadSKUDetail()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == Session["DistributorId"].ToString())
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        SKUPriceDetailController PController = new SKUPriceDetailController();
        PhaysicalStockController mController = new PhaysicalStockController();
        DataTable dtDMSKU = new DataTable();
        dtDMSKU.Columns.Add("SKU_ID", typeof(int));
        dtDMSKU.Columns.Add("SKU_Name", typeof(string));
        if (ddlCustomer.Items.Count > 0)
        {
            if (txtPriceGroup.Text.Trim() == "Default")
            {
                int SaleManID = 0;
                if (int.Parse(this.Session["PrincipalId"].ToString()) > 0)
                {
                    if (DrpRoute.SelectedValue != null)
                    {
                        SaleManID = int.Parse(DrpRoute.SelectedValue.ToString());
                    }
                    DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(this.Session["PrincipalId"].ToString()),
                        Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                        int.Parse(this.Session["DistributorId"].ToString()),
                        int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, CurrentWorkDate, Constants.LongNullValue);
                    this.Session.Add("Dtsku_Price", Dtsku_Price);
                    int OrderNo = (int)this.Session["OrderNo"];
                    if (OrderNo == -1)//If Spot Sale then only show SKUS assigned to Deliveryman
                    {
                        DataTable dtstock = mController.SelectSKUClosingStockFromLoadPass(int.Parse(this.Session["DistributorId"].ToString()), Constants.IntNullValue, txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), 2);
                        foreach (DataRow drStock in dtstock.Rows)
                        {
                            foreach (DataRow dr in Dtsku_Price.Rows)
                            {
                                if (dr["SKU_ID"].ToString() == drStock["SKU_ID"].ToString())
                                {
                                    DataRow drDMSKU = dtDMSKU.NewRow();
                                    drDMSKU["SKU_ID"] = dr["SKU_ID"];
                                    drDMSKU["SKU_NAME"] = dr["SKU_NAME"];
                                    dtDMSKU.Rows.Add(drDMSKU);
                                    break;
                                }
                            }
                        }
                        clsWebFormUtil.FillDropDownList(this.ddlSKuCde, dtDMSKU, "SKU_ID", "SKU_NAME", true);
                    }
                    else
                    {
                        clsWebFormUtil.FillDropDownList(this.ddlSKuCde, Dtsku_Price, "SKU_ID", "SKU_NAME", true);
                    }
                }
            }
            else if (txtPriceGroup.Text.Trim() != "")
            {
                ddlSKuCde.Items.Clear();
                DataTable Dtsku_Price = PController.GetCustomerSKU(int.Parse(this.Session["DistributorId"].ToString()), Convert.ToInt32(ddlCustomer.SelectedValue), CurrentWorkDate);
                this.Session.Add("Dtsku_Price", Dtsku_Price);
                int OrderNo = (int)this.Session["OrderNo"];
                if (OrderNo == -1)//If Spot Sale then only show SKUS assigned to Deliveryman
                {
                    DataTable dtstock = mController.SelectSKUClosingStockFromLoadPass(int.Parse(this.Session["DistributorId"].ToString()), Constants.IntNullValue, txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), 2);
                    foreach (DataRow drStock in dtstock.Rows)
                    {
                        foreach (DataRow dr in Dtsku_Price.Rows)
                        {
                            if (dr["SKU_ID"].ToString() == drStock["SKU_ID"].ToString())
                            {
                                DataRow drDMSKU = dtDMSKU.NewRow();
                                drDMSKU["SKU_ID"] = dr["SKU_ID"];
                                drDMSKU["SKU_NAME"] = dr["SkuPriceDetail3"];
                                dtDMSKU.Rows.Add(drDMSKU);
                            }
                        }
                    }
                    clsWebFormUtil.FillDropDownList(this.ddlSKuCde, dtDMSKU, "SKU_ID", "SKU_NAME", true);
                }
                else
                {
                    clsWebFormUtil.FillDropDownList(this.ddlSKuCde, Dtsku_Price, "SKU_ID", "SkuPriceDetail3", true);
                }
            }
        }
    }
    private void LoadPromotion()
    {
        if (int.Parse(this.Session["PrincipalId"].ToString()) > 0)
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable dt = PController.SelectDataPrice(int.Parse(this.Session["PrincipalId"].ToString()), Constants.IntNullValue,
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0,
                DateTime.Parse(this.Session["OrderDate"].ToString()), Constants.LongNullValue);
            if (bool.Parse(dt.Rows[0]["Is_ManualDiscount"].ToString()) == false)
            {
                OrderEntryController orderController = new OrderEntryController();
                PromotionCollections_Controller Arpc = orderController.LoadSchemes(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), Convert.ToDateTime(this.Session["OrderDate"]));
                this.Session.Add("Arpc", Arpc);
                txtDiscountType.Text = "Auto Discount";
                numtxtUnClaimabledist.ReadOnly = true;
                ChbDiscount.Checked = true;
            }
            else
            {
                txtDiscountType.Text = "Manual Discount";
                numTxtTotalStndrdDiscnt.ReadOnly = false;
                numtxtTotalExtraDiscnt.ReadOnly = false;
                numtxtUnClaimabledist.ReadOnly = false;
                ChbDiscount.Checked = false;
            }
        }
    }
    private void LoadArea()
    {

        SaleForceController mDController = new SaleForceController();
        DataTable r_dt = mDController.SelectRouteAssignedSaleForce(Convert.ToInt32(Session["DistributorId"]), Convert.ToInt32(Session["SaleMan2"]));
        clsWebFormUtil.FillDropDownList(this.DrpRoute, r_dt, 0, 3, true);
        // this.Session.Remove("SaleMan");
        //this.Session.Remove("SaleMan2");
    }
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();

        int TotalQuantity = 0;
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TotalQuantity += Convert.ToInt32(gvr.Cells[3].Text);
        }
        txtTotalQty.Text = TotalQuantity.ToString();
    }
    private void LoadVehicleNO()
    {
        DistributorController DC_ctrl = new DistributorController();

        //lblVehicleNo.Text
        DataTable v_dt = DC_ctrl.SelectVehicleNO(int.Parse(this.Session["SaleMan2"].ToString()));
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
    private void LoadCustomer()
    {
        CustomerDataController mController = new CustomerDataController();
        DataTable dt = mController.UspSelectCustomer(int.Parse(this.Session["DistributorId"].ToString()), "CUSTOMER_NAME", txtSeach.Text);
        this.Grid_users.DataSource = dt;
        this.Grid_users.DataBind();
    }
    private void LoadFreeGrid()
    {
        dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
        GrdFreeSKU.DataSource = dtFreeSKU;
        GrdFreeSKU.DataBind();
    }
    private bool FindCustomer()
    {
        DataTable dtCustomer = (DataTable)this.Session["dtCustomer"];
        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            this.Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
            mCustomerTypeId = int.Parse(foundRows[0]["CHANNEL_TYPE_ID"].ToString());
            mCustomerVolClassId = int.Parse(foundRows[0]["VOLUME_CLASS_ID"].ToString());
            mTownId = int.Parse(foundRows[0]["TOWN_ID"].ToString());

            return true;
        }
        return false;
    }

    private bool CheckDublicateSKU()
    {
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
    private int FreeSKUExist(DataTable dt, int Sku_id)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (int.Parse(dt.Rows[i]["SKU_ID"].ToString()) == Sku_id)
            {
                return i;
            }

        }
        return -1;
    }
    private void ExistenOrderDetail(long OrderId)
    {
        OrderEntryController ord = new OrderEntryController();

        PurchaseSKU = ord.SelectOrderDetail(int.Parse(this.Session["DistributorId"].ToString()), OrderId, Constants.IntNullValue);
        dtFreeSKU = ord.SelectOrderPromotion(int.Parse(this.Session["DistributorId"].ToString()), OrderId);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.Session.Add("dtFreeSKU", dtFreeSKU);
        this.LoadGird();
        this.LoadFreeGrid();
    }

    private bool IsBillBookNoExist()
    {
        bool flag = false;
        if ((long.Parse(drpDocumentNo.SelectedValue) == Constants.LongNullValue || this.Session["hfBillBookNo"].ToString() != txtBillBookNo.Text) && txtBillBookNo.Text.Trim().Length > 0)
        {
            OrderEntryController OEC = new OrderEntryController();
            DataTable dtBillBookNo = OEC.SelectBillBookNo(Convert.ToInt32(this.Session["DistributorId"].ToString()), txtBillBookNo.Text, 0);
            if (dtBillBookNo.Rows.Count > 0)
            {
                flag = true;
            }
            DataTable dtBillBookNo2 = OEC.SelectBillBookNo(Convert.ToInt32(this.Session["DistributorId"].ToString()), txtBillBookNo.Text, 1);
            if (dtBillBookNo2.Rows.Count > 0)
            {
                flag = true;
            }
        }
        return flag;
    }

    #endregion

    #region Search

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.LoadCustomer();
        this.SetTableSorter();
    }

    private void SetTableSorter()
    {
        if (Grid_users.Rows.Count > 1)
        {
            Grid_users.UseAccessibleHeader = true;
            Grid_users.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion

    #region Table Creation

    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("DistributorId", typeof(int));
        PurchaseSKU.Columns.Add("SALE_ORDER_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("DISTRIBUTOR_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("EXTRA_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("RETAIL_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_RATE2", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_AMOUNT2", typeof(decimal));
        PurchaseSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_EXTRA_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("SED_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("NET_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("ADVANCE_TAX_PERCENT", typeof(decimal));
        PurchaseSKU.Columns.Add("ADVANCE_TAX", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(decimal));
        PurchaseSKU.Columns.Add("IS_DELETED", typeof(bool));
        PurchaseSKU.Columns.Add("GST_ON", typeof(char));
        this.Session.Add("PurchaseSKU", PurchaseSKU);
    }

    private void CreateFreeSKU()
    {
        dtFreeSKU = new DataTable();
        dtFreeSKU.Columns.Add("SKU_ID", typeof(int));
        dtFreeSKU.Columns.Add("SKU_Code", typeof(string));
        dtFreeSKU.Columns.Add("SKU_Name", typeof(string));
        dtFreeSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        dtFreeSKU.Columns.Add("DISTRIBUTOR_PRICE", typeof(decimal));
        dtFreeSKU.Columns.Add("QUANTITY", typeof(int));
        dtFreeSKU.Columns.Add("AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_RATE", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_RATE2", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_AMOUNT2", typeof(decimal));
        dtFreeSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("PROMOTION_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_DETAIL_ID", typeof(int));
        dtFreeSKU.Columns.Add("PROMOTION_OFFER_ID", typeof(int));
        this.Session.Add("dtFreeSKU", dtFreeSKU);
    }

    #endregion

    #region Sel/Index Change

    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (long.Parse(drpDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            DataControl dc = new DataControl();
            if (OrderNo2 == -1)
            {
                if (btnSaveOrder.Text == "Save Invoice" || btnSaveOrder.Text == "Update Invoice")
                {
                    if (drpDocumentNo.Items.Count > 0)
                    {
                        decimal ObjGrossSales = 0;
                        decimal ObjStandardDiscount = 0;
                        decimal ObjExtraDiscount = 0;
                        decimal ObjSchemeDiscount = 0;
                        decimal ObjTotalGST = 0;
                        decimal ObjTotalGST2 = 0;
                        decimal ObjTotalSED = 0;
                        decimal ObjTotalTST = 0;
                        decimal ObjNetAmount = 0;
                        decimal ObjClaimableDiscount = 0;


                        DataTable dt = (DataTable)this.Session["dtDoc"];
                        DataRow[] foundRows = dt.Select("DocID  = '" + drpDocumentNo.SelectedValue + "'");

                        if (foundRows.Length > 0)
                        {
                            txtBillBookNo.Text = dt.Rows[0]["MANUAL_INVOICE_ID"].ToString();
                            RblPayMode.SelectedValue = dt.Rows[0]["PayMode"].ToString();
                            numTxtAdvanceTax.Text = dt.Rows[0]["ADVANCE_TAX"].ToString();
                            hfslabTaxPercent.Value = dt.Rows[0]["ADVANCE_TAX_PERCENT"].ToString();
                            btnSaveOrder.Text = "Update Invoice";
                            txtPriceGroup.Text = foundRows[0]["PriceGroup"].ToString();
                            ddlCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
                            this.LoadSKUDetail();
                            txtBillBookNo.Text = dt.Rows[0]["MANUAL_INVOICE_ID"].ToString();
                            numTxtTotalStndrdDiscnt.Text = foundRows[0]["DISCOUNT_AMOUNT"].ToString();
                            numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();

                            this.LoadDocumentDetail(Convert.ToInt64(drpDocumentNo.SelectedValue));

                            DataTable dt2 = (DataTable)this.Session["dtDoc2"];

                            foreach (DataRow dr in dt2.Rows)
                            {
                                ObjStandardDiscount = Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) + Convert.ToDecimal(dc.chkNull_0(numtxtTotalExtraDiscnt.Text));
                                ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                                ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                                ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));
                                ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                                ObjTotalGST2 += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT2"].ToString()));
                                ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                                ObjNetAmount += decimal.Parse(dc.chkNull_0(dr["NET_AMOUNT"].ToString()));
                                ObjClaimableDiscount += decimal.Parse(dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString()));

                            }
                            txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
                            numtxtUnClaimabledist.Text = Convert.ToString(Math.Round(ObjTotalSED, 2));
                            numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST, 2));
                            numTxtTotalTST.Text = Convert.ToString(Math.Round(ObjTotalGST2, 2));
                        }
                    }

                }
            }//
            else
            {
                txtBillBookNo.Text = string.Empty;

                DataTable dt = (DataTable)this.Session["dtOrder"];
                DataRow[] foundRows = dt.Select("SALE_ORDER_ID  = '" + drpDocumentNo.SelectedItem.Value + "'");
                if (foundRows.Length > 0)
                {
                    txtGrossAmount.Text = foundRows[0]["TOTAL_AMOUNT"].ToString();
                    numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
                    numTxtTotalStndrdDiscnt.Text = foundRows[0]["STANDARD_DISCOUNT_AMOUNT"].ToString();
                    DrpVehicleNo.SelectedValue = foundRows[0]["VEHICLE_NO"].ToString();
                    DrpRoute.SelectedValue = foundRows[0]["area_id"].ToString();
                    numTxtAdvanceTax.Text = dt.Rows[0]["ADVANCE_TAX"].ToString();
                    hfslabTaxPercent.Value = dt.Rows[0]["ADVANCE_TAX_PERCENT"].ToString();
                    this.LoadCustomerData();
                    lblVehicleNo.Text = foundRows[0]["VEHICLE_NON"].ToString();
                    numTxtTotalGST.Text = foundRows[0]["GST_AMOUNT"].ToString();
                    numTxtTotalTST.Text = foundRows[0]["TST_AMOUNT"].ToString();
                    txtPriceGroup.Text = foundRows[0]["PriceGroup"].ToString();
                    ddlCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
                    this.LoadSKUDetail();
                    numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
                    numtxtUnClaimabledist.Text = foundRows[0]["SED_AMOUNT"].ToString();
                    numTxtTotlAmnt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dc.chkNull_0(txtGrossAmount.Text)) -
                        Convert.ToDecimal(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) -
                        Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) +
                        Convert.ToDecimal(dc.chkNull_0(numTxtTotalGST.Text)) +
                        Convert.ToDecimal(dc.chkNull_0(numTxtTotalTST.Text)) +
                        Convert.ToDecimal(dc.chkNull_0(numTxtAdvanceTax.Text)), 2));

                    if (foundRows[0]["PO_DATE"].ToString() != "")
                    {
                        txtPODate.Text = Convert.ToDateTime(foundRows[0]["PO_DATE"]).ToString("dd-MMM-yyyy");
                    }
                    txtDCPONo.Text = foundRows[0]["DC_PO_NO"].ToString();

                    this.Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));

                    this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));
                    txtBillBookNo.Text = foundRows[0]["MANUAL_ORDER_ID"].ToString();
                    this.Session.Add("hfBillBookNo", foundRows[0]["MANUAL_ORDER_ID"].ToString());
                    EnableDisableController(false);
                    btnSaveOrder.Text = "Save Invoice";
                    btnSaveOrder.Enabled = false;
                    btnUpdateOrder.Enabled = false;
                    btnCalculate.Enabled = false;
                    this.ClearAll();
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
                    RblPayMode.SelectedValue = foundRows[0]["ORDER_TYPE_ID"].ToString();
                }
                else
                {
                    this.CreateFreeSKU();
                    this.CreatTable();
                    this.LoadGird();
                    this.LoadFreeGrid();
                    ClearMasterALL();
                    btnSaveOrder.Text = "Save Order";
                    this.Session.Remove("hfBillBookNo");
                }
            }
        }
        else
        {
            this.CreateFreeSKU();
            this.CreatTable();
            //this.LoadGird();
            //this.LoadFreeGrid();
            ClearMasterALL();
            if (OrderNo2 != -1)
            {
                btnSaveOrder.Text = "Save Order";
            }
            else
            {
                btnSaveOrder.Text = "Save Invoice";
            }
            this.Session.Remove("hfBillBookNo");

        }
    }

    protected void ChbBatchNo_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbBatchNo.Checked == true)
        {
            txtBatchNo.Text = "";
            txtBatchNo.Enabled = true;
        }
        else
        {
            txtBatchNo.Enabled = false;
            txtBatchNo.Text = "N/A";
        }
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCustomerData();
        ddlCustomer_SelectedIndexChange(null, null);
        txtPriceGroup.Text = "";
        txtCreditLimit.Text = "0";
        txtCreditUsed.Text = "0";
        txtBalance.Text = "0";

        ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
    }

    #endregion

    #region Grid Operations

    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        PurchaseSKU.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird();
        btnCalculate.Enabled = true;
        btnSaveOrder.Enabled = false;
        btnUpdateOrder.Enabled = false;
    }

    #endregion

    #region Click Operations

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //this.Session.Remove("CUSTOMER_ID");
        //this.Session.Remove("PrincipalId");
        //this.Session.Remove("AreaId");
        //this.Session.Remove("OrderBookerId");
        //this.Session.Remove("DeliveryManId");
        ////Response.Redirect("~/Forms/frmOrderEntryStep1.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
        //this.LoadPendingOrder();
        // this.Clear();

        this.LoadPendingOrder();
        ClearMasterALL();


    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (FindCustomer())
        {
            decimal ObjGrossSales = 0;
            decimal ObjStandardDiscount = 0;
            decimal ObjExtraDiscount = 0;
            decimal ObjTotalGST = 0;
            decimal ObjTotalGST2 = 0;
            decimal ObjTotalSED = 0;
            decimal ObjTotalTST = 0;
            decimal ObjFreeGSTAmount = 0;
            decimal ObjFreeGSTAmount2 = 0;
            decimal ObjFreeGrossAmt = 0;
            decimal ObjFreeTST = 0;
            decimal ProductExtDist = 0;
            decimal ProductStdDist = 0;
            decimal slabTaxPercent = 0;
            decimal DetailNetBeforeAdvanceTax = 0;

            DataControl dc = new DataControl();
            OrderEntryController or = new OrderEntryController();
            SKUGroupController GroupCtl = new SKUGroupController();
            DataTable dt = (DataTable)this.Session["PurchaseSKU"];
            foreach (DataRow dr in dt.Rows)
            {
                dr["STANDARD_DISCOUNT"] = 0.0m;
                dr["STANDARD_DISCOUNT_PER"] = 0.0m;
                dr["EXTRA_DISCOUNT"] = 0.0m;
                dr["CLAIM_EXTRA_AMOUNT"] = 0.0m;
                dr["CLAIM_STANDARD_DISCOUNT"] = 0.0m;
                dr["CLAIM_PER"] = 0.0m;
                dr["TST_AMOUNT"] = 0.0m;

            }
            PromotionCollections_Controller Arpc = (PromotionCollections_Controller)this.Session["Arpc"];
            this.CreateFreeSKU();
            this.LoadFreeGrid();

            if (dt.Rows.Count > 0 && ddlCustomer.SelectedValue != "")
            {
                DataTable dtCustomer = (DataTable)this.Session["dtCustomer"];
                DataRow[] foundCustomer = dtCustomer.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");
                if (foundCustomer.Length > 0)
                {
                    var slabRate = foundCustomer[0]["slabTaxRate"].ToString();
                    if (slabRate != null && slabRate.Length > 0)
                    {
                        slabTaxPercent = Convert.ToDecimal(slabRate);
                        hfslabTaxPercent.Value = slabTaxPercent.ToString();
                    }
                }

                if (ChbDiscount.Checked == true)
                {
                    ArrayList ARlistOffer = or.GetPromotionOffers(Arpc, mCustomerVolClassId, mCustomerTypeId, dt, false);

                    for (int i = 0; i < ARlistOffer.Count; i++)
                    {
                        PromoOffers_Collection pofferCol = (PromoOffers_Collection)ARlistOffer[i];

                        if (pofferCol.Is_Claimable)
                        {
                            if (pofferCol.SKU_ID > 0)
                            {
                                #region slab on SKU
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                                    {

                                        if (pofferCol.Is_And == true)
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                                dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                //dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        if (pofferCol.Free_SKU_ID > 0)
                                        {
                                            DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                            int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                            if (foundRows.Length > 0)
                                            {
                                                if (RowId < 0)
                                                {
                                                    DataRow dr1 = dtFreeSKU.NewRow();
                                                    dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                    dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                    dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                    if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        if (IsRegisteredCustomer())
                                                        {
                                                            dr1["GST_AMOUNT2"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["GST_AMOUNT2"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        }
                                                        dr1["TST_AMOUNT"] = 0;

                                                    }
                                                    else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["GST_AMOUNT2"] = 0;
                                                    }
                                                    else
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["GST_AMOUNT2"] = 0;
                                                        dr1["TST_AMOUNT"] = 0;
                                                    }

                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeGSTAmount2 += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                                    ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                    dtFreeSKU.Rows.Add(dr1);
                                                }
                                                else
                                                {

                                                    DataRow dr1 = dtFreeSKU.Rows[RowId];
                                                    ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                    ObjFreeGSTAmount2 = ObjFreeGSTAmount2 - decimal.Parse(dr1["GST_AMOUNT2"].ToString());
                                                    dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                    dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                    dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                    dr1["QUANTITY"] = int.Parse(dr1["QUANTITY"].ToString()) + pofferCol.Quantity;
                                                    if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                    {

                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        if (IsRegisteredCustomer())
                                                        {
                                                            dr1["GST_AMOUNT2"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["GST_AMOUNT2"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        }
                                                        dr1["TST_AMOUNT"] = 0;
                                                    }
                                                    else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["GST_AMOUNT2"] = 0;
                                                    }
                                                    else
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    }

                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeGSTAmount2 += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                                    ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                }
                                            }
                                        }
                                        this.Session.Add("dtFreeSKU", dtFreeSKU);
                                        this.LoadFreeGrid();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region slab on SKU Group

                                #region Free SKU
                                if (pofferCol.Free_SKU_ID > 0)
                                {
                                    DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                    DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                    int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                    if (foundRows.Length > 0)
                                    {
                                        if (RowId < 0)
                                        {
                                            DataRow dr1 = dtFreeSKU.NewRow();
                                            dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                            dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                            dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                            dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                if (IsRegisteredCustomer())
                                                {
                                                    dr1["GST_AMOUNT2"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["GST_AMOUNT2"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                }
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                            }
                                            else
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeGSTAmount2 += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                            ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                            dtFreeSKU.Rows.Add(dr1);
                                        }
                                        else
                                        {

                                            DataRow dr1 = dtFreeSKU.Rows[RowId];
                                            ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                            ObjFreeGSTAmount2 = ObjFreeGSTAmount2 - decimal.Parse(dr1["GST_AMOUNT2"].ToString());
                                            dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                            dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                            dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                            dr1["QUANTITY"] = int.Parse(dr1["QUANTITY"].ToString()) + pofferCol.Quantity;
                                            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                if (IsRegisteredCustomer())
                                                {
                                                    dr1["GST_AMOUNT2"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["GST_AMOUNT2"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                }
                                                dr1["TST_AMOUNT"] = 0;

                                            }
                                            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                            }
                                            else
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeGSTAmount2 += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                            ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                        }
                                        this.Session.Add("dtFreeSKU", dtFreeSKU);
                                        this.LoadFreeGrid();

                                    }
                                }
                                #endregion

                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                                    {
                                        if (pofferCol.Is_And == true)
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                                dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                //dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }

                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            if (pofferCol.SKU_ID > 0)
                            {
                                #region slab on SKU
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                                    {

                                        if (pofferCol.Is_And == true)
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                            }
                                        }
                                        if (pofferCol.Free_SKU_ID > 0)
                                        {
                                            DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                            int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                            if (foundRows.Length > 0)
                                            {
                                                if (RowId < 0)
                                                {
                                                    DataRow dr1 = dtFreeSKU.NewRow();
                                                    dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                    dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                    dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                    if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        if (IsRegisteredCustomer())
                                                        {
                                                            dr1["GST_AMOUNT2"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["GST_AMOUNT2"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        }
                                                        dr1["TST_AMOUNT"] = 0;

                                                    }
                                                    else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["GST_AMOUNT2"] = 0;
                                                    }
                                                    else
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["GST_AMOUNT2"] = 0;
                                                        dr1["TST_AMOUNT"] = 0;
                                                    }
                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeGSTAmount2 += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                                    ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                    dtFreeSKU.Rows.Add(dr1);
                                                }
                                                else
                                                {

                                                    DataRow dr1 = dtFreeSKU.Rows[RowId];
                                                    ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                    ObjFreeGSTAmount2 = ObjFreeGSTAmount2 - decimal.Parse(dr1["GST_AMOUNT2"].ToString());
                                                    dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                    dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                    dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                    dr1["QUANTITY"] = int.Parse(dr1["QUANTITY"].ToString()) + pofferCol.Quantity;
                                                    if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        if (IsRegisteredCustomer())
                                                        {
                                                            dr1["GST_AMOUNT2"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["GST_AMOUNT2"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        }
                                                        dr1["TST_AMOUNT"] = 0;

                                                    }
                                                    else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["GST_AMOUNT2"] = 0;
                                                    }
                                                    else
                                                    {
                                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                        dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                        dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                        dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                        dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                        dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    }
                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeGSTAmount2 += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                                    ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                }
                                            }
                                        }
                                        this.Session.Add("dtFreeSKU", dtFreeSKU);
                                        this.LoadFreeGrid();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region slab on SKU Group

                                #region Free SKU
                                if (pofferCol.Free_SKU_ID > 0)
                                {
                                    DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                    DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                    int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                    if (foundRows.Length > 0)
                                    {
                                        if (RowId < 0)
                                        {
                                            DataRow dr1 = dtFreeSKU.NewRow();
                                            dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                            dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                            dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                            dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                if (IsRegisteredCustomer())
                                                {
                                                    dr1["GST_AMOUNT2"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["GST_AMOUNT2"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                }
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                            }
                                            else
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeGSTAmount2 += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                            ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                            dtFreeSKU.Rows.Add(dr1);
                                        }
                                        else
                                        {

                                            DataRow dr1 = dtFreeSKU.Rows[RowId];
                                            ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                            ObjFreeGSTAmount2 = ObjFreeGSTAmount2 - decimal.Parse(dr1["GST_AMOUNT2"].ToString());
                                            dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                            dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                            dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                            dr1["QUANTITY"] = int.Parse(dr1["QUANTITY"].ToString()) + pofferCol.Quantity;
                                            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                if (IsRegisteredCustomer())
                                                {
                                                    dr1["GST_AMOUNT2"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["GST_AMOUNT2"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                }
                                                dr1["TST_AMOUNT"] = 0;

                                            }
                                            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                            }
                                            else
                                            {
                                                dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                dr1["DISTRIBUTOR_PRICE"] = foundRows[0]["DISTRIBUTOR_PRICE"];
                                                dr1["QUANTITY"] = pofferCol.Quantity.ToString();
                                                dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                dr1["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                                                dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeGSTAmount2 += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                            ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                        }
                                        this.Session.Add("dtFreeSKU", dtFreeSKU);
                                        this.LoadFreeGrid();

                                    }
                                }
                                #endregion


                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                                    {
                                        if (pofferCol.Is_And == true)
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + ((decimal.Parse(pofferCol.Discount.ToString())) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()) - decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                decimal standardPercent = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["STANDARD_DISCOUNT_PER"] = standardPercent;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #region Recalulate Products

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToDecimal(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) != 0)
                        {
                            //dr["STANDARD_DISCOUNT"] = dr["STANDARD_DISCOUNT"]
                        }
                        else
                        {
                            dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));

                        }
                        dr["CLAIM_STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())));
                        dr["SED_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString()));

                        ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        ObjStandardDiscount += decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));

                        #region GST Calculation
                        decimal TempAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        decimal TempDiscount = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        decimal TempExtraDisAmt = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                        dr["GST_AMOUNT2"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(dc.chkNull_0(dr["GST_RATE2"].ToString())) / 100;
                        dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT2"].ToString()));
                        ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                        ObjTotalGST2 += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT2"].ToString()));
                        ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region Reversecalulate Products

                    //decimal ObjGrossSales = 0;
                    decimal ObjTaxableAmount = 0;
                    decimal ObjClaimAblePer = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                    }
                    if (decimal.Parse(dc.chkNull_0(numtxtUnClaimabledist.Text)) > 0)
                    {
                        ObjClaimAblePer = decimal.Parse(dc.chkNull_0(numtxtUnClaimabledist.Text)) / ObjGrossSales;
                    }
                    if (decimal.Parse(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) > 0)
                    {
                        ProductStdDist = decimal.Parse(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) / (ObjGrossSales - ObjTaxableAmount);
                    }
                    if (decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) > 0)
                    {
                        ProductExtDist = decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) / (ObjGrossSales - ObjTaxableAmount);
                    }
                    ObjGrossSales = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal TempAmount = 0;
                        decimal TempDiscount = 0;
                        decimal TempExtraDisAmt = 0;

                        dr["EXTRA_DISCOUNT"] = ProductExtDist * decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        dr["STANDARD_DISCOUNT"] = ProductStdDist * decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));

                        dr["SED_AMOUNT"] = ObjClaimAblePer * decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));

                        #region GST Calculation

                        TempAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        TempDiscount = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        TempExtraDisAmt = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));

                        dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                        dr["GST_AMOUNT2"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(dc.chkNull_0(dr["GST_RATE2"].ToString())) / 100;
                        dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));

                        ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                        ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                        ObjTotalGST2 += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT2"].ToString()));
                        ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));

                        ObjStandardDiscount += decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        #endregion
                    }
                    #endregion
                }

                #region Set Total Values

                txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
                numtxtTotalExtraDiscnt.Text = Convert.ToString(Math.Round(ObjExtraDiscount, 2));
                numTxtTotalStndrdDiscnt.Text = Convert.ToString(Math.Round(ObjStandardDiscount, 2));
                numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST, 2));
                numTxtTotalTST.Text = Convert.ToString(Math.Round(ObjTotalGST2, 2));

                numTxtTotlAmnt.Text = Convert.ToString(Math.Round(ObjGrossSales - ObjExtraDiscount -
                    ObjStandardDiscount + ObjTotalGST + ObjTotalGST2 + ObjTotalTST + ObjFreeTST, 2));

                decimal taxAmount = 0;
                decimal netAmount = 0;

                if (!string.IsNullOrEmpty(numTxtTotlAmnt.Text))
                {
                    netAmount = Convert.ToDecimal(numTxtTotlAmnt.Text);
                    taxAmount = netAmount * (slabTaxPercent / 100);
                    numTxtAdvanceTax.Text = Convert.ToString(Math.Round(taxAmount, 2));
                    numTxtTotlAmnt.Text = Convert.ToString(Math.Round((netAmount + taxAmount), 2));
                }
                decimal DetailAdvAmount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    DetailNetBeforeAdvanceTax += decimal.Parse(dc.chkNull_0(dr["NET_AMOUNT"].ToString()));
                }
                if (!string.IsNullOrEmpty(numTxtAdvanceTax.Text))
                {
                    DetailAdvAmount = decimal.Parse(dc.chkNull_0(numTxtAdvanceTax.Text)) / DetailNetBeforeAdvanceTax;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    decimal AdVTax = decimal.Parse(dc.chkNull_0(dr["NET_AMOUNT"].ToString())) * DetailAdvAmount;
                    dr["ADVANCE_TAX"] = AdVTax;
                    dr["ADVANCE_TAX_PERCENT"] = slabTaxPercent;
                    dr["NET_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["NET_AMOUNT"].ToString())) + AdVTax;
                }


                numtxtUnClaimabledist.Text = Convert.ToString(Math.Round(ObjTotalSED, 2));

                #endregion

                btnSaveOrder.Enabled = true;
                if (drpDocumentNo.SelectedIndex != 0)
                {
                    btnUpdateOrder.Enabled = true;
                }
                if (RblPayMode.SelectedIndex == 1)
                {
                    txtCashReceived.Enabled = true;
                    ScriptManager.GetCurrent(Page).SetFocus(txtCashReceived);
                }
                else
                {
                    txtCashReceived.Enabled = false;
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        decimal mTradePrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));
        decimal mDistPrice = decimal.Parse(dc.chkNull_0(foundRows[0]["DISTRIBUTOR_PRICE"].ToString()));
        int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));

        if (Session["OrderNo"].ToString() == "-1")
        {
            bool flag = true;
            if (GrdPurchase.Rows.Count > 0)
            {
                if (foundRows[0]["GST_ON"].ToString().Trim() == "E")
                {
                    if (GrdPurchase.Rows[0].Cells[8].Text.Trim() == "E")
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    if (GrdPurchase.Rows[0].Cells[8].Text.Trim() == "T" || GrdPurchase.Rows[0].Cells[8].Text.Trim() == "R")
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }

            if (!flag)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please enter same type of tax behaviour products')", true);
                return;
            }
        }
        if (btnSave.Text == "Add Sku")
        {
            if (CheckDublicateSKU())
            {
                DataRow dr = PurchaseSKU.NewRow();
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["GST_ON"] = foundRows[0]["GST_ON"].ToString().Trim();
                if (UnitType == 0)
                {
                    dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["QUANTITY_CTN"] = 0;
                }
                else
                {
                    dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;
                    dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                }
                dr["UNIT_PRICE"] = mTradePrice.ToString();
                dr["DISTRIBUTOR_PRICE"] = mDistPrice.ToString();

                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                    if (IsRegisteredCustomer())
                    {
                        dr["GST_RATE2"] = 0;
                    }
                    else
                    {
                        dr["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                    }
                    dr["TST_AMOUNT"] = 0;
                    dr["BATCH_NO"] = "T";
                }
                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY"].ToString());
                    dr["TST_AMOUNT"] = decimal.Parse(foundRows[0]["GST_RATE_TP"].ToString()) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["GST_RATE"] = 0;
                    dr["GST_RATE2"] = 0;
                    dr["BATCH_NO"] = "R";
                }
                else
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["TST_AMOUNT"] = 0;
                    dr["GST_RATE"] = 0;
                    dr["GST_RATE2"] = 0;
                    dr["BATCH_NO"] = "E";
                }
                dr["STANDARD_DISCOUNT"] = 0;
                dr["EXTRA_DISCOUNT"] = 0;
                dr["GST_AMOUNT"] = 0;
                dr["NET_AMOUNT"] = 0;
                PurchaseSKU.Rows.Add(dr);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('SKU Already Exists')", true);
            }
        }
        else
        {
            DataRow dr = PurchaseSKU.Rows[RowId];
            dr["SKU_ID"] = foundRows[0]["SKU_ID"];
            dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
            dr["BATCH_NO"] = txtBatchNo.Text;
            dr["GST_ON"] = foundRows[0]["GST_ON"].ToString().Trim();
            if (UnitType == 0)
            {
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
            }
            else
            {
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;
            }
            dr["UNIT_PRICE"] = mTradePrice.ToString();
            dr["DISTRIBUTOR_PRICE"] = mDistPrice.ToString();
            dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                if (IsRegisteredCustomer())
                {
                    dr["GST_RATE2"] = 0;
                }
                else
                {
                    dr["GST_RATE2"] = foundRows[0]["GST_RATE_TP2"];
                }
                dr["TST_AMOUNT"] = 0;
                dr["BATCH_NO"] = "T";
            }
            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY"].ToString());
                dr["TST_AMOUNT"] = decimal.Parse(foundRows[0]["GST_RATE_TP"].ToString()) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["GST_RATE"] = 0;
                dr["GST_RATE2"] = 0;
                dr["BATCH_NO"] = "R";
            }
            else
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["TST_AMOUNT"] = 0;
                dr["GST_RATE"] = 0;
                dr["GST_RATE2"] = 0;
                dr["BATCH_NO"] = "E";
            }
            dr["STANDARD_DISCOUNT"] = 0;
            dr["EXTRA_DISCOUNT"] = 0;
            dr["GST_AMOUNT"] = 0;
            dr["NET_AMOUNT"] = 0;
            dr["STANDARD_DISCOUNT_PER"] = 0;
        }

        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.EnableDisableController(false);
        this.LoadGird();
        this.ClearAll();
        ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);

        if (ChbDiscount.Checked == false)
        {
            decimal ObjGrossSales = 0;
            decimal ObjTaxableAmount = 0;
            foreach (DataRow dr in PurchaseSKU.Rows)
            {
                ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                if (dr["BATCH_NO"].ToString() == "T")
                {
                    ObjTaxableAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                }
            }
            txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
            numTxtTotalSED.Text = Convert.ToString(Math.Round(ObjTaxableAmount, 2));
        }
    }

    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        long vehicle = 0;
        int? locationType = null;

        if (DrpVehicleNo.Items.Count > 0)
        {
            vehicle = Convert.ToInt64(DrpVehicleNo.SelectedValue);
        }
        DateTime PO_DATE = Constants.DateNullValue;
        string DC_PO_NO = null;
        if (txtPODate.Text.Trim().Length > 0)
        {
            PO_DATE = Convert.ToDateTime(txtPODate.Text);
        }
        if (txtDCPONo.Text.Trim().Length > 0)
        {
            DC_PO_NO = txtDCPONo.Text;
        }
        if (FindCustomer())
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == Session["DistributorId"].ToString())
                {
                    locationType = int.Parse(dr["SUBZONE_ID"].ToString());
                    if (dr["MaxDayClose"].ToString().Length > 0)
                    {
                        CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        break;
                    }
                }
            }

            DataTable dtCustomerMap = null;
            bool isSynced = true;
            DateTime distributorCurrentWorkDate = Constants.DateNullValue;
            if (locationType == 3 && btnSaveOrder.Text == "Save Invoice") //warehouse
            {
                UserController mUserController = new UserController();
                dtCustomerMap = mUserController.SelectDistributorMapping(6, int.Parse(Session["DistributorId"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()));
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Distributor Working Date should not be greater than Warehouse working Date for auto GRN insertion');", true);
                        return;
                    }
                    else if (CurrentWorkDate > distributorCurrentWorkDate)
                    {
                        isSynced = false;
                    }
                }
            }


            string ManualID = null;
            if (txtBillBookNo.Text.Trim().Length > 0)
            {
                ManualID = txtBillBookNo.Text.ToUpper();
            }
            DataControl DC = new DataControl();
            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
            OrderEntryController mOrderController = new OrderEntryController();

            decimal advanceTaxPercent = !string.IsNullOrEmpty(hfslabTaxPercent.Value) ? decimal.Parse(hfslabTaxPercent.Value) : 0;
            decimal advanceTaxAmount = !string.IsNullOrEmpty(numTxtAdvanceTax.Text) ? decimal.Parse(numTxtAdvanceTax.Text) : 0;

            if (btnSaveOrder.Text == "Save Order")
            {
                if (!IsBillBookNoExist())
                {
                    if (int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Advance_PaymentOrder_id)
                    {
                        CustomerDataController CDC = new CustomerDataController();
                        DataTable dt = CDC.GetCustomerAdvance(long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), int.Parse(this.Session["DistributorId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()));
                        decimal NetCashSale = decimal.Parse(numTxtTotlAmnt.Text) - decimal.Parse(DC.chkNull_0(txtCashReceived.Text));

                        if (decimal.Parse(DC.chkNull_0(dt.Rows[0][0].ToString())) <= NetCashSale)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Advance is " + DC.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                            return;
                        }
                    }

                    bool IsValidInsert = mOrderController.Add_Order(int.Parse(this.Session["DistributorId"].ToString()),
                        ManualID, mTownId, long.Parse(DrpRoute.SelectedValue),
                        int.Parse(this.Session["PrincipalId"].ToString()),
                        long.Parse(this.Session["CUSTOMER_ID"].ToString()),
                        long.Parse(this.Session["CUSTOMER_ID"].ToString()),
                        int.Parse(this.Session["OrderBookerId"].ToString()),
                        int.Parse(this.Session["DeliveryManId"].ToString()),
                        int.Parse(RblPayMode.SelectedValue.ToString()),
                    decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)),
                    decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)),
                    decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)),
                    decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)),
                    decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0,
                    Constants.Order_Pending_Id, PurchaseSKU, dtFreeSKU,
                    int.Parse(this.Session["UserId"].ToString()),
                    DateTime.Parse(this.Session["OrderDate"].ToString()),
                    decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)),
                    decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)), vehicle, PO_DATE,
                    DC_PO_NO, advanceTaxPercent, advanceTaxAmount);

                    if (IsValidInsert)
                    {
                        this.LoadPendingOrder();
                        this.ClearMasterALL();
                        this.Session.Remove("hfBillBookNo");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Bill Book No already exist,Kindly enter different Bill Book No');", true);
                }
            }
            else if (btnSaveOrder.Text == "Save Invoice")
            {
                if (CurrentWorkDate >= Convert.ToDateTime(Session["OrderDate"].ToString()))
                {
                    if (!IsBillBookNoExist())
                    {
                        if (int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Credit_Order_Id || int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Advance_PaymentOrder_id)
                        {
                            CustomerDataController CDC = new CustomerDataController();
                            DataTable dt = CDC.SelectCustomerCreditBalance(long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), int.Parse(this.Session["DistributorId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()));
                            decimal NetCashSale = decimal.Parse(numTxtTotlAmnt.Text) - decimal.Parse(DC.chkNull_0(txtCashReceived.Text));

                            if (decimal.Parse(DC.chkNull_0(dt.Rows[0][0].ToString())) <= NetCashSale)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Credit Limit or Advance is " + DC.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                                return;
                            }
                        }

                        foreach (DataRow dr in PurchaseSKU.Rows)
                        {
                            string OrderNo = Convert.ToString(Session["OrderNo"]);
                            PhaysicalStockController mController = new PhaysicalStockController();
                            DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(dr["SKU_ID"].ToString()), txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()));
                            if (dtstock.Rows.Count > 0)
                            {
                                if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dr["QUANTITY_UNIT"].ToString()))
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Code"].ToString() + "No Stock Found');", true);
                                return;
                            }

                            // Stock Calculation from Load Pass                               
                            #region Load pass stock check   

                            if (OrderNo == "-1")// spot sale
                            {
                                DataTable dtstock2 = mController.SelectSKUClosingStockFromLoadPass(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(dr["SKU_ID"].ToString()), txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), 0);
                                if (dtstock2.Rows.Count > 0)
                                {
                                    decimal freeQuantity = 0;
                                    foreach (DataRow drfree in dtFreeSKU.Rows)
                                    {
                                        if (Convert.ToInt32(dr["SKU_ID"]) == Convert.ToInt32(drfree["SKU_ID"]))
                                        {
                                            freeQuantity += Convert.ToDecimal(drfree["QUANTITY"]);
                                        }
                                    }
                                    if (int.Parse(dtstock2.Rows[0][0].ToString()) < (int.Parse(dr["QUANTITY_UNIT"].ToString()) + freeQuantity))
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Code"].ToString() + " not enough Stock found for this deliveryman');", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Code"].ToString() + "No Stock Found for this deliveryman');", true);
                                    return;
                                }
                            }
                            #endregion
                        }

                        long IsValidInsert = mOrderController.Add_Invoice2(int.Parse(this.Session["DistributorId"].ToString()), ManualID, mTownId, long.Parse(DrpRoute.SelectedValue), int.Parse(this.Session["PrincipalId"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), long.Parse(drpDocumentNo.SelectedValue.ToString()),
                        decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()),
                        PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()),
                        decimal.Parse(DC.chkNull_0(txtCashReceived.Text)),
                        DateTime.Parse(this.Session["OrderDate"].ToString()),
                        decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)),
                        decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)),
                        vehicle, mCustomerTypeId, ddlCustomer.SelectedItem.Text,
                        txtDeliveryMan.Text, PO_DATE, DC_PO_NO,
                        advanceTaxPercent, advanceTaxAmount
                        );
                        if (IsValidInsert > 0)
                        {
                            if (locationType == 3) //Warehouse
                            {
                                if (dtCustomerMap != null && dtCustomerMap.Rows.Count > 0)
                                {
                                    InsertGRN(dtCustomerMap.Rows[0]["DISTRIBUTOR_ID"].ToString(), IsValidInsert.ToString(),
                                        this.Session["CUSTOMER_ID"].ToString(), this.Session["DistributorId"].ToString(),
                                        long.Parse(drpDocumentNo.SelectedValue.ToString()),
                                        decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), vehicle.ToString(),
                                        isSynced, int.Parse(this.Session["PrincipalId"].ToString()),
                                        txtBillBookNo.Text);
                                }
                            }

                            if (drpDocumentNo.Enabled == true)
                            {
                                btnSaveOrder.Text = "Save Invoice";
                            }
                            this.LoadInvoiceToEdit();
                            this.ClearMasterALL();
                            this.Session.Remove("hfBillBookNo");
                            this.LoadSKUDetail();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error Occured. Please Try Again !');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Bill Book No already exist,Kindly enter different Bill Book No');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Order Date Can not be greater than current working date');", true);
                }
            }
            else if (btnSaveOrder.Text == "Update Invoice")
            {
                if (int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Credit_Order_Id || int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Advance_PaymentOrder_id)
                {
                    CustomerDataController CDC = new CustomerDataController();
                    DataTable dt = CDC.SelectCustomerCreditBalance(long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), int.Parse(this.Session["DistributorId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()));
                    decimal NetCashSale = decimal.Parse(numTxtTotlAmnt.Text) - decimal.Parse(DC.chkNull_0(txtCashReceived.Text));

                    if (decimal.Parse(DC.chkNull_0(dt.Rows[0][0].ToString())) <= NetCashSale)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Credit Limit/Advance Amount is " + DC.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                        return;
                    }
                }
                foreach (DataRow dr in PurchaseSKU.Rows)
                {
                    PhaysicalStockController mController = new PhaysicalStockController();
                    #region Comment
                    //if (OrderNo == "0")  //Remainig Stock Calculation from Stock Register
                    //{
                    //    DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(dr["SKU_ID"].ToString()), txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()));
                    //    if (dtstock.Rows.Count > 0)
                    //    {
                    //        if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dr["QUANTITY"].ToString()))
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                    //            return;
                    //        }
                    //        else if (Convert.ToDecimal(dtstock.Rows[0][1]) != Convert.ToDecimal(dr["UNIT_PRICE"]))
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Order Rate is " + dr["UNIT_PRICE"].ToString() + " and Current Rate is " + dtstock.Rows[0][1].ToString() + "');", true);
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Code"].ToString() + "No Stock Found');", true);
                    //        return;
                    //    }
                    //}
                    //// Remaining stock calculation from loadpass .. 
                    //if (OrderNo == "-1")  //Remainig Stock Calculation from Stock Register
                    //{
                    #endregion
                    DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(dr["SKU_ID"].ToString()), txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()));

                    if (dtstock.Rows.Count > 0)
                    {
                        decimal freeQuantity = 0;
                        foreach (DataRow drfree in dtFreeSKU.Rows)
                        {
                            if (Convert.ToInt32(dr["SKU_ID"]) == Convert.ToInt32(drfree["SKU_ID"]))
                            {
                                freeQuantity += Convert.ToDecimal(drfree["QUANTITY"]);
                            }
                        }
                        if (int.Parse(dtstock.Rows[0][0].ToString()) < (int.Parse(dr["QUANTITY_UNIT"].ToString()) + freeQuantity))
                        {
                            DataTable dtstock2 = mController.SelectSKUClosingStock(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(dr["SKU_ID"].ToString()), txtBatchNo.Text, DateTime.Parse(this.Session["OrderDate"].ToString()));
                            if (dtstock2.Rows.Count > 0)
                            {
                                if (int.Parse(dtstock2.Rows[0][0].ToString()) < (int.Parse(dr["QUANTITY_UNIT"].ToString()) + freeQuantity))
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Current Stock is " + dtstock2.Rows[0][0].ToString() + "');", true);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Code"].ToString() + "No Stock Found');", true);
                        return;
                    }
                }
                bool IsValidInsert = mOrderController.Update_Invoice2(Convert.ToInt64(drpDocumentNo.SelectedValue), CurrentWorkDate, int.Parse(this.Session["DistributorId"].ToString()), ManualID, long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()),
                   decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), Constants.DecimalNullValue, int.Parse(RblPayMode.SelectedValue.ToString()),
                   PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()),
                   decimal.Parse(DC.chkNull_0(txtCashReceived.Text)),
                   decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)),
                   decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)), 0, 0,
                   Constants.DateNullValue, int.Parse(this.Session["PrincipalId"].ToString()),
                   mCustomerTypeId, ddlCustomer.SelectedItem.Text, txtDeliveryMan.Text,
                   PO_DATE, DC_PO_NO, advanceTaxPercent, advanceTaxAmount);
                if (IsValidInsert)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice updated successfully.');", true);
                    this.LoadInvoiceToEdit();
                    this.ClearMasterALL();
                    btnSaveOrder.Text = "Save Invoice";
                    this.Session.Remove("hfBillBookNo");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error.');", true);
                }
            }
            else if (btnSaveOrder.Text == "Sale Return")
            {
                long IsValidInsert = mOrderController.Add_SaleReturn(int.Parse(this.Session["DistributorId"].ToString()), mTownId, long.Parse(DrpRoute.SelectedValue), int.Parse(this.Session["PrincipalId"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), long.Parse(drpDocumentNo.SelectedValue.ToString()),
                decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)),
                decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)),
                decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)),
                decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)),
                decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0,
                int.Parse(RblPayMode.SelectedValue.ToString()), PurchaseSKU,
                dtFreeSKU, int.Parse(this.Session["UserId"].ToString()),
                CurrentWorkDate, decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)),
                decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)), vehicle,
                mCustomerTypeId, ddlCustomer.SelectedItem.Text,
                txtDeliveryMan.Text, advanceTaxPercent, advanceTaxAmount);
                if (IsValidInsert > 0)
                {
                    this.LoadPendingOrder();
                    this.ClearMasterALL();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error.');", true);
                }
            }
        }
    }

    public void InsertGRN(string distributorId, string saleInvoiceId, string customerId, string wareHouseId, long orderId,
       decimal totalAmount, string vehicleNo, bool isSynced, int principalId, string billBookNo)
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
            dr1["AMOUNT"] = amount;
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
            CurrentWorkDate, billBookNo, "", "", "", vehicleNo, "", out voucherNo, long.Parse(saleInvoiceId), isSynced);
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
    #endregion

    #region Clear

    private void ClearAll()
    {
        //txtskuCode.Text = "";
        // txtskuName.Text = "";
        //ddlSKuCde.SelectedIndex = 0;
        txtUnitRate.Text = "";
        txtQuantity.Text = "";
        btnSave.Text = "Add Sku";
        btnSaveOrder.Enabled = false;
        btnUpdateOrder.Enabled = false;
        //  ddlSKuCde.Enabled = true;
        btnCalculate.Enabled = true;
        //  txtskuCode.Enabled = true;

    }
    private void ClearMasterALL()
    {
        this.EnableDisableController(true);
        this.Session.Remove("PurchaseSKU");
        this.Session.Remove("dtFreeSKU");
        btnSaveOrder.Enabled = false;
        btnUpdateOrder.Enabled = false;
        this.CreatTable();
        this.CreateFreeSKU();
        this.LoadGird();
        this.LoadFreeGrid();
        txtGrossAmount.Text = "";
        numtxtTotalExtraDiscnt.Text = "";
        numTxtTotalStndrdDiscnt.Text = "";
        numTxtTotalGST.Text = "";
        numTxtTotlAmnt.Text = "";
        hfslabTaxPercent.Value = "0";
        numTxtAdvanceTax.Text = "";
        numTxtTotalSED.Text = "";
        numTxtTotalTST.Text = "";
        numTxtTotalSED.Text = "";
        numtxtUnClaimabledist.Text = "";
        RowId = 0;
        mCustomerTypeId = 0;
        mCustomerVolClassId = 0;
        drpDocumentNo.SelectedIndex = 0;
        txtCashReceived.Text = "";
        txtDCPONo.Text = "";
        txtPODate.Text = "";
        txtBillBookNo.Text = string.Empty;
        if (btnSaveOrder.Text != "Sale Return")
        {
            ScriptManager.GetCurrent(Page).SetFocus(txtBillBookNo);
        }
        else
        {
            ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
        }

    }
    private void Clear()
    {
        DrpRoute.Enabled = true;
        this.Session.Remove("PurchaseSKU");
        this.Session.Remove("dtFreeSKU");
        btnSaveOrder.Enabled = false;
        btnUpdateOrder.Enabled = false;
        this.CreatTable();
        this.CreateFreeSKU();
        this.LoadGird();
        this.LoadFreeGrid();
        txtGrossAmount.Text = "";
        numtxtTotalExtraDiscnt.Text = "";
        numTxtTotalStndrdDiscnt.Text = "";
        numTxtTotalGST.Text = "";
        numTxtTotlAmnt.Text = "";
        numTxtAdvanceTax.Text = "";
        numTxtTotalSED.Text = "";
        numTxtTotalTST.Text = "";
        numTxtTotalSED.Text = "";
        numtxtUnClaimabledist.Text = "";
        RowId = 0;
        mCustomerTypeId = 0;
        mCustomerVolClassId = 0;
        hfslabTaxPercent.Value = "0";
        drpDocumentNo.SelectedIndex = 0;
        txtCashReceived.Text = "";
        txtBillBookNo.Text = string.Empty;
        if (btnSaveOrder.Text != "Sale Return")
        {
            ScriptManager.GetCurrent(Page).SetFocus(txtBillBookNo);
        }
        else
        {
            ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
        }

    }

    #endregion

    private bool IsRegisteredCustomer()
    {
        DataTable dtCustomer = (DataTable)Session["dtCustomer"];
        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_ID = '" + ddlCustomer.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            return bool.Parse(foundRows[0]["IS_GST_REGISTERED"].ToString());
        }
        return false;
    }

    private void EnableDisableController(bool CValue)
    {
        if (CValue == true)
        {
            if (txtGrossAmount.Text.Length > 0)
            {
                DrpRoute.Enabled = true;
                ddlCustomer.Enabled = true;
            }
        }
        else
        {
            DrpRoute.Enabled = false;
            ddlCustomer.Enabled = false;
        }
    }

    protected void ddlCustomer_SelectedIndexChange(object sender, EventArgs e)
    {
        try
        {
            this.LoadSKUDetail();
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }

    protected void GrdPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowId = NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[NewEditIndex].Cells[3].Text;
        txtUnitRate.Text = GrdPurchase.Rows[NewEditIndex].Cells[4].Text;
        txtBatchNo.Text = GrdPurchase.Rows[NewEditIndex].Cells[5].Text;
        if (UnitType == 0)
        {
            txtQuantity.Text = GrdPurchase.Rows[NewEditIndex].Cells[3].Text;
        }
        else
        {
            txtQuantity.Text = GrdPurchase.Rows[NewEditIndex].Cells[7].Text;
        }
        btnSave.Text = "Update SKU";
    }

    protected void btnUpdateOrder_Click(object sender, EventArgs e)
    {

        long vehicle = 0;
        if (DrpVehicleNo.Items.Count > 0)
        {
            vehicle = Convert.ToInt64(DrpVehicleNo.SelectedValue);
        }

        decimal advanceTaxPercent = !string.IsNullOrEmpty(hfslabTaxPercent.Value) ? decimal.Parse(hfslabTaxPercent.Value) : 0;
        decimal advanceTaxAmount = !string.IsNullOrEmpty(numTxtAdvanceTax.Text) ? decimal.Parse(numTxtAdvanceTax.Text) : 0;

        DateTime PO_DATE = Constants.DateNullValue;
        string DC_PO_NO = null;
        if (txtPODate.Text.Trim().Length > 0)
        {
            PO_DATE = Convert.ToDateTime(txtPODate.Text);
        }
        if (txtDCPONo.Text.Trim().Length > 0)
        {
            DC_PO_NO = txtDCPONo.Text;
        }


        if (FindCustomer())
        {
            string ManualID = null;
            if (txtBillBookNo.Text.Trim().Length > 0)
            {
                ManualID = txtBillBookNo.Text.ToUpper();
            }
            DataControl DC = new DataControl();
            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
            OrderEntryController mOrderController = new OrderEntryController();

            if (!IsBillBookNoExist())
            {
                if (int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Advance_PaymentOrder_id)
                {
                    CustomerDataController CDC = new CustomerDataController();
                    DataTable dt = CDC.GetCustomerAdvance(long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), int.Parse(this.Session["DistributorId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()));
                    decimal NetCashSale = decimal.Parse(numTxtTotlAmnt.Text) - decimal.Parse(DC.chkNull_0(txtCashReceived.Text));

                    if (decimal.Parse(DC.chkNull_0(dt.Rows[0][0].ToString())) <= NetCashSale)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Advance is " + DC.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                        return;
                    }
                }

                bool IsValidInsert = mOrderController.Update_Order(long.Parse(drpDocumentNo.SelectedValue), int.Parse(this.Session["DistributorId"].ToString()), ManualID, mTownId, long.Parse(DrpRoute.SelectedValue), int.Parse(this.Session["PrincipalId"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()),
                decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)),
                decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)),
                decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)),
                decimal.Parse(DC.chkNull_0(numTxtTotalGST.Text)),
                decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0,
                Constants.Order_Pending_Id, PurchaseSKU, dtFreeSKU,
                int.Parse(this.Session["UserId"].ToString()),
                DateTime.Parse(this.Session["OrderDate"].ToString()),
                decimal.Parse(DC.chkNull_0(numtxtUnClaimabledist.Text)),
                decimal.Parse(DC.chkNull_0(numTxtTotalTST.Text)),
                vehicle, PO_DATE, DC_PO_NO, advanceTaxPercent, advanceTaxAmount);

                if (IsValidInsert)
                {
                    this.LoadPendingOrder();
                    this.ClearMasterALL();
                    this.Session.Remove("hfBillBookNo");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Bill Book No already exist,Kindly enter different Bill Book No');", true);
            }
        }
    }
}