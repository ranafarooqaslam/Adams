using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_RptAssetDetail : System.Web.UI.Page
{
    AssetsController mController = new AssetsController();
    DataControl dc = new DataControl();
    private static int DistributorId;
    private static int CompanyId;
    private static int RowNo;

    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            LoadAssetType();
            LoadAssetNo();
            LoadChillerNo();
            LoadCustomer();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(this.ListLocation, dt, 0, 2, true);
    }
    private void LoadAssetType()
    {
        AssetsController AController = new AssetsController();
        DataTable dt = AController.SelectAssetType();
        clsWebFormUtil.FillListBox(this.ListAssetType, dt, "AssetTypeID", "AssetTypeName", true);
    }
    private void LoadAssetNo()
    {
        AssetsController AController = new AssetsController();
        DataTable dt = AController.SelectAssetNo(Constants.LongNullValue);
        clsWebFormUtil.FillListBox(this.ListAssetNo, dt, "Asset_Marking_ID", "CompanyAssetNo", true);
    }
    private void LoadChillerNo()
    {
        AssetsController AController = new AssetsController();
        DataTable dt = AController.SelectSerialNo1();
        clsWebFormUtil.FillListBox(this.ListChillerNo, dt, "SerialNo1", "SerialNo1", true);
    }
    private void LoadCustomer()
    {
        AssetsController AController = new AssetsController();
        string locationIds = "";
        for (int i = 0; i < this.ListLocation.Items.Count; i++)
        {
            if (this.ListLocation.Items[i].Selected == true)
            {
                locationIds += this.ListLocation.Items[i].Value.ToString() + ",";
            }
        }

        DataTable dt = AController.SelectCustomerForAsset(locationIds);
        clsWebFormUtil.FillListBox(this.ListCustomer, dt, "CUSTOMER_ID", "CUSTOMER_NAME", true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        ShowAssetDetailReport(1);
      
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        ShowAssetDetailReport(0);
    }
    public void ShowAssetDetailReport(int Type)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();

            SAMSBusinessLayer.Reports.DsReport2 ds = new SAMSBusinessLayer.Reports.DsReport2();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(Session["DISTRIBUTOR_ID"].ToString()));

            string AssetTypeIDs = null;
            string LocationIDs = null;
            string AssetNo = null;
            string transactionType = null;
            string serialNo1 = null;
            string customerIds = null;
            for (int i = 0; i < this.ListAssetType.Items.Count; i++)
            {
                if (this.ListAssetType.Items[i].Selected == true)
                {
                    AssetTypeIDs += this.ListAssetType.Items[i].Value.ToString() + ",";
                }
            }

            for (int i = 0; i < this.ListLocation.Items.Count; i++)
            {
                if (this.ListLocation.Items[i].Selected == true)
                {
                    LocationIDs += this.ListLocation.Items[i].Value.ToString() + ",";
                }
            }

            for (int i = 0; i < this.ListAssetNo.Items.Count; i++)
            {
                if (this.ListAssetNo.Items[i].Selected == true)
                {
                    AssetNo += this.ListAssetNo.Items[i].Text.ToString() + ",";
                }
            }

            for (int i = 0; i < this.ListTransactionType.Items.Count; i++)
            {
                if (this.ListTransactionType.Items[i].Selected == true)
                {
                    transactionType += this.ListTransactionType.Items[i].Value.ToString() + ",";
                }
            }
            for (int i = 0; i < this.ListChillerNo.Items.Count; i++)
            {
                if (this.ListChillerNo.Items[i].Selected == true)
                {
                    serialNo1 += this.ListChillerNo.Items[i].Value.ToString() + ",";
                }
            }
            for (int i = 0; i < this.ListCustomer.Items.Count; i++)
            {
                if (this.ListCustomer.Items[i].Selected == true)
                {
                    customerIds += this.ListCustomer.Items[i].Value.ToString() + ",";
                }
            }

            DataControl dc = new DataControl();
            DataTable result = mController.SelectAssetDetailRpt(AssetTypeIDs,
                LocationIDs, AssetNo, transactionType, serialNo1, customerIds,
                DateTime.Parse(txtStartDate.Text),
                DateTime.Parse(txtEndDate.Text));

            foreach (DataRow dr in result.Rows)
            {
                ds.Tables["RptAssetDetail"].ImportRow(dr);
            }

            ReportDocument CrpReport = new ReportDocument();

            CrpReport = new SAMSBusinessLayer.Reports.CrpAssetDetail();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("DocumentType", "Asset Details");
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url +
                            ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ChbAllAssetType_CheckedChanged(object sender, EventArgs e)
    {
        if (this.ChbAllAssetType.Checked == true)
        {
            for (int i = 0; i < this.ListAssetType.Items.Count; i++)
            {
                this.ListAssetType.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListAssetType.Items.Count; i++)
            {
                this.ListAssetType.Items[i].Selected = false;
            }
        }
    }
    protected void ChbAllLocation_CheckedChanged(object sender, EventArgs e)
    {
        if (this.ChbAllLocation.Checked == true)
        {
            for (int i = 0; i < this.ListLocation.Items.Count; i++)
            {
                this.ListLocation.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListLocation.Items.Count; i++)
            {
                this.ListLocation.Items[i].Selected = false;
            }
        }

        LoadCustomer();
    }
    protected void ChbAllAssetNo_CheckedChanged(object sender, EventArgs e)
    {
        if (this.ChbAllAssetNo.Checked == true)
        {
            for (int i = 0; i < this.ListAssetNo.Items.Count; i++)
            {
                this.ListAssetNo.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListAssetNo.Items.Count; i++)
            {
                this.ListAssetNo.Items[i].Selected = false;
            }
        }
    }

    protected void ChbAllTransactionType_CheckedChanged(object sender, EventArgs e)
    {
        if (this.ChbAllTransactionType.Checked == true)
        {
            for (int i = 0; i < this.ListTransactionType.Items.Count; i++)
            {
                this.ListTransactionType.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListTransactionType.Items.Count; i++)
            {
                this.ListTransactionType.Items[i].Selected = false;
            }
        }
    }

    protected void ChbAllChillerNo_CheckedChanged(object sender, EventArgs e)
    {
        if (this.ChbAllChillerNo.Checked == true)
        {
            for (int i = 0; i < this.ListChillerNo.Items.Count; i++)
            {
                this.ListChillerNo.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListChillerNo.Items.Count; i++)
            {
                this.ListChillerNo.Items[i].Selected = false;
            }
        }
    }

    protected void ChbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        if (this.ChbAllCustomer.Checked == true)
        {
            for (int i = 0; i < this.ListCustomer.Items.Count; i++)
            {
                this.ListCustomer.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.ListCustomer.Items.Count; i++)
            {
                this.ListCustomer.Items[i].Selected = false;
            }
        }
    }

    protected void ListLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }
}