using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplicationPatient.Models
{
    [DataContract]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
