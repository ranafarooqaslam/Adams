using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Models;
using SAMSDatabaseLayer.Classes;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Web;

public partial class Forms_RptCrateAndBasket : System.Web.UI.Page
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
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       ShowExcelReport(1);
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
   
    public void ShowReport(int Type)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

            DataSet result = mController.SelectCratesAndBasetRpt(int.Parse(drpDistributor.SelectedValue.ToString()),
                DateTime.Parse(txtStartDate.Text), int.Parse(drpDocumentType.SelectedValue),
                DateTime.Parse(txtDate.Text));

            ReportDocument CrpReport = new ReportDocument();
            if (drpDocumentType.SelectedValue == "1")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpCratesAndBasketSummary();
            }
            else if (drpDocumentType.SelectedValue == "0")
            {
                CrpReport = new SAMSBusinessLayer.Reports.CrpCratesAndBasketLedger();
            }

            CrpReport.SetDataSource(result);
            CrpReport.Refresh();

            if (drpDocumentType.SelectedValue == "1")
            {
                CrpReport.SetParameterValue("DocumentType", "Crates & Basket Summary");
            }
            else if (drpDocumentType.SelectedValue == "0")
            {
                CrpReport.SetParameterValue("DocumentType", "Crates & Basket Ledger");
            }
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Date", txtStartDate.Text);
            CrpReport.SetParameterValue("EndDate", txtDate.Text);
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("UserName", Session["UserName"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", Type);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openpage", "window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ShowExcelReport(int reportType)
    {
        DataSet result = mController.SelectCratesAndBasetRpt(
            int.Parse(drpDistributor.SelectedValue),
            DateTime.Parse(txtStartDate.Text),
            int.Parse(drpDocumentType.SelectedValue),
            DateTime.Parse(txtDate.Text));

        if (result == null ||
            !result.Tables.Contains("RptCratesAndBasket") ||
            result.Tables["RptCratesAndBasket"].Rows.Count == 0)
        {
            Response.Write("No Record Found....!");
            return;
        }

        DataTable dt = result.Tables["RptCratesAndBasket"];

        string fileName = drpDocumentType.SelectedValue == "1"
            ? "Crates & Basket Summary.xls"
            : "Crates & Basket Ledger.xls";

        string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath()
                      + "\\" + fileName;

        if (drpDocumentType.SelectedValue == "1")
            exportToExcelCustomSummary(dt, path, txtStartDate.Text, txtDate.Text);
        else
            exportToExcelCustomLedger(dt, path, txtStartDate.Text, txtDate.Text);

        FileInfo file = new FileInfo(path);

        if (file.Exists)
        {
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(file.FullName);
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
    public static void exportToExcelCustomSummary(DataTable source, string fileName, string fromDate, string toDate)
    {
        StreamWriter excelDoc = new StreamWriter(fileName);
        const string startExcelXML = "<xml version>\r\n<Workbook " +
              "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
              " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
              "xmlns:x=\"urn:schemas-microsoft-com:office:" +
              "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
              "office:spreadsheet\">\r\n <Styles>\r\n " +
              "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
              "<Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Left\"/>\r\n <Borders/>" +
              "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
              "\r\n <Protection/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
              "x:family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"StringLiteral\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Left\"/>\r\n <NumberFormat" +
              " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
              "ss:ID=\"Decimal\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Right\"/>\r\n <NumberFormat " +
              "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"Integer\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Right\"/>\r\n <NumberFormat " +
              "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
              "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
              "ss:Format=\"dd-mmm-yyyy;@\"/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"SubtotalStyle\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Right\"/>\r\n <Font x:family=\"Swiss\" ss:Bold=\"1\"/>\r\n <NumberFormat ss:Format=\"0\"/>\r\n <Interior ss:Color=\"#c0c0c0\" ss:Pattern=\"Solid\"/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"GrandTotalStyle\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Right\"/>\r\n <Font x:family=\"Swiss\" ss:Bold=\"1\"/>\r\n <NumberFormat ss:Format=\"0\"/>\r\n <Interior ss:Color=\"#c0c0c0\" ss:Pattern=\"Solid\"/>\r\n </Style>\r\n " +
              "</Styles>\r\n ";
        const string endExcelXML = "</Workbook>";

        excelDoc.Write(startExcelXML);
        excelDoc.Write("<Worksheet ss:Name=\"Crates And Basket Summary\">");
        excelDoc.Write("<Table>");
        excelDoc.Write("<Row ss:Height=\"18\">");
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Crates And Basket Summary</Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">From Date: " + fromDate + "</Data></Cell>");
        excelDoc.Write("</Row>");

        excelDoc.Write("<Row ss:Height=\"18\">");
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">To Date: " + toDate + "</Data></Cell>");
        excelDoc.Write("</Row>");

        excelDoc.Write("<Row>");
        foreach (DataColumn col in source.Columns)
        {
            string colName = col.ColumnName;
            if (colName == "WarehouseName" || colName == "DistributorName" ||
                colName == "CategoryName" || colName == "QtyTransferOutFromWarehouse" ||
                colName == "QtyReceivedByDistributor")
            {
                if (colName == "WarehouseName")
                {
                    colName = "From Location";
                }
                else if (colName == "DistributorName")
                {
                    colName = "To Location";
                }
                else if (colName == "CategoryName")
                {
                    colName = "Category";
                }
                else if (colName == "QtyTransferOutFromWarehouse")
                {
                    colName = "Sent";
                }
                else if (colName == "QtyReceivedByDistributor")
                {
                    colName = "Received";
                }

                excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">" + colName + "</Data></Cell>");
            }
        }
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\"> Balance</Data></Cell>");
        excelDoc.Write("</Row>");

        double qtySent = 0;
        double qtyReceived = 0;
        double balance = 0;

        foreach (DataRow row in source.Rows)
        {
            excelDoc.Write("<Row>");
            foreach (DataColumn col in source.Columns)
            {
                string style = "StringLiteral";
                string cellValue = row[col.ColumnName].ToString();

                if (col.ColumnName == "QtyReceivedByDistributor" || col.ColumnName == "QtyTransferOutFromWarehouse")
                {
                    style = "Decimal";
                    cellValue = !string.IsNullOrEmpty(cellValue) ? Convert.ToDouble(cellValue).
                        ToString("0.00") : "0";
                }
                if (col.ColumnName == "WarehouseName" || col.ColumnName == "DistributorName" ||
                col.ColumnName == "CategoryName" || col.ColumnName == "QtyTransferOutFromWarehouse" ||
                col.ColumnName == "QtyReceivedByDistributor")
                {
                    if (row["QtyTransferOutFromWarehouse"] != DBNull.Value)
                        qtySent = Convert.ToDouble(row["QtyTransferOutFromWarehouse"]);

                    if (row["QtyReceivedByDistributor"] != DBNull.Value)
                        qtyReceived = Convert.ToDouble(row["QtyReceivedByDistributor"]);

                    balance = qtySent - qtyReceived;

                    excelDoc.Write("<Cell ss:StyleID=\"" + style + "\"><Data ss:Type=\"String\">" + cellValue + "</Data></Cell>");
                }
            }
            excelDoc.Write("<Cell ss:StyleID=\"Decimal\"><Data ss:Type=\"String\">"
                    + balance.ToString("0.00") +
                    "</Data></Cell>");
            excelDoc.Write("</Row>");
        }

        excelDoc.Write("</Table>");
        excelDoc.Write(" </Worksheet>");
        excelDoc.Write(endExcelXML);
        excelDoc.Close();
    }
    public static void exportToExcelCustomLedger(DataTable source, string fileName, string fromDate, string toDate)
    {
        StreamWriter excelDoc = new StreamWriter(fileName);
        const string startExcelXML = "<xml version>\r\n<Workbook " +
              "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
              " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
              "xmlns:x=\"urn:schemas-microsoft-com:office:" +
              "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
              "office:spreadsheet\">\r\n <Styles>\r\n " +
              "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
              "<Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Left\"/>\r\n <Borders/>" +
              "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
              "\r\n <Protection/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
              "x:family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"StringLiteral\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Left\"/>\r\n <NumberFormat" +
              " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
              "ss:ID=\"Decimal\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Right\"/>\r\n <NumberFormat " +
              "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"Integer\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Right\"/>\r\n <NumberFormat " +
              "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
              "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
              "ss:Format=\"dd-mmm-yyyy;@\"/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"SubtotalStyle\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Right\"/>\r\n <Font x:family=\"Swiss\" ss:Bold=\"1\"/>\r\n <NumberFormat ss:Format=\"0\"/>\r\n <Interior ss:Color=\"#c0c0c0\" ss:Pattern=\"Solid\"/>\r\n </Style>\r\n " +
              "<Style ss:ID=\"GrandTotalStyle\">\r\n <Alignment ss:Vertical=\"Bottom\" ss:Horizontal=\"Right\"/>\r\n <Font x:family=\"Swiss\" ss:Bold=\"1\"/>\r\n <NumberFormat ss:Format=\"0\"/>\r\n <Interior ss:Color=\"#c0c0c0\" ss:Pattern=\"Solid\"/>\r\n </Style>\r\n " +
              "</Styles>\r\n ";
        const string endExcelXML = "</Workbook>";

        excelDoc.Write(startExcelXML);
        excelDoc.Write("<Worksheet ss:Name=\"Crates And Basket Ledger\">");
        excelDoc.Write("<Table>");
        excelDoc.Write("<Row ss:Height=\"18\">");
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Crates And Basket Ledger</Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">From Date: " + fromDate + "</Data></Cell>");
        excelDoc.Write("</Row>");

        excelDoc.Write("<Row ss:Height=\"18\">");
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\"></Data></Cell>");
        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">To Date: " + toDate + "</Data></Cell>");
        excelDoc.Write("</Row>");

        excelDoc.Write("<Row>");
        foreach (DataColumn col in source.Columns)
        {
            string colName = col.ColumnName;
            if (colName == "DOC_NO" || colName == "DOCUMENT_DATE" ||
                colName == "CategoryName" || colName == "OpeningBalance" ||
                colName == "QtyTransferOut" || colName == "QtyTransferIn" || colName == "ClosingBalance")
            {
                if (colName == "DOC_NO")
                {
                    colName = "Doc #";
                }
                else if (colName == "DOCUMENT_DATE")
                {
                    colName = "Date";
                }
                else if (colName == "CategoryName")
                {
                    colName = "Category";
                }
                else if (colName == "OpeningBalance")
                {
                    colName = "Opening Balance";
                }
                else if (colName == "QtyTransferOut")
                {
                    colName = "Sent";
                }
                else if (colName == "QtyTransferIn")
                {
                    colName = "Received";
                }
                else if (colName == "ClosingBalance")
                {
                    colName = "Closing Balance";
                }

                excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">" + colName + "</Data></Cell>");
            }
        }
        excelDoc.Write("</Row>");

        foreach (DataRow row in source.Rows)
        {
            excelDoc.Write("<Row>");
            foreach (DataColumn col in source.Columns)
            {
                string style = "StringLiteral";
                string cellValue = row[col.ColumnName].ToString();

                if (col.ColumnName == "QtyTransferOut" || col.ColumnName == "QtyTransferIn" ||
                    col.ColumnName == "OpeningBalance" || col.ColumnName == "ClosingBalance")
                {
                    style = "Decimal";
                    cellValue = !string.IsNullOrEmpty(cellValue) ? Convert.ToDouble(cellValue).
                        ToString("0.00") : "0";
                }
               
                if (col.ColumnName == "DOC_NO" || col.ColumnName == "DOCUMENT_DATE" ||
                col.ColumnName == "CategoryName" || col.ColumnName == "OpeningBalance" ||
                col.ColumnName == "QtyTransferOut" || col.ColumnName == "QtyTransferIn" || col.ColumnName == "ClosingBalance")
                {
                    excelDoc.Write("<Cell ss:StyleID=\"" + style + "\"><Data ss:Type=\"String\">" + cellValue + "</Data></Cell>");
                }
            }
            excelDoc.Write("</Row>");
        }

        excelDoc.Write("</Table>");
        excelDoc.Write(" </Worksheet>");
        excelDoc.Write(endExcelXML);
        excelDoc.Close();
    }
}