using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Rollback Order, Invoice, Sale Return And Realized Cheque
/// </summary>
public partial class Forms_frmRollBackForm : System.Web.UI.Page
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
            this.LoadPrincipal();
            this.LoadOrderBooker();
            this.LoadLegend();
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
    /// Loads Principals To Principal Comob
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
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate);
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }
    
    /// <summary>
    /// Loads Legends To Legend Combo
    /// </summary>
    private void LoadLegend()
    {
        OrderEntryController or = new OrderEntryController();
        DataTable m_dt = or.SelectLegend(); 
        clsWebFormUtil.FillDropDownList(this.DrpLenged, m_dt, 0, 2, true);
    }
    
    /// <summary>
    /// Load OrderBookers To OrderBooker Combo
    /// </summary>
    private void LoadOrderBooker()
    {
        if (drpDistributor.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectRollBackInvoiceSaleForce(int.Parse(DrpDocumentType.SelectedValue.ToString()),int.Parse(DrpPrincipal.SelectedValue.ToString()),int.Parse(drpDistributor.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpOrderBooker, m_dt, 0, 1, true);
        }
        else
        {
            DrpOrderBooker.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads Rollback Order, Invoice And Sale Return Data To Grid
    /// </summary>
    private void LoadRollbackDocument()
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
        OrderEntryController or = new OrderEntryController();
        DataTable dtOrder = or.SelectRollBackDocument(int.Parse(drpDistributor.SelectedValue.ToString()),int.Parse(DrpPrincipal.SelectedValue.ToString()),
            int.Parse(DrpOrderBooker.SelectedValue.ToString()), int.Parse(DrpDocumentType.SelectedValue.ToString()),CurrentWorkDate);
        GrdOrder.DataSource = dtOrder;
        GrdOrder.DataBind();
    }

    private void LoadRollbackDocument2()//For Purchase
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
        OrderEntryController or = new OrderEntryController();
        DataTable dtOrder = or.SelectRollBackDocument(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
            int.Parse(DrpOrderBooker.SelectedValue.ToString()), int.Parse(DrpDocumentType.SelectedValue.ToString()),CurrentWorkDate);
        GrdPurchase.DataSource = dtOrder;
        GrdPurchase.DataBind();
    }
    
    /// <summary>
    /// Loads Rollback Cheques Data To Grid
    /// </summary>
    private void LoadRollbackCheque()
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
        ChequeEntryController CController = new ChequeEntryController();
        DataTable dt = CController.SelectChequeEntry(Constants.Cheque_Clear ,CurrentWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),0);
        GrdCheque.DataSource = dt;
        GrdCheque.DataBind();       
    }
    
    /// <summary>
    /// Loads OrderBookers To OrderBooker Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    //protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.LoadOrderBooker();
    //}

    /// <summary>
    /// Loads OrderBookers To OrderBooker Combo And Principals To Principal Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadPrincipal();
        this.LoadOrderBooker();         
    }

    /// <summary>
    /// Rollbacks Order, Invoice, Sale Return And Realized Cheque
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPost_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        if (DrpDocumentType.SelectedIndex == 3)
        {                                       //Realized Cheque
            foreach (GridViewRow dr in GrdCheque.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                if (ChbInvoice.Checked == true)
                {
                    ChequeEntryController CController = new ChequeEntryController();
                    DataControl dc = new DataControl();
                    CController.RollbackChequeEntry(int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[6].Text, long.Parse(dr.Cells[0].Text), Constants.LongNullValue, Convert.ToDecimal(dc.chkNull_0(dr.Cells[8].Text)));
                }
            }
            LoadRollbackCheque();
        }
        else if (DrpDocumentType.SelectedIndex == 4)
        {                                       //Purchase
            foreach (GridViewRow dr in GrdPurchase.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                if (ChbInvoice.Checked == true)
                {
                    OrderEntryController ORD = new OrderEntryController();
                    DataControl dc = new DataControl();
                    ORD.UpdateRollBackDocument(Convert.ToInt64(GrdPurchase.DataKeys[dr.RowIndex].Values["Document_ID"]), int.Parse(DrpDocumentType.SelectedValue.ToString()), int.Parse(DrpLenged.SelectedValue.ToString()));
                }
            }
            this.LoadRollbackDocument2();
        }
        else
        {
            foreach (GridViewRow dr in GrdOrder.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("ChbInvoice");
                if (ChbInvoice.Checked == true)
                {
                    OrderEntryController ORD = new OrderEntryController();
                    DataControl dc = new DataControl();
                    ORD.UpdateRollBackDocument(Convert.ToInt64(GrdOrder.DataKeys[dr.RowIndex].Values["Document_ID"]), int.Parse(DrpDocumentType.SelectedValue.ToString()), int.Parse(DrpLenged.SelectedValue.ToString()));
                }
            }
            this.LoadRollbackDocument();
        }
    }

    /// <summary>
    /// Loads Order, Invoice, Sale Return And Realized Cheque Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnGetOrder_Click(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedIndex == 3)
        {
            this.LoadRollbackCheque();
            GrdOrder.Visible = false;
            GrdPurchase.Visible = false;
            GrdCheque.Visible = true;
        }
        else if (DrpDocumentType.SelectedIndex == 4)
        {
            GrdPurchase.Visible = true;
            GrdOrder.Visible = false;
            GrdCheque.Visible = false;
            this.LoadRollbackDocument2();
        }
        else
        {
            GrdOrder.Visible = true;
            GrdCheque.Visible = false;
            this.LoadRollbackDocument();
        }
    }
    /// <summary>
    /// Loads OrderBookers To OrderBooker Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrderBooker();
    }
}