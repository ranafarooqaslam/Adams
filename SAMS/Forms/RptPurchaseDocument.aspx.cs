using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Purchase Document Report
/// </summary>
public partial class Forms_RptPurchaseDocument : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0,
            DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1);
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2);
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }



    protected void ShowReport(int p_Type)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptInventoryController RptInventoryCtl = new RptInventoryController();
        if (rbReportType.SelectedIndex == 0)
        { // with price

           
            if (DrpDocumentType.SelectedIndex != 0)
            { // all others except purchase document and 
                if (DrpDocumentType.SelectedValue == "16")// Returnable Stock Received
                {
                    var crpReport = new SAMSBusinessLayer.Reports.CrpReturnableStockDocumentPrice();
                    DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
                    DataSet ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue));
                    crpReport.SetDataSource(ds);
                    crpReport.Refresh();
                    crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Document");
                    crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
                    crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                    Session.Add("CrpReport", crpReport);
                    Session.Add("ReportType", p_Type);
                    const string url = "'Default.aspx'";
                    const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(cstype, "OpenWindow", script);
                }
                else
                {
                    var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument4();
                    DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
                    DataSet ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue));
                    crpReport.SetDataSource(ds);
                    crpReport.Refresh();
                    crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Document");
                    crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
                    crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                    Session.Add("CrpReport", crpReport);
                    Session.Add("ReportType", p_Type);
                    const string url = "'Default.aspx'";
                    const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(cstype, "OpenWindow", script);
                }
            }
            else
            { // Purchase Document
                var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument();
                DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
                DataSet ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue));
                crpReport.SetDataSource(ds);
                crpReport.Refresh();
                crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Invoice");
                crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
                crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                Session.Add("CrpReport", crpReport);
                Session.Add("ReportType", p_Type);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }


        }
        else
        { // with out price
            if (DrpDocumentType.SelectedIndex != 0)
            { // all others except purchase document
                if (DrpDocumentType.SelectedValue == "16")// Returnable Stock Received
                {
                    var crpReport = new SAMSBusinessLayer.Reports.CrpReturnableStockDocument();
                    DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
                    DataSet ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue));
                    crpReport.SetDataSource(ds);
                    crpReport.Refresh();
                    crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Document");
                    crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
                    crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                    Session.Add("CrpReport", crpReport);
                    Session.Add("ReportType", p_Type);
                    const string url = "'Default.aspx'";
                    const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(cstype, "OpenWindow", script);
                }
                else
                {
                    var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocument2withOutPrice();
                    DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
                    DataSet ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue));
                    crpReport.SetDataSource(ds);
                    crpReport.Refresh();
                    crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Document");
                    crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
                    crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                    Session.Add("CrpReport", crpReport);
                    Session.Add("ReportType", p_Type);
                    const string url = "'Default.aspx'";
                    const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(cstype, "OpenWindow", script);
                }
            }
            else
            { // Purchase Document with out price
                var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseDocumentWithOutPrice();
                DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
                DataSet ds = RptInventoryCtl.SelectPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(DrpDocumentType.SelectedValue));
                crpReport.SetDataSource(ds);
                crpReport.Refresh();
                crpReport.SetParameterValue("DocumentType", DrpDocumentType.SelectedItem.Text + " Invoice");
                crpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
                crpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                Session.Add("CrpReport", crpReport);
                Session.Add("ReportType", p_Type);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }



        }




    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}
