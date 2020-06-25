using KMAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMAP_API.ViewModels
{
    public class VehiculeViewModel
    {
        public Guid Id { get; set; }

        public string NumImmat { get; set; }

        public string Modele { get; set; }

        public int NbPlaces { get; set; }

        public int NbPortes { get; set; }

        public string TypeCarbu { get; set; }

        public bool Actif { get; set; }

        public ICollection<Cle> Cles { get; set; }

        public VehiculeViewModel(Vehicule v)
        {
            Id = v.Id;
            NumImmat = v.NumImmat;
            Modele = v.Modele;
            NbPlaces = v.NbPlaces;
            NbPortes = v.NbPortes;
            TypeCarbu = v.TypeCarbu;
            Actif = v.Actif;
            Cles = v.Cles;
        }
    }
}
