using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class PayRollExpenseModel
    {
        #region Private Members
        private int m_ExpenseHeadID;
        private int m_EmployeeID;
        private int m_User_ID;
        private int m_ExpenseID;
        private DateTime m_Month;
        private DateTime m_Document_Date;
        private decimal m_Amount;
        private string m_Remarks;
        private bool m_IS_ACTIVE;
        private bool m_IS_DELETED;
        #endregion
        #region Public Properties
        public int ExpenseHeadID
        {
            set
            {
                m_ExpenseHeadID = value;
            }
            get
            {
                return m_ExpenseHeadID;
            }
        }
        public int EmployeeID
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
        public int User_ID
        {
            set
            {
                m_User_ID = value;
            }
            get
            {
                return m_User_ID;
            }
        }
        public int ExpenseID
        {
            set
            {
                m_ExpenseID = value;
            }
            get
            {
                return m_ExpenseID;
            }
        }
        public DateTime Month
        {
            set
            {
                m_Month = value;
            }
            get
            {
                return m_Month;
            }
        }
        public DateTime Document_Date
        {
            set
            {
                m_Document_Date = value;
            }
            get
            {
                return m_Document_Date;
            }
        }
        public decimal Amount
        {
            set
            {
                m_Amount = value;
            }
            get
            {
                return m_Amount;
            }
        }
        public string Remarks
        {
            set
            {
                m_Remarks = value;
            }
            get
            {
                return m_Remarks;
            }
        }
        public bool  IS_ACTIVE  
        {
            set
            {
                m_IS_ACTIVE = value;
            }
            get
            {
                return m_IS_ACTIVE;
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
        #endregion
    }
}