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

namespace Positivo.Log.Classes
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

        private IConnection conexao;
        private LogHeader model;
        private Line _line = null;
        private Station _station = null;
        private Phase _phase = null;
        private TestSeq _testseq = null;

        private ProjectSeq _projectseq;
        private TestHeader _testheader;
        private Collection<TestStep> _teststep;
        private Collection<TestResult> _testresult;

        GenericTableDAL _generictableIDAL;
        IDAL<ProjectSeq> _projectseqIDAL;
        IDAL<TestHeader> _testheadIDAL;
        TestResultDAL _testresultIDAL;
        TestStepDAL _teststepIDAL;

        public LogDAL()
        {
            LogHeader model = new LogHeader();
            Line _line = new Line();
            Station _station = new Station();
            Phase _phase = new Phase();
            TestSeq _testseq = new TestSeq();

            ProjectSeq _projectseq = new ProjectSeq();
            TestHeader _testheader = new TestHeader();
            Collection<TestStep> _teststep = new Collection<TestStep>();
            Collection<TestResult> _testresult = new Collection<TestResult>();

            startConn();

        }

        private bool startConn()
        {
            bool failed = true;

            conexao = new Connection();
            conexao.Open();

            failed = false;
            return failed;
        }

        private bool insertLine()
        {
            bool failed = true;
            GenericTable TEMP = new GenericTable();
            _generictableIDAL = new GenericTableDAL(conexao);

            /*
             * Verifica a existência da linha, senão insere nova linha;
            */
            _line.Name = model.line;
            TEMP = _generictableIDAL.findPerCode(_line);
            if (TEMP.ID == 0)
            {
                _line.Description = "Linha de produção " + _line.Name;
                TEMP = _generictableIDAL.insert(_line);
            }
            _line.ID = TEMP.ID;

            failed = false;
            return failed;
        }

        private bool insertPhase()
        {
            bool failed = true;
            GenericTable TEMP = new GenericTable();
            /*
            * Verifica a existência da phase, senão insere nova phase;
            */
            _phase.Name = model.phase;
            TEMP = _generictableIDAL.findPerCode(_phase);
            if (TEMP.ID == 0)
            {
                _phase.Description = "Fase de testes de " + _phase.Name;
                TEMP = _generictableIDAL.insert(_phase);
            }
            _phase.ID = TEMP.ID;

            failed = false;
            return failed;
        }

        private bool insertStation()
        {
            bool failed = true;
            GenericTable TEMP = new GenericTable();
            /*
            * Verifica a existência da estação, senão insere nova estação;
            */
            _station.Name = model.station;
            TEMP = _generictableIDAL.findPerCode(_station);
            if (TEMP.ID == 0)
            {
                _station.Description = "Estação de testes de " + _station.Name;
                TEMP = _generictableIDAL.insert(_station);
            }
            _station.ID = TEMP.ID;

            failed = false;
            return failed;
        }

        private bool insertTestSeq()
        {
            bool failed = true;
            GenericTable TEMP = new GenericTable();
            /*
            * Verifica a existência da Sequencia de testes, senão insere nova sequencia de testes;
            */
            _testseq.Name = model.project + "_" + model.phase;
            TEMP = _generictableIDAL.findPerCode(_testseq);
            if (TEMP.ID == 0)
            {
                _testseq.Description = "Sequencia de testes para " + _testseq.Name;
                TEMP = _generictableIDAL.insert(_testseq);
            }
            _testseq.ID = TEMP.ID;

            failed = false;
            return failed;
        }

        private bool insertProjectSeq()
        {
            bool failed = true;
            /*
            * Verifica a existência da associação entre projeto x sequencia de testes, senão insere nova associação;
            */
            _projectseqIDAL = new ProjectSeqDAL(conexao);
            _projectseq = _projectseqIDAL.findPerCode(model.project);
            if (_projectseq.ID == 0)
            {
                _projectseq.ID_TestSeq = _testseq.ID;
                _projectseq.Name = model.project + "_" + "<New Testplan>";
                _projectseq = _projectseqIDAL.insert(_projectseq);
            }
            
            failed = false;
            return failed;
        }

        private bool insertTestHeader()
        {
            bool failed = true;

            /*
            * Insere novo resultado de testes da tabela TestHeader;
            */
            _testheadIDAL = new TestHeaderDAL(conexao);
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

            failed = false;
            return failed;
        }

        private bool insertTestStep(Collection<LogResult> result)
        {
            bool failed = true;



            failed = false;
            return failed;
        }

        private bool insertTestResult()
        {
            bool failed = true;

            failed = false;
            return failed;
        }


        public bool insert(ref LogHeader _model)
        {
            bool failed = true;

            model = _model;

            using (conexao)
            {
                if (insertLine())
                    return failed;

                if (insertPhase())
                    return failed;

                if (insertStation())
                    return failed;

                if (insertTestSeq())
                    return failed;

                if (insertProjectSeq())
                    return failed;

                if (insertTestHeader())
                    return failed;

                _teststepIDAL = new TestStepDAL(conexao);
                _teststep = _teststepIDAL.findPerTestSeqId(_testseq.ID);

                failed = false;
                
            }
            return failed;
        }

        public void Dispose()
        {
            conexao.Close();
            conexao.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
