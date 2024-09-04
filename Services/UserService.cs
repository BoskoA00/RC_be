using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IS_server.Services
{
    public class UserService : IUser
    {
        private readonly DatabaseContext db;
        private readonly IConfiguration configuration;
        public UserService(DatabaseContext _databaseContext, IConfiguration config)
        {
            this.db = _databaseContext;
            this.configuration = config;
        }


        public async Task<User?>? CreateUser(User user)
        {
            if (user == null)
            {
                return null;
            }
            if (TakenUsername(user.userName))
            {
                return null;
            }
            await db.Users.AddAsync(user);

            await db.SaveChangesAsync();

            return user;

        }

        public async Task<User?> DeleteByUserName(string userName)
        {
            User? user = await db.Users.Where( user => user.userName == userName).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return user;
        }

        public async Task<User?> DeleteUser(int id)
        {
            User? user = await db.Users.Where( user => user.Id == id).FirstOrDefaultAsync();
            List<Therapy> therapies = await db.Therapies.Where(therapy => therapy.patientId == id || therapy.doctorId == id).ToListAsync();
            foreach (Therapy therapy in therapies)
            {
                List<Session> sessions = await db.Sessions.Where( session => session.therapyId == therapy.Id).ToListAsync();
                db.Sessions.RemoveRange(sessions);
            }
            db.Therapies.RemoveRange(therapies);
            List<Report> reports = await db.Reports.Where(therapy => therapy.patientId == id || therapy.doctorId == id).ToListAsync();
            db.Reports.RemoveRange(reports);
            if (user == null)
            {
                return null;
            }
            List<Message> messages = await db.Messages.Where( message => message.senderId == id || message.receiverId == id ).ToListAsync();
            
            db.Messages.RemoveRange(messages);

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return user;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Auth:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.role.ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<List<User>?>? GetAllUsers()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await db.Users.Where(u => u.userName == userName).FirstOrDefaultAsync();
        }

        public string HashPassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return String.Empty;
            }

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }

        public bool TakenPassword(string password)
        {
                string hashedPassword = HashPassword(password);
          
            User? user = db.Users.Where( u => u.password == hashedPassword).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public  bool TakenUsername(string username)
        {
            User? taken =  db.Users.Where( u => u.userName == username).FirstOrDefault();
            if (taken != null)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<User?> UpdateUser(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();

            return user;
                 
        }
        public async Task<List<User>?> GetUsersByRole(int role)
        {
            return await db.Users.Where(u => u.role == role).ToListAsync();
        }

        public  async Task<bool> PromoteUser(int id)
        {
          User? user = await db.Users.Where( u => u.Id == id).FirstOrDefaultAsync();
           if (user == null) { return false; }
            user.role =user.role + 1;
            await db.SaveChangesAsync();
            return true;
        }

        public async  Task<bool> DemoteUser(int id)
        {
            User? user = await db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null) { return false; }
            user.role =user.role -  1;
            await db.SaveChangesAsync();
            return true;

        }
    }
}
