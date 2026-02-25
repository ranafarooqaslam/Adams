using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class spInsertLeaveTransactions
	{
		#region Private Members
		private string sp_Name = "spInsertLeaveTransactions" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_LeaveID;
		private int m_TimeSheetID;
		private int m_EmployeeID;
		private int m_User_ID;
		private int m_LeaveTransactionsID;
		private DateTime m_LeaveFrom;
		private DateTime m_LeaveTo;
		private DateTime m_Document_Date;
		private decimal m_NumberofDays;
		private string m_Note;
		#endregion
		#region Public Properties
		public int LeaveID
		{
			set
			{
				m_LeaveID = value ;
			}
			get
			{
				return m_LeaveID;
			}
		}
		public int TimeSheetID
		{
			set
			{
				m_TimeSheetID = value ;
			}
			get
			{
				return m_TimeSheetID;
			}
		}
		public int EmployeeID
		{
			set
			{
				m_EmployeeID = value ;
			}
			get
			{
				return m_EmployeeID;
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
		public int LeaveTransactionsID
		{
			get
			{
				return m_LeaveTransactionsID;
			}
		}
		public DateTime LeaveFrom
		{
			set
			{
				m_LeaveFrom = value ;
			}
			get
			{
				return m_LeaveFrom;
			}
		}
		public DateTime LeaveTo
		{
			set
			{
				m_LeaveTo = value ;
			}
			get
			{
				return m_LeaveTo;
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
		public decimal NumberofDays
		{
			set
			{
				m_NumberofDays = value ;
			}
			get
			{
				return m_NumberofDays;
			}
		}
		public  string Note
		{
			set
			{
				m_Note = value ;
			}
			get
			{
				return m_Note;
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
		public spInsertLeaveTransactions()
		{
		}
		#endregion
		#region public Methods
		public int  ExecuteQuery()
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
                return m_LeaveTransactionsID = (int)((IDataParameter)(cmd.Parameters["@LeaveTransactionsID"])).Value;
				 
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
				m_LeaveID= Convert.ToInt32(dr["LeaveID"]);
				m_TimeSheetID= Convert.ToInt32(dr["TimeSheetID"]);
				m_EmployeeID= Convert.ToInt32(dr["EmployeeID"]);
				m_User_ID= Convert.ToInt32(dr["User_ID"]);
				m_LeaveTransactionsID= Convert.ToInt32(dr["LeaveTransactionsID"]);
				m_LeaveFrom= Convert.ToDateTime(dr["LeaveFrom"]);
				m_LeaveTo= Convert.ToDateTime(dr["LeaveTo"]);
				m_Document_Date= Convert.ToDateTime(dr["Document_Date"]);
				m_NumberofDays= Convert.ToDecimal(dr["NumberofDays"]);
				m_Note= Convert.ToString(dr["Note"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LeaveID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_LeaveID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LeaveID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TimeSheetID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_TimeSheetID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TimeSheetID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EmployeeID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_EmployeeID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EmployeeID;
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
			parameter.ParameterName = "@LeaveTransactionsID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			parameter.Direction = ParameterDirection.Output;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LeaveFrom" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_LeaveFrom==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LeaveFrom;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LeaveTo" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_LeaveTo==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LeaveTo;
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
			parameter.ParameterName = "@NumberofDays" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_NumberofDays==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_NumberofDays;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Note" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_Note== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Note;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
