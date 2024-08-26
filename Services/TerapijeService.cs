using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class TerapijeService : ITerapija
    {
        private readonly DatabaseContext db;

        public TerapijeService(DatabaseContext _database)
        {
            this.db = _database;
        }

        public bool CodeTaken(string code)
        {
            Terapija? terapija = db.Terapije.Where(t => t.sifra == code).FirstOrDefault();
            if(terapija == null)
            {
                return false;
            }
            return true;

        }

        public async Task<Terapija?>? CreateTerapija(Terapija terapija)
        {
            if (CodeTaken(terapija.sifra))
            {
                return null;
            }
            else
            {
                db.Terapije.Add(terapija);
                await db.SaveChangesAsync();
                return terapija;
            }
        }

        public async Task<Terapija?> DeleteTerapijaByCode(string code)
        {
           Terapija? terapija =await db.Terapije.Where( t => t.sifra == code).FirstOrDefaultAsync();
           if(terapija == null)
            {
                return null;
            }
           db.Terapije.Remove(terapija);
            await db.SaveChangesAsync();
            return terapija;


        }

        public async Task<Terapija?> DeleteTerapijaById(int id)
        {
            Terapija? terapija = await db.Terapije.Where( t => t.Id == id).FirstOrDefaultAsync();
            List<Sesija> sesije = await db.Sesije.Where(s=>s.idTerapije == id).ToListAsync();
            db.Sesije.RemoveRange(sesije);
            if( terapija == null )
            {
                return null;
            }
            else
            {
                db.Terapije.Remove(terapija);
                await db.SaveChangesAsync();
                return terapija;
            }


        }

        public async Task<List<Terapija>?>? GetAll()
        {
            return await db.Terapije.Include(t => t.Doktor).Include(t => t.Pacijent).ToListAsync();
        }

        public async Task<Terapija?> GetTerapijaByCode(string code)
        {
            return await db.Terapije.Where(t => t.sifra == code).Include(t => t.Doktor).Include(t => t.Pacijent).FirstOrDefaultAsync();
        }

        public async Task<Terapija?> GetTerapijaById(int id)
        {
            return await db.Terapije.Where( t => t.Id == id).Include(t=>t.Doktor).Include(t => t.Pacijent).FirstOrDefaultAsync();
        }

        public async Task<List<Terapija>?>? GetTerapijeByDoktor(int userId)
        {
            return await db.Terapije.Where( t => t.idDoktora == userId).Include(t=>t.Pacijent).ToListAsync();  
        }

        public async Task<List<Terapija>?> GetTerapijeByPacijent(int userId)
        {
            return await db.Terapije.Where(t => t.idPacijenta == userId).Include(t=>t.Doktor).ToListAsync();
        }

        public async Task<Terapija?> UpdateTerapija(Terapija terapija)
        {
            db.Terapije.Update(terapija);
            await db.SaveChangesAsync();

            return terapija;


        }
    }
}
