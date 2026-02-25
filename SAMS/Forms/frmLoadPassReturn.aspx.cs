using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmLoadPassReturn : System.Web.UI.Page
{
    
    PhaysicalStockController mController = new PhaysicalStockController();
    private static int RowId;
    static long LOADPASS_ID=Constants.LongNullValue;
    static int stock=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["LOADPASS_ID"] != null)
            {
                if (Session["LOADPASS_ID"].ToString() == "")
                { 
                }
                else
                {
                   long p_LOADPASS_ID = long.Parse(this.Session["LOADPASS_ID"].ToString());
                    txtToDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
                    btnSaveOrder.Text = "Update";
                    LoadPendingOrder(p_LOADPASS_ID);
                    drpDocumentNo.SelectedValue = p_LOADPASS_ID.ToString();
                    LoadDATA();
                    DisableUnable(true);
                }
            }
            else            
            {
                Response.Redirect("~/Forms/frmLoadPassEntry.aspx");           
            }          
        }
    }



    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
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
   
    private void LoadPendingOrder(long p_LoadPass_Id)
    {
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        DataTable dtOrder = or.SelectPendingLoadPass(p_LoadPass_Id);
      //  drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtOrder, 0, 1);
        this.Session.Add("dtOrder", dtOrder);
    }
    
    private void ExistenOrderDetail(long OrderId)
    {
        OrderEntryController ord = new OrderEntryController();
       var PurchaseSKU = ord.SelectLoadPassDetail(OrderId);
       // this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird(PurchaseSKU);

    }


    private void LoadGird()
    {
            GrdPurchase.DataSource = null;
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
                            txtToDate.Text = DateTime.Parse(foundRows[0]["Document_date2"].ToString()).ToString("dd-MMM-yyyy");
                        }
                        catch (Exception EX)
                        {
                            DrpCustomer.SelectedIndex = 0;
                        }

                        DrpPrincipal.SelectedValue = foundRows[0]["PRINCIPAL_ID"].ToString();
                        this.LoadDeliveryman();
                        DrpDeliveryMan.SelectedValue = foundRows[0]["DELIVERY_MAN_ID"].ToString();
                        LoadVehicleNO();





                       // LOADPASS_ID = long.Parse(drpDocumentNo.SelectedValue);
                        this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));
                        EnableGrid();
                    }
                }


            }
            else
            {
                LoadGird();
                ClearMasterALL();
                Response.Redirect("~/Forms/frmLoadPassEntry.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
      
            }
        }
        else
        {
            this.LoadGird();
            ClearMasterALL();
            Response.Redirect("~/Forms/frmLoadPassEntry.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
      
        }
    }
    protected void LoadDATA()
    {
        if (btnSaveOrder.Text == "Update")
        {
            DataTable dt = (DataTable)this.Session["dtOrder"];
            if (dt != null)
            {
                DataRow[] foundRows = dt.Select("LOADPASS_ID  = '" + drpDocumentNo.SelectedValue + "'");
                if (foundRows.Length > 0)
                {
                    LoadDistributor();
                    LoadPrincipal();
                    drpDistributor.SelectedValue = foundRows[0]["DISTRIBUTOR_ID"].ToString();
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
                        txtToDate.Text = DateTime.Parse(foundRows[0]["Document_date2"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    catch (Exception EX)
                    {
                        DrpCustomer.SelectedIndex = 0;
                    }

                    DrpPrincipal.SelectedValue = foundRows[0]["PRINCIPAL_ID"].ToString();
                    this.LoadDeliveryman();
                    DrpDeliveryMan.SelectedValue = foundRows[0]["DELIVERY_MAN_ID"].ToString();
                    LoadVehicleNO();

                   // LOADPASS_ID = long.Parse(drpDocumentNo.SelectedValue);
                    this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));
                    EnableGrid();
                }
            }
        }



    }
 
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpRoute.SelectedValue != Constants.IntNullValue.ToString())
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 3, false);

        }
    }
  
    protected void btnSaveOrder_Click(object sender, EventArgs e)
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
        DataTable PurchaseSKU;
        programmaticModalPopup.Hide();
        DataControl DC = new DataControl();

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

        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRETURN_QUANTITY = gvr.FindControl("txtRETURN_QUANTITY") as TextBox;
            TextBox txtSALE_RETURN_QUANTITY = gvr.FindControl("txtSALE_RETURN_QUANTITY") as TextBox;
            TextBox txtPURCHASE_RETURN_QUANTITY = gvr.FindControl("txtPURCHASE_RETURN_QUANTITY") as TextBox;
            CheckBox ChbIsPending = gvr.FindControl("ChbIsPending") as CheckBox;
            if (Convert.ToInt32(DC.chkNull_0(gvr.Cells[5].Text)) >= Convert.ToInt32(DC.chkNull_0(txtRETURN_QUANTITY.Text)))
            {
                DataRow dr = PurchaseSKU.NewRow();

                dr["SKU_ID"] = Convert.ToInt32(DC.chkNull_0(gvr.Cells[0].Text));

                dr["SKU_CODE"] = DC.chkNull_0(gvr.Cells[1].Text);
                dr["SKU_NAME"] = DC.chkNull_0(gvr.Cells[2].Text);
                dr["SKU_RATE"] = DC.chkNull_0(gvr.Cells[3].Text);
                dr["DEMAND_QUANTITY"] = Convert.ToInt32(DC.chkNull_0(gvr.Cells[4].Text));
                dr["ISSUED_QUANTITY"] = gvr.Cells[5].Text;
                dr["RETURN_QUANTITY"] = txtRETURN_QUANTITY.Text == "" ? Constants.IntNullValue : Convert.ToInt32(DC.chkNull_0(txtRETURN_QUANTITY.Text));
                dr["BALANCE_QUANTITY"] = txtRETURN_QUANTITY.Text == "" ? Constants.IntNullValue : Convert.ToInt32(DC.chkNull_0(gvr.Cells[5].Text)) - Convert.ToInt32(DC.chkNull_0(txtRETURN_QUANTITY.Text));
                dr["SALE_RETURN_QUANTITY"] = txtSALE_RETURN_QUANTITY.Text == "" ? Constants.IntNullValue : Convert.ToInt32(DC.chkNull_0(txtSALE_RETURN_QUANTITY.Text));
                dr["PURCHASE_RETURN_QUANTITY"] = txtPURCHASE_RETURN_QUANTITY.Text == "" ? Constants.IntNullValue : Convert.ToInt32(DC.chkNull_0(txtPURCHASE_RETURN_QUANTITY.Text)); 
               
                dr["IS_PENDING"] = ChbIsPending.Checked;
                if (gvr.Cells[12].Text == "&nbsp;" && ChbIsPending.Checked == false && txtRETURN_QUANTITY.Text != "")
                {
                    dr["DOCUMENT_DATE"] = CurrentWorkDate;
                }
                else if ((gvr.Cells[12].Text == "&nbsp;" && ChbIsPending.Checked == true))
                {
                    dr["DOCUMENT_DATE"]  = Constants.DateNullValue;
                }
                else
                {
                    dr["DOCUMENT_DATE"] =gvr.Cells[12].Text=="&nbsp;" ? Constants .DateNullValue : DateTime.Parse(gvr.Cells[12].Text);
                }
                PurchaseSKU.Rows.Add(dr);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Return Qty shuold be less than Issue Qty')", true);
                return;
                break;
                
            }

        }

        OrderEntryController mOrderController = new OrderEntryController();

        if (btnSaveOrder.Text != "Update")
        {
            LOADPASS_ID = Constants.LongNullValue;
        }
        if (GrdPurchase.Rows.Count > 0)
        {
            #region Stock calculation
           // foreach (DataRow dr in PurchaseSKU.Rows)
          //  {
               
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

          //  }
            #endregion
            bool IsValidInsert = mOrderController.UpdateLoadPass(long.Parse(drpDocumentNo.SelectedValue ), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, Constants.IntNullValue, Constants.LongNullValue, PurchaseSKU, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(txtToDate.Text));

            if (IsValidInsert)
            {
                this.ClearMasterALL();
                
                Response.Redirect("~/Forms/frmLoadPassEntry.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz Try again !');", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz enter any Product first');", true);
        }

    }

    private void ClearMasterALL()
    {

      //  this.Session.Remove("PurchaseSKU");
        this.Session.Remove("dtOrder");
        this.Session.Remove("LOADPASS_ID");
        
        this.LoadGird();
        
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      //  this.Session.Remove("PurchaseSKU");
        this.Session.Remove("dtOrder");
       
       this.LoadGird();
       Response.Redirect("~/Forms/frmLoadPassEntry.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
           
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invalid Workind Date.');", true);
            txtToDate.Text = CurrentWorkDate.ToString("dd-MMM-yyyy");
            return;
        }
        else
        {
            this.ClearMasterALL();
        }
    }


    private void DisableUnable(bool Type)
    
    {
        if (Type)
        {
            drpDocumentNo.Enabled = false;
            txtToDate.Enabled = false;
            ImgBntToDate.Enabled = false;

            drpDistributor.Enabled = false;
            DrpPrincipal.Enabled = false;
            DrpRoute.Enabled = false;
            DrpCustomer.Enabled = false;
            DrpVehicleNo.Enabled = false;
            DrpDeliveryMan.Enabled = false;
        }
        else
        {
            drpDocumentNo.Enabled = true ;
            txtToDate.Enabled = true ;
            ImgBntToDate.Enabled = true ;

            drpDistributor.Enabled = true;
            DrpPrincipal.Enabled = true;
            DrpRoute.Enabled = true;
            DrpCustomer.Enabled = true;
            DrpVehicleNo.Enabled = true;
            DrpDeliveryMan.Enabled = true;
        }
    }

    protected void GrdPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            TextBox txtRETURN_QUANTITY = (TextBox)e.Row.FindControl("txtRETURN_QUANTITY");
            CheckBox ChbIsPending = (CheckBox)e.Row.FindControl("ChbIsPending");
            System.Text.StringBuilder atrBalance = new System.Text.StringBuilder();


            atrBalance.Append("CalculateSold(");
            atrBalance.Append(");");

            txtRETURN_QUANTITY.Attributes.Add("onchange", atrBalance.ToString());
            txtRETURN_QUANTITY.Attributes.Add("onblur", atrBalance.ToString());
            ChbIsPending.Attributes.Add("onchange", atrBalance.ToString());

        }
    }

    protected void EnableGrid()
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

        int i = 0;
        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRETURN_QUANTITY = gvr.FindControl("txtRETURN_QUANTITY") as TextBox;
            TextBox txtSALE_RETURN_QUANTITY = gvr.FindControl("txtSALE_RETURN_QUANTITY") as TextBox;
            TextBox txtPURCHASE_RETURN_QUANTITY = gvr.FindControl("txtPURCHASE_RETURN_QUANTITY") as TextBox;
            TextBox txtBALANCE_QUANTITY = gvr.FindControl("txtBALANCE_QUANTITY") as TextBox;
            CheckBox ChbIsPending = gvr.FindControl("ChbIsPending") as CheckBox;
            var Document_date = gvr.Cells[12].Text == "&nbsp;" ? Constants.DateNullValue : DateTime.Parse(gvr.Cells[12].Text);
            if (Document_date != Constants.DateNullValue)
            {
                if (Document_date < CurrentWorkDate&& !ChbIsPending.Checked)
                {
                    ChbIsPending.Enabled = false;
                    txtRETURN_QUANTITY.Enabled = false;
                    txtSALE_RETURN_QUANTITY.Enabled = false;
                    txtPURCHASE_RETURN_QUANTITY.Enabled = false;
                    txtBALANCE_QUANTITY.Enabled = false;
                }
            }
        }
    }
}