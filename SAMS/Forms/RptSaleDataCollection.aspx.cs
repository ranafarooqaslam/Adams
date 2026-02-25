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
public partial class Forms_RptSaleDataCollection : System.Web.UI.Page
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
            LoadDistributor();
            LoadPrincipal();
            this.LoadCatagory();
            LoadArea();
            LoadOrderBooker();
            LoadChannelType();
            LoadDeliveryman();
            LoadCreditCustomer();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1);
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
    /// Loads Order Bookers To OrderBooker Combo
    /// </summary>
    private void LoadOrderBooker()
    {
        DrpOrderbooker.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0 && DrpPrincipal.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()), Convert.ToInt32(DrpPrincipal.SelectedValue));
            DrpOrderbooker.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpOrderbooker, m_dt, 0, 3);
        }
    }

    /// <summary>
    /// Loads Deliverymen To Deliverman Combo
    /// </summary>
    private void LoadDeliveryman()
    {
        DrpSaleForce.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
            DrpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpSaleForce, m_dt, 0, 3);
        }
    }

    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }

    private void LoadCreditCustomer()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 4);
        }
        else
        {
            DrpCustomer.Items.Add(new ListItem("Customer Not Found", Constants.IntNullValue.ToString()));
        }
    }

    /// <summary>
    /// Loads Channel Types To ChannelType Combo
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillListBox(cblChannel, dt, 0, 2);
    }

    protected void LoadCatagory()
    {
        SkuHierarchyController Hierarchy = new SkuHierarchyController();
        DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));        
        clsWebFormUtil.FillListBox(this.cblCategory, dtCatagory, 4, 6);
    }

    private void LoadSKUDetail()
    {
        cblSKU.Items.Clear();
        var SkuCategory = "";
        if (cblCategory.Items.Count > 0)
        {
            for (int i = 0; i < this.cblCategory.Items.Count; i++)
            {
                if (cblCategory.Items[i].Selected == true)
                {
                    SkuCategory = SkuCategory + ',' + cblCategory.Items[i].Value;
                }
            }
            if (SkuCategory.Length > 0)
            {

                SkuController mContoller = new SkuController();
                DataTable dt = mContoller.SelectSkuInfo(int.Parse(DrpPrincipal.SelectedValue), Constants.IntNullValue, SkuCategory, Constants.IntNullValue, Constants.IntNullValue);
                clsWebFormUtil.FillListBox(this.cblSKU, dt, 0, 2, false);
            }
        }
        cbSKUAll_CheckedChanged(null, null);
    }

    /// <summary>
    /// Loads Order Bookers, Routes And Delivermen
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>    
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadDeliveryman();
        LoadOrderBooker();
        LoadCreditCustomer();
    }

    /// <summary>
    /// Loads Order Bookers And Deliverymen
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDeliveryman();
        LoadOrderBooker();
        LoadCreditCustomer();
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

        foreach (ListItem li in cblChannel.Items)
        {
            if (li.Selected)
            {
                sbChannel.Append(li.Value);
                sbChannel.Append(",");
            }
        }

        foreach (ListItem li in cblSKU.Items)
        {
            if (li.Selected)
            {
                sbSKUs.Append(li.Value);
                sbSKUs.Append(",");
            }
        }
        int CustomerType = Constants.IntNullValue;
        if (ddlCustomerType.SelectedValue != "-1")
        {
            CustomerType = Convert.ToInt32(ddlCustomerType.SelectedValue);
        }

        RptSaleController RptSaleCtl = new RptSaleController();
        DataSet ds = RptSaleCtl.GetRetailSaleDataCollection(int.Parse(rbReportType.SelectedValue),Convert.ToInt32(drpDistributor.SelectedValue),Convert.ToInt32(DrpRoute.SelectedValue),Convert.ToInt32(DrpCustomer.SelectedValue),CustomerType,Convert.ToInt32(DrpPrincipal.SelectedValue),Convert.ToInt32(DrpOrderbooker.SelectedValue)
            ,Convert.ToInt32(DrpSaleForce.SelectedValue),sbChannel.ToString(),sbSKUs.ToString(),Convert.ToDateTime(txtStartDate.Text),Convert.ToDateTime(txtEndDate.Text + " 23:59:59"),Convert.ToInt32(cbWithSKUName.Checked));        
        DataSetToExcel dsexcel = new DataSetToExcel();
        DataControl dc = new DataControl();
        string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath() + "\\RetailSale_DataCollection.xls";
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

    /// <summary>
    /// Loads Order Bookers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOrderBooker();
    }

    protected void cbChannelAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in cblChannel.Items)
        {
            li.Selected = cbChannelAll.Checked;
        }
    }

    protected void cbCategoryAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in cblCategory.Items)
        {
            li.Selected = cbCategoryAll.Checked;
        }
        this.LoadSKUDetail();
    }

    protected void cbSKUAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in cblSKU.Items)
        {
            li.Selected = cbSKUAll.Checked;
        }
    }

    protected void cblCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();
    }
}