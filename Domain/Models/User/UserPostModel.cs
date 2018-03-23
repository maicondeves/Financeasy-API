namespace Financeasy.Api.Domain.Models
{
    public class UserPostModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public long UserId { get; set; }
    }
}