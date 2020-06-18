using System.ComponentModel.DataAnnotations;

namespace KMAP_API.Models
{
    public class Utilisateur : Personnel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Role Role { get; set; }

        public Utilisateur() : base()
        {

        }
    }
}
