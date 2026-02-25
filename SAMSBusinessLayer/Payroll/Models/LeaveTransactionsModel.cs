using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class LeaveTransactionsModel
    {
        #region Private Members
		private int m_LeaveID;
		private int m_TimeSheetID;
		private int m_EmployeeID;
		private int m_User_ID;
		private int m_LeaveTransactionsID;
		private DateTime m_LeaveFrom;
		private DateTime m_LeaveTo;
		private DateTime m_Document_Date;
		private decimal m_NumberofDays;
		private string m_Note;
        private bool m_IS_ACTIVE;
        private bool m_IS_DELETED;
		#endregion
		#region Public Properties
		public int LeaveID
		{
			set
			{
				m_LeaveID = value ;
			}
			get
			{
				return m_LeaveID;
			}
		}
		public int TimeSheetID
		{
			set
			{
				m_TimeSheetID = value ;
			}
			get
			{
				return m_TimeSheetID;
			}
		}
		public int EmployeeID
		{
			set
			{
				m_EmployeeID = value ;
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
				m_User_ID = value ;
			}
			get
			{
				return m_User_ID;
			}
		}
		public int LeaveTransactionsID
		{
            set 
            {
                m_LeaveTransactionsID = value;
            }
			get
			{
				return m_LeaveTransactionsID;
			}
		}
		public DateTime LeaveFrom
		{
			set
			{
				m_LeaveFrom = value ;
			}
			get
			{
				return m_LeaveFrom;
			}
		}
		public DateTime LeaveTo
		{
			set
			{
				m_LeaveTo = value ;
			}
			get
			{
				return m_LeaveTo;
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
		public decimal NumberofDays
		{
			set
			{
				m_NumberofDays = value ;
			}
			get
			{
				return m_NumberofDays;
			}
		}
		public  string Note
		{
			set
			{
				m_Note = value ;
			}
			get
			{
				return m_Note;
			}
		}
        public bool IS_ACTIVE
        {
            set { m_IS_ACTIVE = value ; }
            get { return m_IS_ACTIVE; }

        }
        public bool IS_DELETED
        {
            set { m_IS_DELETED = value; }
            get { return m_IS_DELETED; }
        }
		#endregion
    }
}