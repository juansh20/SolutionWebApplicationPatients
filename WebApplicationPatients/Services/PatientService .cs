using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using WebApplicationPatient.Interfaces;
using WebApplicationPatient.Models;
using Microsoft.AspNetCore.Mvc.Core;

namespace WebApplicationPatient.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly PatientValidator _patientValidator;
        private readonly IMemoryCache _cache;

        public PatientService(IPatientRepository patientRepository, PatientValidator patientValidator, IMemoryCache memoryCache)
        {
            _patientRepository = patientRepository;
            _patientValidator = patientValidator;
            _cache = memoryCache;
        }

        //public async Task<IActionResult> GetAllPatients()
        //{
        //    var patients = await _patientRepository.GetAllPatients();
        //    return new OkObjectResult(patients);
        //}

        public async Task<IActionResult> GetAllPatients(int pageNumber, int pageSize)
        {
            string cacheKey = $"all-patients-{pageNumber}-{pageSize}";

            // Check if the result is in cache
            if (_cache.TryGetValue(cacheKey, out IActionResult result))
            {
                return result;
            }

            var patients = await _patientRepository.GetAllPatients(pageNumber, pageSize);
            result = new OkObjectResult(patients);

            // Cache the result for 60 minutes
            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(60));

            return result;
        }

        public async Task<IActionResult> GetPatientById(int id)
        {
            Patient patient;
            if (!_cache.TryGetValue($"Patient-{id}", out patient))
            {
                patient = await _patientRepository.GetPatientById(id);

                if (patient == null)
                {
                    return new NotFoundResult();
                }

                _cache.Set($"Patient-{id}", patient, TimeSpan.FromMinutes(60)); // almacenar en caché por 60 minutos
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
            _cache.Remove("Patients");

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
            _cache.Remove("Patients");
            _cache.Remove($"Patient-{id}");
            return new NoContentResult();
        }

        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientRepository.DeletePatient(id);

            if (result == 0)
            {
                return new NotFoundResult();
            }
            _cache.Remove("Patients");
            _cache.Remove($"Patient-{id}");
            return new NoContentResult();
        }

        public IActionResult SeedPatients(int count)
        {
            var result = _patientRepository.SeedPatients(count);

            if(!result)
                return new NotFoundResult();

            _cache.Remove("Patients");
            return new NoContentResult();
        }

    }
}
