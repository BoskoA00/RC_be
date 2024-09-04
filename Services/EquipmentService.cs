using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class EquipmentService : IEquipment
    {
        private readonly DatabaseContext db;

        public EquipmentService( DatabaseContext _db)
        {
            this.db = _db;
        }


        public async Task<Equipment?>? CreateEquipment(Equipment equipment)
        {
          if(equipment == null)
            {
                return null;
            }
            else
            {
                db.Equipment.Add(equipment);
                await db.SaveChangesAsync();
                return equipment;
            }
        }

        public async Task<Equipment?>? DeleteEquipment(int id)
        {
            Equipment? equipment = await db.Equipment.Where( equipment => equipment.Id == id).FirstOrDefaultAsync();
            
            if(equipment == null)
            {
                return null;
            }
            else
            {
                db.Equipment.Remove(equipment);
                await db.SaveChangesAsync();
                return equipment;
            }
        }

        public async Task<List<Equipment>?> GetAll()
        {
            return await db.Equipment.Include( equipment => equipment.room).ToListAsync();
        }

        public async Task<Equipment?> GetById(int id)
        {
           return await db.Equipment.Where( equipment => equipment.Id == id).Include( equipment => equipment.room).FirstOrDefaultAsync();
        }

        public async Task<Equipment?>? GetEquipmentByRoomNumber(string roomNumber)
        {
            Room? room = await db.Rooms.Where(room => room.roomNumber == roomNumber).FirstOrDefaultAsync();
            if(room == null)
            {
                return null;
            }

            return await db.Equipment.Where( equipment => equipment.roomId == room.Id).FirstOrDefaultAsync();


        }

        public async Task<Equipment?> GetEquipmentByRoomId(int id)
        {
            Room? room = await db.Rooms.Where(room => room.Id == id).FirstOrDefaultAsync();
            if (room == null)
            {
                return null;
            }

            return await db.Equipment.Where( equipment => equipment.roomId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CodeTaken(string code)
        {
            Equipment? equipment =await db.Equipment.Where(equipment => equipment.code == code).FirstOrDefaultAsync();
            if (equipment == null)
            {
                return false;
            }
            else
                return true;
        }

        public async Task<Equipment?>? UpdateEquipment(Equipment equipment)
        {
            if( equipment == null)
            {
                return null;
            }
            else
            {
                db.Equipment.Update(equipment);
                await db.SaveChangesAsync();

                return equipment; 


            }

        }
    }
}
