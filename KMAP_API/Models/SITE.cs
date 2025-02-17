﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMAP_API.Models
{
    public class Site
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Libelle { get; set; }

        public Entreprise Entreprise { get; set; }

        public ICollection<Personnel> Personnels { get; set; }

        public ICollection<Vehicule> Vehicules { get; set; }

        public Site()
        {

        }
    }
}
