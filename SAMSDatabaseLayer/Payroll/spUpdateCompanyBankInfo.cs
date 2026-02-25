using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;


namespace SAMSDatabaseLayer.Classes
{
	public class spUpdateCompanyBankInfo
	{
		#region Private Members
		private string sp_Name = "spUpdateCompanyBankInfo" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_CompanyBankInfoID;
		private int m_CompanyID;
		private int m_User_ID;
		private bool m_IS_ACTIVE;
		private bool m_IS_DELETED;
		private string m_BankAccountNumber;
		private string m_BankName;
		private string m_BankAccountType;
		private string m_BankCode;
		private string m_BankBranch;
		private string m_ContactPerson;
		#endregion
		#region Public Properties
		public int CompanyBankInfoID
		{
			set
			{
				m_CompanyBankInfoID = value ;
			}
			get
			{
				return m_CompanyBankInfoID;
			}
		}
		public int CompanyID
		{
			set
			{
				m_CompanyID = value ;
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
				m_User_ID = value ;
			}
			get
			{
				return m_User_ID;
			}
		}
		public bool IS_ACTIVE
		{
			set
			{
				m_IS_ACTIVE = value ;
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
				m_IS_DELETED = value ;
			}
			get
			{
				return m_IS_DELETED;
			}
		}
		public  string BankAccountNumber
		{
			set
			{
				m_BankAccountNumber = value ;
			}
			get
			{
				return m_BankAccountNumber;
			}
		}
		public  string BankName
		{
			set
			{
				m_BankName = value ;
			}
			get
			{
				return m_BankName;
			}
		}
		public  string BankAccountType
		{
			set
			{
				m_BankAccountType = value ;
			}
			get
			{
				return m_BankAccountType;
			}
		}
		public  string BankCode
		{
			set
			{
				m_BankCode = value ;
			}
			get
			{
				return m_BankCode;
			}
		}
		public  string BankBranch
		{
			set
			{
				m_BankBranch = value ;
			}
			get
			{
				return m_BankBranch;
			}
		}
		public  string ContactPerson
		{
			set
			{
				m_ContactPerson = value ;
			}
			get
			{
				return m_ContactPerson;
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
		public spUpdateCompanyBankInfo()
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
				m_CompanyBankInfoID= Convert.ToInt32(dr["CompanyBankInfoID"]);
				m_CompanyID= Convert.ToInt32(dr["CompanyID"]);
				m_User_ID= Convert.ToInt32(dr["User_ID"]);
				m_IS_ACTIVE=Convert.ToBoolean(dr["IS_ACTIVE"]);
				m_IS_DELETED=Convert.ToBoolean(dr["IS_DELETED"]);
				m_BankAccountNumber= Convert.ToString(dr["BankAccountNumber"]);
				m_BankName= Convert.ToString(dr["BankName"]);
				m_BankAccountType= Convert.ToString(dr["BankAccountType"]);
				m_BankCode= Convert.ToString(dr["BankCode"]);
				m_BankBranch= Convert.ToString(dr["BankBranch"]);
				m_ContactPerson= Convert.ToString(dr["ContactPerson"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyBankInfoID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CompanyBankInfoID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyBankInfoID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_CompanyID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@User_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_User_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_User_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IS_ACTIVE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_ACTIVE;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IS_DELETED" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_DELETED;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BankAccountNumber" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BankAccountNumber== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BankAccountNumber;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BankName" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BankName== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BankName;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BankAccountType" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BankAccountType== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BankAccountType;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BankCode" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BankCode== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BankCode;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BankBranch" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_BankBranch== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BankBranch;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ContactPerson" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_ContactPerson== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ContactPerson;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}