using System;
using System.Collections.Generic;
using System.Text;
using SAMSCommon.Classes;

namespace SAMSBusinessLayer.Models
{
    public class EmployeeModel
    {
        #region Private Members
        private int m_EmployeeGender;
        private int m_CompanyID;
        private int m_User_ID;
        private int m_DISTRIBUTOR_ID;
        private long m_Employee_ID;
        private DateTime m_EmployeeDrivingLicenseValidUpto;
        private DateTime m_EmployeeDateOfBirth;
        private DateTime m_EmployeeProbationFrom;
        private DateTime m_EmployeeProbationTo;
        private DateTime m_EmployeeDurationFrom;
        private DateTime m_EmployeeDurationTo;
        private DateTime m_Document_Date;
        private bool m_EmployeeMaritalStatus;
        private bool m_IsMonitored;
        private bool m_IS_APPROVED;
        private bool m_EmployeeIsAManager;
        private bool m_IsOvertimeEnabled;
        private bool m_IsPFEnabled;
        private bool m_IS_ACTIVE;
        private string m_EmployeeFullName;
        private string m_EmployeeWorkingAddress;
        private string m_EmployeeWorkEmail;
        private string m_EmployeeWorkPhone;
        private string m_EmployeeWorkMobile;
        private string m_EmployeeCNIC;
        private string m_TimeZone;
        private string m_EmployeePicture;
        private string m_BloodGroup;
        private string m_EmployeeEmergencyContactName;
        private string m_EmployeeOtherInformation;
        private string m_EmployeeTag;
        private string m_EmployeeMachineTag1;
        private string m_EmployeeMachineTag2;
        private string m_EmployeeMachineTag3;
        private string m_EmployeeBankAccountTitle;
        private string m_EmployeeBankName;
        private string m_EmployeeSocialSecurityNumber;
        private string m_EmployeeEOBInumber;
        private string m_EmployeeHomeAddress;
        private string m_EmployeeEmergencyContactNumber;
        private string m_EmployeeNTN;
        private string m_EmployeeDrivingLicenseNumber;
        private string m_EmployeePassportNumber;
        private string m_EmployeeVisaInformation;
        private string m_EmployeeNationality;
        private string m_EmployeeBankAccountNumber;
        #endregion
        #region Public Properties
        public int EmployeeGender
        {
            set
            {
                m_EmployeeGender = value;
            }
            get
            {
                return m_EmployeeGender;
            }
        }
        public bool EmployeeMaritalStatus
        {
            set
            {
                m_EmployeeMaritalStatus = value;
            }
            get
            {
                return m_EmployeeMaritalStatus;
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
        public int DISTRIBUTOR_ID
        {
            set { m_DISTRIBUTOR_ID = value; }
            get { return m_DISTRIBUTOR_ID; }
        }
        public long EmployeeID  
        {
            set
            {
                m_Employee_ID = value;
            }
            get
            {
                return m_Employee_ID;
            }
        }
        public DateTime EmployeeDrivingLicenseValidUpto
        {
            set
            {
                m_EmployeeDrivingLicenseValidUpto = value;
            }
            get
            {
                return m_EmployeeDrivingLicenseValidUpto;
            }
        }
        public DateTime EmployeeDateOfBirth
        {
            set
            {
                m_EmployeeDateOfBirth = value;
            }
            get
            {
                return m_EmployeeDateOfBirth;
            }
        }
        public DateTime EmployeeProbationFrom
        {
            set
            {
                m_EmployeeProbationFrom = value;
            }
            get
            {
                return m_EmployeeProbationFrom;
            }
        }
        public DateTime EmployeeProbationTo
        {
            set
            {
                m_EmployeeProbationTo = value;
            }
            get
            {
                return m_EmployeeProbationTo;
            }
        }
        public DateTime EmployeeDurationFrom
        {
            set
            {
                m_EmployeeDurationFrom = value;
            }
            get
            {
                return m_EmployeeDurationFrom;
            }
        }
        public DateTime EmployeeDurationTo
        {
            set
            {
                m_EmployeeDurationTo = value;
            }
            get
            {
                return m_EmployeeDurationTo;
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
        public bool IsMonitored
        {
            set
            {
                m_IsMonitored = value;
            }
            get
            {
                return m_IsMonitored;
            }
        }
        public bool IS_APPROVED
        {
            set
            {
                m_IS_APPROVED = value;
            }
            get
            {
                return m_IS_APPROVED;
            }
        }
        public bool EmployeeIsAManager
        {
            set
            {
                m_EmployeeIsAManager = value;
            }
            get
            {
                return m_EmployeeIsAManager;
            }
        }
        public bool IsOvertimeEnabled
        {
            set
            {
                m_IsOvertimeEnabled = value;
            }
            get
            {
                return m_IsOvertimeEnabled;
            }
        }
        public bool IsPFEnabled
        {
            set { m_IsPFEnabled = value; }
            get { return m_IsPFEnabled; }
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
        public string EmployeeFullName
        {
            set
            {
                m_EmployeeFullName = value;
            }
            get
            {
                return m_EmployeeFullName;
            }
        }
        public string EmployeeWorkingAddress
        {
            set
            {
                m_EmployeeWorkingAddress = value;
            }
            get
            {
                return m_EmployeeWorkingAddress;
            }
        }
        public string EmployeeWorkEmail
        {
            set
            {
                m_EmployeeWorkEmail = value;
            }
            get
            {
                return m_EmployeeWorkEmail;
            }
        }
        public string EmployeeWorkPhone
        {
            set
            {
                m_EmployeeWorkPhone = value;
            }
            get
            {
                return m_EmployeeWorkPhone;
            }
        }
        public string EmployeeWorkMobile
        {
            set
            {
                m_EmployeeWorkMobile = value;
            }
            get
            {
                return m_EmployeeWorkMobile;
            }
        }
        public string EmployeeCNIC
        {
            set
            {
                m_EmployeeCNIC = value;
            }
            get
            {
                return m_EmployeeCNIC;
            }
        }
        public string TimeZone
        {
            set
            {
                m_TimeZone = value;
            }
            get
            {
                return m_TimeZone;
            }
        }
        public string EmployeePicture
        {
            set
            {
                m_EmployeePicture = value;
            }
            get
            {
                return m_EmployeePicture;
            }
        }
        public string BloodGroup
        {
            set
            {
                m_BloodGroup = value;
            }
            get
            {
                return m_BloodGroup;
            }
        }
        public string EmployeeEmergencyContactName
        {
            set
            {
                m_EmployeeEmergencyContactName = value;
            }
            get
            {
                return m_EmployeeEmergencyContactName;
            }
        }
        public string EmployeeOtherInformation
        {
            set
            {
                m_EmployeeOtherInformation = value;
            }
            get
            {
                return m_EmployeeOtherInformation;
            }
        }
        public string EmployeeTag
        {
            set
            {
                m_EmployeeTag = value;
            }
            get
            {
                return m_EmployeeTag;
            }
        }
        public string EmployeeMachineTag1
        {
            set
            {
                m_EmployeeMachineTag1 = value;
            }
            get
            {
                return m_EmployeeMachineTag1;
            }
        }
        public string EmployeeMachineTag2
        {
            set
            {
                m_EmployeeMachineTag2 = value;
            }
            get
            {
                return m_EmployeeMachineTag2;
            }
        }
        public string EmployeeMachineTag3
        {
            set
            {
                m_EmployeeMachineTag3 = value;
            }
            get
            {
                return m_EmployeeMachineTag3;
            }
        }
        public string EmployeeBankAccountTitle
        {
            set
            {
                m_EmployeeBankAccountTitle = value;
            }
            get
            {
                return m_EmployeeBankAccountTitle;
            }
        }
        public string EmployeeBankName
        {
            set
            {
                m_EmployeeBankName = value;
            }
            get
            {
                return m_EmployeeBankName;
            }
        }
        public string EmployeeSocialSecurityNumber
        {
            set
            {
                m_EmployeeSocialSecurityNumber = value;
            }
            get
            {
                return m_EmployeeSocialSecurityNumber;
            }
        }
        public string EmployeeEOBInumber
        {
            set
            {
                m_EmployeeEOBInumber = value;
            }
            get
            {
                return m_EmployeeEOBInumber;
            }
        }
        public string EmployeeHomeAddress
        {
            set
            {
                m_EmployeeHomeAddress = value;
            }
            get
            {
                return m_EmployeeHomeAddress;
            }
        }
        public string EmployeeEmergencyContactNumber
        {
            set
            {
                m_EmployeeEmergencyContactNumber = value;
            }
            get
            {
                return m_EmployeeEmergencyContactNumber;
            }
        }
        public string EmployeeNTN
        {
            set
            {
                m_EmployeeNTN = value;
            }
            get
            {
                return m_EmployeeNTN;
            }
        }
        public string EmployeeDrivingLicenseNumber
        {
            set
            {
                m_EmployeeDrivingLicenseNumber = value;
            }
            get
            {
                return m_EmployeeDrivingLicenseNumber;
            }
        }
        public string EmployeePassportNumber
        {
            set
            {
                m_EmployeePassportNumber = value;
            }
            get
            {
                return m_EmployeePassportNumber;
            }
        }
        public string EmployeeVisaInformation
        {
            set
            {
                m_EmployeeVisaInformation = value;
            }
            get
            {
                return m_EmployeeVisaInformation;
            }
        }
        public string EmployeeNationality
        {
            set
            {
                m_EmployeeNationality = value;
            }
            get
            {
                return m_EmployeeNationality;
            }
        }
        public string EmployeeBankAccountNumber
        {
            set
            {
                m_EmployeeBankAccountNumber = value;
            }
            get
            {
                return m_EmployeeBankAccountNumber;
            }
        }
        #endregion
    }
}