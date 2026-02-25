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
    public class AttendanceController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public AttendanceController()
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
        public DataTable SelectAttendance(long p_AttendanceID, long p_EmployeeID, int p_CompanyID, int p_DesignationID, int p_DepartmentID, int p_EmployeeLocationID, bool p_EmployeeIsManager, int p_TYPE_ID, DateTime p_Month)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetAttendance objLeave = new spGetAttendance();
                objLeave.Connection = mConnection;
                objLeave.AttendanceID = p_AttendanceID;
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
        public bool InsertAttendeance(AttendanceModel atd)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertAttendance Attendance = new spInsertAttendance();
                Attendance.Connection = mConnection;
                Attendance.DayofMonth = atd.DayofMonth;
                Attendance.Document_Date = atd.Document_Date;
                Attendance.EmployeeID = atd.EmployeeID;
                Attendance.Note = atd.Note;
                Attendance.TimeOfDay = atd.TimeOfDay;
                Attendance.AttendanceType = atd.AttendanceType;
                Attendance.TimeSheetID = atd.TimeSheetID;
                Attendance.IsLate = atd.IsLate;
                Attendance.User_ID = atd.User_ID;
                return Attendance.ExecuteQuery();
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
        public bool UpdateAttendeance(AttendanceModel atd)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateAttendance Attendance = new spUpdateAttendance();
                Attendance.Connection = mConnection;
                Attendance.AttendanceID = atd.AttendanceID;
                Attendance.DayofMonth = atd.DayofMonth;
                Attendance.EmployeeID = atd.EmployeeID;
                Attendance.Note = atd.Note;
                Attendance.TimeOfDay = atd.TimeOfDay;                
                Attendance.TimeSheetID = atd.TimeSheetID;
                Attendance.AttendanceType = atd.AttendanceType;
                Attendance.IsLate = atd.IsLate;
                Attendance.User_ID = atd.User_ID;
                Attendance.IS_ACTIVE = atd.IS_ACTIVE;
                Attendance.IS_DELETED = atd.IS_DELETED;
                return Attendance.ExecuteQuery();
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
        public DataTable GetAttendance(int p_EmployeeLocationID, int p_DepartmentID, int p_EmployeeID,DateTime p_FROM_DATE, DateTime p_TO_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetAttendanceReport objAttendance = new spGetAttendanceReport();
                objAttendance.Connection = mConnection;
                objAttendance.EmployeeLocationID = p_EmployeeLocationID;
                objAttendance.DepartmentID = p_DepartmentID;
                objAttendance.EmployeeID = p_EmployeeID;
                objAttendance.FROM_DATE = p_FROM_DATE;
                objAttendance.TO_DATE = p_TO_DATE;
                DataTable dt = objAttendance.ExecuteTable();
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
        public DataTable GetAttendanceSummary(int p_EmployeeLocationID, int p_DepartmentID, int p_EmployeeID, DateTime p_FROM_DATE, DateTime p_TO_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetAttendanceSummaryReport objAttendance = new spGetAttendanceSummaryReport();
                objAttendance.Connection = mConnection;
                objAttendance.EmployeeLocationID = p_EmployeeLocationID;
                objAttendance.DepartmentID = p_DepartmentID;
                objAttendance.EmployeeID = p_EmployeeID;
                objAttendance.FROM_DATE = p_FROM_DATE;
                objAttendance.TO_DATE = p_TO_DATE;
                DataTable dt = objAttendance.ExecuteTable();
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
        public DataTable GettAttendanceSales(long p_EmployeeID, int p_CompanyID, int p_DesignationID, int p_DepartmentID, int p_EmployeeLocationID, DateTime p_AttendanceMonth)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetAttendanceSales objEmployee = new spGetAttendanceSales();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.DepartmentID = p_DepartmentID;
                objEmployee.DesignationID = p_DesignationID;
                objEmployee.EmployeeLocationID = p_EmployeeLocationID;
                objEmployee.AttendanceMonth = p_AttendanceMonth;
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
        public bool InsertAttendanceSales(DateTime p_AttendanceMonth, DateTime p_DocumentDate, int p_EmployeeID, int p_Attendance, int p_Absent, int p_UserID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertAttendanceSales mAttendance = new spInsertAttendanceSales();
                mAttendance.Connection = mConnection;
                mAttendance.AttendanceMonth = p_AttendanceMonth;
                mAttendance.DocumentDate = p_DocumentDate;
                mAttendance.EmployeeID = p_EmployeeID;
                mAttendance.Attendance = p_Attendance;
                mAttendance.Absent = p_Absent;
                mAttendance.UserID = p_UserID;
                mAttendance.ExecuteQuery();


                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
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
