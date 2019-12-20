using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher
{
    class Program
    {
        //variables to lock and unlock entities
        public static bool lockedTM, lockedOW;

        static void Main(string[] args)
        {
            OwnWatcher ow = new OwnWatcher(XMLFile.PATH);

            TableMonitor tm = new TableMonitor(Strings.ChangesOnProductTableName);

            Console.ReadLine();
        }
    }
}
