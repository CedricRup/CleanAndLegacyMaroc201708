using System;


namespace Lags
{
    
    class Program
    {
        const bool debug = true;

        static void Main(string[] args)
        {
            LagsService service = new LagsService();
            service.chargerOuCreerFichierOrdres("ORDRES.CSV");
            bool flag = false;
            while (!flag)
            {
                Char commande = 'Z';
                while (commande != 'A' && commande != 'L' && commande != 'S' && commande != 'Q' && commande != 'C')
                {
                    Console.WriteLine("A)JOUTER UN ORDRE  L)ISTER   C)ALCULER CA  S)UPPRIMER  Q)UITTER");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    commande = Char.ToUpper(keyInfo.KeyChar);
                    Console.WriteLine();
                }
                switch (commande)
                {
                    case 'Q':
                        {
                            flag = true;
                            break;
                        }
                    case 'L':
                        {
                            service.listerOrdres();
                            break;
                        }
                    case 'A':
                        {
                            service.ajouterOrdre();
                            break;
                        }
                    case 'S':
                        {
                            service.supprimerOrdre();
                            break;
                        }
                    case 'C':
                        {
                            service.calculerLeChiffreAffaire(debug);
                            break;
                        }
                }
            }
        }       
    }
}