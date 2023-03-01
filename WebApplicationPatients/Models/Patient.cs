using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplicationPatient.Models
{
    [DataContract]
    public class Patient
    {
        [Key]
        public int iId { get; set; }
        public string sName { get; set; }
        public string sLastName { get; set; }
        public DateTime dtDateOfBirth { get; set; }
        public string sAddress { get; set; }
        public string sPhoneNumber { get; set; }
        public string sEmail { get; set; }
        public string sGender { get; set; }
        public string sEmergencyContactName { get; set; }
        public string sEmergencyContactPhoneNumber { get; set; }
        public string sPrimaryCarePhysician { get; set; }
        public string sDiagnosis { get; set; }
        public DateTime dtAdmissionDate { get; set; }
        public DateTime? dtDischargeDate { get; set; }
        public bool IsAdmitted { get; set; }
        public bool IsDischarged { get; set; }

    }
}
