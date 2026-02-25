using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For  Catagory Wise Customer Report
/// </summary>
public partial class Forms_rptOutletWiseSale : System.Web.UI.Page
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
            LoadPrincipal();
            LoadCatagory();
            LoadLocation();
            LoadArea();
            LoadCreditCustomer();
            LoadSaleForce();
            DateTime dt = (DateTime)this.Session["CurrentWorkDate"];
            this.txtFromDate.Text = dt.ToString("dd-MMM-yyyy");
            this.txtToDate.Text = dt.ToString("dd-MMM-yyyy");
        }
    }
    private void LoadSaleForce()
    {
        if (DrpLocation.Items.Count > 0)
        {
            DrpSaleForce.Items.Clear();
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            DrpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpSaleForce, m_dt, 0, 3);
        }
        else
        {
            DrpSaleForce.Items.Clear();
        }
    }
    /// <summary>
    /// Loads Categories To Category ListBox
    /// </summary>
    protected void LoadCatagory()
    {
        SkuHierarchyController Hierarchy = new SkuHierarchyController();
        DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));
        DataView dv = new DataView(dtCatagory);
        dv.RowFilter = "Company_id = " + Convert.ToInt32(drpPrincipal.SelectedValue.ToString());
        dtCatagory = dv.ToTable();
        clsWebFormUtil.FillListBox(this.ListCatagory, dtCatagory, 4, 6);
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    protected void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            //this.drpPrincipal.Items.Add(new ListItem("All", "0"));
            clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpLocation.DataSource = dt;
            this.DrpLocation.DataTextField = "DISTRIBUTOR_NAME";
            this.DrpLocation.DataValueField = "DISTRIBUTOR_ID";
            this.DrpLocation.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    private void LoadArea()
    {
        if (DrpLocation.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
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
        if (DrpLocation.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(DrpLocation.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4);
        }
        else
        {
            DrpCustomer.Items.Add(new ListItem("Customer Not Found", Constants.IntNullValue.ToString()));
        }
    }
    /// <summary>
    /// Loads Categories
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ListCatagory.Items.Clear();
        LoadCatagory();
    }
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        // this.LoadSaleForce();
        this.LoadCreditCustomer();
        LoadSaleForce();
    }
    /// <summary>
    /// Checks/UnCheckes All Categories in Category ListBox
    /// </summary>
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


    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

        
        this.LoadArea();
        this.LoadCreditCustomer();
    }
    /// <summary>
    /// Checks/UnChecks All Categoreis
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ChbAllLocationType_CheckedChanged(object sender, EventArgs e)
    {
        CheckAll();
    }

    /// <summary>
    /// Shows  Catagory Wise Customer Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        if (RbReportType.SelectedIndex == 0)
        {
            try
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                RptCustomerController RptCustomerCtl = new RptCustomerController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                string FromDate = parsed_date_fromdate.ToShortDateString();
                string ToDate = parsed_date_todate.ToShortDateString();
                string Catagories_IDs = null;
                SAMSBusinessLayer.Reports.CrpOutletwiseSaleInUnit CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSaleInUnit();
                DataSet ds = null;
                for (int i = 0; i < this.ListCatagory.Items.Count; i++)
                {
                    if (this.ListCatagory.Items[i].Selected == true)
                    {
                        Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                    }
                }

                ds = RptCustomerCtl.OutletWiseSaleinUnit(int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(this.DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), Catagories_IDs,int.Parse(DrpRoute.SelectedValue),int.Parse(DrpCustomer.SelectedValue ), int.Parse (DrpSaleForce .SelectedValue ));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());

                CrpReport.SetParameterValue("Route", this.DrpRoute .SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Customer", this.DrpCustomer.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("SaleForce", this.DrpSaleForce.SelectedItem.Text.ToString());

                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        else if (RbReportType.SelectedIndex == 1)
        {
            try
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                RptCustomerController RptCustomerCtl = new RptCustomerController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                string FromDate = parsed_date_fromdate.ToShortDateString();
                string ToDate = parsed_date_todate.ToShortDateString();
                string Catagories_IDs = null;
                SAMSBusinessLayer.Reports.CrpOutletwiseSale CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSale();
                DataSet ds = null;
                for (int i = 0; i < this.ListCatagory.Items.Count; i++)
                {
                    if (this.ListCatagory.Items[i].Selected == true)
                    {
                        Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                    }
                }

                ds = RptCustomerCtl.OutletWiseSale(int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(this.DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), Catagories_IDs, int.Parse(DrpRoute.SelectedValue),int.Parse(DrpCustomer.SelectedValue), int.Parse (DrpSaleForce .SelectedValue ));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());

                CrpReport.SetParameterValue("Route", this.DrpRoute.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Customer", this.DrpCustomer.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("SaleForce", this.DrpSaleForce.SelectedItem.Text.ToString());

                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        else
        {


            try
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                RptCustomerController RptCustomerCtl = new RptCustomerController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                string FromDate = parsed_date_fromdate.ToShortDateString();
                string ToDate = parsed_date_todate.ToShortDateString();
                string Catagories_IDs = null;
                SAMSBusinessLayer.Reports.CrpOutletwiseSaleBoth CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSaleBoth();
                DataSet ds = null;
                for (int i = 0; i < this.ListCatagory.Items.Count; i++)
                {
                    if (this.ListCatagory.Items[i].Selected == true)
                    {
                        Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                    }
                }

                ds = RptCustomerCtl.OutletWiseSaleBoth(int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(this.DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), Catagories_IDs, int.Parse(DrpRoute.SelectedValue ), int.Parse(DrpCustomer.SelectedValue ), int.Parse (DrpSaleForce .SelectedValue ));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());

                CrpReport.SetParameterValue("Route", this.DrpRoute.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Customer", this.DrpCustomer.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("SaleForce", this.DrpSaleForce.SelectedItem.Text.ToString());

                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 0);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }




        }
    }

    /// <summary>
    /// Shows  Catagory Wise Customer Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        if (RbReportType.SelectedIndex == 0)
        {
            try
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                RptCustomerController RptCustomerCtl = new RptCustomerController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                string FromDate = parsed_date_fromdate.ToShortDateString();
                string ToDate = parsed_date_todate.ToShortDateString();
                string Catagories_IDs = null;
                SAMSBusinessLayer.Reports.CrpOutletwiseSaleInUnit CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSaleInUnit();
                DataSet ds = null;
                for (int i = 0; i < this.ListCatagory.Items.Count; i++)
                {
                    if (this.ListCatagory.Items[i].Selected == true)
                    {
                        Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                    }
                }

                ds = RptCustomerCtl.OutletWiseSaleinUnit(int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(this.DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), Catagories_IDs, int.Parse(DrpRoute.SelectedValue), int.Parse(DrpCustomer.SelectedValue), int.Parse(DrpSaleForce.SelectedValue));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Route", this.DrpRoute.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Customer", this.DrpCustomer.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("SaleForce", this.DrpSaleForce.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        else if (RbReportType.SelectedIndex == 1)
        {
            try
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                RptCustomerController RptCustomerCtl = new RptCustomerController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                string FromDate = parsed_date_fromdate.ToShortDateString();
                string ToDate = parsed_date_todate.ToShortDateString();
                string Catagories_IDs = null;
                SAMSBusinessLayer.Reports.CrpOutletwiseSale CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSale();
                DataSet ds = null;
                for (int i = 0; i < this.ListCatagory.Items.Count; i++)
                {
                    if (this.ListCatagory.Items[i].Selected == true)
                    {
                        Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                    }
                }

                ds = RptCustomerCtl.OutletWiseSale(int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(this.DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), Catagories_IDs, int.Parse(DrpRoute.SelectedValue), int.Parse(DrpCustomer.SelectedValue), int.Parse(DrpSaleForce.SelectedValue));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Route", this.DrpRoute.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Customer", this.DrpCustomer.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("SaleForce", this.DrpSaleForce.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        else
        {


            try
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                RptCustomerController RptCustomerCtl = new RptCustomerController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                string FromDate = parsed_date_fromdate.ToShortDateString();
                string ToDate = parsed_date_todate.ToShortDateString();
                string Catagories_IDs = null;
                SAMSBusinessLayer.Reports.CrpOutletwiseSaleBoth CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSaleBoth();
                DataSet ds = null;
                for (int i = 0; i < this.ListCatagory.Items.Count; i++)
                {
                    if (this.ListCatagory.Items[i].Selected == true)
                    {
                        Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                    }
                }

                ds = RptCustomerCtl.OutletWiseSaleBoth(int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(this.DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), Catagories_IDs, int.Parse(DrpRoute.SelectedValue), int.Parse(DrpCustomer.SelectedValue), int.Parse (DrpSaleForce .SelectedValue ));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Route", this.DrpRoute.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Customer", this.DrpCustomer.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("SaleForce", this.DrpSaleForce.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", 1);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }




        }
    }
}
