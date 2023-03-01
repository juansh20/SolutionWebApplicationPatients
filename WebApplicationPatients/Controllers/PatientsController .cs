﻿using Microsoft.AspNetCore.Mvc;
using WebApplicationPatient.Interfaces;
using WebApplicationPatient.Models;

namespace WebApplicationPatient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            return await _patientService.GetAllPatients();
        }

        [HttpGet("{id}", Name = "GetPatientById")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            return await _patientService.GetPatientById(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] Patient patient)
        {
            return await _patientService.AddPatient(patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            return await _patientService.UpdatePatient(id, patient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            return await _patientService.DeletePatient(id);
        }
    }
}