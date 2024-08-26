using IS_server.DTO;

namespace IS_server.Data
{
    public class User
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int role { get; set; }
        public string kontakt { get; set; }
        public string datumRodjenja { get; set; }
    }
}
