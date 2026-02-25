using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using SAMSBusinessLayer.Models;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Company Related Tasks
    /// <remarks>
    /// <list type="bullet">
    /// <item>Insert Company</item>
    /// <item>Get Company</item>
    /// </list>
    /// </remarks>
    /// </summary>
    public class TaxRuleController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public TaxRuleController()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Gets Company Data
        /// </summary>
        /// <remarks>
        /// Returns Company Data as Datatable
        /// </remarks>
        /// <param name="p_COMPANY_ID">Company</param>
        /// <param name="p_STATUS">Status</param>
        /// <returns>Company Data as Datatable</returns>
        public DataTable SelectTaxRule(int p_CompanyID, int p_TaxRuleID )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectTaxRule objEmployee = new spSelectTaxRule();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.TaxRuleID = p_TaxRuleID;
                DataTable dt = objEmployee.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public int InsertTaxRule(TaxRuleModel model)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertTaxRule expense = new spInsertTaxRule();
                expense.Connection = mConnection;
                expense.Amount = model.Amount;
                expense.Document_Date = model.Document_Date;
                expense.AmountFrom = model.AmountFrom;
                expense.AmountTo = model.AmountTo;
                expense.User_ID = model.User_ID;
                expense.CompanyID = model.CompanyID;
                expense.Percentage = model.Percentage;
                return expense.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public int InsertTaxRuleArchive(TaxRuleModel model)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertTaxRuleArchive expense = new spInsertTaxRuleArchive();
                expense.Connection = mConnection;
                expense.Amount = model.Amount;
                expense.Document_Date = model.Document_Date;
                expense.AmountFrom = model.AmountFrom;
                expense.AmountTo = model.AmountTo;
                expense.User_ID = model.User_ID;
                expense.CompanyID = model.CompanyID;
                expense.Percentage = model.Percentage;
                expense.TaxRuleID = model.TaxRuleID;
                return expense.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdateTaxRule(TaxRuleModel model)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateTaxRule expense = new spUpdateTaxRule();
                expense.Connection = mConnection;
                expense.Amount = model.Amount;
                expense.AmountFrom = model.AmountFrom;
                expense.AmountTo = model.AmountTo;
                expense.User_ID = model.User_ID;
                expense.CompanyID = model.CompanyID;
                expense.Percentage = model.Percentage;
                expense.IS_ACTIVE = model.IS_ACTIVE;
                expense.IS_DELETED = model.IS_DELETED;
                expense.TaxRuleID = model.TaxRuleID;
                return expense.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
    }
}
