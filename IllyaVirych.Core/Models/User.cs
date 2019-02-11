using SQLite;

namespace IllyaVirych.Core.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]       
        public string UserId { get; set; }

        public User(string userId)
        {
            UserId = userId;
        }
        public User()
        {
        }
    }
}
