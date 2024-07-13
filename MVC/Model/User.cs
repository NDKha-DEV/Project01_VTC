using System;

namespace Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set;}
        public string Password { get; set;}
        public string Email { get; set;}
        public string FullName { get; set;}
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set;} = DateTime.Now;
        public string Address { get; set; }
        //public ICollection<>
    }
}