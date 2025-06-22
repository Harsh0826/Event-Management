namespace EventManagement.Models
{
    public class User
    {
        public int Id { get; set; }           // New Primary Key
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
