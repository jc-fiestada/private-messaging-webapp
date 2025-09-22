namespace PrivateChat.Models
{
    class UserDTO
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? HashedPassword { get; set; }
        public string? Sex { get; set; }
        public int? Age { get; set; }
    }
}