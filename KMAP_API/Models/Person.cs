﻿using System;

namespace KMAP_API.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Permis { get; set; }
    }
}
