using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
public partial class Forms_frmDistributorAssignment2 : System.Web.UI.Page
{
    private void DistributorType()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        DataTable distinctChild = dt.AsEnumerable()
                                   .Where(r => r.Field<int>("SUBZONE_ID") == 2 || r.Field<int>("SUBZONE_ID") == 3)
                                   .CopyToDataTable();


        clsWebFormUtil.FillDropDownList(this.ddDistributorType, distinctChild, 0, 2, true);

    }


    private void LoadUnAssingned()
    {

        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectDistributorAssignment(int.Parse(ddDistributorType.SelectedValue), 6, 2, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(lstUnAssignDistributor, dt, 0, 1, true);
    }
    private void LoadAssingned()
    {

        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectDistributorAssignment(int.Parse(ddDistributorType.SelectedValue), 6, 3, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(lstAssignDistributor, dt, 0, 1, true);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            DistributorType();
            LoadUnAssingned();
            LoadAssingned();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        UserController mUserController = new UserController();
        for (int i = 0; i < lstUnAssignDistributor.Items.Count; i++)
        {
            if (lstUnAssignDistributor.Items[i].Selected == true)
            {
                mUserController.InsertDistributorAssignment(int.Parse(ddDistributorType.SelectedValue.ToString()), int.Parse(lstUnAssignDistributor.Items[i].Value.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            }
        }
        this.LoadUnAssingned();
        this.LoadAssingned();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        UserController mUserController = new UserController();
        for (int i = 0; i < lstAssignDistributor.Items.Count; i++)
        {
            if (lstAssignDistributor.Items[i].Selected == true)
            {
                mUserController.DeleteDistributorAssignment(int.Parse(ddDistributorType.SelectedValue.ToString()), int.Parse(lstAssignDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            }
        }
        LoadUnAssingned();
        LoadAssingned();
    }
    protected void ddUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnAssingned();
        LoadAssingned();
    }
    protected void ddDistributorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnAssingned();
        LoadAssingned();
    }

}
