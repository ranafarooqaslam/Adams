using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using CORNDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Company Related Tasks
    /// <remarks>
    /// <list type="bullet">
    /// <item>Insert Company</item>
    /// <item>Get Company</item>
    /// </list>
    /// </remarks>
    /// </summary>
	public class CompanyController
	{

        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
		public CompanyController()
		{
			//
			// TODO: Add constructor logic here
			//
		}


        public DataTable SelectCompany(int p_COMPANY_ID, int p_STATUS)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCOMPANY mdtCompany = new spSelectCOMPANY();
                mdtCompany.Connection = mConnection;
                mdtCompany.STATUS = p_STATUS;
                mdtCompany.COMPANY_ID = p_COMPANY_ID;
                mdtCompany.ISCURRENT = true;
                mdtCompany.ISDELETED = false;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Inserts Company
        /// </summary>
        /// <remarks>
        /// Returns Inserted Company ID as String
        /// </remarks>
        /// <param name="p_ISCURRENT">IsCurrent</param>
        /// <param name="p_ISDELETED">IsDeleted</param>
        /// <param name="p_COMPANY_ID">Company</param>
        /// <param name="p_STATUS">Status</param>
        /// <param name="p_EMAIL_ADDRESS">Email</param>
        /// <param name="p_PHONE">Phone</param>
        /// <param name="p_FAX">Fax</param>
        /// <param name="p_WEBSITE">Website</param>
        /// <param name="p_COMPANY_NAME">Name</param>
        /// <param name="p_ADDRESS1">Address1</param>
        /// <param name="p_ADDRESS2">Address2</param>
        /// <returns>Inserted Company ID as String</returns>
        public string InsertDTCompany2(bool p_ISCURRENT, bool p_ISDELETED, int p_COMPANY_ID, int p_STATUS, string p_EMAIL_ADDRESS, string p_PHONE, string p_FAX, string p_WEBSITE, string p_COMPANY_NAME, string p_ADDRESS1, string p_ADDRESS2)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDTCOMPANY mdtCompany = new spInsertDTCOMPANY();
                mdtCompany.Connection = mConnection;

                mdtCompany.ISCURRENT = p_ISCURRENT;
                mdtCompany.ISDELETED = p_ISDELETED;
                mdtCompany.COMPANY_ID = p_COMPANY_ID;
                mdtCompany.STATUS = p_STATUS;
                mdtCompany.EMAIL_ADDRESS = p_EMAIL_ADDRESS;
                mdtCompany.PHONE = p_PHONE;
                mdtCompany.FAX = p_FAX;
                mdtCompany.WEBSITE = p_WEBSITE;
                mdtCompany.COMPANY_NAME = p_COMPANY_NAME;
                mdtCompany.ADDRESS1 = p_ADDRESS1;
                mdtCompany.ADDRESS2 = p_ADDRESS2;
                mdtCompany.ExecuteQuery();

                return mdtCompany.COMPANY_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public int InsertCompany(int p_COMPANY_GROUP_ID, string p_CompanyName, string p_CompanyTagline, string p_CompanyLogoPath, string p_CompanyServiceTaxNumber, string p_CompanyNTNnumber, string p_CompanyGSTNumber, string p_CompanyUAN, string p_CompanyURL, string p_CompanyChamberRefNumber, string p_CompanyImportExportLicenseNumber, string p_CompanyFormFooter, string p_CompanyReportFooter, int p_PayPeriodFrom, int p_PayPeriodTo, bool p_IS_ACTIVE, int p_User_ID, DateTime p_Document_Date, DateTime p_FiscalYearFrom, DateTime p_FiscalYearTo)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsert_COMPANY mAccountHead = new spInsert_COMPANY();
                mAccountHead.Connection = mConnection;
                mAccountHead.COMPANY_GROUP_ID = p_COMPANY_GROUP_ID;
                mAccountHead.CompanyChamberRefNumber = p_CompanyChamberRefNumber;
                mAccountHead.CompanyFormFooter = p_CompanyFormFooter;
                mAccountHead.CompanyGSTNumber = p_CompanyGSTNumber;
                mAccountHead.CompanyImportExportLicenseNumber = p_CompanyImportExportLicenseNumber;
                mAccountHead.CompanyLogoPath = p_CompanyLogoPath;
                mAccountHead.CompanyName = p_CompanyName;
                mAccountHead.CompanyNTNnumber = p_CompanyNTNnumber;
                mAccountHead.CompanyReportFooter = p_CompanyReportFooter;
                mAccountHead.CompanyServiceTaxNumber = p_CompanyServiceTaxNumber;
                mAccountHead.CompanyTagline = p_CompanyTagline;
                mAccountHead.CompanyUAN = p_CompanyUAN;
                mAccountHead.CompanyURL = p_CompanyURL;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.PayPeriodFrom = p_PayPeriodFrom;
                mAccountHead.PayPeriodTo = p_PayPeriodTo;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.FiscalYearFrom = p_FiscalYearFrom;
                mAccountHead.FiscalYearTo = p_FiscalYearTo;
                int ComapnyID = mAccountHead.ExecuteQuery();
                return ComapnyID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
      




        /// <summary>
        /// Gets Company Data
        /// </summary>
        /// <remarks>
        /// Returns Company Data as Datatable
        /// </remarks>
        /// <param name="p_COMPANY_ID">Company</param>
        /// <param name="p_STATUS">Status</param>
        /// <returns>Company Data as Datatable</returns>
        public DataTable SelectCompany(int p_COMPANY_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCOMPANY mdtCompany = new spSelectCOMPANY();
                mdtCompany.Connection = mConnection;
                mdtCompany.COMPANY_ID = p_COMPANY_ID; 
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SelectCompany_User(int p_COMPANY_ID, int p_UserID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCOMPANY_USER mdtCompany = new spSelectCOMPANY_USER();
                mdtCompany.Connection = mConnection;
                mdtCompany.COMPANY_ID = p_COMPANY_ID;
                mdtCompany.User_ID = p_UserID;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectCompanyContact(int p_CompanyContactID,int p_CompanyID, bool p_IS_DELETED, bool p_IS_ACTIVE ,int  p_User_ID )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCompanyContact mdtCompany = new spSelectCompanyContact();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyContactID = p_CompanyContactID;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.IS_ACTIVE = p_IS_ACTIVE;
                mdtCompany.IS_DELETED = p_IS_DELETED;
                mdtCompany.User_ID = p_User_ID;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectCompanyBankInfo(int  p_CompanyBankInfoID,int p_CompanyID ,int p_User_ID,bool p_S_ACTIVE,bool p_IS_DELETED)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCompanyBankInfo mdtCompany = new spSelectCompanyBankInfo();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyBankInfoID = p_CompanyBankInfoID;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.IS_ACTIVE = p_S_ACTIVE;
                mdtCompany.IS_DELETED = p_IS_DELETED;
                mdtCompany.User_ID = p_User_ID;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectCompany_lOCATIONS(int p_COMPANY_ID, int p_lOCATIONID,int p_UerID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCompany_Locations mdtCompany = new spSelectCompany_Locations();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_COMPANY_ID;
                mdtCompany.CompanyLocationID=p_lOCATIONID;
                mdtCompany.User_ID = p_UerID;
                mdtCompany.IS_ACTIVE=true;
                mdtCompany.IS_DELETED=false;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SelectCompany_Units(int p_UnitID, int p_CompanyLocationID, int p_CompanyID, int p_User_ID, bool p_IS_ACTIVE, bool p_IS_DELETED)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCompany_Units mdtCompany = new spSelectCompany_Units();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.CompanyLocationID = p_CompanyLocationID;
                mdtCompany.IS_ACTIVE = p_IS_ACTIVE;
                mdtCompany.IS_DELETED = p_IS_DELETED;
                mdtCompany.UnitID = p_UnitID;
                mdtCompany.User_ID = p_User_ID;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectOffDays(int p_OffDayID,int p_CompanyID,int p_OffDayValue)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectOffDays mdtCompany = new spSelectOffDays();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.OffDayID = p_OffDayID;
                mdtCompany.OffDayValue = p_OffDayValue;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        /// <summary>
        /// Inserts Company
        /// </summary>
        /// <remarks>
        /// Returns Inserted Company ID as String
        /// </remarks>
        /// <param name="p_ISCURRENT">IsCurrent</param>
        /// <param name="p_ISDELETED">IsDeleted</param>
        /// <param name="p_COMPANY_ID">Company</param>
        /// <param name="p_STATUS">Status</param>
        /// <param name="p_EMAIL_ADDRESS">Email</param>
        /// <param name="p_PHONE">Phone</param>
        /// <param name="p_FAX">Fax</param>
        /// <param name="p_WEBSITE">Website</param>
        /// <param name="p_COMPANY_NAME">Name</param>
        /// <param name="p_ADDRESS1">Address1</param>
        /// <param name="p_ADDRESS2">Address2</param>
        /// <returns>Inserted Company ID as String</returns>
		public string InsertDTCompany(bool p_ISCURRENT,bool p_ISDELETED,int p_COMPANY_ID,int p_STATUS,string p_EMAIL_ADDRESS,string p_PHONE,string p_FAX,string p_WEBSITE,string p_COMPANY_NAME,string p_ADDRESS1,string p_ADDRESS2)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spInsertDTCOMPANY mdtCompany = new spInsertDTCOMPANY();
				mdtCompany.Connection = mConnection;
				
				mdtCompany.ISCURRENT = p_ISCURRENT;
				mdtCompany.ISDELETED = p_ISDELETED ;
				mdtCompany.COMPANY_ID = p_COMPANY_ID;
				mdtCompany.STATUS = p_STATUS;
				mdtCompany.EMAIL_ADDRESS = p_EMAIL_ADDRESS;
				mdtCompany.PHONE = p_PHONE;
				mdtCompany.FAX = p_FAX;
				mdtCompany.WEBSITE = p_WEBSITE;
				mdtCompany.COMPANY_NAME = p_COMPANY_NAME;
				mdtCompany.ADDRESS1 = p_ADDRESS1;
				mdtCompany.ADDRESS2 = p_ADDRESS2;
				mdtCompany.ExecuteQuery();
				
				return mdtCompany.COMPANY_ID.ToString();
				
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return null;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
			
		}
       public int InsertCompanyContact(int p_CompanyID	,string p_Phone,string p_Fax,string p_Email,string p_AddressL1,string p_AddressL2,string p_City,string p_Country,bool p_IS_ACTIVE,int p_User_ID,DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsert_COMPANYCONTACT mAccountHead = new spInsert_COMPANYCONTACT();
                mAccountHead.Connection = mConnection;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.AddressL1 = p_AddressL1;
                mAccountHead.AddressL2 = p_AddressL2;
                mAccountHead.City = p_City;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.Country = p_Country;
                mAccountHead.Email = p_Email;
                mAccountHead.Fax = p_Fax;
                mAccountHead.Phone = p_Phone;
                int ComapnyContactID = mAccountHead.ExecuteQuery();
                return ComapnyContactID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public int InsertCompanyBankInfo(int p_CompanyID  ,string  p_BankAccountNumber ,string p_BankName ,string p_BankAccountType,string p_BankCode,string p_BankBranch,string p_ContactPerson,int p_User_ID,DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCompanyBankInfo mAccountHead = new spInsertCompanyBankInfo();
                mAccountHead.Connection = mConnection;
                mAccountHead.BankAccountNumber = p_BankAccountNumber;
                mAccountHead.BankAccountType = p_BankAccountType;
                mAccountHead.BankBranch = p_BankBranch;
                mAccountHead.BankCode = p_BankCode;
                mAccountHead.BankName = p_BankName;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.ContactPerson = p_ContactPerson;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.User_ID = p_User_ID;
                int ComapnyContactID = mAccountHead.ExecuteQuery();
                return ComapnyContactID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public int InsertCompany_Locations(int p_CompanyID, string p_LocationName, int p_User_ID, DateTime p_Document_Date,bool p_IsActive)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCompany_Locations mAccountHead = new spInsertCompany_Locations();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.LocationName = p_LocationName;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.IS_ACTIVE = p_IsActive;
                int ComapnyContactID = mAccountHead.ExecuteQuery();
                return ComapnyContactID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public int InsertCompany_Units(int p_CompanyLocationID ,int p_CompanyID ,string p_UnitName,int p_User_ID ,DateTime p_Document_Date,bool p_ISActive)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCompany_Units mAccountHead = new spInsertCompany_Units();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.CompanyLocationID = p_CompanyLocationID;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.UnitName = p_UnitName;
                mAccountHead.User_ID = p_User_ID;
                return mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public int InsertOffDays(int p_CompanyID ,int p_OffDayValue,string p_OffDayName)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertOffDays mAccountHead = new spInsertOffDays();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.OffDayName = p_OffDayName;
                mAccountHead.OffDayValue = p_OffDayValue;
                return mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public void UpdateCOMPANY(int p_CompanyID ,int p_COMPANY_GROUP_ID,string p_CompanyName ,string  p_CompanyTagline,string p_CompanyLogoPath,string p_CompanyServiceTaxNumber,string p_CompanyNTNnumber,string p_CompanyGSTNumber,string p_CompanyUAN,string p_CompanyURL,string p_CompanyChamberRefNumber,string p_CompanyImportExportLicenseNumber ,string p_CompanyFormFooter,string p_CompanyReportFooter,int p_PayPeriodFrom,int p_PayPeriodTo ,bool p_IS_DELETED,bool p_IS_ACTIVE ,int p_User_ID,DateTime p_Document_Date
            , DateTime p_FiscalYearFrom, DateTime p_FiscalYearTo)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCOMPANY mAccountHead = new spUpdateCOMPANY();
                mAccountHead.Connection = mConnection;
                mAccountHead.COMPANY_GROUP_ID = p_COMPANY_GROUP_ID;
                mAccountHead.CompanyChamberRefNumber = p_CompanyChamberRefNumber;
                mAccountHead.CompanyFormFooter = p_CompanyFormFooter;
                mAccountHead.CompanyGSTNumber = p_CompanyGSTNumber;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.CompanyImportExportLicenseNumber = p_CompanyImportExportLicenseNumber;
                mAccountHead.CompanyLogoPath = p_CompanyLogoPath;
                mAccountHead.CompanyName = p_CompanyName;
                mAccountHead.CompanyNTNnumber = p_CompanyNTNnumber;
                mAccountHead.CompanyReportFooter = p_CompanyReportFooter;
                mAccountHead.CompanyServiceTaxNumber = p_CompanyServiceTaxNumber;
                mAccountHead.CompanyTagline = p_CompanyTagline;
                mAccountHead.CompanyUAN = p_CompanyUAN;
                mAccountHead.CompanyURL = p_CompanyURL;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.PayPeriodFrom = p_PayPeriodFrom;
                mAccountHead.PayPeriodTo = p_PayPeriodTo;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.FiscalYearFrom = p_FiscalYearFrom;
                mAccountHead.FiscalYearTo = p_FiscalYearTo;
                mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public void UpdateCompanyContact(int p_CompanyContactID,int p_CompanyID,string p_Phone,string p_Fax ,string p_Email,string p_AddressL1 ,string p_AddressL2,string p_City ,string p_Country,bool p_IS_DELETED,bool p_IS_ACTIVE,int p_User_ID)
         {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCompanyContact mAccountHead = new spUpdateCompanyContact();
                mAccountHead.Connection = mConnection;
                mAccountHead.AddressL1 = p_AddressL1;
                mAccountHead.AddressL2 = p_AddressL2;
                mAccountHead.City = p_City;
                mAccountHead.CompanyContactID = p_CompanyContactID;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.Country = p_Country;
                mAccountHead.Email = p_Email;
                mAccountHead.Fax = p_Fax;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.Phone = p_Phone;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public void UpdateCompanyBankInfo(int p_CompanyBankInfoID,int p_CompanyID,string p_BankAccountNumber ,string p_BankName, string p_BankAccountType,string p_BankCode,string p_BankBranch ,string p_ContactPerson ,int p_User_ID,bool p_IS_ACTIVE,bool p_IS_DELETED)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCompanyBankInfo mAccountHead = new spUpdateCompanyBankInfo();
                mAccountHead.Connection = mConnection;
                mAccountHead.BankAccountNumber = p_BankAccountNumber;
                mAccountHead.BankAccountType = p_BankAccountType;
                mAccountHead.BankBranch = p_BankBranch;
                mAccountHead.BankCode = p_BankCode;
                mAccountHead.BankName = p_BankName;
                mAccountHead.CompanyBankInfoID = p_CompanyBankInfoID;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.ContactPerson = p_ContactPerson;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public void UpdateCompany_Locations(int p_CompanyLocationID,int p_CompanyID,string p_LocationName,int  p_User_ID,bool p_IS_ACTIVE,bool p_IS_DELETED)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCompany_Locations mAccountHead = new spUpdateCompany_Locations();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.CompanyLocationID = p_CompanyLocationID;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.LocationName = p_LocationName;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public void UpdateCompany_Units(int  p_UnitID ,int p_CompanyLocationID,int  p_CompanyID,string  p_UnitName,int  p_User_ID ,bool p_IS_ACTIVE ,bool  p_IS_DELETED)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCompany_Units mAccountHead = new spUpdateCompany_Units();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.CompanyLocationID = p_CompanyLocationID;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.UnitID = p_UnitID;
                mAccountHead.UnitName = p_UnitName;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public void DeleteOffDays(int p_OffDayID,  int p_CompanyID,int p_OffDaysValue)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteOffDays mAccountHead = new spDeleteOffDays();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.OffDayID = p_OffDayID;
                mAccountHead.OffDaysValue = p_OffDaysValue;
                mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }



        public bool InsertGazettedHoliday(DateTime p_Date, string p_Description, int p_UserID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertGazettedHoliday mGazetted = new spInsertGazettedHoliday();
                mGazetted.Connection = mConnection;
                mGazetted.Date = p_Date;
                mGazetted.Description = p_Description;
                mGazetted.UserID = p_UserID;
                return mGazetted.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdateGazettedHoliday(int p_GazettedHolidayID, DateTime p_Date, string p_Description, int p_UserID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateGazettedHoliday mGazetted = new spUpdateGazettedHoliday();
                mGazetted.Connection = mConnection;
                mGazetted.Date = p_Date;
                mGazetted.Description = p_Description;
                mGazetted.UserID = p_UserID;
                mGazetted.GazettedHolidayID = p_GazettedHolidayID;
                return mGazetted.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool DeleteGazettedHoliday(int p_GazettedHolidayID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteGazettedHoliday mGazetted = new spDeleteGazettedHoliday();
                mGazetted.Connection = mConnection;
                mGazetted.GazettedHolidayID = p_GazettedHolidayID;
                return mGazetted.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable GetGazettedHoliday(int p_GazettedHolidayID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetGazettedHoliday mGazetted = new spGetGazettedHoliday();
                mGazetted.Connection = mConnection;
                mGazetted.GazettedHolidayID = p_GazettedHolidayID;
                DataTable dt = mGazetted.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable GetFBRIntegration(int p_DistributorID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetFBRIntegration mdtCompany = new uspGetFBRIntegration();
                mdtCompany.Connection = mConnection;
                mdtCompany.DistributorID = p_DistributorID;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
    }
}
