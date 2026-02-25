using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmChequeEntryView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("dtCustomer");
            Session.Remove("ChequeType");
            Session.Remove("LocationID");
            Session.Remove("PrincipalID");
            Session.Remove("StatusID");
            Session.Remove("TypeID");
            Session.Remove("ChequeID");
            Session.Remove("DateFrom");
            Session.Remove("DateTo");
            Session.Remove("CHEQUE_NO");
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadStatus();
            this.LoadCheque();
            this.LoadCustomer();
        }
    }

    #region Dropdown Selected IndexChanged

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCheque();
        this.LoadCustomer();
    }

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCheque();
        this.LoadCustomer();
       
    }

    protected void DrpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCheque();
    }

    protected void rblChequeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCheque();
        this.LoadCustomer();
    }

    #endregion

    #region Load Dropdowns

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        Session.Add("dtLocationInfo", dt);
    }

    protected void LoadStatus()
    {
        DrpStatus.Items.Add(new ListItem("Cheque Received", "527"));
        DrpStatus.Items.Add(new ListItem("Cheque Deposit", "528"));
        DrpStatus.Items.Add(new ListItem("Cheque Realize", "529"));
        DrpStatus.Items.Add(new ListItem("Cheque Bounce", "530"));
        DrpStatus.Items.Add(new ListItem("Cheque Cancel", "560"));
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        DateTime CurrentWorkDate = Constants.DateNullValue;
        DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
        foreach (DataRow dr in dtLocationInfo.Rows)
        {
            if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
            {
                if (dr["MaxDayClose"].ToString().Length > 0)
                {
                    CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                    break;
                }
            }
        }
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue,
            Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, CurrentWorkDate, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }
 
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        CustomerDataController mController = new CustomerDataController();
        rptCustomers.DataSource = null;
        rptCustomers.DataBind();

        if (drpDistributor.Items.Count > 0)
        {
            if (rblChequeType.SelectedValue == "0")
            {
                LedgerController LedgerCtl = new LedgerController();
                DataTable dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), null, Constants.IntNullValue);
                rptCustomers.DataSource = dtCredit;
                rptCustomers.DataBind();
            }
            else
            {
                LedgerController LedgerCtl = new LedgerController();
                DataTable dtCredit = LedgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), null, 4);
                rptCustomers.DataSource = dtCredit;
                rptCustomers.DataBind();
            }
        }
    }
    
    #endregion 

    /// <summary>
    /// Loads Cheques To Grid
    /// </summary>
    private void LoadCheque()
    {
        rptCheque.DataSource = null;
        rptCheque.DataBind();
        if (DrpStatus.SelectedValue != Constants.Cheque_Clear.ToString())
        {
            DateTime CurrentWorkDate = Constants.DateNullValue;
            DataTable dtLocationInfo = (DataTable)Session["dtLocationInfo"];
            foreach (DataRow dr in dtLocationInfo.Rows)
            {
                if (dr["DISTRIBUTOR_ID"].ToString() == drpDistributor.SelectedValue)
                {
                    if (dr["MaxDayClose"].ToString().Length > 0)
                    {
                        CurrentWorkDate = Convert.ToDateTime(dr["MaxDayClose"]);
                        break;
                    }
                }
            }
            ChequeEntryController CController = new ChequeEntryController();
            DataTable dt = CController.SelectChequeEntry(int.Parse(DrpStatus.SelectedValue.ToString()), CurrentWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToInt32(rblChequeType.SelectedValue));
            rptCheque.DataSource = dt;
            rptCheque.DataBind();
        }
    }
   
    protected void rptCheque_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "editcheque")
        {
            Session.Add("LocationID", drpDistributor.SelectedValue);
            Session.Add("PrincipalID", DrpPrincipal.SelectedValue);
            Session.Add("StatusID", DrpStatus.SelectedValue);
            Session.Add("TypeID", rblChequeType.SelectedValue);
            Session.Add("ChequeID", Convert.ToInt64(e.CommandArgument));
            Response.Redirect("frmChequeEntry.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender.Show();
        DataTable dtCustomer = new DataTable();
        dtCustomer.Columns.Add("CUSTOMER_ID", typeof(long));

        foreach (RepeaterItem item in rptCustomers.Items)
        {
            CheckBox cbCustomer = (CheckBox)item.FindControl("cbCustomer");
            HiddenField hfCUSTOMER_ID = (HiddenField)item.FindControl("hfCUSTOMER_ID");
            if (cbCustomer.Checked)
            {
                DataRow dr = dtCustomer.NewRow();
                dr["CUSTOMER_ID"] = hfCUSTOMER_ID.Value;
                dtCustomer.Rows.Add(dr);
            }
        }

        if (dtCustomer.Rows.Count > 0)
        {
            if (cbDate.Checked)
            {
                Session.Add("DateFrom", txtDateFrom.Text);
                Session.Add("DateTo", txtDateTo.Text);
            }
            else
            {
                Session.Remove("DateFrom");
                Session.Remove("DateTo");
            }
            Session.Add("dtCustomer", dtCustomer);
            Session.Add("LocationID", drpDistributor.SelectedValue);
            Session.Add("PrincipalID", DrpPrincipal.SelectedValue);
            Session.Add("StatusID", DrpStatus.SelectedValue);
            Session.Add("TypeID", rblChequeType.SelectedValue);
            if (rblChequeType.SelectedValue == "1")
            {
                if (dtCustomer.Rows.Count > 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select only one Customer for Cheque Advance');", true);
                    return;
                }
            }
            Response.Redirect("frmChequeEntry.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Customer selected');", true);
        }
    }   
}