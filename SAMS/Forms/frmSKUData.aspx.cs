using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU
/// 
/// MODIFY BY: HASSAN MEHMOOD
/// MODIFY DATE: 2016-04-09
/// ADD SALESTAX_PURCHASE_ID AS INT COLUMN IN SK_ACCOUNTDETAIL TABLE
/// </summary>
public partial class Forms_frmSKUData : System.Web.UI.Page
{

    readonly SkuController mController = new SkuController();
    readonly DataControl dc = new DataControl();
    readonly SkuHierarchyController mHer_Controller = new SkuHierarchyController();
    readonly AccountHeadController MController = new AccountHeadController();

    private  DataTable m_dt,m_SKUDt,dt;
    private static int m_sku_id;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAccountHeads();
            Populate_drpSKUCompany();
            Populate_drpSKUDivisions();
            Populate_drpSKUCategory();
            Populate_drpSKUBrand();
            LoadData();
            LoadGrid();
          
        }
    }

    private void GetAccountHeads()
    {

         dt = MController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue, Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(this.DrpStockInHand, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.DrpConsumption, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.DrpDiscountAllowed, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.DrpDiscountRecieved, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.DrpScheme, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.DrpSaleID, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.DrpSalesTax, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(this.DrpSalesTaxPurchase, dt, 0, 11, true);
    }

    /// <summary>
    /// Loads Principal To Principal Combo
    /// </summary>
    private void Populate_drpSKUCompany()
    {
        m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUPrincipal, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddskuPrincipal, m_dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Divisions To Division Combo, Categories To Category Combo, Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskuPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUDivisions();
        Populate_drpSKUCategory();
        Populate_drpSKUBrand();
        this.LoadData();
        this.LoadGrid();
    }

    /// <summary>
    /// Loads Divisions To Division Combo
    /// </summary>
    private void Populate_drpSKUDivisions()
    {
        if (ddskuPrincipal.Items.Count > 0)
        {
            if (ddskuPrincipal.Items.Count > 0)
            {
                m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, int.Parse(ddskuPrincipal.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.ddskudivision, m_dt, 0, 3, true);
            }
        }
        else
        {
            ddskudivision.Items.Clear();   
        }
    }

    /// <summary>
    /// Loads Categories To Category Combo, Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskudivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUCategory();
        Populate_drpSKUBrand();
        this.LoadData();
        this.LoadGrid();
    }

    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        if (ddskudivision.Items.Count > 0)
        {
            if (ddskudivision.Items.Count   > 0)
            {
                m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, int.Parse(ddskudivision.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.ddskucategory, m_dt, 0, 3, true);
            }
        }
        else
        {
            ddskucategory.Items.Clear();   
        }
    }

    /// <summary>
    /// Loads Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUBrand();
        this.LoadData();
        this.LoadGrid();

    }

    /// <summary>
    /// Loads Brands To Brand Combo
    /// </summary>
    private void Populate_drpSKUBrand()
    {
        if (ddskucategory.Items.Count > 0)
        {
            string strSKUCategoryID = this.ddskucategory.SelectedItem.Value;

            if (ddskucategory.Items.Count   > 0)
            {
                m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, int.Parse(ddskucategory.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.ddskuBrand, m_dt, 0, 3, true);
            }
        }
        else
        {
            ddskuBrand.Items.Clear();   
        }
    }

    /// <summary>
    /// Loads SKU Data To Session
    /// </summary>
    private void LoadData()
    {
        SkuController mSKUController = new SkuController();
        m_SKUDt = mSKUController.SelectSkuInfo(int.Parse(ddskuPrincipal.SelectedValue.ToString()), int.Parse(ddskudivision.SelectedValue.ToString()), int.Parse(ddskucategory.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.Session.Add("m_SKUDt", m_SKUDt);
    }

    /// <summary>
    /// Loads Data From Session To Grid
    /// </summary>
    private void LoadGrid()
    {
        m_SKUDt = (DataTable)this.Session["m_SKUDt"];
        switch (ddSearchType.SelectedIndex)
        {            
            case 1:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 3:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 4:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            default:
                m_SKUDt.DefaultView.RowFilter = "SKU_CODE" + " like '%" + "" + "%'";
                break; 
        }
        grdSKUData.DataSource = m_SKUDt.DefaultView;   
        grdSKUData.DataBind();
       }
       
   /// <summary>
   /// Deletes A SKU
   /// </summary>
   /// <param name="sender">object</param>
   /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSKUData_RowDeleting(object sender, GridViewDeleteEventArgs e)
   {
       string result = mController.DeleteSKU(false, int.Parse(grdSKUData.Rows[e.RowIndex].Cells[4].Text), int.Parse(this.Session["UserId"].ToString()));
       this.LoadData();
       this.LoadGrid();
   }

   /// <summary>
   /// Sets PageIndex Of SKU Grid
   /// </summary>
   /// <param name="sender">object</param>
   /// <param name="e">GridViewPageEventArgs</param>
    protected void grdSKUData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSKUData.PageIndex = e.NewPageIndex;
        this.LoadGrid();

    }

   /// <summary>
   /// Save Or Updates an SKU
   /// </summary>
   /// <param name="sender">object</param>
   /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
       bool IsExemted = true;
       
       if (txtpacksize.Text.Length <= 0)
       {
           lblErrorMsg.Text = "Must Enter SKU Packsize";
           return; 
       }
       if (txtskucode.Text.Length <= 0)
       {
           lblErrorMsg.Text = "Must Enter SKU Code";
           return;
       }
       if (txtskuname.Text.Length <= 0)
       {
           lblErrorMsg.Text = "Must Enter SKU Name";
           return;
       }
        try
        {
            if (btnSave.Text == "Save")
            {
                mController.InsertSKUS(IsExemted, cbActive.Checked, char.Parse(DrpSKUTaxType.SelectedValue.ToString()), int.Parse(ddskuPrincipal.SelectedValue.ToString()), int.Parse(ddskudivision.SelectedValue.ToString()), int.Parse(ddskucategory.SelectedValue.ToString()),
                int.Parse(ddskuBrand.SelectedValue.ToString()), Constants.IntNullValue, 0, 0, short.Parse(dc.chkNull_0(txtunitincase.Text)),
                txtskucode.Text.ToUpper().Trim(), txtskuname.Text.Trim(), null, txtpacksize.Text.Trim(), int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString())
                , long.Parse(DrpStockInHand.SelectedValue), long.Parse(DrpConsumption.SelectedValue), long.Parse(DrpDiscountAllowed.SelectedValue), long.Parse(DrpDiscountRecieved.SelectedValue), long.Parse(DrpScheme.SelectedValue)
                , long.Parse(DrpSaleID.SelectedValue), Convert.ToInt32(DrpSalesTax.SelectedValue),
                Convert.ToInt32(DrpSalesTaxPurchase.SelectedValue), Server.HtmlDecode(txtBarCode.Text));
                this.CLearAll();
            }
            else if (btnSave.Text == "Update")
            {
                mController.UpdateSKUS(IsExemted, cbActive.Checked, char.Parse(DrpSKUTaxType.SelectedValue.ToString()), int.Parse(ddskuPrincipal.SelectedValue.ToString()), int.Parse(ddskudivision.SelectedValue.ToString()),
                int.Parse(ddskucategory.SelectedValue.ToString()), int.Parse(ddskuBrand.SelectedValue.ToString()), Constants.IntNullValue, 0
                , 0, short.Parse(dc.chkNull_0(txtunitincase.Text)), m_sku_id, txtskucode.Text.ToUpper().Trim(), txtskuname.Text.Trim(), null, txtpacksize.Text.Trim()
                , int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString())
                , long.Parse(DrpStockInHand.SelectedValue), long.Parse(DrpConsumption.SelectedValue), long.Parse(DrpDiscountAllowed.SelectedValue)
                , long.Parse(DrpDiscountRecieved.SelectedValue), long.Parse(DrpScheme.SelectedValue), long.Parse(DrpSaleID.SelectedValue)
                , Convert.ToInt32(DrpSalesTax.SelectedValue),
                Convert.ToInt32(DrpSalesTaxPurchase.SelectedValue), Server.HtmlDecode(txtBarCode.Text));
                CLearAll();
            }
            LoadData();
        }
        catch (Exception)
        {

            throw;
        } 
        
    }
  
    /// <summary>
    /// Filters SKU From SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid();
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void CLearAll()
    {
        txtpacksize.Text = "";
        txtskucode.Text = "";
        txtunitincase.Text = "";
        txtBarCode.Text = "";
        txtskuname.Text = "";

        btnSave.Text = "Save";

        LoadData();
        LoadGrid();
        GetAccountHeads();
    }

    protected void grdSKUData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        ddskuPrincipal.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[0].Text;
        this.Populate_drpSKUDivisions();
        ddskudivision.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[1].Text;
        this.Populate_drpSKUCategory();
        ddskucategory.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[2].Text;
        this.Populate_drpSKUBrand();
        ddskuBrand.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[3].Text;
        m_sku_id = int.Parse(grdSKUData.Rows[NewEditIndex].Cells[4].Text);
        txtskucode.Text = grdSKUData.Rows[NewEditIndex].Cells[9].Text;
        txtskuname.Text = grdSKUData.Rows[NewEditIndex].Cells[10].Text;
        txtpacksize.Text = grdSKUData.Rows[NewEditIndex].Cells[11].Text;
        txtunitincase.Text = grdSKUData.Rows[NewEditIndex].Cells[12].Text;
        txtBarCode.Text = Server.HtmlDecode(grdSKUData.Rows[NewEditIndex].Cells[23].Text);

        DrpSKUTaxType.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[13].Text.Trim();
        if (grdSKUData.Rows[NewEditIndex].Cells[14].Text != "&nbsp;")
        {
            DrpStockInHand.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[14].Text;
        }
        else
        {
            DrpStockInHand.SelectedIndex = 0;
        }
        if (grdSKUData.Rows[NewEditIndex].Cells[15].Text != "&nbsp;")
        {
            DrpConsumption.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[15].Text;
        }
        else
        {
            DrpConsumption.SelectedIndex = 0;
        }
        if (grdSKUData.Rows[NewEditIndex].Cells[16].Text != "&nbsp;")
        {
            DrpDiscountAllowed.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[16].Text;
        }
        else
        {
            DrpDiscountAllowed.SelectedIndex = 0;
        }
        if (grdSKUData.Rows[NewEditIndex].Cells[17].Text != "&nbsp;")
        {
            DrpDiscountRecieved.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[17].Text;
        }
        else
        {
            DrpDiscountRecieved.SelectedIndex = 0;
        }
        if (grdSKUData.Rows[NewEditIndex].Cells[18].Text != "&nbsp;")
        {
            DrpScheme.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[18].Text;
        }
        else
        {
            DrpScheme.SelectedIndex = 0;
        }
        if (grdSKUData.Rows[NewEditIndex].Cells[19].Text != "&nbsp;")
        {
            DrpSaleID.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[19].Text;
        }
        else
        {
            DrpSaleID.SelectedIndex = 0;
        }
        if (grdSKUData.Rows[NewEditIndex].Cells[20].Text != "&nbsp;" && grdSKUData.Rows[NewEditIndex].Cells[20].Text != "")
        {
            DrpSalesTax.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[20].Text;
        }
        else
        {
            DrpSalesTax.SelectedIndex = 0;
        }
        if (grdSKUData.Rows[NewEditIndex].Cells[21].Text != "&nbsp;" && grdSKUData.Rows[NewEditIndex].Cells[21].Text != "")
        {
            DrpSalesTaxPurchase.SelectedValue = grdSKUData.Rows[NewEditIndex].Cells[21].Text;
        }
        else
        {
            DrpSalesTaxPurchase.SelectedIndex = 0;
        }
        if(grdSKUData.Rows[NewEditIndex].Cells[22].Text == "Active")
        {
            cbActive.Checked = true;
        }
        else
        {
            cbActive.Checked = false;
        }
        btnSave.Text = "Update";
    }
}
