using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Globalization;

/// <summary>
/// Form For Day Close
/// </summary>
public partial class Forms_frmDayCloseStatus : System.Web.UI.Page
{
    DistributorController mController = new DistributorController();

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LastClosedDay(int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["Distributor_Id"].ToString()));
            if (Grid_Hierarchy.Rows.Count > 1)
            {
                Grid_Hierarchy.UseAccessibleHeader = true;
                Grid_Hierarchy.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }

    /// <summary>
    /// Gets Location(s) Last Day Close(s)
    /// </summary>
    /// <param name="UserId">User</param>
    /// <param name="p_Distributor">Location</param>
    private void LastClosedDay(int UserId, int p_Distributor)
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(UserId, p_Distributor);
        if (dt.Rows.Count > 0)
        {
            this.Session.Add("CurrentWorkDate", DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()));
            btnDayClose.Visible = true;
            cbAll.Visible = true;
        }
        else
        {
           this.Session.Add("CurrentWorkDate", DateTime.Now);
           rblDistributorTypes.Visible = true;
           cbAll.Visible = false;
        }
        GetLastClosedDay(UserId, p_Distributor, 0);       
    }
    
    /// <summary>
    /// Loads Location(s) Last Day Close(s) To Grid
    /// </summary>
    /// <param name="UserId">User</param>
    /// <param name="p_Distributor">Location</param>
    /// <param name="p_Status">Status</param>
    private void GetLastClosedDay(int UserId, int p_Distributor, int p_Status)
    {
        DataTable dtable = mController.MaxDayClose(int.Parse(this.Session["UserId"].ToString()), p_Status);
        Grid_Hierarchy.DataSource = dtable;
        Grid_Hierarchy.DataBind();
    }
    
    /// <summary>
    /// Loads Locations(Active/InActive/All) Last Day Close(s) To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void rblDistributorTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLastClosedDay(int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["Distributor_Id"].ToString()), Convert.ToInt32(rblDistributorTypes.SelectedValue));
        if (Grid_Hierarchy.Rows.Count > 1)
        {
            Grid_Hierarchy.UseAccessibleHeader = true;
            Grid_Hierarchy.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
      
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAll.Checked == true)
        {
            foreach (GridViewRow dr in Grid_Hierarchy.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("Chbgrid");
                ChbInvoice.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in Grid_Hierarchy.Rows)
            {
                CheckBox ChbInvoice = (CheckBox)dr.FindControl("Chbgrid");
                ChbInvoice.Checked = false;
            }
        }
    }

    protected void btnDayClose_Click(object sender, EventArgs e)
    {
        int Dist_ID=0;
        bool flag = false;
        string Close_date = "";
        DateTime cl = Constants.DateNullValue;
        if (btnDayClose.Visible == true)
        {
            DistributorController mDayClose = new DistributorController();
            
            foreach (GridViewRow grd in Grid_Hierarchy.Rows)
            {
                CheckBox chb = (CheckBox)grd.FindControl("chbgrid");
                
                if (chb.Checked == true)
                {
                     Dist_ID=Convert.ToInt32(grd.Cells[1].Text);
                  
                     Close_date = grd.Cells[5].Text;
                     cl = DateTime.ParseExact(Close_date,"dd-MM-yyyy", CultureInfo.CreateSpecificCulture("ur-Pk"));
                     cl=cl.AddDays(1);
                    bool dt = mDayClose.UspDayClose(cl, Dist_ID, int.Parse(this.Session["UserID"].ToString()));
                    if (dt == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Some error in day Close Contact System Administrator');", true);
                        return;
                    }
                    else
                    {
                        UserController mController = new UserController();
                        if (mController.InsertUserLogoutTime(Convert.ToInt64(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"])) == "Logout Time Inserted")
                        {
                            flag = true;
                            //this.Session.Clear();
                            //Response.Redirect("~/Login.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Some error in day Close Contact System Administrator');", true);
                            return;
                        }
                    }
                }
            }
            if (flag)
            {
                this.Session.Clear();
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Plz Select any Location');", true);
                           
            }
        }
    }
}