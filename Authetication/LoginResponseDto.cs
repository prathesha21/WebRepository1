namespace WebRepository1.Authetication
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

    }
    public class LoginRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
