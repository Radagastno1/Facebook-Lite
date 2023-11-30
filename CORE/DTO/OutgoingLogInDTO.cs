using System.ComponentModel;

namespace CORE
{
    public class OutgoingLogInDTO
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string Jwt { get; set; }

        public OutgoingLogInDTO(string email, string password, string jwt)
        {
            Email = email;
            PassWord = password;
            Jwt = jwt;
        }
    }
}
