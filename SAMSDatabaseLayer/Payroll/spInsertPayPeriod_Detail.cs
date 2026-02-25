using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class spInsertPayPeriod_Detail
	{
		#region Private Members
		private string sp_Name = "spInsertPayPeriod_Detail" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_PayPeriodID;
		private decimal m_BasicSalary;
		private decimal m_WorkingDays;
		private decimal m_Allowances;
		private decimal m_Deductions;
		private decimal m_NoOfLeave;
		private decimal m_LoansReturned;
		private decimal m_NetPay;
		private decimal m_IncomeTax;
		private decimal m_Leave_Deduction;
		private decimal m_BounceAmount;
		private long m_EmployeeID;
		private long m_PayPeriodDetailID;
		#endregion
		#region Public Properties
		public int PayPeriodID
		{
			set
			{
				m_PayPeriodID = value ;
			}
			get
			{
				return m_PayPeriodID;
			}
		}
		public decimal BasicSalary
		{
			set
			{
				m_BasicSalary = value ;
			}
			get
			{
				return m_BasicSalary;
			}
		}
		public decimal WorkingDays
		{
			set
			{
				m_WorkingDays = value ;
			}
			get
			{
				return m_WorkingDays;
			}
		}
		public decimal Allowances
		{
			set
			{
				m_Allowances = value ;
			}
			get
			{
				return m_Allowances;
			}
		}
		public decimal Deductions
		{
			set
			{
				m_Deductions = value ;
			}
			get
			{
				return m_Deductions;
			}
		}
		public decimal NoOfLeave
		{
			set
			{
				m_NoOfLeave = value ;
			}
			get
			{
				return m_NoOfLeave;
			}
		}
		public decimal LoansReturned
		{
			set
			{
				m_LoansReturned = value ;
			}
			get
			{
				return m_LoansReturned;
			}
		}
		public decimal NetPay
		{
			set
			{
				m_NetPay = value ;
			}
			get
			{
				return m_NetPay;
			}
		}
		public decimal IncomeTax
		{
			set
			{
				m_IncomeTax = value ;
			}
			get
			{
				return m_IncomeTax;
			}
		}
		public decimal Leave_Deduction
		{
			set
			{
				m_Leave_Deduction = value ;
			}
			get
			{
				return m_Leave_Deduction;
			}
		}
		public decimal BounceAmount
		{
			set
			{
				m_BounceAmount = value ;
			}
			get
			{
				return m_BounceAmount;
			}
		}
		public long EmployeeID
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
		public long PayPeriodDetailID
		{
			get
			{
				return m_PayPeriodDetailID;
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
		public spInsertPayPeriod_Detail()
		{
		}
		#endregion
		#region public Methods
		public long  ExecuteQuery()
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
                return m_PayPeriodDetailID = (long)((IDataParameter)(cmd.Parameters["@PayPeriodDetailID"])).Value;
				 
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
				m_PayPeriodID= Convert.ToInt32(dr["PayPeriodID"]);
				m_BasicSalary= Convert.ToDecimal(dr["BasicSalary"]);
				m_WorkingDays= Convert.ToDecimal(dr["WorkingDays"]);
				m_Allowances= Convert.ToDecimal(dr["Allowances"]);
				m_Deductions= Convert.ToDecimal(dr["Deductions"]);
				m_NoOfLeave= Convert.ToDecimal(dr["NoOfLeave"]);
				m_LoansReturned= Convert.ToDecimal(dr["LoansReturned"]);
				m_NetPay= Convert.ToDecimal(dr["NetPay"]);
				m_IncomeTax= Convert.ToDecimal(dr["IncomeTax"]);
				m_Leave_Deduction= Convert.ToDecimal(dr["Leave_Deduction"]);
				m_BounceAmount= Convert.ToDecimal(dr["BounceAmount"]);
				m_EmployeeID=Convert.ToInt64(dr["EmployeeID"]);
				m_PayPeriodDetailID=Convert.ToInt64(dr["PayPeriodDetailID"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PayPeriodID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PayPeriodID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PayPeriodID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BasicSalary" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_BasicSalary==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BasicSalary;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@WorkingDays" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_WorkingDays==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_WorkingDays;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Allowances" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_Allowances==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Allowances;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Deductions" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_Deductions==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Deductions;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@NoOfLeave" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_NoOfLeave==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_NoOfLeave;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LoansReturned" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_LoansReturned==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LoansReturned;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@NetPay" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_NetPay==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_NetPay;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@IncomeTax" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_IncomeTax==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_IncomeTax;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Leave_Deduction" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_Leave_Deduction==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Leave_Deduction;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@BounceAmount" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_BounceAmount==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_BounceAmount;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EmployeeID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_EmployeeID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EmployeeID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PayPeriodDetailID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			parameter.Direction = ParameterDirection.Output;
			pparams.Add(parameter);


		}
		#endregion
	}
}
