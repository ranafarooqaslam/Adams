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
    public class TargetContoller
    {
        /// <summary>
        /// Constructor for TargetContoller Class
        /// </summary>
        public TargetContoller()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool InsertTarget(DateTime p_TARGET_MONTH, DateTime p_DOCUMENT_DATE, int p_EMPLOYEE_ID, int p_TARGET, int p_ACHIEVEMENT, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertTARGET_ENTRY mTarget = new spInsertTARGET_ENTRY();
                mTarget.Connection = mConnection;
                mTarget.TARGET_MONTH = p_TARGET_MONTH;
                mTarget.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mTarget.EMPLOYEE_ID = p_EMPLOYEE_ID;
                mTarget.TARGET = p_TARGET;
                mTarget.ACHIEVEMENT = p_ACHIEVEMENT;
                mTarget.USER_ID = p_USER_ID;
                mTarget.ExecuteQuery();
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
