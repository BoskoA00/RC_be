using IS_server.Data;

namespace IS_server.DTO
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public int role { get; set; }
        public string contact { get; set; }
        public string birthDate { get; set; }
    }
}
