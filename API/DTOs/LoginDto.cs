namespace API.DTOs
{
    public class LoginDto
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set => _userName = value.ToLower();
        }
        public string Password { get; set; }
    }
}
