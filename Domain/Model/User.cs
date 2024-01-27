using Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class User : Entity
    {
        public int id { get; set; }
        public Person Person { get; set; }
        public string PasswordHash { get; set; }
        public string? UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsFisc { get; set; }
        public bool IsVip { get; set; }

        public User(int id, Person person, string hashedPassword, string userName, bool isAdmin, bool isFisc, bool isVip)
            : base(id)
        {
            Person = person;
            PasswordHash = hashedPassword;
            UserName = userName;
            IsAdmin = isAdmin;
            IsFisc = isFisc;
            IsVip = isVip;
        }

        public User() : base(0)
        {
        }
    }
}
