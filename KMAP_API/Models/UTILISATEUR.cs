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

        public Utilisateur(Personnel p) : base()
        {
            Id = p.Id;
            Prenom = p.Prenom;
            Nom = p.Nom;
            Mail = p.Mail;
            Permis = p.Permis;
            Site = p.Site;
            Personnel_Reservations = p.Personnel_Reservations;
        }
    }
}
