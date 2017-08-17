using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lags
{
    public interface IConsole
    {
        String lireSaisieOrdre();
    }

    class ConsoleImplementation : IConsole
    {
        public string lireSaisieOrdre()
        {
            return Console.ReadLine();
        }
    }
}
