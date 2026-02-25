using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;


namespace SAMSDatabaseLayer.Classes
{
	public class spInsertAssets
	{
		#region Private Members
		private string sp_Name = "spInsertAssets" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_AssetTypeID;
		private int m_CompanyID;
		private int m_User_ID;
		private int m_AssetID;
		private DateTime m_Document_Date;
		private decimal m_Amount;
        private string m_RegNo;
		private string m_Make;
		private string m_Model;
		private string m_Color;
		private string m_EngineNo;
		private string m_ChassisNo;
		private string m_Remarks;
		#endregion
		#region Public Properties
		public int AssetTypeID
		{
			set
			{
				m_AssetTypeID = value ;
			}
			get
			{
				return m_AssetTypeID;
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
		public int AssetID
		{
			get
			{
				return m_AssetID;
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
        public string RegNo
        {
            set { m_RegNo = value; ; }
            get { return m_RegNo; }
        }
		public  string Make
		{
			set
			{
				m_Make = value ;
			}
			get
			{
				return m_Make;
			}
		}
		public  string Model
		{
			set
			{
				m_Model = value ;
			}
			get
			{
				return m_Model;
			}
		}
		public  string Color
		{
			set
			{
				m_Color = value ;
			}
			get
			{
				return m_Color;
			}
		}
		public  string EngineNo
		{
			set
			{
				m_EngineNo = value ;
			}
			get
			{
				return m_EngineNo;
			}
		}
		public  string ChassisNo
		{
			set
			{
				m_ChassisNo = value ;
			}
			get
			{
				return m_ChassisNo;
			}
		}
		public  string Remarks
		{
			set
			{
				m_Remarks = value ;
			}
			get
			{
				return m_Remarks;
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
		public spInsertAssets()
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
				m_AssetID = (int)((IDataParameter)(cmd.Parameters["@AssetID"])).Value;
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
				m_AssetTypeID= Convert.ToInt32(dr["AssetTypeID"]);
				m_CompanyID= Convert.ToInt32(dr["CompanyID"]);
				m_User_ID= Convert.ToInt32(dr["User_ID"]);
				m_AssetID= Convert.ToInt32(dr["AssetID"]);
				m_Document_Date= Convert.ToDateTime(dr["Document_Date"]);
				m_Amount= Convert.ToDecimal(dr["Amount"]);
				m_Make= Convert.ToString(dr["Make"]);
				m_Model= Convert.ToString(dr["Model"]);
				m_Color= Convert.ToString(dr["Color"]);
				m_EngineNo= Convert.ToString(dr["EngineNo"]);
				m_ChassisNo= Convert.ToString(dr["ChassisNo"]);
				m_Remarks= Convert.ToString(dr["Remarks"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@AssetTypeID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_AssetTypeID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_AssetTypeID;
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
			parameter.ParameterName = "@AssetID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			parameter.Direction = ParameterDirection.Output;
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
            parameter.ParameterName = "@RegNo";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_RegNo == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_RegNo;
            }
            pparams.Add(parameter);

			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Make" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_Make== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Make;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Model" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_Model== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Model;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Color" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_Color== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Color;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EngineNo" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_EngineNo== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EngineNo;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ChassisNo" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_ChassisNo== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ChassisNo;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@Remarks" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_Remarks== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Remarks;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
