using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class RoomService : IRoom
    {
        private readonly DatabaseContext db;

        public RoomService( DatabaseContext _databaseContext)
        {
            this.db = _databaseContext;
        }


        public  int CheckStatus(string roomNumber)
        {
            Room? room = db.Rooms.Where( room => room.roomNumber == roomNumber ).FirstOrDefault();
            if(room == null )
            {
                return -1;
            }
            return room.status;
        }

        public async Task<Room?>? CreateRoom(Room room)
        {
            if(room == null)
            {
                return null;
            }
            db.Rooms.Add(room);
            await db.SaveChangesAsync();

            return room;
        }

        public async Task<Room?>? DeleteRoom(int id)
        {
            Room? room = await db.Rooms.Where( room => room.Id == id ).FirstOrDefaultAsync();
           
            if(room == null)
            {
                return null;
            }
            else
            {
                Equipment? equipment = await db.Equipment.Where(equipment => equipment.roomId == id).FirstOrDefaultAsync();
                if (equipment != null)
                {

                db.Equipment.Remove(equipment);
                }    
                db.Rooms.Remove(room);
                await db.SaveChangesAsync();
                return room;
            }

        }

        public async Task<bool> DisableRoom(string roomNumber)
        {
            Room? room = await db.Rooms.Where(room => room.roomNumber == roomNumber ).FirstOrDefaultAsync();
            if (room == null)
            {
                return false;
            }
            room.status = 2;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FreeRoom(string roomNumber)
        {
            Room? room = await db.Rooms.Where(room => room.roomNumber == roomNumber).FirstOrDefaultAsync();
            if (room == null)
            {
                return false;
            }
            room.status = 0;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Room>?> GetAll()
        {
            return await db.Rooms.Include(room => room.equipment).ToListAsync();
        }

        public async Task<Room?>? GetById(int id)
        {
            return await db.Rooms.Where( room => room.Id == id).Include(room => room.equipment).FirstOrDefaultAsync();
        }

        public async Task<Room?>? GetRoomByNumber(string roomNumber)
        {
            return await db.Rooms.Where( room => room.roomNumber == roomNumber ).Include(room => room.equipment).FirstOrDefaultAsync();
        }

        public async Task<bool> ReserveRoom(string roomNumber)
        {
            Room? room = await db.Rooms.Where(room => room.roomNumber == roomNumber).FirstOrDefaultAsync();
            if (room == null)
            {
                return false;
            }
            room.status = 1;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Room?>? UpdateRoom(Room room)
        {
           if(room == null)
            {
                return null;
            }
            else
            {
                db.Rooms.Update(room);
                await db.SaveChangesAsync();
                return room;
            }



        }
    }
}
