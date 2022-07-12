using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace ProviderDemo
{
    internal class CsvLine: DynamicObject
    {
        public string header;
        public string lineContent;
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            var headerFields = header.Split(',').ToList();
            var indexOf = headerFields.IndexOf(binder.Name);
            if (indexOf != -1)
            {
                var lineContnet = lineContent.Split(',');
                result=lineContnet[indexOf];
                return true;
            }

            return false;

        }
    }

    class CsvProvider
    {
        private string path;

        public CsvProvider(string filePath)
        {
            this.path = filePath;
        }

        public IEnumerable<CsvLine> GetCsvLines()
        {

            var lines= new List<CsvLine>();

            System.IO.StreamReader _r = new System.IO.StreamReader(path);
            try
            {
                string header = _r.ReadLine();
                
                while (!_r.EndOfStream)
                {
                    CsvLine line = new CsvLine();
                    line.header=header;
                    line.lineContent = _r.ReadLine();
                    lines.Add(line);
                }
            }
            finally
            {
                _r.Close();
            }

            return lines;


        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //  "..//..//Patients.csv"

            //var result = list.Where((p) => p.Location == "blr");

            var provider = new CsvProvider("..//..//Patients.csv");
            var lines=provider.GetCsvLines();

           var result= lines.Where((dynamic line) => Int32.Parse(line.Grade) > 30);

            foreach (dynamic item in result)
            {
                Console.WriteLine(item.Mrn);
            }
        }
    }

    class ElasticType:DynamicObject
    {
        internal Dictionary<string, object> _stateBag = new Dictionary<string, object>();
        
        //set accessor
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _stateBag[binder.Name] = value;
            return true;
        }

        //get accessor
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (this._stateBag.ContainsKey(binder.Name))
            {
                result = this._stateBag[binder.Name];
                return true;
            }
            return false;
        }

    }

   
}
