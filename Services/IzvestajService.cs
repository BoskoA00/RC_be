using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;

namespace IS_server.Services
{
    public class IzvestajService : IIzvestaj
    {
        private readonly DatabaseContext db;

        public IzvestajService( DatabaseContext _db)
        {
            this.db = _db;
        }

        public  bool CodeTaken(string code)
        {
            Izvestaj? izvestaj = db.Izvestaji.Where(i => i.sifra == code).FirstOrDefault();
            if (izvestaj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Izvestaj?>? CreateIzvestaj(Izvestaj izvestaj)
        {
          if(izvestaj == null)
            {
                return null; 
            }
            else
            {
                db.Izvestaji.Add(izvestaj);
                await db.SaveChangesAsync();
                return izvestaj;
            }
        }

        public async Task<Izvestaj?>? DeleteByCode(string code)
        {
            Izvestaj? izvestaj = await db.Izvestaji.Where(i => i.sifra == code).FirstOrDefaultAsync();
            if( izvestaj == null)
            {
                return null;
            }
            else
            {
                db.Izvestaji.Remove(izvestaj);
                await db.SaveChangesAsync();
                return izvestaj;
            }
        }

        public async Task<List<Izvestaj>?>? DeleteByDoktor(int idDoktora)
        {
          List<Izvestaj> list=await db.Izvestaji.Where( i => i.idDoktora == idDoktora ).ToListAsync();
            db.Izvestaji.RemoveRange(list);
            await db.SaveChangesAsync();
            return list;
        }

        public async Task<Izvestaj?>? DeleteById(int id)
        {
            Izvestaj? izvestaj = await db.Izvestaji.Where(i => i.Id == id).FirstOrDefaultAsync();
            if(izvestaj == null)
            {
                return null;
            }
            else
            {
                db.Izvestaji.Remove(izvestaj);
                await db.SaveChangesAsync();
                return izvestaj;
            }

        }

        public async Task<List<Izvestaj>?> DeleteByPacijent(int idPacijenta)
        {
            List<Izvestaj>? list = await db.Izvestaji.Where(i => i.idPacijenta == idPacijenta).ToListAsync();
            db.Izvestaji.RemoveRange(list);
            await db.SaveChangesAsync();
            return list;
        }

        public async Task<List<Izvestaj>?> GetAll()
        {
            return await db.Izvestaji.Include(i => i.Pacijent).Include(i => i.Doktor).ToListAsync();
        }

        public async Task<Izvestaj?>? GetById(int id)
        {
            return await db.Izvestaji.Where(i => i.Id == id).Include(i => i.Pacijent).Include(i => i.Doktor).FirstOrDefaultAsync();
        }

        public async Task<Izvestaj?>? GetIzvestajByCode(string code)
        {
           return await db.Izvestaji.Where( i => i.sifra == code).Include(i => i.Pacijent).Include(i => i.Doktor).FirstOrDefaultAsync();
        }

        public async Task<List<Izvestaj>?>? GetIzvestajiByDoktor(int userId)
        {
            return await db.Izvestaji.Where( i => i.idDoktora == userId).Include(i => i.Pacijent).Include(i => i.Doktor).ToListAsync();
        }
        public async Task<List<Izvestaj>?>? GetIzvestajiByPacijent(int userId)
        {
            return await db.Izvestaji.Where( i => i.idPacijenta == userId).Include(i => i.Pacijent).Include(i => i.Doktor).ToListAsync();
        }

        public async Task<Izvestaj?>? UpdateIzvestaj(Izvestaj izvestaj)
        {
           if(izvestaj == null)
            {
                return null; 
            }
            else
            {
                db.Izvestaji.Update(izvestaj);
                await db.SaveChangesAsync();
                return izvestaj;
            }
        }
    }
}
