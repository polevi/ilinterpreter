using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ILInterpreter
{
    public class Package
    {
        public List<Procedure> procedures = new List<Procedure>();

        public Package()
        {
        }

        public static Package Load(String fileName)
        {
            XmlSerializer s = new XmlSerializer(typeof(Package));
            Package p;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileName, Encoding.UTF8))
            {
                p = (Package)s.Deserialize(sr);
                p.Prepare();
            }
            return p;
        }

        public void Run(String name)
        {
            TStack stack = new TStack();
            MethodBodyExecutor.Execute(this, stack, name);
        }

        public void AddProcedure(Procedure p)
        {
            procedures.Add(p);
        }

        public void Prepare()
        {
            foreach (Procedure p in procedures)
            {
                p.Prepare();
            }
        }

        public Procedure[] Procedures
        {
            get
            {
                return procedures.ToArray();
            }
        }

        [XmlIgnore]
        public Dictionary<String, Procedure> AllMethods
        {
            get
            {
                if (allMethods == null)
                {
                    allMethods = new Dictionary<String, Procedure>();
                    foreach (Procedure p in procedures)
                    {
                        allMethods.Add(p.Name, p);
                    }
                }
                return allMethods;
            }
        }

        private Dictionary<String, Procedure> allMethods = null;

    }
}
