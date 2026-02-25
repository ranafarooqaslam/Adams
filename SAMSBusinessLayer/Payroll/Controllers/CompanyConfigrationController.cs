using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

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
	public class CompanyConfigrationController
	{

        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
		public CompanyConfigrationController()
		{
			//
			// TODO: Add constructor logic here
			//
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
        public DataTable SELECT_ParentDepartments(int p_CompanyID,int p_DepartmentID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSELECT_ParentDepartments mdtCompany = new spSELECT_ParentDepartments();
                mdtCompany.Connection = mConnection;
                mdtCompany.COMMPANY_ID = p_CompanyID;
                mdtCompany.DepartmentID = p_DepartmentID;
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
        public DataTable SelectDesignation(int p_CompanyID, int p_DesignationID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDesignation mdtCompany = new spSelectDesignation();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.DesignationID = p_DesignationID;
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
        public DataTable SelectAllowances(int p_allowanceID,int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectAllowances mdtCompany = new spSelectAllowances();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.AllowanceID = p_allowanceID;
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
        public DataTable SelectDeductions(int p_DeductionID, int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDeductions mdtCompany = new spSelectDeductions();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.DeductionID = p_DeductionID;
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
        public DataTable SelectShift(int p_ShiftID,DateTime p_ShiftTimeFrom, DateTime p_ShiftTimeTo, int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectShift mdtCompany = new spSelectShift();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.ShiftID = p_ShiftID;
                mdtCompany.ShiftTimeFrom = p_ShiftTimeFrom;
                mdtCompany.ShiftTimeTo = p_ShiftTimeTo;
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
        public DataTable SelectShift(int p_ShiftID, DateTime p_ShiftTimeFrom, DateTime p_ShiftTimeTo, int p_CompanyID, int p_EmployeeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectShift mdtCompany = new spSelectShift();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.ShiftID = p_ShiftID;
                mdtCompany.ShiftTimeFrom = p_ShiftTimeFrom;
                mdtCompany.ShiftTimeTo = p_ShiftTimeTo;
                mdtCompany.EmployeeID = p_EmployeeID;
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
        public DataTable SelectLeave(int p_LeaveID, int p_CompanyID )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectLeave mdtCompany = new spSelectLeave();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.LeaveID = p_LeaveID;
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
        public DataTable SelectLeavePaymentType(int p_LeavePaymentTypeID,int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSELECTLeavePaymentType mdtCompany = new spSELECTLeavePaymentType();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.LeavePaymentTypeID = p_LeavePaymentTypeID;
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
        public DataTable SelectWorkHoursException(int p_ExceptionID ,int p_CompanyID,int p_ExceptionTypeID )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectWorkHoursException mdtCompany = new spSelectWorkHoursException();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.ExceptionID = p_ExceptionID;
                mdtCompany.ExceptionTypeID = p_ExceptionTypeID;
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
        public DataTable SelectSalaryStructure( int p_SalaryStructureID ,string p_SalaryStructureName ,int p_CompanyID, int p_SalaryStructureTypeID, decimal p_BasicPay ,decimal p_NetPay ,int p_User_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSalaryStructure mdtCompany = new spSelectSalaryStructure();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.BasicPay = p_BasicPay;
                mdtCompany.NetPay = p_NetPay;
                mdtCompany.SalaryStructureID = p_SalaryStructureID;
                mdtCompany.SalaryStructureName = p_SalaryStructureName;
                mdtCompany.SalaryStructureTypeID = p_SalaryStructureTypeID;
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
        public DataTable SelectSalaryStructureMaster(int p_SalaryStructureID ,int p_CompanyID ,int p_SalaryStructureTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSalaryStructureMaster mdtCompany = new spSelectSalaryStructureMaster();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.SalaryStructureID = p_SalaryStructureID;
                mdtCompany.SalaryStructureTypeID = p_SalaryStructureTypeID;
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
        public int InsertDepartment(string p_DepartmentName ,int p_ParentDepartment,int p_CompanyID,int p_User_ID,DateTime p_Document_Date )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDepartment mAccountHead = new spInsertDepartment();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.DepartmentName = p_DepartmentName;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.ParentDepartment = p_ParentDepartment;
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
        public bool UpdateDepartment(int p_DepartmentID, string p_DepartmentName, int p_ParentDepartment, int p_CompanyID, bool p_IS_DELETED, bool p_IS_ACTIVE, int p_User_ID, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateDepartment mAccountHead = new spUpdateDepartment();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.DepartmentID = p_DepartmentID;
                mAccountHead.DepartmentName = p_DepartmentName;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.ParentDepartment = p_ParentDepartment;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
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
        public int InsertDesignation(string p_DesignationName ,int p_CompanyID,int p_User_ID,DateTime  p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDesignation mAccountHead = new spInsertDesignation();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.DesignationName = p_DesignationName;
                mAccountHead.Document_Date = p_Document_Date;
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
        public bool UpdateDesignation(int p_DesignationID ,string p_DesignationName,int p_CompanyID,bool p_IS_DELETED,bool p_IS_ACTIVE,int p_User_ID,DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateDesignation mAccountHead = new spUpdateDesignation();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.DesignationID = p_DesignationID;
                mAccountHead.DesignationName = p_DesignationName;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
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
        public int InsertAllowances(int p_CompanyID ,string p_AllowanceDescription,decimal p_AllowanceRatio,int p_RatioType,int p_User_ID ,DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertAllowances mAccountHead = new spInsertAllowances();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.AllowanceDescription = p_AllowanceDescription;
                mAccountHead.AllowanceRatio = p_AllowanceRatio;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.RatioType = p_RatioType;
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
        public bool UpdateAllowances(int p_AllowanceID ,int p_CompanyID, string p_AllowanceDescription,decimal p_AllowanceRatio,int p_RatioType,bool p_IS_DELETED,bool p_IS_ACTIVE,int p_User_ID )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateAllowances mAccountHead = new spUpdateAllowances();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.AllowanceDescription = p_AllowanceDescription;
                mAccountHead.AllowanceID = p_AllowanceID;
                mAccountHead.AllowanceRatio = p_AllowanceRatio;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.RatioType = p_RatioType;
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
        public int InsertDeductions(int p_CompanyID, string p_DeductionDescription, decimal p_DeductionRatio, int p_RatioType, int p_User_ID, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDeductions mAccountHead = new spInsertDeductions();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.DeductionDescription = p_DeductionDescription;
                mAccountHead.DeductionRatio = p_DeductionRatio;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.RatioType = p_RatioType;
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
        public bool UpdateDeductions(int p_DeductionID, int p_CompanyID, string p_DeductionDescription, decimal p_DeductionRatio, int p_RatioType, bool p_IS_DELETED, bool p_IS_ACTIVE, int p_User_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateDeductions mAccountHead = new spUpdateDeductions();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.DeductionDescription = p_DeductionDescription;
                mAccountHead.DeductionID =p_DeductionID;
                mAccountHead.DeductionRatio = p_DeductionRatio;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.RatioType = p_RatioType;
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
        public int InsertShift(string p_ShiftName, DateTime p_ShiftTimeFrom, DateTime p_ShiftTimeTo, int p_CompanyID, bool p_IS_ACTIVE, int p_User_ID, DateTime p_Document_Date, int p_LateMinutes)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertShift mAccountHead = new spInsertShift();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.ShiftName = p_ShiftName;
                mAccountHead.ShiftTimeFrom = p_ShiftTimeFrom;
                mAccountHead.ShiftTimeTo = p_ShiftTimeTo;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.LateMinutes = p_LateMinutes;
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
        public bool UpdateShift(int p_ShiftID, string p_ShiftName, DateTime p_ShiftTimeFrom, DateTime p_ShiftTimeTo, int p_CompanyID, bool p_IS_DELETED, bool p_IS_ACTIVE, int p_User_ID, int p_LateMinutes)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateShift mAccountHead = new spUpdateShift();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.ShiftID = p_ShiftID;
                mAccountHead.ShiftName = p_ShiftName;
                mAccountHead.ShiftTimeFrom = p_ShiftTimeFrom;
                mAccountHead.ShiftTimeTo = p_ShiftTimeTo;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.LateMinutes = p_LateMinutes;
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
        public int InsertLeave(string p_LeaveType, int p_CompanyID, int p_MaximumAllowed, int p_LeavePaymentTypeID, bool p_OverRide, int p_User_ID, DateTime p_Document_Date, int p_LATE, int p_SHORT, int p_HALF)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertLeave mAccountHead = new spInsertLeave();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.LeavePaymentTypeID = p_LeavePaymentTypeID;
                mAccountHead.LeaveType = p_LeaveType;
                mAccountHead.MaximumAllowed = p_MaximumAllowed;
                mAccountHead.OverRide = p_OverRide;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.LATE = p_LATE;
                mAccountHead.SHORT = p_SHORT;
                mAccountHead.HALF = p_HALF;
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
        public bool UpdateLeave(int p_LeaveID, string p_LeaveType, int p_CompanyID, int p_MaximumAllowed, int p_LeavePaymentTypeID, bool p_OverRide, bool p_IS_DELETED, bool p_IS_ACTIVE, int p_User_ID, int p_LATE, int p_SHORT, int p_HALF)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateLeave mAccountHead = new spUpdateLeave();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.LeavePaymentTypeID = p_LeavePaymentTypeID;
                mAccountHead.LeaveID = p_LeaveID;
                mAccountHead.LeaveType = p_LeaveType;
                mAccountHead.MaximumAllowed = p_MaximumAllowed;
                mAccountHead.OverRide = p_OverRide;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.LATE = p_LATE;
                mAccountHead.SHORT = p_SHORT;
                mAccountHead.HALF = p_HALF;
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
        public int InsertWorkHoursException(int p_CompanyID ,int p_ExceptionTypeID,string p_ExceptionDescription,decimal p_NumberOfHours,int p_User_ID,DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertWorkHoursException mAccountHead = new spInsertWorkHoursException();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.ExceptionDescription = p_ExceptionDescription;
                mAccountHead.ExceptionTypeID = p_ExceptionTypeID;
                mAccountHead.NumberOfHours = p_NumberOfHours;
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
        public bool UpdateWorkHoursException(int p_ExceptionID,int p_CompanyID,int p_ExceptionTypeID,string p_ExceptionDescription,decimal p_NumberOfHours,bool  p_IS_DELETED,bool p_IS_ACTIVE ,int  p_User_ID )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateWorkHoursException mAccountHead = new spUpdateWorkHoursException();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.ExceptionDescription = p_ExceptionDescription;
                mAccountHead.ExceptionID = p_ExceptionID;
                mAccountHead.ExceptionTypeID = p_ExceptionTypeID;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.NumberOfHours = p_NumberOfHours;
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
        public int InsertSalaryStructureMaster(string p_SalaryStructureName,int p_CompanyID ,int p_SalaryStructureTypeID,decimal p_BasicPay,decimal p_NetPay ,int p_User_ID ,DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSalaryStructureMaster mAccountHead = new spInsertSalaryStructureMaster();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.BasicPay = p_BasicPay;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.NetPay = p_NetPay;
                mAccountHead.SalaryStructureName = p_SalaryStructureName;
                mAccountHead.SalaryStructureTypeID = p_SalaryStructureTypeID;
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
        public int InsertSalaryStructureArchiveMaster(int p_SalaryStructureID ,string p_SalaryStructureName, int p_CompanyID, int p_SalaryStructureTypeID, decimal p_BasicPay, decimal p_NetPay, int p_User_ID, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSalaryStructureArchiveMaster mAccountHead = new spInsertSalaryStructureArchiveMaster();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.BasicPay = p_BasicPay;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.NetPay = p_NetPay;
                mAccountHead.SalaryStructureName = p_SalaryStructureName;
                mAccountHead.SalaryStructureTypeID = p_SalaryStructureTypeID;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.SalaryStructureID = p_SalaryStructureID;
                mAccountHead.IS_ACTIVE = true;
                mAccountHead.IS_DELETED = false;
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
        public bool UpdateSalaryStructureMaster(int p_SalaryStructureID,string  p_SalaryStructureName,int p_CompanyID ,int p_SalaryStructureTypeID ,decimal p_BasicPay,decimal p_NetPay,bool p_IS_DELETED ,bool p_IS_ACTIVE, int p_User_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSalaryStructureMaster mAccountHead = new spUpdateSalaryStructureMaster();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.BasicPay = p_BasicPay;
                mAccountHead.NetPay = p_NetPay;
                mAccountHead.SalaryStructureName = p_SalaryStructureName;
                mAccountHead.SalaryStructureTypeID = p_SalaryStructureTypeID;
                mAccountHead.User_ID = p_User_ID;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.SalaryStructureID = p_SalaryStructureID;
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
        public int InsertSalaryStructureDetail(int p_SalaryStructureID, int p_AllowanceID ,int p_DeductionID ,decimal p_Amount ,string p_Comment, int p_AmountTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSalaryStructureDetail mAccountHead = new spInsertSalaryStructureDetail();
                mAccountHead.Connection = mConnection;
                mAccountHead.AllowanceID = p_AllowanceID;
                mAccountHead.Amount = p_Amount;
                mAccountHead.AmountTypeID = p_AmountTypeID;
                mAccountHead.Comment = p_Comment;
                mAccountHead.DeductionID = p_DeductionID;
                mAccountHead.SalaryStructureID = p_SalaryStructureID;
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
        public int InsertSalaryStructureArchiveDetail(int p_SalaryStructureDetailID, int p_SalaryStructureID, int p_AllowanceID, int p_DeductionID, decimal p_Amount, string p_Comment, int p_AmountTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSalaryStructureArchiveDetail mAccountHead = new spInsertSalaryStructureArchiveDetail();
                mAccountHead.Connection = mConnection;
                mAccountHead.AllowanceID = p_AllowanceID;
                mAccountHead.Amount = p_Amount;
                mAccountHead.AmountTypeID = p_AmountTypeID;
                mAccountHead.Comment = p_Comment;
                mAccountHead.DeductionID = p_DeductionID;
                mAccountHead.SalaryStructureID = p_SalaryStructureID;
                mAccountHead.SalaryStructureDetailID = p_SalaryStructureDetailID; 
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
        public bool DeleteSalaryStructureDetail(int p_SalaryStructureID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteSalaryStructureDetail mAccountHead = new spDeleteSalaryStructureDetail();
                mAccountHead.Connection = mConnection;
                mAccountHead.SalaryStructureID = p_SalaryStructureID;
                 mAccountHead.ExecuteQuery();
                 return true;
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

        #region Provident Fund

        public bool InsertCompanyPF(int p_CompanyID, DateTime p_DocumentDate, decimal p_MaximumAmount, decimal p_Percentage, int p_UserID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCompanyPF mPF = new spInsertCompanyPF();
                mPF.Connection = mConnection;
                mPF.CompanyID = p_CompanyID;
                mPF.DocumentDate = p_DocumentDate;
                mPF.MaximumAmount = p_MaximumAmount;
                mPF.Percentage = p_Percentage;
                mPF.UserID = p_UserID;
                return mPF.ExecuteQuery();
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

        public DataTable GetCompanyPF(int p_PFID, int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCompanyPF mdtCompany = new spSelectCompanyPF();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.PFID = p_PFID;
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

        #endregion

        #region Delete Configuration

        public DataTable DeleteCompanyConfiguration(int p_CONFIGURATION_TYPE, int p_CONFIGURATION_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteCompanyConfiguration mdtCompany = new spDeleteCompanyConfiguration();
                mdtCompany.Connection = mConnection;
                mdtCompany.CONFIGURATION_TYPE = p_CONFIGURATION_TYPE;
                mdtCompany.CONFIGURATION_ID = p_CONFIGURATION_ID;
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

        #endregion
    }
}