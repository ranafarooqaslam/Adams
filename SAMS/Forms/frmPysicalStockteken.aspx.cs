using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;    

public partial class Forms_frmPysicalStockteken : System.Web.UI.Page
{
    DataTable PurchaseSKU;
    DataControl dc = new DataControl();
    private static int RowNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drpDistributor.Focus();
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadSKUDetail();
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
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate);
        clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, 0, 1, true);
    }

    /// <summary>
    /// Loads Document Detail To Document Detail Grid And SKU Detail To ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGird();
        this.LoadSKUDetail();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();
        this.LoadGird();
    }
    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        if (drpPrincipal.Items.Count > 0)
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
            DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 2, CurrentWorkDate);
            clsWebFormUtil.FillDropDownList(this.ddlSKuCde, Dtsku_Price, 0, 9, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
            
        }
    }

    /// <summary>
    ///  Loads Document Detail To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        PhaysicalStockController MController = new PhaysicalStockController();
        DataTable dt = MController.SelectPysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()),0,int.Parse(drpPrincipal.SelectedValue.ToString()));
        GrdPurchase.DataSource = dt;
        GrdPurchase.DataBind();  
    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
        PhaysicalStockController MController = new PhaysicalStockController();
        MController.DELETEPysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), CurrentWorkDate, int.Parse(GrdPurchase.Rows[e.RowIndex].Cells[0].Text));
        this.LoadGird();
    }
            
    /// <summary>
    /// Checks Duplicate SKU in Document Detail Grid
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Saves/Updates Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EvemtArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        DataControl dc = new DataControl();
        if (foundRows.Length > 0)
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

            PhaysicalStockController MController = new PhaysicalStockController();
            if (btnSave.Text == "Save")
            {
                MController.InsertPysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), CurrentWorkDate, int.Parse(foundRows[0]["SKU_ID"].ToString()), int.Parse(dc.chkNull_0(txtQuantity.Text)), int.Parse(dc.chkNull_0(txtusaleableqty.Text)), decimal.Parse(txtUnitRate.Text), 0, int.Parse(drpPrincipal.SelectedValue.ToString()));
            }
            else
            {
                MController.UpdatePysicalStock(int.Parse(drpDistributor.SelectedValue.ToString()), CurrentWorkDate, int.Parse(foundRows[0]["SKU_ID"].ToString()), int.Parse(dc.chkNull_0(txtQuantity.Text)), int.Parse(dc.chkNull_0(txtusaleableqty.Text)), decimal.Parse(txtUnitRate.Text), 0, int.Parse(drpPrincipal.SelectedValue.ToString()));

            }
            this.LoadGird();
            this.ClearAll();
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
        //txtskuCode.Text = "";
        //txtskuName.Text = "";
        txtQuantity.Text = "";
        txtusaleableqty.Text = "";
        txtUnitRate.Text = "0";
        ddlSKuCde.SelectedIndex = 0;
        ddlSKuCde.Enabled = true;
        btnSave.Text = "Save";
    }

    protected void GrdPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowNo = NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[NewEditIndex].Cells[3].Text;
        txtusaleableqty.Text = GrdPurchase.Rows[NewEditIndex].Cells[4].Text;
        txtUnitRate.Text = GrdPurchase.Rows[NewEditIndex].Cells[5].Text;
        txtQuantity.Focus();
        btnSave.Text = "Update Sku";
    }
}