using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lags
{
    public class LagsService
    {
        public const string NOM_FICHER = "ordres.csv";
        protected List<Ordre> ordres = new List<Ordre>();
        private IConsole console = new ConsoleImplementation();

        public LagsService(IConsole console)
        {
            this.console = console;
        }

        public LagsService()
        {
        }

        public virtual void chargerOuCreerFichierOrdres(String nomFichier)
        {
            try
            {
                chargerFichierOrdres(nomFichier);
            }
            catch (FileNotFoundException e)
            {
                creerFichier(nomFichier);
            }
        }

        private void chargerFichierOrdres(string nomFichier)
        {
            using (var reader = new StreamReader(nomFichier))
            {
                while (!reader.EndOfStream)
                {
                    var champs = reader.ReadLine().Split(';');
                    chargerOrdre(champs);
                }
            }
        }

        private void chargerOrdre(string[] champs)
        {
            String id = champs[0];
            int debut = Int32.Parse(champs[1]);
            int duree = Int32.Parse(champs[2]);
            double prix = Double.Parse(champs[3]);
            Ordre ordre = new Ordre(id, debut, duree, prix);
            ordres.Add(ordre);
        }

        void creerFichier(String nomFichier)
        {
            Console.WriteLine("FICHIER ORDRES.CSV NON TROUVE. CREATION FICHIER.");
            using (TextWriter writer = File.CreateText(nomFichier))
            {
                foreach (Ordre ordre in ordres)
                {
                    insererOrdre(ordre, writer);
                }
            }
        }

        private static void insererOrdre(Ordre ordre, TextWriter writer)
        {
            var ligne = creerLigne(ordre);
            writer.WriteLine(ligne);
        }

        private static string creerLigne(Ordre ordre)
        {
            var ligne = string.Format("{0};{1};{2};{3}", ordre.id, ordre.debut, ordre.duree, ordre.prix);
            return ligne;
        }

        public void listerOrdres()
        {
            afficherEntete();
            ordres = ordres.OrderBy(ordre => ordre.debut).ToList();
            ordres.ForEach(afficherOrdre);
            afficherBordure();
        }

        private static void afficherBordure()
        {
            Console.WriteLine("{0,-8} {1:0000000} {2:00000} {3,10:N2}",
                "--------", "-------", "-----", "----------");
        }

        private static void afficherEntete()
        {
            Console.WriteLine("LISTE DES ORDRES");
            Console.WriteLine("{0,-8} {1,7} {2,5} {3,10}",
                "ID", "DEBUT", "DUREE", "PRIX");
            afficherBordure();
        }

        public void afficherOrdre(Ordre ordre)
        {
            Console.WriteLine("{0,-8} {1:0000000} {2:00000} {3,10:N2}",
                ordre.id, ordre.debut, ordre.duree, ordre.prix);

        }

        public void ajouterOrdre()
        {
            Console.WriteLine("AJOUTER UN ORDRE");
            Console.WriteLine("FORMAT = ID;DEBUT;FIN;PRIX");
            String saisieOrdre = console.lireSaisieOrdre().ToUpper();
            chargerOrdre(saisieOrdre.Split(';'));
            creerFichier(NOM_FICHER);
        }

        private double calculerChiffreAffaire(List<Ordre> ordres, bool debug)
        {
            if (ordres.Count() == 0)
                return 0.0;
            Ordre premierOrdre = ordres.ElementAt(0);
            // attention ne marche pas pour les ordres qui depassent la fin de l'année 
            // voir ticket PLAF nO 4807 
            List<Ordre> ordresQuiCommencentApresLaFinDuPremierOrdre = ordres
                .Where(ordre => ordre.debut >= premierOrdre.debut + premierOrdre.duree).ToList();
            List<Ordre> ordresSaufPremier = ordres.GetRange(1, ordres.Count() - 1);
            double chiffreAffaireIncluantPremierOrdre = premierOrdre.prix + calculerChiffreAffaire(ordresQuiCommencentApresLaFinDuPremierOrdre, debug);

            double chiffreAffaireSaufPremierOrdre = calculerChiffreAffaire(ordresSaufPremier, debug);
            Console.Write(debug ? String.Format("{0,10:N2}\n", Math.Max(chiffreAffaireIncluantPremierOrdre, chiffreAffaireSaufPremierOrdre)):".");
            return Math.Max(chiffreAffaireIncluantPremierOrdre, chiffreAffaireSaufPremierOrdre); // LOL
        }

        public void supprimerOrdre()
        {
            Console.WriteLine("SUPPRIMER UN ORDRE");
            Console.Write("ID:");
            string id = Console.ReadLine().ToUpper();
            this.ordres = ordres.Where(ordre => ordre.id != id).ToList();
            creerFichier(NOM_FICHER);
        }

        public double calculerLeChiffreAffaire(bool debug)
        {
            Console.WriteLine("CALCUL CA..");
            ordres = ordres.OrderBy(ordre => ordre.debut).ToList();
            double chiffreAffaire = calculerChiffreAffaire(ordres, debug);
            Console.WriteLine("CA: {0,10:N2}", chiffreAffaire);
            return chiffreAffaire;
        }

    }
}
