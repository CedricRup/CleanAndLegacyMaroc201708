using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lags;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Tests
{
    [TestFixture]
    public class LagsServiceTest
    {
        [Test]
        public void CasDuSlide()
        {
            var lagsService = new LagsServiceDeTest();
            lagsService.chargerOuCreerFichierOrdres("glop");
            Assert.That(lagsService.calculerLeChiffreAffaire(true),Is.EqualTo(19000.0d));
        }

        [Test]
        public void TesterAjoutOrdre()
        {
            var console = new Mock<IConsole>();
            console.Setup(c => c.lireSaisieOrdre()).Returns("ordre;2017001;001;10000");
            File.Delete(LagsService.NOM_FICHER);
            var lagsService = new LagsService(console.Object);
            lagsService.ajouterOrdre();
            var lignes = File.ReadAllLines(LagsService.NOM_FICHER);
            Assert.That(lignes.First(), Is.EqualTo("ORDRE;2017001;1;10000")  );
        }
    }

    public class LagsServiceDeTest : LagsService
    {

        public override void chargerOuCreerFichierOrdres(string nomFichier)
        {
            this.ordres = new List<Ordre>
            {
                new Ordre("DONALD", 2015001, 006, 10000.00),
                new Ordre("DAISY", 2015003, 002, 4000.00),
                new Ordre("PICSOU", 2015007, 007, 8000.00),
                new Ordre("MICKEY", 2015008, 007, 9000.00),
        
            };
        }
    }

}
