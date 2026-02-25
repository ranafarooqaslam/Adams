using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;


namespace SAMSDatabaseLayer.Classes
{
	public class spInsertEmployeeArchive
	{
        #region Private Members
        private string sp_Name = "spInsertEmployeeArchive";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_EmployeeGender;
        private int m_CompanyID;
        private int m_User_ID;
        private DateTime m_EmployeeDrivingLicenseValidUpto;
        private DateTime m_EmployeeDateOfBirth;
        private DateTime m_EmployeeProbationFrom;
        private DateTime m_EmployeeProbationTo;
        private DateTime m_EmployeeDurationFrom;
        private DateTime m_EmployeeDurationTo;
        private DateTime m_Document_Date;
        private bool m_EmployeeMaritalStatus;
        private bool m_IsMonitored;
        private bool m_IS_APPROVED;
        private bool m_EmployeeIsAManager;
        private bool m_IsOvertimeEnabled;
        private bool m_IS_ACTIVE;
        private bool m_IS_DELETED;
        private long m_EmployeeID;
        private long m_EmployeeArchiveID;
        private string m_EmployeeFullName;
        private string m_EmployeeWorkingAddress;
        private string m_EmployeeWorkEmail;
        private string m_EmployeeWorkPhone;
        private string m_EmployeeWorkMobile;
        private string m_EmployeeCNIC;
        private string m_TimeZone;
        private string m_EmployeePicture;
        private string m_BloodGroup;
        private string m_EmployeeEmergencyContactName;
        private string m_EmployeeOtherInformation;
        private string m_EmployeeTag;
        private string m_EmployeeMachineTag1;
        private string m_EmployeeMachineTag2;
        private string m_EmployeeMachineTag3;
        private string m_EmployeeBankAccountTitle;
        private string m_EmployeeBankName;
        private string m_EmployeeSocialSecurityNumber;
        private string m_EmployeeEOBInumber;
        private string m_EmployeeHomeAddress;
        private string m_EmployeeEmergencyContactNumber;
        private string m_EmployeeNTN;
        private string m_EmployeeDrivingLicenseNumber;
        private string m_EmployeePassportNumber;
        private string m_EmployeeVisaInformation;
        private string m_EmployeeNationality;
        private string m_EmployeeBankAccountNumber;
        #endregion
        #region Public Properties
        public int EmployeeGender
        {
            set
            {
                m_EmployeeGender = value;
            }
            get
            {
                return m_EmployeeGender;
            }
        }
        public int CompanyID
        {
            set
            {
                m_CompanyID = value;
            }
            get
            {
                return m_CompanyID;
            }
        }
        public int User_ID
        {
            set
            {
                m_User_ID = value;
            }
            get
            {
                return m_User_ID;
            }
        }
        public DateTime EmployeeDrivingLicenseValidUpto
        {
            set
            {
                m_EmployeeDrivingLicenseValidUpto = value;
            }
            get
            {
                return m_EmployeeDrivingLicenseValidUpto;
            }
        }
        public DateTime EmployeeDateOfBirth
        {
            set
            {
                m_EmployeeDateOfBirth = value;
            }
            get
            {
                return m_EmployeeDateOfBirth;
            }
        }
        public DateTime EmployeeProbationFrom
        {
            set
            {
                m_EmployeeProbationFrom = value;
            }
            get
            {
                return m_EmployeeProbationFrom;
            }
        }
        public DateTime EmployeeProbationTo
        {
            set
            {
                m_EmployeeProbationTo = value;
            }
            get
            {
                return m_EmployeeProbationTo;
            }
        }
        public DateTime EmployeeDurationFrom
        {
            set
            {
                m_EmployeeDurationFrom = value;
            }
            get
            {
                return m_EmployeeDurationFrom;
            }
        }
        public DateTime EmployeeDurationTo
        {
            set
            {
                m_EmployeeDurationTo = value;
            }
            get
            {
                return m_EmployeeDurationTo;
            }
        }
        public DateTime Document_Date
        {
            set
            {
                m_Document_Date = value;
            }
            get
            {
                return m_Document_Date;
            }
        }
        public bool EmployeeMaritalStatus
        {
            set
            {
                m_EmployeeMaritalStatus = value;
            }
            get
            {
                return m_EmployeeMaritalStatus;
            }
        }
        public bool IsMonitored
        {
            set
            {
                m_IsMonitored = value;
            }
            get
            {
                return m_IsMonitored;
            }
        }
        public bool IS_APPROVED
        {
            set
            {
                m_IS_APPROVED = value;
            }
            get
            {
                return m_IS_APPROVED;
            }
        }
        public bool EmployeeIsAManager
        {
            set
            {
                m_EmployeeIsAManager = value;
            }
            get
            {
                return m_EmployeeIsAManager;
            }
        }
        public bool IsOvertimeEnabled
        {
            set
            {
                m_IsOvertimeEnabled = value;
            }
            get
            {
                return m_IsOvertimeEnabled;
            }
        }
        public bool IS_ACTIVE
        {
            set
            {
                m_IS_ACTIVE = value;
            }
            get
            {
                return m_IS_ACTIVE;
            }
        }
        public bool IS_DELETED
        {
            set
            {
                m_IS_DELETED = value;
            }
            get
            {
                return m_IS_DELETED;
            }
        }
        public long EmployeeID
        {
            set
            {
                m_EmployeeID = value;
            }
            get
            {
                return m_EmployeeID;
            }
        }
        public long EmployeeArchiveID
        {
            get
            {
                return m_EmployeeArchiveID;
            }
        }
        public string EmployeeFullName
        {
            set
            {
                m_EmployeeFullName = value;
            }
            get
            {
                return m_EmployeeFullName;
            }
        }
        public string EmployeeWorkingAddress
        {
            set
            {
                m_EmployeeWorkingAddress = value;
            }
            get
            {
                return m_EmployeeWorkingAddress;
            }
        }
        public string EmployeeWorkEmail
        {
            set
            {
                m_EmployeeWorkEmail = value;
            }
            get
            {
                return m_EmployeeWorkEmail;
            }
        }
        public string EmployeeWorkPhone
        {
            set
            {
                m_EmployeeWorkPhone = value;
            }
            get
            {
                return m_EmployeeWorkPhone;
            }
        }
        public string EmployeeWorkMobile
        {
            set
            {
                m_EmployeeWorkMobile = value;
            }
            get
            {
                return m_EmployeeWorkMobile;
            }
        }
        public string EmployeeCNIC
        {
            set
            {
                m_EmployeeCNIC = value;
            }
            get
            {
                return m_EmployeeCNIC;
            }
        }
        public string TimeZone
        {
            set
            {
                m_TimeZone = value;
            }
            get
            {
                return m_TimeZone;
            }
        }
        public string EmployeePicture
        {
            set
            {
                m_EmployeePicture = value;
            }
            get
            {
                return m_EmployeePicture;
            }
        }
        public string BloodGroup
        {
            set
            {
                m_BloodGroup = value;
            }
            get
            {
                return m_BloodGroup;
            }
        }
        public string EmployeeEmergencyContactName
        {
            set
            {
                m_EmployeeEmergencyContactName = value;
            }
            get
            {
                return m_EmployeeEmergencyContactName;
            }
        }
        public string EmployeeOtherInformation
        {
            set
            {
                m_EmployeeOtherInformation = value;
            }
            get
            {
                return m_EmployeeOtherInformation;
            }
        }
        public string EmployeeTag
        {
            set
            {
                m_EmployeeTag = value;
            }
            get
            {
                return m_EmployeeTag;
            }
        }
        public string EmployeeMachineTag1
        {
            set
            {
                m_EmployeeMachineTag1 = value;
            }
            get
            {
                return m_EmployeeMachineTag1;
            }
        }
        public string EmployeeMachineTag2
        {
            set
            {
                m_EmployeeMachineTag2 = value;
            }
            get
            {
                return m_EmployeeMachineTag2;
            }
        }
        public string EmployeeMachineTag3
        {
            set
            {
                m_EmployeeMachineTag3 = value;
            }
            get
            {
                return m_EmployeeMachineTag3;
            }
        }
        public string EmployeeBankAccountTitle
        {
            set
            {
                m_EmployeeBankAccountTitle = value;
            }
            get
            {
                return m_EmployeeBankAccountTitle;
            }
        }
        public string EmployeeBankName
        {
            set
            {
                m_EmployeeBankName = value;
            }
            get
            {
                return m_EmployeeBankName;
            }
        }
        public string EmployeeSocialSecurityNumber
        {
            set
            {
                m_EmployeeSocialSecurityNumber = value;
            }
            get
            {
                return m_EmployeeSocialSecurityNumber;
            }
        }
        public string EmployeeEOBInumber
        {
            set
            {
                m_EmployeeEOBInumber = value;
            }
            get
            {
                return m_EmployeeEOBInumber;
            }
        }
        public string EmployeeHomeAddress
        {
            set
            {
                m_EmployeeHomeAddress = value;
            }
            get
            {
                return m_EmployeeHomeAddress;
            }
        }
        public string EmployeeEmergencyContactNumber
        {
            set
            {
                m_EmployeeEmergencyContactNumber = value;
            }
            get
            {
                return m_EmployeeEmergencyContactNumber;
            }
        }
        public string EmployeeNTN
        {
            set
            {
                m_EmployeeNTN = value;
            }
            get
            {
                return m_EmployeeNTN;
            }
        }
        public string EmployeeDrivingLicenseNumber
        {
            set
            {
                m_EmployeeDrivingLicenseNumber = value;
            }
            get
            {
                return m_EmployeeDrivingLicenseNumber;
            }
        }
        public string EmployeePassportNumber
        {
            set
            {
                m_EmployeePassportNumber = value;
            }
            get
            {
                return m_EmployeePassportNumber;
            }
        }
        public string EmployeeVisaInformation
        {
            set
            {
                m_EmployeeVisaInformation = value;
            }
            get
            {
                return m_EmployeeVisaInformation;
            }
        }
        public string EmployeeNationality
        {
            set
            {
                m_EmployeeNationality = value;
            }
            get
            {
                return m_EmployeeNationality;
            }
        }
        public string EmployeeBankAccountNumber
        {
            set
            {
                m_EmployeeBankAccountNumber = value;
            }
            get
            {
                return m_EmployeeBankAccountNumber;
            }
        }


        public IDbConnection Connection
        {
            set
            {
                m_connection = value;
            }
            get
            {
                return m_connection;
            }
        }
        public IDbTransaction Transaction
        {
            set
            {
                m_transaction = value;
            }
            get
            {
                return m_transaction;
            }
        }
        #endregion
        #region Constructor
        public spInsertEmployeeArchive()
        {
        }
        #endregion
        #region public Methods
        public long ExecuteQuery()
        {
            try
            {
                IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp_Name;
                cmd.Connection = m_connection;
                if (m_transaction != null)
                {
                    cmd.Transaction = m_transaction;
                }
                GetParameterCollection(ref cmd);
                cmd.ExecuteNonQuery();
                return m_EmployeeArchiveID = (long)((IDataParameter)(cmd.Parameters["@EmployeeArchiveID"])).Value;
                 
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }
        public IDataReader ExecuteReader()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public DataTable ExecuteTable()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
                da.SelectCommand = command;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public string ExecuteScalar()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                object o;
                o = command.ExecuteScalar();


                return o.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public void FirstReader(IDataReader dr)
        {
            if (dr.Read())
            {
                m_EmployeeGender = Convert.ToInt32(dr["EmployeeGender"]);
                m_CompanyID = Convert.ToInt32(dr["CompanyID"]);
                m_User_ID = Convert.ToInt32(dr["User_ID"]);
                m_EmployeeDrivingLicenseValidUpto = Convert.ToDateTime(dr["EmployeeDrivingLicenseValidUpto"]);
                m_EmployeeDateOfBirth = Convert.ToDateTime(dr["EmployeeDateOfBirth"]);
                m_EmployeeProbationFrom = Convert.ToDateTime(dr["EmployeeProbationFrom"]);
                m_EmployeeProbationTo = Convert.ToDateTime(dr["EmployeeProbationTo"]);
                m_EmployeeDurationFrom = Convert.ToDateTime(dr["EmployeeDurationFrom"]);
                m_EmployeeDurationTo = Convert.ToDateTime(dr["EmployeeDurationTo"]);
                m_Document_Date = Convert.ToDateTime(dr["Document_Date"]);
                m_EmployeeMaritalStatus = Convert.ToBoolean(dr["EmployeeMaritalStatus"]);
                m_IsMonitored = Convert.ToBoolean(dr["IsMonitored"]);
                m_IS_APPROVED = Convert.ToBoolean(dr["IS_APPROVED"]);
                m_EmployeeIsAManager = Convert.ToBoolean(dr["EmployeeIsAManager"]);
                m_IsOvertimeEnabled = Convert.ToBoolean(dr["IsOvertimeEnabled"]);
                m_IS_ACTIVE = Convert.ToBoolean(dr["IS_ACTIVE"]);
                m_IS_DELETED = Convert.ToBoolean(dr["IS_DELETED"]);
                m_EmployeeID = Convert.ToInt64(dr["EmployeeID"]);
                m_EmployeeArchiveID = Convert.ToInt64(dr["EmployeeArchiveID"]);
                m_EmployeeFullName = Convert.ToString(dr["EmployeeFullName"]);
                m_EmployeeWorkingAddress = Convert.ToString(dr["EmployeeWorkingAddress"]);
                m_EmployeeWorkEmail = Convert.ToString(dr["EmployeeWorkEmail"]);
                m_EmployeeWorkPhone = Convert.ToString(dr["EmployeeWorkPhone"]);
                m_EmployeeWorkMobile = Convert.ToString(dr["EmployeeWorkMobile"]);
                m_EmployeeCNIC = Convert.ToString(dr["EmployeeCNIC"]);
                m_TimeZone = Convert.ToString(dr["TimeZone"]);
                m_EmployeePicture = Convert.ToString(dr["EmployeePicture"]);
                m_BloodGroup = Convert.ToString(dr["BloodGroup"]);
                m_EmployeeEmergencyContactName = Convert.ToString(dr["EmployeeEmergencyContactName"]);
                m_EmployeeOtherInformation = Convert.ToString(dr["EmployeeOtherInformation"]);
                m_EmployeeTag = Convert.ToString(dr["EmployeeTag"]);
                m_EmployeeMachineTag1 = Convert.ToString(dr["EmployeeMachineTag1"]);
                m_EmployeeMachineTag2 = Convert.ToString(dr["EmployeeMachineTag2"]);
                m_EmployeeMachineTag3 = Convert.ToString(dr["EmployeeMachineTag3"]);
                m_EmployeeBankAccountTitle = Convert.ToString(dr["EmployeeBankAccountTitle"]);
                m_EmployeeBankName = Convert.ToString(dr["EmployeeBankName"]);
                m_EmployeeSocialSecurityNumber = Convert.ToString(dr["EmployeeSocialSecurityNumber"]);
                m_EmployeeEOBInumber = Convert.ToString(dr["EmployeeEOBInumber"]);
                m_EmployeeHomeAddress = Convert.ToString(dr["EmployeeHomeAddress"]);
                m_EmployeeEmergencyContactNumber = Convert.ToString(dr["EmployeeEmergencyContactNumber"]);
                m_EmployeeNTN = Convert.ToString(dr["EmployeeNTN"]);
                m_EmployeeDrivingLicenseNumber = Convert.ToString(dr["EmployeeDrivingLicenseNumber"]);
                m_EmployeePassportNumber = Convert.ToString(dr["EmployeePassportNumber"]);
                m_EmployeeVisaInformation = Convert.ToString(dr["EmployeeVisaInformation"]);
                m_EmployeeNationality = Convert.ToString(dr["EmployeeNationality"]);
                m_EmployeeBankAccountNumber = Convert.ToString(dr["EmployeeBankAccountNumber"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeGender";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_EmployeeGender == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeGender;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CompanyID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_CompanyID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CompanyID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@User_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_User_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_User_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeDrivingLicenseValidUpto";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_EmployeeDrivingLicenseValidUpto == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeDrivingLicenseValidUpto;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeDateOfBirth";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_EmployeeDateOfBirth == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeDateOfBirth;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeProbationFrom";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_EmployeeProbationFrom == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeProbationFrom;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeProbationTo";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_EmployeeProbationTo == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeProbationTo;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeDurationFrom";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_EmployeeDurationFrom == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeDurationFrom;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeDurationTo";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_EmployeeDurationTo == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeDurationTo;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Document_Date";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_Document_Date == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Document_Date;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeMaritalStatus";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_EmployeeMaritalStatus;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsMonitored";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsMonitored;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_APPROVED";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_APPROVED;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeIsAManager";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_EmployeeIsAManager;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IsOvertimeEnabled";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IsOvertimeEnabled;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_ACTIVE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_ACTIVE;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_DELETED";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_DELETED;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_EmployeeID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeArchiveID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeFullName";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeFullName == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeFullName;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeWorkingAddress";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeWorkingAddress == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeWorkingAddress;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeWorkEmail";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeWorkEmail == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeWorkEmail;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeWorkPhone";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeWorkPhone == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeWorkPhone;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeWorkMobile";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeWorkMobile == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeWorkMobile;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeCNIC";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeCNIC == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeCNIC;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TimeZone";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_TimeZone == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TimeZone;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeePicture";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeePicture == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeePicture;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BloodGroup";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_BloodGroup == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BloodGroup;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeEmergencyContactName";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeEmergencyContactName == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeEmergencyContactName;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeOtherInformation";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeOtherInformation == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeOtherInformation;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeTag";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeTag == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeTag;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeMachineTag1";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeMachineTag1 == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeMachineTag1;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeMachineTag2";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeMachineTag2 == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeMachineTag2;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeMachineTag3";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeMachineTag3 == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeMachineTag3;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeBankAccountTitle";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeBankAccountTitle == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeBankAccountTitle;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeBankName";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeBankName == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeBankName;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeSocialSecurityNumber";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeSocialSecurityNumber == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeSocialSecurityNumber;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeEOBInumber";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeEOBInumber == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeEOBInumber;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeHomeAddress";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeHomeAddress == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeHomeAddress;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeEmergencyContactNumber";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeEmergencyContactNumber == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeEmergencyContactNumber;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeNTN";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeNTN == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeNTN;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeDrivingLicenseNumber";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeDrivingLicenseNumber == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeDrivingLicenseNumber;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeePassportNumber";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeePassportNumber == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeePassportNumber;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeVisaInformation";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeVisaInformation == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeVisaInformation;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeNationality";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeNationality == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeNationality;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeBankAccountNumber";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EmployeeBankAccountNumber == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeBankAccountNumber;
            }
            pparams.Add(parameter);


        }
        #endregion
	}
}
