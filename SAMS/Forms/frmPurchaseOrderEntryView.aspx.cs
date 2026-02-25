using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmPurchaseOrderEntryView : System.Web.UI.Page
{
    readonly PurchaseController _purCtl = new PurchaseController();

   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            GetPurchaseOrder(Constants.LongNullValue, Constants.IntNullValue,
                Convert.ToInt32(Session["UserID"]), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
          
            FillRepeater();
        }
    }

    private void FillRepeater()
    {
        rReserveSample.DataSource = null;
        rReserveSample.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        var pagedResults = new PagedDataSource { AllowPaging = true, PageSize = 20 };

        var dt = (DataTable)Session["dtOrder"];

        DataView view = new DataView(dt);
        DataTable dt2 = view.ToTable(true, "PURCHASE_ORDER_ID", "DISTRIBUTOR_NAME", "DOCUMENT_DATE", "TOTAL_AMOUNT");

        pagedResults.DataSource = dt2.DefaultView;
        if (pagedResults.Count > 0)
        {
            pagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !pagedResults.IsLastPage;
            linkbtnprev.Enabled = !pagedResults.IsFirstPage;
            rReserveSample.DataSource = pagedResults;
            rReserveSample.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dt.Rows.Count.ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / Convert.ToDecimal(20)));

        }
        else if (pagedResults.Count == 0)
        {
            linkbtnnext.Visible = false;
            linkbtnprev.Visible = false;
            lblTotalNoOfPages.Visible = false;
            lblCurrentPageNo.Visible = false;
            lblTotalNoOfRecords.Visible = false;
            lblOf.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
    }

    public int CurrentPage
    {
        get
        {
            object objview = ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            return (int)objview;
        }
        set
        {
            ViewState["_CurrentPage"] = value;
        }
    }

    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        FillRepeater();
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        FillRepeater();
    }

    public DataTable GetPurchaseOrder(long pPurchaseMasterId, int pDistributorId, int pUserId, DateTime pDocumentDate)
    {
        DataTable dtOrder = _purCtl.SelectPurchaseOrderDocumentNo2(pPurchaseMasterId, pDistributorId, 2, pDocumentDate, pUserId);

        Session.Add("dtOrder", dtOrder);
        return dtOrder;
    }

 


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

       
            dt = GetPurchaseOrder(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Constants.DateNullValue);
        

        DataTable dtFilter = dt.Clone();
        DataRow[] foundRows = dt.Select("DISTRIBUTOR_NAME LIKE '%" + txtProduct.Text + "%'");
        if (foundRows.Length > 0)
        {
            foreach (DataRow t in foundRows)
            {
                dtFilter.ImportRow(t);
            }
        }


        Session.Add("dtOrder", dtFilter);
        FillRepeater();
    }
    protected void rReserveSample_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            long orderId = Convert.ToInt64(e.CommandArgument);
            Session.Add("orderId", orderId);

            //  Response.Redirect("frmPurchaseOrderAdd.aspx?LevelType=3&LevelID=150");

            Response.Redirect("frmDelieveryOrderAdd.aspx?LevelType=" + Request.QueryString["LevelType"] + "&LevelID=" + Request.QueryString["LevelID"]);
        }
    }

}