using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
namespace SAMSDatabaseLayer.Classes
{
	public class spInsertDISTRIBUTOR_USER_DETAIL
	{
		#region Private Members
		private string sp_Name = "spInsertDISTRIBUTOR_USER_DETAIL" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_USER_ID;
		private int m_COMPANY_ID;
		private int m_DISTRIBUTOR_ID;
		private int m_DEPARTMENT_ID;
		private int m_Gender;
		private int m_PAYMENT_MODE;
		private int m_SALARY_CHARGED_TO;
		private int m_SALARY_STRUCTURE_ID;
		private int m_TEMPLATE_ID;
		private int m_SHIFT_ID;
		private int m_EmployeeType;
		private decimal m_LAST_SALARY_DRAWN;
		private DateTime m_EmployeeJoiningDate;
        private DateTime m_EmployeeProbationFrom;
        private DateTime m_EmployeeProbationTo;
		private bool m_MaritalStatus;
		private string m_FATHER_NAME;
		private string m_HUSBAND_NAME;
		private string m_RELIGION;
		private string m_NO_OF_DEPENDENTS;
		private string m_LAST_EDUCATION;
		private string m_LAST_EMPLOYEE_INFO;
		private string m_EMERGENCY_POC_NAME;
		private string m_EMERGENCY_POC_NO;
		private string m_BANK_NAME;
		private string m_BANK_ACCOUNT_TITLE;
		private string m_BANK_ACCOUNT_NO;
		private string m_REPORTING_TO_EMPLOYEE;
		private string m_LAST_DESIGNATION;
		private string m_LAST_REASON_OF_RESIGNATION;
		private string m_LAST_CONTACT_NO;
		private string m_REFERANCE;
		private string m_REPORTING_TO;
		private string m_REMARKS;
		#endregion
		#region Public Properties
		public int USER_ID
		{
			set
			{
				m_USER_ID = value ;
			}
			get
			{
				return m_USER_ID;
			}
		}
		public int COMPANY_ID
		{
			set
			{
				m_COMPANY_ID = value ;
			}
			get
			{
				return m_COMPANY_ID;
			}
		}
		public int DISTRIBUTOR_ID
		{
			set
			{
				m_DISTRIBUTOR_ID = value ;
			}
			get
			{
				return m_DISTRIBUTOR_ID;
			}
		}
		public int DEPARTMENT_ID
		{
			set
			{
				m_DEPARTMENT_ID = value ;
			}
			get
			{
				return m_DEPARTMENT_ID;
			}
		}
		public int Gender
		{
			set
			{
				m_Gender = value ;
			}
			get
			{
				return m_Gender;
			}
		}
		public int PAYMENT_MODE
		{
			set
			{
				m_PAYMENT_MODE = value ;
			}
			get
			{
				return m_PAYMENT_MODE;
			}
		}
		public int SALARY_CHARGED_TO
		{
			set
			{
				m_SALARY_CHARGED_TO = value ;
			}
			get
			{
				return m_SALARY_CHARGED_TO;
			}
		}
		public int SALARY_STRUCTURE_ID
		{
			set
			{
				m_SALARY_STRUCTURE_ID = value ;
			}
			get
			{
				return m_SALARY_STRUCTURE_ID;
			}
		}
		public int TEMPLATE_ID
		{
			set
			{
				m_TEMPLATE_ID = value ;
			}
			get
			{
				return m_TEMPLATE_ID;
			}
		}
		public int SHIFT_ID
		{
			set
			{
				m_SHIFT_ID = value ;
			}
			get
			{
				return m_SHIFT_ID;
			}
		}
		public int EmployeeType
		{
			set
			{
				m_EmployeeType = value ;
			}
			get
			{
				return m_EmployeeType;
			}
		}
		public decimal  LAST_SALARY_DRAWN
		{
			set
			{
				m_LAST_SALARY_DRAWN = value ;
			}
			get
			{
				return m_LAST_SALARY_DRAWN;
			}
		}
		public DateTime EmployeeJoiningDate
		{
			set
			{
				m_EmployeeJoiningDate = value ;
			}
			get
			{
				return m_EmployeeJoiningDate;
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
		public bool MaritalStatus
		{
			set
			{
				m_MaritalStatus = value ;
			}
			get
			{
				return m_MaritalStatus;
			}
		}
		public  string FATHER_NAME
		{
			set
			{
				m_FATHER_NAME = value ;
			}
			get
			{
				return m_FATHER_NAME;
			}
		}
		public  string HUSBAND_NAME
		{
			set
			{
				m_HUSBAND_NAME = value ;
			}
			get
			{
				return m_HUSBAND_NAME;
			}
		}
		public  string RELIGION
		{
			set
			{
				m_RELIGION = value ;
			}
			get
			{
				return m_RELIGION;
			}
		}
		public  string NO_OF_DEPENDENTS
		{
			set
			{
				m_NO_OF_DEPENDENTS = value ;
			}
			get
			{
				return m_NO_OF_DEPENDENTS;
			}
		}
		public  string LAST_EDUCATION
		{
			set
			{
				m_LAST_EDUCATION = value ;
			}
			get
			{
				return m_LAST_EDUCATION;
			}
		}
		public  string LAST_EMPLOYEE_INFO
		{
			set
			{
				m_LAST_EMPLOYEE_INFO = value ;
			}
			get
			{
				return m_LAST_EMPLOYEE_INFO;
			}
		}
		public  string EMERGENCY_POC_NAME
		{
			set
			{
				m_EMERGENCY_POC_NAME = value ;
			}
			get
			{
				return m_EMERGENCY_POC_NAME;
			}
		}
		public  string EMERGENCY_POC_NO
		{
			set
			{
				m_EMERGENCY_POC_NO = value ;
			}
			get
			{
				return m_EMERGENCY_POC_NO;
			}
		}
		public  string BANK_NAME
		{
			set
			{
				m_BANK_NAME = value ;
			}
			get
			{
				return m_BANK_NAME;
			}
		}
		public  string BANK_ACCOUNT_TITLE
		{
			set
			{
				m_BANK_ACCOUNT_TITLE = value ;
			}
			get
			{
				return m_BANK_ACCOUNT_TITLE;
			}
		}
		public  string BANK_ACCOUNT_NO
		{
			set
			{
				m_BANK_ACCOUNT_NO = value ;
			}
			get
			{
				return m_BANK_ACCOUNT_NO;
			}
		}
		public  string REPORTING_TO_EMPLOYEE
		{
			set
			{
				m_REPORTING_TO_EMPLOYEE = value ;
			}
			get
			{
				return m_REPORTING_TO_EMPLOYEE;
			}
		}
		public  string LAST_DESIGNATION
		{
			set
			{
				m_LAST_DESIGNATION = value ;
			}
			get
			{
				return m_LAST_DESIGNATION;
			}
		}
		public  string LAST_REASON_OF_RESIGNATION
		{
			set
			{
				m_LAST_REASON_OF_RESIGNATION = value ;
			}
			get
			{
				return m_LAST_REASON_OF_RESIGNATION;
			}
		}
		public  string LAST_CONTACT_NO
		{
			set
			{
				m_LAST_CONTACT_NO = value ;
			}
			get
			{
				return m_LAST_CONTACT_NO;
			}
		}
		public  string REFERANCE
		{
			set
			{
				m_REFERANCE = value ;
			}
			get
			{
				return m_REFERANCE;
			}
		}
		public  string REPORTING_TO
		{
			set
			{
				m_REPORTING_TO = value ;
			}
			get
			{
				return m_REPORTING_TO;
			}
		}
		public  string REMARKS
		{
			set
			{
				m_REMARKS = value ;
			}
			get
			{
				return m_REMARKS;
			}
		}


		public IDbConnection  Connection
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
		public IDbTransaction  Transaction
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
		public spInsertDISTRIBUTOR_USER_DETAIL()
		{
		}
		#endregion
		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch(Exception e)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
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
				if(m_transaction!=null)
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
			catch(Exception exp)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public void FirstReader(IDataReader dr)
		{
			if(dr.Read())
			{
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_COMPANY_ID= Convert.ToInt32(dr["COMPANY_ID"]);
				m_DISTRIBUTOR_ID= Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
				m_DEPARTMENT_ID= Convert.ToInt32(dr["DEPARTMENT_ID"]);
				m_Gender= Convert.ToInt32(dr["Gender"]);
				m_PAYMENT_MODE= Convert.ToInt32(dr["PAYMENT_MODE"]);
				m_SALARY_CHARGED_TO= Convert.ToInt32(dr["SALARY_CHARGED_TO"]);
				m_SALARY_STRUCTURE_ID= Convert.ToInt32(dr["SALARY_STRUCTURE_ID"]);
				m_TEMPLATE_ID= Convert.ToInt32(dr["TEMPLATE_ID"]);
				m_SHIFT_ID= Convert.ToInt32(dr["SHIFT_ID"]);
				m_EmployeeType= Convert.ToInt32(dr["EmployeeType"]);
				m_LAST_SALARY_DRAWN= Convert.ToDecimal(dr["LAST_SALARY_DRAWN"]);
				m_EmployeeJoiningDate= Convert.ToDateTime(dr["EmployeeJoiningDate"]);
                m_EmployeeProbationFrom = Convert.ToDateTime(dr["EmployeeProbationFrom"]);
                m_EmployeeProbationTo = Convert.ToDateTime(dr["EmployeeProbationTo"]);
				m_MaritalStatus=Convert.ToBoolean(dr["MaritalStatus"]);
				m_FATHER_NAME= Convert.ToString(dr["FATHER_NAME"]);
				m_HUSBAND_NAME= Convert.ToString(dr["HUSBAND_NAME"]);
				m_RELIGION= Convert.ToString(dr["RELIGION"]);
				m_NO_OF_DEPENDENTS= Convert.ToString(dr["NO_OF_DEPENDENTS"]);
				m_LAST_EDUCATION= Convert.ToString(dr["LAST_EDUCATION"]);
				m_LAST_EMPLOYEE_INFO= Convert.ToString(dr["LAST_EMPLOYEE_INFO"]);
				m_EMERGENCY_POC_NAME= Convert.ToString(dr["EMERGENCY_POC_NAME"]);
				m_EMERGENCY_POC_NO= Convert.ToString(dr["EMERGENCY_POC_NO"]);
				m_BANK_NAME= Convert.ToString(dr["BANK_NAME"]);
				m_BANK_ACCOUNT_TITLE= Convert.ToString(dr["BANK_ACCOUNT_TITLE"]);
				m_BANK_ACCOUNT_NO= Convert.ToString(dr["BANK_ACCOUNT_NO"]);
				m_REPORTING_TO_EMPLOYEE= Convert.ToString(dr["REPORTING_TO_EMPLOYEE"]);
				m_LAST_DESIGNATION= Convert.ToString(dr["LAST_DESIGNATION"]);
				m_LAST_REASON_OF_RESIGNATION= Convert.ToString(dr["LAST_REASON_OF_RESIGNATION"]);
				m_LAST_CONTACT_NO= Convert.ToString(dr["LAST_CONTACT_NO"]);
				m_REFERANCE= Convert.ToString(dr["REFERANCE"]);
				m_REPORTING_TO= Convert.ToString(dr["REPORTING_TO"]);
				m_REMARKS= Convert.ToString(dr["REMARKS"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@USER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_USER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_USER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@COMPANY_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_COMPANY_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMPANY_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DISTRIBUTOR_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DISTRIBUTOR_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DISTRIBUTOR_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DEPARTMENT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DEPARTMENT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DEPARTMENT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Gender" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_Gender==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Gender;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PAYMENT_MODE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PAYMENT_MODE==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PAYMENT_MODE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SALARY_CHARGED_TO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SALARY_CHARGED_TO==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SALARY_CHARGED_TO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SALARY_STRUCTURE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SALARY_STRUCTURE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SALARY_STRUCTURE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TEMPLATE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_TEMPLATE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TEMPLATE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SHIFT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SHIFT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SHIFT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EmployeeType" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_EmployeeType==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EmployeeType;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LAST_SALARY_DRAWN" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
			if(m_LAST_SALARY_DRAWN==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LAST_SALARY_DRAWN;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EmployeeJoiningDate" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_EmployeeJoiningDate==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EmployeeJoiningDate;
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
			parameter.ParameterName = "@MaritalStatus" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_MaritalStatus;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@FATHER_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_FATHER_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_FATHER_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@HUSBAND_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_HUSBAND_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_HUSBAND_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RELIGION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_RELIGION== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RELIGION;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@NO_OF_DEPENDENTS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_NO_OF_DEPENDENTS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_NO_OF_DEPENDENTS;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LAST_EDUCATION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_LAST_EDUCATION== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LAST_EDUCATION;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LAST_EMPLOYEE_INFO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_LAST_EMPLOYEE_INFO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LAST_EMPLOYEE_INFO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EMERGENCY_POC_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_EMERGENCY_POC_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EMERGENCY_POC_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EMERGENCY_POC_NO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_EMERGENCY_POC_NO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EMERGENCY_POC_NO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BANK_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BANK_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BANK_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BANK_ACCOUNT_TITLE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BANK_ACCOUNT_TITLE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BANK_ACCOUNT_TITLE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BANK_ACCOUNT_NO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BANK_ACCOUNT_NO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BANK_ACCOUNT_NO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@REPORTING_TO_EMPLOYEE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_REPORTING_TO_EMPLOYEE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_REPORTING_TO_EMPLOYEE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LAST_DESIGNATION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_LAST_DESIGNATION== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LAST_DESIGNATION;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LAST_REASON_OF_RESIGNATION" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_LAST_REASON_OF_RESIGNATION== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LAST_REASON_OF_RESIGNATION;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LAST_CONTACT_NO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_LAST_CONTACT_NO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LAST_CONTACT_NO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@REFERANCE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_REFERANCE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_REFERANCE;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@REPORTING_TO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_REPORTING_TO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_REPORTING_TO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@REMARKS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_REMARKS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_REMARKS;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
