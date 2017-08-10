using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lags;
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
            lagsService.getFichierOrder("glop");
            Assert.That(lagsService.CalculerLeCA(true),Is.EqualTo(-25000));
        }
    }

    public class LagsServiceDeTest : LagsService
    {
        public override void getFichierOrder(string fileName)
        {
            this.ListOrdre = new List<Ordre>
            {
                new Ordre("DONALD", 2015001, 006, 10000.00),
                new Ordre("DAISY", 2015003, 002, 4000.00),
                new Ordre("PICSOU", 2015007, 007, 8000.00),
                new Ordre("MICKEY", 2015008, 007, 9000.00),
        
            };
        }
    }

}
