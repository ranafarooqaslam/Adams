using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using DevExpress.DashboardCommon;

public partial class Forms_frmDashboard : System.Web.UI.Page
{
    GeoHierarchyController gController = new GeoHierarchyController();
    DistributorController DController = new DistributorController();

    DataSet objDashFile = null, objDsDashboard = null;
    DataSource objDataSource = null;
    DashboardSqlDataSource objSQLDataSource = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            this.LoadRegion();
            this.LoadZone();
            this.LoadTerritory();
            this.LoadAssingned();            
            this.LoadDashboardData();
        }
    }

    protected void LoadRegion()
    {
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, false, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.ddlRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlRegion, dt, "GEO_ID", "GEO_NAME");
    }

    /// <summary>
    /// Loads Assigned Locations To Assigned ListBox on Location Tab
    /// </summary>
    private void LoadAssingned()
    {
        ddlLocation.Items.Clear();
        UserController mUserController = new UserController();
        DataTable dt = DController.GetTownWistUnAssignDistributor(Constants.IntNullValue, Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlTerritory.SelectedValue), Convert.ToInt32(Session["UserID"]), 0);
        this.ddlLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 1);

    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadZone();
        this.LoadTerritory();
        this.LoadAssingned();
        this.LoadDashboardData();
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTerritory();
        this.LoadAssingned();
        this.LoadDashboardData();
    }

    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAssingned();
        this.LoadDashboardData();
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDashboardData();
    }
        
    /// <summary>
    /// Loads Zones To Area Combo
    /// </summary>
    protected void LoadZone()
    {
        if (ddlRegion.Items.Count > 0)
        {
            ddlZone.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            ddlZone.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlZone, dt, "GEO_ID", "GEO_NAME");
        }
    }

    /// <summary>
    /// Loads Territories To Territory Combo
    /// </summary>
    protected void LoadTerritory()
    {
        if (ddlZone.Items.Count > 0)
        {
            ddlTerritory.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            ddlTerritory.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlZone.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlTerritory, dt, "GEO_ID", "GEO_NAME");
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        this.LoadDashboardData();
    }

    private void LoadDashboardData()
    {
        RptSaleController rptCtl = new RptSaleController();
        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();

        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        try
        {
            objDashFile = rptCtl.GetDashboardFile(Convert.ToInt32(Request.QueryString["LevelID"]));
            Session.Add("objDashFile", objDashFile);

            objDsDashboard = rptCtl.GetDashboardData(Convert.ToDateTime(txtDate.Text),null);            
            dshViewer.DashboardId = "Top 5 Sales Persons";
            objDsDashboard.Tables[0].TableName = "Top 5 Sales Persons";
            objDsDashboard.Tables[1].TableName = "Dashboard Data";
            objDsDashboard.Tables[2].TableName = "Last 5 Month Sales";
            objDsDashboard.Tables[3].TableName = "Top 5 SKU Sales";
            Session.Add("objDsDashboard", objDsDashboard);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void dshViewer_DashboardLoading(object sender, DevExpress.DashboardWeb.DashboardLoadingEventArgs e)
    {
        objDashFile = (DataSet)Session["objDashFile"];
        if (objDashFile.Tables[0].Rows[0]["vbrFile"] != null && !String.IsNullOrEmpty(Convert.ToString(objDashFile.Tables[0].Rows[0]["vbrFile"])))
        {
            using (Stream ms = new MemoryStream((byte[])objDashFile.Tables[0].Rows[0]["vbrFile"]))
            {
                ms.Seek(0L, SeekOrigin.Begin);
                using (StreamReader streamReader = new StreamReader(ms))
                    e.DashboardXml = streamReader.ReadToEnd();
            }
        }
    }

    protected void dshViewer_DataLoading(object sender, DevExpress.DashboardWeb.DataLoadingWebEventArgs e)
    {
        objDsDashboard = (DataSet)Session["objDsDashboard"];
        e.Data = objDsDashboard.Tables[e.DataSourceName];
    }    
}