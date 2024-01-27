using Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class User : Entity
    {
        public int id { get; set; }
        public Person Person { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public string? UserName { get; set; }

        public User(int id, Person person, string role, string hashedPassword, string userName)
            : base(id)
        {
            Person = person;
            Role = role;
            PasswordHash = hashedPassword;
            UserName = userName;
        }
        public User() : base(0)
        {
        }
    }
}
