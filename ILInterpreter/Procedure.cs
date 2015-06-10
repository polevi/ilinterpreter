using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILInterpreter
{
    public class Procedure
    {
        private Type[] arguments;
        public String Name { get; set; }
        public List<ILInstruction> instructions = new List<ILInstruction>();

        public Procedure()
        {
        }

        public Procedure(String name)
        {
            this.Name = name;
        }

        public void AddInstruction(ILInstruction instruction)
        {
            instructions.Add(instruction);
        }

        public void Prepare()
        {
            arguments = ArgumentsFromString(Name.Split(new String[] { "::" }, StringSplitOptions.RemoveEmptyEntries)[1]);

            foreach (ILInstruction ili in instructions)
            {
                ili.Prepare();
            }
        }

        public ILInstruction[] Instructions
        {
            get
            {
                return instructions.ToArray();
            }
        }

        public Type[] GetArguments()
        {
            return arguments;
        }

        private Type[] ArgumentsFromString(String name)
        {
            int pos = name.IndexOf('(');
            String n = name.Substring(0, pos);
            String a = name.Substring(pos + 1, name.Length - pos - 2);

            Type[] args = null;
            if (!String.IsNullOrEmpty(a))
            {
                String[] sargs;
                sargs = a.Split(',');
                args = new Type[sargs.Length];
                for (int i = 0; i < sargs.Length; i++)
                {
                    args[i] = TypeInfo.GetType(sargs[i]);
                }
            }
            return args != null ? args : new Type[] { };
        }
    }
}
