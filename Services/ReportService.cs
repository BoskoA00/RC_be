using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class ReportService : IReport
    {
        private readonly DatabaseContext db;

        public ReportService( DatabaseContext _db)
        {
            this.db = _db;
        }

        public  bool CodeTaken(string code)
        {
            Report? report = db.Reports.Where(i => i.code == code).FirstOrDefault();
            if (report == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Report?>? CreateReport(Report report)
        {
          if(report == null)
            {
                return null; 
            }
            else
            {
                db.Reports.Add(report);
                await db.SaveChangesAsync();
                return report;
            }
        }

        public async Task<Report?>? DeleteByCode(string code)
        {
            Report? report = await db.Reports.Where(i => i.code == code).FirstOrDefaultAsync();
            if( report == null)
            {
                return null;
            }
            else
            {
                db.Reports.Remove(report);
                await db.SaveChangesAsync();
                return report;
            }
        }

        public async Task<List<Report>?>? DeleteByDoctor(int doctorId)
        {
          List<Report> list=await db.Reports.Where( i => i.doctorId == doctorId ).ToListAsync();
            db.Reports.RemoveRange(list);
            await db.SaveChangesAsync();
            return list;
        }

        public async Task<Report?>? DeleteById(int id)
        {
            Report? report = await db.Reports.Where(i => i.Id == id).FirstOrDefaultAsync();
            if(report == null)
            {
                return null;
            }
            else
            {
                db.Reports.Remove(report);
                await db.SaveChangesAsync();
                return report;
            }

        }

        public async Task<List<Report>?> DeleteByPatient(int patientId)
        {
            List<Report>? list = await db.Reports.Where(i => i.patientId == patientId).ToListAsync();
            db.Reports.RemoveRange(list);
            await db.SaveChangesAsync();
            return list;
        }

        public async Task<List<Report>?> GetAll()
        {
            return await db.Reports.Include(i => i.Patient).Include(i => i.Doctor).ToListAsync();
        }

        public async Task<Report?>? GetById(int id)
        {
            return await db.Reports.Where(i => i.Id == id).Include(i => i.Patient).Include(i => i.Doctor).FirstOrDefaultAsync();
        }

        public async Task<Report?>? GetReportByCode(string code)
        {
           return await db.Reports.Where( i => i.code == code).Include(i => i.Patient).Include(i => i.Doctor).FirstOrDefaultAsync();
        }

        public async Task<List<Report>?>? GetReportsByDoctor(int userId)
        {
            return await db.Reports.Where( i => i.doctorId == userId).Include(i => i.Patient).Include(i => i.Doctor).ToListAsync();
        }
        public async Task<List<Report>?>? GetReportsByPatient(int userId)
        {
            return await db.Reports.Where( i => i.patientId == userId).Include(i => i.Patient).Include(i => i.Doctor).ToListAsync();
        }

        public async Task<Report?>? UpdateReport(Report report)
        {
           if(report == null)
            {
                return null; 
            }
            else
            {
                db.Reports.Update(report);
                await db.SaveChangesAsync();
                return report;
            }
        }
    }
}
