using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class SobaService : ISoba
    {
        private readonly DatabaseContext db;

        public SobaService( DatabaseContext _databaseContext)
        {
            this.db = _databaseContext;
        }


        public  int CheckStatus(string brSobe)
        {
            Soba? soba = db.Sobe.Where( s => s.brojSobe == brSobe ).FirstOrDefault();
            if(soba == null )
            {
                return -1;
            }
            return soba.status;
        }

        public async Task<Soba?>? CreateSoba(Soba soba)
        {
            if(soba == null)
            {
                return null;
            }
            db.Sobe.Add(soba);
            await db.SaveChangesAsync();

            return soba;
        }

        public async Task<Soba?>? DeleteSoba(int id)
        {
            Soba? soba = await db.Sobe.Where( s => s.Id == id ).FirstOrDefaultAsync();
           
            if(soba == null)
            {
                return null;
            }
            else
            {
                    Oprema? oprema = await db.Opreme.Where(o => o.idSobe == id).FirstOrDefaultAsync();
                if (oprema != null)
                {

                db.Opreme.Remove(oprema);
                }    
                db.Sobe.Remove(soba);
                await db.SaveChangesAsync();
                return soba;
            }

        }

        public async Task<bool> DisableRoom(string brojSobe)
        {
            Soba? s = await db.Sobe.Where(s => s.brojSobe == brojSobe ).FirstOrDefaultAsync();
            if (s == null)
            {
                return false;
            }
            s.status = 2;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FreeRoom(string brojSobe)
        {
            Soba? s = await db.Sobe.Where(s => s.brojSobe == brojSobe).FirstOrDefaultAsync();
            if (s == null)
            {
                return false;
            }
            s.status = 0;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Soba>?> GetAll()
        {
            return await db.Sobe.Include(s=>s.oprema).ToListAsync();
        }

        public async Task<Soba?>? GetById(int id)
        {
            return await db.Sobe.Where( s => s.Id == id).Include(s=>s.oprema).FirstOrDefaultAsync();
        }

        public async Task<Soba?>? GetSobaByBrSobe(string broj)
        {
            return await db.Sobe.Where( s => s.brojSobe == broj ).Include(s=>s.oprema).FirstOrDefaultAsync();
        }

        public async Task<bool> ReserveRoom(string brojSobe)
        {
            Soba? s = await db.Sobe.Where(s => s.brojSobe == brojSobe).FirstOrDefaultAsync();
            if (s == null)
            {
                return false;
            }
            s.status = 1;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Soba?>? UpdateSoba(Soba soba)
        {
           if(soba == null)
            {
                return null;
            }
            else
            {
                db.Sobe.Update(soba);
                await db.SaveChangesAsync();
                return soba;
            }



        }
    }
}
