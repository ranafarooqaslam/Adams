using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
    public class SALARY_INCREMENTModel
	
    {
        #region Private Members
        private string sp_Name = "spInsertSALARY_INCREMENT";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_Employee_ID;
        private int m_COMPANY_ID;
        private int m_DISTRIBUTOR_ID;
        private int m_USER_ID;
        private int m_INCREMENT_ID;
        private decimal m_PREVIOUS_SALARY;
        private decimal m_INCREMENT_AMOUNT;
        private decimal m_NEW_SALARY;
        private DateTime m_INCREMENT_DATE;
        private DateTime m_APPLICABLE_DATE;
        private DateTime m_DOCUMENT_DATE;
        private DateTime m_LASTUPDATE_DATE;
        private bool m_IS_DELETED;
        private string m_REMARKS;
        #endregion
        #region Public Properties
        public int Employee_ID
        {
            set
            {
                m_Employee_ID = value;
            }
            get
            {
                return m_Employee_ID;
            }
        }
        public int COMPANY_ID
        {
            set
            {
                m_COMPANY_ID = value;
            }
            get
            {
                return m_COMPANY_ID;
            }
        }
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
        public int USER_ID
        {
            set
            {
                m_USER_ID = value;
            }
            get
            {
                return m_USER_ID;
            }
        }
        public int INCREMENT_ID
        {
            get
            {
                return m_INCREMENT_ID;
            }
            set
            {
                m_INCREMENT_ID = value;
            }
        }
        public decimal PREVIOUS_SALARY
        {
            set
            {
                m_PREVIOUS_SALARY = value;
            }
            get
            {
                return m_PREVIOUS_SALARY;
            }
        }
        public decimal INCREMENT_AMOUNT
        {
            set
            {
                m_INCREMENT_AMOUNT = value;
            }
            get
            {
                return m_INCREMENT_AMOUNT;
            }
        }
        public decimal NEW_SALARY
        {
            set
            {
                m_NEW_SALARY = value;
            }
            get
            {
                return m_NEW_SALARY;
            }
        }
        public DateTime INCREMENT_DATE
        {
            set
            {
                m_INCREMENT_DATE = value;
            }
            get
            {
                return m_INCREMENT_DATE;
            }
        }
        public DateTime APPLICABLE_DATE
        {
            set
            {
                m_APPLICABLE_DATE = value;
            }
            get
            {
                return m_APPLICABLE_DATE;
            }
        }
        public DateTime DOCUMENT_DATE
        {
            set
            {
                m_DOCUMENT_DATE = value;
            }
            get
            {
                return m_DOCUMENT_DATE;
            }
        }
        public DateTime LASTUPDATE_DATE
        {
            set
            {
                m_LASTUPDATE_DATE = value;
            }
            get
            {
                return m_LASTUPDATE_DATE;
            }
        }
        public bool IS_DELETED
        {
            set
            {
                m_IS_DELETED = value;
            }
            get
            {
                return m_IS_DELETED;
            }
        }
        public string REMARKS
        {
            set
            {
                m_REMARKS = value;
            }
            get
            {
                return m_REMARKS;
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
       
		
	}
}
