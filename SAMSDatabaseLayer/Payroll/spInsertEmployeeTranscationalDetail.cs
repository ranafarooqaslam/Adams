using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class spInsertEmployeeTranscationalDetail
	{
        #region Private Members
        private string sp_Name = "spInsertEmployeeTranscationalDetail";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_CompanyID;
        private int m_DepartmentID;
        private int m_EmployeeLocationID;
        private int m_EmployeeUnitID;
        private int m_EmployeeWorkingScheduleID;
        private int m_EmployeeContractTypeID;
        private int m_SalaryStructureID;
        private int m_TemplateID;
        private int m_ShiftID;
        private int m_ExceptionID;
        private int m_DesignationID;
        private int m_SalaryBehaviorID;
        private long m_EmployeeID;
        private long m_EmployeeTranscationalDetailID;
        #endregion
        #region Public Properties
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
        public int DepartmentID
        {
            set
            {
                m_DepartmentID = value;
            }
            get
            {
                return m_DepartmentID;
            }
        }
        public int EmployeeLocationID
        {
            set
            {
                m_EmployeeLocationID = value;
            }
            get
            {
                return m_EmployeeLocationID;
            }
        }
        public int EmployeeUnitID
        {
            set
            {
                m_EmployeeUnitID = value;
            }
            get
            {
                return m_EmployeeUnitID;
            }
        }
        public int EmployeeWorkingScheduleID
        {
            set
            {
                m_EmployeeWorkingScheduleID = value;
            }
            get
            {
                return m_EmployeeWorkingScheduleID;
            }
        }
        public int EmployeeContractTypeID
        {
            set
            {
                m_EmployeeContractTypeID = value;
            }
            get
            {
                return m_EmployeeContractTypeID;
            }
        }
        public int SalaryStructureID
        {
            set
            {
                m_SalaryStructureID = value;
            }
            get
            {
                return m_SalaryStructureID;
            }
        }
        public int TemplateID
        {
            set
            {
                m_TemplateID = value;
            }
            get
            {
                return m_TemplateID;
            }
        }
        public int ShiftID
        {
            set
            {
                m_ShiftID = value;
            }
            get
            {
                return m_ShiftID;
            }
        }
        public int ExceptionID
        {
            set
            {
                m_ExceptionID = value;
            }
            get
            {
                return m_ExceptionID;
            }
        }
        public int DesignationID
        {
            set
            {
                m_DesignationID = value;
            }
            get
            {
                return m_DesignationID;
            }
        }
        public int SalaryBehaviorID
        {
            set
            {
                m_SalaryBehaviorID = value;
            }
            get
            {
                return m_SalaryBehaviorID;
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
        public long EmployeeTranscationalDetailID
        {
            get
            {
                return m_EmployeeTranscationalDetailID;
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
        public spInsertEmployeeTranscationalDetail()
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
                return m_EmployeeTranscationalDetailID = (long)((IDataParameter)(cmd.Parameters["@EmployeeTranscationalDetailID"])).Value;
                
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
                m_CompanyID = Convert.ToInt32(dr["CompanyID"]);
                m_DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                m_EmployeeLocationID = Convert.ToInt32(dr["EmployeeLocationID"]);
                m_EmployeeUnitID = Convert.ToInt32(dr["EmployeeUnitID"]);
                m_EmployeeWorkingScheduleID = Convert.ToInt32(dr["EmployeeWorkingScheduleID"]);
                m_EmployeeContractTypeID = Convert.ToInt32(dr["EmployeeContractTypeID"]);
                m_SalaryStructureID = Convert.ToInt32(dr["SalaryStructureID"]);
                m_TemplateID = Convert.ToInt32(dr["TemplateID"]);
                m_ShiftID = Convert.ToInt32(dr["ShiftID"]);
                m_ExceptionID = Convert.ToInt32(dr["ExceptionID"]);
                m_DesignationID = Convert.ToInt32(dr["DesignationID"]);
                m_SalaryBehaviorID = Convert.ToInt32(dr["SalaryBehaviorID"]);
                m_EmployeeID = Convert.ToInt64(dr["EmployeeID"]);
                m_EmployeeTranscationalDetailID = Convert.ToInt64(dr["EmployeeTranscationalDetailID"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
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
            parameter.ParameterName = "@DepartmentID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DepartmentID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DepartmentID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeLocationID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_EmployeeLocationID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeLocationID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeUnitID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_EmployeeUnitID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeUnitID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeWorkingScheduleID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_EmployeeWorkingScheduleID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeWorkingScheduleID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EmployeeContractTypeID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_EmployeeContractTypeID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EmployeeContractTypeID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SalaryStructureID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SalaryStructureID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SalaryStructureID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TemplateID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_TemplateID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TemplateID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ShiftID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_ShiftID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ShiftID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ExceptionID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_ExceptionID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ExceptionID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DesignationID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DesignationID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DesignationID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SalaryBehaviorID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SalaryBehaviorID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SalaryBehaviorID;
            }
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
            parameter.ParameterName = "@EmployeeTranscationalDetailID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


        }
        #endregion
	}
}
