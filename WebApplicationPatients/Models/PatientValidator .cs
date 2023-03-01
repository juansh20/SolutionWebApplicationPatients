using FluentValidation;

namespace WebApplicationPatient.Models
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(patient => patient.sName).NotEmpty();
            RuleFor(patient => patient.sLastName).NotEmpty();
            RuleFor(patient => patient.dtDateOfBirth).NotEmpty().LessThan(DateTime.Now);
            RuleFor(patient => patient.sAddress).NotEmpty();
            RuleFor(patient => patient.sPhoneNumber).NotEmpty();
            RuleFor(patient => patient.sEmail).NotEmpty().EmailAddress();
            RuleFor(patient => patient.sGender).NotEmpty();
            RuleFor(patient => patient.sEmergencyContactName).NotEmpty();
            RuleFor(patient => patient.sEmergencyContactPhoneNumber).NotEmpty();
            RuleFor(patient => patient.sPrimaryCarePhysician).NotEmpty();
            RuleFor(patient => patient.sDiagnosis).NotEmpty();
            RuleFor(patient => patient.dtAdmissionDate).NotEmpty().LessThan(DateTime.Now);
        }
    }
}
