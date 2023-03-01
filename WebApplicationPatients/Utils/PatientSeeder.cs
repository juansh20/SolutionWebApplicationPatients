using Bogus;
using WebApplicationPatients.Context;
using System;
using System.Linq;
using WebApplicationPatient.Models;

namespace WebApplicationPatients.Utils
{
    public static class PatientSeeder
    {
        public static void Seed(DefaultContext context, int count)
        {
           // if (!context.Patients.Any())
           // {
                var faker = new Faker<Patient>()
                    .RuleFor(p => p.sName, f => f.Name.FirstName())
                    .RuleFor(p => p.sLastName, f => f.Name.LastName())
                    .RuleFor(p => p.dtDateOfBirth, f => f.Date.Past(60, DateTime.Now.AddYears(-18)))
                    .RuleFor(p => p.sAddress, f => f.Address.FullAddress())
                    .RuleFor(p => p.sPhoneNumber, f => f.Phone.PhoneNumber())
                    .RuleFor(p => p.sEmail, f => f.Internet.Email())
                    .RuleFor(p => p.sGender, f => f.PickRandom("Male", "Female"))
                    .RuleFor(p => p.sEmergencyContactName, f => f.Name.FullName())
                    .RuleFor(p => p.sEmergencyContactPhoneNumber, f => f.Phone.PhoneNumber())
                    .RuleFor(p => p.sPrimaryCarePhysician, f => f.Name.FullName())
                    .RuleFor(p => p.sDiagnosis, f => f.Lorem.Sentence())
                    .RuleFor(p => p.dtAdmissionDate, f => f.Date.Past(2))
                    .RuleFor(p => p.dtDischargeDate, (f, p) => p.IsDischarged ? f.Date.Past(1, p.dtAdmissionDate) : null)
                    .RuleFor(p => p.IsAdmitted, f => f.Random.Bool())
                    .RuleFor(p => p.IsDischarged, (f, p) => p.IsAdmitted ? f.Random.Bool() : false);

                var patients = faker.Generate(count);

                foreach (var patient in patients)
                {
                    context.Patients.Add(patient);
                }

                context.SaveChanges();
            //}
        }
    }
}
