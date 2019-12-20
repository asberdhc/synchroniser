using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Watcher
{
    class OwnWatcher
    {
        private FileSystemWatcher sw;
        private Data.D d;

        public OwnWatcher(string path)
        {
            sw = new FileSystemWatcher();
            sw.Path = path;
            sw.Deleted += FileDeleted;
            sw.Changed += FileSaved;
            sw.EnableRaisingEvents = true;
            d = new Data.D();
        }

        public void FileDeleted(object source, FileSystemEventArgs e)
        {
            if (!Program.lockedOW)
            {
                Program.lockedTM = true;
                d.Delete(int.Parse(e.Name.Split('.')[0]));
                Program.lockedTM = false;
            }
            Console.WriteLine("eliminado" + e.Name);
        }

        public void FileSaved(object source, FileSystemEventArgs e)
        {
            if (!Program.lockedTM)
            {
                Program.lockedTM = true;
                if (e.Name.Split('.')[0] == "0")
                    d.Insert(XMLFile.DeserializeList<Prod>(e.Name));
                else
                    d.Update(XMLFile.DeserializeList<Prod>(e.Name));
                Program.lockedTM = false;
            }
            Console.WriteLine("guardado" + e.Name);
        }
    }
}
