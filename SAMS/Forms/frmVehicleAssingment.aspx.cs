using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;
using System.Data;

public partial class Forms_frmVehicleAssingment : System.Web.UI.Page
{
    private static int RowId;
    static long v_id = Constants.LongNullValue;
    DistributorController DC_ctrl = new DistributorController();
    static DataTable dt_V = null;

    #region Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadDesignation();
            LoadSaleForce();
            loadVehicle();
            LoadGrid();
            txtChassisno .Attributes.Add("Readonly", "Readonly");
            txtEngine .Attributes.Add("Readonly", "Readonly");
            txtMake .Attributes.Add("Readonly", "Readonly");
            txtModel .Attributes.Add("Readonly", "Readonly");
        }
    }

    private void LoadDesignation()
    {
        this.DrpDesignation.Items.Clear();
        CompanyConfigrationController ccc = new CompanyConfigrationController();
        DataTable dt = ccc.SelectDesignation(Int32.Parse(Session["WorkingCompanyID"].ToString()), Constants.IntNullValue);
        DrpDesignation.DataSource = dt;
        DrpDesignation.DataTextField = "DesignationName";
        DrpDesignation.DataValueField = "DesignationID";
        DrpDesignation.DataBind();
    }
    
    private void LoadSaleForce()
    {
        if (drpDistributor.Items.Count > 0 && DrpDesignation.Items.Count > 0)
        {
            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectDistributorUser(int.Parse(DrpDesignation.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpAssignTo, dt, 0, 6, true);
        }
    }
    
    private void LoadDistributor()
    {  
        DataTable dt = DC_ctrl.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }
    
    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpAssignTo, m_dt, 0, 3, true);
        }
    }
    
    private void LoadGrid()
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
        DataTable g_dt = DC_ctrl.SelectVehicleInfo(int.Parse(drpDistributor.SelectedValue), CurrentWorkDate);        
        grd_vehicle.DataSource = g_dt;
        grd_vehicle.DataBind();
    }
    
    private void loadVehicle()
    {
        dt_V = DC_ctrl.SelectVehicleNO2(int.Parse(drpDistributor.SelectedValue));
        clsWebFormUtil.FillDropDownList(this.DrpVehicleno, dt_V, 0, 2, true);
        DrpVehicleno_SelectedIndexChanged(null, null);
    }
    
    #endregion

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDeliveryman();
        this.LoadGrid();
        loadVehicle();
  
    }
       
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
        int Orderbooker_id = Constants .IntNullValue ;
        int Driver_Id = Constants.IntNullValue;
        int DeliveryMan_Id = Constants.IntNullValue;
        int LOADER_Id = Constants.IntNullValue;
        if (btnSave.Text != "Update")
        {
           // v_id = Constants.LongNullValue;
        }
        if(int.Parse(DrpDesignation.SelectedValue) == Constants.SALES_FORCE_DELIVERYMAN || int.Parse(DrpDesignation.SelectedValue) == Constants.SALES_FORCE_SALESPERSON)
        {
            DeliveryMan_Id = int.Parse(DrpAssignTo.SelectedValue.ToString());
        }
        if (int.Parse(DrpDesignation.SelectedValue) == Constants.SALES_FORCE_ORDERBOOKER)
        {
            Orderbooker_id   = int.Parse(DrpAssignTo.SelectedValue.ToString());
        }        
        if (int.Parse(DrpDesignation.SelectedValue) == Constants.SALES_Driver)
        {
            Driver_Id = int.Parse(DrpAssignTo.SelectedValue.ToString());
        }
        if (int.Parse(DrpDesignation.SelectedValue) == Constants.SALES_Loader)
        {
            LOADER_Id = int.Parse(DrpAssignTo.SelectedValue.ToString());
        }
        bool isInserted = DC_ctrl.InsertVehicle(v_id, int.Parse(drpDistributor.SelectedValue), DrpVehicleno .SelectedItem .Text, txtMake.Text.ToString(), txtModel.Text.ToString(), txtEngine.Text.ToString(), txtChassisno.Text.ToString()
                          , DeliveryMan_Id, CurrentWorkDate, System.DateTime.Now, int.Parse(this.Session["userid"].ToString()), chbIs_Active.Checked, Orderbooker_id, Driver_Id, LOADER_Id);
        if (isInserted == true)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Record insert successfully.');");
            ClearAll();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Some error occurred.');");
        }
    }
 
    private void ClearAll()
    {
        chbIs_Active.Checked = true;
        LoadDistributor();
        LoadDeliveryman();
        LoadGrid();
        loadVehicle();
    }

    protected void DrpVehicleno_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow[] foundRows = dt_V.Select("VEHICLE_ID  = '" + DrpVehicleno.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            v_id = long .Parse ( foundRows[0]["VEHICLE_ID"].ToString());
            drpDistributor.SelectedValue = foundRows[0]["DISTRIBUTOR_ID"].ToString();
            txtMake.Text = foundRows[0]["MAKE"].ToString();
            txtModel.Text = foundRows[0]["MODEL"].ToString();
            txtEngine.Text = foundRows[0]["ENGINE_NO"].ToString();
            txtChassisno.Text = foundRows[0]["CHASSIS_NO"].ToString();
            string chk = foundRows[0]["IS_ACTIVE"].ToString();
            if (chk == "True")
            {
                chbIs_Active.Checked = true;
            }
            else
            {
                chbIs_Active.Checked = false;
            }
            if (foundRows[0]["ORDERBOOKER_ID"].ToString() != "")
            {
                this.LoadDesignation();
                this.LoadSaleForce();
            }
            else
            {
                this.LoadDesignation();
                this.LoadSaleForce();
            }

        }
    }
    
    protected void DrpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
    }


    protected void grd_vehicle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowId = NewEditIndex;
        v_id = Convert.ToInt64(grd_vehicle.Rows[NewEditIndex].Cells[0].Text);
        txtMake.Text = grd_vehicle.Rows[NewEditIndex].Cells[2].Text;
        txtModel.Text = grd_vehicle.Rows[NewEditIndex].Cells[3].Text;
        txtEngine.Text = grd_vehicle.Rows[NewEditIndex].Cells[4].Text;
        txtChassisno.Text = grd_vehicle.Rows[NewEditIndex].Cells[5].Text;
        string chk = grd_vehicle.Rows[NewEditIndex].Cells[7].Text;
        if (chk == "True")
        {
            chbIs_Active.Checked = true;
        }
        else
        {
            chbIs_Active.Checked = false;
        }
        drpDistributor.SelectedValue = grd_vehicle.Rows[NewEditIndex].Cells[8].Text;
        DrpAssignTo.SelectedValue = grd_vehicle.Rows[NewEditIndex].Cells[9].Text;
        btnSave.Text = "Update";
        chbIs_Active.Enabled = true;
    }
}