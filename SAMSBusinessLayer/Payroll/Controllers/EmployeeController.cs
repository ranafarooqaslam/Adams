using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using SAMSBusinessLayer.Models;
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
    public class EmployeeController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public EmployeeController()
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
        public DataTable SelectEmployee(long p_EmployeeID, int p_CompanyID, int p_DesignationID, int p_DepartmentID, int p_EmployeeLocationID, bool p_EmployeeIsManager)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEmployee objEmployee = new spSelectEmployee();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.DepartmentID = p_DepartmentID;
                objEmployee.DesignationID = p_DesignationID;
                objEmployee.EmployeeLocationID = p_EmployeeLocationID;
                objEmployee.EmployeeIsManager = p_EmployeeIsManager;
                DataTable dt = objEmployee.ExecuteTable();
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
        public DataTable SelectEmployee(long p_EmployeeID, int p_CompanyID, int p_DesignationID, int p_DepartmentID, int p_EmployeeLocationID, bool p_EmployeeIsManager,int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEmployee objEmployee = new spSelectEmployee();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.DepartmentID = p_DepartmentID;
                objEmployee.DesignationID = p_DesignationID;
                objEmployee.EmployeeLocationID = p_EmployeeLocationID;
                objEmployee.EmployeeIsManager = p_EmployeeIsManager;
                objEmployee.TYPE_ID = p_TYPE_ID;
                DataTable dt = objEmployee.ExecuteTable();
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
        public DataTable SelectEmployee(long p_EmployeeID, int p_CompanyID, int p_DesignationID, int p_DepartmentID, int p_EmployeeLocationID, bool p_EmployeeIsManager, int p_TYPE_ID, int p_PageSize, int p_Page)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEmployee2 objEmployee = new spSelectEmployee2();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.DepartmentID = p_DepartmentID;
                objEmployee.DesignationID = p_DesignationID;
                objEmployee.EmployeeLocationID = p_EmployeeLocationID;
                objEmployee.EmployeeIsManager = p_EmployeeIsManager;
                objEmployee.TYPE_ID = p_TYPE_ID;
                objEmployee.PageSize = p_PageSize;
                objEmployee.Page = p_Page;
                DataTable dt = objEmployee.ExecuteTable();
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
        public DataTable SelectEmployee_Contract(int p_ContractID, int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEmployee_Contract mdtCompany = new spSelectEmployee_Contract();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.ContractID = p_ContractID;
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
        public long InsertEmployee(EmployeeModel emp)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertEmployee Employee = new spInsertEmployee();
                Employee.Connection = mConnection;
                Employee.BloodGroup = emp.BloodGroup;
                Employee.CompanyID = emp.CompanyID;
                Employee.Document_Date = emp.Document_Date;
                Employee.EmployeeBankAccountNumber = emp.EmployeeBankAccountNumber;
                Employee.EmployeeBankAccountTitle = emp.EmployeeBankAccountTitle;
                Employee.EmployeeBankName = emp.EmployeeBankName;
                Employee.EmployeeCNIC = emp.EmployeeCNIC;
                Employee.EmployeeDateOfBirth = emp.EmployeeDateOfBirth;
                Employee.EmployeeDrivingLicenseNumber = emp.EmployeeDrivingLicenseNumber;
                Employee.EmployeeDrivingLicenseValidUpto = emp.EmployeeDrivingLicenseValidUpto;
                Employee.EmployeeDurationFrom = emp.EmployeeDurationFrom;
                Employee.EmployeeDurationTo = emp.EmployeeDurationTo;
                Employee.EmployeeEmergencyContactName = emp.EmployeeEmergencyContactName;
                Employee.EmployeeEmergencyContactNumber = emp.EmployeeEmergencyContactNumber;
                Employee.EmployeeEOBInumber = emp.EmployeeEOBInumber;
                Employee.EmployeeFullName = emp.EmployeeFullName;
                Employee.EmployeeGender = emp.EmployeeGender;
                Employee.EmployeeHomeAddress = emp.EmployeeHomeAddress;
                Employee.EmployeeIsAManager = emp.EmployeeIsAManager;
                Employee.EmployeeMachineTag1 = emp.EmployeeMachineTag1;
                Employee.EmployeeMachineTag2 = emp.EmployeeMachineTag2;
                Employee.EmployeeMachineTag3 = emp.EmployeeMachineTag3;
                Employee.EmployeeMaritalStatus = emp.EmployeeMaritalStatus;
                Employee.EmployeeNationality = emp.EmployeeNationality;
                Employee.EmployeeNTN = emp.EmployeeNTN;
                Employee.EmployeeOtherInformation = emp.EmployeeOtherInformation;
                Employee.EmployeePassportNumber = emp.EmployeePassportNumber;
                Employee.EmployeePicture = emp.EmployeePicture;
                Employee.EmployeeProbationFrom = emp.EmployeeProbationFrom;
                Employee.EmployeeProbationTo = emp.EmployeeProbationTo;
                Employee.EmployeeSocialSecurityNumber = emp.EmployeeSocialSecurityNumber;
                Employee.EmployeeTag = emp.EmployeeTag;
                Employee.EmployeeVisaInformation = emp.EmployeeVisaInformation;
                Employee.EmployeeWorkEmail = emp.EmployeeWorkEmail;
                Employee.EmployeeWorkingAddress = emp.EmployeeWorkingAddress;
                Employee.EmployeeWorkMobile = emp.EmployeeWorkMobile;
                Employee.EmployeeWorkPhone = emp.EmployeeWorkPhone;
                Employee.IS_ACTIVE = emp.IS_ACTIVE;
                Employee.IS_APPROVED = emp.IS_APPROVED;
                Employee.IsMonitored = emp.IsMonitored;
                Employee.IsOvertimeEnabled = emp.IsOvertimeEnabled;
                Employee.IsPFEnabled = emp.IsPFEnabled;
                Employee.TimeZone = emp.TimeZone;
                Employee.User_ID = emp.User_ID;
                Employee.DISTRIBUTOR_ID = emp.DISTRIBUTOR_ID;
                return Employee.ExecuteQuery();
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
        public long InsertEmployeeArchive(EmployeeModel emp)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertEmployeeArchive Employee = new spInsertEmployeeArchive();
                Employee.Connection = mConnection;
                Employee.BloodGroup = emp.BloodGroup;
                Employee.CompanyID = emp.CompanyID;
                Employee.Document_Date = emp.Document_Date;
                Employee.EmployeeBankAccountNumber = emp.EmployeeBankAccountNumber;
                Employee.EmployeeBankAccountTitle = emp.EmployeeBankAccountTitle;
                Employee.EmployeeBankName = emp.EmployeeBankName;
                Employee.EmployeeCNIC = emp.EmployeeCNIC;
                Employee.EmployeeDateOfBirth = emp.EmployeeDateOfBirth;
                Employee.EmployeeDrivingLicenseNumber = emp.EmployeeDrivingLicenseNumber;
                Employee.EmployeeDrivingLicenseValidUpto = emp.EmployeeDrivingLicenseValidUpto;
                Employee.EmployeeDurationFrom = emp.EmployeeDurationFrom;
                Employee.EmployeeDurationTo = emp.EmployeeDurationTo;
                Employee.EmployeeEmergencyContactName = emp.EmployeeEmergencyContactName;
                Employee.EmployeeEmergencyContactNumber = emp.EmployeeEmergencyContactNumber;
                Employee.EmployeeEOBInumber = emp.EmployeeEOBInumber;
                Employee.EmployeeFullName = emp.EmployeeFullName;
                Employee.EmployeeGender = emp.EmployeeGender;
                Employee.EmployeeHomeAddress = emp.EmployeeHomeAddress;
                Employee.EmployeeIsAManager = emp.EmployeeIsAManager;
                Employee.EmployeeMachineTag1 = emp.EmployeeMachineTag1;
                Employee.EmployeeMachineTag2 = emp.EmployeeMachineTag2;
                Employee.EmployeeMachineTag3 = emp.EmployeeMachineTag3;
                Employee.EmployeeMaritalStatus = emp.EmployeeMaritalStatus;
                Employee.EmployeeNationality = emp.EmployeeNationality;
                Employee.EmployeeNTN = emp.EmployeeNTN;
                Employee.EmployeeOtherInformation = emp.EmployeeOtherInformation;
                Employee.EmployeePassportNumber = emp.EmployeePassportNumber;
                Employee.EmployeePicture = emp.EmployeePicture;
                Employee.EmployeeProbationFrom = emp.EmployeeProbationFrom;
                Employee.EmployeeProbationTo = emp.EmployeeProbationTo;
                Employee.EmployeeSocialSecurityNumber = emp.EmployeeSocialSecurityNumber;
                Employee.EmployeeTag = emp.EmployeeTag;
                Employee.EmployeeVisaInformation = emp.EmployeeVisaInformation;
                Employee.EmployeeWorkEmail = emp.EmployeeWorkEmail;
                Employee.EmployeeWorkingAddress = emp.EmployeeWorkingAddress;
                Employee.EmployeeWorkMobile = emp.EmployeeWorkMobile;
                Employee.EmployeeWorkPhone = emp.EmployeeWorkPhone;
                Employee.IS_ACTIVE = emp.IS_ACTIVE;
                Employee.IS_DELETED = false;
                Employee.IS_APPROVED = emp.IS_APPROVED;
                Employee.IsMonitored = emp.IsMonitored;
                Employee.IsOvertimeEnabled = emp.IsOvertimeEnabled;
                Employee.TimeZone = emp.TimeZone;
                Employee.User_ID = emp.User_ID;
                Employee.EmployeeID = emp.EmployeeID;
                return Employee.ExecuteQuery();
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
        public long InsertEmployeeTranscationalDetail(EmployeeTranscationalDetailModel ETD)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertEmployeeTranscationalDetail mAccountHead = new spInsertEmployeeTranscationalDetail();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = ETD.CompanyID;
                mAccountHead.DepartmentID = ETD.DepartmentID;
                mAccountHead.DesignationID = ETD.DesignationID;
                mAccountHead.EmployeeContractTypeID = ETD.EmployeeContractTypeID;
                mAccountHead.EmployeeID = ETD.EmployeeID;
                mAccountHead.EmployeeLocationID = ETD.EmployeeLocationID;
                
                mAccountHead.EmployeeUnitID = ETD.EmployeeUnitID;
                mAccountHead.EmployeeWorkingScheduleID = ETD.EmployeeWorkingScheduleID;
                mAccountHead.ExceptionID = ETD.ExceptionID;
                mAccountHead.SalaryBehaviorID = ETD.SalaryBehaviorID;
                mAccountHead.SalaryStructureID = ETD.SalaryStructureID;
                mAccountHead.ShiftID = ETD.ShiftID;
                mAccountHead.TemplateID = ETD.TemplateID;
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
        public long InsertEmployeeTranscationalArchiveDetail(EmployeeTranscationalDetailModel ETD)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertEmployeeTranscationalArchiveDetail mAccountHead = new spInsertEmployeeTranscationalArchiveDetail();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = ETD.CompanyID;
                mAccountHead.DepartmentID = ETD.DepartmentID;
                mAccountHead.DesignationID = ETD.DesignationID;
                mAccountHead.EmployeeContractTypeID = ETD.EmployeeContractTypeID;
                mAccountHead.EmployeeID = ETD.EmployeeID;
                mAccountHead.EmployeeLocationID = ETD.EmployeeLocationID;
                mAccountHead.EmployeeUnitID = ETD.EmployeeUnitID;
                mAccountHead.EmployeeWorkingScheduleID = ETD.EmployeeWorkingScheduleID;
                mAccountHead.ExceptionID = ETD.ExceptionID;
                mAccountHead.SalaryBehaviorID = ETD.SalaryBehaviorID;
                mAccountHead.SalaryStructureID = ETD.SalaryStructureID;
                mAccountHead.ShiftID = ETD.ShiftID;
                mAccountHead.TemplateID = ETD.TemplateID;
                mAccountHead.EmployeeTranscationalDetailID = ETD.EmployeeTranscationalDetailID;
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
        public bool UpdateEmployee(EmployeeModel emp)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateEmployee Employee = new spUpdateEmployee();
                Employee.Connection = mConnection;
                Employee.BloodGroup = emp.BloodGroup;
                Employee.CompanyID = emp.CompanyID;
                Employee.EmployeeBankAccountNumber = emp.EmployeeBankAccountNumber;
                Employee.EmployeeBankAccountTitle = emp.EmployeeBankAccountTitle;
                Employee.EmployeeBankName = emp.EmployeeBankName;
                Employee.EmployeeCNIC = emp.EmployeeCNIC;
                Employee.EmployeeDateOfBirth = emp.EmployeeDateOfBirth;
                Employee.EmployeeDrivingLicenseNumber = emp.EmployeeDrivingLicenseNumber;
                Employee.EmployeeDrivingLicenseValidUpto = emp.EmployeeDrivingLicenseValidUpto;
                Employee.EmployeeDurationFrom = emp.EmployeeDurationFrom;
                Employee.EmployeeDurationTo = emp.EmployeeDurationTo;
                Employee.EmployeeEmergencyContactName = emp.EmployeeEmergencyContactName;
                Employee.EmployeeEmergencyContactNumber = emp.EmployeeEmergencyContactNumber;
                Employee.EmployeeEOBInumber = emp.EmployeeEOBInumber;
                Employee.EmployeeFullName = emp.EmployeeFullName;
                Employee.EmployeeGender = emp.EmployeeGender;
                Employee.EmployeeHomeAddress = emp.EmployeeHomeAddress;
                Employee.EmployeeIsAManager = emp.EmployeeIsAManager;
                Employee.EmployeeMachineTag1 = emp.EmployeeMachineTag1;
                Employee.EmployeeMachineTag2 = emp.EmployeeMachineTag2;
                Employee.EmployeeMachineTag3 = emp.EmployeeMachineTag3;
                Employee.EmployeeMaritalStatus = emp.EmployeeMaritalStatus;
                Employee.EmployeeNationality = emp.EmployeeNationality;
                Employee.EmployeeNTN = emp.EmployeeNTN;
                Employee.EmployeeOtherInformation = emp.EmployeeOtherInformation;
                Employee.EmployeePassportNumber = emp.EmployeePassportNumber;
                Employee.EmployeePicture = emp.EmployeePicture;
                Employee.EmployeeProbationFrom = emp.EmployeeProbationFrom;
                Employee.EmployeeProbationTo = emp.EmployeeProbationTo;
                Employee.EmployeeSocialSecurityNumber = emp.EmployeeSocialSecurityNumber;
                Employee.EmployeeTag = emp.EmployeeTag;
                Employee.EmployeeVisaInformation = emp.EmployeeVisaInformation;
                Employee.EmployeeWorkEmail = emp.EmployeeWorkEmail;
                Employee.EmployeeWorkingAddress = emp.EmployeeWorkingAddress;
                Employee.EmployeeWorkMobile = emp.EmployeeWorkMobile;
                Employee.EmployeeWorkPhone = emp.EmployeeWorkPhone;
                Employee.IS_ACTIVE = emp.IS_ACTIVE;
                Employee.IS_APPROVED = emp.IS_APPROVED;
                Employee.IsMonitored = emp.IsMonitored;
                Employee.IsOvertimeEnabled = emp.IsOvertimeEnabled;
                Employee.IsPFEnabled = emp.IsPFEnabled;
                Employee.TimeZone = emp.TimeZone;
                Employee.User_ID = emp.User_ID;
                Employee.EmployeeID = emp.EmployeeID;
                Employee.DISTRIBUTOR_ID = emp.DISTRIBUTOR_ID;
                return Employee.ExecuteQuery();
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
        public bool UpdateEmployeeTranscationalDetail(EmployeeTranscationalDetailModel ETD)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateEmployeeTranscationalDetail mAccountHead = new spUpdateEmployeeTranscationalDetail();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = ETD.CompanyID;
                mAccountHead.DepartmentID = ETD.DepartmentID;
                mAccountHead.DesignationID = ETD.DesignationID;
                mAccountHead.EmployeeContractTypeID = ETD.EmployeeContractTypeID;
                mAccountHead.EmployeeID = ETD.EmployeeID;
                mAccountHead.EmployeeLocationID = ETD.EmployeeLocationID;
                mAccountHead.EmployeeUnitID = ETD.EmployeeUnitID;
                mAccountHead.EmployeeWorkingScheduleID = ETD.EmployeeWorkingScheduleID;
                mAccountHead.ExceptionID = ETD.ExceptionID;
                mAccountHead.SalaryBehaviorID = ETD.SalaryBehaviorID;
                mAccountHead.SalaryStructureID = ETD.SalaryStructureID;
                mAccountHead.ShiftID = ETD.ShiftID;
                mAccountHead.TemplateID = ETD.TemplateID;
                mAccountHead.EmployeeTranscationalDetailID = 0;
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
        public bool ApproveEmployee(int Isapproved, int EmployeeID)
        {
            string CommandText = "Update Employee set IS_APPROVED=" + Isapproved+ " WHERE EmployeeID="+EmployeeID;
            IDbConnection mConnection = null;
            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            spUpdateEmployee Employee = new spUpdateEmployee();
            IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = CommandText;
            cmd.Connection = mConnection;
            cmd.ExecuteNonQuery();
            return true;
        }
        public DataTable GetEmployeeShift(int p_ShiftID, int p_EmployeeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetEmployeeShift mdtCompany = new spGetEmployeeShift();
                mdtCompany.Connection = mConnection;
                mdtCompany.ShiftID = p_ShiftID;
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
        public DataTable GetEmployeeList(int p_CompanyID, int p_LOCATIONID, int p_DEPARTMENTID, int p_EmployeeContractTypeID, int p_StatusID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spEmployeeList objEmployee = new spEmployeeList();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_CompanyID;
                objEmployee.LOCATIONID = p_LOCATIONID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
                objEmployee.StatusID = p_StatusID;
                DataTable dt = objEmployee.ExecuteTable();
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
        public DataTable GetAdvance(int p_CompanyID, int p_LOCATIONID, int p_DEPARTMENTID, int p_EmployeeContractTypeID, int p_EmployeeID, DateTime p_START_DATE, DateTime p_END_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetAdvance objEmployee = new spGetAdvance();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_CompanyID;
                objEmployee.LOCATIONID = p_LOCATIONID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.END_DATE = p_END_DATE;
                DataTable dt = objEmployee.ExecuteTable();
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
        public DataTable GetAdvanceIssuanceSlip(int p_CompanyID, int p_LOCATIONID, int p_DEPARTMENTID, int p_EmployeeContractTypeID, int p_EmployeeID, DateTime p_START_DATE, DateTime p_END_DATE)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetAdvanceIssuanceSlip objEmployee = new spGetAdvanceIssuanceSlip();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_CompanyID;
                objEmployee.LOCATIONID = p_LOCATIONID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.END_DATE = p_END_DATE;
                DataTable dt = objEmployee.ExecuteTable();
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

        public DataTable GetFundTransfer(int p_CompanyID, int p_LOCATIONID, int p_DEPARTMENTID, int p_EmployeeContractTypeID, int p_EmployeeID, DateTime p_START_DATE, DateTime p_END_DATE)
        {


            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetAdvanceIssuanceSlip objEmployee = new spGetAdvanceIssuanceSlip();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_CompanyID;
                objEmployee.LOCATIONID = p_LOCATIONID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.END_DATE = p_END_DATE;
                DataTable dt = objEmployee.ExecuteTable();
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
        
        
        
        public DataTable GetEmployeeTimeCard(int p_LOCATIONID, int p_DEPARTMENTID, int p_EmployeeContractTypeID, int p_EmployeeID, DateTime p_FROM_DATE, DateTime p_TO_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetTimeCard objEmployee = new spGetTimeCard();
                objEmployee.Connection = mConnection;
                objEmployee.EmployeeLocationID = p_LOCATIONID;
                objEmployee.DepartmentID = p_DEPARTMENTID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.FROM_DATE = p_FROM_DATE;
                objEmployee.TO_DATE = p_TO_DATE;
                DataTable dt = objEmployee.ExecuteTable();
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
        public DataTable GettEmployeeTarget(long p_EmployeeID, int p_CompanyID, int p_DesignationID, int p_DepartmentID, int p_EmployeeLocationID, DateTime p_TARGET_MONTH)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetEmployeeTarget objEmployee = new spGetEmployeeTarget();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.DepartmentID = p_DepartmentID;
                objEmployee.DesignationID = p_DesignationID;
                objEmployee.EmployeeLocationID = p_EmployeeLocationID;
                objEmployee.TARGET_MONTH = p_TARGET_MONTH;
                DataTable dt = objEmployee.ExecuteTable();
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
