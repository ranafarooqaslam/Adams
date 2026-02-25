using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
  public   class EmployeeTranscationalDetailModel
    {
        #region Private Members
        private int m_CompanyID;
        private int m_DepartmentID;
        private int m_EmployeeLocationID;
        private int m_EmployeeUnitID;
        private int m_EmployeeWorkingScheduleID;
        private int m_SalaryStructureID;
        private int m_TemplateID;
        private int m_ShiftID;
        private int m_ExceptionID;
        private int m_DesignationID;
        private int m_SalaryBehaviorID;
        private long m_EmployeeID;
        private long m_EmployeeTranscationalDetailID;
        private int m_EmployeeContractTypeID;
        #endregion 
        #region Public Properties
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
        public int DepartmentID
        {
            set
            {
                m_DepartmentID = value;
            }
            get
            {
                return m_DepartmentID;
            }
        }
        public int EmployeeLocationID
        {
            set
            {
                m_EmployeeLocationID = value;
            }
            get
            {
                return m_EmployeeLocationID;
            }
        }
        public int EmployeeUnitID
        {
            set
            {
                m_EmployeeUnitID = value;
            }
            get
            {
                return m_EmployeeUnitID;
            }
        }
        public int EmployeeWorkingScheduleID
        {
            set
            {
                m_EmployeeWorkingScheduleID = value;
            }
            get
            {
                return m_EmployeeWorkingScheduleID;
            }
        }
        public int SalaryStructureID
        {
            set
            {
                m_SalaryStructureID = value;
            }
            get
            {
                return m_SalaryStructureID;
            }
        }
        public int TemplateID
        {
            set
            {
                m_TemplateID = value;
            }
            get
            {
                return m_TemplateID;
            }
        }
        public int ShiftID
        {
            set
            {
                m_ShiftID = value;
            }
            get
            {
                return m_ShiftID;
            }
        }
        public int ExceptionID
        {
            set
            {
                m_ExceptionID = value;
            }
            get
            {
                return m_ExceptionID;
            }
        }
        public int DesignationID
        {
            set
            {
                m_DesignationID = value;
            }
            get
            {
                return m_DesignationID;
            }
        }
        public int SalaryBehaviorID
        {
            set
            {
                m_SalaryBehaviorID = value;
            }
            get
            {
                return m_SalaryBehaviorID;
            }
        }
        public long EmployeeID
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
        public long EmployeeTranscationalDetailID
        {
            set
            {
                m_EmployeeTranscationalDetailID = value;
            }
            get
            {
                return m_EmployeeTranscationalDetailID;
            }
        }
        public int EmployeeContractTypeID
        {
            set
            {
                m_EmployeeContractTypeID = value;
            }
            get
            {
                return m_EmployeeContractTypeID;
            }
        }
        #endregion
    }
}
