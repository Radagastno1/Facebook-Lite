namespace CORE
{
    public class IncomingLogInDTO
    {
        public string Email { get; set; }
        public string PassWord { get; set; }

        public IncomingLogInDTO(string email, string password)
        {
            Email = email;
            PassWord = password;
        }
    }
}
