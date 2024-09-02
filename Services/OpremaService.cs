using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class OpremaService : IOprema
    {
        private readonly DatabaseContext db;

        public OpremaService( DatabaseContext _db)
        {
            this.db = _db;
        }


        public async Task<Oprema?>? CreateOprema(Oprema oprema)
        {
          if(oprema == null)
            {
                return null;
            }
            else
            {
                db.Opreme.Add(oprema);
                await db.SaveChangesAsync();
                return oprema;
            }
        }

        public async Task<Oprema?>? DeleteOprema(int id)
        {
            Oprema? oprema = await db.Opreme.Where(o => o.Id == id).FirstOrDefaultAsync();
            
            if(oprema == null)
            {
                return null;
            }
            else
            {
                db.Opreme.Remove(oprema);
                await db.SaveChangesAsync();
                return oprema;
            }
        }

        public async Task<List<Oprema>?> GetAll()
        {
            return await db.Opreme.Include(o => o.soba).ToListAsync();
        }

        public async Task<Oprema?> GetById(int id)
        {
           return await db.Opreme.Where( o => o.Id == id).Include(o=>o.soba).FirstOrDefaultAsync();
        }

        public async Task<Oprema?>? GetOpremaByBrojSobe(string brojSobe)
        {
            Soba? soba = await db.Sobe.Where(s => s.brojSobe == brojSobe).FirstOrDefaultAsync();
            if(soba == null)
            {
                return null;
            }

            return await db.Opreme.Where(o => o.idSobe == soba.Id).FirstOrDefaultAsync();


        }

        public async Task<Oprema?> GetOpremaByIdSobe(int id)
        {
           return await db.Opreme.Where( o => o.idSobe == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SifraTaken(string sifra)
        {
            Oprema? o =await db.Opreme.Where(oprema => oprema.sifra == sifra).FirstOrDefaultAsync();
            if (o == null)
            {
                return false;
            }
            else
                return true;
        }

        public async Task<Oprema?>? UpdateOprema(Oprema oprema)
        {
            if( oprema == null)
            {
                return null;
            }
            else
            {
                db.Opreme.Update(oprema);
                await db.SaveChangesAsync();

                return oprema; 


            }

        }
    }
}
