using Microsoft.AspNetCore.Mvc;
using WebApplicationPatient.Interfaces;
using WebApplicationPatient.Models;
using Microsoft.AspNetCore.Mvc.Core;

namespace WebApplicationPatient.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly PatientValidator _patientValidator;

        public PatientService(IPatientRepository patientRepository, PatientValidator patientValidator)
        {
            _patientRepository = patientRepository;
            _patientValidator = patientValidator;
        }

        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllPatients();
            return new OkObjectResult(patients);
        }

        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _patientRepository.GetPatientById(id);

            if (patient == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(patient);
        }

        public async Task<IActionResult> AddPatient(Patient patient)
        {
            var validationResult = _patientValidator.Validate(patient);
            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors);
            }

            var id = await _patientRepository.AddPatient(patient);

            return new CreatedAtRouteResult("GetPatientById", new { id }, patient);
        }

        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            var validationResult = _patientValidator.Validate(patient);
            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors);
            }

            var result = await _patientRepository.UpdatePatient(id, patient);

            if (result == 0)
            {
                return new NotFoundResult();
            }

            return new NoContentResult();
        }

        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientRepository.DeletePatient(id);

            if (result == 0)
            {
                return new NotFoundResult();
            }

            return new NoContentResult();
        }

        public IActionResult SeedPatients(int count)
        {
            var result = _patientRepository.SeedPatients(count);

            if(!result)
                return new NotFoundResult();

            return new NoContentResult();
        }

    }
}
