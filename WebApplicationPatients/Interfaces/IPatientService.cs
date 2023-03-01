using Microsoft.AspNetCore.Mvc;
using WebApplicationPatient.Models;

namespace WebApplicationPatient.Interfaces
{
    public interface IPatientService
    {
        Task<IActionResult> GetAllPatients();
        Task<IActionResult> GetPatientById(int id);
        Task<IActionResult> AddPatient(Patient patient);
        Task<IActionResult> UpdatePatient(int id, Patient patient);
        Task<IActionResult> DeletePatient(int id);
    }
}
