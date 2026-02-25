using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;
using System.Data;

public partial class Forms_frmVehicle : System.Web.UI.Page
{
    private static int RowId;
    static long v_id;
    DistributorController DC_ctrl = new DistributorController();

    #region Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadDeliveryman();
            LoadGrid();
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
        DataTable g_dt = DC_ctrl.SelectVehicleInfo2(int.Parse(drpDistributor.SelectedValue), CurrentWorkDate);        
        grd_vehicle.DataSource = g_dt;
        grd_vehicle.DataBind();
    }
    #endregion

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDeliveryman();
        this.LoadGrid();
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
        if (btnSave.Text != "Update")
        {
            v_id = Constants.LongNullValue;
        }
        bool isInserted = DC_ctrl.InsertVehicle(v_id, int.Parse(drpDistributor.SelectedValue), txtVehicleno.Text.ToString(), txtMake.Text.ToString(), txtModel.Text.ToString(), txtEngine.Text.ToString(), txtChassisno.Text.ToString()
                          , -1 , CurrentWorkDate, System.DateTime.Now, int.Parse(this.Session["userid"].ToString()),chbIs_Active.Checked, Constants .IntNullValue ,Constants .IntNullValue ,Constants .IntNullValue );
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
        txtVehicleno.Text = "";
        txtChassisno.Text = "";
        txtEngine.Text = "";
        txtMake.Text = "";
        txtModel.Text = "";
        txtChassisno.Text = "";
        btnSave.Text = "Save";
        chbIs_Active.Enabled = false;
        chbIs_Active.Checked = true;
        LoadDistributor();
        LoadDeliveryman();
        LoadGrid();
    }


    protected void grd_vehicle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int NewEditIndex = gvr.RowIndex;
        RowId = NewEditIndex;
        v_id = Convert.ToInt64(grd_vehicle.Rows[NewEditIndex].Cells[0].Text);
        txtVehicleno.Text = grd_vehicle.Rows[NewEditIndex].Cells[1].Text;
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
        btnSave.Text = "Update";
        chbIs_Active.Enabled = true;
    }
}