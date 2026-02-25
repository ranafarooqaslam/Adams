using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class ExpenseHeadModel
    {
        #region Private Members
        private int m_ExpenseHeadID;
        private int m_CompanyID;
        private int m_User_ID;
        private bool m_IS_DELETED;
        private bool m_IS_ACTIVE;
        private string m_ExpenseHead_Name;
        private string m_Remarks;
        #endregion
        #region Public Properties
        public int ExpenseHeadID
        {
            set { m_ExpenseHeadID = value; }
            get { return m_ExpenseHeadID; }
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
        public string ExpenseHead_Name
        {
            set
            {
                m_ExpenseHead_Name = value;
            }
            get
            {
                return m_ExpenseHead_Name;
            }
        }
        public string Remarks
        {
            set { m_Remarks = value; }
            get { return m_Remarks; }
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