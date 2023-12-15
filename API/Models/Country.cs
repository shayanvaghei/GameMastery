namespace API.Models
{
    [Table("Country", Schema = "Setting")]
    public class Country
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
