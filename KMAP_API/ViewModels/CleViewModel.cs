using KMAP_API.Models;
using System;

namespace KMAP_API.ViewModels
{
    public class CleViewModel
    {
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        public CleViewModel()
        {

        }

        public CleViewModel(Cle c)
        {
            Id = c.Id;
            Libelle = c.Libelle;
        }
    }
}
