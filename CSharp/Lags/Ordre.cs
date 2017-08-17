﻿using System;

namespace Lags
{
    public class Ordre
    {
        public Ordre(String id, int debut, int duree, double prix)
        {
            this.id = id;
            this.debut = debut;  // au format AAAAJJJ par exemple 25 février 2015 = 2015056
            this.duree = duree;
            this.prix = prix;
        }

        
        public string id { get; set; }
        
        public int debut { get; set; }

        public int duree { get; set; }

        public double prix { get; set; }

    }
}
