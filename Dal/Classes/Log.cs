using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Positivo.Dal.Interfaces;
using Positivo.Dal.Classes;

namespace Positivo.Dal.Classes
{

    public class LogHeader
    {
        //TestHeader table columns
        public int ID { get; set; }
        public string project {get;set;}
        public string line {get;set;}
        public string phase { get; set; }
        public string station {get;set;}
        public string version {get;set;}
        public DateTime starttime {get;set;}
        public DateTime endtime {get;set;}
        public string sn {get;set;}
        public string testresult {get;set;}
        public float elapsetime {get;set;}
    }

    public class LogResult
    {
        //TestResult table columns
        public string IdTp { get; set; }
        public double Result { get; set; }
        public double elapsetimeresult { get; set; }
    }

    public class LogDAL : IDisposable
    {
        private Line _line = new Line();
        private Station _station = new Station();
        private Phase _phase = new Phase();
        private TestSeq _testseq = new TestSeq();

        private ProjectSeq _projectseq = new ProjectSeq();
        private TestHeader _testheader = new TestHeader();
        private Collection<TestStep> _teststep = new Collection<TestStep>();
        private Collection<TestResult> _testresult = new Collection<TestResult>();

        GenericTableDAL _generictableIDAL;
        IDAL<ProjectSeq> _projectseqIDAL;
        IDAL<TestHeader> _testheadIDAL;
        TestResultDAL _testresultIDAL;
        TestStepDAL _teststepIDAL;

        public bool insert(ref LogHeader model)
        {
            bool failed = true;
            //Testes
            using (IConnection conexao = new Connection())
            {
                conexao.Open();

                //GenericTableDAL _generictableIDAL;
                GenericTable TEMP = new GenericTable();

                _generictableIDAL = new GenericTableDAL(conexao);
                
                //Line _line = new Line();
                _line.Name = model.line;
                TEMP = _generictableIDAL.findPerCode(_line);
                if (TEMP.ID == 0)
                {
                    _line.Description = "Linha de produção " + _line.Name;
                    TEMP = _generictableIDAL.insert(_line);
                }
                _line.ID = TEMP.ID;

                //Phase _phase = new Phase();
                _phase.Name = model.phase;
                TEMP = _generictableIDAL.findPerCode(_phase);
                if (TEMP.ID == 0)
                {
                    _phase.Description = "Fase de testes de " + _phase.Name;
                    TEMP = _generictableIDAL.insert(_phase);
                }
                _phase.ID = TEMP.ID;

                //Station _station = new Station();
                _station.Name = model.station;
                TEMP = _generictableIDAL.findPerCode(_station);
                if (TEMP.ID == 0)
                {
                    _station.Description = "Estação de testes de " + _station.Name;
                    TEMP = _generictableIDAL.insert(_station);
                }
                _station.ID = TEMP.ID;

                //TestSeq _testseq = new TestSeq();
                _testseq.Name = model.project + "_" + model.phase;
                TEMP = _generictableIDAL.findPerCode(_testseq);
                if (TEMP.ID == 0)
                {
                    _testseq.Description = "Sequencia de testes para " + _testseq.Name;
                    TEMP = _generictableIDAL.insert(_testseq);
                }
                _testseq.ID = TEMP.ID;

                _projectseqIDAL = new ProjectSeqDAL(conexao);
                //ProjectSeq _projectseq = new ProjectSeq();
                _projectseq = _projectseqIDAL.findPerCode(model.project);
                if (_projectseq.ID == 0)
                {
                    _projectseq.ID_Test_Seq = _testseq.ID;
                    _projectseq.Name = model.project + "_" + "<New Testplan>";
                    _projectseq = _projectseqIDAL.insert(_projectseq);
                }

                _testheadIDAL = new TestHeaderDAL(conexao);
                //TestHeader _testHeader = new TestHeader();
                _testheader.ID_Line = _line.ID;
                _testheader.ID_Phase = _phase.ID;
                _testheader.ID_Station = _station.ID;
                _testheader.ID_ProjectSeq = _projectseq.ID;
                _testheader.Version = model.version;
                _testheader.StartTime = model.starttime;
                _testheader.EndTime = model.endtime;
                _testheader.SN = model.sn;
                _testheader.Elapse_Time = model.elapsetime;
                _testheader.Test_Result = model.testresult;

                _testheader = _testheadIDAL.insert(_testheader);

                model.ID = _projectseq.ID;

               _teststepIDAL = new TestStepDAL(conexao);
               _teststep = _teststepIDAL.findPerTestSeqId(_testseq.ID);
                //_teststepIDAL.fin

                
            }
            return (failed = false);
        }

        public bool insert(Collection<LogResult> result)
        {
            bool failed = true;

            return (failed = false);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
