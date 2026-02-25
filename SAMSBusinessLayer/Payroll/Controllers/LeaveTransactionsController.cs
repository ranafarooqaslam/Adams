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
    public class LeaveTransactionsController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public LeaveTransactionsController()
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
        public DataTable SelectEmployee(long p_EmployeeID, int p_CompanyID ,int p_DesignationID , int p_DepartmentID  ,int p_EmployeeLocationID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEmployee objEmployee = new spSelectEmployee();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.DepartmentID = p_DepartmentID;
                objEmployee.DesignationID = p_DesignationID;
                objEmployee.EmployeeLocationID = p_EmployeeLocationID;
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
        public DataTable SelectLeave(long p_LeaveTransactionsID,long p_EmployeeID, int p_CompanyID, int p_DesignationID, int p_DepartmentID, int p_EmployeeLocationID, bool p_EmployeeIsManager, int p_TYPE_ID, DateTime p_Month)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetLeave objLeave = new spGetLeave();
                objLeave.Connection = mConnection;
                objLeave.LeaveTransactionsID = p_LeaveTransactionsID;
                objLeave.CompanyID = p_CompanyID;
                objLeave.EmployeeID = p_EmployeeID;
                objLeave.DepartmentID = p_DepartmentID;
                objLeave.DesignationID = p_DesignationID;
                objLeave.EmployeeLocationID = p_EmployeeLocationID;
                objLeave.EmployeeIsManager = p_EmployeeIsManager;
                objLeave.TYPE_ID = p_TYPE_ID;
                objLeave.MONTH = p_Month;
                DataTable dt = objLeave.ExecuteTable();
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
        public int InsertLeaveTransactions(LeaveTransactionsModel Leave)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertLeaveTransactions LeaveTranscation = new spInsertLeaveTransactions();
                LeaveTranscation.Connection = mConnection;
                LeaveTranscation.Document_Date = Leave.Document_Date;
                LeaveTranscation.EmployeeID = Leave.EmployeeID;
                LeaveTranscation.LeaveFrom = Leave.LeaveFrom;
                LeaveTranscation.LeaveID = Leave.LeaveID;
                LeaveTranscation.LeaveTo = Leave.LeaveTo;
                LeaveTranscation.Note = Leave.Note;
                LeaveTranscation.NumberofDays = Leave.NumberofDays;
                LeaveTranscation.TimeSheetID = Leave.TimeSheetID;
                LeaveTranscation.User_ID = Leave.User_ID;
                return LeaveTranscation.ExecuteQuery();
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
        public bool UpdateLeaveTransactions(LeaveTransactionsModel Leave)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateLeaveTransactions LeaveTranscation = new spUpdateLeaveTransactions();
                LeaveTranscation.Connection = mConnection;
                LeaveTranscation.LeaveTransactionsID = Leave.LeaveTransactionsID;
                LeaveTranscation.EmployeeID = Leave.EmployeeID;
                LeaveTranscation.LeaveFrom = Leave.LeaveFrom;
                LeaveTranscation.LeaveID = Leave.LeaveID;
                LeaveTranscation.LeaveTo = Leave.LeaveTo;
                LeaveTranscation.Note = Leave.Note;
                LeaveTranscation.NumberofDays = Leave.NumberofDays;
                LeaveTranscation.TimeSheetID = Leave.TimeSheetID;
                LeaveTranscation.User_ID = Leave.User_ID;
                LeaveTranscation.IS_ACTIVE = Leave.IS_ACTIVE;
                LeaveTranscation.IS_DELETED = Leave.IS_DELETED;
                return LeaveTranscation.ExecuteQuery();
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
