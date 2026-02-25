using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class spUpdateCOMPANY
	{
		#region Private Members
		private string sp_Name = "spUpdateCOMPANY" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_CompanyID;
		private int m_COMPANY_GROUP_ID;
		private int m_PayPeriodFrom;
		private int m_PayPeriodTo;
		private int m_User_ID;
		private DateTime m_Document_Date;
        private DateTime m_FiscalYearFrom;
        private DateTime m_FiscalYearTo;
		private bool m_IS_DELETED;
		private bool m_IS_ACTIVE;
		private string m_CompanyName;
		private string m_CompanyTagline;
		private string m_CompanyLogoPath;
		private string m_CompanyServiceTaxNumber;
		private string m_CompanyNTNnumber;
		private string m_CompanyGSTNumber;
		private string m_CompanyUAN;
		private string m_CompanyURL;
		private string m_CompanyChamberRefNumber;
		private string m_CompanyImportExportLicenseNumber;
		private string m_CompanyFormFooter;
		private string m_CompanyReportFooter;
		#endregion
		#region Public Properties
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
		public int COMPANY_GROUP_ID
		{
			set
			{
				m_COMPANY_GROUP_ID = value ;
			}
			get
			{
				return m_COMPANY_GROUP_ID;
			}
		}
		public int PayPeriodFrom
		{
			set
			{
				m_PayPeriodFrom = value ;
			}
			get
			{
				return m_PayPeriodFrom;
			}
		}
		public int PayPeriodTo
		{
			set
			{
				m_PayPeriodTo = value ;
			}
			get
			{
				return m_PayPeriodTo;
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
		public DateTime Document_Date
		{
			set
			{
				m_Document_Date = value ;
			}
			get
			{
				return m_Document_Date;
			}
		}
        public DateTime FiscalYearFrom
        {
            set { m_FiscalYearFrom = value; }
            get { return m_FiscalYearFrom; }
        }
        public DateTime FiscalYearTo
        {
            set { m_FiscalYearTo = value; }
            get { return m_FiscalYearTo; }
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
		public  string CompanyName
		{
			set
			{
				m_CompanyName = value ;
			}
			get
			{
				return m_CompanyName;
			}
		}
		public  string CompanyTagline
		{
			set
			{
				m_CompanyTagline = value ;
			}
			get
			{
				return m_CompanyTagline;
			}
		}
		public  string CompanyLogoPath
		{
			set
			{
				m_CompanyLogoPath = value ;
			}
			get
			{
				return m_CompanyLogoPath;
			}
		}
		public  string CompanyServiceTaxNumber
		{
			set
			{
				m_CompanyServiceTaxNumber = value ;
			}
			get
			{
				return m_CompanyServiceTaxNumber;
			}
		}
		public  string CompanyNTNnumber
		{
			set
			{
				m_CompanyNTNnumber = value ;
			}
			get
			{
				return m_CompanyNTNnumber;
			}
		}
		public  string CompanyGSTNumber
		{
			set
			{
				m_CompanyGSTNumber = value ;
			}
			get
			{
				return m_CompanyGSTNumber;
			}
		}
		public  string CompanyUAN
		{
			set
			{
				m_CompanyUAN = value ;
			}
			get
			{
				return m_CompanyUAN;
			}
		}
		public  string CompanyURL
		{
			set
			{
				m_CompanyURL = value ;
			}
			get
			{
				return m_CompanyURL;
			}
		}
		public  string CompanyChamberRefNumber
		{
			set
			{
				m_CompanyChamberRefNumber = value ;
			}
			get
			{
				return m_CompanyChamberRefNumber;
			}
		}
		public  string CompanyImportExportLicenseNumber
		{
			set
			{
				m_CompanyImportExportLicenseNumber = value ;
			}
			get
			{
				return m_CompanyImportExportLicenseNumber;
			}
		}
		public  string CompanyFormFooter
		{
			set
			{
				m_CompanyFormFooter = value ;
			}
			get
			{
				return m_CompanyFormFooter;
			}
		}
		public  string CompanyReportFooter
		{
			set
			{
				m_CompanyReportFooter = value ;
			}
			get
			{
				return m_CompanyReportFooter;
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
		public spUpdateCOMPANY()
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
				m_CompanyID= Convert.ToInt32(dr["CompanyID"]);
				m_COMPANY_GROUP_ID= Convert.ToInt32(dr["COMPANY_GROUP_ID"]);
				m_PayPeriodFrom= Convert.ToInt32(dr["PayPeriodFrom"]);
				m_PayPeriodTo= Convert.ToInt32(dr["PayPeriodTo"]);
				m_User_ID= Convert.ToInt32(dr["User_ID"]);
				m_Document_Date= Convert.ToDateTime(dr["Document_Date"]);
				m_IS_DELETED=Convert.ToBoolean(dr["IS_DELETED"]);
				m_IS_ACTIVE=Convert.ToBoolean(dr["IS_ACTIVE"]);
				m_CompanyName= Convert.ToString(dr["CompanyName"]);
				m_CompanyTagline= Convert.ToString(dr["CompanyTagline"]);
				m_CompanyLogoPath= Convert.ToString(dr["CompanyLogoPath"]);
				m_CompanyServiceTaxNumber= Convert.ToString(dr["CompanyServiceTaxNumber"]);
				m_CompanyNTNnumber= Convert.ToString(dr["CompanyNTNnumber"]);
				m_CompanyGSTNumber= Convert.ToString(dr["CompanyGSTNumber"]);
				m_CompanyUAN= Convert.ToString(dr["CompanyUAN"]);
				m_CompanyURL= Convert.ToString(dr["CompanyURL"]);
				m_CompanyChamberRefNumber= Convert.ToString(dr["CompanyChamberRefNumber"]);
				m_CompanyImportExportLicenseNumber= Convert.ToString(dr["CompanyImportExportLicenseNumber"]);
				m_CompanyFormFooter= Convert.ToString(dr["CompanyFormFooter"]);
				m_CompanyReportFooter= Convert.ToString(dr["CompanyReportFooter"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
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
			parameter.ParameterName = "@COMPANY_GROUP_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_COMPANY_GROUP_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_COMPANY_GROUP_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PayPeriodFrom" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PayPeriodFrom==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PayPeriodFrom;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PayPeriodTo" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PayPeriodTo==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PayPeriodTo;
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
			parameter.ParameterName = "@Document_Date" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_Document_Date==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Document_Date;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IS_DELETED" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_DELETED;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IS_ACTIVE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_IS_ACTIVE;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyName" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyName== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyName;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyTagline" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyTagline== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyTagline;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyLogoPath" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyLogoPath== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyLogoPath;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyServiceTaxNumber" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyServiceTaxNumber== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyServiceTaxNumber;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyNTNnumber" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyNTNnumber== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyNTNnumber;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyGSTNumber" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyGSTNumber== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyGSTNumber;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyUAN" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyUAN== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyUAN;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyURL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyURL== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyURL;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyChamberRefNumber" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyChamberRefNumber== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyChamberRefNumber;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyImportExportLicenseNumber" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyImportExportLicenseNumber== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyImportExportLicenseNumber;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyFormFooter" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyFormFooter== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyFormFooter;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CompanyReportFooter" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CompanyReportFooter== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CompanyReportFooter;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@FiscalYearFrom";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_FiscalYearFrom == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_FiscalYearFrom;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@FiscalYearTo";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_FiscalYearTo == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_FiscalYearTo;
            }
            pparams.Add(parameter);


		}
		#endregion
	}
}
