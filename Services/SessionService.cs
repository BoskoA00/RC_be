using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class SessionService : Interfaces.ISession
    {
        private readonly DatabaseContext db;

        public SessionService(DatabaseContext _database)
        {
            this.db = _database;
        }

        public async Task<Session?>? CreateSession(Session session)
        {
           db.Sessions.Add(session);
           await db.SaveChangesAsync();

            return session;
        }

        public async Task<Session?>? DeleteSession(int id)
        {
            Session? session =await db.Sessions.Where(sessions => sessions.Id == id).FirstOrDefaultAsync();

            if (session != null)
            {
                db.Sessions.Remove(session);
                await db.SaveChangesAsync();
                return session;
            }
            else
                return null;
        }

        public async Task<bool> DeleteSessionsByTherapy(int therapyId)
        {
          Therapy therapy = await db.Therapies.Where( therapy => therapy.Id == therapyId).FirstOrDefaultAsync();
            if (therapy == null)
            {
                return false;
            }
            List<Session> list = await db.Sessions.Where( session => session.therapyId == therapy.Id).ToListAsync();
            db.Sessions.RemoveRange(list);
            await db.SaveChangesAsync();

            return true;

        }

        public async Task<List<Session>?>? GetAllSessions()
        {
            return await db.Sessions.Include( session => session.therapy).Include(session => session.room).ToListAsync();
        }

        public async Task<Session?> GetSessionById(int id)
        {
            return await db.Sessions.Where( session => session.Id == id).Include( session => session.therapy).Include( session => session.room).FirstOrDefaultAsync();
        }

        public async Task<List<Session?>?> GetSessionsByDoctorId(int id)
        {
            List<Therapy>? therapies =await db.Therapies.Where( therapy => therapy.doctorId == id).ToListAsync();
            if(therapies== null)
            {
                return null;
            }
            List<Session> sessions = new List<Session>();
            foreach (Therapy therapy in therapies)
            {
                var sessionsList = await db.Sessions.Where(session => session.therapyId == therapy.Id).Include(session => session.room).ToListAsync();
                sessionsList.AddRange(sessionsList);
            }

            return sessions;

        }
           

        public async Task<List<Session>?>? GetSessionsByTherapyCode(string code)
        {
            Therapy? therapy = await db.Therapies.Where( therapy => therapy.code == code).FirstOrDefaultAsync();
            if(therapy != null)
            { 
                return await db.Sessions.Where( session => session.therapyId == therapy.Id).ToListAsync();
            }

            return null;

        }

        public async Task<List<Session>?> GetSessionsByPatientId(int id)
        {
            List<Therapy>? therapies = await db.Therapies.Where( therapy => therapy.patientId == id).ToListAsync();
            if (therapies == null)
            {
                return null;
            }
            List<Session> sessions = new List<Session>();
            foreach (Therapy therapy in therapies)
            {
                var sessionList = await db.Sessions.Where(session => session.therapyId == therapy.Id).Include(session => session.room).ToListAsync();
                sessions.AddRange(sessionList);
            }

            return sessions;
        }

        public async Task<List<Session>?>? GetSessionsByTherapyId(int therapyId)
        {
            return await db.Sessions.Where( sessions => sessions.therapyId == therapyId).Include( session => session.room).ToListAsync();
        }

        public async Task<Session?>? UpdateSession(Session session)
        {
            if (session == null)
            {
                return null;
            }    
            db.Sessions.Update(session);
            await db.SaveChangesAsync();

            return session;
        
        }
    }
}
