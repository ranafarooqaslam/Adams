using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class AttendanceModel
    {
        #region Private Members
        private int m_EmployeeID;
        private int m_TimeSheetID;
        private int m_AttendanceType;
        private int m_User_ID;
        private DateTime m_DayofMonth;
        private string m_TimeOfDay;
        private DateTime m_Document_Date;
        private string m_Note;
        private bool m_IsLate;
        private bool m_IS_ACTIVE;
        private bool m_IS_DELETED;
        private long m_AttendanceID;
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
        public int TimeSheetID
        {
            set
            {
                m_TimeSheetID = value;
            }
            get
            {
                return m_TimeSheetID;
            }
        }
        public int AttendanceType
        {
            set { m_AttendanceType = value; }
            get { return m_AttendanceType; }
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
        public DateTime DayofMonth
        {
            set
            {
                m_DayofMonth = value;
            }
            get
            {
                return m_DayofMonth;
            }
        }
        public string TimeOfDay
        {
            set
            {
                m_TimeOfDay = value;
            }
            get
            {
                return m_TimeOfDay;
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
        public string Note
        {
            set
            {
                m_Note = value;
            }
            get
            {
                return m_Note;
            }
        }
        public bool IsLate
        {
            set { m_IsLate = value; }
            get { return m_IsLate; }
        }
        public bool IS_ACTIVE
        {
            set { m_IS_ACTIVE = value; }
            get { return m_IS_ACTIVE; }
        }
        public bool IS_DELETED
        {
            set { m_IS_DELETED = value; }
            get { return m_IS_DELETED; }
        }
        public long AttendanceID
        {
            set { m_AttendanceID = value; }
            get { return m_AttendanceID; }
        }
        #endregion
    }
}