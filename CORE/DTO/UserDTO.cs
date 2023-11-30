namespace CORE
{
    public class UserDTO
    {
        public string FirstName { get; set; }

        public UserDTO(string firstName)
        {
            FirstName = firstName;
        }
    }
}
