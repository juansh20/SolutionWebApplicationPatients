
using WebApplicationPatient.Models;
using WebApplicationPatients.Utils;

namespace WebApplicationPatient.Interfaces
{
    public interface IPatientRepository
    {
        Task<PagedResult<Patient>> GetAllPatients(int pageNumber, int pageSize);
        Task<Patient> GetPatientById(int id);
        Task<int> AddPatient(Patient patient);
        Task<int> UpdatePatient(int id, Patient patient);
        Task<int> DeletePatient(int id);
        bool SeedPatients(int count);
    }
}
