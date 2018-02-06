using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using Positivo.Dal;
using Positivo.AutoComp;

namespace ReportData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();
            TB tbi = new TB();
            try
            {

                var fileEntries = Directory.GetFiles("C:\\Users\\58035\\Documents\\Visual Studio 2012\\Projects\\ReportNS\\TestResult\\20170629\\").Select(fn => new FileInfo(fn)).OrderByDescending(f => f.LastWriteTime);
                //string[] fileEntries = Directory.GetFiles("C:\\_Celular_Tablets\\App_ODM\\RTS_V6.0.20160630\\TestResult\\20170629\\").Select(fn => new FileInfo(fn)).OrderByDescending(f => f.CreationTime).ToString();

                foreach (FileInfo fileName in fileEntries)
                {
                    tbi.GetData(fileName.FullName);
                }
            }
            catch { }

                tbi.saveDataToLocal();

                dgv.Width = 550;
                dgv.DataSource = tbi.dtData;

                this.Controls.Add(dgv);

            List<double> list = (from row in tbi.dtData.AsEnumerable()
                                 where row.Field<double?>("DC0016") > 0
                                 select row.Field<double>("DC0016")
                                  ).ToList<double>();

            double _correct = list.CompensationCorrection(25,21) + 0.1;

            RtsXml correctnow = new RtsXml ("C:\\Users\\58035\\Documents\\Visual Studio 2012\\Projects\\ReportNS\\S520_4G_Antenna.xml", "BC1_Loss_List_0", _correct);

        }
    }
}
