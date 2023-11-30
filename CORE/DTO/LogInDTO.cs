namespace DTO
{
    public class LogInDTO
    {
        public string Email { get; set; }
        public string PassWord { get; set; }

        public LogInDTO(string email, string password)
        {
            Email = email;
            PassWord = password;
        }
    }
}
