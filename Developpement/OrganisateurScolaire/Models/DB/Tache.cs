﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OrganisateurScolaire.Models
{
    /// <summary>
    /// DBModel (tblTaches).
    /// </summary>
    public class Tache
    {

        public int ID { get; init; }
        public List<Rappel> Rappels { get; set; }
        public string Statut { get; init; }
        public string Titre { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Description { get; set; }
        public Brush Background { get; set; }
        public int IdCours { get; set; }
        public int IdCategorie { get; set; }


        public Tache(int iD , string titre, DateTime dateFin, string description, int idCours, int idCategorie)
        {
            ID = iD;
            Titre = titre;
            DateFin = dateFin;
            Description = description;
            IdCours = idCours;
            IdCategorie = idCategorie;
        }
        public Tache() { }
    }
}
