using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmDistributorCustomer : System.Web.UI.Page
{
    DataControl dc = new DataControl();

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
            this.LoadTown();
            this.LoadRoute();
            this.LoadMarket();
            this.LoadChannelType();
            this.LoadBusinessType();
            this.LoadGroup();
            this.LoadPromotionClass();
            this.LoadTaxSlab();
            this.LoadTaxSlabType();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            btnSearch.Attributes.Add("onclick", "return SearchRecord()");
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtRegdate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadGroup()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectGroup(1);
        drpPriceGroup.Items.Add(new clsListItems("Default", "0"));
        clsWebFormUtil.FillDropDownList(drpPriceGroup, dt, 0, 1, false);
    }
    private void LoadTaxSlab()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectTaxSlab(1);
        clsWebFormUtil.FillDropDownList(DrpTaxSlab, dt, 0, 1, false);
    }
    private void LoadTaxSlabType()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectTaxSlab(2);
        clsWebFormUtil.FillDropDownList(DrpSlabType, dt, 0, 1, false);
    }

    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Towns To Town Combo, Routes To Routes Comb And Markets To Market Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTown();
        this.LoadRoute();
        this.LoadMarket();
        this.SetTableSorter();
    }

    /// <summary>
    /// Loads Towns To Town Combo
    /// </summary>
    protected void LoadTown()
    {
        if (DrpDistributor.Items.Count > 0)
        {
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(int.Parse(DrpDistributor.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpTown, dt, 0, 1, true);
        }
    }

    /// <summary>
    /// Loads Routes To Routes Comb And Markets To Market Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadRoute();
        this.LoadMarket();
        this.SetTableSorter();
    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
    {
        if (DrpDistributor.Items.Count > 0 && DrpTown.Items.Count > 0)
        {
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(DrpTown.SelectedValue.ToString()), null, null);
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        }
    }

    /// <summary>
    /// Loads Markets To Market Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadMarket();
        this.SetTableSorter();
    }

    /// <summary>
    /// Loads Markets To Market Combo
    /// </summary>
    private void LoadMarket()
    {
        if (DrpDistributor.Items.Count > 0 && DrpTown.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            DistributorRouteController gController = new DistributorRouteController();
            DataTable dt = gController.SelectDistributorRoute(Constants.LongNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(DrpTown.SelectedValue.ToString()), long.Parse(DrpRoute.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpMarket, dt, 0, 8, true);
        }
        else
        {
            DrpMarket.Items.Clear();
        }
    }

    /// <summary>
    /// Loads Channel Types To Channel Type Combo
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(drpChannelType, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Business Types To Business Type Combo
    /// </summary>
    private void LoadBusinessType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerTypeBusiness, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(DrpBusinessType, dt, 0, 2, true);
    }
    
    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    private void LoadCustomer()
    {
        if (DrpDistributor.Items.Count > 0 && DrpTown.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpMarket.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.UspSelectCustomer(int.Parse(DrpDistributor.SelectedValue.ToString()), ddSearchType.SelectedValue.ToString(), txtSeach.Text);
            this.Grid_users.DataSource = dt;
            this.Grid_users.DataBind();
        }
    }

    private void LoadPromotionClass()
    {
        try
        {
            UserController UserInfo = new UserController();
            DataTable dtUser = UserInfo.SelectSlashUser(Convert.ToInt32(Session["UserID"].ToString()));
            lblPromotion.Visible = false;
            ddlPromotionClass.Visible = false;
            if (dtUser.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtUser.Rows[0]["CAN_EDIT_PROMOTION"]))
                {
                    SLASHCodesController mController = new SLASHCodesController();
                    DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerVolumeClassType, null, Constants.IntNullValue, bool.Parse("True"));
                    clsWebFormUtil.FillDropDownList(ddlPromotionClass, dt, 0, 2);
                    this.ddlPromotionClass.SelectedValue = "88";
                    lblPromotion.Visible = true;
                    ddlPromotionClass.Visible = true;
                }
            }            
        }
        catch (Exception ex)
        {
            
        }        
    }
    
    /// <summary>
    /// Sets PageIndex Of Customer Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void Grid_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_users.PageIndex = e.NewPageIndex;
        this.LoadCustomer();
        this.SetTableSorter();
    }

    /// <summary>
    /// Enables GST No TextBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbIsRegister_CheckedChanged(object sender, EventArgs e)
    {
        if (ChbIsRegister.Checked == true)
        {
            txtIsRegister.Text = "";
            txtIsRegister.Enabled = true;
        }
        else
        {
            txtIsRegister.Text = "";
            txtIsRegister.Enabled = false;
        }
        this.SetTableSorter();
    }

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int PromotionClass = 88;
        CustomerDataController mController = new CustomerDataController();
        string Mesg ="Please Try Again !";
        var slabType = Constants.IntNullValue;
        if (DrpSlabType.Enabled == true)
        {
            slabType = int.Parse(DrpSlabType.SelectedValue);
        }

        if (btnSave.Text == "Save")
        {
            SETTINGS_TABLE_Controller mSettingsTableControl = new SETTINGS_TABLE_Controller();
            DataTable dtSettingsTable = mSettingsTableControl.Select_SETTINGS_TABLE("CUSTOMER", "CUSTOMER_ID", int.Parse(DrpDistributor.SelectedValue.ToString()));

            if (dtSettingsTable.Rows.Count > 0)
            {
                long CustomerId = long.Parse(dtSettingsTable.Rows[0]["Value"].ToString()) + 1;
                string StrCode = "";


                if (CustomerId.ToString().Length == 1)
                {
                    StrCode = "OT0000" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 2)
                {
                    StrCode = "OT000" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 3)
                {
                    StrCode = "OT00" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 4)
                {
                    StrCode = "OT0" + CustomerId.ToString();
                }
                else if (CustomerId.ToString().Length == 5)
                {
                    StrCode = "OT" + CustomerId.ToString();
                }                
                if(ddlPromotionClass.Visible)
                {
                    PromotionClass = Convert.ToInt32(ddlPromotionClass.SelectedValue);
                }
             Mesg=   mController.InsertCustomer(CustomerId, ChbIsRegister.Checked, chkIsActive.Checked,
                int.Parse(drpChannelType.SelectedValue.ToString()), PromotionClass,int.Parse(DrpBusinessType.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(DrpMarket.SelectedValue.ToString()),
                int.Parse(DrpTown.SelectedValue.ToString()), int.Parse(DrpDistributor.SelectedValue.ToString()), txtIsRegister.Text, txtContactPerson.Text,txtPhoneNo.Text, "", StrCode, txtCustomerName.Text, txtAddress.Text, DateTime.Parse(txtRegdate.Text),
                1, int.Parse(drpPriceGroup .SelectedValue .ToString ()), txtCNIC.Text,
                txtNTN.Text,0, Server.HtmlDecode(txtLat.Text.Trim()),
                Server.HtmlDecode(txtLong.Text.Trim()), int.Parse(DrpTaxSlab.SelectedValue), slabType);

                this.Session.Add("CustomerId", CustomerId);
            }
        }
        else
        {
            if(ddlPromotionClass.Visible)
            {
                PromotionClass = Convert.ToInt32(ddlPromotionClass.SelectedValue);
            }
            else
            {
                PromotionClass = Constants.IntNullValue;
            }
          Mesg=  mController.UpdateCustomer(long.Parse(this.Session["CustomerId"].ToString()), ChbIsRegister.Checked, chkIsActive.Checked,
            int.Parse(drpChannelType.SelectedValue.ToString()), PromotionClass,int.Parse(DrpBusinessType.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString())
            , int.Parse(DrpMarket.SelectedValue.ToString()),int.Parse(DrpTown.SelectedValue.ToString()), int.Parse(DrpDistributor.SelectedValue.ToString()), txtIsRegister.Text, txtContactPerson.Text,
            txtPhoneNo.Text, "", null, txtCustomerName.Text, txtAddress.Text, Constants.DateNullValue,
            1, int.Parse (drpPriceGroup .SelectedValue .ToString ()),
            txtCNIC.Text, txtNTN.Text,0, Server.HtmlDecode(txtLat.Text.Trim()),
            Server.HtmlDecode(txtLong.Text.Trim()), int.Parse(DrpTaxSlab.SelectedValue), slabType);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + Mesg + "');", true);
        this.ClearAll();
    }

    /// <summary>
    /// Clears All Controls Through ClearAll() Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    /// <summary>
    /// Filters Customer From Customer Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.LoadCustomer();
        this.SetTableSorter();
    }

    /// <summary>
    /// Set Customer Grid For JQuery Sorting
    /// </summary>
    private void SetTableSorter()
    {
        if (Grid_users.Rows.Count > 1)
        {
            Grid_users.UseAccessibleHeader = true;
            Grid_users.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    /// <summary>
    /// Clears All  Constrols
    /// </summary>
    private void ClearAll()
    {
        txtCustomerName.Text = "";
        txtContactPerson.Text = "";
        txtAddress.Text = "";
        txtPhoneNo.Text = "";
        txtSeach.Text = "";
        txtIsRegister.Text = "";
        txtNTN.Text = string.Empty;
        txtLat.Text = "";
        txtLong.Text = "";
        txtCNIC.Text = string.Empty;
        btnSave.Text = "Save";
        Grid_users.DataSource = null;
        Grid_users.DataBind();

    }

    protected void Grid_users_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;

        if (Grid_users.Rows[NewEditIndex].Cells[28].Text == "True")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Customer Is Shifted to Other Location, can not be modified');", true);
            return;
        }

        this.Session.Add("CustomerId", long.Parse(Grid_users.Rows[NewEditIndex].Cells[0].Text));
        DrpDistributor.SelectedValue = Grid_users.Rows[NewEditIndex].Cells[1].Text;
        DrpBusinessType.SelectedValue = Grid_users.Rows[NewEditIndex].Cells[2].Text;
        if (ddlPromotionClass.Visible)
        {
            ddlPromotionClass.SelectedValue = Grid_users.Rows[NewEditIndex].Cells[3].Text;
        }
        drpChannelType.SelectedValue = Grid_users.Rows[NewEditIndex].Cells[4].Text;
        this.LoadTown();
        DrpTown.SelectedValue = Grid_users.Rows[NewEditIndex].Cells[5].Text;
        this.LoadRoute();
        DrpRoute.SelectedValue = Grid_users.Rows[NewEditIndex].Cells[6].Text;
        this.LoadMarket();
        DrpMarket.SelectedValue = Grid_users.Rows[NewEditIndex].Cells[7].Text;
        txtCustomerName.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[9].Text)).Trim().Replace("&nbsp", "");
        txtContactPerson.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[10].Text)).Trim().Replace("&nbsp", "");
        txtPhoneNo.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[11].Text)).Trim().Replace("&nbsp", "");
        txtAddress.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[13].Text)).Trim().Replace("&nbsp", "");
        var registerTxt = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[14].Text)).Trim().Replace("&nbsp", "");
        txtIsRegister.Text = registerTxt.Replace("&nbsp;", "");

        if (string.IsNullOrEmpty(registerTxt) || registerTxt == "&nbsp;")
        {
            txtIsRegister.Text = "";
            ChbIsRegister.Checked = false;
        }
        else
        {
            ChbIsRegister.Checked = true;
            txtIsRegister.Text = registerTxt.Replace("&nbsp;", "");
        }
        chkIsActive.Checked = bool.Parse(Grid_users.Rows[NewEditIndex].Cells[19].Text);
        txtRegdate.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[20].Text)).Trim().Replace("&nbsp", "");
        txtCNIC.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[23].Text)).Trim().Replace("&nbsp", "");
        txtNTN.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[24].Text)).Trim().Replace("&nbsp", "");
        txtLat.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[26].Text)).Trim().Replace("&nbsp", "");
        txtLong.Text = Server.HtmlDecode(Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[27].Text)).Trim().Replace("&nbsp", "");
        string taxslabId = Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[29].Text);
        string slabTypeId = Server.HtmlDecode(Grid_users.Rows[NewEditIndex].Cells[30].Text);

        if (!string.IsNullOrEmpty(taxslabId) && taxslabId != "0")
        {
            DrpTaxSlab.SelectedValue = taxslabId;
        }

        if (!string.IsNullOrEmpty(slabTypeId) && slabTypeId != "0")
        {
            DrpSlabType.SelectedValue = slabTypeId;
        }
        else
        {
            DrpSlabType.SelectedIndex = -1;
            DrpSlabType.Enabled = false;
        }

        if (DrpTaxSlab.SelectedItem.Text == "Not Applicable")
        {
            DrpSlabType.SelectedIndex = -1;
            DrpSlabType.Enabled = false;
        }

        try
        {
            drpPriceGroup.SelectedValue = Grid_users.Rows[NewEditIndex].Cells[22].Text.Replace("&nbsp;", "0");
        }
        catch
        {
            drpPriceGroup.SelectedIndex = 0;
        }
        btnSave.Text = "Update";
        this.SetTableSorter();
    }

    protected void DrpTaxSlab_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpTaxSlab.SelectedItem.Text == "Not Applicable")
        {
            DrpSlabType.SelectedIndex = -1;
            DrpSlabType.Enabled = false;
        }
        else
        {
            DrpSlabType.SelectedIndex = 0;
            DrpSlabType.Enabled = true;
        }
    }
}