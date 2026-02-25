using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmRePrint : System.Web.UI.Page
{
    readonly RptCustomerController _CustomerCtrl = new RptCustomerController();
    UserController userControl = new UserController();
    static DataTable dtLocation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtstartDate.Text = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            txtEndDate.Text = (DateTime.Parse(Session["CurrentWorkDate"].ToString())).ToString("dd-MMM-yyyy");
            dtLocation = userControl.SelectSlashUser2(int.Parse(Session["UserId"].ToString()));
            Session.Add("dtLocation", dtLocation);
            LoadDistributor();
            LoadGridInvoices();
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);


    }

    protected void btnRePrint_Click(object sender, EventArgs e)
    {
        LoadGridInvoices();
    }
    private void LoadGridInvoices()
    {

        if (drpDistributor.Items.Count > 0)
        {
            DataTable piDt = _CustomerCtrl.SelectCustomerInvoicePrint(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, 0, DateTime.Parse(txtstartDate.Text), DateTime.Parse(txtEndDate.Text));
            GrdPrintInvoice.DataSource = piDt;
            GrdPrintInvoice.DataBind();
        }
    }
    protected void GrdPrintInvoice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {int did = int.Parse(drpDistributor.SelectedValue);
        if (did == 1234)
        {
            did = Constants.IntNullValue;
        }

        int sale_inv_id = Convert.ToInt32(GrdPrintInvoice.Rows[e.RowIndex].Cells[0].Text);
        //if (HttpContext.Current.Session["PosReportType"].ToString() == "0")
        //{
        var crpReport = new SAMSBusinessLayer.Reports.CrpPrintInvoice();
        //}
        //else
        //{
        //    crpReport = new SAMSBusinessLayer.Reports.CrpPrintInvoice2();
        //}

        var rcc = new RptCustomerController();
        var DPrint = new DocumentPrintController();
        DataTable dt = DPrint.SelectReportTitle(did);

        DataTable dtNotes = rcc.GetNotes(did);
        DataSet ds = null;
        ds = _CustomerCtrl.PrintInvoice(did, Constants.IntNullValue, 2, int.Parse(sale_inv_id.ToString()),
            Constants.DateNullValue, Constants.DateNullValue, "2");

        string notes = "";
        if (dtNotes != null && dtNotes.Rows.Count > 0)
        {
            for (int i = 0; i < dtNotes.Rows.Count; i++)
            {
                notes = notes + ". " + dtNotes.Rows[i]["SLIP_NOTE"].ToString() + "\n";
            }
        }
        crpReport.SetDataSource(ds);
        crpReport.Refresh();




        crpReport.SetParameterValue("COMPANY_NAME", "/images/cloth.png");
        DataTable dtLocation = (DataTable)HttpContext.Current.Session["dtLocation"];
        //if (HttpContext.Current.Session["PosReportType"].ToString() == "1")
        //{
        //crpReport.SetParameterValue("Location", dtLocation.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        //crpReport.SetParameterValue("LocationAddress", dtLocation.Rows[0]["address1"].ToString());
        //crpReport.SetParameterValue("LocationAddress2", "Email: " + dtLocation.Rows[0]["address2"].ToString());
        //crpReport.SetParameterValue("LocationContact", "PH: " + dtLocation.Rows[0]["CONTACT_NUMBER"].ToString());
        //}
        //else
        //{
        crpReport.SetParameterValue("PHONE_NUMBER", dt.Rows[0]["CONTACT_NUMBER"].ToString());
        crpReport.SetParameterValue("UserLogin", HttpContext.Current.Session["UserName2"].ToString());
        crpReport.SetParameterValue("notes", notes);
        //}
        crpReport.SetParameterValue("DecimalPoint", 2);
        crpReport.SetParameterValue("BillType", "Cash Memo");
        HttpContext.Current.Session.Add("CrpReport", crpReport);
        HttpContext.Current.Session.Add("ReportType", 0);

        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=500,height=550,left=20,top=40\");</script>";
        Type cstype = GetType();
        var cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);

    }

    protected void GrdPrintInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        var lnkbtn = (LinkButton)e.Row.FindControl("lnkbtn_print");
        var mgr = ScriptManager.GetCurrent(Page);
        if (mgr != null)
            mgr.RegisterPostBackControl(lnkbtn);


        e.Row.BackColor = e.Row.Cells[6].Text == "Sales Refund" ? Color.Lavender : Color.White;
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGridInvoices();
    }
}