using Microsoft.Data.SqlClient;
using WebApplicationPatient.Interfaces;
using WebApplicationPatient.Models;
using Dapper;
using WebApplicationPatients.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationPatient.Repositories
{
    public class PatientRepository : IPatientRepository
    {

        private readonly DefaultContext _context;

        public PatientRepository(DefaultContext context)
        {

            _context = context;
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<int> AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient.iId;
        }

        public async Task<int> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.iId)
            {
                return 0;
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return 0;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.iId == id);
        }

        public async Task<int> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

    }
}
