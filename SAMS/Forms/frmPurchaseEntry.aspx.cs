using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseEntry : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    DataControl dc = new DataControl();
    PhaysicalStockController mController = new PhaysicalStockController();
    private static int RowNo;
    
    private static int PrivouseQty, FreePrivousQty;
    DataTable PurchaseSKU;

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
            this.LoadSKUDetail();
            this.CreatTable();
            this.GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            DrpDocumentType_SelectedIndexChanged(null, null);
           
        }
    }
    
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("Quantity", typeof(int));
        PurchaseSKU.Columns.Add("FREE_SKU", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        this.Session.Add("PurchaseSKU", PurchaseSKU);  
    
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
    private bool CalculatePurchase(DateTime MWorkDate)
    {
        decimal mTotalAmount = 0;
        int typeID = 0;
        string Voucher_No = null;
        if (int.Parse(DrpDocumentType.SelectedValue.ToString()) < 0)
        {
            typeID = (int.Parse(DrpDocumentType.SelectedValue.ToString()) * -1);
        }
        else
        {
            typeID = int.Parse(DrpDocumentType.SelectedValue.ToString());
        }
        var mController = new PurchaseController();
        var dtPurchaseDetail = (DataTable)this.Session["PurchaseSKU"];
        foreach (DataRow dr in dtPurchaseDetail.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());

        }        
        if (DrpDocumentType.SelectedIndex == 0)
        {
            // transfer out (shop)          
            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                long DocumentNO = mController.InsertPurchaseDocumentID(int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));
               if (DocumentNO != Constants.LongNullValue)
               {
                   PrintReport(DocumentNO, typeID, DrpDocumentType.SelectedItem .Text );
                   return true;
               }
               else
               {
                   return false; 
               }
               
                
            }
            else
            {
                bool mResult = mController.UpdatePurchaseDocumentTransfor_Out_Shop(int.Parse(drpDocumentNo.SelectedValue), int.Parse(drpDistributor.SelectedValue), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue), int.Parse(drpDistributor.SelectedValue)
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue));

                PrintReport(int.Parse(drpDocumentNo.SelectedValue), typeID, DrpDocumentType.SelectedItem.Text);
                return mResult;
            }
        }
        else if (DrpDocumentType.SelectedIndex == 1)
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                long DocumentNO = mController.InsertPurchaseDocument2ID(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(ddlAccountHead.SelectedValue), ddlAccountHead.SelectedItem.Text, out Voucher_No);
                if (DocumentNO != Constants.LongNullValue)
                {
                    PrintVoucher(Voucher_No);
                    PrintReport(DocumentNO, typeID,DrpDocumentType.SelectedItem.Text);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                bool mResult = mController.UpdatePurchaseDocument2Transfer_Out_Branch(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(ddlAccountHead.SelectedValue), ddlAccountHead.SelectedItem.Text, out Voucher_No);
                PrintReport(int.Parse(drpDocumentNo.SelectedValue), typeID, DrpDocumentType.SelectedItem.Text);
                PrintVoucher(Voucher_No);
                return mResult;
            }
        }
        else if (DrpDocumentType.SelectedIndex == 2)
        {
            //purchase return,Returnable Stock
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {

                long DocumentNO = mController.InsertPurchaseDocument2ID(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), out Voucher_No);
                if (DocumentNO != Constants.LongNullValue)
                {
                    
                    PrintVoucher(Voucher_No);
                    PrintReport(DocumentNO, typeID, DrpDocumentType.SelectedItem.Text);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                bool mResult = mController.UpdatePurchaseDocument2PurchaseReutn(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()),out Voucher_No);
                PrintReport(int.Parse(drpDocumentNo.SelectedValue), typeID, DrpDocumentType.SelectedItem.Text);
                PrintVoucher(Voucher_No);
                return mResult;
            }
        }
        else if (DrpDocumentType.SelectedIndex == 3 || DrpDocumentType.SelectedIndex == 4)
        {
            // transfer in,Returnable Stock Received
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                long DocumentNO = mController.InsertPurchaseDocumentID(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                if (DocumentNO != Constants.LongNullValue)
                {
                    PrintReport(DocumentNO, typeID, DrpDocumentType.SelectedItem.Text);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                bool mResult = mController.UpdatePurchaseDocumentTransfer_In(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                PrintReport(int.Parse(drpDocumentNo.SelectedValue), typeID, DrpDocumentType.SelectedItem.Text);
                return mResult;
            }
        }
        else if (DrpDocumentType.SelectedIndex == 5 || DrpDocumentType.SelectedIndex == 6)
        {
            //Returnable Replace Send
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                long DocumentNO = mController.InsertPurchaseDocumentReturnable(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(ddlAccountHead.SelectedValue), ddlAccountHead.SelectedItem.Text,out Voucher_No);
                if (DocumentNO != Constants.LongNullValue)
                {

                    PrintVoucher(Voucher_No);
                    PrintReport(DocumentNO, typeID, DrpDocumentType.SelectedItem.Text);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                bool mResult = mController.UpdatePurchaseDocumentReturnable(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(ddlAccountHead.SelectedValue), ddlAccountHead.SelectedItem.Text, out Voucher_No);
                PrintReport(int.Parse(drpDocumentNo.SelectedValue), typeID, DrpDocumentType.SelectedItem.Text);
                PrintVoucher(Voucher_No);
                return mResult;
            }
        }
        else
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                long DocumentNO = mController.InsertPurchaseDocumentID(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                if (DocumentNO != Constants.LongNullValue)
                {
                    PrintReport(DocumentNO, typeID, DrpDocumentType.SelectedItem.Text);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                bool mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, typeID
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
                PrintReport(int.Parse(drpDocumentNo.SelectedValue), typeID, DrpDocumentType.SelectedItem.Text);
                return mResult;
            }
        }
    }
    private int CheckStockStatus(int SKU_ID)
    {
        if (DrpDocumentType.SelectedIndex == 2 || DrpDocumentType.SelectedIndex == 3 || DrpDocumentType.SelectedIndex == 4 || DrpDocumentType.SelectedIndex == 5 || DrpDocumentType.SelectedIndex == 6)
        {
            return -1;
        }
        else
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

            PhaysicalStockController mController = new PhaysicalStockController();
            DataTable dt = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue.ToString()), SKU_ID, txtBatchNo.Text, CurrentWorkDate);
            if (dt.Rows.Count > 0)
            {
                if (int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty >= int.Parse(txtQuantity.Text))
                {
                    return -1;
                }
                else
                {
                    return int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty;
                }
            }
        }

        return 0;
    }
    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            DrpDocumentType.Enabled = false;
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
        }
        else
        {
            DrpDocumentType.Enabled = true;
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }
   
    #region Load
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        int type=0;
        if(int.Parse(DrpDocumentType.SelectedValue.ToString())<0)
        {
            type=(int.Parse(DrpDocumentType.SelectedValue.ToString())*-1);
        }
        else
        {
            type=int.Parse(DrpDocumentType.SelectedValue.ToString());
        }
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(type, Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
    }
    /// <summary>
    /// Loads Principals To Principal Comb
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
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, 0, 1, true);
    }
    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2,true);
        Session.Add("dtLocationInfo", dt);
    }
    
    /// <summary>
    /// Loads Locations To Location To Combo
    /// </summary>
    private void LoadToDistributor()
    {
        DrpTransferFor.Items.Clear();
        if (DrpDocumentType.SelectedValue == "15" || DrpDocumentType.SelectedValue == "16")
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue,6, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTransferFor, dt, 0, 2);
        }
        else
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTransferFor, dt, 0, 2);
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            if (DrpDocumentType.SelectedIndex == 0 || DrpDocumentType.SelectedIndex == 1)
            {
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 2 )
            {

                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 3 || DrpDocumentType.SelectedIndex == 4 )
            {
                drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            else if (DrpDocumentType.SelectedIndex == 5 || DrpDocumentType.SelectedIndex == 6)
            {
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            else
            {
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
            }
            PurchaseSKU = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }
        
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        int TotalValue = 0;
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalValue += int.Parse(dr["Quantity"].ToString());

        }
        txtTotalQuantity.Text = TotalValue.ToString(); 
    }
   
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

            DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue.ToString()),
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()),
                Constants.IntNullValue, 1, CurrentWorkDate, Constants.LongNullValue);
            clsWebFormUtil.FillDropDownList(this.ddlSKuCde, Dtsku_Price, 0, 9, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
    }

    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtHead, 0, 11);
    }

    #endregion

    #region Sel/Index Change

    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblAccountHead.Visible = false;
        ddlAccountHead.Visible = false;
        ddlAccountHead.Items.Clear();
        if (DrpDocumentType.SelectedIndex == 0 || DrpDocumentType.SelectedIndex == 1)
        {
            lblfromLocation.Text = "Transfer From";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            LoadToDistributor();
            this.DrpTransferFor.Visible = true;
            Label4.Visible = true;
            Label4.Text = "Transfer To";
            if (DrpDocumentType.SelectedIndex == 1)
            {
                lblAccountHead.Visible = true;
                ddlAccountHead.Visible = true;
                this.LoadAccountDetail();
            }
            this.GetDocumentNo();
        }
        else if (DrpDocumentType.SelectedIndex == 2)
        {
            lbltoLocation.Text = "Principal";
            lblfromLocation.Text = "Return From";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            this.DrpTransferFor.Visible = false;
            Label4.Visible = false;
            this.GetDocumentNo();
        }
        else if (DrpDocumentType.SelectedIndex == 3 || DrpDocumentType.SelectedIndex == 4)
        {
            lblfromLocation.Text = "Transfer to";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            LoadToDistributor();
            this.DrpTransferFor.Visible = true;
            Label4.Visible = true;
            Label4.Text = "Transfer From";
            this.GetDocumentNo();
        }
        else if (DrpDocumentType.SelectedIndex == 5)
        {
            lbltoLocation.Text = "Principal";
            lblfromLocation.Text = "Return From";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            this.LoadToDistributor();
            this.DrpTransferFor.Visible = true;
            Label4.Visible = true;
            Label4.Text = "Distributor";
            lblAccountHead.Visible = true;
            ddlAccountHead.Visible = true;
            this.LoadAccountDetail();
            this.GetDocumentNo();
        }
        else if (DrpDocumentType.SelectedIndex == 6)
        {
            lblfromLocation.Text = "Location";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            this.LoadToDistributor();
            this.DrpTransferFor.Visible = true;
            Label4.Visible = true;
            Label4.Text = "Distributor";
            this.GetDocumentNo();
            lblAccountHead.Visible = true;
            ddlAccountHead.Visible = true;
            this.LoadAccountDetail();
        }
        else
        {
            lbltoLocation.Text = "Principal";
            lblfromLocation.Text = "Location";
            drpDistributor.Enabled = true;
            drpPrincipal.Enabled = true;
            this.DrpTransferFor.Visible = false;
            Label4.Visible = false;
            this.GetDocumentNo();
        }
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTable();
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            this.ClearAll();
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            DrpDocumentType.Enabled = true;
        }
        else
        {
            txtBuiltyNo.Text = "";
            txtDocumentNo.Text = "";
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            DrpDocumentType.Enabled = false;
            this.LoadDocumentDetail();
            this.LoadSKUDetail();
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadToDistributor();
        this.LoadSKUDetail();
    }
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();
    }

    /// <summary>
    /// Enables/Disables Batch No TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbBatchNo_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbBatchNo.Checked == true)
        {
            lblBatchNo.Enabled = true;
            txtBatchNo.Enabled = true;
        }
        else
        {
            txtBatchNo.Text = "N/A"; 
            lblBatchNo.Enabled = false;
            txtBatchNo.Enabled = false;
        }
    }

    /// <summary>
    /// Enables/Disables Apply Free SKU TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbFreeSKU_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbFreeSKU.Checked == true)
        {
            lblFreeSKU.Enabled = true;
            txtFreeSKU.Enabled = true;
        }
        else
        {
            txtFreeSKU.Text = "0";
            lblFreeSKU.Enabled = false;
            txtFreeSKU.Enabled = false;
        }
    }

    #endregion

    #region Grid Operations
    
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.LoadGird();
        }
    }

    #endregion

    #region Click Operations

    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
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
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            decimal mPrice = Convert.ToDecimal(foundRows[0]["DISTRIBUTOR_PRICE"]);
            if(DrpDocumentType.SelectedValue == "15" || DrpDocumentType.SelectedValue == "16")
            {
                mPrice = Convert.ToDecimal(foundRows[0]["TRADE_PRICE"]);
            }
            decimal mStdDiscount = decimal.Parse(dc.chkNull_0(foundRows[0]["DISTRIBUTOR_DISCOUNT"].ToString()));
            decimal mGSTRate = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString()));
            int CurrentStock = CheckStockStatus(int.Parse(dc.chkNull_0(foundRows[0]["SKU_ID"].ToString())));

            #region check Stock Limit
#region purchase Return
            if (DrpDocumentType.SelectedValue == "3" )// Purchase Return
            {
                DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue), int.Parse(ddlSKuCde.SelectedValue.ToString()), "", CurrentWorkDate);
                if (dtstock.Rows.Count > 0)
                {
                    if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
                    {

                        if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dc.chkNull_0(txtQuantity.Text)) + int.Parse(dc.chkNull_0(txtFreeSKU.Text)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + foundRows[0]["SKU_CODE"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                            return;
                        }
                    }
                    else
                    {

                        if ((int.Parse(dtstock.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty) < int.Parse(dc.chkNull_0(txtQuantity.Text)) + int.Parse(dc.chkNull_0(txtFreeSKU.Text)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + foundRows[0]["SKU_CODE"].ToString() + " Current Stock is " + (int.Parse(dtstock.Rows[0][0].ToString()) + FreePrivousQty + PrivouseQty).ToString() + "');", true);
                            return;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + foundRows[0]["SKU_CODE"].ToString() + "No Stock Found');", true);
                    return;
                }
            }
            #endregion
            #region returnable replace dispatch
            if (DrpDocumentType.SelectedValue == "15")//Returnable Replace Dispatch
            {
                DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue),Convert.ToInt32(DrpTransferFor.SelectedValue), int.Parse(ddlSKuCde.SelectedValue.ToString()), "", CurrentWorkDate, 17);
                if (dtstock.Rows.Count > 0)
                {
                    if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
                    {

                        if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dc.chkNull_0(txtQuantity.Text)) + int.Parse(dc.chkNull_0(txtFreeSKU.Text)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + foundRows[0]["SKU_CODE"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                            return;
                        }
                        // further additional check , is returnable stock exists ..or already have dispatch (same sku returnable received- return dispatch till date)
                        if (int.Parse(dtstock.Rows[0][2].ToString()) < int.Parse(dc.chkNull_0(txtQuantity.Text)) + int.Parse(dc.chkNull_0(txtFreeSKU.Text)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + foundRows[0]["SKU_CODE"].ToString() + " Returnable Stock is " + dtstock.Rows[0][2].ToString() + "');", true);
                            return;
                        }
                    }
                    else
                    {

                        if ((int.Parse(dtstock.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty) < int.Parse(dc.chkNull_0(txtQuantity.Text)) + int.Parse(dc.chkNull_0(txtFreeSKU.Text)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + foundRows[0]["SKU_CODE"].ToString() + " Current Stock is " + (int.Parse(dtstock.Rows[0][0].ToString()) + FreePrivousQty + PrivouseQty).ToString() + "');", true);
                            return;
                        }
                        // further additional check , is returnable stock exists ..or already have dispatch (same sku returnable received- return dispatch till date)

                        if ((int.Parse(dtstock.Rows[0][2].ToString()) + PrivouseQty + FreePrivousQty) < int.Parse(dc.chkNull_0(txtQuantity.Text)) + int.Parse(dc.chkNull_0(txtFreeSKU.Text)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + foundRows[0]["SKU_CODE"].ToString() + " Returnable Stock is " + (int.Parse(dtstock.Rows[0][2].ToString()) + FreePrivousQty + PrivouseQty).ToString() + "');", true);
                            return;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + foundRows[0]["SKU_CODE"].ToString() + "No Stock Found');", true);
                    return;
                }
            }
            #endregion
            #endregion

            if (btnSave.Text == "Add Sku")
            {
                if (CheckDublicateSKU())
                {
                    if (CurrentStock == -1)
                    {
                        DataRow dr = PurchaseSKU.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                        dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                        dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                        dr["BATCH_NO"] = txtBatchNo.Text;
                        dr["FREE_SKU"] = int.Parse(dc.chkNull_0(txtFreeSKU.Text));
                        dr["PRICE"] = mPrice;
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                        dr["AMOUNT"] = mPrice * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                        PurchaseSKU.Rows.Add(dr);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Current closing Stock is " + CurrentStock.ToString() + "');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Already Exists ');", true);
                    return;
                }
            }
            else if (btnSave.Text == "Update Sku")
            {
                if (CurrentStock == -1)
                {
                    DataRow dr = PurchaseSKU.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["BATCH_NO"] = txtBatchNo.Text;
                    dr["PRICE"] = mPrice;
                    dr["Quantity"] = int.Parse(txtQuantity.Text);
                    dr["FREE_SKU"] = int.Parse(dc.chkNull_0(txtFreeSKU.Text));
                    dr["AMOUNT"] = mPrice * decimal.Parse(dc.chkNull_0(txtQuantity.Text));

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + "Current closing Stock is " + CurrentStock.ToString() + "');", true);
                    return;
                }
            }
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.ClearAll();
            this.LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong SKU Select');", true);

        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        programmaticModalPopup.Hide();
        if (DrpDocumentType.SelectedIndex == 3 || DrpDocumentType.SelectedIndex == 4 || DrpDocumentType.SelectedIndex == 0 || DrpDocumentType.SelectedIndex == 1)
        {

            if (drpDistributor.SelectedValue.ToString() == DrpTransferFor.SelectedValue.ToString())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Transfer to Location must be different ');", true);
                return;
            }
        }
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
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
            if (CurrentWorkDate != Constants.DateNullValue)
            {
                if (CalculatePurchase(CurrentWorkDate))
                {
                    PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
                    PurchaseSKU.Rows.Clear();
                    this.Session.Add("PurchaseSKU", PurchaseSKU);
                    this.LoadGird();
                    this.GetDocumentNo();
                    drpDistributor.Enabled = true;
                    drpPrincipal.Enabled = true;
                    DrpDocumentType.Enabled = true;
                    this.ClearAll();
                    txtBuiltyNo.Text = "";
                    txtDocumentNo.Text = "";
                    txtDocumentNo.Text = "";
                    DisAbaleOption(false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Try Again');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Day close not found for Location');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' At least one SKU must enter');", true);

        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        this.CreatTable();
        this.LoadGird();
        this.ClearAll();
        txtDocumentNo.Text = "";
        txtBuiltyNo.Text = "";
        txtDocumentNo.Text = ""; 
    }

    #endregion

    private void ClearAll()
    {
       // txtskuCode.Text = "";
        //txtskuName.Text = "";

        ddlSKuCde.SelectedIndex = 0;
        txtQuantity.Text = "";
        txtFreeSKU.Text = "0";
        txtBatchNo.Text = "N/A";
        ddlSKuCde.Enabled = true;

        btnSave.Text = "Add Sku";
        PrivouseQty = 0;
        FreePrivousQty = 0;
    }

    private void PrintReport(long p_Document_ID, int p_typeID,string p_DocumentTitle)
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

        if (p_typeID == 15)
        {
            var mController = new DocumentPrintController();
            var rptInventoryCtl = new RptInventoryController();
            var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument4();
            DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            DataSet ds = rptInventoryCtl.SelectPurchaseDocument(p_Document_ID, int.Parse(drpDistributor.SelectedValue),Constants.IntNullValue, CurrentWorkDate, CurrentWorkDate, p_typeID);
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Document");
            crpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", crpReport);
            Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        }
        else
        {
            var mController = new DocumentPrintController();
            var rptInventoryCtl = new RptInventoryController();
            var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument3();
            var dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
            var ds = rptInventoryCtl.SelectPurchaseDocument(p_Document_ID, int.Parse(drpDistributor.SelectedValue),
                Constants.IntNullValue, CurrentWorkDate, CurrentWorkDate, p_typeID);
            crpReport.SetDataSource(ds);
            crpReport.Refresh();
            crpReport.SetParameterValue("DocumentType", p_DocumentTitle + " Document");
            if (p_typeID == 15 || p_typeID == 16)
            {
                crpReport.SetParameterValue("Principal", "Trade Price");
            }
            else
            {
                crpReport.SetParameterValue("Principal", "Purchase Price");
            }
            crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            this.Session.Add("CrpReport", crpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            Response.Write("<script>window.open(" + url + ",'_blank');</script>");
        }
    }
    
    private void PrintVoucher(string p_Voucher_No)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint2(int.Parse(drpDistributor.SelectedValue), p_Voucher_No, Constants.Journal_Voucher);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());
        this.Session.Add("CrpReport2", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default2.aspx'";
        Response.Write("<script>window.open("+url+",'_blank');</script>");
    }

    protected void GrdPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowNo = NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[NewEditIndex].Cells[3].Text;
        PrivouseQty = int.Parse(GrdPurchase.Rows[NewEditIndex].Cells[3].Text);
        FreePrivousQty = int.Parse(GrdPurchase.Rows[NewEditIndex].Cells[4].Text);
        txtFreeSKU.Text = GrdPurchase.Rows[NewEditIndex].Cells[4].Text;
        txtBatchNo.Text = GrdPurchase.Rows[NewEditIndex].Cells[5].Text;
        ddlSKuCde.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update Sku";
    }
}