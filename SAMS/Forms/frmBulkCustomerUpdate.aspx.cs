using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmBulkCustomerUpdate : System.Web.UI.Page
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
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
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
        this.SetTableSorter();
    }

    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    private void LoadCustomer()
    {
        if (DrpDistributor.Items.Count > 0 && DrpTown.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectCustomer(int.Parse(DrpDistributor.SelectedValue.ToString()),
                int.Parse(DrpTown.SelectedValue), int.Parse(DrpRoute.SelectedValue));
            this.Grid_users.DataSource = dt;
            this.Grid_users.DataBind();
        }
    }
    protected void Grid_users_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var ddlSlabType = e.Row.FindControl("DrpSlabType") as DropDownList;
            var ddlTaxSlab = e.Row.FindControl("DrpTaxSlab") as DropDownList;

            DistributorController DController = new DistributorController();
            DataTable taxSlabdt = DController.SelectTaxSlab(1);
            DataTable slabTypedt = DController.SelectTaxSlab(2);

            DataRowView dataRow = (DataRowView)e.Row.DataItem;

            // Set the selected value of the dropdown lists based on the data
            if (ddlTaxSlab != null)
            {
                ddlTaxSlab.DataSource = taxSlabdt;
                ddlTaxSlab.DataTextField = "TAX_SLAB_NAME";
                ddlTaxSlab.DataValueField = "TAX_SLAB_ID";
                ddlTaxSlab.DataBind();

                ddlTaxSlab.SelectedValue = dataRow["TAX_SLAB_ID"].ToString();
            }

            if (ddlSlabType != null)
            {
                ddlSlabType.DataSource = slabTypedt;
                ddlSlabType.DataTextField = "TAX_SLAB_NAME";
                ddlSlabType.DataValueField = "TAX_SLAB_ID";
                ddlSlabType.DataBind();

                ddlSlabType.SelectedValue = dataRow["CLAUSE_ID"].ToString();
            }

            if (ddlTaxSlab.SelectedItem.Text == "Not Applicable")
            {
                ddlSlabType.Enabled = false;
            }
            else
            {
                ddlSlabType.Enabled = true;
            }
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
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomerDataController mController = new CustomerDataController();

        foreach (GridViewRow row in Grid_users.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                long customerId = Convert.ToInt64(row.Cells[0].Text);

                // Retrieve old values from the GridView
                string oldTaxSlab = Server.HtmlDecode(Server.HtmlDecode(row.Cells[29].Text)).Trim().Replace("&nbsp", "");
                string oldTaxClause = Server.HtmlDecode(Server.HtmlDecode(row.Cells[30].Text)).Trim().Replace("&nbsp", "");

                // Retrieve new values from the edit controls
                DropDownList DrpTaxSlab = (DropDownList)row.FindControl("DrpTaxSlab");
                DropDownList DrpTaxClause = (DropDownList)row.FindControl("DrpSlabType");
                string newTaxSlab = DrpTaxSlab.SelectedValue;

                int taxSlabId = int.Parse(newTaxSlab);
                string newTaxClause = "0";
                int clauseId = Constants.IntNullValue;

                if (DrpTaxSlab.SelectedItem.Text == "Not Applicable")
                {
                    clauseId = Constants.IntNullValue;
                }
                else
                {
                    newTaxClause = DrpTaxClause.SelectedValue;
                    clauseId = int.Parse(newTaxClause);
                }

                // Check if there are changes in tax slab or tax clause
                if (oldTaxSlab != newTaxSlab || oldTaxClause != newTaxClause)
                {
                    bool isRegister = false;
                var registerText = Server.HtmlDecode(Server.HtmlDecode(row.Cells[14].Text)).Trim().Replace("&nbsp", "");
                    if (string.IsNullOrEmpty(registerText.Trim()) || registerText == "&nbsp;")
                {
                    isRegister = false;
                    registerText = "";
                }
                else
                {
                    isRegister = true;
                }
                bool isActive = bool.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[19].Text)).Trim().Replace("&nbsp", "")));
                int channelType = int.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[4].Text)).Trim().Replace("&nbsp", "")));
                int promoClass = int.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[3].Text)).Trim().Replace("&nbsp", "")));
                int businessType = int.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[2].Text)).Trim().Replace("&nbsp", "")));
                int routeId = int.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[6].Text)).Trim().Replace("&nbsp", "")));
                int marketId = int.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[7].Text)).Trim().Replace("&nbsp", "")));
                int townId = int.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[5].Text)).Trim().Replace("&nbsp", "")));
                int distributorId = int.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[1].Text)).Trim().Replace("&nbsp", "")));
                string contactPerson = Server.HtmlEncode(Server.HtmlDecode(Server.HtmlDecode(row.Cells[10].Text)).Trim().Replace("&nbsp", ""));
                string phoneNo = Server.HtmlDecode(Server.HtmlDecode(row.Cells[11].Text)).Trim().Replace("&nbsp", "");
                string customerName = Server.HtmlDecode(Server.HtmlDecode(row.Cells[9].Text)).Trim().Replace("&nbsp", "");
                string address = Server.HtmlDecode(Server.HtmlDecode(row.Cells[13].Text)).Trim().Replace("&nbsp", "");
                int priceGroup = int.Parse(dc.chkNull_0(Server.HtmlDecode(Server.HtmlDecode(row.Cells[22].Text)).Trim().Replace("&nbsp", "")));
                string cnic = Server.HtmlDecode(Server.HtmlDecode(row.Cells[23].Text)).Trim().Replace("&nbsp", "");
                string ntn = Server.HtmlDecode(Server.HtmlDecode(row.Cells[24].Text)).Trim().Replace("&nbsp", "");
                string latitude = Server.HtmlDecode(Server.HtmlDecode(row.Cells[26].Text)).Trim().Replace("&nbsp", "");
                string longitude = Server.HtmlDecode(Server.HtmlDecode(row.Cells[27].Text)).Trim().Replace("&nbsp", "");


                    mController.UpdateCustomer(customerId, isRegister, isActive,
             channelType, promoClass, businessType, routeId
             , marketId, townId, distributorId, registerText, contactPerson,
             phoneNo, "", null, customerName, address, Constants.DateNullValue,
             1, priceGroup, cnic, ntn, 0, latitude,
             longitude, taxSlabId, clauseId);

                }
            }
        }

        // Exit edit mode after saving changes
        Grid_users.EditIndex = -1;
        LoadCustomer(); // Rebind the GridView to reflect changes
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Updated Successfully');", true);
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

    protected void DrpTaxSlab_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlTaxSlab = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlTaxSlab.NamingContainer;

        if (row != null)
        {
            DropDownList ddlSlabType = (DropDownList)row.FindControl("DrpSlabType");

            if (ddlSlabType != null)
            {
                // Example logic to update ddlSlabType based on selected value in ddlTaxSlab
                // You can replace this with your actual logic
                string selectedTaxSlab = ddlTaxSlab.SelectedItem.Text;


                if (selectedTaxSlab == "Not Applicable")
                {
                    ddlSlabType.Enabled = false;
                }
                else
                {
                    ddlSlabType.Enabled = true;
                }
                // Add more conditions as needed
            }
        }
    }

    protected void btnLoadData_Click(object sender, EventArgs e)
    {
        LoadCustomer();
        SetTableSorter();
    }
}