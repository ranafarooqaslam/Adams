using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class ResignationModel
	{
        	#region Private Members
	
		private int m_Employee_ID;
		private int m_COMPANY_ID;
		private int m_DISTRIBUTOR_ID;
		private int m_USER_ID;
		private int m_Resignation_ID;
		private DateTime m_RESIGNATION_Date;
		private DateTime m_TIME_STAMP;
		private DateTime m_LASTUPDATE_DATE;
		private bool m_IS_DELETED;
		private string m_RESIGNATION_REASON;
		#endregion
		#region Public Properties
		public int Employee_ID
		{
			set
			{
				m_Employee_ID = value ;
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
				m_COMPANY_ID = value ;
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
				m_DISTRIBUTOR_ID = value ;
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
				m_USER_ID = value ;
			}
			get
			{
				return m_USER_ID;
			}
		}
		public int Resignation_ID
		{
			get
			{
				return m_Resignation_ID;
			}
            set
            {
                m_Resignation_ID = value;
            }

		}
		public DateTime RESIGNATION_Date
		{
			set
			{
				m_RESIGNATION_Date = value ;
			}
			get
			{
				return m_RESIGNATION_Date;
			}
		}
		public DateTime TIME_STAMP
		{
			set
			{
				m_TIME_STAMP = value ;
			}
			get
			{
				return m_TIME_STAMP;
			}
		}
		public DateTime LASTUPDATE_DATE
		{
			set
			{
				m_LASTUPDATE_DATE = value ;
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
				m_IS_DELETED = value ;
			}
			get
			{
				return m_IS_DELETED;
			}
		}
		public  string RESIGNATION_REASON
		{
			set
			{
				m_RESIGNATION_REASON = value ;
			}
			get
			{
				return m_RESIGNATION_REASON;
			}
		}


		#endregion
		
	}
}
