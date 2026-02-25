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
    public class PayRollExpenseController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public PayRollExpenseController()
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
        public DataTable SelectExpenseHead(int p_CompanyID, int p_ExpenseHeadID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectExpenseHead objEmployee = new spSelectExpenseHead();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.ExpenseHeadID = p_ExpenseHeadID;
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
        public DataTable SelectExpense(int p_ExpenseID ,int p_ExpenseHeadID,int p_EmployeeID ,DateTime p_Month, int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectExpense expense = new spSelectExpense();
                expense.Connection = mConnection;
                expense.ExpenseHeadID = p_ExpenseHeadID;
                expense.EmployeeID = p_EmployeeID;
                expense.ExpenseID = p_ExpenseID;
                expense.Month = p_Month;
                expense.CompanyID = p_CompanyID;
                DataTable dt = expense.ExecuteTable();
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
        public int InsertExpense(PayRollExpenseModel model)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertExpense expense = new spInsertExpense();
                expense.Connection = mConnection;
                expense.Amount = model.Amount;
                expense.Document_Date = model.Document_Date;
                expense.EmployeeID = model.EmployeeID;
                expense.ExpenseHeadID = model.ExpenseHeadID;
                expense.Month = model.Month;
                expense.User_ID = model.User_ID;
                expense.Remarks = model.Remarks;
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
        public bool UpdateExpense(PayRollExpenseModel model)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateExpense expense = new spUpdateExpense();
                expense.Connection = mConnection;
                expense.Amount = model.Amount;
                expense.EmployeeID = model.EmployeeID;
                expense.ExpenseHeadID = model.ExpenseHeadID;
                expense.Month = model.Month;
                expense.User_ID = model.User_ID;
                expense.Remarks = model.Remarks;
                expense.IS_DELETED = model.IS_DELETED;
                expense.IS_ACTIVE = model.IS_ACTIVE;
                expense.ExpenseID = model.ExpenseID;
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
        public bool InsertExpenseHead(ExpenseHeadModel ehm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spInsertExpenseHead expense = new spInsertExpenseHead();
                expense.Transaction = mTranscation;
                expense.Connection = mConnection;
                expense.CompanyID = ehm.CompanyID;
                expense.Remarks = ehm.Remarks;
                expense.ExpenseHead_Name = ehm.ExpenseHead_Name;
                expense.User_ID = ehm.User_ID;
                expense.ExecuteQuery();
                mTranscation.Commit();
                return true;
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
        public bool UpdateExpenseHead(ExpenseHeadModel ehm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spUpdateExpenseHead expense = new spUpdateExpenseHead();
                expense.Transaction = mTranscation;
                expense.Connection = mConnection;
                expense.ExpenseHeadID = ehm.ExpenseHeadID;
                expense.ExpenseHead_Name = ehm.ExpenseHead_Name;
                expense.Remarks = ehm.Remarks;
                expense.User_ID = ehm.User_ID;
                expense.IS_ACTIVE = ehm.IS_ACTIVE;
                expense.IS_DELETED = ehm.IS_DELETED;
                expense.ExecuteQuery();
                mTranscation.Commit();
                return true;
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
