using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;


namespace SAMSDatabaseLayer.Classes
{
	public class spInsertSalaryStructureArchiveDetail
	{
		#region Private Members
		private string sp_Name = "spInsertSalaryStructureArchiveDetail" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_SalaryStructureDetailID;
		private int m_SalaryStructureID;
		private int m_AllowanceID;
		private int m_DeductionID;
		private int m_AmountTypeID;
		private int m_SalaryStructureArchiveDetailID;
		private decimal m_Amount;
		private string m_Comment;
		#endregion
		#region Public Properties
		public int SalaryStructureDetailID
		{
			set
			{
				m_SalaryStructureDetailID = value ;
			}
			get
			{
				return m_SalaryStructureDetailID;
			}
		}
		public int SalaryStructureID
		{
			set
			{
				m_SalaryStructureID = value ;
			}
			get
			{
				return m_SalaryStructureID;
			}
		}
		public int AllowanceID
		{
			set
			{
				m_AllowanceID = value ;
			}
			get
			{
				return m_AllowanceID;
			}
		}
		public int DeductionID
		{
			set
			{
				m_DeductionID = value ;
			}
			get
			{
				return m_DeductionID;
			}
		}
		public int AmountTypeID
		{
			set
			{
				m_AmountTypeID = value ;
			}
			get
			{
				return m_AmountTypeID;
			}
		}
		public int SalaryStructureArchiveDetailID
		{
			get
			{
				return m_SalaryStructureArchiveDetailID;
			}
		}
		public decimal Amount
		{
			set
			{
				m_Amount = value ;
			}
			get
			{
				return m_Amount;
			}
		}
		public  string Comment
		{
			set
			{
				m_Comment = value ;
			}
			get
			{
				return m_Comment;
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
		public spInsertSalaryStructureArchiveDetail()
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
			return	m_SalaryStructureArchiveDetailID = (int)((IDataParameter)(cmd.Parameters["@SalaryStructureArchiveDetailID"])).Value;
				 
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
				m_SalaryStructureDetailID= Convert.ToInt32(dr["SalaryStructureDetailID"]);
				m_SalaryStructureID= Convert.ToInt32(dr["SalaryStructureID"]);
				m_AllowanceID= Convert.ToInt32(dr["AllowanceID"]);
				m_DeductionID= Convert.ToInt32(dr["DeductionID"]);
				m_AmountTypeID= Convert.ToInt32(dr["AmountTypeID"]);
				m_SalaryStructureArchiveDetailID= Convert.ToInt32(dr["SalaryStructureArchiveDetailID"]);
				m_Amount= Convert.ToDecimal(dr["Amount"]);
				m_Comment= Convert.ToString(dr["Comment"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SalaryStructureDetailID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SalaryStructureDetailID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SalaryStructureDetailID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SalaryStructureID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SalaryStructureID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SalaryStructureID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@AllowanceID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_AllowanceID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_AllowanceID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DeductionID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DeductionID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DeductionID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@AmountTypeID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_AmountTypeID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_AmountTypeID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@SalaryStructureArchiveDetailID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			parameter.Direction = ParameterDirection.Output;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Amount" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Decimal);
			if(m_Amount==Constants.DecimalNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Amount;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Comment" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_Comment== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Comment;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
