using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class AssetsModel
    {
        #region Private Members
        private int m_AssetTypeID;
        private int m_CompanyID;
        private int m_User_ID;
        private int m_AssetID;
        private DateTime m_Document_Date;
        private decimal m_Amount;
        private string m_RegNo;
        private string m_Make;
        private string m_Model;
        private string m_Color;
        private string m_EngineNo;
        private string m_ChassisNo;
        private string m_Remarks;
        private bool m_IS_ACTIVE;
        private bool m_IS_DELETED;
        #endregion
        #region Public Properties
        public int AssetTypeID
        {
            set
            {
                m_AssetTypeID = value;
            }
            get
            {
                return m_AssetTypeID;
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
        public int AssetID
        {
            set { m_AssetID = value; }
            get
            {
                return m_AssetID;
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
        public string RegNo
        {
            set { m_RegNo = value; }
            get { return m_RegNo; }
        }
        public string Make
        {
            set
            {
                m_Make = value;
            }
            get
            {
                return m_Make;
            }
        }
        public string Model
        {
            set
            {
                m_Model = value;
            }
            get
            {
                return m_Model;
            }
        }
        public string Color
        {
            set
            {
                m_Color = value;
            }
            get
            {
                return m_Color;
            }
        }
        public string EngineNo
        {
            set
            {
                m_EngineNo = value;
            }
            get
            {
                return m_EngineNo;
            }
        }
        public string ChassisNo
        {
            set
            {
                m_ChassisNo = value;
            }
            get
            {
                return m_ChassisNo;
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
        public bool IS_ACTIVE
        {
            set { m_IS_ACTIVE = value; }
            get { return m_IS_ACTIVE; }
        }
        public bool IS_DELETED
        {
            set { m_IS_DELETED = value;}
            get { return m_IS_DELETED; }
        }
        #endregion
    }
}