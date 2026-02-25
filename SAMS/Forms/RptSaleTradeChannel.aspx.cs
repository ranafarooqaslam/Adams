using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Trade Channel Sale Report
/// </summary>
public partial class Forms_RptSaleTradeChannel : System.Web.UI.Page
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
            this.LoadPrincipal();
            this.LoadArea();
            LoadDeliveryman();
            LoadCreditCustomer();
            LoadCatagory();
            LoadSKUDetail();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    
    private void LoadCreditCustomer()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor .Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4);
        }
        else
        {
            DrpCustomer.Items.Add(new ListItem("Customer Not Found", Constants.IntNullValue.ToString()));
        }
    }
    
    protected void LoadCatagory()
    {
        SkuHierarchyController Hierarchy = new SkuHierarchyController();
        DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));
        //DataView dv = new DataView(dtCatagory);
        //dv.RowFilter = "Company_id = " + Convert.ToInt32(drpPrincipal.SelectedValue.ToString());
        //dtCatagory = dv.ToTable();
        clsWebFormUtil.FillListBox(this.ListCatagory, dtCatagory, 4, 6);
    }
    
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, 
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, 
            DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Constants.LongNullValue);
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        drpDistributor.Items.Clear();

        if (rblType.SelectedIndex == 0)
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
        }
        else
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
        }
    }

    /// <summary>
    /// Loads Delivermen To Deliverman Combo
    /// </summary>
    private void LoadDeliveryman()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DrpSaleForce.Items.Clear();
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            DrpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpSaleForce, m_dt, 0, 3);
        }
        else
        {
            DrpSaleForce.Items.Clear();
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

    /// <summary>
    /// Shows Trade Channel Sale Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        PrintReport(0);
    }

    /// <summary>
    /// Loads Routes And Delivermen
    /// </summary>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadDeliveryman();
    }

    /// <summary>
    /// Shows Trade Channel Sale Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        PrintReport(1);
    }

    /// <summary>
    /// Enables/Disables Deliveryman And Route Combos
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblType.SelectedIndex == 0)
        {
            DrpRoute.Enabled = true;
            DrpSaleForce.Enabled = true;
            ListSkus.Visible = true;
            ChbAllSkus.Visible = true;
            lblSkus.Visible = true;
            Panel2.Visible = true;
        }
        else
        {
            DrpRoute.Enabled = false;
            DrpSaleForce.Enabled = false;
            ListSkus.Visible = false;
            ChbAllSkus.Visible = false;
            lblSkus.Visible = false;
            Panel2.Visible = false;
        }
        this.LoadDistributor();
    }

    protected void ChbAllLocationType_CheckedChanged(object sender, EventArgs e)
    {
        CheckAll();
        LoadSKUDetail();
    }

    protected void CheckAll()
    {
        if (this.ChbAllCatagories.Checked == true)
        {
            for (int i = 0; i < this.ListCatagory.Items.Count; i++)
            {
                this.ListCatagory.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListCatagory.Items.Count; i++)
            {
                this.ListCatagory.Items[i].Selected = false;
            }
        }

    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCreditCustomer();
    }

    protected void ChbAllSkus_CheckedChanged(object sender, EventArgs e)
    {
        if (this.ChbAllSkus.Checked == true)
        {
            for (int i = 0; i < this.ListSkus.Items.Count; i++)
            {
                this.ListSkus.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListSkus.Items.Count; i++)
            {
                this.ListSkus.Items[i].Selected = false;
            }
        }

    }

    private void LoadSKUDetail()
    {
        
        ListSkus.Items.Clear();
        var SkuCategory = "";
        if (ListCatagory.Items.Count > 0)
        {
            for (int i = 0; i < this.ListCatagory.Items.Count; i++)
            {
                if (ListCatagory.Items[i].Selected == true)
                {
                    SkuCategory = SkuCategory + ',' + ListCatagory.Items[i].Value;
                }
            }
            if (SkuCategory.Length > 0)
            {
               
                SkuController mContoller = new SkuController();
                DataTable dt = mContoller.SelectSkuInfo(int.Parse(DrpPrincipal.SelectedValue), Constants.IntNullValue, SkuCategory, Constants.IntNullValue, Constants.IntNullValue);

                clsWebFormUtil.FillListBox(this.ListSkus, dt, 0, 2, false);
            }
        }
        ChbAllSkus_CheckedChanged(null, null);

    }

    protected void ListCatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUDetail();
    }

    protected void PrintReport(int Type)
    {
        DataControl dc = new DataControl();
        DocumentPrintController DPrint = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();

        if (rblType.SelectedIndex == 0)
        {
            string Catagories_IDs = null;
            for (int i = 0; i < this.ListCatagory.Items.Count; i++)
            {
                if (this.ListCatagory.Items[i].Selected == true)
                {
                    Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                }
            }


            string Sku_IDs = null;
            for (int i = 0; i < this.ListSkus.Items.Count; i++)
            {
                if (this.ListSkus.Items[i].Selected == true)
                {
                    Sku_IDs += this.ListSkus.Items[i].Value.ToString() + ",";
                }
            }




            DataSet ds = RptSaleCtl.SelectTradeChannelSale(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
               DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(DrpSaleForce.SelectedValue.ToString()), Catagories_IDs, int.Parse(DrpCustomer.SelectedValue), Sku_IDs);

            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

            SAMSBusinessLayer.Reports.CrpTradeChannelReport CrpReport = new SAMSBusinessLayer.Reports.CrpTradeChannelReport();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("Area", DrpRoute.SelectedItem.Text);
            CrpReport.SetParameterValue("SaleForce", DrpSaleForce.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {



            string Catagories_IDs = null;
            for (int i = 0; i < this.ListCatagory.Items.Count; i++)
            {
                if (this.ListCatagory.Items[i].Selected == true)
                {
                    Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                }
            }




            DataSet ds = RptSaleCtl.SelectTradeChannelSale(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                          DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()), Catagories_IDs, int.Parse(DrpCustomer.SelectedValue));

            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

            SAMSBusinessLayer.Reports.CrpSaleTradeBranchWise CrpReport = new SAMSBusinessLayer.Reports.CrpSaleTradeBranchWise();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

}