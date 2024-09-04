using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class TherapyService : ITherapy
    {
        private readonly DatabaseContext db;

        public TherapyService(DatabaseContext _database)
        {
            this.db = _database;
        }

        public bool CodeTaken(string code)
        {
            Therapy? therapy = db.Therapies.Where( therapy => therapy.code == code).FirstOrDefault();
            if(therapy == null)
            {
                return false;
            }
            return true;

        }

        public async Task<Therapy?>? CreateTherapy(Therapy therapy)
        {
            if (CodeTaken(therapy.code))
            {
                return null;
            }
            else
            {
                db.Therapies.Add(therapy);
                await db.SaveChangesAsync();
                return therapy;
            }
        }

        public async Task<Therapy?> DeleteTherapyByCode(string code)
        {
           Therapy? therapy =await db.Therapies.Where(therapy => therapy.code == code).FirstOrDefaultAsync();
           if(therapy == null)
            {
                return null;
            }
           db.Therapies.Remove(therapy);
            await db.SaveChangesAsync();
            return therapy;


        }

        public async Task<Therapy?> DeleteTherapyById(int id)
        {
            Therapy? therapy = await db.Therapies.Where( therapy => therapy.Id == id).FirstOrDefaultAsync();
            List<Session> sessions = await db.Sessions.Where( session => session.therapyId == id).ToListAsync();
            db.Sessions.RemoveRange(sessions);
            if( therapy == null )
            {
                return null;
            }
            else
            {
                db.Therapies.Remove(therapy);
                await db.SaveChangesAsync();
                return therapy;
            }


        }

        public async Task<List<Therapy>?>? GetAll()
        {
            return await db.Therapies.Include( therapy => therapy.doctor).Include(therapy => therapy.patient).ToListAsync();
        }

        public async Task<Therapy?> GetTherapyByCode(string code)
        {
            return await db.Therapies.Where( therapy => therapy.code == code).Include( therapy => therapy.doctor).Include( therapy => therapy.patient).FirstOrDefaultAsync();
        }

        public async Task<Therapy?> GetTherapyById(int id)
        {
            return await db.Therapies.Where( therapy => therapy.Id == id).Include( therapy => therapy.doctor).Include( therapy => therapy.patient).FirstOrDefaultAsync();
        }

        public async Task<List<Therapy>?>? GetTherapiesByDoctorId(int userId)
        {
            return await db.Therapies.Where( therapy => therapy.doctorId == userId).Include( therapy => therapy.patient).ToListAsync();  
        }

        public async Task<List<Therapy>?> GetTherapiesByPatientId(int userId)
        {
            return await db.Therapies.Where( therapy => therapy.patientId == userId).Include( therapy => therapy.doctor).ToListAsync();
        }

        public async Task<Therapy?> UpdateTherapy(Therapy therapy)
        {
            db.Therapies.Update(therapy);
            await db.SaveChangesAsync();

            return therapy;


        }
    }
}
