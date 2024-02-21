using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomJWTAuthentication.Models.Entities
{

    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public User() { }
        public User(RegisterRequest user, Role Role)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            this.Role = Role;
            PasswordHash = user.Password;
        }
    }
}
