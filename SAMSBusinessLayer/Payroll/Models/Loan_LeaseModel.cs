using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class Loan_LeaseModel
    { 
        #region Private Members
        private int m_EmployeeID;
        private int m_CompanyID;
        private int m_NoOfMonth;
        private int m_ApprovelID;
        private int m_Loan_Lease_TypeID;
        private int m_AssetID;
        private int m_TypeID;
        private int m_User_ID;
        private int m_Loan_LeaseID;
        private DateTime m_ApprovedDate;
        private DateTime m_DateFrom;
        private DateTime m_DateTo;
        private DateTime m_Document_Date;
        private decimal m_Amount;
        private decimal m_EmployeeContributation;
        private decimal m_CompanyContributation;
        private bool m_IS_DELETED;
        private bool m_IS_ACTIVE;
        private string m_Comments;
        #endregion
        #region Public Properties
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
        public int CompanyID
        {
            set
            {
                m_CompanyID = value;
            }
            get
            {
                return m_CompanyID;
            }
        }
        public int NoOfMonth
        {
            set
            {
                m_NoOfMonth = value;
            }
            get
            {
                return m_NoOfMonth;
            }
        }
        public int ApprovelID
        {
            set
            {
                m_ApprovelID = value;
            }
            get
            {
                return m_ApprovelID;
            }
        }
        public int Loan_Lease_TypeID
        {
            set
            {
                m_Loan_Lease_TypeID = value;
            }
            get
            {
                return m_Loan_Lease_TypeID;
            }
        }
        public int AssetID
        {
            set
            {
                m_AssetID = value;
            }
            get
            {
                return m_AssetID;
            }
        }
        public int TypeID
        {
            set
            {
                m_TypeID = value;
            }
            get
            {
                return m_TypeID;
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
        public int Loan_LeaseID
        {
            get
            {
                return m_Loan_LeaseID;
            }
            set
            {
                m_Loan_LeaseID = value;
            }
        }
        public DateTime ApprovedDate
        {
            set
            {
                m_ApprovedDate = value;
            }
            get
            {
                return m_ApprovedDate;
            }
        }
        public DateTime DateFrom
        {
            set
            {
                m_DateFrom = value;
            }
            get
            {
                return m_DateFrom;
            }
        }
        public DateTime DateTo
        {
            set
            {
                m_DateTo = value;
            }
            get
            {
                return m_DateTo;
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
        public decimal EmployeeContributation
        {
            set
            {
                m_EmployeeContributation = value;
            }
            get
            {
                return m_EmployeeContributation;
            }
        }
        public decimal CompanyContributation
        {
            set
            {
                m_CompanyContributation = value;
            }
            get
            {
                return m_CompanyContributation;
            }
        }
        public string Comments
        {
            set
            {
                m_Comments = value;
            }
            get
            {
                return m_Comments;
            }
        }
        public bool IS_DELETED
        {
            set { m_IS_DELETED = value; }
            get { return m_IS_DELETED; }
        }
        public bool IS_ACTIVE
        {
            set { m_IS_ACTIVE = value; }
            get { return m_IS_ACTIVE; }
        }
        #endregion
    }
}