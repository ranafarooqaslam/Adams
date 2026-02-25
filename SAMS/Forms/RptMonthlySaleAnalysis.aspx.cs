using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Monthly Sale Report
/// </summary>
public partial class Forms_RptMonthlySaleAnalysis : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.DistributorType();
            this.LoadAssingned();
            this.LoadPrincipal();
            this.LoadArea();
            LoadDeliveryman();
            LoadCreditCustomer();
            LoadCatagory();
            SetDivs();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartYear.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("yyyy");
            txtEndYear.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("yyyy");

            txtFromMonth.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
            txtToMonth.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");

            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

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

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCreditCustomer();
    }
    private void DistributorType()
    {
        DistributorController dController = new DistributorController();
        DataTable dt = dController.SelectDistributorTypeInfo(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddDistributorType, dt, 0, 2);
    }

    /// <summary>
    /// Loads Assigned Locations To Location Combo
    /// </summary>
    private void LoadAssingned()
    {
        if (ddDistributorType.Items.Count > 0)
        {
            drpDistributor.Items.Clear();
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectUserAssignment(int.Parse(this.Session["UserId"].ToString()),int.Parse(ddDistributorType.SelectedValue.ToString()), 1, int.Parse(this.Session["CompanyId"].ToString()));
            drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Assigned Locations
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddDistributorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAssingned(); 
    }
    
    /// <summary>
    /// Shows Report in Excel Or PDF
    /// </summary>
    /// <param name="p_Report_Type">ReportType</param>
    private void ShowReport(int p_Report_Type)
    {
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        if (DrpUnitType.SelectedValue == "0")
        {
            dtFrom = new DateTime(Convert.ToInt32(this.txtStartYear.Text), 1, 1);
            dtTo = new DateTime(Convert.ToInt32(this.txtEndYear.Text), 12, 31);
        }
        else if (DrpUnitType.SelectedValue == "1")
        {
            DateTime dtFromMonth = DateTime.Parse(txtFromMonth.Text);
            dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

            DateTime dtToMonth = DateTime.Parse(txtToMonth.Text);
            dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
            dtTo = dtTo.AddMonths(1).AddDays(-1);
        }
        else
        {
            dtFrom = Convert.ToDateTime(txtStartDate.Text);
            dtTo = Convert.ToDateTime(txtEndDate.Text);
        }

        string Catagories_IDs = null;
        for (int i = 0; i < this.ListCatagory.Items.Count; i++)
        {
            if (this.ListCatagory.Items[i].Selected == true)
            {
                Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
            }
        }
        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();

        DataSet ds = RptSaleCtl.GetDistributorReconcilation(byte.Parse(DrpUnitType.SelectedIndex.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), dtFrom, dtTo, int.Parse(this.Session["UserId"].ToString()), byte.Parse(DrpReportType.SelectedIndex.ToString()), byte.Parse(RadioButtonList1.SelectedIndex.ToString()),Convert.ToInt32(ddDistributorType.SelectedItem.Value));

        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        SAMSBusinessLayer.Reports.CrpMonthSaleValume CrpReport = new SAMSBusinessLayer.Reports.CrpMonthSaleValume();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("FromDate", dtFrom);
        CrpReport.SetParameterValue("ToDate", dtTo);
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);

        //CrpReport.SetParameterValue("Route", DrpRoute.SelectedItem.Text);
        //CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
        //CrpReport.SetParameterValue("Saleforce", DrpSaleForce.SelectedItem.Text);


        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ReportType", DrpReportType.SelectedItem.Text);
        CrpReport.SetParameterValue("ParameterType", DrpUnitType.SelectedItem.Text);
        CrpReport.SetParameterValue("Price", RadioButtonList1.SelectedItem.Text);

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", p_Report_Type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script); 
    }

    /// <summary>
    /// Shows Monthly Sale Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Monthly Sale Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Sets Date, Month And Year Divisions Visibility
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpUnitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetDivs();
    }

    /// <summary>
    /// Sets Date, Month And Year Divisions Visibility As Per Report Type
    /// </summary>
    private void SetDivs()
    {
        if (DrpUnitType.SelectedValue == "0")
        {
            divYear.Visible = true;
            divMonth.Visible = false;
            divDate.Visible = false;
        }
        else if (DrpUnitType.SelectedValue == "1")
        {
            divYear.Visible = false;
            divMonth.Visible = true;
            divDate.Visible = false;
        }
        else if (DrpUnitType.SelectedValue == "2")
        {
            divYear.Visible = false;
            divMonth.Visible = false;
            divDate.Visible = true;
        }
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
    protected void ChbAllLocationType_CheckedChanged(object sender, EventArgs e)
    {
        CheckAll();
    }

    protected void DrpSaleForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
