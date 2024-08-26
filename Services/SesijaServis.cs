using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class SesijaServis : ISesija
    {
        private readonly DatabaseContext db;

        public SesijaServis(DatabaseContext _database)
        {
            this.db = _database;
        }

        public async Task<Sesija?>? CreateSesija(Sesija sesija)
        {
           db.Sesije.Add(sesija);
           await db.SaveChangesAsync();

            return sesija;
        }

        public async Task<Sesija?>? DeleteSesija(int id)
        {
            Sesija? sesija =await db.Sesije.Where(se => se.Id == id).FirstOrDefaultAsync();

            if (sesija != null)
            {
                db.Sesije.Remove(sesija);
                await db.SaveChangesAsync();
                return sesija;
            }
            else
                return null;
        }

        public async Task<bool> DeleteSesijeByTerapija(int idTerapije)
        {
          Terapija t = await db.Terapije.Where( t => t.Id == idTerapije).FirstOrDefaultAsync();
            if (t == null)
            {
                return false;
            }
            List<Sesija> lista = await db.Sesije.Where(s => s.idTerapije == t.Id).ToListAsync();
            db.Sesije.RemoveRange(lista);
            await db.SaveChangesAsync();

            return true;

        }

        public async Task<List<Sesija>?>? GetAllSesije()
        {
            return await db.Sesije.Include(s=>s.terapija).Include(s=>s.soba).ToListAsync();
        }

        public async Task<Sesija?> GetSesijaById(int id)
        {
            return await db.Sesije.Where(se => se.Id == id).Include(s => s.terapija).Include(s => s.soba).FirstOrDefaultAsync();
        }

        public async Task<List<Sesija?>?> GetSesijaByIdDoktora(int id)
        {
            List<Terapija>? t =await db.Terapije.Where( t => t.idDoktora == id).ToListAsync();
            if(t== null)
            {
                return null;
            }
            List<Sesija> s = new List<Sesija>();
            foreach (Terapija terapija in t)
            {
                var sesije =await db.Sesije.Where(s => s.idTerapije == terapija.Id).Include(s=> s.soba).ToListAsync();
                s.AddRange(sesije);
            }

            return s;

        }
           

        public async Task<List<Sesija>?>? GetSesijaByTerapijaCode(string code)
        {
            Terapija? terapija = await db.Terapije.Where(t => t.sifra == code).FirstOrDefaultAsync();
            if(terapija != null)
            { 
                return await db.Sesije.Where( se => se.idTerapije == terapija.Id).ToListAsync();
            }

            return null;

        }

        public async Task<List<Sesija>?> GetSesijeByIdPacijent(int id)
        {
            List<Terapija>? t = await db.Terapije.Where(t => t.idPacijenta == id).ToListAsync();
            if (t == null)
            {
                return null;
            }
            List<Sesija> s = new List<Sesija>();
            foreach (Terapija terapija in t)
            {
                var sesije = await db.Sesije.Where(s => s.idTerapije == terapija.Id).Include(s => s.soba).ToListAsync();
                s.AddRange(sesije);
            }

            return s;
        }

        public async Task<List<Sesija>?>? GetSesijeByTerapija(int terapijaId)
        {
            return await db.Sesije.Where( se => se.idTerapije == terapijaId).Include(s=>s.soba).ToListAsync();
        }

        public async Task<Sesija?>? UpdateSesija(Sesija sesija)
        {
            if (sesija == null)
            {
                return null;
            }    
            db.Sesije.Update(sesija);
            await db.SaveChangesAsync();

            return sesija;
        
        }
    }
}
