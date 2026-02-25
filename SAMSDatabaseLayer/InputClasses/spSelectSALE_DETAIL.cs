using System;
using System.Collections.Generic;
using System.Text;
using SAMSDataAccessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;

namespace SAMSDatabaseLayer.Classes
{
    public class spSelectSALE_DETAIL
    {
        #region Private Members
        private string sp_Name = "spSelectSALE_DETAIL";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_DISTRIBUTOR_ID;
        //private int m_SKU_ID;
        //private int m_QUANTITY;
        //private long m_SALE_INVOICE_DETAIL_ID;
        private long m_SALE_INVOICE_ID;
       // private string m_BATCH_NO;
        #endregion
        #region Public Properties
        public int DISTRIBUTOR_ID
        {
            set
            {
                m_DISTRIBUTOR_ID = value;
            }
            get
            {
                return m_DISTRIBUTOR_ID;
            }
        }
        //public int SKU_ID
        //{
        //    set
        //    {
        //        m_SKU_ID = value;
        //    }
        //    get
        //    {
        //        return m_SKU_ID;
        //    }
        //}
        //public int QUANTITY
        //{
        //    set
        //    {
        //        m_QUANTITY = value;
        //    }
        //    get
        //    {
        //        return m_QUANTITY;
        //    }
        //}
        //public long SALE_INVOICE_DETAIL_ID
        //{
        //    set
        //    {
        //        m_SALE_INVOICE_DETAIL_ID = value;
        //    }
        //    get
        //    {
        //        return m_SALE_INVOICE_DETAIL_ID;
        //    }
        //}
        public long SALE_INVOICE_ID
        {
            set
            {
                m_SALE_INVOICE_ID = value;
            }
            get
            {
                return m_SALE_INVOICE_ID;
            }
        }
        //public string BATCH_NO
        //{
        //    set
        //    {
        //        m_BATCH_NO = value;
        //    }
        //    get
        //    {
        //        return m_BATCH_NO;
        //    }
        //}


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
        public spSelectSALE_DETAIL()
        {
        }
        #endregion
        #region public Methods
        public bool ExecuteQuery()
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
                return true;
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
                m_DISTRIBUTOR_ID = Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
                //m_SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                //m_QUANTITY = Convert.ToInt32(dr["QUANTITY"]);
                //m_SALE_INVOICE_DETAIL_ID = Convert.ToInt64(dr["SALE_INVOICE_DETAIL_ID"]);
                m_SALE_INVOICE_ID = Convert.ToInt64(dr["SALE_INVOICE_ID"]);
              //  m_BATCH_NO = Convert.ToString(dr["BATCH_NO"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DISTRIBUTOR_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DISTRIBUTOR_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DISTRIBUTOR_ID;
            }
            pparams.Add(parameter);


            //parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            //parameter.ParameterName = "@SKU_ID";
            //parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            //if (m_SKU_ID == Constants.IntNullValue)
            //{
            //    parameter.Value = DBNull.Value;
            //}
            //else
            //{
            //    parameter.Value = m_SKU_ID;
            //}
            //pparams.Add(parameter);


            //parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            //parameter.ParameterName = "@QUANTITY";
            //parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            //if (m_QUANTITY == Constants.IntNullValue)
            //{
            //    parameter.Value = DBNull.Value;
            //}
            //else
            //{
            //    parameter.Value = m_QUANTITY;
            //}
            //pparams.Add(parameter);


            //parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            //parameter.ParameterName = "@SALE_INVOICE_DETAIL_ID";
            //parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            //if (m_SALE_INVOICE_DETAIL_ID == Constants.LongNullValue)
            //{
            //    parameter.Value = DBNull.Value;
            //}
            //else
            //{
            //    parameter.Value = m_SALE_INVOICE_DETAIL_ID;
            //}
            //pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SALE_INVOICE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_SALE_INVOICE_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SALE_INVOICE_ID;
            }
            pparams.Add(parameter);


            //parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            //parameter.ParameterName = "@BATCH_NO";
            //parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            //if (m_BATCH_NO == null)
            //{
            //    parameter.Value = DBNull.Value;
            //}
            //else
            //{
            //    parameter.Value = m_BATCH_NO;
            //}
            //pparams.Add(parameter);


        }
        #endregion
    }
}
