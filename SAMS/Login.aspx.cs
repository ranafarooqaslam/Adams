using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Login : System.Web.UI.Page
{
    UserController mController = new UserController();
    RoleManagementController ObjRole = new RoleManagementController();
    CompanyController cc = new CompanyController();
    readonly DistributorController _mDist = new DistributorController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //try
            //{
            //    if (Convert.ToInt32(Session["UserID"]) > 0)
            //    {
            //        Response.Redirect("Forms/Home.aspx");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    txtLogin.Focus();
            ////}


            //DataTable dtLicenseData = _mDist.GetLicenseData();

            //if (dtLicenseData.Rows.Count > 0)
            //{
            //    if (Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"]) >= 30)
            //    {
            //        Response.Redirect("License.aspx");
            //    }
            //    if (Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"]) >= 25)
            //    {
            //        //lblLicenseMsg.Text = Convert.ToString(70 - Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"]));

            //        lblLicenseMsg.Text = "Software License will expire after " + Convert.ToString(30 - Convert.ToInt32(dtLicenseData.Rows[0]["DAYS"])) + " days. Please Contact to Service Provider.";
            //        dvLicense.Visible = true;
            //    }
            //    else
            //    {
            //        txtLogin.Focus();
            //    }
            //}
            txtLogin.Focus();




        }
    }
    
    public void ValidateUser()
    {

        if (txtLogin.Text == "" && txtPassword.Text == "")
        {
            return;
        }
        else
        {
            DataTable dt = mController.SelectSlashUser(txtLogin.Text, SAMSCommon.Classes.Cryptography.Encrypt(txtPassword.Text, "b0tin@74"));
            Session.Clear();
            if (dt.Rows.Count > 0)
            {
                this.Session.Add("UserID", Convert.ToInt32(dt.Rows[0]["USER_ID"].ToString()));
                this.Session.Add("UserName", dt.Rows[0]["USER_DETAIL"].ToString());
                this.Session.Add("DISTRIBUTOR_ID", Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                this.Session.Add("DistributorId", Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                this.Session.Add("CompanyId", Convert.ToInt32(dt.Rows[0]["COMPANY_ID"].ToString()));
                this.Session.Add("RoleID", Convert.ToInt32(dt.Rows[0]["ROLE_ID"]));
                LastClosedDay(Convert.ToInt32(dt.Rows[0]["USER_ID"]), Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                this.Session.Add("UserName2", dt.Rows[0]["USER_NAME"].ToString());
                this.Session.Add("UserName", dt.Rows[0]["USER_DETAIL"].ToString());
                DataTable dtCompanies = cc.SelectCompany_User(Constants.IntNullValue, Constants.IntNullValue);
                if (dtCompanies != null)
                {
                    if (dtCompanies.Rows.Count > 0)
                    {
                        this.Session.Add("dtCompanies", dtCompanies);
                        this.Session.Add("WorkingCompanyName", dtCompanies.Rows[0]["CompanyName"].ToString());
                        this.Session.Add("WorkingCompanyID", dtCompanies.Rows[0]["CompanyID"].ToString());
                        this.Session.Add("CompanyLogoPath", dtCompanies.Rows[0]["CompanyLogoPath"].ToString());
                    }
                }
                long User_Log_ID = mController.InsertUserLoginTime(Convert.ToInt32(dt.Rows[0]["USER_ID"]));
                this.Session.Add("User_Log_ID", User_Log_ID);
                Response.Redirect("Forms/Home.aspx");
            }
            else
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Wrong User Id/Password";
                return;
            }
        }
    }

    private void LastClosedDay(int UserId, int p_Distributor)
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(UserId, p_Distributor);
        if (dt.Rows.Count > 0)
        {
            this.Session.Add("CurrentWorkDate", DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()));

        }
        else
        {
            this.Session.Add("CurrentWorkDate", DateTime.Now);
        }        
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
       //if (System.Configuration.ConfigurationManager.AppSettings["ComputerInfo"] == SAMSCommon.Classes.Cryptography.Encrypt(SAMSCommon.Classes.ComputerInfo.Value(), "Fast1234"))
       // {
            ValidateUser();
       // }
       // else
        //{
           // lblErrorMsg.Visible = true;
         //   lblErrorMsg.Text = "Invalid Key. Contact to Administrator.";
        //}
    }
}