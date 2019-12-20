using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher
{
    class TableMonitor
    {
        private string tableName;
        private Data.D d;
        private int currentId;
        private int newId;

        public TableMonitor(string tableName)
        {
            //initialize all variables
            this.tableName = tableName;
            d = new Data.D();
            currentId = d.GetLastRecordOn(tableName).IdLog;

            //start the monitor to the data base
            Start();//23
        }

        private async Task Start()
        {
            Console.WriteLine("Monitoring Started");
            while (true)
            {
                await StartAsync();
            }
        }

        private async Task StartAsync()
        {
            Console.Write(Program.lockedTM ? "L" : "U");
            if (!Program.lockedTM)
            {
                //get a new id to know if there are changes
                newId = d.GetLastRecordOn(tableName).IdLog;
                if (currentId != newId)
                {
                    List<ChangeOnProduct> newRows = d.GetAllRecordsFrom(tableName, currentId + 1);
                    foreach (ChangeOnProduct row in newRows)
                    {
                        switch (row.ActionMade)
                        {
                            case 1:
                                InsertWasMade(row.IdProduct);
                                break;
                            case 2:
                                DeleteWasMade(row.IdProduct);
                                break;
                            case 3:
                                UpdateWaasMade(row.IdProduct);
                                break;
                            default:
                                Console.WriteLine("Error");
                                break;
                        }
                    }
                }
            }
        }

        private void DeleteWasMade(int IdProduct)
        {
            //We block the OunWatcher instance
            Program.lockedOW = true;

            //do the job
            XMLFile.Delete(IdProduct + ".xml");

            //unlock and update currentId
            currentId = d.GetLastRecordOn(tableName).IdLog;
            Program.lockedOW = false;
        }

        private void UpdateWaasMade(int IdProduct)
        {
            //We block the OunWatcher instance
            Program.lockedOW = true;

            //do the job
            var UpdateProduct = d.GetProductByID(IdProduct);
            XMLFile.Delete(IdProduct + ".xml");
            XMLFile.SerializeList(UpdateProduct, IdProduct + ".xml");

            //unlock and update currentId
            currentId = d.GetLastRecordOn(tableName).IdLog;
            Program.lockedOW = false;
        }

        private void InsertWasMade(int IdProduct)
        {
            //We block the OunWatcher instance
            Program.lockedOW = true;

            //do the job
            Prod p = d.GetProductByID(IdProduct);
            XMLFile.SerializeList<Prod>(p, p.Id + ".xml");
            Console.WriteLine("xml created");

            //unlock and update currentId
            currentId = d.GetLastRecordOn(tableName).IdLog;
            Program.lockedOW = false;
        }
    }
}
