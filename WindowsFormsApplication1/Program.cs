using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Positivo.Dal.Interfaces;
using Positivo.Dal.Classes;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            LogHeader header = new LogHeader();
            header.project = "Projeto 10";
            header.line = "4.11";
            header.phase = "CHECKIMEI";
            header.station = "CHECKIMEI_5";
            header.version = "V5.1";
            header.starttime = DateTime.Parse("2017-03-16 10:31:51");
            header.endtime = DateTime.Now;
            header.sn = "4ABCD5678";
            header.testresult = "PASS";
            header.elapsetime = 70;

            LogDAL _teste = new LogDAL();
            _teste.insert(ref header);

            Collection<LogResult> result = new Collection<LogResult>();


        }
    }
}
