using GestionNavire.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionNavire.Classesmetier
{
    internal class Navire
    {
        private string imo;
        private string nom;
        private string libelleFret;
        private int qteFretMaxi;
        private int qteFret;

        public Navire(string imo, string nom, string libelleFret, int qteFretMaxi, int qteFret)
        {
            this.Imo = imo;
            this.Nom = nom;
            this.LibelleFret = libelleFret;
            this.QteFretMaxi = qteFretMaxi;
            this.QteFret = qteFret;
        }

        public Navire(string imo, string nom) : this(imo, nom, "Indéfini", 0, 0) { }

        public string Imo {
            get => imo;
            set
            {
                if (Regex.IsMatch(value, @"^IMO[1-9]\d{6}$"))
                {
                    this.imo = value;
                }
                else
                {
                    throw new Exception("Erreur, format de l'imo invalide");
                }
            }
        }
        public string Nom { get => nom; set => nom = value; }
        public string LibelleFret { get => libelleFret; set => libelleFret = value; }
        public int QteFretMaxi
        {
            get => qteFretMaxi;
            set
            {
                if (value >= 0)
                {
                    this.qteFretMaxi = value;
                }
                else
                {
                    throw new Exception("Erreur, quantité de fret non valide");
                }
            }
        }
        public int QteFret
        {
            get => qteFret;
            set
            {
                if (value >= 0 && value <= qteFretMaxi)
                {
                    this.qteFret = value;
                }
                else
                {
                    throw new GestionPortException("Valeur incohérente pour la quantité de fret stockée dans le navire");
                }
            }
        }

        public string Affiche(Navire navire)
        {
            return ($" Identification : {navire.imo.ToString()}\n" +
                $" Nom : {navire.nom.ToString()}\n" +
                $" Type de frêt : {navire.libelleFret.ToString()}\n" +
                $" Qte de frêt : {navire.qteFretMaxi.ToString()}\n" +
                $"----------------------------------");
        }

        public void Decharger(int quantite)
        {
            if (quantite < 0) throw new GestionPortException("La quantité à décharger ne peut être négative ou nulle");
            else if (quantite > QteFret) throw new GestionPortException("Impossible de décharger plus que la quantité de fret dans le navire");
            
            QteFret -= quantite;
        }

        public bool EstDecharge()
        {
            return QteFret == 0;
        }
    }
}
