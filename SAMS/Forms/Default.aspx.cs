using System;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using SAMSCommon.Classes;
using CrystalDecisions.Shared;
using System.Web;
using System.Threading;

public partial class Forms_Default : System.Web.UI.Page
{
    ReportDocument CrpReport = null;
    System.IO.Stream oStream = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument CrpReport = (ReportDocument)this.Session["CrpReport"];
        Stream oStream = null;

        try
        {
            if (int.Parse(this.Session["ReportType"].ToString()) == 0)
            {
                oStream = CrpReport.ExportToStream(ExportFormatType.PortableDocFormat);

                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length));

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
            }
            else
            {
                string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath() + "\\Exported.xls";
                CrpReport.ExportToDisk(ExportFormatType.Excel, path);

                FileInfo file = new FileInfo(path);

                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.Flush();
                }
            }

            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (ThreadAbortException)
        {
            // Ignore - caused by Response handling
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);
        }
        finally
        {
            if (CrpReport != null)
            {
                CrpReport.Close();
                CrpReport.Dispose();
            }

            if (oStream != null)
            {
                oStream.Close();
                oStream.Dispose();
            }

            Session.Remove("CrpReport");
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        try
        {
            if (this.CrpReport != null)
            {
                CrpReport.Close();
                CrpReport.Dispose();
            }
            if (this.oStream != null)
            {
                oStream.Flush();
                oStream.Close();
                oStream.Dispose();
                
            }
            GC.Collect();
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);
        }
    }

}