using WebApplicationPatient.Models;

namespace WebApplicationPatient.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatients();
        Task<Patient> GetPatientById(int id);
        Task<int> AddPatient(Patient patient);
        Task<int> UpdatePatient(int id, Patient patient);
        Task<int> DeletePatient(int id);
        bool SeedPatients(int count);
    }
}
