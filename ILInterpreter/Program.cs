using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ILInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileName = new System.IO.FileInfo(Assembly.GetExecutingAssembly().Location).Directory.Parent.Parent.Parent.FullName + @"\ILDisassembler\bin\Debug\TestLibrary.dll.xml";

            Package.Load(fileName).Run("local.TestClass::Main(System.String[])");

            Console.ReadLine();
        }
    }
}
