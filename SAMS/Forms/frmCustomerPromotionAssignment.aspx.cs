using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Customer Credit Limit And Principal Assignment
/// </summary>
public partial class Forms_frmCustomerPromotionAssignment : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadPromotionClass();
            this.LoadDistributor();
            this.LoadArea();
            this.LoadCustomer();            
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
        }
    }

    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    private void LoadCustomer()
    {
        if (DrpDistributor.Items.Count > 0)
        {
            cblCustomer.Items.Clear();
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(DrpDistributor.SelectedValue), int.Parse(ddlRoute.SelectedValue), Constants.IntNullValue, -1);
            clsWebFormUtil.FillListBox(this.cblCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_NAME");
            ddlPromoiton_SelectedIndexChanged(null, null);
        }
        else
        {
            cblCustomer.Items.Clear();
        }        
    }

    /// <summary>
    /// Load Locations To Location Comb
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }

    private void LoadArea()
    {
        if (DrpDistributor.Items.Count > 0)
        {
            ddlRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            //ddlRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlRoute, dt, 0, 6);
        }
        else
        {
            ddlRoute.Items.Clear();
        }
    }

    private void LoadPromotionClass()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerVolumeClassType, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(ddlPromoiton, dt, 0, 2);
        this.ddlPromoiton.SelectedValue = "88";
    }

    /// <summary>
    /// Inserts Customer Credit Limit And Assigns Customer To Principal
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (cblCustomer.Items.Count > 0 && ddlPromoiton.Items.Count > 0)
        {
            CustomerDataController mCustomer = new CustomerDataController();
            string[] CustomerID = null;
            bool flag = true;
            foreach (ListItem li in cblCustomer.Items)
            {
                if (li.Value.Contains(","))
                {
                    CustomerID = li.Value.Split(',');
                    if (CustomerID.Length > 0)
                    {
                        if (li.Selected)
                        {
                            flag = mCustomer.UpdateCustomerPromotionClass(Convert.ToInt64(CustomerID[0]), Convert.ToInt32(ddlPromoiton.SelectedValue), Convert.ToInt32(DrpDistributor.SelectedValue));
                        }                        
                        if (!flag)
                        {
                            break;
                        }

                    }
                }
            }
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Promotion Class assigned successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
            }
        }
    }

    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadCustomer();
    }

    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbAllCustomer.Checked = false;
        this.LoadCustomer();
    }

    protected void ddlPromoiton_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] CustomerID = null;
        foreach (ListItem li in cblCustomer.Items)
        {
            if (li.Value.Contains(","))
            {
                CustomerID = li.Value.Split(',');
                if (CustomerID.Length > 0)
                {
                    if (CustomerID[1].ToString() == ddlPromoiton.SelectedValue)
                    {
                        li.Selected = true;
                    }
                    else
                    {
                        li.Selected = false;
                    }
                }
            }
        }
    }
}