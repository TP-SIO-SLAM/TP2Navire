using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionNavire.Exceptions;

namespace GestionNavire.Classesmetier
{
    internal class Stockage
    {
        private readonly int numero;
        private int capaciteMaxi;
        private int capaciteDispo;


        public Stockage(int numero, int capaciteMaxi, int capaciteDispo)
        {
            this.numero = numero;
            this.capaciteMaxi = capaciteMaxi;
            this.capaciteDispo = capaciteDispo;

            if (capaciteMaxi <= 0) throw new GestionPortException("Impossible de créer un stockages avec une capacité négative");
            else if (capaciteDispo > capaciteMaxi) throw new GestionPortException("La capacitéDispo, ne doit pas être supérieure à la capaciteMaxi.");
        }

        public Stockage(int numero, int capaciteMaxi) : this(numero, capaciteMaxi, capaciteMaxi) { }

        public void Stocker(int quantite)
        {
            CapaciteDispo -= quantite;
        }


        public int CapaciteMaxi { get => capaciteMaxi; set => capaciteMaxi = value; }
        public int CapaciteDispo
        {
            get => capaciteDispo;

            private set
            {
                if (value <= 0) throw new GestionPortException("la quantité à stocker dans un stockages ne peut être négative");
                else if (this.capaciteDispo >= value) this.capaciteDispo -= value;
                else throw new GestionPortException("Impossible de stocker plus que la capacité disponible dans le stockages");
            }
        }
    }
}
