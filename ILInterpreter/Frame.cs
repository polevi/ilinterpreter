using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILInterpreter
{
    public class Frame
    {
        private Package package;
        private TStack stack;
        private TValue[] locals;
        private TValue[] arguments;

        public Frame(Package package, TStack stack)
        {
            this.package = package;
            this.stack = stack;
            this.locals = new TValue[256];
        }

        public void LoadArguments(Type[] args)
        {
            if (args.Length == 0 || stack.Count == 0)
                return;
            arguments = new TValue[args.Length];
            for (int i = args.Length - 1; i >= 0; i--)
                arguments[i] = stack.Pop().ConvertTo(args[i]);
        }

        public void Push(TValue value)
        {
            stack.Push(value);
        }

        public TValue Pop()
        {
            return stack.Pop();
        }

        public TValue[] Locals
        {
            get
            {
                return locals;
            }
        }

        public TValue[] Arguments
        {
            get
            {
                return arguments;
            }
        }

        public TStack Stack
        {
            get
            {
                return stack;
            }
        }

        public Package Package
        {
            get
            {
                return this.package;
            }
        }
    }
}
