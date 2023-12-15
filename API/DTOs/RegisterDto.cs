namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Player name must be in between {2} - {1} length of characters")]
        public string PlayerName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be in between {2} - {1} length of characters")]
        public string Password { get; set; }
        public int CountryId { get; set; }
    }
}
