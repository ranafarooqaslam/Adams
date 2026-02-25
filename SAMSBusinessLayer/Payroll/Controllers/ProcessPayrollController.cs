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
    public class ProcessPayrollController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public ProcessPayrollController()
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
        public DataTable SELECT_PayRollForPayPeriod(int p_COMMPANY_ID ,long p_EmployeeID,int p_LOCATIONID, int p_DEPARTMENTID ,bool p_IS_ACTIVE, DateTime p_START_DATE, DateTime p_END_DATE, int p_SALAR_BEHAVIOR)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSELECT_PayRollForPayPeriod objEmployee = new spSELECT_PayRollForPayPeriod();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.SALAR_BEHAVIOR = p_SALAR_BEHAVIOR;
                objEmployee.IS_ACTIVE = p_IS_ACTIVE;
                objEmployee.LOCATIONID = p_LOCATIONID;
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
        public DataTable SELECT_PAY_PERIOD(int p_COMMPANY_ID, bool p_IS_POST)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSELECT_PAY_PERIOD objEmployee = new spSELECT_PAY_PERIOD();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.IS_POST = p_IS_POST;
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
        public DataTable Select_Installments_ProcessPayroll(DateTime p_PeriodStartDate, DateTime  p_PeriodEndDate , int p_CompannyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelect_Installments_ProcessPayroll objEmployee = new spSelect_Installments_ProcessPayroll();
                objEmployee.Connection = mConnection;
                objEmployee.CompannyID = p_CompannyID;
                objEmployee.PeriodEndDate = p_PeriodEndDate;
                objEmployee.PeriodStartDate = p_PeriodStartDate;
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
        
        public int InsertPayPeriod_Master(int p_CompanyID, DateTime p_PeriodStartDate, DateTime p_PeriodEndDate, int p_User_ID, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPayPeriod_Master expense = new spInsertPayPeriod_Master();
                expense.Connection = mConnection;
                expense.CompanyId = p_CompanyID;
                expense.Document_Date = p_Document_Date;
                expense.PeriodEndDate = p_PeriodEndDate;
                expense.PeriodStartDate = p_PeriodStartDate;
                expense.User_ID = p_User_ID;
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
        public long InsertPayPeriod_Detail(int p_PayPeriodID , long p_EmployeeID  ,decimal p_BasicSalary ,decimal p_WorkingDays ,decimal p_Allowances , decimal p_Deductions,decimal p_NoOfLeave, decimal p_LoansReturned,decimal p_NetPay ,decimal p_IncomeTax,decimal p_Leave_Deduction,decimal p_BounceAmount)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPayPeriod_Detail expense = new spInsertPayPeriod_Detail();
                expense.Connection = mConnection;
                expense.Allowances = p_Allowances;
                expense.BasicSalary = p_BasicSalary;
                expense.BounceAmount = p_BounceAmount;
                expense.Deductions = p_Deductions;
                expense.EmployeeID = p_EmployeeID;
                expense.IncomeTax = p_IncomeTax;
                expense.Leave_Deduction = p_Leave_Deduction;
                expense.LoansReturned = p_LoansReturned;
                expense.NetPay = p_NetPay;
                expense.NoOfLeave = p_NoOfLeave;
                expense.PayPeriodID = p_PayPeriodID;
                expense.WorkingDays = p_WorkingDays;
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
        public bool UpdateInstallments(int InstallmentID, decimal InstallmentAmount)
        {
            string CommandText = "Update Installments set AmountPaid=" + InstallmentAmount+ " WHERE InstallmentID="+InstallmentID;
            IDbConnection mConnection = null;
            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            spUpdateEmployee Employee = new spUpdateEmployee();
            IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = CommandText;
            cmd.Connection = mConnection;
            cmd.ExecuteNonQuery();
            return true;
        }
        public bool PostPayPeriod(int payPeriodID)
        {
            string CommandText = "Update PayPeriod_Master set IsPosted=" + 1 + " WHERE PayPeriodID=" + payPeriodID;
            IDbConnection mConnection = null;
            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            spUpdateEmployee Employee = new spUpdateEmployee();
            IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = CommandText;
            cmd.Connection = mConnection;
            cmd.ExecuteNonQuery();
            return true;
        }
        //public int InsertTaxRuleArchive(TaxRuleModel model)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spInsertTaxRuleArchive expense = new spInsertTaxRuleArchive();
        //        expense.Connection = mConnection;
        //        expense.Amount = model.Amount;
        //        expense.Document_Date = model.Document_Date;
        //        expense.AmountFrom = model.AmountFrom;
        //        expense.AmountTo = model.AmountTo;
        //        expense.User_ID = model.User_ID;
        //        expense.CompanyID = model.CompanyID;
        //        expense.Percentage = model.Percentage;
        //        expense.TaxRuleID = model.TaxRuleID;
        //        return expense.ExecuteQuery();
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        throw exp;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}
        //public bool UpdateTaxRule(TaxRuleModel model)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spUpdateTaxRule expense = new spUpdateTaxRule();
        //        expense.Connection = mConnection;
        //        expense.Amount = model.Amount;
        //        expense.AmountFrom = model.AmountFrom;
        //        expense.AmountTo = model.AmountTo;
        //        expense.User_ID = model.User_ID;
        //        expense.CompanyID = model.CompanyID;
        //        expense.Percentage = model.Percentage;
        //        expense.IS_ACTIVE = model.IS_ACTIVE;
        //        expense.IS_DELETED = model.IS_DELETED;
        //        expense.TaxRuleID = model.TaxRuleID;
        //        return expense.ExecuteQuery();
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        throw exp;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        public long InsertSalary(DateTime p_SALARY_MONTH, int p_LOCATION_ID, int p_DEPARTMENT_ID, int p_TYPE_ID, int p_BONUS_TYPE_ID, int p_EmployeeID, decimal p_BASIC_SALARY, decimal p_ALLOWNCES, decimal p_DEDUCTIONS, decimal p_FINE, int p_USER_ID
            , int p_WORK_DATES, int p_OUTSIDE_HOURS, int p_VISITS, string p_REMARKS, bool p_IsProcess)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSALARY_DETAIL salary = new spInsertSALARY_DETAIL();
                salary.Connection = mConnection;
                salary.SALARY_MONTH = p_SALARY_MONTH;
                salary.LOCATION_ID = p_LOCATION_ID;
                salary.DEPARTMENT_ID = p_DEPARTMENT_ID;
                salary.TYPE_ID = p_TYPE_ID;
                salary.BONUS_TYPE_ID = p_BONUS_TYPE_ID;
                salary.EmployeeID = p_EmployeeID;
                salary.BASIC_SALARY = p_BASIC_SALARY;
                salary.ALLOWNCES = p_ALLOWNCES;
                salary.DEDUCTIONS = p_DEDUCTIONS;
                salary.FINE = p_FINE;
                salary.USER_ID = p_USER_ID;
                salary.WORK_DATES = p_WORK_DATES;
                salary.OUTSIDE_HOURS = p_OUTSIDE_HOURS;
                salary.VISITS = p_VISITS;
                salary.REMARKS = p_REMARKS;
                salary.IsProcess = p_IsProcess;
                salary.ExecuteQuery();
                return salary.SALARY_DETAIL_ID;
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

        public bool UpdateSalary(long p_SALARY_DETAIL_ID, int p_LOCATION_ID, int p_DEPARTMENT_ID, int p_TYPE_ID, int p_BONUS_TYPE_ID, int p_EmployeeID, decimal p_BASIC_SALARY, decimal p_ALLOWNCES, decimal p_DEDUCTIONS, decimal p_FINE, int p_STATUS_ID, int p_USER_ID, bool p_IS_DELETED
            , int p_WORK_DATES, int p_OUTSIDE_HOURS, int p_VISITS, string p_REMARKS, bool p_IsProcess)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSALARY_DETAIL salary = new spUpdateSALARY_DETAIL();
                salary.Connection = mConnection;
                salary.SALARY_DETAIL_ID = p_SALARY_DETAIL_ID;
                salary.LOCATION_ID = p_LOCATION_ID;
                salary.DEPARTMENT_ID = p_DEPARTMENT_ID;
                salary.TYPE_ID = p_TYPE_ID;
                salary.BONUS_TYPE_ID = p_BONUS_TYPE_ID;
                salary.EmployeeID = p_EmployeeID;
                salary.BASIC_SALARY = p_BASIC_SALARY;
                salary.ALLOWNCES = p_ALLOWNCES;
                salary.DEDUCTIONS = p_DEDUCTIONS;
                salary.FINE = p_FINE;
                salary.STATUS_ID = p_STATUS_ID;
                salary.USER_ID = p_USER_ID;
                salary.WORK_DATES = p_WORK_DATES;
                salary.OUTSIDE_HOURS = p_OUTSIDE_HOURS;
                salary.VISITS = p_VISITS;
                salary.IS_DELETED = p_IS_DELETED;
                salary.REMARKS = p_REMARKS;
                salary.IsProcess = p_IsProcess;
                return salary.ExecuteQuery();
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

        public bool InsertDeduction(long p_SalaryDetailID, DateTime p_DeductionMonth, int p_EmployeeID, int p_DeductionID, int p_DeductionType, decimal p_DeductionAmount)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDeductionsTransaction salary = new spInsertDeductionsTransaction();
                salary.Connection = mConnection;
                salary.SalaryDetailID = p_SalaryDetailID;
                salary.DeductionMonth = p_DeductionMonth;
                salary.EmployeeID = p_EmployeeID;
                salary.DeductionID = p_DeductionID;
                salary.DeductionType = p_DeductionType;
                salary.DeductionAmount = p_DeductionAmount;
                
                return salary.ExecuteQuery();
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

        public bool InsertAllowance(long p_SalaryDetailID, DateTime p_AllowanceMonth, int p_EmployeeID, int p_AllowanceID, int p_AllowanceType, decimal p_AllowanceAmount)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertAllowanceTransaction salary = new spInsertAllowanceTransaction();
                salary.Connection = mConnection;
                salary.SalaryDetailID = p_SalaryDetailID;
                salary.AllowanceMonth = p_AllowanceMonth;
                salary.EmployeeID = p_EmployeeID;
                salary.AllowanceID = p_AllowanceID;
                salary.AllowanceType = p_AllowanceType;
                salary.AllowanceAmount = p_AllowanceAmount;

                return salary.ExecuteQuery();
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
        public DataSet GePayRollForPayPeriodDetail(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DEPARTMENTID, DateTime p_START_DATE, DateTime p_END_DATE, int p_EmployeeContractTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSELECT_PayRollForPayPeriodDetail objEmployee = new spSELECT_PayRollForPayPeriodDetail();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
                DataSet ds = objEmployee.ExecuteDataSet();
                return ds;
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
        public DataSet GetPaySlipStaff(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DEPARTMENTID, DateTime p_START_DATE, DateTime p_END_DATE, int p_EmployeeContractTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.PayrollReports.dsPayroll dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetPaySlipStaff objEmployee = new spGetPaySlipStaff();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
                DataSet ds = objEmployee.ExecuteDataSet();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dsPayroll.Tables["spGetPaySlipStaff"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    dsPayroll.Tables["spGetPaySlipStaff1"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    dsPayroll.Tables["spGetPaySlipStaff2"].ImportRow(dr);
                }

                return dsPayroll;
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
    
        public DataSet GetDisburesmentStaff(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DEPARTMENTID, DateTime p_START_DATE, DateTime p_END_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.PayrollReports.dsPayroll dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetDisburesmentStaff objEmployee = new spGetDisburesmentStaff();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                DataSet ds = objEmployee.ExecuteTable();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dsPayroll.Tables["spGetDisburesmentStaff"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    dsPayroll.Tables["spGetDisburesmentStaff1"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    dsPayroll.Tables["spGetDisburesmentStaff2"].ImportRow(dr);
                }

                return dsPayroll;

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

        public DataSet GetFundTransfer(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DEPARTMENTID, DateTime p_START_DATE, DateTime p_END_DATE)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.PayrollReports.dsPayroll dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetFundTransferStaff objEmployee = new spGetFundTransferStaff();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                DataSet ds = objEmployee.ExecuteTable();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dsPayroll.Tables["spGetDisburesmentStaff"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    dsPayroll.Tables["spGetDisburesmentStaff1"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    dsPayroll.Tables["spGetDisburesmentStaff2"].ImportRow(dr);
                }

                return dsPayroll;

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
      
        public DataSet GetPaymentReceipt(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DESTRIBUTORID, DateTime p_START_DATE, DateTime p_END_DATE)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                Reports.PayrollReports.dsPayroll dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetPaymentReceipt objEmployee = new spGetPaymentReceipt();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DISTRIBUTORID  = p_DESTRIBUTORID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                DataSet ds = objEmployee.ExecuteTable();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dsPayroll.Tables["spGetPaymentReceipt"].ImportRow(dr);
                }

                return dsPayroll;

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

        public DataTable  GetHRINNDW(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_ConstracctType, DateTime p_START_DATE, DateTime p_END_DATE)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetHRINNDWReport objEmployee = new spGetHRINNDWReport();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.CONTRACTTYPEID  = p_ConstracctType;
                
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                DataTable  Dt = objEmployee.ExecuteTable();
                return Dt;

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
    


        public DataSet GetDisburesmentLabour(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DEPARTMENTID, DateTime p_START_DATE, DateTime p_END_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.PayrollReports.dsPayroll dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetDisburesmentLabour objEmployee = new spGetDisburesmentLabour();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                
                DataSet ds = objEmployee.ExecuteTable();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dsPayroll.Tables["spGetDisburesmentLabour"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    dsPayroll.Tables["spGetDisburesmentLabour1"].ImportRow(dr);
                }

                return dsPayroll;

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
        public DataTable GetPaySlipLabour(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DEPARTMENTID, DateTime p_START_DATE, DateTime p_END_DATE, int p_EmployeeContractTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetPaySlipLabour objEmployee = new spGetPaySlipLabour();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
                return objEmployee.ExecuteTable();
                
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
        public DataSet GetDSRSheet(int p_TIER_ID, DateTime p_START_DATE, DateTime p_END_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.PayrollReports.dsPayroll dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetDSRReimbursement objEmployee = new spGetDSRReimbursement();
                objEmployee.Connection = mConnection;
                objEmployee.TIER_ID = p_TIER_ID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                DataSet ds = objEmployee.ExecuteTable();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dsPayroll.Tables["spGetDSRReimbursement"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    dsPayroll.Tables["spGetDSRReimbursement1"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    dsPayroll.Tables["spGetDSRReimbursement2"].ImportRow(dr);
                }

                return dsPayroll;

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
        public DataSet GetSalesStaffSalary(int p_COMMPANY_ID, long p_EmployeeID, int p_ZoneID, int p_RegionID, DateTime p_START_DATE, DateTime p_END_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.PayrollReports.dsPayroll dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetSalesStaffSalary objEmployee = new spGetSalesStaffSalary();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.RegionID = p_RegionID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.ZoneID = p_ZoneID;
                DataSet ds = objEmployee.ExecuteTable();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dsPayroll.Tables["spGetSalesStaffSalary"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    dsPayroll.Tables["spGetSalesStaffSalary1"].ImportRow(dr);
                }

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    dsPayroll.Tables["spGetSalesStaffSalary2"].ImportRow(dr);
                }

                return dsPayroll;

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
    }
}
