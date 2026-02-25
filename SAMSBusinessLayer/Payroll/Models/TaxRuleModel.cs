using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class TaxRuleModel
    {
        #region Private Members
		private int m_CompanyID;
		private int m_User_ID;
		private int m_TaxRuleID;
		private DateTime m_Document_Date;
		private decimal m_AmountFrom;
		private decimal m_AmountTo;
		private decimal m_Percentage;
		private decimal m_Amount;
        private bool m_IS_ACTIVE;
        private bool m_IS_DELETED;
		#endregion
		#region Public Properties
		public int CompanyID
		{
			set
			{
				m_CompanyID = value ;
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
				m_User_ID = value ;
			}
			get
			{
				return m_User_ID;
			}
		}
		public int TaxRuleID
		{
            set
            {
                m_TaxRuleID = value;
            }
			get
			{
				return m_TaxRuleID;
			}
		}
		public DateTime Document_Date
		{
			set
			{
				m_Document_Date = value ;
			}
			get
			{
				return m_Document_Date;
			}
		}
		public decimal AmountFrom
		{
			set
			{
				m_AmountFrom = value ;
			}
			get
			{
				return m_AmountFrom;
			}
		}
		public decimal AmountTo
		{
			set
			{
				m_AmountTo = value ;
			}
			get
			{
				return m_AmountTo;
			}
		}
		public decimal Percentage
		{
			set
			{
				m_Percentage = value ;
			}
			get
			{
				return m_Percentage;
			}
		}
		public decimal Amount
		{
			set
			{
				m_Amount = value ;
			}
			get
			{
				return m_Amount;
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
        public bool IS_ACTIVE
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
		#endregion
    }
}