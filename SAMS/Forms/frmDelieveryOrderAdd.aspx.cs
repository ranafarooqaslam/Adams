using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommon.Classes;
using System.Collections;

public partial class Forms_frmDelieveryOrderAdd : System.Web.UI.Page
{
    
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
    readonly OrderEntryController oeCtrl = new OrderEntryController();
    
    private static int _mCustomerTypeId;
    private static int _mCustomerVolClassId;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("pragma", "no-cache");

            if (!Page.IsPostBack)
            {
                
                btnSaveOrder.Enabled = false;
                if (Session["orderId"] != null)
                {
                    LoadOrder();
                    Session.Add("PurchaseSKU", PurchaseSKU);

                }
                if (Session["invoiceId"] != null)
                {
                    LoadInvoice();
                    Session.Add("PurchaseSKU", PurchaseSKU);

                }
                LoadCustomer();
                LoadGird();

                LoadPromotion();
                LoadSKUDetail();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
  
    #region Load
    
    private void LoadOrder()
    {
        lblOrderNo.Text = "PO No: " + Session["orderId"].ToString();

        DataTable dt = oeCtrl.SelectPendingOrder2(Convert.ToInt64(Session["orderId"]), 1);
        if (dt.Rows.Count > 0)
        {
            hfOrderQty.Value = dt.Rows[0]["TOTAL_QTY"].ToString();
            hfDistributor.Value = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            hfPrincipal.Value = dt.Rows[0]["PRINCIPAL_ID"].ToString();
            hfCustomer.Value = dt.Rows[0]["CUSTOMER_ID"].ToString();
            hfLocation.Value = dt.Rows[0]["LOCATION_ID"].ToString();
            lblDate.Text =  dt.Rows[0]["DOCUMENT_DATE"].ToString();
           

            _mCustomerTypeId = int.Parse(dt.Rows[0]["BUSINESS_TYPE_ID"].ToString());
            _mCustomerVolClassId = int.Parse(dt.Rows[0]["VOLUME_CLASS_ID"].ToString());
            hfIsGstRegister.Value = dt.Rows[0]["IS_GST_REGISTERED"].ToString();
            //  hfGst.Value = dtCustomer.Rows[0]["TAX_RATE"].ToString();
            //    hfAddGst.Value = dtCustomer.Rows[0]["TAX_RATE2"].ToString();

            CustomerLedger();

            //------------------------------------------------------------------\\
            ddlDistributor.Text = dt.Rows[0]["DISTRIBUTOR_NAME"].ToString();
            lblCustomer.Text = dt.Rows[0]["CUSTOMER_DETAIL"].ToString();

            PurchaseSKU = oeCtrl.SelectOrderDetail(int.Parse(hfLocation.Value), long.Parse(Session["orderId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 0);

        }
    }

    private void LoadInvoice()
    {
        var id = Session["invoiceId"].ToString().Split('-');
        lblDONo.Text = "DO No: " + id[1];
        lblOrderNo.Text = "PO No: " + id[0];
        DataTable dt = oeCtrl.SelectPendingOrder2(Convert.ToInt64(id[1]), 2);
        if (dt.Rows.Count > 0)
        {

            hfOrderQty.Value = dt.Rows[0]["TOTAL_QTY"].ToString();
            hfDistributor.Value = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            hfPrincipal.Value = dt.Rows[0]["PRINCIPAL_ID"].ToString();
            hfCustomer.Value = dt.Rows[0]["CUSTOMER_ID"].ToString();
            numTxtTotalStndrdDiscnt.Text = dt.Rows[0]["DISCOUNT_AMOUNT"].ToString();
            numtxtTotalExtraDiscnt.Text = dt.Rows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
            lblDate.Text = dt.Rows[0]["PO_DATE"].ToString();
            RblPayMode.SelectedValue = dt.Rows[0]["PayMode"].ToString();

            btnSaveOrder.Text = "Update Invoice";
          

            _mCustomerTypeId = int.Parse(dt.Rows[0]["BUSINESS_TYPE_ID"].ToString());
            _mCustomerVolClassId = int.Parse(dt.Rows[0]["VOLUME_CLASS_ID"].ToString());
            hfIsGstRegister.Value = dt.Rows[0]["IS_GST_REGISTERED"].ToString();
            //  hfGst.Value = dtCustomer.Rows[0]["TAX_RATE"].ToString();
            //    hfAddGst.Value = dtCustomer.Rows[0]["TAX_RATE2"].ToString();

            CustomerLedger();

            //------------------------------------------------------------------\\
            ddlDistributor.Text = dt.Rows[0]["DISTRIBUTOR_NAME"].ToString();
            lblCustomer.Text = dt.Rows[0]["CUSTOMER_DETAIL"].ToString();

            PurchaseSKU = oeCtrl.SelectOrderDetail(int.Parse(hfDistributor.Value), long.Parse(id[1]), long.Parse(id[0]), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 1);
        }
    }
    private void LoadCustomer()
    {
        string SelectedTex = lblCustomer.Text;
        lblCustomer.Text= SelectedTex.Substring(0, SelectedTex.IndexOf("/"));
        txtCreditLimit.Text = SelectedTex.Substring(SelectedTex.IndexOf("/") + 1, SelectedTex.IndexOf("~") - SelectedTex.IndexOf("/") - 1);
        txtCreditUsed.Text = SelectedTex.Substring(SelectedTex.IndexOf("~") + 1, SelectedTex.IndexOf("=") - SelectedTex.IndexOf("~") - 1);
        txtPriceGroup.Text = SelectedTex.Substring(SelectedTex.IndexOf("=") + 1);
        txtBalance.Text = (Convert.ToDecimal(txtCreditLimit.Text) + Convert.ToDecimal(txtCreditUsed.Text)).ToString();

    }

    private void CustomerLedger()
    {
        
            //decimal Opening = 0;
            //DataTable dt = _customerCtrl.SelectCustomerCreditBalance(long.Parse(hfCustomer.Value), Constants.IntNullValue, Convert.ToInt32(hfDistributor.Value), Constants.Advance_PaymentOrder_id);

            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        Opening = Opening + decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString()));
                   
            //        lblLedgerBalance.Text = string.Format("{0:0,0.00}", Opening);
            //    }
            //    else
            //    {
            //        lblLedgerBalance.Text = Convert.ToString(Opening);
            //    }
            //}
            //else
            //{
            //    lblLedgerBalance.Text = Convert.ToString(Opening);
            //}

        
    }
    private void LoadPromotion()
    {
        
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable dt = PController.SelectDataPrice(int.Parse(hfPrincipal.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));

            if (bool.Parse(dt.Rows[0]["Is_ManualDiscount"].ToString()) == false)
            {
                OrderEntryController orderController = new OrderEntryController();
                PromotionCollections_Controller Arpc = oeCtrl.LoadSchemes(int.Parse(hfDistributor.Value), int.Parse(hfPrincipal.Value), Convert.ToDateTime(this.Session["CurrentWorkDate"]));
                Session.Add("Arpc", Arpc);
                txtDiscountType.Text = "Auto Discount";
                
                ChbDiscount.Checked = true;
            }
            else
            {
                txtDiscountType.Text = "Manual Discount";
                numTxtTotalStndrdDiscnt.ReadOnly = false;
                numtxtTotalExtraDiscnt.ReadOnly = false;
               
                ChbDiscount.Checked = false;
            }
        
    }

    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();

        DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(hfPrincipal.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(hfDistributor.Value), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        Session.Add("Dtsku_Price", Dtsku_Price);
    }
    #endregion

    #region Detail Panel

    DataTable PurchaseSKU = new DataTable();

    private void LoadGird()
    {
        int TotalValue = 0;
        //decimal TotalAmount = 0;
        int TotalSale = 0;
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];

        grdOrder.DataSource = PurchaseSKU;
        grdOrder.DataBind();

        if (Session["invoiceId"] == null)
        {
            foreach (DataRow dr in PurchaseSKU.Rows)
            {
                TotalValue += int.Parse(dr["QUANTITY_UNIT"].ToString());
                TotalSale += int.Parse(dr["RcdQty"].ToString());
            }

            if (TotalSale == TotalValue)
            {

                btnSaveOrder.Enabled = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Could not Dispatch against that Order.');", true);
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                //  "alert('Could not Dispatch against that Order.'); window.location='" +
                //    Request.ApplicationPath + "/Forms/frmSaleOrderView.aspx?LevelType=3&TopID=26&LevelID=97';", true);
                return;
            }
        }
        else
        {

            decimal ObjGrossSales = 0;
            decimal ObjStandardDiscount = 0;
            decimal ObjExtraDiscount = 0;

            decimal ObjTotalGST = 0;
            decimal ObjTotalGST2 = 0;
            decimal ObjTotalSED = 0;
            decimal ObjTotalTST = 0;
            decimal ObjNetAmount = 0;
            decimal ObjClaimableDiscount = 0;

            foreach (DataRow dr in PurchaseSKU.Rows)
            {
                ObjStandardDiscount = Convert.ToDecimal(_dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) + Convert.ToDecimal(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text));
                ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                ObjExtraDiscount += decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                ObjTotalSED += decimal.Parse(_dc.chkNull_0(dr["SED_AMOUNT"].ToString()));
                ObjTotalGST += decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                ObjTotalGST2 += decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT2"].ToString()));
                ObjTotalTST += decimal.Parse(_dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                ObjNetAmount += decimal.Parse(_dc.chkNull_0(dr["NET_AMOUNT"].ToString()));
                ObjClaimableDiscount += decimal.Parse(_dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString()));

            }
            txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
            // numtxtUnClaimabledist.Text = Convert.ToString(Math.Round(ObjTotalSED, 2));
            numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST, 2));
            numTxtTotalTST.Text = Convert.ToString(Math.Round(ObjTotalGST2, 2));
            numTxtTotlAmnt.Text = Convert.ToString(Math.Round(ObjNetAmount - ObjStandardDiscount, 2));
        }
    }

    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        if (e.Row.RowIndex == 0)
        {
            //Set the focus to control on the edited row
            e.Row.FindControl("txtCurrentRecQty").Focus();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCurrentRecQty = (TextBox)e.Row.FindControl("txtCurrentRecQty");
            
        }

    }

    protected void txtCurrentRecQty_TextChanged(object sender, EventArgs e)
    {

        TextBox txtCurrentRecQty = sender as TextBox;
        TextBox txtRecQty = sender as TextBox;

        try
        {
            if (txtCurrentRecQty == null) return;
            GridViewRow item = (GridViewRow)txtCurrentRecQty.NamingContainer;

            int recieveQty = 0;
            if (!string.IsNullOrEmpty(txtCurrentRecQty.Text))
            {
                //prev rec qty
                recieveQty = int.Parse(item.Cells[7].Text) + int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text));

            }

            DataTable order = (DataTable)Session["PurchaseSKU"];
            int orderQty = int.Parse(order.Rows[item.RowIndex]["QUANTITY_UNIT"].ToString());


            if (recieveQty > orderQty)
            {
                txtCurrentRecQty.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Dispatch Qty should not be greater than Order Qty!');", true);

            }
            else
            {
                order.Rows[item.RowIndex]["CurrentRcdQty"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)));
                order.Rows[item.RowIndex]["AMOUNT"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * decimal.Parse(order.Rows[item.RowIndex]["UNIT_PRICE"].ToString()));

                if (item.RowIndex + 1 < grdOrder.Rows.Count)
                {
                    Session.Add("PurchaseSKU", order);

                    grdOrder.Rows[item.RowIndex + 1].Cells[4].Focus();
                }
                else
                {
                    Session.Add("PurchaseSKU", order);
                }
            }
            btnSaveOrder.Enabled = false;
        }
        catch (Exception)
        {
            
            throw;
        }
    }


    #endregion

    private bool IsRegisteredCustomer()
    {

        if (hfIsGstRegister.Value == "True")
        {
            return bool.Parse(hfIsGstRegister.Value);
        }
        return false;
    }

    #region Click Operations
    protected void btnCalculate_Click(object sender, EventArgs e)
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

       
        SKUGroupController GroupCtl = new SKUGroupController();

        DataTable dt = (DataTable)this.Session["PurchaseSKU"];

        foreach (DataRow dr in dt.Rows)
        {
            dr["STANDARD_DISCOUNT"] = 0;
            dr["STANDARD_DISCOUNT_PER"] = 0;
            dr["EXTRA_DISCOUNT"] = 0;
            dr["CLAIM_EXTRA_AMOUNT"] = 0;
            dr["CLAIM_STANDARD_DISCOUNT"] = 0;
            dr["CLAIM_PER"] = 0;

            dr["AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["UNIT_PRICE"].ToString())) * int.Parse(dr["CurrentRcdQty"].ToString());

        }
        PromotionCollections_Controller Arpc = (PromotionCollections_Controller)this.Session["Arpc"];

        CreateFreeSKU();
        LoadFreeGrid();

        try
        {
            if (dt.Rows.Count > 0 && hfCustomer.Value != "")
            {
                if (ChbDiscount.Checked == true)
                {
                    ArrayList ARlistOffer = oeCtrl.GetPromotionOffers(Arpc, _mCustomerVolClassId, _mCustomerTypeId, dt, false);

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
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                                dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                //dr["STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["CLAIM_AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_PER"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        if (IsRegisteredCustomer())
                                                        {
                                                            dr1["GST_AMOUNT2"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["GST_AMOUNT2"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["GST_AMOUNT2"] = 0;
                                                        dr1["TST_AMOUNT"] = 0;
                                                    }

                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeGSTAmount2 += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                                    ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        if (IsRegisteredCustomer())
                                                        {
                                                            dr1["GST_AMOUNT2"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["GST_AMOUNT2"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    }

                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeGSTAmount2 += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                                    ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                if (IsRegisteredCustomer())
                                                {
                                                    dr1["GST_AMOUNT2"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["GST_AMOUNT2"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeGSTAmount2 += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                            ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                if (IsRegisteredCustomer())
                                                {
                                                    dr1["GST_AMOUNT2"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["GST_AMOUNT2"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeGSTAmount2 += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                            ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
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
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                                dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                //dr["STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                                //dr["CLAIM_AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                                dr["CLAIM_PER"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
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
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        if (IsRegisteredCustomer())
                                                        {
                                                            dr1["GST_AMOUNT2"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["GST_AMOUNT2"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = 0;
                                                        dr1["GST_AMOUNT2"] = 0;
                                                        dr1["TST_AMOUNT"] = 0;
                                                    }
                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeGSTAmount2 += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                                    ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                        if (IsRegisteredCustomer())
                                                        {
                                                            dr1["GST_AMOUNT2"] = 0;
                                                        }
                                                        else
                                                        {
                                                            dr1["GST_AMOUNT2"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                        dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
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
                                                        dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    }
                                                    dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                    dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                    dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                    dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                    ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                    ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                    ObjFreeGSTAmount2 += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                                    ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                if (IsRegisteredCustomer())
                                                {
                                                    dr1["GST_AMOUNT2"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["GST_AMOUNT2"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeGSTAmount2 += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                            ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                if (IsRegisteredCustomer())
                                                {
                                                    dr1["GST_AMOUNT2"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["GST_AMOUNT2"] = (decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP2"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["TST_AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
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
                                                dr1["AMOUNT"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                dr1["GST_AMOUNT"] = 0;
                                                dr1["GST_AMOUNT2"] = 0;
                                                dr1["TST_AMOUNT"] = 0;
                                            }
                                            dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                            dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                            dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                            dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                            ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                            ObjFreeGSTAmount += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                            ObjFreeGSTAmount2 += decimal.Parse(_dc.chkNull_0(dr1["GST_AMOUNT2"].ToString()));
                                            ObjFreeTST += decimal.Parse(_dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
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
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["EXTRA_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + ((decimal.Parse(pofferCol.Discount.ToString())) / 100) * (decimal.Parse(dr["AMOUNT"].ToString()) - decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));
                                            }
                                        }
                                        else
                                        {
                                            if (pofferCol.Offer_Value > 0)
                                            {
                                                dr["STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            }
                                            if (pofferCol.Discount > 0)
                                            {
                                                dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
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
                        if (Convert.ToString(dr["STANDARD_DISCOUNT"]) != "0")
                        {
                            //dr["STANDARD_DISCOUNT"] = dr["STANDARD_DISCOUNT"]
                        }
                        else
                        {
                            dr["STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));

                        }
                        dr["CLAIM_STANDARD_DISCOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_PER"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString())) - decimal.Parse(_dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())));
                        dr["SED_AMOUNT"] = decimal.Parse(_dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString())) + decimal.Parse(_dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString()));

                        ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                        ObjStandardDiscount += decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        ObjExtraDiscount += decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        ObjTotalSED += decimal.Parse(_dc.chkNull_0(dr["SED_AMOUNT"].ToString()));

                        #region GST Calculation
                        decimal TempAmount = decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                        decimal TempDiscount = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        decimal TempExtraDisAmt = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                        dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(_dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                        dr["GST_AMOUNT2"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(_dc.chkNull_0(dr["GST_RATE2"].ToString())) / 100;
                        dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) + decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT2"].ToString()));
                        ObjTotalGST += decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                        ObjTotalGST2 += decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT2"].ToString()));
                        ObjTotalTST += decimal.Parse(_dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
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
                        ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                    }

                    if (decimal.Parse(_dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) > 0)
                    {
                        ProductStdDist = decimal.Parse(_dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) / (ObjGrossSales - ObjTaxableAmount);
                    }
                    if (decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) > 0)
                    {
                        ProductExtDist = decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) / (ObjGrossSales - ObjTaxableAmount);
                    }
                    ObjGrossSales = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal TempAmount = 0;
                        decimal TempDiscount = 0;
                        decimal TempExtraDisAmt = 0;

                        dr["EXTRA_DISCOUNT"] = ProductExtDist * decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                        dr["STANDARD_DISCOUNT"] = ProductStdDist * decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));

                        dr["SED_AMOUNT"] = ObjClaimAblePer * decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));

                        #region GST Calculation

                        TempAmount = decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                        TempDiscount = decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        TempExtraDisAmt = decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));

                        dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(_dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                        dr["GST_AMOUNT2"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(_dc.chkNull_0(dr["GST_RATE2"].ToString())) / 100;
                        dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) + decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString()));

                        ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["AMOUNT"].ToString()));
                        ObjTotalGST += decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                        ObjTotalGST2 += decimal.Parse(_dc.chkNull_0(dr["GST_AMOUNT2"].ToString()));
                        ObjTotalTST += decimal.Parse(_dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                        ObjTotalSED += decimal.Parse(_dc.chkNull_0(dr["SED_AMOUNT"].ToString()));

                        ObjStandardDiscount += decimal.Parse(_dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                        ObjExtraDiscount += decimal.Parse(_dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
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
                numTxtTotlAmnt.Text = Convert.ToString(Math.Round(ObjGrossSales - ObjExtraDiscount - ObjStandardDiscount + ObjTotalGST + ObjTotalGST2 + ObjTotalTST + ObjFreeTST, 2));

                #endregion

                btnSaveOrder.Enabled = true;
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
            btnSaveOrder.Enabled = true;
        }
        catch (Exception)
        {
           
            throw;
        }
    }
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        #region Customer Limit

        if (int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Credit_Order_Id || int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Advance_PaymentOrder_id)
        {
            CustomerDataController CDC = new CustomerDataController();
            DataTable dt = CDC.SelectCustomerCreditBalance(long.Parse(hfCustomer.Value), int.Parse(hfPrincipal.Value), int.Parse(hfDistributor.Value), int.Parse(RblPayMode.SelectedValue.ToString()));
            decimal NetCashSale = decimal.Parse(numTxtTotlAmnt.Text) - decimal.Parse(_dc.chkNull_0(txtCashReceived.Text));

            if (decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString())) <= NetCashSale)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Credit Limit/Advance Amount is " + _dc.chkNull_0(dt.Rows[0][0].ToString()) + "');", true);
                return;
            }
        }

        #endregion

        bool flag = true;

        #region Stock and Dispatch Checking

        int i = 0;
        int orderQty = 0;
        int recieveQty = 0;

        PurchaseSKU = (DataTable)Session["PurchaseSKU"];

        PhaysicalStockController mController = new PhaysicalStockController();

        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
           
            if (Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text)) > 0)
            {
                orderQty = int.Parse(PurchaseSKU.Rows[i]["QUANTITY_UNIT"].ToString());
                recieveQty = Convert.ToInt32(txtRecQty.Text) + Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text));

                if (recieveQty > orderQty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Dispatch Qty should not be greater than Order Qty.');", true);
                    return;
                }
                DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(hfDistributor.Value), int.Parse(PurchaseSKU.Rows[i]["SKU_ID"].ToString()), "N/A", DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                if (dtstock.Rows.Count > 0)
                {
                    if ((int.Parse(dtstock.Rows[0][0].ToString()) + int.Parse(PurchaseSKU.Rows[i]["PrevRcdQty"].ToString())) < int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + PurchaseSKU.Rows[i]["SKU_NAME"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                        return;
                    }
                    
                }
                else
                {

                    flag = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + PurchaseSKU.Rows[i]["SKU_NAME"].ToString() + "No Stock Found');", true);
                    return;
                }
                
            }
            i += 1;
        }

        #endregion 

        if (flag)//if Stock and dispatch checking is OKK
        {
          
            dtFreeSKU = (DataTable)Session["dtFreeSKU"];

            DateTime PO_DATE = Constants.DateNullValue;
            if (lblDate.Text.Trim().Length > 0)
            {
                PO_DATE = Convert.ToDateTime(lblDate.Text);
            }

            if (PurchaseSKU.Rows.Count > 0)
            {
                #region Insertion 

                if (Session["orderId"] != null)
                {
                    bool IsValidInsert = oeCtrl.AddInvoice(int.Parse(hfDistributor.Value), "PO", 0, int.Parse(hfLocation.Value), int.Parse(hfPrincipal.Value), long.Parse(hfCustomer.Value), long.Parse(hfCustomer.Value), 0, 0, Convert.ToInt64(Session["orderId"]),
                        decimal.Parse(_dc.chkNull_0(txtGrossAmount.Text)), decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotlAmnt.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()),
                        PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()), decimal.Parse(_dc.chkNull_0(txtCashReceived.Text)), DateTime.Parse(Session["CurrentWorkDate"].ToString()), decimal.Parse(_dc.chkNull_0(numTxtTotalTST.Text)), 0, 0, _mCustomerTypeId, lblCustomer.Text, null, PO_DATE, lblOrderNo.Text);

                    if (IsValidInsert)
                    {
                        Session.Remove("orderId");
                        Response.Redirect("frmSaleOrderView.aspx?LevelID" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                    }
                }

                #endregion

                #region Updation
                else
                {
                    var id = Session["invoiceId"].ToString().Split('-');

                    bool IsValidInsert = oeCtrl.Update_Invoice2(Convert.ToInt64(id[1]), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(hfDistributor.Value), "PO", long.Parse(hfCustomer.Value), 0,
                       decimal.Parse(_dc.chkNull_0(txtGrossAmount.Text)), decimal.Parse(_dc.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotalGST.Text)), decimal.Parse(_dc.chkNull_0(numTxtTotlAmnt.Text)), Constants.DecimalNullValue, int.Parse(RblPayMode.SelectedValue.ToString()),
                       PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()),
                       decimal.Parse(_dc.chkNull_0(txtCashReceived.Text)), 
                       decimal.Parse(_dc.chkNull_0(numTxtTotalTST.Text)), 0, 0, 0,
                       Constants.DateNullValue, int.Parse(hfPrincipal.Value),
                       _mCustomerTypeId, lblCustomer.Text, null, PO_DATE, lblOrderNo.Text,
                       Constants.DecimalNullValue, Constants.DecimalNullValue);

                    if (IsValidInsert)
                    {
                        Session.Remove("invoiceId");
                        Response.Redirect("frmSaleOrderView.aspx?LevelID" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                    }
                }
                #endregion

            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Select At least one Product');", true);
            }
        }
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("orderId");
        Session.Remove("invoiceId");

        Response.Redirect("frmSaleOrderView.aspx?LevelID" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());

    }

    #endregion

    #region FreeSKU

    DataTable dtFreeSKU;

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
    
    private void LoadFreeGrid()
    {
        dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
        GrdFreeSKU.DataSource = dtFreeSKU;
        GrdFreeSKU.DataBind();
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

    #endregion

}