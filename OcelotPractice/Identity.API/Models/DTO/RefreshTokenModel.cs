namespace Identity.API.Models.DTO
{
    public class RefreshTokenModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime TokenExpiryDate { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
