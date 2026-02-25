using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class pr_frmGazettedHolidays : System.Web.UI.Page
{
    CompanyController CC = new CompanyController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ResetForm();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No date selected.');", true);
                return;
            }
            if (hdnGazettedID.Value == "" || hdnGazettedID.Value == null)
            {
                if (CC.InsertGazettedHoliday(Convert.ToDateTime(this.txtDate.Text),txtDescription.Text, Convert.ToInt32(Session["UserID"])))
                {
                   // Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
                    Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());// + "&TopID=" + Request.QueryString["TopID"].ToString());
               
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
                    return;
                }
            }
            else
            {
                if (CC.UpdateGazettedHoliday(Convert.ToInt32(hdnGazettedID.Value),Convert.ToDateTime(this.txtDate.Text),txtDescription.Text, Convert.ToInt32(Session["UserID"])))
                {
                    Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());// + "&TopID=" + Request.QueryString["TopID"].ToString());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('" + ex.Message + "');", true);
        }
    }
    protected void btnDisCard_Click(object sender, EventArgs e)
    {
        Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());// + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    private void ResetForm()
    {        
        fillrptGazetted(Constants.IntNullValue);
        hdnGazettedID.Value = string.Empty;
        if (Session["Add"] != null)
        {
            pnlGazettedContent.Visible = true;
            pnlGazettedGrid.Visible = false;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("GazettedHolidayID");
        }
        else if (Session["Edit"] != null)
        {
            pnlGazettedContent.Visible = true;
            pnlGazettedGrid.Visible = false;

            DataTable dt = CC.GetGazettedHoliday(Convert.ToInt32(Session["GazettedHolidayID"]));
            hdnGazettedID.Value = dt.Rows[0]["GazettedHolidayID"].ToString();
            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            txtDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToString("dd-MMM-yyyy");

            imgGazetted.ImageUrl = "../images/btn-update.png";

            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("GazettedHolidayID");
        }
        else
        {
            pnlGazettedContent.Visible = false;
            pnlGazettedGrid.Visible = true;
            Session.Remove("Add");
            Session.Remove("Edit");
            Session.Remove("GazettedHolidayID");
        }
    }
    protected void btnShowpnlGazettedContent_Click(object sender, EventArgs e)
    {
        Session.Add("Add", "Add");
        Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());// + "&TopID=" + Request.QueryString["TopID"].ToString());
    }
    protected void rptGazetted_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Session.Add("Edit", "Edit");
            Session.Add("GazettedHolidayID", e.CommandArgument.ToString());
            Response.Redirect("pr_frmGazettedHolidays.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());// + "&TopID=" + Request.QueryString["TopID"].ToString());
        }
        else if (e.CommandName == "delete")
        {
            if (CC.DeleteGazettedHoliday(Convert.ToInt32(e.CommandArgument)))
            {
                fillrptGazetted(Constants.IntNullValue);
            }
        }
    }
    private void fillrptGazetted(int GazettedHolidayID)
    {
        DataTable dt = CC.GetGazettedHoliday(GazettedHolidayID);
        rptGazetted.DataSource = dt;
        rptGazetted.DataBind();
    }
    /// <summary>
    /// Loads Existing Towns To Town Grid of Town Tab
    /// </summary>    
}