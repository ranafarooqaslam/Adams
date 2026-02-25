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
    public class SalarySlipController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public SalarySlipController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet SelectSalarySlip(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DEPARTMENTID, bool p_IS_ACTIVE, DateTime p_START_DATE, DateTime p_END_DATE, int p_SALAR_BEHAVIOR)
        {
            IDbConnection mConnection = null;
            DataSet ds = new DataSet();
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSELECT_Salary_Slip salslip = new spSELECT_Salary_Slip();
                salslip.Connection = mConnection;
                salslip.COMMPANY_ID = p_COMMPANY_ID;
                salslip.EmployeeID = p_EmployeeID;
                salslip.DEPARTMENTID = p_DEPARTMENTID;
                salslip.IS_ACTIVE = p_IS_ACTIVE;
                salslip.START_DATE = p_START_DATE;
                salslip.END_DATE = p_END_DATE;
                salslip.SALAR_BEHAVIOR = p_SALAR_BEHAVIOR;
                salslip.LOCATIONID = p_LOCATIONID;
                DataTable dtSalarySlip = salslip.ExecuteTable();
                DataSet u = dtSalarySlip.DataSet;
                u.Tables.Remove(dtSalarySlip.TableName);
                dtSalarySlip.TableName = "dtSalarySlip";
                ds.Tables.Add(dtSalarySlip);
                spSELECT_Salary_Slip_IncomeTax salslipIncomeTax = new spSELECT_Salary_Slip_IncomeTax();
                salslipIncomeTax.Connection = mConnection;
                salslipIncomeTax.COMMPANY_ID = p_COMMPANY_ID;
                salslipIncomeTax.EmployeeID = p_EmployeeID;
                salslipIncomeTax.DEPARTMENTID = p_DEPARTMENTID;
                salslipIncomeTax.IS_ACTIVE = p_IS_ACTIVE;
                salslipIncomeTax.START_DATE = p_START_DATE;
                salslipIncomeTax.END_DATE = p_END_DATE;
                salslipIncomeTax.LOCATIONID = p_LOCATIONID;
                salslipIncomeTax.SALAR_BEHAVIOR = p_SALAR_BEHAVIOR;
                DataTable dtSalarySlipIncomeTax = salslipIncomeTax.ExecuteTable();
                u = dtSalarySlipIncomeTax.DataSet;
                u.Tables.Remove(dtSalarySlipIncomeTax.TableName);
                dtSalarySlipIncomeTax.TableName = "dtSalarySlipIncomeTax";
                ds.Tables.Add(dtSalarySlipIncomeTax);
                spSELECT_Salary_Slip_Leaves salslipLeave = new spSELECT_Salary_Slip_Leaves();
                salslipLeave.Connection = mConnection;
                salslipLeave.COMMPANY_ID = p_COMMPANY_ID;
                salslipLeave.EmployeeID = p_EmployeeID;
                salslipLeave.DEPARTMENTID = p_DEPARTMENTID;
                salslipLeave.IS_ACTIVE = p_IS_ACTIVE;
                salslipLeave.START_DATE = p_START_DATE;
                salslipLeave.END_DATE = p_END_DATE;
                salslipLeave.LOCATIONID = p_LOCATIONID;
                DataTable dtsalslipLeave = salslipLeave.ExecuteTable();
                u = dtsalslipLeave.DataSet;
                u.Tables.Remove(dtsalslipLeave.TableName);
                dtsalslipLeave.TableName = "dtsalslipLeave";
                ds.Tables.Add(dtsalslipLeave);
                spSELECT_Salary_Slip_Loan_Installment salslipInstallment = new spSELECT_Salary_Slip_Loan_Installment();
                salslipInstallment.Connection = mConnection;
                salslipInstallment.COMMPANY_ID = p_COMMPANY_ID;
                salslipInstallment.EmployeeID = p_EmployeeID;
                salslipInstallment.DEPARTMENTID = p_DEPARTMENTID;
                salslipInstallment.IS_ACTIVE = p_IS_ACTIVE;
                salslipInstallment.START_DATE = p_START_DATE;
                salslipInstallment.END_DATE = p_END_DATE;
                salslipInstallment.LOCATIONID = p_LOCATIONID;
                DataTable dtsalslipInstallment = salslipInstallment.ExecuteTable();
                u = dtsalslipInstallment.DataSet;
                u.Tables.Remove(dtsalslipInstallment.TableName);
                dtsalslipInstallment.TableName = "dtsalslipInstallment";
                ds.Tables.Add(dtsalslipInstallment);
                spSELECT_Salary_Slip_UnPaid_LEAVE salslipunpaidLEave = new spSELECT_Salary_Slip_UnPaid_LEAVE();
                salslipunpaidLEave.Connection = mConnection;
                salslipunpaidLEave.COMMPANY_ID = p_COMMPANY_ID;
                salslipunpaidLEave.EmployeeID = p_EmployeeID;
                salslipunpaidLEave.DEPARTMENTID = p_DEPARTMENTID;
                salslipunpaidLEave.IS_ACTIVE = p_IS_ACTIVE;
                salslipunpaidLEave.START_DATE = p_START_DATE;
                salslipunpaidLEave.END_DATE = p_END_DATE;
                salslipunpaidLEave.LOCATIONID = p_LOCATIONID;
                DataTable dtsalslipunpaidLEave = salslipunpaidLEave.ExecuteTable();
                u = dtsalslipunpaidLEave.DataSet;
                u.Tables.Remove(dtsalslipunpaidLEave.TableName);
                dtsalslipunpaidLEave.TableName = "dtsalslipunpaidLEave";
                ds.Tables.Add(dtsalslipunpaidLEave);
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

        public DataTable GetSalarySummaryReport(int p_COMMPANY_ID, long p_EmployeeID, int p_LOCATIONID, int p_DEPARTMENTID, DateTime p_START_DATE, DateTime p_END_DATE, int p_EmployeeContractTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.PayrollReports.dsPayroll dsPayroll = new Reports.PayrollReports.dsPayroll();
                spGetSalarySummary objEmployee = new spGetSalarySummary();
                objEmployee.Connection = mConnection;
                objEmployee.COMMPANY_ID = p_COMMPANY_ID;
                objEmployee.DEPARTMENTID = p_DEPARTMENTID;
                objEmployee.EmployeeID = p_EmployeeID;
                objEmployee.END_DATE = p_END_DATE;
                objEmployee.START_DATE = p_START_DATE;
                objEmployee.LOCATIONID = p_LOCATIONID;
                objEmployee.EmployeeContractTypeID = p_EmployeeContractTypeID;
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
    }
}