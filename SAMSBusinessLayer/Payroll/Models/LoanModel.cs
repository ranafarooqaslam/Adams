using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class LoanModel
    {  
        #region Private Members
        private int m_LoanTypeID;
        private int m_CompanyID;
        private int m_MaxAllow;
        private int m_User_ID;
        private bool m_IS_DELETED;
        private bool m_IS_ACTIVE;
        private string m_LoanTypeName;
        #endregion
        #region Public Properties
        public int LoanTypeID
        {
            set { m_LoanTypeID = value; }
            get { return m_LoanTypeID; }
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
        public int MaxAllow
        {
            set
            {
                m_MaxAllow = value;
            }
            get
            {
                return m_MaxAllow;
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
        public string LoanTypeName
        {
            set
            {
                m_LoanTypeName = value;
            }
            get
            {
                return m_LoanTypeName;
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