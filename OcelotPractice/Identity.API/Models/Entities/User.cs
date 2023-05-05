namespace Identity.API.Models.Entities
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = default!;

        public byte[] PasswordSalt { get; set; } = default!;
    }
}
