using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// From to Add, Edit, Delete Geo Heirarical Data
/// </summary>
public partial class Forms_frmGeoHierarchyData : System.Web.UI.Page
{
    GeoHierarchyController GControler = new GeoHierarchyController();
    private static int RegionId, ZoneId, TerritoryId, TownId;

    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadRegion();
            this.LoadZone();
            this.LoadTerritoryZone();
            this.LoadTerritory();
            this.LoadTownZone();
            this.LoadTownTerritory();
            this.LoadTown();
            //AppMaster master = new AppMaster();
            //master = (AppMaster)this.Master;
            //Panel panel = new Panel();
            //panel = master.FindControl("searchpanel") as Panel;
            //panel.Visible = true;
            SAMSCommon.Classes.Configuration.DistributorId = 1;
            btnRegionSave.Attributes.Add("onclick", "return ValidatRegion()");
            btnSaveZone.Attributes.Add("onclick", "return ValidatZone()");
            btnSaveTerritory.Attributes.Add("onclick", "return ValidateTerritory()");
            btnSaveTown.Attributes.Add("onclick", "return ValidateTown()");

        }
    }
    
    #region Region Tab

    /// <summary>
    /// Loads Existing Regions To All Region Combos And Region Grid Of Region Tab.
    /// </summary>
    protected void LoadRegion()
    {
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpRegion, dt, 0, 4, true);
        clsWebFormUtil.FillDropDownList(this.drpRegionTerritory, dt, 0, 4, true);
        clsWebFormUtil.FillDropDownList(this.DrpTownRegion, dt, 0, 4, true);
        grdRegionData.DataSource = dt;
        grdRegionData.DataBind();
    }
    protected void grdRegionData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        RegionId = int.Parse(grdRegionData.Rows[gvr.RowIndex].Cells[0].Text);
        txtRegionCode.Text = grdRegionData.Rows[gvr.RowIndex].Cells[1].Text;
        txtRegionName.Text = grdRegionData.Rows[gvr.RowIndex].Cells[2].Text;
        ChIsActive.Checked = bool.Parse(grdRegionData.Rows[gvr.RowIndex].Cells[3].Text);
        btnRegionSave.Text = "Update";
    }

    /// <summary>
    /// Saves Or Updates A Region.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnRegionSave_Click(object sender, EventArgs e)
    {
        if (btnRegionSave.Text == "Save")
        {
            string InsertValue = GControler.InsertHierarchy(Constants.IntNullValue, txtRegionCode.Text, txtRegionName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Region, int.Parse(this.Session["UserId"].ToString()), SAMSCommon.Classes.Configuration.DistributorId, null);
        }
        else
        {
            if (ChIsActive.Checked == false)
            {
                DataTable dt = GControler.SelectDistributorHierachyWithType(1, RegionId, null);
                if (dt.Rows.Count > 0)
                {
                    lblErrorMsg.Text = "An Active Zone Exists...";
                    return;
                }
            }
            GControler.UpdateHierarchy(RegionId, Constants.IntNullValue, txtRegionCode.Text, txtRegionName.Text, true, ChIsActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Region, int.Parse(this.Session["UserId"].ToString()), SAMSCommon.Classes.Configuration.DistributorId, null);
        }
        this.LoadRegion();
        txtRegionCode.Text = "";
        txtRegionName.Text = "";
        btnRegionSave.Text = "Save";
        lblErrorMsg.Text = "";
    }

    #endregion

    #region Zone Tab

    /// <summary>
    /// Loads Existing Zones To Zone Grid of Zone Tab
    /// </summary>
    protected void LoadZone()
    {
        if (DrpRegion.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            grdZoneData.DataSource = dt;
            grdZoneData.DataBind();
        }
    }

    /// <summary>
    /// Loads Zones To Zone Grid Of Zone Tab.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadZone();
    }
    protected void grdZoneData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        ZoneId = int.Parse(grdZoneData.Rows[gvr.RowIndex].Cells[1].Text);
        txtZoneCode.Text = grdZoneData.Rows[gvr.RowIndex].Cells[2].Text;
        txtZoneName.Text = grdZoneData.Rows[gvr.RowIndex].Cells[3].Text;
        chbIsZoneActive.Checked = bool.Parse(grdZoneData.Rows[gvr.RowIndex].Cells[4].Text);
        btnSaveZone.Text = "Update";
    }

    /// <summary>
    /// Saves Or Updates A Zone.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveZone_Click(object sender, EventArgs e)
    {
        if (btnSaveZone.Text == "Save")
        {
            string InsertValue = GControler.InsertHierarchy(int.Parse(DrpRegion.SelectedValue.ToString()), txtZoneCode.Text, txtZoneName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Zone, int.Parse(this.Session["UserId"].ToString()), SAMSCommon.Classes.Configuration.DistributorId, null);
        }
        else
        {
            if (chbIsZoneActive.Checked == false)
            {
                DataTable dt = GControler.SelectDistributorHierachyWithType(2, int.Parse(DrpRegion.SelectedValue.ToString()), ZoneId, Constants.IntNullValue);
                if (dt.Rows.Count > 0)
                {
                    lblErrorMsgDivsion.Text = "An Active Territory Exists...";
                    return;
                }
            }
            GControler.UpdateHierarchy(ZoneId, int.Parse(DrpRegion.SelectedValue.ToString()), txtZoneCode.Text, txtZoneName.Text, true, chbIsZoneActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Zone, int.Parse(this.Session["UserId"].ToString()), SAMSCommon.Classes.Configuration.DistributorId, null);
        }

        this.LoadZone();
        this.LoadTerritoryZone();
        this.LoadTownZone();
        txtZoneCode.Text = "";
        txtZoneName.Text = "";
        btnSaveZone.Text = "Save";
        lblErrorMsgDivsion.Text = "";
    }

    #endregion

    #region Territory Tab

    /// <summary>
    /// Loads Existing Territories To Territory Grid of Territory Tab
    /// </summary>
    protected void LoadTerritory()
    {
        if (DrpZone.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpZone.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            grdTerritoryData.DataSource = dt;
            grdTerritoryData.DataBind();
        }
    }

    /// <summary>
    /// Loads Zones To Zone Combo Of Territory Tab.
    /// </summary>
    private void LoadTerritoryZone()
    {
        if (drpRegionTerritory.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpRegionTerritory.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpZone, dt, 0, 4, true);

        }
    }

    /// <summary>
    /// Loads Zones To Zone Combo And Territories To Territory Combo Of Territory Tab.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpRegionTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTerritoryZone();
        this.LoadTerritory();
    }

    /// <summary>
    /// Loads Territories To Territory Grid of Territory Tab.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTerritory();
    }

    /// <summary>
    /// Sets PageIndex Of Territory Grid On Territory Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void grdTerritoryData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTerritoryData.PageIndex = e.NewPageIndex;
        this.LoadTerritory();
    }
    protected void grdTerritoryData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        TerritoryId = int.Parse(grdTerritoryData.Rows[gvr.RowIndex].Cells[1].Text);
        txtTerritoryCode.Text = grdTerritoryData.Rows[gvr.RowIndex].Cells[2].Text;
        txtTerritoryName.Text = grdTerritoryData.Rows[gvr.RowIndex].Cells[3].Text;
        chbIsTerritoryActive.Checked = bool.Parse(grdTerritoryData.Rows[gvr.RowIndex].Cells[4].Text);
        btnSaveTerritory.Text = "Update";
    }

    /// <summary>
    /// Saves Or Updates A Territory.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveTerritory_Click(object sender, EventArgs e)
    {
        if (btnSaveTerritory.Text == "Save")
        {
            string InsertValue = GControler.InsertHierarchy(int.Parse(DrpZone.SelectedValue.ToString()), txtTerritoryCode.Text, txtTerritoryName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Territory, int.Parse(this.Session["UserId"].ToString()), SAMSCommon.Classes.Configuration.DistributorId, null);
        }
        else
        {
            if (chbIsTerritoryActive.Checked == false)
            {
                DataTable dt = GControler.SelectDistributorHierachyWithType(3, int.Parse(DrpRegion.SelectedValue.ToString()), int.Parse(DrpZone.SelectedValue.ToString()), TerritoryId);
                if (dt.Rows.Count > 0)
                {
                    lblErrorZone.Text = "An Active Town Exists...";
                    return;
                }
            }
            GControler.UpdateHierarchy(TerritoryId, int.Parse(DrpZone.SelectedValue.ToString()), txtTerritoryCode.Text, txtTerritoryName.Text, true, chbIsTerritoryActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Territory, int.Parse(this.Session["UserId"].ToString()), SAMSCommon.Classes.Configuration.DistributorId, null);
        }
        this.LoadTerritory();
        this.LoadTownTerritory();
        txtTerritoryCode.Text = "";
        txtTerritoryName.Text = "";
        btnSaveTerritory.Text = "Save";
        lblErrorZone.Text = "";
    }
    #endregion

    #region Town Tab

    /// <summary>
    /// Loads Existing Towns To Town Grid of Town Tab
    /// </summary>
    protected void LoadTown()
    {
        if (DrpTerritory.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTerritory.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            grdTownData.DataSource = dt;
            grdTownData.DataBind();
        }
    }

    /// <summary>
    /// Loads Zones To Zone Combo Of Town Tab.
    /// </summary>
    private void LoadTownZone()
    {
        if (DrpTownRegion.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTownZone, dt, 0, 4, true);

        }
    }

    /// <summary>
    /// Loads Territories To Territory Grid Of Town Tab.
    /// </summary>
    private void LoadTownTerritory()
    {
        if (DrpTownZone.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpTownZone.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpTerritory, dt, 0, 4, true);
        }
    }

    /// <summary>
    /// Loads Zones To Zone Combo,Territories To Territory Combo And Towns To Town Grid Of Town Tab.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpTownRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTownZone();
        this.LoadTownTerritory();
        this.LoadTown();
    }

    /// <summary>
    /// Loads Territories To Territory Combo And Towns To Town Grid Of Town Tab.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpTownZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTownTerritory();
        this.LoadTown();
    }
    protected void grdZoneData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdZoneData.PageIndex = e.NewPageIndex;
        this.LoadZone();
    }
    /// <summary>
    /// Loads Towns To Town Grid of Town Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTown();
    }
    protected void grdTownData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        TownId = int.Parse(grdTownData.Rows[gvr.RowIndex].Cells[1].Text);
        txtTownCode.Text = grdTownData.Rows[gvr.RowIndex].Cells[2].Text;
        txtTownName.Text = grdTownData.Rows[gvr.RowIndex].Cells[3].Text;
        chbIsTownActive.Checked = bool.Parse(grdTownData.Rows[gvr.RowIndex].Cells[4].Text);
        btnSaveTown.Text = "Update";
    }


    /// <summary>
    /// Sets PageIndex Of Town Grid On Town Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>   
    protected void grdTownData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTownData.PageIndex = e.NewPageIndex;
        this.LoadTown();
    }

    /// <summary>
    /// Saves Or Updates A Town.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveTown_Click(object sender, EventArgs e)
    {
        if (btnSaveTown.Text == "Save")
        {
            string InsertValue = GControler.InsertHierarchy(int.Parse(DrpTerritory.SelectedValue.ToString()), txtTownCode.Text, txtTownName.Text, true, true, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Town, int.Parse(this.Session["UserId"].ToString()), SAMSCommon.Classes.Configuration.DistributorId, null);
        }
        else
        {
            if (chbIsTownActive.Checked == false)
            {
                DataTable dt = GControler.SelectDistributorHierachyWithType(9, TownId, null);
                if (dt.Rows.Count > 0)
                {
                    lblErrorZone1.Text = "This Town is Assigned...";
                    return;
                }
            }
            GControler.UpdateHierarchy(TownId, int.Parse(DrpTerritory.SelectedValue.ToString()), txtTownCode.Text, txtTownName.Text, true, chbIsTownActive.Checked, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.Town, int.Parse(this.Session["UserId"].ToString()), SAMSCommon.Classes.Configuration.DistributorId, null);
        }
        this.LoadTown();
        txtTownCode.Text = "";
        txtTownName.Text = "";
        btnSaveTown.Text = "Save";
        lblErrorZone1.Text = "";
    }

    #endregion    
}
