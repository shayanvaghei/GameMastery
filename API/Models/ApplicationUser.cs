namespace API.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PlayerName { get; set; }
        [StringLength(500)]
        public string About { get; set; }
        public int Points { get; set; } = 0;

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
