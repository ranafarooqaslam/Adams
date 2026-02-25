using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For  Customer Wise (DSR) Report
/// </summary>
public partial class Forms_RptCustomerPromotion : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();
            this.LoadChannelType();
            this.LoadCreditCustomer();
            txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");            
        }
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Channel Types To ChannelType Combo
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        ddlChannel.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlChannel, dt, 0, 2);
    }

    private void LoadCreditCustomer()
    {
        ddlCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.GetCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue,Constants.IntNullValue, Convert.ToInt32(ddlChannel.SelectedValue));
            ddlCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlCustomer, dt, "CUSTOMER_ID", "CUSTOMER_NAME");
        }
        else
        {
            ddlCustomer.Items.Add(new ListItem("Customer Not Found", Constants.IntNullValue.ToString()));
        }
    }

    /// <summary>
    /// Shows Customer Wise (DSR) in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbChannel = new System.Text.StringBuilder();
        System.Text.StringBuilder sbSKUs = new System.Text.StringBuilder();


        RptSaleController RptSaleCtl = new RptSaleController();
        DataSet ds = RptSaleCtl.GetCustomerPromotion(Convert.ToInt32(drpDistributor.SelectedValue),Convert.ToInt32(ddlChannel.SelectedValue),Convert.ToInt32(ddlCustomer.SelectedValue),Convert.ToInt32(ddlStatus.SelectedValue),Convert.ToDateTime(txtDate.Text));
        DataSetToExcel dsexcel = new DataSetToExcel();
        DataControl dc = new DataControl();
        string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath() + "\\CustomerPormotionReport.xls";
        DataSetToExcel.exportToExcel(ds, path);
        System.IO.FileInfo file = new System.IO.FileInfo(path);
        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
        else
        {
            Response.Write("This file does not exist.");
        }
    }

    protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCreditCustomer();
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCreditCustomer();
    }
}