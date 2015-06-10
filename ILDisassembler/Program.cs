using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ILDisassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            Globals.LoadOpCodes();

            Assembly a = Assembly.GetAssembly(typeof(TestLibrary.TestClass));
            LocalScope scope = new LocalScope(a);

            Package package = new Package();

            using (System.IO.StreamWriter wr = new System.IO.StreamWriter(a.Location + ".xml", false, Encoding.UTF8))
            {
                foreach (MethodInfo mi in FindMethods(a))
                {
                    MethodBodyReader mr = new MethodBodyReader(mi, scope);

                    String procName = String.Format("{0}::{1}{2}", Globals.ProcessSpecialTypes(mi.ReflectedType, a), mi.Name, Globals.ParametersAsString(mi.GetParameters()));
                    Procedure p = new Procedure(procName);
                    foreach (ILInstruction ili in mr.instructions)
                    {
                        ILInstr i = new ILInstr(ili, scope);
                        p.AddInstruction(i);
                    }

                    package.AddProcedure(p);
                }

                package.Prepare();

                XmlSerializer s = new XmlSerializer(typeof(Package));
                s.Serialize(wr, package);
            }

            Console.WriteLine("ok");
            Console.ReadLine();
        }

        static IEnumerable<MethodInfo> FindMethods(Assembly a)
        {
            Module[] modules = a.GetModules();
            for (int i = 0; i < modules.Length; i++)
            {
                Type[] types = modules[i].GetTypes();
                for (int k = 0; k < types.Length; k++)
                {
                    MethodInfo[] mis = types[k].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                    for (int j = 0; j < mis.Length; j++)
                    {
                        yield return mis[j];
                    }
                }
            }
        }
    }
}
