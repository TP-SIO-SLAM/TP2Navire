using GestionNavire.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNavire.Classesmetier
{
    class Port
    {
        private string nom;
        private int nbNavireMax;
        private Dictionary<string, Navire> navires;
        private List<Stockage> stockages;

        public Port(string nom)
        {
            this.nom = nom;
            this.nbNavireMax = 5;
            this.navires = new Dictionary<string, Navire>();
            this.stockages = new List<Stockage>();
        }

        internal Dictionary<string, Navire> Navires { get => navires; set => navires = value; }


        public void EnregistrerArrivee(Navire navire)
        {

            try
            {
                if (this.navires.Count < this.nbNavireMax) this.navires.Add(navire.Imo, navire);
                else throw new GestionPortException("Ajout impossible,le port est complet");
            }
            catch (ArgumentException)
            {
                throw new GestionPortException("Le navire " + navire.Imo + " est déjà enregistré");
            }

            /*if (this.nbNavireMax != this.navires.Count)
                if (!EstPresent(navire.Imo)) this.navires.Add(navire.Imo, navire);
                else throw new Exception("Le navire est déjà dans le port");
            else
                throw new Exception("Ajout impossible,le port est complet");*/
        }

        public bool EstPresent(String imo)
        {
            return this.navires.ContainsKey(imo);

        }

        public void EnregistrerDepart(String imo)
        {
            if (this.EstPresent(imo))
                this.navires.Remove(imo);
            else
                throw new GestionPortException($"Impossible d'enregistrer le départ du navire " + imo + ", il n'est pas dans le port");           
            
        }


        public void Dechargement(String imo)
        {
            Navire navire = GetNavire(imo);
            if (navire != null)
            {
                if (navire.LibelleFret == "Porte-conteneurs")
                {

                    int i = 0;
                    while (i < stockages.Count() && !navire.EstDecharge())
                    {
                        if (stockages[i].CapaciteDispo > 0)
                        {
                            if (navire.QteFret <= stockages[i].CapaciteDispo)
                            {
                                stockages[i].Stocker(navire.QteFret);
                                navire.Decharger(navire.QteFret);
                            }
                            else
                            {
                                navire.Decharger(stockages[i].CapaciteDispo);
                                stockages[i].Stocker(stockages[i].CapaciteDispo);

                            }
                        }

                        i++;
                    }


                    if (!navire.EstDecharge())
                        throw new GestionPortException("Le navire" + imo + "n'a pas pu être entièrement déchargé, il reste " + navire.QteFret + " tonnes");


                }
                else throw new GestionPortException("Ce type de navire ne peut être déchargé dans les zones de stockage");
            }
            else throw new GestionPortException("Le navire n'est pas dans le port");
        }


        public void AjoutStockage(Stockage stockage)
        {
            stockages.Add(stockage);
        }

        public Navire GetNavire(String imo)
        {
            if (navires.ContainsKey(imo)) return navires[imo];
            else return null;
        }

    }
}
