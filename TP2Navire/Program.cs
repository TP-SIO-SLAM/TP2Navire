using GestionNavire.Classesmetier;
using GestionNavire.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNavire.Application
{
    internal class Program
    {
        private static Port port;

        static void Main(string[] args) {
            try
            {
                port = new Port("Toulon");

                try { TesterEnregistrerArrivee(); }
                catch (GestionPortException ex)
                { Console.WriteLine(ex.Message); }
                try { TesterEnregistrerArriveeV2(); }
                catch (GestionPortException ex)
                { Console.WriteLine(ex.Message); }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("--------Début des déchargements----------");
                Console.WriteLine("-----------------------------------------");
                Console.ResetColor();

                AjouterStockages();
                TesterDechargerNavires();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("----------Fin des déchargements----------");
                Console.WriteLine("-----------------------------------------");
                Console.ResetColor();

                try { TesterEnregistrerDepart(); }
                catch (GestionPortException ex) {  Console.WriteLine(ex.Message); }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Fin normale du programme");
                Console.ResetColor();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Console.ReadKey(); }
        }

        private static void Instanciations()
        {
            try
            {
                Navire navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827, 0);
                navire = new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500, 20);
                navire = new Navire("IMO8715871", "MSC PILAR", "Porte-conteneurs", 67183, 30);
                navire = new Navire("IMO9235232", "FORTUNE TRADER", "Cargo", 74750, 50);
                Navire petitNavire = new Navire("IMO9574004", "TRITON SEAHAWK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void TesterEnregistrerArrivee()
        {
            Navire navire = null;
            try
            {
                navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827, 12000);
                port.EnregistrerArrivee(navire);
                navire = new Navire("IMO9427633", "MSC Bool", "Hydrocarbures", 156827, 12000);

                /*navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 500, 50);
                port.EnregistrerArrivee(navire);
               *//* navire = new Navire("IMO9427639", "Copper Spirit", "Hydrocarbures", 156827);
                port.EnregistrerArrivee(navire);*//*
                navire = new Navire("IMO9427635", "Copper Spirit", "Hydrocarbures", 500, 100);
                port.EnregistrerArrivee(navire);
                *//*navire.Decharger(0);
                navire.EstDecharge();*/
                /*navire = new Navire("IMO9427632", "Copper Spirit", "Hydrocarbures", 156827);
                port.EnregistrerDepart(navire.Imo);*/
            }
            catch (GestionPortException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void TesterEnregistrerArriveeV2()
        {
            Navire navire = null;
            try
            {
                port.EnregistrerArrivee(new Navire("IMO9839272", "MSC Isabella", "Porte-conteneurs", 197500, 50));
                port.EnregistrerArrivee(new Navire("IMO8715871", "MSC PILAR"));
                port.EnregistrerArrivee(new Navire("IMO9235232", "FORTUNE TRADER", "Cargo", 74750, 0));
                port.EnregistrerArrivee(new Navire("IMO9405423", "SERENA", "Tanker", 158583, 0));
                port.EnregistrerArrivee(new Navire("IMO9574004", "TRITON SEAHAWK", "Hydrocarbures", 51201, 500));
                port.EnregistrerArrivee(new Navire("IMO9748681", "NORDIC SPACE", "Tanker", 157587, 0));

            }
            catch (GestionPortException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException)
            {
                throw new GestionPortException("Le navire " + navire.Imo + " est déjà enregistré");
            }
        }

        private static void TesterEnregistrerDepart()
        {
            try
            {
                port.EnregistrerDepart("IMO9427639");
                port.EnregistrerDepart("IMO9405423");
                port.EnregistrerDepart("IMO1111111");
            }
            catch (Exception ex)
            {
                throw new GestionPortException(ex.Message);
            }
        }

        public static void TesterInstanciationsStockage()
        {
            try
            { new Stockage(1, 15000); }
            catch (GestionPortException ex)
            { Console.WriteLine(ex.Message); }

            try
            { new Stockage(2, 12000, 10000); }
            catch (GestionPortException ex)
            { Console.WriteLine(ex.Message); }

            try
            { new Stockage(3, -25000, -10000); }
            catch (GestionPortException ex)
            { Console.WriteLine(ex.Message); }

            try
            { new Stockage(4, 15000, 20000); }
            catch (GestionPortException ex)
            { Console.WriteLine(ex.Message); }

        }

        public static void AjouterStockages()
        {
            port.AjoutStockage(new Stockage(1, 160000));
            port.AjoutStockage(new Stockage(2, 12000));
            port.AjoutStockage(new Stockage(3, 25000));
            port.AjoutStockage(new Stockage(4, 15000));
            port.AjoutStockage(new Stockage(5, 15000));
            port.AjoutStockage(new Stockage(6, 15000));
            port.AjoutStockage(new Stockage(7, 15000));
            port.AjoutStockage(new Stockage(8, 15000));
            port.AjoutStockage(new Stockage(9, 35000));
            port.AjoutStockage(new Stockage(10, 19000));
        }


        public static void TesterDechargerNavires()
        {
            try
            {
                string imo = "IMO9839272";
                port.Dechargement(imo);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Navire " + imo + " déchargé");
                Console.ResetColor();
                port.EnregistrerDepart(imo);
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
            try
            {
                string imo = "IMO1111111";
                port.Dechargement(imo);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Navire " + imo + " déchargé");
                Console.ResetColor();
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
            try
            {
                string imo = "IMO9574004";
                port.Dechargement(imo);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Navire " + imo + " déchargé");
                Console.ResetColor();
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
            try
            {
                port.EnregistrerArrivee(new Classesmetier.Navire("IMO9786841", "EVER GLOBE", "Porte-conteneurs", 198937, 19000));
                string imo = "IMO9786841";
                port.Dechargement(imo);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Navire " + imo + " déchargé");
                Console.ResetColor();
                port.EnregistrerDepart(imo);
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
            try
            {
                port.EnregistrerArrivee(new Classesmetier.Navire("IMO9776432", "CHACGM LOUIS BLERIOT", "Porte-conteneurs", 202684, 19000));
                string imo = "IMO9776432";
                port.Dechargement(imo);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Navire " + imo + " déchargé");
                Console.ResetColor();
            }
            catch (GestionPortException ex) { Console.WriteLine(ex.Message); }
        }
    }
}
